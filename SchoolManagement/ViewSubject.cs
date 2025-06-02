using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Oracle.ManagedDataAccess.Client;

namespace SchoolManagement
{
    public partial class ViewSubject : KryptonForm
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

        public string CourseCode { get; private set; }

        public ViewSubject(string mahp)
        {
            InitializeComponent();
            CourseCode = mahp;
            getCourseInformation(mahp);
        }

        private void getCourseInformation(string mahp)
        {
            try
            {
                string findCourse = @"SELECT mh.MAMH, mh.MAHP, hp.TENHP, hp.SOTC, hp.STLT, hp.STTH, hp.MADV, nv.HOTEN, dv.TENDV, nv.MANV, mh.NGAYBATDAU, mh.NGAYKETTHUC 
                                       FROM pdb_admin.QLDH_MONHOC mh JOIN pdb_admin.QLDH_HOCPHAN hp ON mh.MAHP = hp.MAHP
                                       JOIN pdb_admin.QLDH_NHANVIEN nv ON nv.MANV = mh.MAGV
                                       JOIN pdb_admin.QLDH_DONVI dv ON dv.MADV = hp.MADV
                                       WHERE mh.MAHP = :mahp";
                using (OracleCommand cmd = new OracleCommand(@findCourse, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("mahp", mahp));

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtSubjectCode.Text = reader["MAMH"].ToString().Trim();
                            txtCourseCode.Text = reader["MAHP"].ToString().Trim();
                            txtCourseName.Text = reader["TENHP"].ToString().Trim();
                            txtCredits.Text = reader["SOTC"].ToString().Trim();
                            txtTheory.Text = reader["STLT"].ToString().Trim();
                            txtPractical.Text = reader["STTH"].ToString().Trim() ;
                            txtFacility.Text = reader["TENDV"].ToString().Trim();
                            txtTeacher.Text = reader["HOTEN"].ToString().Trim();
                            txtStart.Text = Convert.ToDateTime(reader["NGAYBATDAU"]).ToString("dd/MM/yyyy");
                            txtEnd.Text = Convert.ToDateTime(reader["NGAYKETTHUC"]).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            MessageBox.Show("Course not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        private void lbClasses_Click(object sender, EventArgs e)
        {
            SubjectManagement sub = new SubjectManagement();
            this.Hide();
            sub.ShowDialog();
            this.Close();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

		private void notifications_Click(object sender, EventArgs e)
		{
            ViewNotice viewNotification = new ViewNotice(Login.ID);
            this.Hide();
            viewNotification.ShowDialog();
            this.Close();
        }

		private void label5_Click(object sender, EventArgs e)
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

		private void label9_Click(object sender, EventArgs e)
		{
			ViewSchedule viewDetail = new ViewSchedule(PersonnelMenu._username, PersonnelMenu._role);
			this.Hide();
			viewDetail.ShowDialog();
			this.Close();
		}

		private void label15_Click(object sender, EventArgs e)
		{
			PersonnelRegister personnelRegister = new PersonnelRegister();
			this.Hide();
			personnelRegister.ShowDialog();
			this.Close();
		}
	}
}
