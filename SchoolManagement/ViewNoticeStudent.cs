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
	public partial class ViewNoticeStudent : KryptonForm
	{
		public static string _username;
		private string _originalPhoneNumber;    // lưu số điện thoại ban đầu
		private string _changePassword = null;  // gán nếu người dùng muốn thay đổi mật khẩu
		public static string _role = null;

		public ViewNoticeStudent(string username)
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

		private void notifications_Click(object sender, EventArgs e)
		{
			ViewNotice viewNotification = new ViewNotice(Login.ID);
			this.Hide();
			viewNotification.ShowDialog();
			this.Close();
		}

        // Quit button
		private void kryptonButton1_Click(object sender, EventArgs e)
		{

		}
	}
}
