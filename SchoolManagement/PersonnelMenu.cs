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
    public partial class PersonnelMenu : KryptonForm
    {
        public PersonnelMenu()
        {
            InitializeComponent();
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {
            try
            {
                string infoQuery = "SELECT * FROM SYS.QLDH_NHANVIEN WHERE HOTEN = 'NEW'";
                using (OracleCommand cmd =  new OracleCommand(infoQuery, DatabaseSession.Connection))
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string username = reader.GetString(0);
                        string fullname = reader.GetString(1);
                        string gender = reader.GetString(2);
                        string dob = reader.GetString(3);
                        string salary = reader.GetString(4);
                        string bonus = reader.GetString(5);
                        string address = reader.GetString(6);
                        string phone = reader.GetString(7);
                        string role = reader.GetString(8);

                        txtHoTen.Text = fullname;
                        txtRoleName.Text = role;
                        txtID.Text = username;
                        txtBirth.Text = dob;
                        txtAddress.Text = address;
                        txtGender.Text = gender;
                        txtPhone.Text = phone;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
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

		private void pbProfile_Click(object sender, EventArgs e)
        {
            AdminProfile myProfile = new AdminProfile();
            this.Hide();
            myProfile.ShowDialog();
            this.Close();
        }

        private void pbStudents_Click(object sender, EventArgs e)
        {
            StudentManager student = new StudentManager(); 
            this.Hide();
            student.ShowDialog(); 
            this.Close();
        }

        private void pbPersonnel_Click(object sender, EventArgs e)
        {
            PersonnelManager personnelManager = new PersonnelManager();
            this.Hide();
            personnelManager.ShowDialog();
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

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
