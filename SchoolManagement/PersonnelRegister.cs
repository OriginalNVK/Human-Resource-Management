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
    public partial class PersonnelRegister : KryptonForm
    {
        public PersonnelRegister()
        {
            InitializeComponent();

            LoadRegisterStudent();
        }
        private void LoadRegisterStudent()
        {
            if (PersonnelMenu._role != "NV PĐT" && PersonnelMenu._role != "NV PKT")
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string query = @"
            SELECT MAMH, MASV, NGAYDK
            FROM PDB_ADMIN.QLDH_DANGKY";

                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {


                    DataTable dt = new DataTable();
                    adapter.Fill(dt); // <- Không dùng ExecuteReader



                    dgvRegister.DataSource = dt;

                    // Định dạng cột
                    dgvRegister.Columns["MAMH"].HeaderText = "Mã học phần";
                    dgvRegister.Columns["MASV"].HeaderText = "Mã sinh viên";
                    dgvRegister.Columns["NGAYDK"].HeaderText = "Ngày đăng ký";
                    dgvRegister.Columns["NGAYDK"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    // Căn giữa
                    dgvRegister.Columns["NGAYDK"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvRegister.Columns["MAMH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvRegister.Columns["MASV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Các tuỳ chọn hiển thị
                    dgvRegister.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvRegister.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvRegister.AllowUserToAddRows = false;
                    dgvRegister.AllowUserToDeleteRows = false;
                    dgvRegister.ReadOnly = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }






        

      

        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (PersonnelMenu._role != "NV PĐT")
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvRegister.SelectedRows.Count > 0)
            {
                try
                {
                    string maMH = dgvRegister.SelectedRows[0].Cells["MAMH"].Value.ToString();
                    string maSV = dgvRegister.SelectedRows[0].Cells["MASV"].Value.ToString();
                    string query = @"
                    DELETE FROM PDB_ADMIN.QLDH_DANGKY
                    WHERE MAMH = :maMH AND MASV = :maSV";
                    using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("maMH", maMH));
                        cmd.Parameters.Add(new OracleParameter("maSV", maSV));
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Xóa đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRegisterStudent(); // Tải lại danh sách đăng ký
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (PersonnelMenu._role != "NV PDT" && PersonnelMenu._role != "NV PKT")
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string searchText = txtSearch.Text.Trim();
                string query;
                if (string.IsNullOrEmpty(searchText))
                {
                    query = @"
                SELECT MAMH, MASV, NGAYDK
                FROM PDB_ADMIN.QLDH_DANGKY";
                }
                else
                {
                    query = @"
                SELECT MAMH, MASV, NGAYDK
                FROM PDB_ADMIN.QLDH_DANGKY
                WHERE MASV LIKE :searchText OR MAMH LIKE :searchText";
                }

                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("searchText", "%" + searchText + "%"));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvRegister.DataSource = dt;
                        dgvRegister.Columns["MAMH"].HeaderText = "Mã học phần";
                        dgvRegister.Columns["MASV"].HeaderText = "Mã sinh viên";
                        dgvRegister.Columns["NGAYDK"].HeaderText = "Ngày đăng ký";
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRegister_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void lbStudents_Click(object sender, EventArgs e)
        {
            StudentManager studentManager = new StudentManager();
            this.Hide();
            studentManager.ShowDialog();
            this.Close();
        }

        private void lbProfile_Click(object sender, EventArgs e)
        {
            AdminProfile myProfile = new AdminProfile();
            this.Hide();
            myProfile.ShowDialog();
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lbClasses_Click(object sender, EventArgs e)
        {
            SubjectManagement subjectManager = new SubjectManagement();
            this.Hide();
            subjectManager.ShowDialog();
            this.Close();
        }

        private void pbAddRoles_Click(object sender, EventArgs e)
        {
            if (PersonnelMenu._role != "NV PĐT")
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RegisterManagement registerManagement = new RegisterManagement();
            this.Hide();
            registerManagement.ShowDialog();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            PersonnelRegister personnelRegister = new PersonnelRegister();
            this.Hide();
            personnelRegister.ShowDialog();
            this.Close();
        }

        private void lbTeachers_Click(object sender, EventArgs e)
        {
            PersonnelManager personnelManager = new PersonnelManager();
            this.Hide();
            personnelManager.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

       
    }
}
