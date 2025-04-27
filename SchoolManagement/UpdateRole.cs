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
            this.dgvUser.CellClick += dgvUser_CellClick;
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
                string oradb = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

                using (OracleConnection conn = new OracleConnection(oradb))
                {
                    conn.Open();

                    // Lấy danh sách các bảng
                    string queryTables = "SELECT table_name FROM all_tables WHERE table_name LIKE 'QLDH_%' AND table_name != 'QLDH_ADMIN' ORDER BY table_name";
                    OracleDataAdapter adapter = new OracleDataAdapter(queryTables, conn);
                    DataTable dtTables = new DataTable();

                    adapter.Fill(dtTables);

                    // Thêm các cột quyền vào DataTable
                    dtTables.Columns.Add("SELECT", typeof(string));
                    dtTables.Columns.Add("INSERT", typeof(string));
                    dtTables.Columns.Add("UPDATE", typeof(string));
                    dtTables.Columns.Add("DELETE", typeof(string));

                    foreach (DataRow row in dtTables.Rows)
                    {
                        row["SELECT"] = "x";
                        row["INSERT"] = "x";
                        row["UPDATE"] = "x";
                        row["DELETE"] = "x";
                    }

                    // Check quyền đã được cấp
                    foreach (DataRow row in dtTables.Rows)
                    {
                        string tableName = row["TABLE_NAME"].ToString();

                        string queryPrivs = @"SELECT privilege
                                     FROM dba_tab_privs
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
                                    if (privilege == "SELECT") row["SELECT"] = "v";
                                    if (privilege == "INSERT") row["INSERT"] = "v";
                                    if (privilege == "UPDATE") row["UPDATE"] = "v";
                                    if (privilege == "DELETE") row["DELETE"] = "v";
                                }
                            }
                        }
                    }

                    dgvUser.DataSource = null;
                    dgvUser.DataSource = dtTables;

                    // Tùy chỉnh hiển thị
                    dgvUser.AutoGenerateColumns = true;
                    dgvUser.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to fetch privileges data\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewColumn column = dgvUser.Columns[e.ColumnIndex];
                    string columnName = column.Name;

                    // Chỉ cho phép click vào các cột quyền
                    if (columnName == "SELECT" || columnName == "INSERT" || columnName == "UPDATE" || columnName == "DELETE")
                    {
                        DataGridViewRow row = dgvUser.Rows[e.RowIndex];
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
                string oradb = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

                using (OracleConnection conn = new OracleConnection(oradb))
                {
                    conn.Open();
                    OracleTransaction transaction = conn.BeginTransaction();  // Bắt đầu transaction

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = transaction;

                        foreach (DataGridViewRow row in dgvUser.Rows)
                        {
                            if (row.IsNewRow) continue; // Bỏ dòng trống mới thêm

                            string tableName = row.Cells["TABLE_NAME"].Value.ToString().ToUpper();

                            string[] privileges = { "SELECT", "INSERT", "UPDATE", "DELETE" };

                            foreach (string privilege in privileges)
                            {
                                string cellValue = row.Cells[privilege].Value?.ToString();

                                if (cellValue == "v")
                                {
                                    cmd.CommandText = $"BEGIN SYS.GRANT_PRIVS_TO_ROLE(:roleName, :tableName, :privilege); END;"; 
                                }
                                else if (cellValue == "x")
                                {
                                    cmd.CommandText = $"BEGIN SYS.REVOKE_PRIVS_FROM_ROLE(:roleName, :tableName, :privilege); END;";
                                }
                                else
                                {
                                    continue;
                                }

                                // Thêm các tham số cần thiết
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = RoleName;
                                cmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName;
                                cmd.Parameters.Add(":privilege", OracleDbType.Varchar2).Value = privilege;

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("Permissions updated successfully!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
