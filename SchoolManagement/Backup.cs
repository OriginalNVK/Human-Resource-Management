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
    public partial class Backup : KryptonForm
    {
        public Backup()
        {
            InitializeComponent();
			InitializeDataGridView();
			LoadBackupHistory();
		}

		private void InitializeDataGridView()
		{
			dataGridView1.Columns.Clear();
			dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
			{
				HeaderText = "",
				Width = 30,
				Name = "colSelect"
			});

			dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "Backup Time",
				Width = 250,
				Name = "colBackupTime"
			});
		}

		private void LoadBackupHistory()
		{
			dataGridView1.Rows.Clear();
			var history = BackupHelper.GetBackupLog();
			foreach (var entry in history)
			{
				dataGridView1.Rows.Add(false, entry);
			}
		}

		private string GetSelectedBackup()
		{
			foreach (DataGridViewRow row in dataGridView1.Rows)
			{
				if ((bool)row.Cells[0].Value)
				{
					return row.Cells[1].Value.ToString();
				}
			}
			return null;
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
            AdminProfile myProfile = new AdminProfile();
            this.Hide();
            myProfile.ShowDialog();
            this.Close();
        }

        private void pbRole_Click(object sender, EventArgs e)
        {
            RoleManager roleManager = new RoleManager();
            this.Hide();
			roleManager.ShowDialog();
            this.Close();
        }

        private void pbUsers_Click(object sender, EventArgs e)
        {
            UsersManager userManager = new UsersManager();
            this.Hide();
			userManager.ShowDialog();
            this.Close();
        }

		private void notifications_Click(object sender, EventArgs e)
		{

		}

        private void label5_Click(object sender, EventArgs e)
        {
            AuditView auditView = new AuditView();
            this.Hide();
            auditView.ShowDialog();
            this.Close();
        }

        private void addNoticeBtn_Click(object sender, EventArgs e)
        {
			AddNotification addNotification = new AddNotification();
            this.Hide();
            addNotification.ShowDialog();
			this.Close();
        }

		private void btnBackup_Click(object sender, EventArgs e)
		{
			string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
			if (BackupHelper.PerformBackup(timestamp))
			{
				MessageBox.Show("Backup thành công!");
				BackupHelper.AppendBackupLog(timestamp);
				LoadBackupHistory();
			}
			else
			{
				MessageBox.Show("Backup thất bại.");
			}
		}

		private void btnRevert_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedBackup();
			if (selected == null)
			{
				MessageBox.Show("Vui lòng chọn một bản backup để khôi phục.");
				return;
			}

			// Parse timestamp từ chuỗi (nếu đúng định dạng yyyyMMdd_HHmmss)
			if (!DateTime.TryParseExact(selected, "yyyyMMdd_HHmmss", null, System.Globalization.DateTimeStyles.None, out DateTime untilTime))
			{
				MessageBox.Show("Thời gian backup không hợp lệ.");
				return;
			}

			var confirm = MessageBox.Show($"Bạn có chắc muốn khôi phục bản backup lúc {untilTime}?", "Xác nhận", MessageBoxButtons.YesNo);
			if (confirm == DialogResult.Yes)
			{
				bool success = BackupHelper.PerformRestore(selected, untilTime);
				MessageBox.Show(success ? "Khôi phục thành công!" : "Khôi phục thất bại.");
			}
		}
	}
}
