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
				string oradb = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

				using (OracleConnection conn = new OracleConnection(oradb))
				{
					conn.Open();
					string query = "";
					OracleCommand cmd;

					// Xác định query dựa trên role của user
					switch (role.ToUpper())
					{
						case "ADMIN":
							query = @"SELECT t.MATK, t.MATKHAU, t.CHUCVU, 
                                    a.HOTEN, a.PHAI, a.DT, a.DCHI, a.NGSINH
                             FROM SYS.TAIKHOAN t
                             JOIN SYS.QLDH_ADMIN a ON t.MATK = a.MAAD
                             WHERE t.MATK = :username";
							break;
						case "NHAN VIEN":
							query = @"SELECT t.MATK, t.MATKHAU, t.CHUCVU, 
                                    n.HOTEN, n.PHAI, n.DT, n.DCHI, n.NGSINH
                             FROM SYS.TAIKHOAN t
                             JOIN SYS.QLDH_NHANVIEN n ON t.MATK = n.MANV
                             WHERE t.MATK = :username";
							break;
						case "SINH VIEN":
							query = @"SELECT t.MATK, t.MATKHAU, t.CHUCVU, 
                                    s.HOTEN, s.PHAI, s.DT, s.DCHI, s.NGSINH
                             FROM SYS.TAIKHOAN t
                             JOIN SYS.QLDH_SINHVIEN s ON t.MATK = s.MASV
                             WHERE t.MATK = :username";
							break;
						default:
							throw new Exception("Vai trò người dùng không hợp lệ");
					}

					cmd = new OracleCommand(query, conn);
					cmd.Parameters.Add(":username", OracleDbType.Varchar2).Value = username;

					using (OracleDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							// Hiển thị thông tin cơ bản
							txtUsername.Text = reader["MATK"].ToString();
							txtPassword.Text = reader["MATKHAU"].ToString();
							RoleDropdown.Text = reader["CHUCVU"].ToString();
							txtFullname.Text = reader["HOTEN"].ToString();

							// Xử lý giới tính
							string gender = reader["PHAI"].ToString();
							if (gender == "Nam")
								GenderDropdown.Text = "Nam";
							else if (gender == "Nu")
								GenderDropdown.Text= "Nu";

							txtPhoneNum.Text = reader["DT"].ToString();
							txtAddress.Text = reader["DCHI"].ToString();

							// Xử lý ngày sinh
							if (reader["NGSINH"] != DBNull.Value)
							{
								DateTime birthDate = Convert.ToDateTime(reader["NGSINH"]);
								dtpDOB.Value = birthDate;
							}
						}
						else
						{
							MessageBox.Show("Không tìm thấy thông tin người dùng");
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi tải thông tin người dùng:\n" + ex.Message);
			}
		}

		private void btnUpdateUser_Click(object sender, EventArgs e)
		{
			try
			{
				// Lấy thông tin từ các controls trên form
				string username = txtUsername.Text.Trim();
				string password = txtPassword.Text.Trim();
				string role = RoleDropdown.SelectedItem?.ToString()?.Replace(" ", "");
				string fullname = txtFullname.Text.Trim();
				string gender = GenderDropdown.SelectedItem?.ToString();
				string phoNum = txtPhoneNum.Text.Trim();
				string address = txtAddress.Text.Trim();
				string dob = dtpDOB.Value.ToString("dd-MMM-yyyy");

				// Kiểm tra các trường bắt buộc
				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
				{
					MessageBox.Show("Vui lòng điền đầy đủ thông tin bắt buộc (Username, Role)");
					return;
				}

				string oradb = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

				using (OracleConnection conn = new OracleConnection(oradb))
				{
					conn.Open();
					OracleTransaction transaction = conn.BeginTransaction();

					try
					{
						// 1. Cập nhật bảng TAIKHOAN
						string queryAccount = "UPDATE SYS.TAIKHOAN SET ";
						bool hasPasswordUpdate = !string.IsNullOrEmpty(password);

						if (hasPasswordUpdate)
						{
							queryAccount += "MATKHAU = :password, ";
						}

						queryAccount += "CHUCVU = :role WHERE MATK = :username";

						using (OracleCommand cmdAccount = new OracleCommand(queryAccount, conn))
						{
							cmdAccount.Transaction = transaction;
							cmdAccount.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
							cmdAccount.Parameters.Add("role", OracleDbType.Varchar2).Value = role;

							if (hasPasswordUpdate)
							{
								cmdAccount.Parameters.Add("password", OracleDbType.Varchar2).Value = password;
							}

							int accountResult = cmdAccount.ExecuteNonQuery();
							if (accountResult <= 0)
							{
								transaction.Rollback();
								MessageBox.Show("Không tìm thấy tài khoản để cập nhật");
								return;
							}
						}

						// 2. Cập nhật bảng tương ứng với role
						string roleSpecificQuery = "";
						switch (role)
						{
							case "ADMIN":
								roleSpecificQuery = @"UPDATE SYS.QLDH_ADMIN 
                                           SET HOTEN = :fullname, 
                                               PHAI = :gender, 
                                               NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
                                               DCHI = :address, 
                                               DT = :phoNum 
                                           WHERE MAAD = :username";
								break;

							case "NHANVIEN":
								roleSpecificQuery = @"UPDATE SYS.QLDH_NHANVIEN 
                                            SET HOTEN = :fullname, 
                                                PHAI = :gender, 
                                                NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
                                                DCHI = :address, 
                                                DT = :phoNum 
                                            WHERE MANV = :username";
								break;

							case "SINHVIEN":
								roleSpecificQuery = @"UPDATE SYS.QLDH_SINHVIEN 
                                            SET HOTEN = :fullname, 
                                                PHAI = :gender, 
                                                NGSINH = TO_DATE(:dob, 'DD-MON-YYYY'), 
                                                DCHI = :address, 
                                                DT = :phoNum 
                                            WHERE MASV = :username";
								break;

							default:
								transaction.Rollback();
								MessageBox.Show("Role không hợp lệ");
								return;
						}

						using (OracleCommand cmdRole = new OracleCommand(roleSpecificQuery, conn))
						{
							cmdRole.Transaction = transaction;
							cmdRole.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
							cmdRole.Parameters.Add("fullname", OracleDbType.Varchar2).Value = fullname;
							cmdRole.Parameters.Add("gender", OracleDbType.Varchar2).Value = gender;
							cmdRole.Parameters.Add("dob", OracleDbType.Varchar2).Value = dob;
							cmdRole.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
							cmdRole.Parameters.Add("phoNum", OracleDbType.Varchar2).Value = phoNum;

							int roleResult = cmdRole.ExecuteNonQuery();
							if (roleResult <= 0)
							{
								transaction.Rollback();
								MessageBox.Show("Không tìm thấy thông tin chi tiết để cập nhật");
								return;
							}
						}

						transaction.Commit();
						MessageBox.Show("Cập nhật người dùng thành công!");

						// Cập nhật danh sách người dùng
						UsersManager userManager = new UsersManager();
						userManager.RefreshUserList();
						this.Close();
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						MessageBox.Show("Lỗi khi cập nhật người dùng:\n" + ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu:\n" + ex.Message);
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
                string oradb = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

                using (OracleConnection conn = new OracleConnection(oradb))
                {
                    conn.Open();
                    string query = "SELECT ROLE FROM DBA_ROLES WHERE ROLE LIKE 'NV_%' OR ROLE = 'SV' OR ROLE LIKE 'TEST_%'";

                    OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Tắt auto size trước
                    dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvUser.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                    // Xóa dữ liệu cũ
                    dgvUser.DataSource = null;
                    dgvUser.Columns.Clear();

                    // 1. Tạo cột checkbox
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

                    // 2. Thêm cột Role
                    dgvUser.Columns.Add("Role", "Role");

                    // 3. Đổ dữ liệu thủ công
                    foreach (DataRow row in dt.Rows)
                    {
                        int index = dgvUser.Rows.Add();
                        dgvUser.Rows[index].Cells["Role"].Value = row["ROLE"];
                        dgvUser.Rows[index].Cells["chk"].Value = false; // Mặc định không chọn
                    }

                    // 4. Cấu hình thêm
                    dgvUser.RowHeadersVisible = false;
                    dgvUser.AllowUserToAddRows = false;
                    dgvUser.MultiSelect = false;
                    dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvUser.EditMode = DataGridViewEditMode.EditOnEnter;

                    // 5. Bật lại AutoSize nếu cần
                    dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // 6. Gán sự kiện CellClick nếu cần
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
