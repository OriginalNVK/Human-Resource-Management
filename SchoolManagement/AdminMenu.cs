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
    public partial class AdminMenu : KryptonForm
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {
            LoadTotals();
        } 

        private void pbLogout_Click(object sender, EventArgs e)
        {
            LogOut();
        }

        private void LogOut()
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }
        private void LoadTotals()
        {
            LoadTotalStudent();
            LoadTotalTeacher();
            LoadTotalClass();
            LoadTotalSubject();
        }
        private void LoadTotalStudent()
        {
            try
            {
                string oradb = ConfigurationManager
			   .ConnectionStrings["SchoolDB"]
			   .ConnectionString;
                OracleConnection conn = new OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(MASV) FROM SYS.QLDH_SINHVIEN";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                dr.Read();
                lbToTalStudent.Text = dr.GetString(0);

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void LoadTotalTeacher()
        {
            try
            {
				string oradb = ConfigurationManager
			   .ConnectionStrings["SchoolDB"]
			   .ConnectionString;
				OracleConnection conn = new OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(MANV) FROM SYS.QLDH_NHANVIEN";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                dr.Read();
                lbTotalTeacher.Text = dr.GetString(0);

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void LoadTotalClass()
        {
            try
            {
				string oradb = ConfigurationManager
			   .ConnectionStrings["SchoolDB"]
			   .ConnectionString;
				OracleConnection conn = new OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(MAHP) FROM SYS.QLDH_HOCPHAN";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                dr.Read();
                lbTotalClass.Text = dr.GetString(0);

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void LoadTotalSubject()
        {
            try
            {
				string oradb = ConfigurationManager
			   .ConnectionStrings["SchoolDB"]
			   .ConnectionString;
				OracleConnection conn = new OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(MADV) FROM SYS.QLDH_DONVI";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                dr.Read();
                lbTotalSubject.Text = dr.GetString(0);

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            AdminProfile myProfile = new AdminProfile();
            myProfile.Show();
        }

        private void pbStudents_Click(object sender, EventArgs e)
        {
            StudentManager student = new StudentManager(); 
            student.Show(); 
        }

        private void pbTeachers_Click(object sender, EventArgs e)
        {
            PersonnelManager teacher = new PersonnelManager();
            teacher.Show();
        }

        private void pbPersonnel_Click(object sender, EventArgs e)
        {
            PersonnelManager personnelManager = new PersonnelManager();
            personnelManager.Show();
        }

        private void pbRole_Click(object sender, EventArgs e)
        {
            RoleManager roleManager = new RoleManager();
			roleManager.Show();
        }

        private void pbUsers_Click(object sender, EventArgs e)
        {
            UsersManager userManager = new UsersManager();
			userManager.Show();
        }

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void label6_Click(object sender, EventArgs e)
		{

		}
	}
}
