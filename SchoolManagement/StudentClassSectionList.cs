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
    public partial class StudentClassSectionList : KryptonForm
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
        public StudentClassSectionList()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                OracleConnection conn = new OracleConnection(oradb);  // C#
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT MSSV FROM SYSTEM.SINHVIEN";
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    string sname = reader.GetString(0);
                    cbStudents.Items.Add(sname.ToString());
                }

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                OracleConnection conn = new OracleConnection(oradb);  // C#
                OracleCommand cmd = new OracleCommand("SP_LOPHP_SINHVIEN_ADD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_malophp", OracleDbType.Varchar2).Value = RoleManager.ClassSectionID;
                cmd.Parameters.Add("p_mssv", OracleDbType.Varchar2).Value = cbStudents.Text; 
                conn.Open();

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
            this.Close();
        }
    }
}
