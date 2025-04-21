using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Oracle.ManagedDataAccess.Client;

namespace SchoolManagement
{
    public partial class TeacherProfile : KryptonForm
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
        public TeacherProfile()
        {
            InitializeComponent();
            LoadTextBox();
        }
        private void LoadTextBox()
        {
            try
            {
                string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                OracleConnection conn = new OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM SYSTEM.GIAOVIEN, SYSTEM.TAIKHOAN WHERE MAGV='" + Login.ID + "' AND MAGV=TENDN";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US" 

                dr.Read();
                txtHoTen.Text = dr.GetString(2);
                txtID.Text = dr.GetString(0);
                txtClass.Text = dr.GetString(1);
                txtBirth.Text = dr.GetString(4);
                txtAddress.Text = dr.GetString(5);
                txtGender.Text = dr.GetString(3);
                txtSalary.Text = double.Parse(dr.GetString(6)).ToString("#,###", cul.NumberFormat) + "VND";
                txtPassword.Text = dr.GetString(9);

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
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
    }
}
