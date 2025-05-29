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
using Oracle.ManagedDataAccess.Client;


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

        public string RoleName { get; private set; }
        public UpdateRole(string roleName)
        {
            InitializeComponent();
            RoleName = roleName;
            this.Load += UpdateRole_Load;
            this.dgvTables.CellClick += dgvTables_CellClick;
            this.dgvViews.CellClick += dvgViews_CellClick;
            this.dgvProcs.CellClick += dvgProcs_CellClick;

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
                OracleConnection conn = DatabaseSession.Connection;
                if (conn == null || conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Failed to connect with database!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy danh sách bảng
                string queryTables = "SELECT table_name FROM all_tables WHERE table_name LIKE 'QLDH_%' AND table_name != 'QLDH_ADMIN' ORDER BY table_name";
                OracleDataAdapter tableAdapter = new OracleDataAdapter(queryTables, conn); // Renamed to 'tableAdapter'
                DataTable dtTables = new DataTable();
                tableAdapter.Fill(dtTables);

                // Thêm cột INSERT, DELETE, SELECT và UPDATE vào DataTable
                dtTables.Columns.Add("INSERT", typeof(string));
                dtTables.Columns.Add("DELETE", typeof(string));
                dtTables.Columns.Add("SELECT_COLUMN", typeof(string)); // Thêm cột SELECT
                dtTables.Columns.Add("UPDATE_COLUMN", typeof(string)); // Thêm cột UPDATE

                // Gán DataSource cho DataGridView
                dgvTables.DataSource = dtTables;

                // Ẩn các cột SELECT_COLUMN và UPDATE_COLUMN (chúng ta sẽ dùng ComboBox thay thế)
                dgvTables.Columns["SELECT_COLUMN"].Visible = false;
                dgvTables.Columns["UPDATE_COLUMN"].Visible = false;

                // Tạo và cấu hình cột SELECT là ComboBox
                DataGridViewComboBoxColumn selectColumn = new DataGridViewComboBoxColumn();
                selectColumn.Name = "SELECT";
                selectColumn.HeaderText = "SELECT";
                selectColumn.DataPropertyName = "SELECT_COLUMN"; // Liên kết với cột ẩn
                selectColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing; // Chỉ hiển thị khi click
                selectColumn.FlatStyle = FlatStyle.Flat;
                dgvTables.Columns.Add(selectColumn);

                // Tạo và cấu hình cột UPDATE là ComboBox
                DataGridViewComboBoxColumn updateColumn = new DataGridViewComboBoxColumn();
                updateColumn.Name = "UPDATE";
                updateColumn.HeaderText = "UPDATE";
                updateColumn.DataPropertyName = "UPDATE_COLUMN"; // Liên kết với cột ẩn
                updateColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing; // Chỉ hiển thị khi click
                updateColumn.FlatStyle = FlatStyle.Flat;
                dgvTables.Columns.Add(updateColumn);

                // Điền dữ liệu dropdown cho từng dòng
                foreach (DataGridViewRow row in dgvTables.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng mới

                    string tableName = row.Cells["TABLE_NAME"].Value?.ToString();
                    if (string.IsNullOrEmpty(tableName)) continue;

                    List<string> columns = GetColumnsFromTable(tableName);

                    if (columns.Count > 0)
                    {
                        var selectCell = (DataGridViewComboBoxCell)row.Cells["SELECT"];
                        selectCell.DataSource = new List<string>(columns);
                        selectCell.Value = selectCell.Items.Count > 0 ? selectCell.Items[0] : null;

                        var updateCell = (DataGridViewComboBoxCell)row.Cells["UPDATE"];
                        updateCell.DataSource = new List<string>(columns);
                        updateCell.Value = updateCell.Items.Count > 0 ? updateCell.Items[0] : null;
                    }
                }

                // Đánh dấu quyền đã được cấp (INSERT/DELETE)
                foreach (DataGridViewRow row in dgvTables.Rows)
                {
                    if (row.IsNewRow) continue;

                    string tableName = row.Cells["TABLE_NAME"].Value?.ToString();
                    if (string.IsNullOrEmpty(tableName)) continue;

                    string queryPrivs = @"SELECT privilege FROM dba_tab_privs
                          WHERE grantee = :roleName AND table_name = :tableName";

                    using (OracleCommand cmd = new OracleCommand(queryPrivs, conn))
                    {
                        cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = roleName.ToUpper();
                        cmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName.ToUpper();

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string privilege = reader["PRIVILEGE"].ToString().ToUpper();
                                if (privilege == "INSERT") row.Cells["INSERT"].Value = "v";
                                if (privilege == "DELETE") row.Cells["DELETE"].Value = "v";
                            }
                        }
                    }
                }

                // Xử lý sự kiện CellClick để kích hoạt dropdown
                dgvTables.CellClick += (sender, e) =>
                {
                    if (e.RowIndex >= 0 && (e.ColumnIndex == dgvTables.Columns["SELECT"].Index ||
                                           e.ColumnIndex == dgvTables.Columns["UPDATE"].Index))
                    {
                        dgvTables.BeginEdit(true);
                        if (dgvTables.EditingControl is DataGridViewComboBoxEditingControl editingControl)
                        {
                            editingControl.DroppedDown = true;
                        }
                    }
                };

                // ===== Lấy danh sách VIEWS =====
                string queryViews = @"
											SELECT view_name 
											FROM all_views 
											WHERE view_name LIKE 'QLDH_%'
											ORDER BY view_name";

                DataTable dtViews = new DataTable();
                using (OracleDataAdapter viewAdapter = new OracleDataAdapter(queryViews, conn)) // Renamed to 'viewAdapter'
                {
                    viewAdapter.Fill(dtViews);
                }

                dtViews.Columns.Add("SELECT", typeof(string));
                dtViews.Columns.Add("INSERT", typeof(string));
                dtViews.Columns.Add("UPDATE", typeof(string));
                dtViews.Columns.Add("DELETE", typeof(string));

                foreach (DataRow row in dtViews.Rows)
                {
                    row["SELECT"] = "x";
                    row["INSERT"] = "x";
                    row["UPDATE"] = "x";
                    row["DELETE"] = "x";
                }
                // ===== Lấy danh sách STORED PROCEDURES và FUNCTIONS =====
                string queryProcs = @"
            SELECT object_name AS name, object_type AS type
            FROM all_objects 
            WHERE (object_type = 'PROCEDURE' OR object_type = 'FUNCTION') AND object_name LIKE 'QLDH_%'
            ORDER BY object_name";

                DataTable dtProcs = new DataTable();
                using (OracleDataAdapter adapter = new OracleDataAdapter(queryProcs, conn))
                {
                    adapter.Fill(dtProcs);
                }

                dtProcs.Columns.Add("EXECUTE", typeof(string));

                foreach (DataRow row in dtProcs.Rows)
                {
                    row["EXECUTE"] = "x"; // Mặc định không có quyền
                }
                // ===== Kiểm tra quyền trên View =====
                string privilegeQuery = @"
			SELECT privilege 
			FROM dba_tab_privs 
			WHERE grantee = :roleName AND table_name = :tableName";

                foreach (DataRow row in dtViews.Rows)
                {
                    string viewName = row["VIEW_NAME"].ToString();
                    using (OracleCommand cmd = new OracleCommand(privilegeQuery, conn))
                    {
                        cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = roleName.ToUpper();
                        cmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = viewName.ToUpper();
                        using (OracleDataReader reader = cmd.ExecuteReader())
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
                // ===== Kiểm tra quyền trên Procedure/Function =====

                string procPrivilegeQuery = @"
            SELECT privilege
            FROM dba_tab_privs
            WHERE grantee = :roleName AND table_name = :procName AND privilege = 'EXCUTE'";
                foreach (DataRow row in dtProcs.Rows)
                {
                    string procName = row["NAME"].ToString();
                    using (OracleCommand cmd = new OracleCommand(procPrivilegeQuery, conn))
                    {
                        cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = roleName.ToUpper();
                        cmd.Parameters.Add(":procName", OracleDbType.Varchar2).Value = procName.ToUpper();
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string privilege = reader["PRIVILEGE"].ToString().ToUpper();
                                if (privilege == "EXECUTE") row["EXECUTE"] = "v";
                            }
                        }
                    }
                }
                // ===== Gán DataSource cho DataGridView Views =====
                dgvViews.DataSource = null;
                dgvViews.DataSource = dtViews;
                dgvViews.AutoGenerateColumns = true;
                dgvViews.AllowUserToAddRows = false;

                // Hiển thị Stored Procedures
                dgvProcs.DataSource = null;
                dgvProcs.DataSource = dtProcs;
                if (dgvProcs.Columns["EXECUTE"] != null)
                {

                    dgvProcs.Columns["NAME"].Width = 380;
                }
                dgvProcs.AutoGenerateColumns = true;
                dgvProcs.AllowUserToAddRows = false;


                dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTables.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to fetch privileges data\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get list of table columns
        private List<string> GetColumnsFromTable(string tableName)
        {
            List<string> columns = new List<string>();
            try
            {
                string query = @"SELECT column_name 
                         FROM all_tab_columns 
                         WHERE table_name = :tableName
                         ORDER BY column_id";

                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName.ToUpper();

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columns.Add(reader.GetString(0));
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách cột của bảng " + tableName + ":\n" + ex.Message);
            }
            return columns;
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
                    DataGridViewComboBoxCell cell = dgvTables.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                    if (cell == null) return;

                    string selectedColumn = cell.Value?.ToString();

                    // Nếu đã chọn cột => thu hồi quyền
                    if (!string.IsNullOrEmpty(selectedColumn))
                    {
                        DialogResult result = MessageBox.Show($"Bạn có muốn thu hồi quyền {columnName} trên cột '{selectedColumn}'?",
                            "Xác nhận thu hồi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            cell.Value = null; // Thu hồi
                                               // TODO: Gọi hàm revoke ở đây nếu cần thiết
                        }
                    }
                    else
                    {
                        // Hiện dropdown mặc định của ComboBox (người dùng sẽ chọn)
                        dgvTables.BeginEdit(true);
                    }
                }
                else if (columnName == "INSERT" || columnName == "DELETE")
                {
                    // Toggle kiểu checkbox (v hoặc "")
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

        private void dvgViews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewColumn column = dgvViews.Columns[e.ColumnIndex];
                    string columnName = column.Name;
                    // Chỉ cho phép click vào các cột quyền
                    if (columnName == "SELECT" || columnName == "INSERT" || columnName == "UPDATE" || columnName == "DELETE")
                    {
                        DataGridViewRow row = dgvViews.Rows[e.RowIndex];
                        string currentValue = row.Cells[columnName].Value?.ToString();
                        if (currentValue == "x")
                        {
                            row.Cells[columnName].Value = "v";  // cấp quyền
                        }
                        else if (currentValue == "v")
                        {
                            row.Cells[columnName].Value = "x";  // thu hồi quyền
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while toggling permission:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dvgProcs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewColumn column = dgvProcs.Columns[e.ColumnIndex];
                    string columnName = column.Name;
                    // Chỉ cho phép click vào các cột quyền
                    if (columnName == "EXECUTE")
                    {
                        DataGridViewRow row = dgvProcs.Rows[e.RowIndex];
                        string currentValue = row.Cells[columnName].Value?.ToString();
                        if (currentValue == "x")
                        {
                            row.Cells[columnName].Value = "v";  // cấp quyền
                        }
                        else if (currentValue == "v")
                        {
                            row.Cells[columnName].Value = "x";  // thu hồi quyền
                        }
                    }
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
                OracleConnection conn = DatabaseSession.Connection;

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleTransaction transaction = conn.BeginTransaction();  // Bắt đầu transaction

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;

                    foreach (DataGridViewRow row in dgvTables.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string tableName = row.Cells["TABLE_NAME"].Value.ToString().ToUpper();
                        string[] privileges = { "SELECT", "INSERT", "UPDATE", "DELETE" };

                        foreach (string privilege in privileges)
                        {
                            string cellValue = row.Cells[privilege].Value?.ToString();

                            if (cellValue == "v")
                            {
                                cmd.CommandText = $"BEGIN PDB_ADMIN.GRANT_PRIVS_TO_ROLE(:roleName, :tableName, :privilege); END;";
                            }
                            else if (cellValue == "x")
                            {
                                cmd.CommandText = $"BEGIN PDB_ADMIN.REVOKE_PRIVS_FROM_ROLE(:roleName, :tableName, :privilege); END;";
                            }
                            else
                            {
                                continue;
                            }

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = RoleName;
                            cmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName;
                            cmd.Parameters.Add(":privilege", OracleDbType.Varchar2).Value = privilege;

                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (DataGridViewRow row in dgvViews.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string viewName = row.Cells["VIEW_NAME"].Value.ToString().ToUpper();
                        string[] privileges = { "SELECT", "INSERT", "UPDATE", "DELETE" };

                        foreach (string privilege in privileges)
                        {
                            string cellValue = row.Cells[privilege].Value?.ToString();

                            if (cellValue == "v")
                            {
                                cmd.CommandText = $"BEGIN PDB_ADMIN.GRANT_PRIVS_TO_ROLE(:roleName, :viewName, :privilege); END;";
                            }
                            else if (cellValue == "x")
                            {
                                cmd.CommandText = $"BEGIN PDB_ADMIN.REVOKE_PRIVS_FROM_ROLE(:roleName, :viewName, :privilege); END;";
                            }
                            else
                            {
                                continue;
                            }

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = RoleName;
                            cmd.Parameters.Add(":viewName", OracleDbType.Varchar2).Value = viewName;
                            cmd.Parameters.Add(":privilege", OracleDbType.Varchar2).Value = privilege;

                            cmd.ExecuteNonQuery();
                        }
                    }
                    foreach (DataGridViewRow row in dgvProcs.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string procName = row.Cells["NAME"].Value.ToString().ToUpper();
                        string cellValue = row.Cells["EXECUTE"].Value?.ToString();
                        if (cellValue == "v")
                        {
                            cmd.CommandText = $"BEGIN PDB_ADMIN.GRANT_PRIVS_TO_ROLE(:roleName, :procName, 'EXECUTE'); END;";
                        }
                        else if (cellValue == "x")
                        {
                            cmd.CommandText = $"BEGIN PDB_ADMIN.REVOKE_PRIVS_FROM_ROLE(:roleName, :procName, 'EXECUTE'); END;";
                        }
                        else
                        {
                            continue;
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = RoleName;
                        cmd.Parameters.Add(":procName", OracleDbType.Varchar2).Value = procName;
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                MessageBox.Show("Permissions updated successfully!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update permissions:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label7_Click(object sender, EventArgs e)
        {
            label7.Text = RoleName;
        }
    }
}
