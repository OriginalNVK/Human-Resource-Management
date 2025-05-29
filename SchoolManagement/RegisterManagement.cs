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
    public partial class RegisterManagement : KryptonForm
    {
        public RegisterManagement()
        {
            InitializeComponent();
            LoadSubject();
        }

        private void LoadSubject()
        {
            try
            {
                string query = @"
                SELECT MAMH, TENHP
                FROM PDB_ADMIN.QLDH_MONHOC JOIN PDB_ADMIN.QLDH_HOCPHAN ON PDB_ADMIN.QLDH_MONHOC.MAHP = PDB_ADMIN.QLDH_HOCPHAN.MAHP
                WHERE SYSDATE BETWEEN NGAYBATDAU AND NGAYBATDAU + 15 
                ";
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSubject.DataSource = dt;
                    // Định dạng cột
                    dgvSubject.Columns["MAMH"].HeaderText = "Mã môn học";
                    dgvSubject.Columns["TENHP"].HeaderText = "Tên môn học";
                    // Tự động điều chỉnh kích thước cột
                    dgvSubject.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    // Căn giữa
                    dgvSubject.Columns["MAMH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvSubject.Columns["TENHP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    // Các tuỳ chọn hiển thị
                    dgvSubject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvSubject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvSubject.AllowUserToAddRows = false;
                    dgvSubject.AllowUserToDeleteRows = false;
                    dgvSubject.ReadOnly = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RegisterManagement_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string searchText = txtSearch.Text.Trim();
                string query = @"
                SELECT MAMH, TENHP
                FROM PDB_ADMIN.QLDH_MONHOC JOIN PDB_ADMIN.QLDH_HOCPHAN ON PDB_ADMIN.QLDH_MONHOC.MAHP = PDB_ADMIN.QLDH_HOCPHAN.MAHP
                WHERE (TENHP LIKE '%' || :searchText || '%' OR MAMH LIKE '%' || :searchText || '%')
                AND SYSDATE BETWEEN NGAYBATDAU AND NGAYBATDAU + 15
                ";
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("searchText", searchText));
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                        dgvSubject.DataSource = dt;
                        // Định dạng cột
                        dgvSubject.Columns["MAMH"].HeaderText = "Mã môn học";
                        dgvSubject.Columns["TENHP"].HeaderText = "Tên môn học";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSubject.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một môn học để đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string masv = txtMASV.Text.Trim();
                string mamh = dgvSubject.SelectedRows[0].Cells["MAMH"].Value.ToString();
                string query = @"
                INSERT INTO PDB_ADMIN.QLDH_DANGKY (MAMH, MASV, NGAYDK)
                VALUES (:mamh, :masv, SYSDATE)";

                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("mamh", mamh));
                    cmd.Parameters.Add(new OracleParameter("masv", masv));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbPrev_Click(object sender, EventArgs e)
        {
            PersonnelRegister personnelRegister = new PersonnelRegister();
            this.Hide();
            personnelRegister.ShowDialog();
            this.Close();
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

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
            AdminProfile myProfile = new AdminProfile();
            this.Hide();
            myProfile.ShowDialog();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            PersonnelRegister personnelRegister = new PersonnelRegister();
            this.Hide();
            personnelRegister.ShowDialog();
            this.Close();
        }
    }
}
