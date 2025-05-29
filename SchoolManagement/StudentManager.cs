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
	public partial class StudentManager : KryptonForm
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

		public StudentManager()
		{
			InitializeComponent();
			lbHello.Text = $"Hello, {Login.ID}";
			string loginId = Login.ID;

			LoadStudents();
		}

		private void LoadStudents()
		{
			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Lấy MAGV hiện tại từ đăng nhập
				string magv = Login.ID; // Đây là MANV của giáo viên

				// Truy vấn từ VIEW V_STUDENTS_BY_GV
				string sql = @"
				SELECT TENHP, MASV, HOTEN, PHAI, NGSINH, DCHI, DT, TINHTRANG, DIEMTH, DIEMQT, DIEMCK, DIEMTK
				FROM PDB_ADMIN.V_STUDENTS_BY_GV
				";

				OracleCommand cmd = new OracleCommand(sql, conn);

				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				// Đổi tên cột hiển thị
				dt.Columns["TENHP"].ColumnName = "TÊN HỌC PHẦN";
				dt.Columns["MASV"].ColumnName = "MÃ SV";
				dt.Columns["HOTEN"].ColumnName = "HỌ TÊN";
				dt.Columns["PHAI"].ColumnName = "PHÁI";
				dt.Columns["NGSINH"].ColumnName = "NGÀY SINH";
				dt.Columns["DCHI"].ColumnName = "ĐỊA CHỈ";
				dt.Columns["DT"].ColumnName = "ĐIỆN THOẠI";
				dt.Columns["TINHTRANG"].ColumnName = "TÌNH TRẠNG";
				dt.Columns["DIEMTH"].ColumnName = "ĐIỂM TH";
				dt.Columns["DIEMQT"].ColumnName = "ĐIỂM QT";
				dt.Columns["DIEMCK"].ColumnName = "ĐIỂM CK";
				dt.Columns["DIEMTK"].ColumnName = "ĐIỂM TK";

				dgvStudents.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi load danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
