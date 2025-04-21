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
    public partial class SubjectManager : KryptonForm
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
        private int pageSize = 20;
        public SubjectManager()
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
                cmd.CommandText = "SELECT * FROM ( SELECT a.MAMH \"Subject ID\", a.TENMH \"Name\", a.SOTC \"Credits\", rownum r__ FROM ( SELECT * FROM SYSTEM.MONHOC ORDER BY MAMH ASC ) a WHERE rownum < ((" + currFrom.ToString() + " * " + pageSize.ToString() + ") + 1 ) ) WHERE r__ >= (((" + currFrom.ToString() + "-1) * " + pageSize.ToString() + ") + 1)";
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

        private void pbPrev_Click(object sender, EventArgs e)
        {

            if (currFrom > 1)
            {
                currFrom--;
                LoadStudents();
            }
        }

        private void pbNext_Click(object sender, EventArgs e)
        {

            currFrom++;
            LoadStudents();
        }

        private void dgvStudents_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                isSelected = true;
                showAction();

                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
                txtID.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString(); 
                txtAddress.Text = row.Cells[2].Value.ToString(); 

            }
        }
        private void showAction()
        {
            pbStudents.Visible = true;
            lbStudents.Visible = true;
            pbEdit.Visible = true;
            lbEdit.Visible = true;
            pbDelete.Visible = true;
            lbDelete.Visible = true;
            pbSave.Visible = false;
            lbSave.Visible = false;
        }

        private void pbStudents_Click(object sender, EventArgs e)
        {
            action = 0;

            pbStudents.Visible = false;
            lbStudents.Visible = false;
            pbEdit.Visible = false;
            lbEdit.Visible = false;
            pbDelete.Visible = false;
            lbDelete.Visible = false;
            pbSave.Visible = true;
            lbSave.Visible = true;

            txtID.Visible = true;
            label10.Visible = true;
            txtID.Enabled = true;

            txtName.Text = "";
            txtName.Enabled = true;

            txtAddress.Text = "";
            txtAddress.Enabled = true; 
        }

        private void pbEdit_Click(object sender, EventArgs e)
        {
            if (!isSelected)
            {
                MessageBox.Show("Please choose student to edit!");
                return;
            }
            action = 1;

            pbStudents.Visible = false;
            lbStudents.Visible = false;
            pbEdit.Visible = false;
            lbEdit.Visible = false;
            pbDelete.Visible = false;
            lbDelete.Visible = false;
            pbSave.Visible = true;
            lbSave.Visible = true;

            txtID.Enabled = false;
            txtName.Enabled = true;
            txtAddress.Enabled = true; 
        }

        private void pbReload_Click(object sender, EventArgs e)
        {
            Refesh();
        }
        private void Refesh()
        {
            pbStudents.Visible = true;
            lbStudents.Visible = true;
            pbEdit.Visible = true;
            lbEdit.Visible = true;
            pbDelete.Visible = true;
            lbDelete.Visible = true;
            pbSave.Visible = false;
            lbSave.Visible = false;

            LoadStudents();
            txtSearch.Text = "";
            txtID.Text = "";
            txtName.Text = "";
            txtAddress.Text = ""; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (!isSelected)
            {
                MessageBox.Show("Please choose subject to delete!");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                    OracleConnection conn = new OracleConnection(oradb);  // C#
                    OracleCommand cmd = new OracleCommand("SP_MONHOC_DELETE", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_mamh", OracleDbType.Varchar2).Value = txtID.Text;
                    conn.Open();

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Delete success");

                    conn.Dispose();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            }
            isSelected = false;

            Refesh();
        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            if (action == 0)
            {
                try
                {
                    string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                    OracleConnection conn = new OracleConnection(oradb);  // C#
                    OracleCommand cmd = new OracleCommand("SP_MONHOC_ADD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_mamh", OracleDbType.Varchar2).Value = txtID.Text;
                    cmd.Parameters.Add("p_tenmh", OracleDbType.Varchar2).Value = txtName.Text; 
                    cmd.Parameters.Add("p_sotc", OracleDbType.Int32).Value = Int32.Parse(txtAddress.Text); 
                    conn.Open();

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    cmd.ExecuteNonQuery();

                    conn.Dispose();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
                MessageBox.Show("Add success");
            }
            else
            {
                try
                {
                    string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
                    OracleConnection conn = new OracleConnection(oradb);  // C#
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE SYSTEM.MONHOC SET TENMH=N'" + txtName.Text + "', SOTC=" + txtAddress.Text + " WHERE MAMH='" + txtID.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    OracleDataReader reader = cmd.ExecuteReader();

                    conn.Dispose();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
                MessageBox.Show("Edit success");
            }
            Refesh();
        }
    }
}
