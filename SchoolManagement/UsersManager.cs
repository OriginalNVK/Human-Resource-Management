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
	public partial class UsersManager : KryptonForm
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
		private int action; // 0 - add, 1 - edit
		private bool isSelected = false;
		private int currFrom = 1;
		private int pageSize = 10;

		public static string ClassSectionID;
		public static string SubjectID;
		public static int limited;
		public UsersManager()
		{
			InitializeComponent();
			LoadUsers();
		}
		private void LoadUsers()
		{
			try
			{
				string oradb = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

				using (OracleConnection conn = new OracleConnection(oradb))
				{
					conn.Open();
					string query = "SELECT MATK AS USERNAME, CHUCVU AS ROLE FROM SYS.TAIKHOAN";

					OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					// Tắt tất cả chế độ auto size trước
					dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
					dgvUser.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

					// Xóa dữ liệu cũ
					dgvUser.DataSource = null;
					dgvUser.Columns.Clear();

					// 1. Tạo cột checkbox với đầy đủ thuộc tính
					DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
					{
						Name = "chk",
						HeaderText = "",
						Width = 40,
						ReadOnly = false, // QUAN TRỌNG: Phải để false
						FalseValue = false,
						TrueValue = true,
						IndeterminateValue = false
					};
					dgvUser.Columns.Add(chk);

					// 2. Thêm cột dữ liệu
					dgvUser.Columns.Add("Username", "Username");
					dgvUser.Columns.Add("Role", "Role");

					// 3. Đổ dữ liệu thủ công
					foreach (DataRow row in dt.Rows)
					{
						int index = dgvUser.Rows.Add();
						dgvUser.Rows[index].Cells["Username"].Value = row["USERNAME"];
						dgvUser.Rows[index].Cells["Role"].Value = row["ROLE"];
						dgvUser.Rows[index].Cells["chk"].Value = false; // Mặc định không chọn
					}

					// 4. Cấu hình quan trọng
					dgvUser.RowHeadersVisible = false;
					dgvUser.AllowUserToAddRows = false;
					dgvUser.MultiSelect = false;
					dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

					// 5. Bật lại AutoSize nếu cần (SAU khi đã ẩn row headers)
					dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

					// 6. Gán sự kiện CellClick (QUAN TRỌNG)
					dgvUser.CellClick += DgvUser_CellClick;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi tải dữ liệu người dùng:\n" + ex.Message);
			}
		}

		// Xử lý sự kiện click checkbox
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


		private void dgvClass_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				isSelected = true;
				//showAction();

				//DataGridViewRow row = dgvClass.Rows[e.RowIndex];
				//txtID.Text = row.Cells[0].Value.ToString();
				//cbSubject.Text = row.Cells[1].Value.ToString();
				//cbTeacher.Text = row.Cells[2].Value.ToString();

				//txtSchedule.Text = row.Cells[3].Value.ToString();
				//txtNOS.Text = row.Cells[4].Value.ToString();

				//ClassSectionID = txtID.Text;
				//SubjectID = cbSubject.Text;
				//limited = Int32.Parse(txtNOS.Text);
			}
		}

		private void pbStudents_Click(object sender, EventArgs e)
		{
			AddUser addUser = new AddUser();
			this.Hide();
			addUser.ShowDialog();
			this.Close();
		}

		private void pbEdit_Click(object sender, EventArgs e)
		{
			var selectedRow = dgvUser.Rows.Cast<DataGridViewRow>()
				.FirstOrDefault(row => row.Cells["chk"].Value != null &&
									 Convert.ToBoolean(row.Cells["chk"].Value));

			if (selectedRow != null)
			{
				UpdateUser updateForm = new UpdateUser(
					selectedRow.Cells["Username"].Value.ToString(),
					selectedRow.Cells["Role"].Value.ToString());

				this.Hide();
				updateForm.ShowDialog();
				this.Show();

				// Refresh dữ liệu sau khi chỉnh sửa
				LoadUsers();
			}
			else
			{
				KryptonMessageBox.Show("Vui lòng chọn người dùng cần chỉnh sửa",
									 "Thông báo",
									 MessageBoxButtons.OK,
									 MessageBoxIcon.Warning);
			}
		}

		private void btnDeleteUser_Click(object sender, EventArgs e)
		{
			//try
			//{
			//	// Lấy username và role từ các controls
			//	string username = txtUsername.Text.Trim();
			//	string role = RoleDropdown.SelectedItem?.ToString();

			//	// Kiểm tra dữ liệu đầu vào
			//	if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
			//	{
			//		MessageBox.Show("Vui lòng chọn Username và Role để xóa.");
			//		return;
			//	}

			//	string oradb = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;

			//	using (OracleConnection conn = new OracleConnection(oradb))
			//	{
			//		conn.Open();
			//		OracleTransaction transaction = conn.BeginTransaction(); // Bắt đầu transaction

			//		try
			//		{
			//			// 1. Xóa dữ liệu từ bảng tương ứng với Role trước
			//			string roleSpecificDeleteQuery = "";

			//			switch (role)
			//			{
			//				case "ADMIN":
			//					roleSpecificDeleteQuery = "DELETE FROM SYS.QLDH_ADMIN WHERE MAAD = :username";
			//					break;

			//				case "NHAN VIEN":
			//					roleSpecificDeleteQuery = "DELETE FROM SYS.QLDH_NHANVIEN WHERE MANV = :username";
			//					break;

			//				case "SINH VIEN":
			//					roleSpecificDeleteQuery = "DELETE FROM SYS.QLDH_SINHVIEN WHERE MASV = :username";
			//					break;

			//				default:
			//					transaction.Rollback();
			//					MessageBox.Show("Role không hợp lệ");
			//					return;
			//			}

			//			using (OracleCommand cmdRoleDelete = new OracleCommand(roleSpecificDeleteQuery, conn))
			//			{
			//				cmdRoleDelete.Transaction = transaction;
			//				cmdRoleDelete.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

			//				int roleDeleteResult = cmdRoleDelete.ExecuteNonQuery();
			//				if (roleDeleteResult <= 0)
			//				{
			//					transaction.Rollback();
			//					MessageBox.Show("Không thể xóa thông tin chi tiết cho role");
			//					return;
			//				}
			//			}

			//			// 2. Xóa tài khoản trong bảng TAIKHOAN
			//			string deleteAccountQuery = "DELETE FROM SYS.TAIKHOAN WHERE MATK = :username";

			//			using (OracleCommand cmdAccountDelete = new OracleCommand(deleteAccountQuery, conn))
			//			{
			//				cmdAccountDelete.Transaction = transaction;
			//				cmdAccountDelete.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

			//				int accountDeleteResult = cmdAccountDelete.ExecuteNonQuery();
			//				if (accountDeleteResult <= 0)
			//				{
			//					transaction.Rollback();
			//					MessageBox.Show("Không thể xóa tài khoản");
			//					return;
			//				}
			//			}

			//			transaction.Commit(); // Commit transaction nếu mọi thứ thành công
			//			MessageBox.Show("Xóa người dùng thành công!");

			//			// Cập nhật danh sách người dùng
			//			UsersManager userManager = new UsersManager();
			//			this.Hide();
			//			userManager.ShowDialog();
			//			this.Close();
			//		}
			//		catch (Exception ex)
			//		{
			//			transaction.Rollback(); // Rollback nếu có lỗi
			//			MessageBox.Show("Lỗi khi xóa người dùng:\n" + ex.Message);
			//		}
			//	}
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu:\n" + ex.Message);
			//}
		}


		private void pbNext_Click(object sender, EventArgs e)
		{

			currFrom++;
			//LoadClasses();
		}

		private void pbPrev_Click(object sender, EventArgs e)
		{

			if (currFrom > 1)
			{
				currFrom--;
				//LoadClasses();
			}
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{

		}

		private void UsersManager_Load(object sender, EventArgs e)
		{

		}

		private void Dashboard_Click(object sender, EventArgs e)
		{

		}

		private void lbHello_Click(object sender, EventArgs e)
		{

		}

		private void dgvClass_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		public void RefreshUserList()
		{
			LoadUsers(); // Gọi private method
		}

		private void lbUsers_Click(object sender, EventArgs e)
		{
			UsersManager userManager = new UsersManager();
			this.Hide();
			userManager.ShowDialog();
			this.Close();
		}

		private void lbRole_Click(object sender, EventArgs e)
		{
			RoleManager roleManager = new RoleManager();
			this.Hide();
			roleManager.ShowDialog();
			this.Close();
		}

		private void lbStudent_Click(object sender, EventArgs e)
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

		private void label4_Click(object sender, EventArgs e)
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}
	}
}
