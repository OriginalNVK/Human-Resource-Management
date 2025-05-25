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
			MessageBox.Show($"Login ID: {Login.ID}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
			lbHello.Text = $"Hello, {Login.ID}";
			string loginId = Login.ID;

			LoadStudents();
		}

		private void LoadStudents()
		{
			try
			{
				string userQuery = @"
				SELECT * FROM PDB_ADMIN.QLDH_SINHVIEN
				ORDER BY HOTEN";


				using (OracleCommand cmd = new OracleCommand(userQuery, DatabaseSession.Connection))
				using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
				{
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					// Tìm role của từng user
					foreach (DataRow row in dt.Rows)
					{
						string username = row["MASV"].ToString();
						string name = row["HOTEN"].ToString();
						string status = row["TINHTRANG"].ToString();
					}

					// Cập nhật lưới
					dgvStudent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
					dgvStudent.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

					dgvStudent.DataSource = null;
					dgvStudent.Columns.Clear();

					// Thêm cột checkbox
					DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
					{
						Name = "chk",
						HeaderText = "",
						Width = 40,
						ReadOnly = false
					};
					dgvStudent.Columns.Add(chk);

					// Cột Ten và MaSV
					dgvStudent.Columns.Add("Ten", "Ten");
					dgvStudent.Columns.Add("MaSV", "MaSV");
					dgvStudent.Columns.Add("Status", "Status");

					foreach (DataRow row in dt.Rows)
					{
						int index = dgvStudent.Rows.Add();
						dgvStudent.Rows[index].Cells["Ten"].Value = row["HOTEN"];
						dgvStudent.Rows[index].Cells["MaSV"].Value = row["MASV"];
						dgvStudent.Rows[index].Cells["Status"].Value = row["TINHTRANG"];
						dgvStudent.Rows[index].Cells["chk"].Value = false;
					}

					dgvStudent.RowHeadersVisible = false;
					dgvStudent.AllowUserToAddRows = false;
					dgvStudent.MultiSelect = false;
					dgvStudent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					dgvStudent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

					dgvStudent.CellClick -= DgvStudent_CellClick;
					dgvStudent.CellClick += DgvStudent_CellClick;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi tải danh sách SINHVIEN Oracle:\n" + ex.Message);
			}
		}

		private void pbNext_Click(object sender, EventArgs e)
		{
			currFrom++;
		}

		private void pbPrev_Click(object sender, EventArgs e)
		{
			if (currFrom > 1)
			{
				currFrom--;
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

		private void label4_Click(object sender, EventArgs e)
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}

		private void DgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Chỉ xử lý click vào cột checkbox

			// Toggle trạng thái checkbox
			bool currentValue = Convert.ToBoolean(dgvStudent.Rows[e.RowIndex].Cells["chk"].Value ?? false);
			dgvStudent.Rows[e.RowIndex].Cells["chk"].Value = !currentValue;

			// Bỏ chọn các dòng khác
			foreach (DataGridViewRow row in dgvStudent.Rows)
			{
				if (row.Index != e.RowIndex)
				{
					row.Cells["chk"].Value = false;
				}
			}
		}
		private void dgvClass_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				isSelected = true;
				//showAction();

				//DataGridViewRow row = dgvClass.Rows[e.RowIndex];
				//txtID.Text = row.Cells[0].Value.ToString();
				//cbSubject.Text = row.Cells[1].Value.ToString();
				//cbTeacher.Text = row.Cells[2].Value.ToString();

				//txtSchedule.Text = row.Cells[3].Value.ToString();
				//txtNOS.Text = row.Cells[4].Value.ToString();

				//ClassSectionID = txtID.Text;
				//SubjectID = cbSubject.Text;
				//limited = Int32.Parse(txtNOS.Text);
			}
		}
		private void dgvClass_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
		private void pbEdit_Click(object sender, EventArgs e)
		{
			var selectedRow = dgvStudent.Rows.Cast<DataGridViewRow>()
				.FirstOrDefault(row => row.Cells["chk"].Value != null &&
									 Convert.ToBoolean(row.Cells["chk"].Value));

			if (selectedRow != null)
			{
				UpdateStudent updateForm = new UpdateStudent(
					selectedRow.Cells["MaSV"].Value.ToString(),
					selectedRow.Cells["Status"].Value.ToString()
					);

				this.Hide();
				updateForm.ShowDialog();
				this.Close();
			}
			else
			{
				KryptonMessageBox.Show("Pls choose a student to edit",
									 "Warning",
									 MessageBoxButtons.OK,
									 MessageBoxIcon.Warning);
			}
		}
	}
}
