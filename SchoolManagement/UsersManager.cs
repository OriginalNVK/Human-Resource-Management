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
				string userQuery = @"
            SELECT USERNAME 
            FROM ALL_USERS 
            WHERE USERNAME NOT IN (
                'SYS','SYSTEM','OUTLN','SYSBACKUP','SYSDG','SYSKM',
                'DBSNMP','APPQOSSYS','AUDSYS','GSMADMIN_INTERNAL','GSMUSER',
                'ANONYMOUS','XDB','CTXSYS','ORDDATA','ORDPLUGINS','ORDSYS',
                'MDSYS','LBACSYS','OLAPSYS','SI_INFORMTN_SCHEMA','DVSYS',
                'DVF','OJVMSYS','WMSYS','TSMSYS','GGSYS','REMOTE_SCHEDULER_AGENT',
                'ORACLE_OCM','XS$NULL','APEX_PUBLIC_USER','DIP','SPATIAL_CSW_ADMIN_USR',
                'SPATIAL_WFS_ADMIN_USR','MGMT_VIEW',
                'C##SCHOOL USER ADMIN','DBSFWUSER','DGPDB_INT','GSMCATUSER',
                'MDDATA','PDB_ADMIN','SYS$UMF','SYSRAC', 'C##SCHOOL_USER_ADMIN'
            ) AND USERNAME NOT LIKE 'C##%'
            ORDER BY USERNAME";

				using (OracleCommand cmd = new OracleCommand(userQuery, DatabaseSession.Connection))
				using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
				{
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					// Thêm cột role vào datatable
					dt.Columns.Add("ROLE", typeof(string));

					// Tìm role của từng user
					foreach (DataRow row in dt.Rows)
					{
						string username = row["USERNAME"].ToString();
						string role = "";

						// ADMIN
						using (OracleCommand roleCmd = new OracleCommand("SELECT 1 FROM PDB_ADMIN.QLDH_ADMIN WHERE MAAD = :username", DatabaseSession.Connection))
						{
							roleCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
							var result = roleCmd.ExecuteScalar();
							if (result != null)
							{
								role = "ADMIN";
							}
						}

						// NHAN VIEN
						if (role == "")
						{
							using (OracleCommand roleCmd = new OracleCommand("SELECT 1 FROM PDB_ADMIN.QLDH_NHANVIEN WHERE MANV = :username", DatabaseSession.Connection))
							{
								roleCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
								var result = roleCmd.ExecuteScalar();
								if (result != null)
								{
									role = "NHAN VIEN";
								}
							}
						}

						// SINH VIEN
						if (role == "")
						{
							using (OracleCommand roleCmd = new OracleCommand("SELECT 1 FROM PDB_ADMIN.QLDH_SINHVIEN WHERE MASV = :username", DatabaseSession.Connection))
							{
								roleCmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
								var result = roleCmd.ExecuteScalar();
								if (result != null)
								{
									role = "SINH VIEN";
								}
							}
						}

						row["ROLE"] = role;
					}

					// Cập nhật lưới
					dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
					dgvUser.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

					dgvUser.DataSource = null;
					dgvUser.Columns.Clear();

					// Thêm cột checkbox
					DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
					{
						Name = "chk",
						HeaderText = "",
						Width = 40,
						ReadOnly = false
					};
					dgvUser.Columns.Add(chk);

					// Cột Username và Role
					dgvUser.Columns.Add("Username", "Username");
					dgvUser.Columns.Add("Role", "Role");

					foreach (DataRow row in dt.Rows)
					{
						int index = dgvUser.Rows.Add();
						dgvUser.Rows[index].Cells["Username"].Value = row["USERNAME"];
						dgvUser.Rows[index].Cells["Role"].Value = row["ROLE"];
						dgvUser.Rows[index].Cells["chk"].Value = false;
					}

					dgvUser.RowHeadersVisible = false;
					dgvUser.AllowUserToAddRows = false;
					dgvUser.MultiSelect = false;
					dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

					dgvUser.CellClick -= DgvUser_CellClick;
					dgvUser.CellClick += DgvUser_CellClick;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi tải danh sách user Oracle:\n" + ex.Message);
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
				this.Close();
			}
			else
			{
				KryptonMessageBox.Show("Vui lòng chọn người dùng cần chỉnh sửa",
									 "Thông báo",
									 MessageBoxButtons.OK,
									 MessageBoxIcon.Warning);
			}
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

		private void pbDelete_Click(object sender, EventArgs e)
		{
			try
			{
				// Lấy connection chung đã mở từ DatabaseSession
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Kết nối chưa được khởi tạo hoặc chưa mở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Tìm username Oracle schema cần xóa
				string usernameToDelete = null;
				foreach (DataGridViewRow row in dgvUser.Rows)
				{
					if (Convert.ToBoolean(row.Cells["chk"].Value ?? false))
					{
						usernameToDelete = row.Cells["Username"].Value?.ToString();
						break;
					}
				}

				if (string.IsNullOrWhiteSpace(usernameToDelete))
				{
					MessageBox.Show("Vui lòng chọn user để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				// Xác nhận xóa
				var dlg = MessageBox.Show(
					$"Bạn có chắc chắn muốn xóa Oracle user '{usernameToDelete}' (kèm toàn bộ schema) không?",
					"Xác nhận xóa user",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning
				);
				if (dlg != DialogResult.Yes)
					return;

				string userType = DatabaseSession.DetermineUserType(conn, usernameToDelete.ToUpper());

				if (userType == null)
				{
					Console.WriteLine($"Không xác định được loại người dùng cho: {usernameToDelete}");
				}
				else
				{
					// Xoá dữ liệu ứng với loại người dùng
					string deleteQuery = "";
					switch (userType)
					{
						case "NhanVien":
							deleteQuery = "DELETE FROM PDB_ADMIN.QLDH_NHANVIEN WHERE MANV = :username";
							break;
						case "Admin":
							deleteQuery = "DELETE FROM PDB_ADMIN.QLDH_ADMIN WHERE MAAD = :username";
							break;
						case "SinhVien":
							deleteQuery = "DELETE FROM PDB_ADMIN.QLDH_SINHVIEN WHERE MASV = :username";
							break;
					}

					if (!string.IsNullOrEmpty(deleteQuery))
					{
						using (var cmdDelete = new OracleCommand(deleteQuery, conn))
						{
							cmdDelete.Parameters.Add("username", OracleDbType.Varchar2).Value = usernameToDelete.ToUpper();
							cmdDelete.ExecuteNonQuery();
							Console.WriteLine($"Đã xoá dữ liệu {userType} cho user: {usernameToDelete}");
						}
					}
				}

					// Thực thi DROP USER … CASCADE
					string sql = $"DROP USER \"{usernameToDelete.ToUpper()}\" CASCADE";
				using (var cmd = new OracleCommand(sql, conn))
				{
					cmd.ExecuteNonQuery();
				}

				MessageBox.Show($"Đã xóa user '{usernameToDelete}' thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Làm mới danh sách
				LoadUsers();
			}
			catch (OracleException oex)
			{
				MessageBox.Show($"Lỗi Oracle khi xóa user:\n{oex.Message}", "Lỗi Oracle", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi xóa user:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void label5_Click(object sender, EventArgs e)
        {
            AuditView auditView = new AuditView();
            this.Hide();
            auditView.ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void addNoticeBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
