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
	public partial class UpdateStudent : KryptonForm
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
		private string _mainStatus;

		private ComponentFactory.Krypton.Toolkit.KryptonButton btnRevokeAllPrivileges;
        private string v;

        public UpdateStudent(string username, string status)
		{
			InitializeComponent();
			// Hiển thị thông tin cơ bản
			txtUsername.Text = username;
            StatusDropDown.Text = status;
			_mainStatus = status;
			// Load các thông tin chi tiết khác từ database nếu cần
			LoadInformation(username, status);
		}

        public UpdateStudent(string v)
        {
            this.v = v;
        }

        private void LoadInformation(string username, string status)
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

						string query = @"
						SELECT u.USERNAME,
							s.HOTEN, s.PHAI, s.DT, s.DCHI, s.NGSINH
						FROM PDB_ADMIN.QLDH_SINHVIEN s
						JOIN ALL_USERS u ON u.USERNAME = s.MASV
						WHERE u.USERNAME = :username
						";


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

						// Status truyền vào
						StatusDropDown.Text = status;

						// Các thông tin profile
						txtFullname.Text = reader.GetString(reader.GetOrdinal("HOTEN"));
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

		private void btnUpdateStudent_Click(object sender, EventArgs e)
		{
			try
			{
				string username = txtUsername.Text.Trim().ToUpper();
				string status = StatusDropDown.SelectedItem?.ToString();

				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(status))
				{
					MessageBox.Show("Vui lòng điền Username và chọn Status.");
					return;
				}

				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Kết nối chưa khởi tạo hoặc chưa mở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				MessageBox.Show($"SẴN SÀNG UPDATE:\nMASV = {username}\nTINHTRANG = {status}");

				string updateDetails = @"UPDATE PDB_ADMIN.QLDH_SINHVIEN 
										SET TINHTRANG = :status
										WHERE MASV = :username";

				using (OracleCommand updateCmd = new OracleCommand(updateDetails, conn))
				{
					updateCmd.CommandTimeout = 30;
					updateCmd.BindByName = true;
					updateCmd.Parameters.Add("status", OracleDbType.Varchar2).Value = status;
					updateCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

					int result = updateCmd.ExecuteNonQuery();

					if (result <= 0)
					{
						MessageBox.Show("Không tìm thấy thông tin người dùng để cập nhật.");
					}
					else
					{
						MessageBox.Show("Cập nhật người dùng thành công!");
					}
				}
			}
			catch (OracleException ex)
			{
				MessageBox.Show("Lỗi Oracle:\n" + ex.Message);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi hệ thống:\n" + ex.Message);
			}

			// Quay lại form
			try
			{
				this.Hide();
				StudentManager studentManager = new StudentManager();
				studentManager.ShowDialog();
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi mở lại form quản lý:\n" + ex.Message);
			}
		}




	
		private void DgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Chỉ xử lý click vào cột checkbox

			// Toggle trạng thái checkbox
			bool currentValue = Convert.ToBoolean(dgvStudent.Rows[e.RowIndex].Cells["chk"].Value ?? false);
			dgvStudent.Rows[e.RowIndex].Cells["chk"].Value = !currentValue;

			// Bỏ chọn các dòng khác
			foreach (DataGridViewRow row in dgvStudent.Rows)
			{
				if (row.Index != e.RowIndex)
				{
					row.Cells["chk"].Value = false;
				}
			}
		}

		private void lbClasses_Click(object sender, EventArgs e)
		{
			SubjectManagement subjectManager = new SubjectManagement();
			this.Hide();
			subjectManager.ShowDialog();
			this.Close();

		}

		private void label4_Click(object sender, EventArgs e)
		{
			ClassList classList = new ClassList();
			this.Hide();
			classList.ShowDialog();
			this.Close();
		}

		private void lbStudents_Click(object sender, EventArgs e)
		{
			StudentManager studentManager = new StudentManager();
			this.Hide();
			studentManager.ShowDialog();
			this.Close();
		}

		private void lbTeachers_Click(object sender, EventArgs e)
		{
			PersonnelManager personnelManager = new PersonnelManager();
			this.Hide();
			personnelManager.ShowDialog();
			this.Close();
		}

		private void lbProfile_Click(object sender, EventArgs e)
		{
			PersonnelMenu personnelMenu = new PersonnelMenu(Login.ID);
			this.Hide();
			personnelMenu.ShowDialog();
			this.Close();
		}

		private void label6_Click(object sender, EventArgs e)
		{
			ViewSchedule viewDetail = new ViewSchedule(PersonnelMenu._username, PersonnelMenu._role);
			this.Hide();
			viewDetail.ShowDialog();
			this.Close();
		}

		private void label8_Click(object sender, EventArgs e)
		{
			PersonnelRegister personnelRegister = new PersonnelRegister();
			this.Hide();
			personnelRegister.ShowDialog();
			this.Close();
		}

		private void logoutBtn_Click(object sender, EventArgs e)
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}
	}
}
