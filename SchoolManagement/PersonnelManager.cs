using System;
using System.Data;
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
            if (PersonnelMenu._role != "TRGĐV" && PersonnelMenu._role != "NV TCHC")
            {
                MessageBox.Show("Bạn không có quyền xem, vui lòng thử lại sau!", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (PersonnelMenu._role == "TRGĐV")
            {
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
            else
            {
                try
                {
                    OracleConnection conn = DatabaseSession.Connection;
                    if (conn == null || conn.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Failed to connect with database!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // Fix for CS1002, CS0246, CS0168, CS0201 errors in the problematic SQL query
                    // Original problematic code:
                    // WHERE VAITRO != "NV TCTH"";

                    string sql = @"
                        SELECT MANV, HOTEN, PHAI, NGSINH, LUONG, PHUCAP, DT, VAITRO, MADV 
                        FROM PDB_ADMIN.QLDH_NHANVIEN
                        WHERE VAITRO != 'NV TCHC'";

                    OracleCommand cmd = new OracleCommand(sql, conn);


                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Đặt tiêu đề cột
                    dt.Columns["MANV"].ColumnName = "MÃ NHÂN VIÊN";
                    dt.Columns["HOTEN"].ColumnName = "HỌ TÊN";
                    dt.Columns["PHAI"].ColumnName = "PHÁI";
                    dt.Columns["NGSINH"].ColumnName = "NGÀY SINH";
                    dt.Columns["LUONG"].ColumnName = "LƯƠNG";
                    dt.Columns["PHUCAP"].ColumnName = "PHỤ CẤP";
                    dt.Columns["DT"].ColumnName = "ĐIỆN THOẠI";
                    dt.Columns["VAITRO"].ColumnName = "VAI TRÒ";
                    dgvPersonnels.DataSource = dt;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi load danh sách nhân viên: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (PersonnelMenu._role != "NV TCHC")
            {
                MessageBox.Show("Bạn không có quyền xóa nhân viên!", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvPersonnels.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string selectedEmployeeId = dgvPersonnels.SelectedRows[0].Cells["MÃ NHÂN VIÊN"].Value.ToString();
                string selectedEmployeeVaiTro = dgvPersonnels.SelectedRows[0].Cells["VAI TRÒ"].Value.ToString();
                if (selectedEmployeeVaiTro == "TRGĐV")
                {
                    string sql = @"
					UPDATE PDB_ADMIN.QLDH_DONVI
					SET TRUONGDV = NULL 
					WHERE TRUONGDV = :manv";
                    using (OracleCommand cmd = new OracleCommand(sql, DatabaseSession.Connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("manv", selectedEmployeeId));
                        cmd.ExecuteNonQuery();

                    }
                }
                else if (selectedEmployeeVaiTro == "GV")
                {
                    string sql = @"
					UPDATE PDB_ADMIN.QLDH_MONHOC
					SET MAGV = NULL 
					WHERE MAGV = :manv";
                    using (OracleCommand cmd = new OracleCommand(sql, DatabaseSession.Connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("manv", selectedEmployeeId));
                        cmd.ExecuteNonQuery();
                    }
                }

                string query = @"
				DELETE FROM PDB_ADMIN.QLDH_NHANVIEN 
				WHERE MANV = :manv";
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("manv", selectedEmployeeId));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPersonnels(); // Tải lại danh sách nhân viên
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbAddRoles_Click(object sender, EventArgs e)
        {
            if (PersonnelMenu._role != "NV TCHC")
            {
                MessageBox.Show("Bạn không có quyền thêm nhân viên!", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AddEmployee addEmployeeForm = new AddEmployee();
            this.Hide();
            addEmployeeForm.ShowDialog();
            this.Close();
        }

        private void pbEdit_Click(object sender, EventArgs e)
        {
            if (PersonnelMenu._role != "NV TCHC")
            {
                MessageBox.Show("Bạn không có quyền sửa thông tin nhân viên!", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvPersonnels.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "CẢNH BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string selectedEmployeeId = dgvPersonnels.SelectedRows[0].Cells["MÃ NHÂN VIÊN"].Value.ToString();
            EditEmployee editEmployeeForm = new EditEmployee(selectedEmployeeId);
            this.Hide();
            editEmployeeForm.ShowDialog();
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
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

        private void lbPersonnels_Click(object sender, EventArgs e)
        {
            PersonnelManager personnelManager = new PersonnelManager();
            this.Hide();
            personnelManager.ShowDialog();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            PersonnelRegister personnelRegister = new PersonnelRegister();
            this.Hide();
            personnelRegister.ShowDialog();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ViewSchedule viewDetail = new ViewSchedule( PersonnelMenu._username, PersonnelMenu._role);
            this.Hide();
            viewDetail.ShowDialog();
            this.Close();
        }

		private void pictureBox1_Click(object sender, EventArgs e)
		{
            ViewNotice viewNotification = new ViewNotice(Login.ID);
            this.Hide();
            viewNotification.ShowDialog();
            this.Close();
        }

		private void label4_Click_1(object sender, EventArgs e)
		{
			ClassList classList = new ClassList();
			this.Hide();
			classList.ShowDialog();
			this.Close();
		}

		private void lbProfile_Click_1(object sender, EventArgs e)
		{
			PersonnelMenu personnelMenu = new PersonnelMenu(Login.ID);
			this.Hide();
			personnelMenu.ShowDialog();
			this.Close();
		}
	}
}
