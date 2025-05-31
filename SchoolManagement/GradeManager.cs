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
	public partial class GradeManager : KryptonForm
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

		public GradeManager()
		{
			InitializeComponent();
			lbHello.Text = $"Hello, {Login.ID}";
			string loginId = Login.ID;
			LoadSubjects();
		}

		private void LoadSubjects()
		{
			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				string magv = Login.ID; 

				string sql = @"
					SELECT MAHP, TENHP, HK, NAM 
					FROM PDB_ADMIN.QLDH_VIEW_SUBJECTS_BY_GV
					ORDER BY NAM DESC, HK DESC
				";

				OracleCommand cmd = new OracleCommand(sql, conn);

				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				DataRow allSubjectsRow = dt.NewRow();
				allSubjectsRow["MAHP"] = "NONE";
				allSubjectsRow["TENHP"] = "Chọn môn học";
				dt.Rows.InsertAt(allSubjectsRow, 0);

				comboBox1.DataSource = dt;
				comboBox1.DisplayMember = "TENHP";
				comboBox1.ValueMember = "MAHP";
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi load danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//viết hàm xử lý sự kiện khi người dùng chọn môn học rồi nhấn nút tìm kiếm 
		private void btnSearch_Click(object sender, EventArgs e)
		{
			if (comboBox1.SelectedValue != null && comboBox1.SelectedValue.ToString() != "NONE")
			{
				// Kiểm tra xem người dùng đã chọn môn học hay chưa
				if (comboBox1.SelectedIndex == 0)
				{
					MessageBox.Show("Vui lòng chọn một môn học để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				// Lấy giá trị môn học đã chọn
				string selectedSubject = comboBox1.SelectedValue.ToString();
				// Thực hiện tìm kiếm theo môn học đã chọn
				LoadScoreBoard(selectedSubject);
			}
			else
			{
				MessageBox.Show("Vui lòng chọn một môn học để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void LoadScoreBoard(string subjectId)
		{
			try
			{
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Không thể kết nối cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				      string sql = @"
						SELECT 
							MASV as ""MÃ SV"",
							HOTEN as ""HỌ TÊN"",
							DIEMTH as ""ĐIỂM TH"",
							DIEMQT as ""ĐIỂM QT"",
							DIEMCK as ""ĐIỂM CK"",
							DIEMTK as ""ĐIỂM TK""
						FROM PDB_ADMIN.QLDH_VIEW_SCOREBOARD
						WHERE MAHP = :subjectId
						ORDER BY MASV
					";

				OracleCommand cmd = new OracleCommand(sql, conn);
				cmd.Parameters.Add(new OracleParameter("subjectId", subjectId));

				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				dgvScoreBoard.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi load bảng điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }
    }
}
