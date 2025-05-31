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
				OracleConnection conn = DatabaseSession.Connection;
				OracleCommand cmd = new OracleCommand("SELECT COUNT(MASV) FROM pdb_admin.QLDH_SINHVIEN", conn);
				cmd.CommandType = CommandType.Text;
				OracleDataReader dr = cmd.ExecuteReader();

				if (dr.Read())
				{
					lbToTalStudent.Text = dr.GetInt32(0).ToString();
				}
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
				OracleConnection conn = DatabaseSession.Connection;
				OracleCommand cmd = new OracleCommand("SELECT COUNT(MANV) FROM pdb_admin.QLDH_NHANVIEN", conn);
				cmd.CommandType = CommandType.Text;
				OracleDataReader dr = cmd.ExecuteReader();

				if (dr.Read())
				{
					lbTotalTeacher.Text = dr.GetInt32(0).ToString();
				}
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
				OracleConnection conn = DatabaseSession.Connection;
				OracleCommand cmd = new OracleCommand("SELECT COUNT(MAHP) FROM pdb_admin.QLDH_HOCPHAN", conn);
				cmd.CommandType = CommandType.Text;
				OracleDataReader dr = cmd.ExecuteReader();

				if (dr.Read())
				{
					lbTotalClass.Text = dr.GetInt32(0).ToString();
				}
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
				OracleConnection conn = DatabaseSession.Connection;
				OracleCommand cmd = new OracleCommand("SELECT COUNT(MADV) FROM pdb_admin.QLDH_DONVI", conn);
				cmd.CommandType = CommandType.Text;
				OracleDataReader dr = cmd.ExecuteReader();

				if (dr.Read())
				{
					lbTotalSubject.Text = dr.GetInt32(0).ToString();
				}
			}
			catch (Exception es)
			{
				MessageBox.Show(es.Message);
			}
		}


		private void pbProfile_Click(object sender, EventArgs e)
        {
            AdminProfile myProfile = new AdminProfile();
            this.Hide();
            myProfile.ShowDialog();
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

		private void notifications_Click(object sender, EventArgs e)
		{

		}
	}
}
