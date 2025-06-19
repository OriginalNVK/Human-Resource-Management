using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Oracle.ManagedDataAccess.Client;

namespace SchoolManagement
{
	public partial class ClassList : KryptonForm
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

		public ClassList()
		{
			InitializeComponent();
			lbHello.Text = $"Hello, {Login.ID}";
			string loginId = Login.ID;
			LoadClasses();
		}

		private void LoadClasses()
		{
			if (Login.TYPE_ROLE != "GV")
			{
				MessageBox.Show("Không có quyền truy cập thông tin này", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Lấy MAGV hiện tại từ đăng nhập
				string magv = Login.ID; // Đây là MANV của giáo viên

				// Truy vấn từ VIEW V_STUDENTS_BY_GV
				string sql = @"
				SELECT MAMH, TENHP, SOTC, HK, NAM, NGAYBATDAU, NGAYKETTHUC
				FROM PDB_ADMIN.V_MONHOC_BY_GV
				WHERE MAGV = :magv
				";

				OracleCommand cmd = new OracleCommand(sql, conn);
				cmd.Parameters.Add(":magv", OracleDbType.Varchar2).Value = Login.ID; // Lọc theo giáo viên đang đăng nhập

				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				// Đổi tên cột hiển thị đúng với VIEW
				dt.Columns["MAMH"].ColumnName = "MÃ MÔN HỌC";
				dt.Columns["TENHP"].ColumnName = "TÊN HỌC PHẦN";
				dt.Columns["SOTC"].ColumnName = "SỐ TC";
				dt.Columns["HK"].ColumnName = "HỌC KỲ";
				dt.Columns["NAM"].ColumnName = "NĂM";
				dt.Columns["NGAYBATDAU"].ColumnName = "NGÀY BẮT ĐẦU";
				dt.Columns["NGAYKETTHUC"].ColumnName = "NGÀY KẾT THÚC";

				// Gán vào DataGridView
				dgvClasses.DataSource = dt;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi load danh sách lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			string name = txtSearch.Text.Trim().ToLower(); // Giả sử txtSearch là TextBox
			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Lấy MAGV hiện tại từ đăng nhập
				string magv = Login.ID; // Đây là MANV của giáo viên

				// Truy vấn từ VIEW V_STUDENTS_BY_GV
				string sql = @"
				SELECT MAMH, TENHP, SOTC, HK, NAM, NGAYBATDAU, NGAYKETTHUC
				FROM PDB_ADMIN.V_MONHOC_BY_GV
				WHERE MAGV = :magv
				";

				if (!string.IsNullOrWhiteSpace(name))
				{
					sql += " AND LOWER(TENHP) LIKE :name";
				}

				OracleCommand cmd = new OracleCommand(sql, conn);
				cmd.Parameters.Add(":magv", OracleDbType.Varchar2).Value = Login.ID; // Lọc theo giáo viên đang đăng nhập
				if (!string.IsNullOrWhiteSpace(name))
				{
					cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = "%" + name.ToLower() + "%";
				}

				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				// Đổi tên cột hiển thị đúng với VIEW
				dt.Columns["MAMH"].ColumnName = "MÃ MÔN HỌC";
				dt.Columns["TENHP"].ColumnName = "TÊN HỌC PHẦN";
				dt.Columns["SOTC"].ColumnName = "SỐ TC";
				dt.Columns["HK"].ColumnName = "HỌC KỲ";
				dt.Columns["NAM"].ColumnName = "NĂM";
				dt.Columns["NGAYBATDAU"].ColumnName = "NGÀY BẮT ĐẦU";
				dt.Columns["NGAYKETTHUC"].ColumnName = "NGÀY KẾT THÚC";

				// Gán vào DataGridView
				dgvClasses.DataSource = dt;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi load danh sách lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void pbReload_Click(object sender, EventArgs e)
		{
			LoadClasses();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			if (dgvClasses.DataSource == null || dgvClasses.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			try
			{
				// Chọn nơi lưu file
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "PDF files (*.pdf)|*.pdf";
				sfd.FileName = "DanhSachLopHoc.pdf";

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
					PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
					doc.Open();

					// Tiêu đề
					string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
					BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
					iTextSharp.text.Font unicodeFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);

					Paragraph title = new Paragraph("DANH SÁCH LỚP", unicodeFont);
					title.Alignment = Element.ALIGN_CENTER;
					title.SpacingAfter = 20f;
					doc.Add(title);

					// Tạo bảng với số cột = số cột của DataGridView
					PdfPTable table = new PdfPTable(dgvClasses.Columns.Count);
					table.WidthPercentage = 100;

					// Thêm header
					foreach (DataGridViewColumn column in dgvClasses.Columns)
					{
						PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
						cell.BackgroundColor = BaseColor.LIGHT_GRAY;
						cell.HorizontalAlignment = Element.ALIGN_CENTER;
						table.AddCell(cell);
					}

					// Thêm dữ liệu từng dòng
					foreach (DataGridViewRow row in dgvClasses.Rows)
					{
						if (row.IsNewRow) continue;
						foreach (DataGridViewCell cell in row.Cells)
						{
							table.AddCell(cell.Value?.ToString());
						}
					}

					doc.Add(table);
					doc.Close();
					writer.Close();

					MessageBox.Show("Xuất PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void lbClasses_Click(object sender, EventArgs e)
		{
			SubjectManagement subjectManager = new SubjectManagement();
			this.Hide();
			subjectManager.ShowDialog();
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

		private void label2_Click(object sender, EventArgs e)
		{
			ViewSchedule viewDetail = new ViewSchedule(PersonnelMenu._username, PersonnelMenu._role);
			this.Hide();
			viewDetail.ShowDialog();
			this.Close();
		}

		private void label5_Click(object sender, EventArgs e)
		{
			PersonnelRegister personnelRegister = new PersonnelRegister();
			this.Hide();
			personnelRegister.ShowDialog();
			this.Close();
		}

		private void label4_Click(object sender, EventArgs e)
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}

		private void label8_Click(object sender, EventArgs e)
		{
			GradeManager gradeManager = new GradeManager();
			this.Hide();
			gradeManager.ShowDialog();
			this.Close();
		}
	}
}
