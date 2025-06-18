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
	public partial class UpdateStudentCTSV : KryptonForm
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

		public UpdateStudentCTSV(string username)
		{
			InitializeComponent();
			// Hiển thị thông tin cơ bản
			txtUsername.Text = username;

			// Load các thông tin chi tiết khác từ database nếu cần
			LoadInformation(username);
			getAllDepartment();
		}
		

		/// <summary>
		/// Lấy danh sách các đơn vị (khoa) từ cơ sở dữ liệu và hiển thị trong combo box.
		/// </summary>
		
		private void getAllDepartment()
		{
			try
			{
				// Lấy danh sách các đơn vị theo tên duy nhất, lấy MADV nhỏ nhất cho mỗi tên đơn vị
				string departmentQuery = @"SELECT MIN(MADV) AS MADV, TENDV
										   FROM pdb_admin.QLDH_DONVI
										   GROUP BY TENDV
										   ORDER BY TENDV";

				using (OracleCommand cmd = new OracleCommand(departmentQuery, DatabaseSession.Connection))
				using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
				{
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					// Không cần thêm cột hiển thị đầy đủ nữa, chỉ hiển thị TENDV
					comboDepartment.DataSource = dt;
					comboDepartment.DisplayMember = "TENDV"; // Hiển thị tên khoa (đơn vị) duy nhất
					comboDepartment.ValueMember = "MADV";   // Lưu MADV (lấy MADV đầu tiên của đơn vị đó)
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: \n" + ex.Message);
			}
		}

        private void LoadInformation(string username)
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
						SELECT s.MASV AS USERNAME,
							s.HOTEN, s.PHAI, s.NGSINH, s.DCHI, s.DT, s.KHOA
						FROM PDB_ADMIN.QLDH_SINHVIEN s
						WHERE s.MASV = :username
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

						// Các thông tin profile
						txtFullname.Text = reader.GetString(reader.GetOrdinal("HOTEN"));

						comboGender.SelectedItem = reader.GetString(reader.GetOrdinal("PHAI"));

						dateTimePickerBirthday.Value = reader.GetDateTime(reader.GetOrdinal("NGSINH"));

						txtAddress.Text = reader.GetString(reader.GetOrdinal("DCHI"));

						txtPhone.Text = reader.GetString(reader.GetOrdinal("DT"));

						// Khoa
						comboDepartment.SelectedValue = reader.GetString(reader.GetOrdinal("KHOA"));
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

			if (string.IsNullOrEmpty(username))
			{
				MessageBox.Show("Vui lòng điền Username.");
				return;
			}

			OracleConnection conn = DatabaseSession.Connection;
			if (conn == null || conn.State != ConnectionState.Open)
			{
				MessageBox.Show("Kết nối chưa khởi tạo hoặc chưa mở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Lấy thông tin từ các controls
			string fullname = txtFullname.Text.Trim();
			string gender = comboGender.SelectedItem?.ToString();
			string birthday = dateTimePickerBirthday.Value.ToString("dd/MM/yyyy");
			string address = txtAddress.Text.Trim();
			string phone = txtPhone.Text.Trim();
			string department = comboDepartment.SelectedValue?.ToString();

			if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(gender) ||
				string.IsNullOrEmpty(birthday) || string.IsNullOrEmpty(address) ||
				string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(department))
			{
				MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
				return;
			}

			string updateQuery = @"
				UPDATE PDB_ADMIN.QLDH_SINHVIEN
				SET HOTEN = :fullname,
				PHAI = :gender,
				NGSINH = TO_DATE(:birthday, 'DD/MM/YYYY'),
				DCHI = :address,
				DT = :phone,
				KHOA = :department
				WHERE MASV = :username";

			using (var cmd = new OracleCommand(updateQuery, conn))
			{
				cmd.BindByName = true;
				cmd.Parameters.Add("fullname", OracleDbType.Varchar2).Value = fullname;
				cmd.Parameters.Add("gender", OracleDbType.Varchar2).Value = gender;
				cmd.Parameters.Add("birthday", OracleDbType.Varchar2).Value = birthday;
				cmd.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
				cmd.Parameters.Add("phone", OracleDbType.Varchar2).Value = phone;
				cmd.Parameters.Add("department", OracleDbType.Varchar2).Value = department;
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

				int result = cmd.ExecuteNonQuery();

				if (result <= 0)
				{
				MessageBox.Show("Không tìm thấy thông tin người dùng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
				MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			}
			catch (OracleException oex)
			{
			MessageBox.Show($"Lỗi Oracle khi cập nhật thông tin:\n{oex.Message}", "Lỗi Oracle", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
			MessageBox.Show($"Lỗi khi cập nhật thông tin người dùng:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			// Quay lại form quản lý sinh viên
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StudentManager student = new StudentManager();
            this.Hide();
            student.ShowDialog();
            this.Close();
        }
    }
}
