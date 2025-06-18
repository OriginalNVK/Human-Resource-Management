using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Oracle.ManagedDataAccess.Client;

namespace SchoolManagement
{
	public partial class ViewSchedule : KryptonForm
	{
		private string _manv;
        private string _role;

		public ViewSchedule(string manv, string role)
		{
			InitializeComponent();
            _manv = manv;
            _role = role;
			LoadSubject(manv, role);
		}

		private void LoadSubject(string manv, string role)
		{
			try
			{
                string subjectQuery;
                switch (role)
                {
                    case "GV":
                        subjectQuery = @"SELECT * FROM PDB_ADMIN.QLDH_VIEW_SCHEDULES_BY_GV";
                        break;
                    case "TRGĐV":
                        subjectQuery = @"SELECT * FROM PDB_ADMIN.VIEW_SCHEDULE_OF_EMPLOYEES";
                        break;
                    default:
                        MessageBox.Show("Vai trò không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                    using (OracleCommand cmd = new OracleCommand(@subjectQuery, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("manv", manv));

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvSchedule.DataSource = dt;

                        // Show subjects information
                        dgvSchedule.Columns["MAMH"].HeaderText = "SUBJECT CODE";
                        dgvSchedule.Columns["MAHP"].HeaderText = "COURSE CODE";
                        dgvSchedule.Columns["TENHP"].HeaderText = "COURSE NAME";
                        dgvSchedule.Columns["SOTC"].HeaderText = "CREDIT";
                        dgvSchedule.Columns["STLT"].HeaderText = "THEORY LESSON";
                        dgvSchedule.Columns["STTH"].HeaderText = "PRACTICAL LESSONS";
                        dgvSchedule.Columns["HK"].HeaderText = "TERM";
                        dgvSchedule.Columns["NAMHOC"].HeaderText = "YEAR";
                    }
                }    
                
            }
			catch (Exception ex)
			{
                MessageBox.Show("Error:\n" + ex.Message);
			}
		}

        private void pbReload_Click(object sender, EventArgs e)
        {
            LoadSubject(_manv, _role);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSchedule.CurrentRow == null || dgvSchedule.CurrentRow.Cells["MAMH"].Value == null)
                {
                    MessageBox.Show("Please choose a Subject first!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string courseCode = dgvSchedule.CurrentRow.Cells["MAHP"].Value.ToString();
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

		private void notifications_Click(object sender, EventArgs e)
		{
            ViewNotice viewNotification = new ViewNotice(Login.ID);
            this.Hide();
            viewNotification.ShowDialog();
            this.Close();
        }

		private void lbClasses_Click(object sender, EventArgs e)
		{
			SubjectManagement subjectManager = new SubjectManagement();
			this.Hide();
			subjectManager.ShowDialog();
			this.Close();
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

		private void label8_Click(object sender, EventArgs e)
		{
			GradeManager gradeManager = new GradeManager();
			this.Hide();
			gradeManager.ShowDialog();
			this.Close();
		}
	}
}
