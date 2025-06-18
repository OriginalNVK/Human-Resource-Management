using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Office.Interop.Excel;
using Oracle.ManagedDataAccess.Client;
using static System.ComponentModel.Design.ObjectSelectorEditor;
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
            this.dgvViews.CellClick += dgvViews_CellClick;
            this.dgvProcs.CellClick += dgvProc_CellClick;
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
                string queryViews = @"
			        SELECT view_name 
			        FROM all_views 
			        WHERE  OWNER = 'PDB_ADMIN'
			        ORDER BY view_name";
                
                using (OracleCommand command = new OracleCommand(queryViews, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                {
                    System.Data.DataTable dtView = new System.Data.DataTable();
                    adapter.Fill(dtView);
                    dtView.Columns.Add("SELECT",typeof(string));
                    dtView.Columns.Add("INSERT",typeof (string));
                    dtView.Columns.Add("UPDATE", typeof(string));
                    dtView.Columns.Add("DELETE",typeof(string));
                    foreach (DataRow row in dtView.Rows)
                    {
                        row["SELECT"] = 'x';
                        row["INSERT"] = 'x';
                        row["UPDATE"] = 'x';
                        row["DELETE"] = "x";
                    }
                    foreach(DataRow row in dtView.Rows)
                    {
                        string viewName = row["VIEW_NAME"].ToString();
                        string queryPrivs = @"SELECT privilege
                                            FROM dba_tab_privs
                                            WHERE grantee = :roleName AND table_name = :viewName AND OWNER = 'PDB_ADMIN'";
                        using (OracleCommand cmd = new OracleCommand(queryPrivs, DatabaseSession.Connection))
                        {
                           cmd.Parameters.Add("roleName",OracleDbType.Varchar2).Value = roleName;
                            cmd.Parameters.Add("viewName",OracleDbType.Varchar2).Value=viewName;
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
                    dgvViews.DataSource = dtView;
                    dgvViews.Columns["VIEW_NAME"].HeaderText = "VIEW";
                }
                string queryProcs = @"SELECT object_name AS NAME, object_type AS TYPE
                                    FROM all_objects 
                                    WHERE (object_type = 'PROCEDURE' OR object_type = 'FUNCTION') AND OWNER = 'PDB_ADMIN'
                                    ORDER BY object_name";
                using (OracleCommand command = new OracleCommand(queryProcs, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                {
                    System.Data.DataTable dtProc = new System.Data.DataTable(); 
                    adapter.Fill(dtProc);
                    dtProc.Columns.Add("EXECUTE", typeof(string));
                    foreach (DataRow row in dtProc.Rows)
                    {
                        row["EXECUTE"] = 'x';
                    }
                    foreach(DataRow row in dtProc.Rows)
                    {
                        string procName = row["NAME"].ToString();
                        string queryPrivs = @"SELECT privilege
                                            FROM dba_tab_privs
                                            WHERE grantee = :roleName AND table_name = :procName AND privilege = 'EXECUTE'";
                        using (OracleCommand cmd = new OracleCommand(queryPrivs, DatabaseSession.Connection))
                        {
                            cmd.Parameters.Add("roleName", OracleDbType.Varchar2).Value = roleName;
                            cmd.Parameters.Add("procName",OracleDbType.Varchar2).Value = procName;
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
                    dgvProcs.DataSource = dtProc;
                    dgvProcs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    int totalWidth = dgvProcs.Width; // Lấy chiều rộng của DataGridView (899px)
                    if (dgvProcs.Columns.Count >= 3)
                    {
                        
                        dgvProcs.Columns["TYPE"].Width = (int)(totalWidth * 0.20); // 30%
                        dgvProcs.Columns["EXECUTE"].Width = (int)(totalWidth * 0.20); // 20%
                        dgvProcs.Columns["NAME"].Width = totalWidth - dgvProcs.Columns["TYPE"].Width - dgvProcs.Columns["EXECUTE"].Width - this.dgvViews.RowHeadersWidth; // 50%
                    }
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
					if (selectedColumnsByPrivilege.ContainsKey(key))
					{
						// Nếu đã có trong cache → dùng luôn
						selectedColumns = selectedColumnsByPrivilege[key];
					}
					else
					{
						selectedColumns = new List<string>();

						// Kiểm tra xem quyền có được cấp trên toàn bảng không
						string checkTabPriv = @"SELECT 1 FROM DBA_TAB_PRIVS 
                            WHERE GRANTEE = :roleName 
                              AND TABLE_NAME = :tableName 
                              AND PRIVILEGE = :privilege";
						using (OracleCommand cmd = new OracleCommand(checkTabPriv, DatabaseSession.Connection))
						{
							cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = roleName.ToUpper();
							cmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName.ToUpper();
							cmd.Parameters.Add(":privilege", OracleDbType.Varchar2).Value = columnName.ToUpper();

							object result = cmd.ExecuteScalar();
							if (result != null)
							{
								selectedColumns = new List<string>(columns);
							}
							else
							{
								string queryColPrivs = @"SELECT COLUMN_NAME FROM DBA_COL_PRIVS 
                                     WHERE GRANTEE = :roleName 
                                       AND TABLE_NAME = :tableName 
                                       AND PRIVILEGE = :privilege";

								using (OracleCommand colCmd = new OracleCommand(queryColPrivs, DatabaseSession.Connection))
								{
									colCmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = roleName.ToUpper();
									colCmd.Parameters.Add(":tableName", OracleDbType.Varchar2).Value = tableName.ToUpper();
									colCmd.Parameters.Add(":privilege", OracleDbType.Varchar2).Value = columnName.ToUpper();

									using (OracleDataReader reader = colCmd.ExecuteReader())
									{
										while (reader.Read())
										{
											selectedColumns.Add(reader["COLUMN_NAME"].ToString());
										}
									}
								}
							}
						}

						// Lưu lần đầu vào cache
						selectedColumnsByPrivilege[key] = selectedColumns;
					}

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

        private void dgvViews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                string columnName = dgvViews.Columns[e.ColumnIndex].Name;
                if (columnName == "INSERT" || columnName == "DELETE"|| columnName == "SELECT" || columnName == "UPDATE")
                {
                    DataGridViewCell cell = dgvViews.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string currentValue = cell.Value?.ToString();

                    cell.Value = currentValue == "v" ? "x" : "v";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while toggling permission:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvProc_CellClick(Object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                string columnName = dgvProcs.Columns[e.ColumnIndex].Name;
                if (columnName == "EXECUTE" )
                {
                    DataGridViewCell cell = dgvProcs.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string currentValue = cell.Value?.ToString();

                    cell.Value = currentValue == "v" ? "x" : "v";
                }
            }
            catch ( Exception ex)
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
				string roleName = RoleName.ToUpper();

				foreach (DataGridViewRow row in dgvTables.Rows)
				{
					string tableName = row.Cells["TABLE_NAME"].Value.ToString().ToUpper();

					foreach (string privilege in new[] { "SELECT", "INSERT", "UPDATE", "DELETE" })
					{
						string cellValue = row.Cells[privilege].Value?.ToString();
						bool isChecked = cellValue == "v";
						string key = $"{roleName}|{tableName}|{privilege}";
						string sql = "";

						if (isChecked)
						{
							// --- GRANT ---
							if (privilege == "SELECT" || privilege == "UPDATE")
							{
								if (selectedColumnsByPrivilege.TryGetValue(key, out var columns) && columns.Count > 0)
								{
									string columnList = string.Join(", ", columns.Select(c => $"{c}"));
									sql = $"GRANT {privilege}({columnList}) ON PDB_ADMIN.{tableName} TO {roleName}";
								}
								else
								{
									// Nếu không có cột nào được chọn, bỏ qua
									continue;
								}
							}
							else
							{
								sql = $"GRANT {privilege} ON PDB_ADMIN.\"{tableName}\" TO \"{roleName}\"";
							}

                            MessageBox.Show(sql);

							using (OracleCommand cmd = new OracleCommand(sql, DatabaseSession.Connection))
							{
								cmd.ExecuteNonQuery();
							}
						}
						else
						{
							// --- REVOKE ---
							bool hasPrivilege = false;

							// Kiểm tra quyền cấp trên bảng
							string checkTablePrivSql = @"SELECT 1 FROM DBA_TAB_PRIVS WHERE GRANTEE = :p_role AND TABLE_NAME = :p_table AND PRIVILEGE = :p_privilege";

							//string checkTablePrivSql = $"SELECT 1 FROM DBA_TAB_PRIVS WHERE GRANTEE = {roleName} AND TABLE_NAME = {tableName} AND PRIVILEGE = {privilege}";
                            //MessageBox.Show("checkTablePrivSql = " + checkTablePrivSql);

							using (OracleCommand checkCmd = new OracleCommand(checkTablePrivSql, DatabaseSession.Connection))
							{
								checkCmd.Parameters.Add(":p_role", OracleDbType.Varchar2).Value = roleName;
								checkCmd.Parameters.Add(":p_table", OracleDbType.Varchar2).Value = tableName;
								checkCmd.Parameters.Add(":p_privilege", OracleDbType.Varchar2).Value = privilege;

								hasPrivilege = checkCmd.ExecuteScalar() != null;
							}

							// Nếu không có trên bảng → kiểm tra trên cột
							if (!hasPrivilege && (privilege == "SELECT" || privilege == "UPDATE"))
							{
								string checkColPrivSql = @"SELECT 1 FROM DBA_COL_PRIVS WHERE GRANTEE = :p_role AND TABLE_NAME = :p_table AND PRIVILEGE = :p_privilege";

								//string checkColPrivSql = $"SELECT 1 FROM DBA_COL_PRIVS WHERE GRANTEE = {roleName} AND TABLE_NAME = {tableName} AND PRIVILEGE = {privilege}";

								using (OracleCommand checkColCmd = new OracleCommand(checkColPrivSql, DatabaseSession.Connection))
								{
									checkColCmd.Parameters.Add(":p_role", OracleDbType.Varchar2).Value = roleName;
									checkColCmd.Parameters.Add(":p_table", OracleDbType.Varchar2).Value = tableName;
									checkColCmd.Parameters.Add(":p_privilege", OracleDbType.Varchar2).Value = privilege;

									hasPrivilege = checkColCmd.ExecuteScalar() != null;
								}

								//MessageBox.Show("checkColPrivSql = " + checkColPrivSql);
							}

							// Nếu có quyền → REVOKE
							if (hasPrivilege)
							{
								string revokeSql = $"REVOKE {privilege} ON PDB_ADMIN.\"{tableName}\" FROM \"{roleName}\"";

                                //MessageBox.Show("revokeSql = " +  revokeSql);

								using (OracleCommand revokeCmd = new OracleCommand(revokeSql, DatabaseSession.Connection))
								{
									revokeCmd.ExecuteNonQuery();
								}
							}
						}
					}
				}

                string query;
                foreach (DataGridViewRow row in dgvViews.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    string viewName = row.Cells["VIEW_NAME"].Value?.ToString().ToUpper();
                    if (string.IsNullOrEmpty(viewName))
                        continue;

                    string[] privileges = { "SELECT", "INSERT", "UPDATE", "DELETE" };
                    foreach (string privilege in privileges)
                    {
                        string cellValue = row.Cells[privilege].Value?.ToString()?.ToLower();

                        if (cellValue == "v" )
                        {
                            viewName = "PDB_ADMIN." + viewName;
                            query = $"GRANT {privilege} ON {viewName} TO {RoleName}";


                            using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                            {
                                                         
                               
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
     
                           query = "BEGIN PDB_ADMIN.REVOKE_PRIVS_FROM_ROLE(:roleName, :objectName, :privilege); END;";

                            using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                            {
                                cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = RoleName;
                                cmd.Parameters.Add(":objectName", OracleDbType.Varchar2).Value = viewName;
                                cmd.Parameters.Add(":privilege", OracleDbType.Varchar2).Value = privilege;

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                foreach (DataGridViewRow row in dgvProcs.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    string procName = row.Cells["NAME"].Value?.ToString().ToUpper();
                    if (string.IsNullOrEmpty(procName))
                        continue;

                    string cellValue = row.Cells["EXECUTE"].Value?.ToString()?.ToLower();
                    if (cellValue == "v")
                    {
                        procName = "PDB_ADMIN." + procName;
                        query = $"GRANT EXECUTE ON {procName} TO {RoleName}";

                        using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                        {


                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {

                        query = "BEGIN PDB_ADMIN.REVOKE_PRIVS_FROM_ROLE(:roleName, :objectName, :privilege); END;";

                        using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                        {
                            cmd.Parameters.Add(":roleName", OracleDbType.Varchar2).Value = RoleName;
                            cmd.Parameters.Add(":objectName", OracleDbType.Varchar2).Value = procName;
                            cmd.Parameters.Add(":privilege", OracleDbType.Varchar2).Value = "EXECUTE";

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Cập nhật quyền thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi lưu quyền:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
