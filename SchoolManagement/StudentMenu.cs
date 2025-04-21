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
    public partial class StudentMenu : KryptonForm
    {
        public StudentMenu()
        {
            InitializeComponent(); 
            LoadInfo();
        }

        private void LoadInfo()
        {
            try
            {
                string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                OracleConnection conn = new OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM SYSTEM.SINHVIEN WHERE MSSV='" + Login.ID + "'";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    lbHello.Text = "Hello, Student " + dr.GetString(3);
                }
                else
                {
                    Login login = new Login();
                    this.Hide();
                    login.ShowDialog();
                    this.Close();
                }

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
    }

        private void pbLogout_Click(object sender, EventArgs e)
        { 
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void pbGrade_Click(object sender, EventArgs e)
        {
            StudentGrade studentGrade = new StudentGrade();
            studentGrade.Show();
        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            StudentProfile student = new StudentProfile();
            student.ShowDialog();
        }

        private void pbCalendar_Click(object sender, EventArgs e)
        { 
            Schedule schedule = new Schedule(false);
            schedule.Show();
        }
    }
}
