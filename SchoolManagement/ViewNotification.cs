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
	public partial class ViewNotice : KryptonForm
	{
		public static string _username;
		private string _originalPhoneNumber;    // lưu số điện thoại ban đầu
		private string _changePassword = null;  // gán nếu người dùng muốn thay đổi mật khẩu
		public static string _role = null;

		public ViewNotice(string username)
		{
			InitializeComponent();
			_username = username;
			LoadNotification();
		}

        private void LoadNotification()
        {
            try
            {
                string notificationQuery = @"SELECT MATB, ND FROM PDB_ADMIN.QLDH_THONGBAO";

                using (OracleCommand cmd = new OracleCommand(notificationQuery, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvNotification.DataSource = dt;

                    // Cấu hình giao diện
                    dgvNotification.ReadOnly = true;
                    dgvNotification.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvNotification.MultiSelect = false;
                    dgvNotification.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvNotification.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dgvNotification.AllowUserToAddRows = false;
                    dgvNotification.AllowUserToDeleteRows = false;
                    dgvNotification.AllowUserToResizeRows = false;

                    // Đặt tên cột
                    dgvNotification.Columns["MATB"].HeaderText = "CODE";
                    dgvNotification.Columns["ND"].HeaderText = "CONTENT";

                    // Đặt chế độ thủ công
                    dgvNotification.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    // Giả sử tổng chiều rộng là chiều rộng của DataGridView
                    int totalWidth = dgvNotification.Width;

                    // Gán chiều rộng cho cột CODE = 1/5
                    dgvNotification.Columns["MATB"].Width = totalWidth / 5;

                    // Cột CONTENT chiếm phần còn lại (tự động co giãn)
                    dgvNotification.Columns["ND"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


                    // Căn chỉnh cột
                    dgvNotification.Columns["MATB"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvNotification.Columns["ND"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    // Thiết kế tiêu đề cột
                    dgvNotification.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
                    dgvNotification.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvNotification.EnableHeadersVisualStyles = false;
                    dgvNotification.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
                    dgvNotification.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    // Highlight khi chọn
                    dgvNotification.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
                    dgvNotification.DefaultCellStyle.SelectionForeColor = Color.Black;

                    // Font chung
                    dgvNotification.DefaultCellStyle.Font = new Font("Century Gothic", 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        private void pbLogout_Click(object sender, EventArgs e)
		{
			LogOut();
		}

		private void LogOut()
		{
			Login login = new Login();
			this.Hide();
			login.ShowDialog();
			this.Close();
		}

		private void pbProfile_Click(object sender, EventArgs e)
		{
			PersonnelMenu personnelMenu = new PersonnelMenu(Login.ID);
			this.Hide();
			personnelMenu.ShowDialog();
			this.Close();
		}

		private void pbStudents_Click(object sender, EventArgs e)
		{
			StudentManager studentManager = new StudentManager();
			this.Hide();
			studentManager.ShowDialog();
			this.Close();
		}

		private void pbPersonnel_Click(object sender, EventArgs e)
		{
			PersonnelManager personnelManager = new PersonnelManager();
			this.Hide();
			personnelManager.ShowDialog();
			this.Close();
		}

		private void pbClass_Click(object sender, EventArgs e)
		{
			ClassList classList = new ClassList();
			this.Hide();
			classList.ShowDialog();
			this.Close();
		}

		private void pbUsers_Click(object sender, EventArgs e)
		{
			UsersManager userManager = new UsersManager();
			this.Hide();
			userManager.ShowDialog();
			this.Close();
		}

        private void pbSubject_Click(object sender, EventArgs e)
        {
			SubjectManagement subjectManager = new SubjectManagement();
			this.Hide();
			subjectManager.ShowDialog();
			this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            PersonnelRegister personnelRegister = new PersonnelRegister();
            this.Hide();
            personnelRegister.ShowDialog();
            this.Close();
        }

        private void view_Schedule(object sender, EventArgs e)
        {
			ViewSchedule viewDetail = new ViewSchedule(_username, _role);
			this.Hide();
			viewDetail.ShowDialog();
			this.Close();
        }

		private void notifications_Click(object sender, EventArgs e)
		{
			ViewNotice viewNotification = new ViewNotice(Login.ID);
			this.Hide();
			viewNotification.ShowDialog();
			this.Close();
		}

		private void label8_Click(object sender, EventArgs e)
		{

		}
	}
}
