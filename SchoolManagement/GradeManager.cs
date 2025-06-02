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
using Oracle.ManagedDataAccess.Client;

namespace SchoolManagement
{
	public partial class GradeManager : KryptonForm
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

		public GradeManager()
		{
			// display the role of the user
			MessageBox.Show($"Chào mừng {PersonnelMenu._role} đến với hệ thống quản lý điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
			InitializeComponent();
			lbHello.Text = $"Hello, {Login.ID}";
			string loginId = Login.ID;
			LoadSubjects();
		}

		private void LoadSubjects()
		{
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

					string magv = Login.ID;

					string sql = @"
						SELECT MAHP, TENHP, HK, NAM 
						FROM PDB_ADMIN.QLDH_VIEW_SUBJECTS_BY_GV
						ORDER BY NAM DESC, HK DESC
					";

					OracleCommand cmd = new OracleCommand(sql, conn);

					OracleDataAdapter adapter = new OracleDataAdapter(cmd);
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					DataRow allSubjectsRow = dt.NewRow();
					allSubjectsRow["MAHP"] = "NONE";
					allSubjectsRow["TENHP"] = "Chọn môn học";
					dt.Rows.InsertAt(allSubjectsRow, 0);

					comboBox1.DataSource = dt;
					comboBox1.DisplayMember = "TENHP";
					comboBox1.ValueMember = "MAHP";
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi load danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else if (PersonnelMenu._role == "NV PKT")
			{
				try
				{
					OracleConnection conn = DatabaseSession.Connection;
					if (conn == null || conn.State != ConnectionState.Open)
					{
						MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					string magv = Login.ID;

					string sql = @"
						SELECT * FROM PDB_ADMIN.QLDH_VIEW_ALL_SUBJECTS
					";

					OracleCommand cmd = new OracleCommand(sql, conn);

					OracleDataAdapter adapter = new OracleDataAdapter(cmd);
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					DataRow allSubjectsRow = dt.NewRow();
					allSubjectsRow["MAHP"] = "NONE";
					allSubjectsRow["TENHP"] = "Chọn môn học";
					dt.Rows.InsertAt(allSubjectsRow, 0);

					comboBox1.DataSource = dt;
					comboBox1.DisplayMember = "TENHP";
					comboBox1.ValueMember = "MAHP";
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi load danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

		}

		//viết hàm xử lý sự kiện khi người dùng chọn môn học rồi nhấn nút tìm kiếm 
		private void btnSearch_Click(object sender, EventArgs e)
		{
			if (comboBox1.SelectedValue != null && comboBox1.SelectedValue.ToString() != "NONE")
			{
				// Kiểm tra xem người dùng đã chọn môn học hay chưa
				if (comboBox1.SelectedIndex == 0)
				{
					MessageBox.Show("Vui lòng chọn một môn học để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				// Lấy giá trị môn học đã chọn
				string selectedSubject = comboBox1.SelectedValue.ToString();
				// Thực hiện tìm kiếm theo môn học đã chọn
				LoadScoreBoard(selectedSubject);
			}
			else
			{
				MessageBox.Show("Vui lòng chọn một môn học để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void LoadScoreBoard(string subjectId)
		{
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
							SELECT 
								MASV as ""MÃ SV"",
								HOTEN as ""HỌ TÊN"",
								DIEMTH as ""ĐIỂM TH"",
								DIEMQT as ""ĐIỂM QT"",
								DIEMCK as ""ĐIỂM CK"",
								DIEMTK as ""ĐIỂM TK""
							FROM PDB_ADMIN.QLDH_VIEW_SCOREBOARD
							WHERE MAHP = :subjectId
							ORDER BY MASV
						";

					OracleCommand cmd = new OracleCommand(sql, conn);
					cmd.Parameters.Add(new OracleParameter("subjectId", subjectId));

					OracleDataAdapter adapter = new OracleDataAdapter(cmd);
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					dgvScoreBoard.DataSource = dt;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi load bảng điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else if (PersonnelMenu._role == "NV PKT")
			{
				try
				{
					OracleConnection conn = DatabaseSession.Connection;
					if (conn == null || conn.State != ConnectionState.Open)
					{
						MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					//messagebox the subjectId
					MessageBox.Show("Đang tải bảng điểm cho môn học: " + subjectId, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
					string sql = @"
							SELECT 
								MASV,
								HOTEN,
								DIEMTH,
								DIEMQT,
								DIEMCK,
								DIEMTK,
								MAHP
							FROM PDB_ADMIN.QLDH_VIEW_SCOREBOARD1
							WHERE MAHP = :subjectId
							ORDER BY MASV
						";

					OracleCommand cmd = new OracleCommand(sql, conn);
					cmd.Parameters.Add(new OracleParameter("subjectId", subjectId));

					OracleDataAdapter adapter = new OracleDataAdapter(cmd);
					{
						DataTable dt = new DataTable();
						adapter.Fill(dt);

						// Cấu hình lại DataGridView
						dgvScoreBoard.DataSource = null;
						dgvScoreBoard.Columns.Clear();
						dgvScoreBoard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
						dgvScoreBoard.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
						dgvScoreBoard.RowHeadersVisible = false;
						dgvScoreBoard.AllowUserToAddRows = false;
						dgvScoreBoard.MultiSelect = false;
						dgvScoreBoard.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

						// Thêm cột checkbox
						DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
						{
							Name = "chk",
							HeaderText = "",
							Width = 40,
							ReadOnly = false
						};
						dgvScoreBoard.Columns.Add(chk);

						// Thêm các cột hiển thị
						dgvScoreBoard.Columns.Add("Ten", "HOTEN");
						dgvScoreBoard.Columns.Add("MÃ SV", "MASV");
						dgvScoreBoard.Columns["MÃ SV"].Visible = false; // Ẩn cột MÃ SV
						dgvScoreBoard.Columns.Add("DiemTH", "DIEMTH");
						dgvScoreBoard.Columns.Add("DiemQT", "DIEMQT");
						dgvScoreBoard.Columns.Add("DiemCK", "DIEMCK");
						dgvScoreBoard.Columns.Add("DiemTK", "DIEMTK");
						dgvScoreBoard.Columns.Add("MaHP", "MAHP");
						dgvScoreBoard.Columns["MaHP"].Visible = false; // Ẩn cột MaHP

						// Đưa dữ liệu từ dt lên dgvScoreBoard
						foreach (DataRow row in dt.Rows)
						{
							int index = dgvScoreBoard.Rows.Add();
							dgvScoreBoard.Rows[index].Cells["Ten"].Value = row["HOTEN"];
							dgvScoreBoard.Rows[index].Cells["MÃ SV"].Value = row["MASV"];
							dgvScoreBoard.Rows[index].Cells["DiemTH"].Value = row["DIEMTH"];
							dgvScoreBoard.Rows[index].Cells["DiemQT"].Value = row["DIEMQT"];
							dgvScoreBoard.Rows[index].Cells["DiemCK"].Value = row["DIEMCK"];
							dgvScoreBoard.Rows[index].Cells["DiemTK"].Value = row["DIEMTK"];
							dgvScoreBoard.Rows[index].Cells["MaHP"].Value = row["MAHP"];
							dgvScoreBoard.Rows[index].Cells["chk"].Value = false;
						}

						// Gán lại sự kiện
						dgvScoreBoard.CellClick -= dgvScoreBoard_CellClick;
						dgvScoreBoard.CellClick += dgvScoreBoard_CellClick;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi load bảng điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void dgvScoreBoard_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Chỉ xử lý click vào cột checkbox

			// Toggle trạng thái checkbox
			bool currentValue = Convert.ToBoolean(dgvScoreBoard.Rows[e.RowIndex].Cells["chk"].Value ?? false);
			dgvScoreBoard.Rows[e.RowIndex].Cells["chk"].Value = !currentValue;

			// Bỏ chọn các dòng khác
			foreach (DataGridViewRow row in dgvScoreBoard.Rows)
			{
				if (row.Index != e.RowIndex)
				{
					row.Cells["chk"].Value = false;
				}
			}
		}

		private void label7_Click(object sender, EventArgs e)
		{

		}

		private void btnExport_Click(object sender, EventArgs e)
		{

		}
		private void pbEdit_Click(object sender, EventArgs e)
		{
			if (PersonnelMenu._role == "NV PKT")
			{
				if (dgvScoreBoard == null || dgvScoreBoard.Rows.Count == 0)
				{
					KryptonMessageBox.Show("Không có dữ liệu để chỉnh sửa!",
										   "Thông báo",
										   MessageBoxButtons.OK,
										   MessageBoxIcon.Information);
					return;
				}

				// Tìm dòng được chọn
				var selectedRow = dgvScoreBoard.Rows
					.Cast<DataGridViewRow>()
					.FirstOrDefault(row =>
						row.Cells["chk"].Value != null &&
						Convert.ToBoolean(row.Cells["chk"].Value));

				if (selectedRow != null)
				{
					using (UpdateGrade updateForm = new UpdateGrade(
						subjectId: selectedRow.Cells["MaHP"].Value.ToString(),
						username: selectedRow.Cells["MÃ SV"].Value.ToString()))
					{
						if (updateForm.ShowDialog() == DialogResult.OK)
						{
							// Refresh data after successful update
							LoadScoreBoard(selectedRow.Cells["MaHP"].Value.ToString());
						}
					}
				}
			}
		}
	}
	
}
