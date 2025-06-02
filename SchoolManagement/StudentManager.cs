using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

//using Microsoft.Office.Interop.Excel;
using Oracle.ManagedDataAccess.Client;

namespace SchoolManagement
{
	public partial class StudentManager : KryptonForm
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

		public StudentManager()
		{
			InitializeComponent();
			lbHello.Text = $"Hello, {Login.ID}";
			string loginId = Login.ID;

			LoadStudents();
		}

		private void LoadStudents()
		{
			// check role
			MessageBox.Show("Role: " + PersonnelMenu._role, "Role Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
			if (PersonnelMenu._role == "GV")
			{
				try
				{
					OracleConnection conn = DatabaseSession.Connection;
					if (conn == null || conn.State != ConnectionState.Open)
					{
						MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					string sql = @"
					SELECT TENHP, MASV, HOTEN, PHAI, NGSINH, DCHI, DT, TINHTRANG, DIEMTH, DIEMQT, DIEMCK, DIEMTK
					FROM PDB_ADMIN.V_STUDENTS_BY_GV";

					OracleCommand cmd = new OracleCommand(sql, conn);
					OracleDataAdapter adapter = new OracleDataAdapter(cmd);
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					// Đổi tên cột hiển thị
					dt.Columns["TENHP"].ColumnName = "TÊN HỌC PHẦN";
					dt.Columns["MASV"].ColumnName = "MÃ SV";
					dt.Columns["HOTEN"].ColumnName = "HỌ TÊN";
					dt.Columns["PHAI"].ColumnName = "PHÁI";
					dt.Columns["NGSINH"].ColumnName = "NGÀY SINH";
					dt.Columns["DCHI"].ColumnName = "ĐỊA CHỈ";
					dt.Columns["DT"].ColumnName = "ĐIỆN THOẠI";
					dt.Columns["TINHTRANG"].ColumnName = "TÌNH TRẠNG";
					dt.Columns["DIEMTH"].ColumnName = "ĐIỂM TH";
					dt.Columns["DIEMQT"].ColumnName = "ĐIỂM QT";
					dt.Columns["DIEMCK"].ColumnName = "ĐIỂM CK";
					dt.Columns["DIEMTK"].ColumnName = "ĐIỂM TK";

					dgvStudents.DataSource = dt;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi load danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else if (PersonnelMenu._role == "NV PĐT")
			{
				try
				{
					string userQuery = @"
					SELECT MASV, HOTEN, TINHTRANG
					FROM PDB_ADMIN.QLDH_SINHVIEN
					ORDER BY HOTEN";

					using (OracleCommand cmd = new OracleCommand(userQuery, DatabaseSession.Connection))
					using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						adapter.Fill(dt);

						// Cấu hình lại DataGridView
						dgvStudents.DataSource = null;
						dgvStudents.Columns.Clear();
						dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
						dgvStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
						dgvStudents.RowHeadersVisible = false;
						dgvStudents.AllowUserToAddRows = false;
						dgvStudents.MultiSelect = false;
						dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

						// Thêm cột checkbox
						DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
						{
							Name = "chk",
							HeaderText = "",
							Width = 40,
							ReadOnly = false
						};
						dgvStudents.Columns.Add(chk);

						// Thêm các cột hiển thị
						dgvStudents.Columns.Add("Ten", "HỌ TÊN");
						dgvStudents.Columns.Add("MaSV", "MÃ SV");
						dgvStudents.Columns.Add("Status", "TÌNH TRẠNG");

						// Đưa dữ liệu từ dt lên dgvStudents
						foreach (DataRow row in dt.Rows)
						{
							int index = dgvStudents.Rows.Add();
							dgvStudents.Rows[index].Cells["Ten"].Value = row["HOTEN"];
							dgvStudents.Rows[index].Cells["MaSV"].Value = row["MASV"];
							dgvStudents.Rows[index].Cells["Status"].Value = row["TINHTRANG"];
							dgvStudents.Rows[index].Cells["chk"].Value = false;
						}

						// Gán lại sự kiện
						dgvStudents.CellClick -= dgvStudent_CellClick;
						dgvStudents.CellClick += dgvStudent_CellClick;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi tải danh sách SINH VIÊN Oracle:\n" + ex.Message);
				}
			}
			else if (PersonnelMenu._role == "NV CTSV")
			{
				try
				{
					string userQuery = @"
					SELECT MASV, HOTEN
					FROM PDB_ADMIN.QLDH_SINHVIEN
					ORDER BY HOTEN";

					using (OracleCommand cmd = new OracleCommand(userQuery, DatabaseSession.Connection))
					using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						adapter.Fill(dt);

						// Cấu hình lại DataGridView
						dgvStudents.DataSource = null;
						dgvStudents.Columns.Clear();
						dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
						dgvStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
						dgvStudents.RowHeadersVisible = false;
						dgvStudents.AllowUserToAddRows = false;
						dgvStudents.MultiSelect = false;
						dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

						// Thêm cột checkbox
						DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
						{
							Name = "chk",
							HeaderText = "",
							Width = 40,
							ReadOnly = false
						};
						dgvStudents.Columns.Add(chk);

						// Thêm các cột hiển thị
						dgvStudents.Columns.Add("Ten", "HỌ TÊN");
						dgvStudents.Columns.Add("MaSV", "MÃ SV");

						// Đưa dữ liệu từ dt lên dgvStudents
						foreach (DataRow row in dt.Rows)
						{
							int index = dgvStudents.Rows.Add();
							dgvStudents.Rows[index].Cells["Ten"].Value = row["HOTEN"];
							dgvStudents.Rows[index].Cells["MaSV"].Value = row["MASV"];
							dgvStudents.Rows[index].Cells["chk"].Value = false;
						}

						// Gán lại sự kiện
						dgvStudents.CellClick -= dgvStudent_CellClick;
						dgvStudents.CellClick += dgvStudent_CellClick;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi tải danh sách SINH VIÊN Oracle:\n" + ex.Message);
				}
			}
		}

		private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Chỉ xử lý click vào cột checkbox

			// Toggle trạng thái checkbox
			bool currentValue = Convert.ToBoolean(dgvStudents.Rows[e.RowIndex].Cells["chk"].Value ?? false);
			dgvStudents.Rows[e.RowIndex].Cells["chk"].Value = !currentValue;

			// Bỏ chọn các dòng khác
			foreach (DataGridViewRow row in dgvStudents.Rows)
			{
				if (row.Index != e.RowIndex)
				{
					row.Cells["chk"].Value = false;
				}
			}
		}

		private void pbEdit_Click(object sender, EventArgs e)
		{
			if (PersonnelMenu._role == "GV")
			{
				// Giáo viên không có quyền chỉnh sửa trạng thái sinh viên
				KryptonMessageBox.Show("Bạn không có quyền thực hiện chức năng này!",
									   "Cảnh báo",
									   MessageBoxButtons.OK,
									   MessageBoxIcon.Warning);
				return;
			}

			else if (PersonnelMenu._role == "NV PĐT")
			{
				// Kiểm tra DataGridView tồn tại và không rỗng
				if (dgvStudents == null || dgvStudents.Rows.Count == 0)
				{
					KryptonMessageBox.Show("Không có dữ liệu để chỉnh sửa!",
										   "Thông báo",
										   MessageBoxButtons.OK,
										   MessageBoxIcon.Information);
					return;
				}

				// Tìm dòng được chọn
				var selectedRow = dgvStudents.Rows
					.Cast<DataGridViewRow>()
					.FirstOrDefault(row =>
						row.Cells["chk"].Value != null &&
						Convert.ToBoolean(row.Cells["chk"].Value));

				if (selectedRow != null)
				{
					UpdateStudent updateForm = new UpdateStudent(
						selectedRow.Cells["MaSV"].Value.ToString(),
						selectedRow.Cells["Status"].Value.ToString()
					);

					this.Hide();
					updateForm.ShowDialog();
					this.Close();
				}
				else
				{
					KryptonMessageBox.Show("Vui lòng chọn một sinh viên để chỉnh sửa.",
										   "Thông báo",
										   MessageBoxButtons.OK,
										   MessageBoxIcon.Warning);
				}
			}
			else if (PersonnelMenu._role == "NV CTSV")
			{
				// Kiểm tra DataGridView tồn tại và không rỗng
				if (dgvStudents == null || dgvStudents.Rows.Count == 0)
				{
					KryptonMessageBox.Show("Không có dữ liệu để chỉnh sửa!",
										   "Thông báo",
										   MessageBoxButtons.OK,
										   MessageBoxIcon.Information);
					return;
				}

				// Tìm dòng được chọn
				var selectedRow = dgvStudents.Rows
					.Cast<DataGridViewRow>()
					.FirstOrDefault(row =>
						row.Cells["chk"].Value != null &&
						Convert.ToBoolean(row.Cells["chk"].Value));

				if (selectedRow != null)
				{
					UpdateStudentCTSV updateForm = new UpdateStudentCTSV(
						selectedRow.Cells["MaSV"].Value.ToString()
					);

					this.Hide();
					updateForm.ShowDialog();
					this.Close();
				}
				else
				{
					KryptonMessageBox.Show("Vui lòng chọn một sinh viên để chỉnh sửa.",
										   "Thông báo",
										   MessageBoxButtons.OK,
										   MessageBoxIcon.Warning);
				}
			}
		}

		

		// Login

		private void label4_Click(object sender, EventArgs e)
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}

		// Schedule
		private void label2_Click(object sender, EventArgs e)
		{
			ViewSchedule viewSchedule = new ViewSchedule(Login.ID, PersonnelMenu._role);
			this.Hide();
			viewSchedule.ShowDialog();
			this.Close();
		}

		private void lbProfile_Click(object sender, EventArgs e)
		{
			PersonnelMenu personnelMenu = new PersonnelMenu(Login.ID);
			this.Hide();
			personnelMenu.ShowDialog();
			this.Close();
		}

		// Register Management
		private void label5_Click(object sender, EventArgs e)
		{
			PersonnelRegister personnelRegister = new PersonnelRegister();
			this.Hide();
			personnelRegister.ShowDialog();
			this.Close();
		}

		// Personnel Manager
		private void lbTeachers_Click(object sender, EventArgs e)
		{
			PersonnelManager personnelManager = new PersonnelManager();
			this.Hide();
			personnelManager.ShowDialog();
			this.Close();
		}


		// Student Manager
		private void lbStudents_Click(object sender, EventArgs e)
		{
			StudentManager studentManager= new StudentManager();
			this.Hide();
			studentManager.ShowDialog();
			this.Close();
		}

		// CLass list
		private void label6_Click(object sender, EventArgs e)
		{
			ClassList classList = new ClassList();
			this.Hide();
			classList.ShowDialog();
			this.Close();
		}

		private void StudentManager_Load(object sender, EventArgs e)
		{

		}

		private void lbClasses_Click(object sender, EventArgs e)
		{
			SubjectManagement subjectManager = new SubjectManagement();
			this.Hide();
			subjectManager.ShowDialog();
			this.Close();
		}

        private void notifications_Click(object sender, EventArgs e)
        {
            ViewNotice viewNotification = new ViewNotice(Login.ID);
            this.Hide();
            viewNotification.ShowDialog();
            this.Close();
        }
    }
}
