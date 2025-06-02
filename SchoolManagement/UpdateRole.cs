using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Office.Interop.Excel;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.Design.AxImporter;


namespace SchoolManagement
{
    public partial class UpdateRole : KryptonForm
    {
        private const int CS_DropShadow = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = CS_DropShadow;
                return cp;
            }
        }
        private int action; // 0 - add, 1 - edit
        private bool isSelected = false;
        private int currFrom = 1;
        private int pageSize = 10;


        public static string ClassSectionID;
        public static string SubjectID;
        public static int limited;

        private Dictionary<string, List<string>> selectedColumnsByPrivilege = new Dictionary<string, List<string>>();

        public string RoleName { get; private set; }
        public UpdateRole(string roleName)
        {
            InitializeComponent();
            RoleName = roleName;
            this.Load += UpdateRole_Load;
            this.dgvTables.CellClick += dgvTables_CellClick;
            this.dgvViews.CellClick += dvgViews_CellClick;
        }

        private void UpdateRole_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RoleName))
            {
                ShowUpdateInformation(RoleName);
                label7.Text = RoleName;
            }
            else
            {
                MessageBox.Show("No role selected to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void ShowUpdateInformation(string roleName)
        {
            try
            {
                string queryTables = "SELECT table_name FROM all_tables WHERE table_name LIKE 'QLDH_%' AND table_name != 'QLDH_ADMIN' ORDER BY table_name";
                using (OracleCommand cmd = new OracleCommand(queryTables, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);
                    dt.Columns.Add("INSERT", typeof(string));
                    dt.Columns.Add("DELETE", typeof(string));
                    dt.Columns.Add("SELECT", typeof(string));
                    dt.Columns.Add("UPDATE", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        row["SELECT"] = "x";
                        row["INSERT"] = "x";
                        row["UPDATE"] = "x";
                        row["DELETE"] = "x";
                    }

                    // Check quyền đã được cấp
                    foreach (DataRow row in dt.Rows)
                    {
                        string tableName = row["TABLE_NAME"].ToString();
                        string queryPrivs = @"SELECT privilege
                                     FROM dba_tab_privs
                                     WHERE grantee = :roleName AND table_name = :tableName";

                        using (OracleCommand command = new OracleCommand(queryPrivs, DatabaseSession.Connection))
                        {
                            command.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = roleName.ToUpper();
                            command.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName.ToUpper();

                            using (OracleDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string privilege = reader["PRIVILEGE"].ToString().ToUpper();
                                    if (privilege == "SELECT") row["SELECT"] = "v";
                                    if (privilege == "INSERT") row["INSERT"] = "v";
                                    if (privilege == "UPDATE") row["UPDATE"] = "v";
                                    if (privilege == "DELETE") row["DELETE"] = "v";
                                }
                            }
                        }
                    }


                    dgvTables.DataSource = dt;
                    dgvTables.Columns["TABLE_NAME"].HeaderText = "TABLE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        private void dgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                string columnName = dgvTables.Columns[e.ColumnIndex].Name;
                if (columnName == "SELECT" || columnName == "UPDATE")
                {
                    string roleName = RoleName;
                    string tableName = dgvTables.Rows[e.RowIndex].Cells["TABLE_NAME"].Value.ToString();

                    // Lấy danh sách cột bảng
                    List<string> columns = new List<string>();
                    string queryColumns = @"SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = :tableName ORDER BY COLUMN_ID";
                    using (OracleCommand cmd = new OracleCommand(queryColumns, DatabaseSession.Connection))
                    {
                        cmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName.ToUpper();

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                columns.Add(reader["COLUMN_NAME"].ToString());
                            }
                        }
                    }

                    // Tạo key cho Dictionary lưu trạng thái
                    string key = $"{roleName.ToUpper()}|{tableName.ToUpper()}|{columnName.ToUpper()}";

                    // Lấy danh sách cột đã chọn trước đó nếu có
                    List<string> selectedColumns;
                    if (!selectedColumnsByPrivilege.TryGetValue(key, out selectedColumns))
                        selectedColumns = new List<string>();

                    using (Form form = new Form())
                    {
                        form.Text = $"Chọn cột cho quyền {columnName} - Bảng {tableName}";
                        form.StartPosition = FormStartPosition.CenterParent;
                        form.Size = new Size(300, 400);
                        form.FormBorderStyle = FormBorderStyle.FixedDialog;
                        form.MaximizeBox = false;
                        form.MinimizeBox = false;

                        System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel()
                        {
                            Dock = DockStyle.Top,
                            AutoScroll = true,
                            Height = 300,
                        };
                        form.Controls.Add(panel);

                        int y = 10;
                        foreach (var col in columns)
                        {
                            var cb = new System.Windows.Forms.CheckBox()
                            {
                                Text = col,
                                Location = new System.Drawing.Point(10, y),
                                AutoSize = true,
                                Checked = selectedColumns.Contains(col)
                            };
                            panel.Controls.Add(cb);
                            y += 25;
                        }

                        System.Windows.Forms.Button btnOk = new System.Windows.Forms.Button()
                        {
                            Text = "OK",
                            DialogResult = DialogResult.OK,
                            Location = new System.Drawing.Point(form.ClientSize.Width / 2 - 80, panel.Bottom + 10),
                            Size = new Size(75, 30)
                        };
                        form.Controls.Add(btnOk);

                        System.Windows.Forms.Button btnCancel = new System.Windows.Forms.Button()
                        {
                            Text = "Cancel",
                            DialogResult = DialogResult.Cancel,
                            Location = new System.Drawing.Point(form.ClientSize.Width / 2 + 10, panel.Bottom + 10),
                            Size = new Size(75, 30)
                        };
                        form.Controls.Add(btnCancel);

                        form.AcceptButton = btnOk;
                        form.CancelButton = btnCancel;

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            List<string> newSelectedCols = panel.Controls.OfType<System.Windows.Forms.CheckBox>()
                                .Where(cb => cb.Checked)
                                .Select(cb => cb.Text)
                                .ToList();

                            // Lưu lại trạng thái đã chọn
                            selectedColumnsByPrivilege[key] = newSelectedCols;

                            // Cập nhật cell dgvTables thành "v"
                            if (newSelectedCols.Count > 0)
                                dgvTables.Rows[e.RowIndex].Cells[columnName].Value = "v";
                            else
                                dgvTables.Rows[e.RowIndex].Cells[columnName].Value = "x";

                            // Bạn có thể xử lý thêm lưu quyền theo cột nếu cần
                        }
                    }
                }
                else if (columnName == "INSERT" || columnName == "DELETE")
                {
                    DataGridViewCell cell = dgvTables.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string currentValue = cell.Value?.ToString();

                    cell.Value = currentValue == "v" ? "x" : "v";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while toggling permission:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dvgViews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                string columnName = dgvTables.Columns[e.ColumnIndex].Name;
                if (columnName == "SELECT" || columnName == "UPDATE")
                {
                    // Lấy tên bảng ở dòng hiện tại
                    string tableName = dgvTables.Rows[e.RowIndex].Cells["TABLE_NAME"].Value?.ToString();

                    // Hiện MessageBox
                    MessageBox.Show($"Bạn vừa click vào quyền {columnName} của bảng {tableName}.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Nếu cần thêm xử lý khác thì viết ở đây
                }
                else if (columnName == "INSERT" || columnName == "DELETE")
                {
                    DataGridViewCell cell = dgvTables.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string currentValue = cell.Value?.ToString();

                    cell.Value = currentValue == "v" ? "" : "v";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while toggling permission:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbUsers_Click(object sender, EventArgs e)
        {
            UsersManager userManager = new UsersManager();
            this.Hide();
            userManager.ShowDialog();
            this.Close();
        }

        private void lbRoles_Click(object sender, EventArgs e)
        {
            RoleManager roleManager = new RoleManager();
            this.Hide();
            roleManager.ShowDialog();
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            StudentManager student = new StudentManager();
            this.Hide();
            student.ShowDialog();
            this.Close();
        }

        private void lbPersonnel_Click(object sender, EventArgs e)
        {
            PersonnelManager personnelManager = new PersonnelManager();
            this.Hide();
            personnelManager.ShowDialog();
            this.Close();
        }

        private void lbProfile_Click(object sender, EventArgs e)
        {
            AdminProfile myProfile = new AdminProfile();
            this.Hide();
            myProfile.ShowDialog();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string roleName = RoleName;
                foreach (DataGridViewRow row in dgvTables.Rows)
                {
                    string tableName = row.Cells["TABLE_NAME"].Value.ToString();

                    foreach (string privilege in new[] { "SELECT", "INSERT", "UPDATE", "DELETE" })
                    {
                        var cellValue = row.Cells[privilege].Value?.ToString();
                        if (cellValue != "v") continue;

                        string sql = "";

                        if ((privilege == "SELECT" || privilege == "UPDATE"))
                        {
                            // Kiểm tra nếu có lựa chọn cột từ form phụ
                            string key = $"{roleName}|{tableName}|{privilege}";
                            if (selectedColumnsByPrivilege.TryGetValue(key, out var columns) && columns.Count > 0)
                            {
                                string columnList = string.Join(", ", columns);
                                sql = $"GRANT {privilege}({columnList}) ON PDB_ADMIN.{tableName} TO {roleName}";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (privilege == "INSERT" || privilege == "DELETE")
                        {
                            sql = $"GRANT {privilege} ON PDB_ADMIN.{tableName} TO {roleName}";
                        }

                        //MessageBox.Show("SQL: " + sql);

                        using (OracleCommand cmd = new OracleCommand(sql, DatabaseSession.Connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Grant privileges successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:\n" +  ex.Message);
            }
        }


        private void label7_Click(object sender, EventArgs e)
        {
            label7.Text = RoleName;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            AuditView auditView = new AuditView();
            this.Hide();
            auditView.ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void addNoticeBtn_Click(object sender, EventArgs e)
        {
            AddNotification addNotification = new AddNotification();
            this.Hide();
            addNotification.ShowDialog();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
