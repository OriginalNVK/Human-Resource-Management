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
    public partial class AddUser : KryptonForm
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
		public AddUser()
        {
            InitializeComponent();
			LoadAllRoles();
        }

		private void btnCreateUser_Click(object sender, EventArgs e)
		{
			try
			{
				// Lấy thông tin từ các controls trên form
				string username = txtUsername.Text.Trim().ToUpper();
				string password = txtPassword.Text.Trim();
				string role = RoleDropdown.SelectedItem?.ToString();
				string fullname = txtFullname.Text.Trim();
				string gender = GenderDropdown.SelectedItem?.ToString();
				string phoNum = txtPhoneNum.Text.Trim();
				string address = txtAddress.Text.Trim();
				string dob = dtpDOB.Value.ToString("dd-MMM-yyyy");

				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
				{
					MessageBox.Show("Vui lòng nhập đầy đủ Username, Password và Role.");
					return;
				}

				// Lấy danh sách role được chọn trong dgvUser
				List<string> grantedRoles = new List<string>();
				foreach (DataGridViewRow row in dgvUser.Rows)
				{
					bool isChecked = Convert.ToBoolean(row.Cells["chk"].Value ?? false);
					if (isChecked)
					{
						string roleName = row.Cells["Role"].Value?.ToString();
						if (!string.IsNullOrEmpty(roleName))
							grantedRoles.Add(roleName);
					}
				}

				if (grantedRoles.Count == 0)
				{
					MessageBox.Show("Vui lòng chọn ít nhất một quyền (role) cho người dùng.");
					return;
				}

				// Dùng connection từ DatabaseSession, KHÔNG dùng `using`
				var conn = DatabaseSession.Connection;
				if (conn.State != ConnectionState.Open)
					conn.Open();

				OracleTransaction transaction = conn.BeginTransaction();

				try
				{
					string createUserQuery = $"CREATE USER {username} IDENTIFIED BY \"{password}\"";
					using (OracleCommand cmdCreateUser = new OracleCommand(createUserQuery, conn))
					{
						cmdCreateUser.Transaction = transaction;
						cmdCreateUser.ExecuteNonQuery();
					}

					string grantConnectQuery = $"GRANT CONNECT TO {username}";
					using (OracleCommand cmdGrantConnect = new OracleCommand(grantConnectQuery, conn))
					{
						cmdGrantConnect.Transaction = transaction;
						cmdGrantConnect.ExecuteNonQuery();
					}

					foreach (string selectedRole in grantedRoles)
					{
						string grantRoleQuery = $"BEGIN EXECUTE IMMEDIATE 'GRANT {selectedRole} TO {username}'; END;";
						using (OracleCommand cmdGrantRole = new OracleCommand(grantRoleQuery, conn))
						{
							cmdGrantRole.Transaction = transaction;
							cmdGrantRole.ExecuteNonQuery();
						}
					}

					string roleInsertQuery = "";
					switch (role)
					{
						case "ADMIN":
							roleInsertQuery = "INSERT INTO SYS.QLDH_ADMIN (MAAD, HOTEN, PHAI, NGSINH, DCHI, DT) " +
											  "VALUES (:username, :fullname, :gender, TO_DATE(:dob, 'DD-MON-YYYY'), :address, :phoNum)";
							break;
						case "NHAN VIEN":
							roleInsertQuery = "INSERT INTO SYS.QLDH_NHANVIEN (MANV, HOTEN, PHAI, NGSINH, DCHI, DT, LUONG, PHUCAP, VAITRO, MADV) " +
											  "VALUES (:username, :fullname, :gender, TO_DATE(:dob, 'DD-MON-YYYY'), :address, :phoNum, 0, 0, 'NVCB', NULL)";
							break;
						case "SINH VIEN":
							roleInsertQuery = "INSERT INTO SYS.QLDH_SINHVIEN (MASV, HOTEN, PHAI, NGSINH, DCHI, DT, KHOA, TINHTRANG) " +
											  "VALUES (:username, :fullname, :gender, TO_DATE(:dob, 'DD-MON-YYYY'), :address, :phoNum, NULL, 'Dang hoc')";
							break;
						default:
							transaction.Rollback();
							MessageBox.Show("Role không hợp lệ.");
							return;
					}

					using (OracleCommand cmdInsertDetail = new OracleCommand(roleInsertQuery, conn))
					{
						cmdInsertDetail.Transaction = transaction;
						cmdInsertDetail.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
						cmdInsertDetail.Parameters.Add("fullname", OracleDbType.Varchar2).Value = fullname;
						cmdInsertDetail.Parameters.Add("gender", OracleDbType.Varchar2).Value = gender;
						cmdInsertDetail.Parameters.Add("dob", OracleDbType.Varchar2).Value = dob;
						cmdInsertDetail.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
						cmdInsertDetail.Parameters.Add("phoNum", OracleDbType.Varchar2).Value = phoNum;

						int result = cmdInsertDetail.ExecuteNonQuery();
						if (result <= 0)
						{
							transaction.Rollback();
							MessageBox.Show("Không thể thêm thông tin chi tiết người dùng.");
							return;
						}
					}

					transaction.Commit();
					MessageBox.Show("Tạo người dùng và phân quyền thành công!");

					UsersManager userManager = new UsersManager();
					this.Hide();
					userManager.ShowDialog();
					this.Close();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					MessageBox.Show("Lỗi khi tạo người dùng:\n" + ex.Message);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi kết nối cơ sở dữ liệu:\n" + ex.Message);
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
				var conn = DatabaseSession.Connection;

				if (conn.State != ConnectionState.Open)
					conn.Open();

				// Lấy danh sách các roles được định nghĩa
				string query = "SELECT ROLE FROM DBA_ROLES WHERE ROLE LIKE 'NV_%' OR ROLE = 'SV' OR ROLE LIKE 'TEST_%' OR ROLE = 'ADMIN_ROLE'";
				OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
				DataTable allRolesTable = new DataTable();
				adapter.Fill(allRolesTable);

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

				// Thêm từng dòng (không đánh dấu role nào sẵn)
				foreach (DataRow row in allRolesTable.Rows)
				{
					string roleName = row["ROLE"].ToString();
					int index = dgvUser.Rows.Add();
					dgvUser.Rows[index].Cells["Role"].Value = roleName;
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
