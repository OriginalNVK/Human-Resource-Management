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
    public partial class RegisteredSubject : KryptonForm
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
        public RegisteredSubject()
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

				// Truy vấn trực tiếp từ view
				string sql = @"SELECT MAMH, TENHP, SOTC, GIANGVIEN, HK, NAM FROM PDB_ADMIN.QLDH_VIEW_REGISTERED_INFO_SV";

				// Nếu bạn muốn lọc theo học kỳ + năm học:
				// int selectedHK = 1;
				// int selectedNam = 2025;
				// sql += " WHERE HK = :hk AND NAM = :nam";

				OracleCommand cmd = new OracleCommand(sql, conn);

				// Nếu lọc theo HK + NAM thì truyền tham số như sau:
				// cmd.Parameters.Add(":hk", OracleDbType.Int32).Value = selectedHK;
				// cmd.Parameters.Add(":nam", OracleDbType.Int32).Value = selectedNam;

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
					dgvSubjects.Columns.Insert(0, chk); // Thêm vào vị trí đầu tiên
				}	
				dt.Columns["MAMH"].ColumnName = "MÃ MÔN HỌC";
				dt.Columns["TENHP"].ColumnName = "TÊN HỌC PHẦN";
				dt.Columns["SOTC"].ColumnName = "SỐ TÍN CHỈ";
				dt.Columns["GIANGVIEN"].ColumnName = "GIẢNG VIÊN";
				dt.Columns["HK"].ColumnName = "HỌC KÌ";
				dt.Columns["NAM"].ColumnName = "NĂM";

				dgvSubjects.DataSource = dt;
				// Tùy chọn: căn giữa tiêu đề và nội dung checkbox
				dgvSubjects.Columns["Chon"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
				dgvSubjects.Columns["Chon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi load môn học đã đăng ký: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
						string deleteSql = @"DELETE FROM PDB_ADMIN.QLDH_DANGKY WHERE MAMH = :mamh AND MASV = :masv";

						OracleCommand deleteCmd = new OracleCommand(deleteSql, conn);
						deleteCmd.Parameters.Add(":mamh", maMH);
						deleteCmd.Parameters.Add(":masv", Login.ID);

						int rowsAffected = deleteCmd.ExecuteNonQuery();
						if (rowsAffected > 0)
							count++;
					}
				}

				if (count > 0)
				{
					MessageBox.Show($"Hủy đăng ký thành công {count} môn học.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Không có môn học nào bị hủy do hết hạn hoặc chưa chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				StudentMenu studentMenu = new StudentMenu();
				this.Hide();
				studentMenu.ShowDialog();
				this.Close();
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
