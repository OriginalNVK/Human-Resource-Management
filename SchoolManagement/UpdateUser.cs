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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace SchoolManagement
{
	public partial class UpdateUser : KryptonForm
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
		private string _mainRole;

		private ComponentFactory.Krypton.Toolkit.KryptonButton btnRevokeAllPrivileges;
		public UpdateUser(string username, string role)
		{
			InitializeComponent();
			// Hiển thị thông tin cơ bản
			txtUsername.Text = username;
			RoleDropdown.Text = role;

			_mainRole = role;
			// Load các thông tin chi tiết khác từ database nếu cần
			LoadInformation(username, role);
			LoadAllRoles(username);
		}

		private void LoadInformation(string username, string role)
		{
			try
			{
				// Lấy connection đã được mở ở login
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Kết nối chưa khởi tạo hoặc chưa mở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				string query;
				switch (role.ToUpper())
				{
					case "ADMIN":
						query = @"
            SELECT u.USERNAME,
                   a.HOTEN, a.PHAI, a.DT, a.DCHI, a.NGSINH
              FROM PDB_ADMIN.QLDH_ADMIN a
              JOIN ALL_USERS u ON u.USERNAME = a.MAAD
             WHERE u.USERNAME = :username";
						break;

					case "NHAN VIEN":
						query = @"
            SELECT u.USERNAME,
                   n.HOTEN, n.PHAI, n.DT, n.DCHI, n.NGSINH
              FROM PDB_ADMIN.QLDH_NHANVIEN n
              JOIN ALL_USERS u ON u.USERNAME = n.MANV
             WHERE u.USERNAME = :username";
						break;

					case "SINH VIEN":
						query = @"
            SELECT u.USERNAME,
                   s.HOTEN, s.PHAI, s.DT, s.DCHI, s.NGSINH
              FROM PDB_ADMIN.QLDH_SINHVIEN s
              JOIN ALL_USERS u ON u.USERNAME = s.MASV
             WHERE u.USERNAME = :username";
						break;

					default:
						MessageBox.Show("Vai trò người dùng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
				}

				using (var cmd = new OracleCommand(query, conn))
				{
					cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username.ToUpper();

					using (var reader = cmd.ExecuteReader())
					{
						if (!reader.Read())
						{
							MessageBox.Show("Không tìm thấy thông tin người dùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
							return;
						}

						// Username
						txtUsername.Text = reader.GetString(reader.GetOrdinal("USERNAME"));

						// Role truyền vào
						RoleDropdown.Text = role;

						// Các thông tin profile
						txtFullname.Text = reader.GetString(reader.GetOrdinal("HOTEN"));
						GenderDropdown.Text = reader.GetString(reader.GetOrdinal("PHAI"));

						txtPhoneNum.Text = reader.GetString(reader.GetOrdinal("DT"));
						txtAddress.Text = reader.GetString(reader.GetOrdinal("DCHI"));

						if (!reader.IsDBNull(reader.GetOrdinal("NGSINH")))
							dtpDOB.Value = reader.GetDateTime(reader.GetOrdinal("NGSINH"));
					}
				}
			}
			catch (OracleException oex)
			{
				MessageBox.Show($"Lỗi Oracle khi tải thông tin:\n{oex.Message}", "Lỗi Oracle", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi tải thông tin người dùng:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnUpdateUser_Click(object sender, EventArgs e)
		{
			try
			{
				// Lấy thông tin từ các trường nhập liệu
				string username = txtUsername.Text.Trim().ToUpper();
				string password = txtPassword.Text.Trim();
				// Lấy vai trò được chọn từ DataGridView
				string role = dgvUser.Rows.Cast<DataGridViewRow>()
									.Where(r => Convert.ToBoolean(r.Cells["chk"].Value ?? false))
									.Select(r => r.Cells["Role"].Value.ToString())
									.FirstOrDefault();

				string fullname = txtFullname.Text.Trim();
				string gender = GenderDropdown.SelectedItem?.ToString();
				string phoNum = txtPhoneNum.Text.Trim();
				string address = txtAddress.Text.Trim();
				string dob = dtpDOB.Value.ToString("dd-MMM-yyyy");

				// Kiểm tra dữ liệu đầu vào
				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
				{
					MessageBox.Show("Vui lòng điền Username và chọn Role.");
					return;
				}

				// Lấy kết nối database (giữ nguyên logic cũ)
				OracleConnection conn = DatabaseSession.Connection;
				MessageBox.Show(conn.ToString());
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Kết nối chưa khởi tạo hoặc chưa mở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Bắt đầu transaction để đảm bảo tính toàn vẹn dữ liệu
				OracleTransaction transaction = conn.BeginTransaction();

				try
				{
					// Cập nhật mật khẩu nếu người dùng nhập mật khẩu mới (giữ nguyên logic cũ)
					if (!string.IsNullOrEmpty(password))
					{
						string alterQuery = $"ALTER USER {username} IDENTIFIED BY \"{password}\"";
						using (OracleCommand alterCmd = new OracleCommand(alterQuery, conn))
						{
							alterCmd.Transaction = transaction;
							alterCmd.ExecuteNonQuery();
						}
					}

					// Lấy danh sách vai trò hiện tại của người dùng
					string grantedRolesQuery = "SELECT GRANTED_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :username";
					List<string> grantedRoles = new List<string>();
					using (OracleCommand grantedCmd = new OracleCommand(grantedRolesQuery, conn))
					{
						grantedCmd.Transaction = transaction;
						grantedCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
						using (OracleDataReader reader = grantedCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								grantedRoles.Add(reader.GetString(0));
							}
						}
					}

					// Thu hồi các vai trò đã cấp nhưng bị bỏ chọn
					foreach (string grantedRole in grantedRoles)
					{
						if (grantedRole != role && !dgvUser.Rows.Cast<DataGridViewRow>()
							.Any(r => r.Cells["Role"].Value.ToString() == grantedRole && Convert.ToBoolean(r.Cells["chk"].Value ?? false)))
						{
							string revokeQuery = $"BEGIN EXECUTE IMMEDIATE 'REVOKE {grantedRole} FROM {username}'; END;";
							using (OracleCommand revokeCmd = new OracleCommand(revokeQuery, conn))
							{
								revokeCmd.Transaction = transaction;
								revokeCmd.ExecuteNonQuery();
							}
						}
					}

					// Kiểm tra user đã có role hay chưa (giữ nguyên logic cũ)
					string checkRoleQuery = @"
						SELECT COUNT(*) 
						FROM DBA_ROLE_PRIVS 
						WHERE GRANTEE = :username AND GRANTED_ROLE = :role";

					using (OracleCommand checkCmd = new OracleCommand(checkRoleQuery, conn))
					{
						checkCmd.Transaction = transaction;
						checkCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
						checkCmd.Parameters.Add("role", OracleDbType.Varchar2).Value = role;

						int count = Convert.ToInt32(checkCmd.ExecuteScalar());

						// Nếu chưa có role thì mới gán
						if (count == 0)
						{
							string grantQuery = $"BEGIN EXECUTE IMMEDIATE 'GRANT {role} TO {username}'; END;";
							using (OracleCommand grantCmd = new OracleCommand(grantQuery, conn))
							{
								grantCmd.Transaction = transaction;
								grantCmd.ExecuteNonQuery();
							}
						}
					}

					// Cập nhật thông tin chi tiết của người dùng dựa trên vai trò (giữ nguyên logic cũ)
					string updateDetails = "";
					switch (_mainRole.ToUpper())
					{
						case "ADMIN":
							updateDetails = @"UPDATE PDB_ADMIN.QLDH_ADMIN 
										SET HOTEN = :fullname, PHAI = :gender, 
											NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
											DCHI = :address, DT = :phoNum 
										WHERE MAAD = :username";
							break;
						case "NHAN VIEN":
							updateDetails = @"UPDATE PDB_ADMIN.QLDH_NHANVIEN 
										SET HOTEN = :fullname, PHAI = :gender, 
											NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
											DCHI = :address, DT = :phoNum 
										WHERE MANV = :username";
							break;
						case "SINH VIEN":
							updateDetails = @"UPDATE PDB_ADMIN.QLDH_SINHVIEN 
										SET HOTEN = :fullname, PHAI = :gender, 
											NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
											DCHI = :address, DT = :phoNum 
										WHERE MASV = :username";
							break;
						default:
							transaction.Rollback();
							MessageBox.Show("Vai trò không hợp lệ.");
							return;
					}

					// Thực thi câu lệnh cập nhật thông tin người dùng (giữ nguyên logic cũ)
					using (OracleCommand updateCmd = new OracleCommand(updateDetails, conn))
					{
						updateCmd.Transaction = transaction;
						updateCmd.Parameters.Add("fullname", OracleDbType.Varchar2).Value = fullname;
						updateCmd.Parameters.Add("gender", OracleDbType.Varchar2).Value = gender;
						updateCmd.Parameters.Add("dob", OracleDbType.Varchar2).Value = dob;
						updateCmd.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
						updateCmd.Parameters.Add("phoNum", OracleDbType.Varchar2).Value = phoNum;
						updateCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

						int result = updateCmd.ExecuteNonQuery();
						if (result <= 0)
						{
							transaction.Rollback();
							MessageBox.Show("Không tìm thấy thông tin người dùng để cập nhật.");
							return;
						}
					}

					// Commit transaction nếu mọi thứ thành công
					transaction.Commit();
					MessageBox.Show("Cập nhật người dùng thành công!");
					this.Close();
				}
				catch (Exception ex)
				{
					// Rollback transaction nếu có lỗi
					transaction.Rollback();
					MessageBox.Show("Lỗi khi cập nhật người dùng:\n" + ex.Message);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi kết nối CSDL:\n" + ex.Message);
			}

			// Chuyển về form UsersManager (giữ nguyên logic cũ)
			UsersManager userManager = new UsersManager();
			this.Hide();
			userManager.ShowDialog();
			this.Close();
		}

		// Phương thức mới: Thu hồi toàn bộ quyền được cấp trực tiếp cho user
		private void btnRevokeAllPrivileges_Click(object sender, EventArgs e)
		{
			try
			{
				// Lấy username từ trường nhập liệu
				string username = txtUsername.Text.Trim().ToUpper();

				// Kiểm tra username hợp lệ
				if (string.IsNullOrEmpty(username))
				{
					MessageBox.Show("Please Enter Username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				// Lấy kết nối database
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Connecton Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Xác nhận hành động từ người dùng
				DialogResult confirmResult = MessageBox.Show(
					$"Are you sure about revoking all the privileges of {username}?\nThis can not be undone.",
					"Confirm action",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning);

				if (confirmResult != DialogResult.Yes)
				{
					return;
				}

				// Bắt đầu transaction để đảm bảo tính toàn vẹn
				OracleTransaction transaction = conn.BeginTransaction();

				try
				{
					// 1. Thu hồi các vai trò (roles) được cấp trực tiếp
					string rolesQuery = "SELECT GRANTED_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :username";
					List<string> roles = new List<string>();
					using (OracleCommand rolesCmd = new OracleCommand(rolesQuery, conn))
					{
						rolesCmd.Transaction = transaction;
						rolesCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
						using (OracleDataReader reader = rolesCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								roles.Add(reader.GetString(0));
							}
						}
					}

					foreach (string role in roles)
					{
						string revokeRoleQuery = $"BEGIN EXECUTE IMMEDIATE 'REVOKE {role} FROM {username}'; END;";
						using (OracleCommand revokeCmd = new OracleCommand(revokeRoleQuery, conn))
						{
							revokeCmd.Transaction = transaction;
							revokeCmd.ExecuteNonQuery();
						}
					}

					// 2. Thu hồi các quyền hệ thống (system privileges)
					string sysPrivsQuery = "SELECT PRIVILEGE FROM DBA_SYS_PRIVS WHERE GRANTEE = :username";
					List<string> sysPrivs = new List<string>();
					using (OracleCommand sysPrivsCmd = new OracleCommand(sysPrivsQuery, conn))
					{
						sysPrivsCmd.Transaction = transaction;
						sysPrivsCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
						using (OracleDataReader reader = sysPrivsCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								sysPrivs.Add(reader.GetString(0));
							}
						}
					}

					foreach (string priv in sysPrivs)
					{
						string revokeSysPrivQuery = $"BEGIN EXECUTE IMMEDIATE 'REVOKE {priv} FROM {username}'; END;";
						using (OracleCommand revokeCmd = new OracleCommand(revokeSysPrivQuery, conn))
						{
							revokeCmd.Transaction = transaction;
							revokeCmd.ExecuteNonQuery();
						}
					}

					// 3. Thu hồi các quyền đối tượng (object privileges)
					string tabPrivsQuery = @"
						SELECT PRIVILEGE, OWNER, TABLE_NAME 
						FROM DBA_TAB_PRIVS 
						WHERE GRANTEE = :username";
					List<(string Privilege, string Owner, string TableName)> tabPrivs = new List<(string, string, string)>();
					using (OracleCommand tabPrivsCmd = new OracleCommand(tabPrivsQuery, conn))
					{
						tabPrivsCmd.Transaction = transaction;
						tabPrivsCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
						using (OracleDataReader reader = tabPrivsCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								tabPrivs.Add((
									reader.GetString(0), // PRIVILEGE
									reader.GetString(1), // OWNER
									reader.GetString(2)  // TABLE_NAME
								));
							}
						}
					}

					foreach (var priv in tabPrivs)
					{
						string revokeTabPrivQuery = $"BEGIN EXECUTE IMMEDIATE 'REVOKE {priv.Privilege} ON {priv.Owner}.{priv.TableName} FROM {username}'; END;";
						using (OracleCommand revokeCmd = new OracleCommand(revokeTabPrivQuery, conn))
						{
							revokeCmd.Transaction = transaction;
							revokeCmd.ExecuteNonQuery();
						}
					}

					// Commit transaction
					transaction.Commit();
					MessageBox.Show($"All privileges of {username} revoked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

					// Cập nhật lại DataGridView để phản ánh thay đổi
					LoadAllRoles(username);
				}
				catch (Exception ex)
				{
					// Rollback transaction nếu có lỗi
					transaction.Rollback();
					MessageBox.Show($"Error on revoking:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error on connecting to DB:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Chỉ xử lý click vào cột checkbox

			// Toggle trạng thái checkbox
			bool currentValue = Convert.ToBoolean(dgvUser.Rows[e.RowIndex].Cells["chk"].Value ?? false);
			dgvUser.Rows[e.RowIndex].Cells["chk"].Value = !currentValue;

			// Bỏ chọn các dòng khác
			foreach (DataGridViewRow row in dgvUser.Rows)
			{
				if (row.Index != e.RowIndex)
				{
					row.Cells["chk"].Value = false;
				}
			}
		}

		private void LoadAllRoles(string username)
		{
			try
			{
				var conn = DatabaseSession.Connection;

				if (conn.State != ConnectionState.Open)
					conn.Open();

				// Lấy danh sách các roles được định nghĩa
				string query = "SELECT ROLE FROM DBA_ROLES WHERE ROLE LIKE 'NV_%' OR ROLE = 'SV' OR ROLE LIKE 'TEST_%' OR ROLE = 'ADMIN_ROLE'";
				OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
				DataTable allRolesTable = new DataTable();
				adapter.Fill(allRolesTable);

				// Lấy danh sách roles đã được gán cho user
				string grantedQuery = "SELECT GRANTED_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :username";
				OracleCommand grantedCmd = new OracleCommand(grantedQuery, conn);
				grantedCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username.ToUpper();

				List<string> grantedRoles = new List<string>();
				using (OracleDataReader reader = grantedCmd.ExecuteReader())
				{
					while (reader.Read())
					{
						grantedRoles.Add(reader.GetString(0));
					}
				}

				// Setup DataGridView
				dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
				dgvUser.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
				dgvUser.DataSource = null;
				dgvUser.Columns.Clear();

				DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
				{
					Name = "chk",
					HeaderText = "",
					Width = 40,
					ReadOnly = false,
					FalseValue = false,
					TrueValue = true,
					IndeterminateValue = false
				};
				dgvUser.Columns.Add(chk);
				dgvUser.Columns.Add("Role", "Role");

				// Thêm từng dòng và đánh dấu tick nếu user đã có role đó
				foreach (DataRow row in allRolesTable.Rows)
				{
					string roleName = row["ROLE"].ToString();
					int index = dgvUser.Rows.Add();
					dgvUser.Rows[index].Cells["Role"].Value = roleName;
					dgvUser.Rows[index].Cells["chk"].Value = grantedRoles.Contains(roleName);
				}

				// Cấu hình thêm cho DataGridView
				dgvUser.RowHeadersVisible = false;
				dgvUser.AllowUserToAddRows = false;
				dgvUser.MultiSelect = false;
				dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgvUser.EditMode = DataGridViewEditMode.EditOnEnter;
				dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
				dgvUser.CellClick += DgvUser_CellClick;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi hiển thị role:\n" + ex.Message);
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

		private void lbStudents_Click(object sender, EventArgs e)
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

		private void label6_Click(object sender, EventArgs e)
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}
	}
}
