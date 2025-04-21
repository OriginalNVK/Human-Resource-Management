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
    public partial class Schedule : KryptonForm
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
        private static bool isGiaoVien;
        public Schedule()
        {
            InitializeComponent();
            LoadSchedule();
        }

        public Schedule(bool b)
        {
            isGiaoVien = b;
            InitializeComponent();
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            if (isGiaoVien)
            {
                try
                {
                    string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                    OracleConnection conn = new OracleConnection(oradb);  // C#
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT A.MAMH, TENMH, LICHHOC FROM SYSTEM.LOPHP A, SYSTEM.MONHOC B WHERE A.MAMH = B.MAMH AND MAGV = '" + Login.ID + "' ORDER BY LICHHOC ASC";
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string sname = reader.GetString(2);
                        if (sname.Contains("Thứ 2"))
                        {
                            txtMonday.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 3"))
                        {
                            txtTuesday.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 4"))
                        {
                            txtWed.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 5"))
                        {
                            txtThurs.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 6"))
                        {
                            txtFri.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 7"))
                        {
                            txtSat.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                    }

                    conn.Dispose();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            }
            else
            {
                try
                {
                    string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                    OracleConnection conn = new OracleConnection(oradb);  // C#
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT B.MAMH, C.TENMH, B.LICHHOC FROM SYSTEM.KETQUA A, SYSTEM.LOPHP B, SYSTEM.MONHOC C WHERE A.MSSV='" + Login.ID + "' AND A.MALOPHP=B.MALOPHP AND B.MAMH=C.MAMH ORDER BY LICHHOC ASC";
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string sname = reader.GetString(2);
                        if (sname.Contains("Thứ 2"))
                        {
                            txtMonday.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 3"))
                        {
                            txtTuesday.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 4"))
                        {
                            txtWed.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 5"))
                        {
                            txtThurs.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 6"))
                        {
                            txtFri.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                        else if (sname.Contains("Thứ 7"))
                        {
                            txtSat.Text += reader.GetString(0) + " - " + reader.GetString(1) + "\n" + sname + "\n\n\n";
                        }
                    }

                    conn.Dispose();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            }
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {

        }

        private void txtSat_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
