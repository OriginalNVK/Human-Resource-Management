using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Oracle.ManagedDataAccess.Client;

namespace SchoolManagement
{
	public partial class ViewSchedule : KryptonForm
	{
		private string _manv;
        private string _role;

		public ViewSchedule(string manv, string role)
		{
			InitializeComponent();
            _manv = manv;
            _role = role;
			LoadSubject(manv, role);
		}

		private void LoadSubject(string manv, string role)
		{
			try
			{
                string subjectQuery;
                switch (role)
                {
                    case "NV_GV":
                        subjectQuery = @"SELECT mh.MAMH, mh.MAHP, hp.TENHP, hp.SOTC, hp.STLT, hp.STTH, mh.HK, TO_CHAR(mh.NAM) || '-' || TO_CHAR(mh.NAM + 1) AS NAMHOC
                                     FROM pdb_admin.QLDH_MONHOC mh JOIN pdb_admin.QLDH_HOCPHAN hp ON mh.MAHP = hp.MAHP
                                     JOIN pdb_admin.QLDH_NHANVIEN nv ON nv.MANV = mh.MAGV
                                     JOIN pdb_admin.QLDH_DONVI dv ON dv.MADV = hp.MADV
                                     WHERE mh.MAGV = :manv";
                        break;
                    case "NV_TRGDV":
                        subjectQuery = @"SELECT mh.MAMH, mh.MAHP, hp.TENHP, hp.SOTC, hp.STLT, hp.STTH, mh.HK, TO_CHAR(mh.NAM) || '-' || TO_CHAR(mh.NAM + 1) AS NAMHOC
                                       FROM pdb_admin.QLDH_MONHOC mh JOIN pdb_admin.QLDH_HOCPHAN hp ON mh.MAHP = hp.MAHP
                                       JOIN pdb_admin.QLDH_NHANVIEN nv ON nv.MANV = mh.MAGV
                                       JOIN pdb_admin.QLDH_DONVI dv ON dv.MADV = hp.MADV
                                       WHERE nv.MADV = (
                                       SELECT MADV 
                                       FROM pdb_admin.QLDH_NHANVIEN 
                                       WHERE MANV = :manv)";
                        break;
                    default:
                        MessageBox.Show("Vai trò không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                    using (OracleCommand cmd = new OracleCommand(@subjectQuery, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("manv", manv));

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvSchedule.DataSource = dt;

                        // Show subjects information
                        dgvSchedule.Columns["MAMH"].HeaderText = "SUBJECT CODE";
                        dgvSchedule.Columns["MAHP"].HeaderText = "COURSE CODE";
                        dgvSchedule.Columns["TENHP"].HeaderText = "COURSE NAME";
                        dgvSchedule.Columns["SOTC"].HeaderText = "CREDIT";
                        dgvSchedule.Columns["STLT"].HeaderText = "THEORY LESSON";
                        dgvSchedule.Columns["STTH"].HeaderText = "PRACTICAL LESSONS";
                        dgvSchedule.Columns["HK"].HeaderText = "TERM";
                        dgvSchedule.Columns["NAMHOC"].HeaderText = "YEAR";
                    }
                }    
                
            }
			catch (Exception ex)
			{
                MessageBox.Show("Error:\n" + ex.Message);
			}
		}

        private void pbReload_Click(object sender, EventArgs e)
        {
            LoadSubject(_manv, _role);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSchedule.CurrentRow == null || dgvSchedule.CurrentRow.Cells["MAMH"].Value == null)
                {
                    MessageBox.Show("Please choose a Subject first!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string courseCode = dgvSchedule.CurrentRow.Cells["MAHP"].Value.ToString();
                ViewSubject subjectDetail = new ViewSubject(courseCode);
                this.Hide();
                subjectDetail.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

		private void notifications_Click(object sender, EventArgs e)
		{

		}
	}
}
