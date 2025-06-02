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
	public partial class PersonnelMenu : KryptonForm
	{
		public static string _username;
		private string _originalPhoneNumber;    // lưu số điện thoại ban đầu
		private string _changePassword = null;  // gán nếu người dùng muốn thay đổi mật khẩu
		public static string _role = null;

		public PersonnelMenu(string username)
		{
			InitializeComponent();
			_username = username;
			LoadPersonnelInfo();
		}

		private void LoadPersonnelInfo()
		{
			txtPhone.ReadOnly = false;
			try
			{
				string query = @"
            SELECT HOTEN, PHAI, NGSINH, LUONG, PHUCAP, DCHI, DT, VAITRO, TENDV
            FROM PDB_ADMIN.QLDH_NHANVIEN NV
			JOIN PDB_ADMIN.QLDH_DONVI DV ON NV.MADV = DV.MADV
            WHERE MANV = :username";

				using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
				{
					cmd.Parameters.Add(new OracleParameter("username", _username));

					using (OracleDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							txtHoTen.Text = reader.GetString(0);       // HOTEN
							txtGender.Text = reader.GetString(1);      // PHAI

							DateTime dob = reader.GetDateTime(2);      // NGSINH
							txtBirth.Text = dob.ToString("dd/MM/yyyy");

							decimal salary = reader.GetDecimal(3);     // LUONG
							txtSalary.Text = salary.ToString("N0");

							decimal bonus = reader.GetDecimal(4);      // PHUCAP
							txtBonus.Text = bonus.ToString("N0");

							txtAddress.Text = reader.GetString(5);     // DCHI
							txtPhone.Text = reader.GetString(6);       // DT
							_originalPhoneNumber = txtPhone.Text;
							txtRoleName.Text = reader.GetString(7);    // VAITRO
							txtDepartment.Text = reader.GetString(8);  // TENDV
							txtHello.Text = $"Hello, {reader.GetString(0)}";
							_role = reader.GetString(7);
						}
						else
						{
							MessageBox.Show("Không tìm thấy nhân viên!");
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi truy vấn: " + ex.Message);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				// Lấy số điện thoại hiện tại trong textbox
				string currentPhone = txtPhone.Text.Trim();
				bool isPhoneChanged = currentPhone != _originalPhoneNumber;
				_changePassword = txtNewPassword.Text.Trim();

				using (OracleCommand cmd = new OracleCommand())
				{
					cmd.Connection = DatabaseSession.Connection;

					if (isPhoneChanged)
					{
						cmd.CommandText = "UPDATE PDB_ADMIN.QLDH_NHANVIEN SET DT = :newPhone WHERE MANV = :username";
						cmd.Parameters.Add(new OracleParameter("newPhone", currentPhone));
						cmd.Parameters.Add(new OracleParameter("username", _username));
						cmd.ExecuteNonQuery();
						MessageBox.Show("Cập nhật số điện thoại thành công!");
					}

					if (!string.IsNullOrEmpty(_changePassword))
					{
						cmd.Parameters.Clear(); // reset tham số
						cmd.CommandText = "ALTER USER " + _username + " IDENTIFIED BY \"" + _changePassword + "\"";
						cmd.ExecuteNonQuery();
						MessageBox.Show("Cập nhật mật khẩu thành công!");
					}

					if (!isPhoneChanged && string.IsNullOrEmpty(_changePassword))
					{
						MessageBox.Show("Không có thay đổi nào để lưu.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
			}
		}

		private void pbLogout_Click(object sender, EventArgs e)
		{
			LogOut();
		}

		private void LogOut()
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}

		private void pbProfile_Click(object sender, EventArgs e)
		{
			PersonnelMenu personnelMenu = new PersonnelMenu(Login.ID);
			this.Hide();
			personnelMenu.ShowDialog();
			this.Close();
		}

		private void pbStudents_Click(object sender, EventArgs e)
		{
			StudentManager studentManager = new StudentManager();
			this.Hide();
			studentManager.ShowDialog();
			this.Close();
		}

		private void pbPersonnel_Click(object sender, EventArgs e)
		{
			PersonnelManager personnelManager = new PersonnelManager();
			this.Hide();
			personnelManager.ShowDialog();
			this.Close();
		}

		private void pbClass_Click(object sender, EventArgs e)
		{
			ClassList classList = new ClassList();
			this.Hide();
			classList.ShowDialog();
			this.Close();
		}

		private void pbUsers_Click(object sender, EventArgs e)
		{
			UsersManager userManager = new UsersManager();
			this.Hide();
			userManager.ShowDialog();
			this.Close();
		}

		private void pbSubject_Click(object sender, EventArgs e)
		{
			SubjectManagement subjectManager = new SubjectManagement();
			this.Hide();
			subjectManager.ShowDialog();
			this.Close();
		}

		private void label5_Click(object sender, EventArgs e)
		{
			PersonnelRegister personnelRegister = new PersonnelRegister();
			this.Hide();
			personnelRegister.ShowDialog();
			this.Close();
		}

        private void view_Schedule(object sender, EventArgs e)
        {
			ViewSchedule viewDetail = new ViewSchedule(_username, Login.TYPE_ROLE);
			this.Hide();
			viewDetail.ShowDialog();
			this.Close();
        }
    }
}
