using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
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
    public partial class AddSubject : KryptonForm
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
		public AddSubject()
        {
            InitializeComponent();
            getAllFacility();
            getAllTeacher();
			facilityList.SelectedIndexChanged += FacilityOrLocationChanged;
			locationList.SelectedIndexChanged += FacilityOrLocationChanged;
		}

        private void getAllFacility()
        {
            try
            {
                string facilityQuery = @"SELECT MIN(MADV) AS MADV, TENDV
										   FROM pdb_admin.QLDH_DONVI
										   GROUP BY TENDV
										   ORDER BY TENDV";

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

        private void getAllTeacher()
        {
            try
            {
                string teacherQuery = @"SELECT MANV, HOTEN 
                                        FROM pdb_admin.QLDH_NHANVIEN";

                using (OracleCommand cmd = new OracleCommand(teacherQuery, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    teacherList.DataSource = dt;
                    teacherList.DisplayMember = "HOTEN";
                    teacherList.ValueMember = "MANV";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }
        }

		private void FacilityOrLocationChanged(object sender, EventArgs e)
		{
			if (facilityList.SelectedItem == null || locationList.SelectedItem == null)
				return;

			string selectedFacility = facilityList.Text.ToString();
			string selectedLocation = locationList.Text.ToString();

			try
			{
				string getIdQuery = @"SELECT MADV 
                              FROM PDB_ADMIN.QLDH_DONVI 
                              WHERE TENDV = :dep AND COSO = :location";

				string queSeDepID = null;

				using (OracleCommand cmdGetMADV = new OracleCommand(getIdQuery, DatabaseSession.Connection))
				{
					cmdGetMADV.Parameters.Add(new OracleParameter("dep", selectedFacility));
					cmdGetMADV.Parameters.Add(new OracleParameter("location", selectedLocation));

					using (OracleDataReader reader = cmdGetMADV.ExecuteReader())
					{
						if (reader.Read())
							queSeDepID = reader.GetString(0);
						else
						{
							teacherList.DataSource = null;
							MessageBox.Show("Không tìm thấy đơn vị phù hợp.");
							return;
						}
					}
				}

				string teacherQuery = @"SELECT MANV, HOTEN FROM pdb_admin.QLDH_NHANVIEN WHERE MADV = :kh";

				using (OracleCommand cmd = new OracleCommand(teacherQuery, DatabaseSession.Connection))
				{
					cmd.Parameters.Add(new OracleParameter("kh", queSeDepID));
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
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi cập nhật danh sách giáo viên: " + ex.Message);
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

        private void btnCreateSubject_Click(object sender, EventArgs e)
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
                    string.IsNullOrWhiteSpace(txtPractical.Text)
                    )
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
                string facility = facilityList.Text.ToString();
                string location = locationList.Text.ToString();
                string teacherCode = teacherList.SelectedValue.ToString();
                string start = startDay.Value.Date.ToString("dd-MM-yyyy");
                string end = endDay.Value.Date.ToString("dd-MM-yyyy");

				string queSeDepID = null;
                string getIdQuery = @"SELECT MADV 
                                      FROM PDB_ADMIN.QLDH_DONVI 
                                      WHERE TENDV = :dep 
                                      AND COSO = :location";

				using (OracleTransaction transaction = DatabaseSession.Connection.BeginTransaction())
                {
                    using (OracleCommand cmdGetMADV = new OracleCommand(getIdQuery, DatabaseSession.Connection))
                    {
                        cmdGetMADV.Transaction = transaction;
                        cmdGetMADV.Parameters.Add(new OracleParameter("dep", OracleDbType.Varchar2)).Value = facility;
						cmdGetMADV.Parameters.Add(new OracleParameter("location", OracleDbType.Varchar2)).Value = location;

						using (OracleDataReader reader = cmdGetMADV.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                queSeDepID = reader.GetString(0);
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageBox.Show("Không tìm thấy đơn vị phù hợp.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }

				string insertCourse = @"INSERT INTO pdb_admin.QLDH_HOCPHAN (MAHP, TENHP, SOTC, STLT, STTH, MADV)
                                        VALUES (:mahp, :tenhp, :tc, :lt, :th, :khoa)";

                string insertSubject = @"INSERT INTO pdb_admin.QLDH_MONHOC (MAMH, MAHP, MAGV, HK, NAM, NGAYBATDAU, NGAYKETTHUC)
                                         VALUES (:mamh, :mahp, :magv, :hk, :nam, TO_DATE(:startDay, 'DD-MM-YYYY'), TO_DATE(:endDay, 'DD-MM-YYYY'))";

                using (OracleTransaction transaction = DatabaseSession.Connection.BeginTransaction())
                {
                    try
                    {
                        using (OracleCommand cmd1 = new OracleCommand(insertCourse, DatabaseSession.Connection))
                        {
                            cmd1.Transaction = transaction;
                            cmd1.Parameters.Add(new OracleParameter("mahp", courseCode));
                            cmd1.Parameters.Add(new OracleParameter("tenhp", courseName));
                            cmd1.Parameters.Add(new OracleParameter("tc", credits));
                            cmd1.Parameters.Add(new OracleParameter("lt", theory));
                            cmd1.Parameters.Add(new OracleParameter("th", practical));
                            cmd1.Parameters.Add(new OracleParameter("khoa", queSeDepID));
                            cmd1.ExecuteNonQuery();
                        }

                        // Xác định học kỳ hiện tại để chèn vào cột HK và NAM
                        int currentHK = GetCurrentSemesterCode();
                        int currentYear = DateTime.Today.Year - 1;

                        using (OracleCommand cmd2 = new OracleCommand(insertSubject, DatabaseSession.Connection))
                        {
                            cmd2.Transaction = transaction;
                            cmd2.Parameters.Add(new OracleParameter("mamh", subjectCode));
                            cmd2.Parameters.Add(new OracleParameter("mahp", courseCode));
                            cmd2.Parameters.Add(new OracleParameter("magv", teacherCode));
                            cmd2.Parameters.Add(new OracleParameter("hk", currentHK));
                            cmd2.Parameters.Add(new OracleParameter("nam", currentYear));
                            cmd2.Parameters.Add(new OracleParameter("startDay", start));
                            cmd2.Parameters.Add(new OracleParameter("endDay", end));
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

		private void label6_Click(object sender, EventArgs e)
		{
			GradeManager gradeManager = new GradeManager();
			this.Hide();
			gradeManager.ShowDialog();
			this.Close();
		}
	}
}
