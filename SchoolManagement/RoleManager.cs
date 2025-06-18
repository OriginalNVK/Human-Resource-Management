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
    public partial class RoleManager : KryptonForm
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

        public RoleManager()
        {
            InitializeComponent();
			LoadRoles();
        }

		// Load all role
		private void LoadRoles()
		{
            try
            {
                string rolesQuery = @"SELECT granted_role AS ROLE,
									 LISTAGG(grantee, ', ') WITHIN GROUP (ORDER BY grantee) AS USERS
									 FROM dba_role_privs
									 WHERE (granted_role LIKE 'NV_%' OR granted_role = 'SV' OR granted_role LIKE 'TEST_%')
									 AND grantee IN (SELECT username FROM dba_users)
									 GROUP BY granted_role
									 ORDER BY granted_role";

                using (OracleCommand cmd =  new OracleCommand(rolesQuery, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvUser.DataSource = dt;

                    // Show roles information
                    dgvUser.Columns["ROLE"].HeaderText = "Role";
                    dgvUser.Columns["USERS"].HeaderText = "User";
                }
            }
			catch (Exception ex) 
			{
				MessageBox.Show("Error:\n" + ex.Message);
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

		private void label9_Click(object sender, EventArgs e)
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

		private void pbAddRoles_Click(object sender, EventArgs e)
		{
            // Form for create new role
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "ADD NEW ROLE",
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = "ROLE NAME:" };
            TextBox textBox = new TextBox() { Left = 125, Top = 20, Width = 150 };
            Button confirmation = new Button() { Text = "Add", Left = 60, Width = 80, Top = 70, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 150, Width = 80, Top = 70, DialogResult = DialogResult.Cancel };

            confirmation.Click += (s, ea) => { prompt.Close(); };
            cancel.Click += (s, ea) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);

            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            if (prompt.ShowDialog(this) == DialogResult.OK)
            {
                string roleName = textBox.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(roleName))
                {
                    string query = $"CREATE ROLE {roleName}";
                    try
                    {
                        using (OracleCommand command = new OracleCommand(query, DatabaseSession.Connection))
                        {
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Role created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRoles(); // Load roles again
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error creating role:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a role name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

		private void pbEdit_Click(object sender, EventArgs e)
		{
            try
            {
                // Kiểm tra xem người dùng đã chọn role chưa
                if (dgvUser.CurrentRow == null || dgvUser.CurrentRow.Cells["ROLE"].Value == null)
                {
                    MessageBox.Show("Please choose a role first!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy tên role đã chọn
                string roleName = dgvUser.CurrentRow.Cells["ROLE"].Value.ToString();

                // Hiển thị thông báo xác nhận trước khi chỉnh sửa
                DialogResult result = MessageBox.Show(
                    $"Do you want to edit this role \"{roleName}\"?",
                    "Confirm Edit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // Nếu người dùng chọn Yes, thực hiện chỉnh sửa role
                if (result == DialogResult.Yes)
                {
                    // Tạo form để chỉnh sửa role (có thể là form EditRole hoặc UpdateRole)
                    UpdateRole updateRoleForm = new UpdateRole(roleName);

                    this.Hide();  // Ẩn form hiện tại
                    updateRoleForm.ShowDialog();  // Hiển thị form chỉnh sửa
                    this.Close();  // Sau khi đóng form chỉnh sửa, hiển thị lại form hiện tại

                    LoadRoles();  // Tải lại danh sách roles sau khi chỉnh sửa
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edit role failed:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void pbDelete_Click(object sender, EventArgs e)
		{
            try
            {
                if (dgvUser.CurrentRow == null || dgvUser.CurrentRow.Cells["ROLE"].Value == null)
                {
                    MessageBox.Show("Please choose role first!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string roleName = dgvUser.CurrentRow.Cells["ROLE"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Delete this role \"{roleName}\"?\nYou cannot undo!",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    string dropQuery = $"DROP ROLE {roleName}";

                    using (OracleCommand cmd = new OracleCommand(dropQuery, DatabaseSession.Connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Delete role successfully!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadRoles(); // Reload lại danh sách roles sau khi xóa
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete role failed:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadRolesList(object sender, EventArgs e)
        {
			LoadRoles();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter a role name to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT ROLE FROM DBA_ROLES WHERE ROLE LIKE :search";

            try
            {
                using (OracleCommand command = new OracleCommand(query, DatabaseSession.Connection))
                {
                    command.Parameters.Add(new OracleParameter("search", $"%{searchText}%"));

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No matching roles found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        dgvUser.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching roles:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            AuditView auditView = new AuditView();
            this.Hide();
            auditView.ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void addNoticeBtn_Click(object sender, EventArgs e)
        {
            AddNotification addNotification = new AddNotification();
            this.Hide();
            addNotification.ShowDialog();
            this.Close();
        }

		private void label6_Click(object sender, EventArgs e)
		{
			Backup backup = new Backup();
			this.Hide();
			backup.ShowDialog();
			this.Close();
		}

		private void pictureBox4_Click(object sender, EventArgs e)
		{

		}
	}
}
