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

			ID = txtUsername.Text;

			// Gọi InitializeSession để xác thực và thiết lập phiên làm việc
			bool success = DatabaseSession.InitializeSession(txtUsername.Text, txtPassword.Text);
			
            if (success)
			{
				// Đăng nhập thành công, mở form tương ứng
				string userType = DatabaseSession.UserType;
                OpenUserForm(userType);
			}
			else
			{
				error.Text = "Sai tên đăng nhập, mật khẩu hoặc tài khoản không có quyền truy cập";
			}
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
					formToOpen = new PersonnelMenu(txtUsername.Text); // Or create a new EmployeeManager form
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
