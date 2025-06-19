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
	public partial class SubjectManagement : KryptonForm
	{
		private string _username;
		private string _originalPhoneNumber;    // lưu số điện thoại ban đầu
		private string _changePassword = null;  // gán nếu người dùng muốn thay đổi mật khẩu

		public SubjectManagement()
		{
			InitializeComponent();
			LoadSubject();
		}

		private void LoadSubject()
		{
            if (PersonnelMenu._role != "NV PĐT")
            {
                MessageBox.Show("Không có quyền truy cập thông tin này", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //PersonnelMenu personnelMenu = new PersonnelMenu(Login.ID);
                //this.Hide();
                //personnelMenu.ShowDialog();
                //this.Close();
                return;
            }
                try
                {
                    string subjectQuery = @"SELECT * FROM PDB_ADMIN.QLDH_VIEW_SUBJECT_PDT";

                    using (OracleCommand cmd = new OracleCommand(subjectQuery, DatabaseSession.Connection))
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvSubject.DataSource = dt;

                        // Show subjects information
                        dgvSubject.Columns["MAMH"].HeaderText = "SUBJECT CODE";
                        dgvSubject.Columns["MAHP"].HeaderText = "COURSE CODE";
                        dgvSubject.Columns["TENHP"].HeaderText = "COURSE NAME";
                        dgvSubject.Columns["MADV"].HeaderText = "FACILITY CODE";
                        dgvSubject.Columns["TENDV"].HeaderText = "FACILITY NAME";
                        dgvSubject.Columns["SOTC"].HeaderText = "CREDIT";
                        dgvSubject.Columns["HOTEN"].HeaderText = "TEACHER";
                        dgvSubject.Columns["HK"].HeaderText = "TERM";
                        dgvSubject.Columns["NAMHOC"].HeaderText = "YEAR";
                        dgvSubject.Columns["NGAYBATDAU"].HeaderText = "START";
                        dgvSubject.Columns["NGAYKETTHUC"].HeaderText = "END";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.Message);
                }
            
		}

        private void pbSubject_Click(object sender, EventArgs e)
        {
			SubjectManagement subjectManager = new SubjectManagement();
			this.Hide();
			subjectManager.ShowDialog();
			this.Close();
		}

        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubject.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a subject to delete.");
                return;
            }

            // Lấy mã môn học từ dòng đang chọn
            string selectedMaMH = dgvSubject.SelectedRows[0].Cells["MAMH"].Value.ToString();

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this subject?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string deleteQuery = "DELETE FROM pdb_admin.QLDH_MONHOC WHERE MAMH = :mam";

                    using (OracleCommand cmd = new OracleCommand(deleteQuery, DatabaseSession.Connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("mam", selectedMaMH));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Subject deleted successfully.");
                            LoadSubject(); // Load lại danh sách sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show("Subject not found or could not be deleted.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting subject:\n" + ex.Message);
                }
            }
        }

        private void pbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSubject.CurrentRow == null || dgvSubject.CurrentRow.Cells["MAMH"].Value == null)
                {
                    MessageBox.Show("Please choose a Subject first!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string subjectCode = dgvSubject.CurrentRow.Cells["MAMH"].Value.ToString();
                string facility = dgvSubject.CurrentRow.Cells["MADV"].Value.ToString();

                // Hiển thị thông báo xác nhận trước khi chỉnh sửa
                DialogResult result = MessageBox.Show(
                    $"Do you want to edit this subject \"{subjectCode}\"?",
                    "Confirm Edit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    UpdateSubject updateSubjectForm = new UpdateSubject(subjectCode, facility);

                    this.Hide();
                    updateSubjectForm.ShowDialog();
                    this.Close();

                    LoadSubject();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edit role failed:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbReload_Click(object sender, EventArgs e)
        {
            LoadSubject();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void pbAddSubject_Click(object sender, EventArgs e)
        {
            AddSubject addSubject = new AddSubject();
            this.Hide();
            addSubject.ShowDialog();
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSubject.CurrentRow == null || dgvSubject.CurrentRow.Cells["MAMH"].Value == null)
                {
                    MessageBox.Show("Please choose a Subject first!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string courseCode = dgvSubject.CurrentRow.Cells["MAHP"].Value.ToString();
                ViewSubject subjectDetail = new ViewSubject(courseCode);
                this.Hide();
                subjectDetail.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

		private void label2_Click(object sender, EventArgs e)
		{
			ClassList classList = new ClassList();
			this.Hide();
			classList.ShowDialog();
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

		private void label7_Click(object sender, EventArgs e)
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

        private void notifications_Click(object sender, EventArgs e)
        {
            ViewNotice viewNotification = new ViewNotice(Login.ID);
            this.Hide();
            viewNotification.ShowDialog();
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
