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
		public UpdateUser(string username, string role)
        {
            InitializeComponent();
			// Hiển thị thông tin cơ bản
			txtUsername.Text = username;
			RoleDropdown.Text = role;

			// Load các thông tin chi tiết khác từ database nếu cần
			LoadInformation(username, role);
			LoadAllRoles();
        }

		private void LoadInformation(string username, string role)
		{
			try
			{
				// Lấy connection đã được mở ở login
				OracleConnection conn = DatabaseSession.Connection;
				MessageBox.Show(conn.ToString());
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
              FROM SYS.QLDH_ADMIN a
              JOIN ALL_USERS u ON u.USERNAME = a.MAAD
             WHERE u.USERNAME = :username";
						break;

					case "NHAN VIEN":
						query = @"
            SELECT u.USERNAME,
                   n.HOTEN, n.PHAI, n.DT, n.DCHI, n.NGSINH
              FROM SYS.QLDH_NHANVIEN n
              JOIN ALL_USERS u ON u.USERNAME = n.MANV
             WHERE u.USERNAME = :username";
						break;

					case "SINH VIEN":
						query = @"
            SELECT u.USERNAME,
                   s.HOTEN, s.PHAI, s.DT, s.DCHI, s.NGSINH
              FROM SYS.QLDH_SINHVIEN s
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
						string gender = reader.GetString(reader.GetOrdinal("PHAI"));
						GenderDropdown.Text = gender.Equals("Nam", StringComparison.OrdinalIgnoreCase) ? "Nam" : "Nu";

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
				string username = txtUsername.Text.Trim().ToUpper();
				string password = txtPassword.Text.Trim();
				string role = dgvUser.Rows.Cast<DataGridViewRow>()
								 .Where(r => Convert.ToBoolean(r.Cells["chk"].Value ?? false))
								 .Select(r => r.Cells["Role"].Value.ToString())
								 .FirstOrDefault();

				string fullname = txtFullname.Text.Trim();
				string gender = GenderDropdown.SelectedItem?.ToString();
				string phoNum = txtPhoneNum.Text.Trim();
				string address = txtAddress.Text.Trim();
				string dob = dtpDOB.Value.ToString("dd-MMM-yyyy");

				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
				{
					MessageBox.Show("Vui lòng điền Username và chọn Role.");
					return;
				}

				// KHÔNG dùng using vì DatabaseSession.Connection là static/singleton
				OracleConnection conn = DatabaseSession.Connection;
				MessageBox.Show(conn.ToString());
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Kết nối chưa khởi tạo hoặc chưa mở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				OracleTransaction transaction = conn.BeginTransaction();

				try
				{
					if (!string.IsNullOrEmpty(password))
					{
						string alterQuery = $"ALTER USER {username} IDENTIFIED BY \"{password}\"";
						using (OracleCommand alterCmd = new OracleCommand(alterQuery, conn))
						{
							alterCmd.Transaction = transaction;
							alterCmd.ExecuteNonQuery();
						}
					}

					string grantQuery = $"GRANT {role} TO {username}";
					using (OracleCommand grantCmd = new OracleCommand(grantQuery, conn))
					{
						grantCmd.Transaction = transaction;
						grantCmd.ExecuteNonQuery();
					}

					string updateDetails = "";
					switch (role.ToUpper())
					{
						case "ADMIN":
							updateDetails = @"UPDATE SYS.QLDH_ADMIN 
						SET HOTEN = :fullname, PHAI = :gender, 
							NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
							DCHI = :address, DT = :phoNum 
						WHERE MAAD = :username";
							break;
						case "NHAN VIEN":
							updateDetails = @"UPDATE SYS.QLDH_NHANVIEN 
						SET HOTEN = :fullname, PHAI = :gender, 
							NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
							DCHI = :address, DT = :phoNum 
						WHERE MANV = :username";
							break;
						case "SINH VIEN":
							updateDetails = @"UPDATE SYS.QLDH_SINHVIEN 
						SET HOTEN = :fullname, PHAI = :gender, 
							NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
							DCHI = :address, DT = :phoNum 
						WHERE MASV = :username";
							break;
						default:
							transaction.Rollback();
							MessageBox.Show("Role không hợp lệ.");
							return;
					}

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

					transaction.Commit();
					MessageBox.Show("Cập nhật người dùng thành công!");
					this.Close();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					MessageBox.Show("Lỗi khi cập nhật người dùng:\n" + ex.Message);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi kết nối CSDL:\n" + ex.Message);
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

		private void LoadAllRoles()
		{
			try
			{
				using (OracleConnection conn = DatabaseSession.Connection)
				{
					if (conn.State != ConnectionState.Open)
						conn.Open();

					string query = "SELECT ROLE FROM DBA_ROLES WHERE ROLE LIKE 'NV_%' OR ROLE = 'SV' OR ROLE LIKE 'TEST_%' OR ROLE = 'ADMIN_ROLE'";
					OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
					DataTable dt = new DataTable();
					adapter.Fill(dt);

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

					foreach (DataRow row in dt.Rows)
					{
						int index = dgvUser.Rows.Add();
						dgvUser.Rows[index].Cells["Role"].Value = row["ROLE"];
						dgvUser.Rows[index].Cells["chk"].Value = false;
					}

					dgvUser.RowHeadersVisible = false;
					dgvUser.AllowUserToAddRows = false;
					dgvUser.MultiSelect = false;
					dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					dgvUser.EditMode = DataGridViewEditMode.EditOnEnter;
					dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
					dgvUser.CellClick += DgvUser_CellClick;
				}
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
