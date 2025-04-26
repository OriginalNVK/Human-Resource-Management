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
		public UpdateUser()
        {
            InitializeComponent();
        }

		private void btnCreateUser_Click(object sender, EventArgs e)
		{
			try
			{
				// Lấy thông tin từ các controls trên form
				string username = txtUsername.Text.Trim();
				string password = txtPassword.Text.Trim();
				string role = RoleDropdown.SelectedItem?.ToString();
				string fullname = txtFullname.Text.Trim();
				string gender = GenderDropdown.SelectedItem?.ToString();
				string phoNum = txtPhoneNum.Text.Trim();
				string address = txtAddress.Text.Trim();
				string dob = dtpDOB.Value.ToString("dd-MMM-yyyy");

				// Kiểm tra các trường bắt buộc
				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
				{
					MessageBox.Show("Vui lòng điền đầy đủ thông tin bắt buộc (Username, Password, Role)");
					return;
				}

				string oradb = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

				using (OracleConnection conn = new OracleConnection(oradb))
				{
					conn.Open();
					OracleTransaction transaction = conn.BeginTransaction(); // Bắt đầu transaction

					try
					{
						// 1. Thêm vào bảng TAIKHOAN trước
						string queryAccount = "INSERT INTO SYS.TAIKHOAN (MATK, MATKHAU, CHUCVU) " +
											"VALUES (:username, :password, :role)";

						using (OracleCommand cmdAccount = new OracleCommand(queryAccount, conn))
						{
							cmdAccount.Transaction = transaction;
							cmdAccount.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
							cmdAccount.Parameters.Add("password", OracleDbType.Varchar2).Value = password;
							cmdAccount.Parameters.Add("role", OracleDbType.Varchar2).Value = role;

							int accountResult = cmdAccount.ExecuteNonQuery();
							if (accountResult <= 0)
							{
								transaction.Rollback();
								MessageBox.Show("Không thể thêm tài khoản");
								return;
							}
						}

						// 2. Thêm vào bảng tương ứng với role
						string roleSpecificQuery = "";
						switch (role)
						{
							case "ADMIN":
								roleSpecificQuery = "INSERT INTO SYS.QLDH_ADMIN (MAAD, HOTEN, PHAI, NGSINH, DCHI, DT) " +
												 "VALUES (:username, :fullname, :gender, TO_DATE(:dob, 'DD-MON-YYYY'), :address, :phoNum)";
								break;

							case "NHAN VIEN":
								// Lưu ý: Bảng NHANVIEN có thêm các trường LUONG, PHUCAP, VAITRO, MADV
								// Ở đây tôi giả định có thêm các control để nhập các thông tin này
								roleSpecificQuery = "INSERT INTO SYS.QLDH_NHANVIEN (MANV, HOTEN, PHAI, NGSINH, DCHI, DT, LUONG, PHUCAP, VAITRO, MADV) " +
												 "VALUES (:username, :fullname, :gender, TO_DATE(:dob, 'DD-MON-YYYY'), :address, :phoNum, 0, 0, 'NVCB', NULL)";
								break;

							case "SINH VIEN":
								// Lưu ý: Bảng SINHVIEN có thêm các trường KHOA, TINHTRANG
								// Ở đây tôi giả định có thêm các control để nhập các thông tin này
								roleSpecificQuery = "INSERT INTO SYS.QLDH_SINHVIEN (MASV, HOTEN, PHAI, NGSINH, DCHI, DT, KHOA, TINHTRANG) " +
												 "VALUES (:username, :fullname, :gender, TO_DATE(:dob, 'DD-MON-YYYY'), :address, :phoNum, NULL, 'Dang hoc')";
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
								MessageBox.Show("Không thể thêm thông tin chi tiết cho role");
								return;
							}
						}

						transaction.Commit(); // Commit transaction nếu cả 2 lệnh thành công
						MessageBox.Show("Thêm người dùng thành công!");

						// Cập nhật danh sách người dùng
						UsersManager userManager = new UsersManager();
						userManager.RefreshUserList();
						this.Close();
					}
					catch (Exception ex)
					{
						transaction.Rollback(); // Rollback nếu có lỗi
						MessageBox.Show("Lỗi khi thêm người dùng:\n" + ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu:\n" + ex.Message);
			}
		}
	}
}
