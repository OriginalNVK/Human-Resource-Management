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

                // ===== Lấy danh sách TABLES =====
                string queryTables = @"
			SELECT table_name 
			FROM all_tables 
			WHERE table_name LIKE 'QLDH_%' AND table_name != 'QLDH_ADMIN'
			ORDER BY table_name";

                DataTable dtTables = new DataTable();
                using (OracleDataAdapter adapter = new OracleDataAdapter(queryTables, conn))
                {
                    adapter.Fill(dtTables);
                }

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

                // ===== Lấy danh sách VIEWS =====
                string queryViews = @"
			SELECT view_name 
			FROM all_views 
			WHERE view_name LIKE 'QLDH_%'
			ORDER BY view_name";

                DataTable dtViews = new DataTable();
                using (OracleDataAdapter adapter = new OracleDataAdapter(queryViews, conn))
                {
                    adapter.Fill(dtViews);
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
                // ===== Lấy danh sách STORED PROCEDURES =====
                string queryProcs = @"
            SELECT object_name AS procedure_name
            FROM all_objects 
            WHERE object_type = 'PROCEDURE' AND object_name LIKE 'QLDH_%'
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
                // ===== Kiểm tra quyền trên TABLES =====
                string privilegeQuery = @"
			SELECT privilege 
			FROM dba_tab_privs 
			WHERE grantee = :roleName AND table_name = :tableName";

                foreach (DataRow row in dtTables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();

                    using (OracleCommand cmd = new OracleCommand(privilegeQuery, conn))
                    {
                        cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = roleName.ToUpper();
                        cmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName.ToUpper();

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string priv = reader["PRIVILEGE"].ToString().ToUpper();
                                if (priv == "SELECT") row["SELECT"] = "v";
                                if (priv == "INSERT") row["INSERT"] = "v";
                                if (priv == "UPDATE") row["UPDATE"] = "v";
                                if (priv == "DELETE") row["DELETE"] = "v";
                            }
                        }
                    }
                }

                // ===== Kiểm tra quyền trên VIEWS =====
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
                                string priv = reader["PRIVILEGE"].ToString().ToUpper();
                                if (priv == "SELECT") row["SELECT"] = "v";
                                if (priv == "INSERT") row["INSERT"] = "v";
                                if (priv == "UPDATE") row["UPDATE"] = "v";
                                if (priv == "DELETE") row["DELETE"] = "v";
                            }
                        }
                    }
                }

                // ===== Kiểm tra quyền EXECUTE trên STORED PROCEDURES =====
                string procPrivilegeQuery = @"
            SELECT privilege 
            FROM dba_tab_privs 
            WHERE grantee = :roleName AND table_name = :procName AND privilege = 'EXECUTE'";

                foreach (DataRow row in dtProcs.Rows)
                {
                    string procName = row["PROCEDURE_NAME"].ToString();

                    using (OracleCommand cmd = new OracleCommand(procPrivilegeQuery, conn))
                    {
                        cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = roleName.ToUpper();
                        cmd.Parameters.Add(":procName", OracleDbType.Varchar2).Value = procName.ToUpper();

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                row["EXECUTE"] = "v"; // Có quyền EXECUTE
                            }
                        }
                    }
                }


                // ===== Hiển thị dữ liệu =====
                dgvTables.DataSource = null;
                dgvTables.DataSource = dtTables;
                dgvTables.AutoGenerateColumns = true;
                dgvTables.AllowUserToAddRows = false;

                dgvViews.DataSource = null;
                dgvViews.DataSource = dtViews;
                dgvViews.AutoGenerateColumns = true;
                dgvViews.AllowUserToAddRows = false;

                // Hiển thị Stored Procedures
                dgvProcs.DataSource = null;
                dgvProcs.DataSource = dtProcs;
                if (dgvProcs.Columns["EXECUTE"] != null)
                {
                    dgvProcs.Columns["EXECUTE"].Width = 100; // Set a fixed width (e.g., 50 pixels)
                                                            // Alternatively, you can use AutoSizeMode if preferred:
                                                            // dgvProcs.Columns["EXECUTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }
                dgvProcs.AutoGenerateColumns = true;
                dgvProcs.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy quyền truy cập:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewColumn column = dgvTables.Columns[e.ColumnIndex];
                    string columnName = column.Name;

                    // Chỉ cho phép click vào các cột quyền
                    if (columnName == "SELECT" || columnName == "INSERT" || columnName == "UPDATE" || columnName == "DELETE")
                    {
                        DataGridViewRow row = dgvTables.Rows[e.RowIndex];
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
                    DataGridViewColumn column = dgvViews.Columns[e.ColumnIndex];
                    string columnName = column.Name;
                    // Chỉ cho phép click vào các cột quyền
                    if (columnName == "EXECUTE" )
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
                                cmd.CommandText = $"BEGIN SYS.GRANT_PRIVS_TO_ROLE(:roleName, :viewName, :privilege); END;";
                            }
                            else if (cellValue == "x")
                            {
                                cmd.CommandText = $"BEGIN SYS.REVOKE_PRIVS_FROM_ROLE(:roleName, :viewName, :privilege); END;";
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
