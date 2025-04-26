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
			if (txtUsername.Text == "" || txtPassword.Text == "")
			{
				error.Text = "Invalid ID or Password";
				return;
			}
			string oradb = ConfigurationManager
			   .ConnectionStrings["SchoolDB"]
			   .ConnectionString;


			using (var conn = new OracleConnection(oradb))
			{
				try
				{
					conn.Open();
					OracleCommand cmd = new OracleCommand();
					cmd.Connection = conn;
					cmd.CommandText = "Select * from SYS.TAIKHOAN";
					cmd.CommandText = $"SELECT MATK, CHUCVU FROM SYS.TAIKHOAN WHERE MATK='{txtUsername.Text}' AND MATKHAU=standard_hash('{txtPassword.Text}', 'MD5')";
					cmd.CommandType = CommandType.Text;
					OracleDataReader dr = cmd.ExecuteReader();
					//MessageBox.Show($"Row count = {dr}");
					if (dr.HasRows)
					{
						dr.Read();
						ID = dr.GetString(0);
						TYPE_USER = dr.GetString(1);
						if (dr.GetString(1) == "Admin")
						{
							AdminMenu adminMenu = new AdminMenu();
							this.Hide();
							adminMenu.ShowDialog();
							this.Close();
						}
						else if (dr.GetString(1) == "Giao Vien")
						{
							TeacherManager teacherMenu = new TeacherManager();
							this.Hide();
							teacherMenu.ShowDialog();
							this.Close();
						}
						else
						{
							StudentMenu student = new StudentMenu();
							this.Hide();
							student.ShowDialog();
							this.Close();
						}
					}
					else
					{
						error.Text = "Invalid ID or Password";
					}

					conn.Dispose();
				}
				catch (OracleException ox)
				{
					MessageBox.Show($"Oracle error: {ox.Message}");
				}
			}
		}
	}
}
