using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Design;
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
    public partial class StudentProfile : KryptonForm
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
        public StudentProfile()
        {
            InitializeComponent();
            LoadTextBox();
        }
        private void LoadTextBox()
        {
			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Failed to connect with database!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				string sql = @"SELECT MASV, HOTEN, PHAI, NGSINH, DCHI, DT, TINHTRANG, TENDV
                   FROM PDB_ADMIN.QLDH_SINHVIEN SV
                   JOIN PDB_ADMIN.QLDH_DONVI DV ON SV.KHOA = DV.MADV
                   WHERE SV.MASV = :username";

				OracleCommand cmd = new OracleCommand(sql, conn);
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = Login.ID;

				OracleDataReader dr = cmd.ExecuteReader();
				if (dr.HasRows)
				{
					dr.Read();
					txtID.Text = dr.GetString(0);                                // MASV
					txtHoTen.Text = dr.GetString(1);                             // HOTEN
					txtPhai.Text = dr.GetString(2);                              // PHAI
					txtNgSinh.Text = dr.GetDateTime(3).ToString("dd/MM/yyyy");  // NGSINH
					txtDiaChi.Text = dr.GetString(4);                            // DCHI
					txtDT.Text = dr.GetString(5);                                // DT
					txtTinhTrang.Text = dr.GetString(6);                         // TINHTRANG
					txtKhoa.Text = dr.GetString(7);                              // TENDV
				}
				else
				{
					MessageBox.Show("No data found for the given student ID.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				dr.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                OracleConnection conn = DatabaseSession.Connection;
                if(conn == null || conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Failed to connect with database!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string sql = "UPDATE PDB_ADMIN.QLDH_SINHVIEN SET DCHI = :diachi, DT = :dt WHERE MASV = :masv";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("diachi", OracleDbType.Varchar2).Value = txtDiaChi.Text;
                cmd.Parameters.Add("dt", OracleDbType.Varchar2).Value = txtDT.Text;
                cmd.Parameters.Add("masv", OracleDbType.Varchar2).Value = txtID.Text;
                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated > 0)
                {
                    MessageBox.Show("Update successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No rows updated. Please check your input.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            StudentMenu student = new StudentMenu();
            this.Hide();
            student.ShowDialog();
            this.Close();
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {
            string address = txtDiaChi.Text.Trim();

            // Optional: Example validation (e.g., check if the address is empty)
            if (string.IsNullOrEmpty(address))
            {
                // You can set a visual cue or message if the address is empty
                txtDiaChi.StateCommon.Border.Color1 = Color.Red;
                txtDiaChi.StateCommon.Border.Color2 = Color.Red;
            }
            else
            {
                // Reset border color to default if the address is valid
                this.txtDiaChi.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
                this.txtDiaChi.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            }
        }

        private void txtDT_TextChanged(object sender, EventArgs e)
        {
            string address = txtDiaChi.Text.Trim();

            // Optional: Example validation (e.g., check if the address is empty)
            if (string.IsNullOrEmpty(address))
            {
                // You can set a visual cue or message if the address is empty
                txtDT.StateCommon.Border.Color1 = Color.Red;
                txtDT.StateCommon.Border.Color2 = Color.Red;
            }
            else
            {
                // Reset border color to default if the address is valid
                this.txtDT.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
                this.txtDT.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            }
        }

        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
