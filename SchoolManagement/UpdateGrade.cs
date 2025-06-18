using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Oracle.ManagedDataAccess.Client;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace SchoolManagement
{
	public partial class UpdateGrade : KryptonForm
	{
		private const int CS_DropShadow = 0x00020000;
	
		private string _mainStatus;

        private string v;

		private string maMH;

        public UpdateGrade(string subjectId, string username)
		{
            InitializeComponent(); // Add this line first
            LoadInformation(subjectId, username);
		}

        private void LoadInformation(string subjectId, string username)
		{
			try
			{
				// Lấy connection đã được mở ở login
				OracleConnection conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Kết nối chưa khởi tạo hoặc chưa mở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Debug connection
				MessageBox.Show($"Database: {conn.DatabaseName}\nDataSource: {conn.DataSource}", "Connection Debug");

				string query = @"
						SELECT 
							MASV,
							HOTEN,
							DIEMTH,
							DIEMQT,
							DIEMCK,
							DIEMTK,
							MAMH
						FROM PDB_ADMIN.QLDH_VIEW_SCOREBOARD1 
						WHERE MASV = :username 
						AND MAHP = :subjectId";

				// Debug parameters
				MessageBox.Show($"Parameters:\nMASV = {username}\nMAHP = {subjectId}", "Parameter Debug");

				using (var cmd = new OracleCommand(query, conn))
				{
					// Add parameters in correct order as they appear in query
					cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username.Trim().ToUpper();
					cmd.Parameters.Add("subjectId", OracleDbType.Varchar2).Value = subjectId.Trim().ToUpper();

					// Debug full query
					string debugQuery = query.Replace(":username", $"'{username.Trim().ToUpper()}'")
								   .Replace(":subjectId", $"'{subjectId.Trim().ToUpper()}'");
					MessageBox.Show($"Executing query:\n{debugQuery}", "Query Debug");

					using (var reader = cmd.ExecuteReader())
					{
						// Debug result set
						if (reader.HasRows)
						{
							MessageBox.Show("Query returned results", "Query Debug");
						}
						else
						{
							MessageBox.Show("Query returned no results", "Query Debug");
						}

						if (!reader.Read())
						{
							MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						txtUsername.Text = username.ToUpper();
						// Các thông tin profile
						txtFullname.Text = reader.GetString(reader.GetOrdinal("HOTEN"));

						txtMaMH.Text = subjectId.ToUpper();
						// Các thông tin điểm
						if (!reader.IsDBNull(reader.GetOrdinal("DIEMTH")))
						{
							txtDiemTH.Text = reader.GetDecimal(reader.GetOrdinal("DIEMTH")).ToString();
						}
						else
						{
							txtDiemTH.Text = "0";
						}
						if (!reader.IsDBNull(reader.GetOrdinal("DIEMQT")))
						{
							txtDiemQT.Text = reader.GetDecimal(reader.GetOrdinal("DIEMQT")).ToString();
						}
						else
						{
							txtDiemQT.Text = "0";
						}
						if (!reader.IsDBNull(reader.GetOrdinal("DIEMCK")))
						{
							txtDiemCK.Text = reader.GetDecimal(reader.GetOrdinal("DIEMCK")).ToString();
						}
						else
						{
							txtDiemCK.Text = "0";
						}
						if (!reader.IsDBNull(reader.GetOrdinal("DIEMTK")))
						{
							txtDiemTK.Text = reader.GetDecimal(reader.GetOrdinal("DIEMTK")).ToString();
						}
						else
						{
							txtDiemTK.Text = "0";
						}

						maMH = reader.GetString(reader.GetOrdinal("MAMH")).ToUpper();
					}
				}
			}
			catch (OracleException oex)
			{
				MessageBox.Show($"Oracle Error:\nCode: {oex.Number}\nMessage: {oex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"System Error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnUpdateGrade_Click(object sender, EventArgs e)
		{
			OracleConnection conn = null;
			try
			{
				string username = txtUsername.Text.Trim().ToUpper();
				string mamh = txtMaMH.Text.Trim().ToUpper();

				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(mamh))
				{
					MessageBox.Show("Vui lòng điền đầy đủ thông tin Mã sinh viên và Mã môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				if (!decimal.TryParse(txtDiemTH.Text, out decimal diemTh) ||
					!decimal.TryParse(txtDiemQT.Text, out decimal diemQt) ||
					!decimal.TryParse(txtDiemCK.Text, out decimal diemCk) ||
					!decimal.TryParse(txtDiemTK.Text, out decimal diemTk))
				{
					MessageBox.Show("Vui lòng nhập đúng định dạng điểm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Add this validation in btnUpdateGrade_Click before the update
				if (diemTh < 0 || diemTh > 10 ||
					diemQt < 0 || diemQt > 10 ||
					diemCk < 0 || diemCk > 10 ||
					diemTk < 0 || diemTk > 10)
				{
					MessageBox.Show("Điểm phải nằm trong khoảng từ 0 đến 10.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				conn = DatabaseSession.Connection;
				if (conn == null || conn.State != ConnectionState.Open)
				{
					MessageBox.Show("Kết nối chưa khởi tạo hoặc chưa mở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				string updateQuery = @"
            UPDATE PDB_ADMIN.QLDH_DANGKY 
            SET DIEMTH = :diemTh, DIEMQT = :diemQt, DIEMCK = :diemCk, DIEMTK = :diemTk
            WHERE MASV = :username AND MAMH = :mamh";

				using (OracleCommand cmd = new OracleCommand(updateQuery, conn))
				{
					cmd.Parameters.Add("diemTh", OracleDbType.Decimal).Value = diemTh;
					cmd.Parameters.Add("diemQt", OracleDbType.Decimal).Value = diemQt;
					cmd.Parameters.Add("diemCk", OracleDbType.Decimal).Value = diemCk;
					cmd.Parameters.Add("diemTk", OracleDbType.Decimal).Value = diemTk;
					cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
					cmd.Parameters.Add("mamh", OracleDbType.Varchar2).Value = maMH;

					int rowsAffected = cmd.ExecuteNonQuery();

					if (rowsAffected > 0)
					{
						MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.DialogResult = DialogResult.OK;
						this.Close();
					}
					else
					{
						MessageBox.Show("Không tìm thấy thông tin để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	
		private void DgvGrade_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Chỉ xử lý click vào cột checkbox

			// Toggle trạng thái checkbox
			bool currentValue = Convert.ToBoolean(dgvGrade.Rows[e.RowIndex].Cells["chk"].Value ?? false);
			dgvGrade.Rows[e.RowIndex].Cells["chk"].Value = !currentValue;

			// Bỏ chọn các dòng khác
			foreach (DataGridViewRow row in dgvGrade.Rows)
			{
				if (row.Index != e.RowIndex)
				{
					row.Cells["chk"].Value = false;
				}
			}
		}
		private void lbUsers_Click(object sender, EventArgs e)
		{
			UsersManager userManager = new UsersManager();
			this.Hide();
			userManager.ShowDialog();
			this.Close();
		}

		private void lbRoles_Click(object sender, EventArgs e)
		{
			RoleManager roleManager = new RoleManager();
			this.Hide();
			roleManager.ShowDialog();
			this.Close();
		}

		private void lbStudents_Click(object sender, EventArgs e)
		{
			StudentManager student = new StudentManager();
			this.Hide();
			student.ShowDialog();
			this.Close();
		}

		private void lbPersonnel_Click(object sender, EventArgs e)
		{
			PersonnelManager personnelManager = new PersonnelManager();
			this.Hide();
			personnelManager.ShowDialog();
			this.Close();
		}

		private void lbProfile_Click(object sender, EventArgs e)
		{
			AdminProfile myProfile = new AdminProfile();
			this.Hide();
			myProfile.ShowDialog();
			this.Close();
		}

		private void label6_Click(object sender, EventArgs e)
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}

		private void label3_Click(object sender, EventArgs e)
		{
			GradeManager gradeManager = new GradeManager();
			this.Hide();
			gradeManager.ShowDialog();
			this.Close();
		}
	}
}
