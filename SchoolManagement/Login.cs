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
	public partial class Login : KryptonForm
	{
		public static string ID;
		public static string TYPE_USER;
		public Login()
		{
			InitializeComponent();

		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
			{
				error.Text = "Vui lòng nhập tên đăng nhập và mật khẩu";
				return;
			}

			// Try to authenticate with Oracle credentials
			string connectionString = BuildConnectionString(txtUsername.Text, txtPassword.Text);

			using (var conn = new OracleConnection(connectionString))
			{
				try
				{
					conn.Open();

					// Determine user type by checking which table contains their username
					string userType = DetermineUserType(conn, txtUsername.Text);

					if (userType != null)
					{
						ID = txtUsername.Text; // Store the username
						TYPE_USER = userType;  // Store the user type

						// Open appropriate form based on user type
						OpenUserForm(userType);
					}
					else
					{
						error.Text = "Tài khoản không tồn tại hoặc không có quyền truy cập";
					}
				}
				catch (OracleException ox)
				{
					// Oracle error 1017 is invalid username/password
					if (ox.Number == 1017)
					{
						error.Text = "Sai tên đăng nhập hoặc mật khẩu";
					}
					else
					{
						MessageBox.Show($"Lỗi Oracle: {ox.Message}");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Lỗi: {ex.Message}");
				}
			}
		}

		private string BuildConnectionString(string username, string password)
		{
			// Get base connection string from config
			string baseConnectionString = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

			// Create Oracle connection string builder
			OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder(baseConnectionString);

			// Replace with user-provided credentials
			builder.UserID = username;
			builder.Password = password;

			return builder.ToString();
		}

		private string DetermineUserType(OracleConnection conn, string username)
		{
			// Check if user is an admin
			string checkAdmin = "SELECT 1 FROM SYS.QLDH_ADMIN WHERE MAAD = :username";
			using (var cmd = new OracleCommand(checkAdmin, conn))
			{
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
				object result = cmd.ExecuteScalar();
				if (result != null) return "Admin";
			}

			// Check if user is an employee
			string checkEmployee = "SELECT 1 FROM SYS.QLDH_NHANVIEN WHERE MANV = :username";
			using (var cmd = new OracleCommand(checkEmployee, conn))
			{
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
				object result = cmd.ExecuteScalar();
				if (result != null) return "NhanVien";
			}

			// Check if user is a student
			string checkStudent = "SELECT 1 FROM SYS.QLDH_SINHVIEN WHERE MASV = :username";
			using (var cmd = new OracleCommand(checkStudent, conn))
			{
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
				object result = cmd.ExecuteScalar();
				if (result != null) return "SinhVien";
			}

			return null;
		}

		private void OpenUserForm(string userType)
		{
			Form formToOpen = null;

			switch (userType)
			{
				case "Admin":
					formToOpen = new AdminMenu();
					break;
				case "NhanVien":
					formToOpen = new TeacherManager(); // Or create a new EmployeeManager form
					break;
				case "SinhVien":
					formToOpen = new StudentMenu();
					break;
			}

			if (formToOpen != null)
			{
				this.Hide();
				formToOpen.ShowDialog();
				this.Close();
			}
		}
	}
}
