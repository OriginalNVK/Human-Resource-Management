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
    public partial class UpdateSubject : KryptonForm
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

        public string SubjectCode { get; private set; }
        public string Facility { get; private set; }
        public UpdateSubject(string mamh, string kh)
        {
            InitializeComponent();
            SubjectCode = mamh;
            Facility = kh;
            getAllFacility();
            getAllTeacher(kh);
            getSubjectInformation(mamh);
        }

        private void getAllFacility()
        {
            try
            {
                string facilityQuery = @"SELECT MADV, TENDV FROM pdb_admin.QLDH_DONVI";

                using (OracleCommand cmd = new OracleCommand(facilityQuery, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    facilityList.DataSource = dt;
                    facilityList.DisplayMember = "TENDV"; // Hiển thị tên khoa
                    facilityList.ValueMember = "MADV";    // Lưu MADV khi cần
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }
        }

        private void getAllTeacher(string kh)
        {
            try
            {
                string teacherQuery = @"SELECT MANV, HOTEN FROM pdb_admin.QLDH_NHANVIEN WHERE MADV = :kh";

                using (OracleCommand cmd = new OracleCommand(teacherQuery, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("kh", kh));
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        teacherList.DataSource = dt;
                        teacherList.DisplayMember = "HOTEN";
                        teacherList.ValueMember = "MANV";
                    }
                }    
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }
        }

        private void getSubjectInformation(string mamh)
        {
            try
            {
                string findSubject = @"SELECT mh.MAMH, mh.MAHP, hp.TENHP, hp.SOTC, hp.STLT, hp.STTH, hp.MADV, nv.HOTEN, nv.MANV, mh.NGAYBATDAU, mh.NGAYKETTHUC 
                                       FROM pdb_admin.QLDH_MONHOC mh JOIN pdb_admin.QLDH_HOCPHAN hp ON mh.MAHP = hp.MAHP
                                       JOIN pdb_admin.QLDH_NHANVIEN nv ON nv.MANV = mh.MAGV
                                       WHERE mh.MAMH = :mamh";
                using (OracleCommand cmd = new OracleCommand(@findSubject, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("mamh", mamh));

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtSubjectCode.Text = reader["MAMH"].ToString();
                            txtCourseCode.Text = reader["MAHP"].ToString();
                            txtCourseName.Text = reader["TENHP"].ToString();
                            txtCredits.Text = reader["SOTC"].ToString();
                            txtTheory.Text = reader["STLT"].ToString();
                            txtPractical.Text = reader["STTH"].ToString() ;

                            facilityList.SelectedValue = reader["MADV"].ToString();

                            teacherList.SelectedValue = reader["MANV"].ToString();

                            startDay.Value = Convert.ToDateTime(reader["NGAYBATDAU"]);
                            endDay.Value = Convert.ToDateTime(reader["NGAYKETTHUC"]);
                        }
                        else
                        {
                            MessageBox.Show("Subject not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SubjectManagement subjectManagement = new SubjectManagement();
            this.Hide();
            subjectManagement.ShowDialog();
            this.Close();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        (bool isInSemester, DateTime semStart, DateTime semEnd) GetCurrentSemesterRange()
        {
            DateTime today = DateTime.Today;
            int year = today.Year;

            DateTime semester1Start = new DateTime(year, 9, 1);
            DateTime semester1End = new DateTime(year, 12, 31);

            DateTime semester2Start = new DateTime(year, 1, 1);
            DateTime semester2End = new DateTime(year, 4, 30);

            DateTime semester3Start = new DateTime(year, 5, 1);
            DateTime semester3End = new DateTime(year, 8, 31);

            if (today >= semester1Start && today <= semester1End)
                return (true, semester1Start, semester1End);

            if (today >= semester2Start && today <= semester2End)
                return (true, semester2Start, semester2End);

            if (today >= semester3Start && today <= semester3End)
                return (true, semester3Start, semester3End);

            return (false, DateTime.MinValue, DateTime.MinValue);
        }

        private int GetCurrentSemesterCode()
        {
            var now = DateTime.Today;
            if (now.Month >= 9 && now.Month <= 12) return 1; // Học kỳ 1
            if (now.Month >= 1 && now.Month <= 4) return 2;  // Học kỳ 2
            return 3; // Học kỳ 3 (tháng 5-8)
        }

        private void btnUpdateSubject_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtSubjectCode.Text) ||
                    string.IsNullOrWhiteSpace(txtCourseCode.Text) ||
                    string.IsNullOrWhiteSpace(txtCourseName.Text) ||
                    facilityList.SelectedIndex < 0 ||
                    teacherList.SelectedIndex < 0 ||
                    string.IsNullOrWhiteSpace(txtCredits.Text) ||
                    string.IsNullOrWhiteSpace(txtTheory.Text) ||
                    string.IsNullOrWhiteSpace(txtPractical.Text))
                {
                    MessageBox.Show("Please enter all field.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra định dạng số
                if (!int.TryParse(txtCredits.Text.Trim(), out int credits) ||
                    !int.TryParse(txtTheory.Text.Trim(), out int theory) ||
                    !int.TryParse(txtPractical.Text.Trim(), out int practical))
                {
                    MessageBox.Show("Number of credits, theory lessons and practical lessons must be integer.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra ngày kết thúc không nhỏ hơn ngày bắt đầu
                if (endDay.Value.Date < startDay.Value.Date)
                {
                    MessageBox.Show("INVALID DAY.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var (validSemester, semStart, semEnd) = GetCurrentSemesterRange();

                if (!validSemester || startDay.Value.Date < semStart || startDay.Value.Date > semEnd || endDay.Value.Date < semStart || endDay.Value.Date > semEnd)
                {
                    MessageBox.Show("Start day and End day must be in this term.", "INVALID TIME", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string subjectCode = txtSubjectCode.Text.Trim();
                string courseCode = txtCourseCode.Text.Trim();
                string courseName = txtCourseName.Text.Trim();
                string facility = facilityList.SelectedValue.ToString();
                string teacherCode = teacherList.SelectedValue.ToString();
                string start = startDay.Value.Date.ToString("dd-MM-yyyy");
                string end = endDay.Value.Date.ToString("dd-MM-yyyy");

                string updateCourse = @"UPDATE pdb_admin.QLDH_HOCPHAN 
                                SET TENHP = :tenhp, SOTC = :tc, STLT = :lt, STTH = :th, MADV = :khoa 
                                WHERE MAHP = :mahp";

                string updateSubject = @"UPDATE pdb_admin.QLDH_MONHOC 
                                 SET MAGV = :magv, NGAYBATDAU = TO_DATE(:startDay, 'DD-MM-YYYY'), NGAYKETTHUC = TO_DATE(:endDay, 'DD-MM-YYYY') 
                                 WHERE MAMH = :mamh";

                using (OracleTransaction transaction = DatabaseSession.Connection.BeginTransaction())
                {
                    try
                    {
                        using (OracleCommand cmd1 = new OracleCommand(updateCourse, DatabaseSession.Connection))
                        {
                            cmd1.Transaction = transaction;
                            cmd1.BindByName = true;
                            cmd1.Parameters.Add(new OracleParameter("mahp", courseCode));
                            cmd1.Parameters.Add(new OracleParameter("tenhp", courseName));
                            cmd1.Parameters.Add(new OracleParameter("tc", credits));
                            cmd1.Parameters.Add(new OracleParameter("lt", theory));
                            cmd1.Parameters.Add(new OracleParameter("th", practical));
                            cmd1.Parameters.Add(new OracleParameter("khoa", facility));
                            cmd1.ExecuteNonQuery();
                        }

                        // Xác định học kỳ hiện tại để chèn vào cột HK và NAM
                        int currentHK = GetCurrentSemesterCode();
                        int currentYear = DateTime.Today.Year - 1;

                        using (OracleCommand cmd2 = new OracleCommand(updateSubject, DatabaseSession.Connection))
                        {
                            cmd2.Transaction = transaction;
                            cmd2.Parameters.Add(new OracleParameter("magv", teacherCode));
                            cmd2.Parameters.Add(new OracleParameter("startDay", start));
                            cmd2.Parameters.Add(new OracleParameter("endDay", end));
                            cmd2.Parameters.Add(new OracleParameter("mamh", subjectCode));
                            cmd2.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Subject and course created successfully.");
                        SubjectManagement subjectManagement = new SubjectManagement();
                        this.Hide();
                        subjectManagement.ShowDialog();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error:\n" + ex.Message, "Insert failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

		private void notifications_Click(object sender, EventArgs e)
		{

		}
	}
}
