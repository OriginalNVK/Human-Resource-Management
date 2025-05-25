using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SchoolManagement
{
	public partial class PersonnelManager : KryptonForm
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

		public PersonnelManager()
		{
			InitializeComponent();
			LoadPersonnels();
		}

		private void LoadPersonnels()
		{
			if (PersonnelMenu._role != "TRGĐV")
			{
				MessageBox.Show("Bạn không có quyền xem, vui lòng thử lại sau!", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Failed to connect with database!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Lấy MADV của trưởng đơn vị hiện tại
				string getMaDvQuery = "SELECT MADV FROM PDB_ADMIN.QLDH_NHANVIEN WHERE MANV = SYS_CONTEXT('USERENV','SESSION_USER')";
				OracleCommand getMaDvCmd = new OracleCommand(getMaDvQuery, conn);
				string madv = getMaDvCmd.ExecuteScalar()?.ToString();

				if (string.IsNullOrEmpty(madv))
				{
					MessageBox.Show("Không thể lấy thông tin đơn vị của người dùng!", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Truy vấn danh sách nhân viên thuộc cùng MADV
				string sql = @"
				SELECT MANV, HOTEN, PHAI, NGSINH, DCHI, DT, VAITRO 
				FROM PDB_ADMIN.QLDH_NHANVIEN 
				WHERE MADV = :madv";

				OracleCommand cmd = new OracleCommand(sql, conn);
				cmd.Parameters.Add(":madv", OracleDbType.Varchar2).Value = madv;

				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				// Đặt tiêu đề cột
				dt.Columns["MANV"].ColumnName = "MÃ NHÂN VIÊN";
				dt.Columns["HOTEN"].ColumnName = "HỌ TÊN";
				dt.Columns["PHAI"].ColumnName = "PHÁI";
				dt.Columns["NGSINH"].ColumnName = "NGÀY SINH";
				dt.Columns["DCHI"].ColumnName = "ĐỊA CHỈ";
				dt.Columns["DT"].ColumnName = "ĐIỆN THOẠI";
				dt.Columns["VAITRO"].ColumnName = "VAI TRÒ";

				dgvPersonnels.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi load danh sách nhân viên: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

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

		private void btnSearch_Click(object sender, EventArgs e)
		{
			string name = txtSearch.Text.Trim().ToLower(); // Giả sử txtSearch là TextBox

			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Failed to connect with database!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Lấy MADV của trưởng đơn vị hiện tại
				string getMaDvQuery = "SELECT MADV FROM PDB_ADMIN.QLDH_NHANVIEN WHERE MANV = SYS_CONTEXT('USERENV','SESSION_USER')";
				OracleCommand getMaDvCmd = new OracleCommand(getMaDvQuery, conn);
				string madv = getMaDvCmd.ExecuteScalar()?.ToString();

				if (string.IsNullOrEmpty(madv))
				{
					MessageBox.Show("Không thể lấy thông tin đơn vị của người dùng!", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Truy vấn danh sách nhân viên theo MADV và tên (nếu có nhập)
				string sql = @"
				SELECT MANV, HOTEN, PHAI, NGSINH, DCHI, DT, VAITRO 
				FROM PDB_ADMIN.QLDH_NHANVIEN 
				WHERE MADV = :madv";

				if (!string.IsNullOrWhiteSpace(name))
				{
					sql += " AND LOWER(HOTEN) LIKE :name";
				}

				OracleCommand cmd = new OracleCommand(sql, conn);
				cmd.Parameters.Add(":madv", OracleDbType.Varchar2).Value = madv;

				if (!string.IsNullOrWhiteSpace(name))
				{
					cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = "%" + name.ToLower() + "%";
				}

				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				// Đặt tiêu đề cột
				dt.Columns["MANV"].ColumnName = "MÃ NHÂN VIÊN";
				dt.Columns["HOTEN"].ColumnName = "HỌ TÊN";
				dt.Columns["PHAI"].ColumnName = "PHÁI";
				dt.Columns["NGSINH"].ColumnName = "NGÀY SINH";
				dt.Columns["DCHI"].ColumnName = "ĐỊA CHỈ";
				dt.Columns["DT"].ColumnName = "ĐIỆN THOẠI";
				dt.Columns["VAITRO"].ColumnName = "VAI TRÒ";

				dgvPersonnels.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi tìm kiếm nhân viên: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void pbReload_Click(object sender, EventArgs e)
		{
			LoadPersonnels();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			if (dgvPersonnels.DataSource == null || dgvPersonnels.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			try
			{
				// Chọn nơi lưu file
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "PDF files (*.pdf)|*.pdf";
				sfd.FileName = "DanhSachNhanVien.pdf";

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
					PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
					doc.Open();

					// Tiêu đề
					string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
					BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
					iTextSharp.text.Font unicodeFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);

					Paragraph title = new Paragraph("DANH SÁCH NHÂN VIÊN", unicodeFont);
					title.Alignment = Element.ALIGN_CENTER;
					title.SpacingAfter = 20f;
					doc.Add(title);

					// Tạo bảng với số cột = số cột của DataGridView
					PdfPTable table = new PdfPTable(dgvPersonnels.Columns.Count);
					table.WidthPercentage = 100;

					// Thêm header
					foreach (DataGridViewColumn column in dgvPersonnels.Columns)
					{
						PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
						cell.BackgroundColor = BaseColor.LIGHT_GRAY;
						cell.HorizontalAlignment = Element.ALIGN_CENTER;
						table.AddCell(cell);
					}

					// Thêm dữ liệu từng dòng
					foreach (DataGridViewRow row in dgvPersonnels.Rows)
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
	}
}
