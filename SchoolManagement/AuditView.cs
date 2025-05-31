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
    public partial class AuditView : KryptonForm
    {
        public AuditView()
        {
            InitializeComponent();
        }
        private void pbProfile_Click(object sender, EventArgs e)
        {
            AdminProfile myProfile = new AdminProfile();
            this.Hide();
            myProfile.ShowDialog();
            this.Close();
        }

        private void pbStudents_Click(object sender, EventArgs e)
        {
            StudentManager student = new StudentManager();
            this.Hide();
            student.ShowDialog();
            this.Close();
        }

        private void pbPersonnel_Click(object sender, EventArgs e)
        {
            PersonnelManager personnelManager = new PersonnelManager();
            this.Hide();
            personnelManager.ShowDialog();
            this.Close();
        }

        private void pbRole_Click(object sender, EventArgs e)
        {
            RoleManager roleManager = new RoleManager();
            this.Hide();
            roleManager.ShowDialog();
            this.Close();
        }

        private void pbUsers_Click(object sender, EventArgs e)
        {
            UsersManager userManager = new UsersManager();
            this.Hide();
            userManager.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string tableAudit = cmbTableAudit.SelectedItem.ToString();
                string typeAudit = cmbType.SelectedItem.ToString();


                string sql = @"
                SELECT DBUSERNAME, SQL_TEXT, OBJECT_NAME, ACTION_NAME, EVENT_TIMESTAMP
                FROM UNIFIED_AUDIT_TRAIL
                WHERE OBJECT_NAME = :tableAudit
                AND ACTION_NAME = :typeAudit
                ORDER BY EVENT_TIMESTAMP DESC
                ";

                using (OracleCommand cmd = new OracleCommand(sql, DatabaseSession.Connection))
                {
                    cmd.BindByName = true; // Quan trọng: bind tham số theo tên
                    cmd.Parameters.Add(new OracleParameter("tableAudit", tableAudit));
                    cmd.Parameters.Add(new OracleParameter("typeAudit", typeAudit));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvAudit.DataSource = dt;
                        dgvAudit.Columns["DBUSERNAME"].HeaderText = "Người dùng";
                        dgvAudit.Columns["SQL_TEXT"].HeaderText = "Lệnh SQL";
                        dgvAudit.Columns["OBJECT_NAME"].HeaderText = "Bảng";
                        dgvAudit.Columns["EVENT_TIMESTAMP"].HeaderText = "Thời gian";
                        dgvAudit.Columns["EVENT_TIMESTAMP"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                        dgvAudit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
               


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AuditView_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (lbEdit.Text.ToString() == "OFF") {
                lbEdit.Text = "ON";
                dgvAudit.ReadOnly = false;
                EnableAudit();
            }
            else
            {
                lbEdit.Text = "OFF";
                dgvAudit.ReadOnly = true;
                DisableAudit();
            }
        }
        private void EnableAudit()
        {
            try
            {
                string table = cmbTableAudit.SelectedItem.ToString();
                string name = "AUDIT_" + table.ToUpper();
                MessageBox.Show(name);
                string sql = @"
                BEGIN
                    DBMS_FGA.ENABLE_POLICY(object_schema => 'PDB_ADMIN', object_name => :table, policy_name => :name);
                END;";
                using (OracleCommand cmd = new OracleCommand(sql, DatabaseSession.Connection))
                {
                    cmd.BindByName = true; // Quan trọng: bind tham số theo tên
                    cmd.Parameters.Add(new OracleParameter("table", table));
                    cmd.Parameters.Add(new OracleParameter("name", name));

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Audit trail đã được kích hoạt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kích hoạt audit trail: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisableAudit()
        {
            try
            {
                string table = cmbTableAudit.SelectedItem.ToString();
                string name = "AUDIT_" + table.ToUpper();
                string sql = @"
                BEGIN
                    DBMS_FGA.DISABLE_POLICY(object_schema => 'PDB_ADMIN', object_name => :table, policy_name => :name);
                END;";
                using (OracleCommand cmd = new OracleCommand(sql, DatabaseSession.Connection))
                {
                    cmd.BindByName = true; // Quan trọng: bind tham số theo tên
                    cmd.Parameters.Add(new OracleParameter("table", table));
                    cmd.Parameters.Add(new OracleParameter("name", name));
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Audit trail đã được tắt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tắt audit trail: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvAudit.DataSource == null || dgvAudit.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                // Chọn nơi lưu file  
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = "AUDIT.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                    doc.Open();

                    // Tiêu đề  
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font unicodeFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);

                    Paragraph title = new Paragraph("AUDIT", unicodeFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 20f;
                    doc.Add(title);

                    // Tạo bảng với số cột = số cột của DataGridView  
                    PdfPTable table = new PdfPTable(dgvAudit.Columns.Count);
                    table.WidthPercentage = 100;

                    // Thêm header  
                    foreach (DataGridViewColumn column in dgvAudit.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }

                    // Thêm dữ liệu từng dòng  
                    foreach (DataGridViewRow row in dgvAudit.Rows)
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
