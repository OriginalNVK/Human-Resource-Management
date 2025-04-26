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

namespace SchoolManagement
{
	public partial class UsersManager : KryptonForm
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
		public UsersManager()
		{
			InitializeComponent();
			LoadUsers();
		}
		private void LoadUsers()
		{
			try
			{
				string oradb = ConfigurationManager
					.ConnectionStrings["SchoolDB"]
					.ConnectionString;

				using (OracleConnection conn = new OracleConnection(oradb))
				{
					conn.Open();

					string query = "SELECT MATK, CHUCVU FROM SYS.TAIKHOAN";

					OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					dgvUser.DataSource = dt;

					// Tùy chỉnh hiển thị nếu muốn
					dgvUser.Columns["MATK"].HeaderText = "Username";
					dgvUser.Columns["CHUCVU"].HeaderText = "Role";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi tải dữ liệu người dùng:\n" + ex.Message);
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

		private void pbStudents_Click(object sender, EventArgs e)
		{
			AddUser addUser = new AddUser();
			this.Hide();
			addUser.ShowDialog();
			this.Close();
		}

		private void pbEdit_Click(object sender, EventArgs e)
		{

			if (!isSelected)
			{
				MessageBox.Show("Please choose class to edit!");
				return;
			}
			action = 1;
			pbAddUsers.Visible = false;
			lbStudents.Visible = false;
			pbEdit.Visible = false;
			lbEdit.Visible = false;
			pbDelete.Visible = false;
		}

		private void pbDelete_Click(object sender, EventArgs e)
		{

		}

		private void pbNext_Click(object sender, EventArgs e)
		{

			currFrom++;
			//LoadClasses();
		}

		private void pbPrev_Click(object sender, EventArgs e)
		{

			if (currFrom > 1)
			{
				currFrom--;
				//LoadClasses();
			}
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{

		}

		private void UsersManager_Load(object sender, EventArgs e)
		{

		}

		private void Dashboard_Click(object sender, EventArgs e)
		{

		}

		private void lbHello_Click(object sender, EventArgs e)
		{

		}

		private void dgvClass_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		public void RefreshUserList()
		{
			LoadUsers(); // Gọi private method
		}

		private void lbUsers_Click(object sender, EventArgs e)
		{
			UsersManager userManager = new UsersManager();
			this.Hide();
			userManager.ShowDialog();
			this.Close();
		}

		private void lbRole_Click(object sender, EventArgs e)
		{
			RoleManager roleManager = new RoleManager();
			this.Hide();
			roleManager.ShowDialog();
			this.Close();
		}

		private void lbStudent_Click(object sender, EventArgs e)
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
	}
}
