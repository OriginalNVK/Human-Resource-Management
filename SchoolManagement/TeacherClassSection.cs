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
    public partial class TeacherClassSection : KryptonForm
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
        private int action; // 0 - add, 1 - edit
        private bool isSelected = false;
        private int currFrom = 1;
        private int pageSize = 10;


        public static string ClassSectionID;
        public static string SubjectID;
        public static int limited;
        public TeacherClassSection()
        {
            InitializeComponent();
            LoadClasses();
        }
        private void LoadClasses()
        {
            try
            {
                string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                OracleConnection conn = new OracleConnection(oradb);  // C#
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM ( SELECT a.MALOPHP \"CLASS SECTION ID\", a.MAMH \"SUBJECT ID\", a.MAGV \"TEACHER ID\", a.BATDAU \"START\", a.KETTHUC \"FINISH\", a.LICHHOC \"SCHEDULE\", a.SISO \"N.O.S\", rownum r__ FROM ( SELECT * FROM SYSTEM.LOPHP WHERE MAGV='" + Login.ID + "' ORDER BY MALOPHP ASC ) a WHERE rownum < ((" + currFrom.ToString() + " * " + pageSize.ToString() + ") + 1 ) ) WHERE r__ >= (((" + currFrom.ToString() + "-1) * " + pageSize.ToString() + ") + 1)";
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();


                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dgvClass.DataSource = dataTable;


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
                cmd.CommandText = "SELECT * FROM ( SELECT a.MALOPHP \"CLASS SECTION ID\", a.MAMH \"SUBJECT ID\", a.MAGV \"TEACHER ID\", a.BATDAU \"START\", a.KETTHUC \"FINISH\", a.LICHHOC \"SCHEDULE\", a.SISO \"N.O.S\", rownum r__ FROM ( SELECT * FROM SYSTEM.LOPHP WHERE MAGV='" + Login.ID + "' AND (MALOPHP LIKE '%" + txtSearch.Text + "%' OR MAMH LIKE '%" + txtSearch.Text + "%' OR BATDAU LIKE '%" + txtSearch.Text + "%' OR KETTHUC LIKE '%" + txtSearch.Text + "%' OR LICHHOC LIKE '%" + txtSearch.Text + "%' OR SISO LIKE '%" + txtSearch.Text + "%') ORDER BY MALOPHP ASC ) a WHERE rownum < ((" + currFrom.ToString() + " * " + pageSize.ToString() + ") + 1 ) ) WHERE r__ >= (((" + currFrom.ToString() + "-1) * " + pageSize.ToString() + ") + 1)";
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();


                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dgvClass.DataSource = dataTable;


                conn.Dispose();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void pbReload_Click(object sender, EventArgs e)
        {
            isSelected = false;
            txtSearch.Text = "";
            LoadClasses();
        }

        private void openStudents()
        {
            StudentsInClassSection studentsInClassSection = new StudentsInClassSection("Giao vien");
            studentsInClassSection.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (isSelected)
            {
                openStudents();
            }
        }

        private void dgvClass_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isSelected = true;
            openStudents();
        }

        private void dgvClass_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                isSelected = true; 

                DataGridViewRow row = dgvClass.Rows[e.RowIndex];
                 

                ClassSectionID = row.Cells[0].Value.ToString();
                SubjectID = row.Cells[1].Value.ToString();
                limited = Int32.Parse(row.Cells[6].Value.ToString());
            }
        }
    }
}
