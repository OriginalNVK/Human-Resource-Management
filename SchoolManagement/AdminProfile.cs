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
    public partial class AdminProfile : KryptonForm
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
        public AdminProfile()
        {
            InitializeComponent();
            LoadTextBox();
        }

        private void LoadTextBox()
        {
            /*try
            {
				string oradb = ConfigurationManager
			   .ConnectionStrings["SchoolDB"]
			   .ConnectionString;
				OracleConnection conn = new OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM pdb_adminTAIKHOAN WHERE MATK='" + Login.ID + "'";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                dr.Read();
                txtHoTen.Text = dr.GetString(1);
                txtID.Text = dr.GetString(0);
                txtPassword.Text = dr.GetString(2);

                conn.Dispose();
            }*/

            try
            {
                string query = "SELECT USERNAME, USER_ID FROM dba_users WHERE username = 'ADMIN01'";
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string username = reader.GetString(1);
                        string accountStatus = reader.GetString(0);

                        txtHoTen.Text = accountStatus;
                        txtID.Text = username;
                        txtPassword.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy người dùng ADMIN01.");
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
				string oradb = ConfigurationManager
			   .ConnectionStrings["SchoolDB"]
			   .ConnectionString;
				OracleConnection conn = new OracleConnection(oradb);  // C#
                OracleCommand cmd = new OracleCommand("SP_TAIKHOAN_PASSWORD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_tendn", OracleDbType.Varchar2).Value = Login.ID;
                cmd.Parameters.Add("p_matkhau", OracleDbType.Varchar2).Value = txtPassword.Text;
                conn.Open();

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfull");
                this.Close();

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        { 
            this.Close();
        }
    }
}
