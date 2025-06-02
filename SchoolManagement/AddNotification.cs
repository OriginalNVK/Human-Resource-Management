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
    public partial class AddNotification : KryptonForm
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
		public AddNotification()
        {
            InitializeComponent();
            getAllRoles();
        }

        private void getAllRoles()
        {
            try
            {
                string facilityQuery = @"SELECT * FROM DBA_ROLES
                                         WHERE ROLE LIKE 'NV%' OR ROLE = 'SV'";

                using (OracleCommand cmd = new OracleCommand(facilityQuery, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    roleList.DataSource = dt;
                    roleList.DisplayMember = "ROLE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }
        }

        private void addNoticeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string content = txtContent.Text.Trim();

                if (string.IsNullOrEmpty(content))
                {
                    MessageBox.Show("Vui lòng nhập nội dung thông báo.");
                    return;
                }

                string role = roleList.Text.ToString();
                if (string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Vui lòng chọn vai trò.");
                    return;
                }

                // Lấy các khoa/phòng được chọn
                List<string> facilityList = new List<string>();
                if (mathCheck.Checked) facilityList.Add("TOAN");
                if (physicCheck.Checked) facilityList.Add("LY");
                if (chemistryCheck.Checked) facilityList.Add("HOA");
                if (administrativeCheck.Checked) facilityList.Add("HANHCHINH");

                if (facilityList.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một đơn vị (khoa/phòng).");
                    return;
                }

                // Lấy cơ sở được chọn
                List<string> locationList = new List<string>();
                if (location1Check.Checked) locationList.Add("CS1");
                if (location2Check.Checked) locationList.Add("CS2");

                if (locationList.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một cơ sở.");
                    return;
                }

                string roleParam = "";
                if (role == "SV") roleParam = "SV";
                else if (role == "NV_TRGDV") roleParam = "TRGDV";
                else roleParam = "NV";

                // Tạo nhãn (label)
                string label = roleParam + ":" + string.Join(",", facilityList) + ":" + string.Join(",", locationList);

                string noticeQuery = @"INSERT INTO pdb_admin.QLDH_THONGBAO(ND, NOTICE_POLICY_COL) VALUES (
                                  :cont, 
                                   CHAR_TO_LABEL('NOTICE_POLICY', :label))";
                using (OracleCommand cmd = new OracleCommand(noticeQuery, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(new OracleParameter("cont", content));
                    cmd.Parameters.Add(new OracleParameter("label", label));
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Add noctice successfully");

            }
            catch (Exception err)
            {
                MessageBox.Show("Error:\n" + err.Message);
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

        private void label8_Click(object sender, EventArgs e)
        {
            AuditView auditView = new AuditView();
            this.Hide();
            auditView.ShowDialog();
            this.Close();
        }

        private void labelAddNotice_Click(object sender, EventArgs e)
        {
            AddNotification addNotification = new AddNotification();
            this.Hide(); 
            addNotification.ShowDialog(); 
            this.Close();
        }
    }
}
