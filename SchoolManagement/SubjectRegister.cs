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
    public partial class SubjectRegister : KryptonForm
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
        public SubjectRegister()
        {
            InitializeComponent();
            LoadSubject();
        }
		private void LoadSubject()
		{
			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Failed to connect with database!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Xác định học kỳ hiện tại
				int month = DateTime.Now.Month;
				string currentHK;
				if (month >= 8 && month <= 12)
					currentHK = "1"; // HK1
				else if (month >= 1 && month <= 5)
					currentHK = "2"; // HK2
				else
					currentHK = "3"; // HK hè

				// Xác định năm học phù hợp với học kỳ
				int currentYear = DateTime.Now.Year;

				// Ngày bắt đầu đăng ký cho từng học kỳ
				var hkStartDates = new Dictionary<string, DateTime>
		{
			{ "1", new DateTime(currentYear, 8, 10) },   // HK1: 10/08
			{ "2", new DateTime(currentYear, 1, 04) },    // HK2: 04/01
			{ "3", new DateTime(currentYear, 6, 10) }    // HK hè: 10/06
		};

				DateTime startDate = hkStartDates[currentHK];
				if ((DateTime.Now - startDate).TotalDays > 14)
				{
					MessageBox.Show("Không có môn học mở nào, thời gian đăng ký môn không phải hôm nay", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
					dgvSubjects.DataSource = null; // Xóa dữ liệu cũ
					return;
				}

				// Truy vấn các môn học chưa đăng ký và thuộc kỳ hiện tại
				string sql = @"
			SELECT MAMH, TENHP, SOTC, HOTEN, HK, NAM
			FROM PDB_ADMIN.QLDH_VIEW_COURSE_INFO_SV
			WHERE MAMH NOT IN (
				SELECT MAMH 
				FROM PDB_ADMIN.QLDH_DANGKY
				WHERE MASV = SYS_CONTEXT('USERENV','SESSION_USER')
			)
			AND HK = :hk AND NAM = :nam";

				OracleCommand cmd = new OracleCommand(sql, conn);
				cmd.Parameters.Add(":hk", OracleDbType.Varchar2).Value = currentHK;
				cmd.Parameters.Add(":nam", OracleDbType.Int32).Value = currentYear - 1 ;

				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				// Thêm cột checkbox nếu chưa có
				if (!dgvSubjects.Columns.Contains("Chon"))
				{
					DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
					chk.HeaderText = "Chọn";
					chk.Name = "Chon";
					chk.Width = 50;
					dgvSubjects.Columns.Insert(0, chk);
				}

				// Đặt tên hiển thị cho cột
				dt.Columns["MAMH"].ColumnName = "MÃ MÔN HỌC";
				dt.Columns["TENHP"].ColumnName = "TÊN HỌC PHẦN";
				dt.Columns["SOTC"].ColumnName = "SỐ TÍN CHỈ";
				dt.Columns["HOTEN"].ColumnName = "GIẢNG VIÊN";
				dt.Columns["HK"].ColumnName = "HỌC KÌ";
				dt.Columns["NAM"].ColumnName = "NĂM";

				dgvSubjects.DataSource = dt;
				dgvSubjects.Columns["Chon"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
				dgvSubjects.Columns["Chon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi load môn học mở: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}




		private void btnSave_Click(object sender, EventArgs e)
        {
			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Failed to connect with database!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				int count = 0;

				// Ngày bắt đầu đăng ký cho từng học kỳ
				var hkStartDates = new Dictionary<string, DateTime>
				{
					{ "1", new DateTime(DateTime.Now.Year, 8, 10) },   // HK1: 10/08
				    { "2", new DateTime(DateTime.Now.Year, 1, 4) },    // HK2: 04/01
				    { "3", new DateTime(DateTime.Now.Year, 6, 10) }    // HK hè: 10/06
				};

				foreach (DataGridViewRow row in dgvSubjects.Rows)
				{
					bool isSelected = Convert.ToBoolean(row.Cells["Chon"].Value);
					if (!isSelected) continue;

					string maMH = row.Cells["MÃ MÔN HỌC"].Value?.ToString();
					string hk = row.Cells["HỌC KÌ"].Value?.ToString();

					if (string.IsNullOrEmpty(maMH) || string.IsNullOrEmpty(hk) || !hkStartDates.ContainsKey(hk))
						continue;

					DateTime startDate = hkStartDates[hk];
					TimeSpan elapsed = DateTime.Now - startDate;

					if (elapsed.TotalDays <= 14 && elapsed.TotalDays >= 0) // Chỉ trong vòng 14 ngày
					{
						string insertSql = @"INSERT INTO pdb_admin.QLDH_DANGKY (MASV, MAMH, NGAYDK) 
                                     VALUES (:masv, :mamh, :ngaydk)";

						OracleCommand insertCmd = new OracleCommand(insertSql, conn);
						insertCmd.Parameters.Add(":masv", Login.ID);
						insertCmd.Parameters.Add(":mamh", maMH);
						insertCmd.Parameters.Add(":ngaydk", DateTime.Now);

						insertCmd.ExecuteNonQuery();
						count++;
					}
				}

				if (count > 0)
				{
					MessageBox.Show($"Đăng ký thành công {count} môn học.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Không có môn học nào được đăng ký (hết hạn hoặc chưa chọn).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi đăng ký môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
			StudentMenu studentMenu = new StudentMenu();
			this.Hide();
			studentMenu.ShowDialog();
			this.Close();
        }

        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {	

        }
    }
}
