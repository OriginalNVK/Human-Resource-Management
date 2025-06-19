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
    public partial class StudentScore : KryptonForm
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
        public StudentScore()
        {
            InitializeComponent();
            LoadScoreBoard();
        }
		
        private void LoadScoreBoard()
        {
            try
            {
                string scoreQuery = @"SELECT * FROM PDB_ADMIN.QLDH_VIEW_SCORE_SV";
				using (OracleCommand cmd = new OracleCommand(scoreQuery, DatabaseSession.Connection))
				{
					using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						adapter.Fill(dt);

						dgvScore.DataSource = dt;

						// Show subjects information
						dgvScore.Columns["MAHP"].HeaderText = "COURSE CODE";
						dgvScore.Columns["TENHP"].HeaderText = "COURSE NAME";
						//dgvScore.Columns["SOTC"].HeaderText = "CREDIT";
						//dgvScore.Columns["STLT"].HeaderText = "THEORY LESSON";
						//dgvScore.Columns["STTH"].HeaderText = "PRACTICAL LESSONS";
						dgvScore.Columns["HK"].HeaderText = "TERM";
						dgvScore.Columns["NAMHOC"].HeaderText = "YEAR";
						dgvScore.Columns["DIEMTH"].HeaderText = "PRACTICAL SCORE";
						dgvScore.Columns["DIEMQT"].HeaderText = "PROCESS SCORE";
						dgvScore.Columns["DIEMCK"].HeaderText = "FINAL SCORE";
						dgvScore.Columns["DIEMTK"].HeaderText = "OVERALL SCORE";
					}
				}
			}
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
		private void quitBtn_Click(object sender, EventArgs e)
		{
			StudentMenu studentMenu = new StudentMenu();
			this.Hide();
			studentMenu.ShowDialog();
			this.Close();
		}
	}
}
