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
    public partial class StudentGrade : KryptonForm
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
        public StudentGrade()
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
                cmd.CommandText = "SELECT rownum no, A.MALOPHP \"Class section ID\", D.MAMH \"Subject ID\", D.TENMH \"Subject name\", D.SOTC \"Subject credits\", A.GIUAKI \"Mid term\", A.CUOIKI \"Final term\", A.DTB \"Average\" FROM ( SELECT MALOPHP, MSSV, GIUAKI, CUOIKI, DTB FROM SYSTEM.KETQUA WHERE MSSV='" + Login.ID + "' ) A, ( SELECT MALOPHP, C.MAMH, TENMH, SOTC FROM ( SELECT MALOPHP, MAMH FROM SYSTEM.LOPHP ) B, ( SELECT MAMH, TENMH, SOTC FROM SYSTEM.MONHOC ) C WHERE B.MAMH=C.MAMH ) D WHERE A.MALOPHP=D.MALOPHP";
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();


                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dgvStudents.DataSource = dataTable;


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
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT rownum no, A.MALOPHP \"Class section ID\", D.MAMH \"Subject ID\", D.TENMH \"Subject name\", D.SOTC \"Subject credits\", A.GIUAKI \"Mid term\", A.CUOIKI \"Final term\", A.DTB \"Average\" FROM ( SELECT MALOPHP, MSSV, GIUAKI, CUOIKI, DTB FROM SYSTEM.KETQUA WHERE MSSV='" + Login.ID + "' ) A, ( SELECT MALOPHP, C.MAMH, TENMH, SOTC FROM ( SELECT MALOPHP, MAMH FROM SYSTEM.LOPHP ) B, ( SELECT MAMH, TENMH, SOTC FROM SYSTEM.MONHOC ) C WHERE B.MAMH=C.MAMH ) D WHERE A.MALOPHP=D.MALOPHP AND (A.MALOPHP LIKE '%" + txtSearch.Text + "%' OR D.MAMH LIKE '%" + txtSearch.Text + "%' OR D.TENMH LIKE '%" + txtSearch.Text + "%' OR D.SOTC LIKE '%" + txtSearch.Text + "%' OR A.GIUAKI LIKE '%" + txtSearch.Text + "%' OR A.CUOIKI LIKE '%" + txtSearch.Text + "%' OR A.DTB LIKE '%" + txtSearch.Text + "%')";
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();


                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dgvStudents.DataSource = dataTable;

                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void pbReload_Click(object sender, EventArgs e)
        {
            LoadStudents();
            txtSearch.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Data";
            for (int i = 1; i < dgvStudents.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgvStudents.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dgvStudents.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dgvStudents.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgvStudents.Rows[i].Cells[j].Value.ToString();
                }
            }
            workbook.SaveAs("Desktop\\Data.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            app.Quit();
        }
    }
}
