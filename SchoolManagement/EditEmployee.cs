﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Oracle.ManagedDataAccess.Client;

namespace SchoolManagement
{
    public partial class EditEmployee : KryptonForm
    {
        public EditEmployee(string manv)
        {
            InitializeComponent();
            LoadEmployee(manv);
        }
   
        private void LoadEmployee(string manv)
        {
            try
            {
                string query = "SELECT MANV, HOTEN, NGSINH, PHAI, DCHI, DT, LUONG, PHUCAP, VAITRO, MADV FROM PDB_ADMIN.QLDH_NHANVIEN WHERE MANV = :manv";
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(":manv", OracleDbType.Varchar2).Value = manv;
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtID.Text = reader["MANV"].ToString();
                            txtName.Text = reader["HOTEN"].ToString();
                            // Fix for the CS1061 error in the following line:
                            // dtpBirth.Value = reader["NGSINH"].Value.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
    
                            // Updated code:
                            dtpBirth.Value = Convert.ToDateTime(reader["NGSINH"]); ;
                           // dtpBirth.Value = reader["NGSINH"].Value.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture); // Ensure NGSINH is not null
                            cmbGender.SelectedItem = reader["PHAI"].ToString();
                            txtAddress.Text = reader["DCHI"].ToString();
                            txtPhone.Text = reader["DT"].ToString();
                            cmbRole.SelectedItem = reader["VAITRO"].ToString();
                            cmbDepartment.Items.Clear();
                            getDepartment(); // Load departments into the combo box
                                             // Set the selected department based on MADV
                            cmbDepartment.SelectedValue = getDepartmentName(reader["MADV"].ToString());
                            txtSalary.Text = reader["LUONG"].ToString();
                            txtBonus.Text = reader["PHUCAP"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên với mã: " + manv, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void lbClasses_Click(object sender, EventArgs e)
        {
            SubjectManagement subjectManager = new SubjectManagement();
            this.Hide();
            subjectManager.ShowDialog();
            this.Close();
        }

        private void lbStudents_Click(object sender, EventArgs e)
        {
            StudentManager studentManager = new StudentManager();
            this.Hide();
            studentManager.ShowDialog();
            this.Close();
        }

        private void lbTeachers_Click(object sender, EventArgs e)
        {
            PersonnelManager personnelManager = new PersonnelManager();
            this.Hide();
            personnelManager.ShowDialog();
            this.Close();
        }

        private void lbProfile_Click(object sender, EventArgs e)
        {
            PersonnelMenu myProfile = new PersonnelMenu(Login.ID);
            this.Hide();
            myProfile.ShowDialog();
            this.Close();

        }

        private void label2_Click(object sender, EventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string manv = txtID.Text.Trim();
            string hoten = txtName.Text.Trim();
            string ngaysinh = dtpBirth.Value.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture); // Corrected to use the 'Value' property of the DateTimePicker  
            string gioitinh = cmbGender.Text.ToString();
            string diachi = txtAddress.Text.Trim();
            string dienthoai = txtPhone.Text.Trim();
            string vaitro = cmbRole.Text.ToString();
            
            string donvi = getDepartmentID(cmbDepartment.Text.ToString()); // Get the department ID based on the selected name
     
            string luong = txtSalary.Text.Trim();
            string phucap = txtBonus.Text.Trim();

          

            if (string.IsNullOrEmpty(manv) || string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(diachi) || string.IsNullOrEmpty(dienthoai) ||
                string.IsNullOrEmpty(luong) || string.IsNullOrEmpty(phucap) || string.IsNullOrEmpty(donvi))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"UPDATE PDB_ADMIN.QLDH_NHANVIEN
            SET HOTEN = :hoten,
    NGSINH = TO_DATE(:ngaysinh, 'DD-MON-YYYY'),
    PHAI = :gioitinh,
    DCHI = :diachi,
    DT = :dienthoai,
    LUONG = :luong,
    PHUCAP = :phucap,
    VAITRO = :vaitro,
    MADV = :donvi
WHERE MANV = :manv";

            try
            {
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(":hoten", OracleDbType.Varchar2).Value = hoten;
                    cmd.Parameters.Add(":ngaysinh", OracleDbType.Varchar2).Value = ngaysinh;
                    cmd.Parameters.Add(":gioitinh", OracleDbType.Varchar2).Value = gioitinh;
                    cmd.Parameters.Add(":diachi", OracleDbType.Varchar2).Value = diachi;
                    cmd.Parameters.Add(":dienthoai", OracleDbType.Varchar2).Value = dienthoai;
                    cmd.Parameters.Add(":luong", OracleDbType.Decimal).Value = luong;
                    cmd.Parameters.Add(":phucap", OracleDbType.Decimal).Value = phucap;
                    cmd.Parameters.Add(":vaitro", OracleDbType.Varchar2).Value = vaitro;
                    cmd.Parameters.Add(":donvi", OracleDbType.Varchar2).Value = donvi;
                    cmd.Parameters.Add(":manv", OracleDbType.Varchar2).Value = manv;


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void getDepartment()
        {
            try
            {
                string query = "SELECT TENDV, COSO FROM PDB_ADMIN.QLDH_DONVI";
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Thêm cột hiển thị tạm thời
                    dt.Columns.Add("HIENTHI", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        row["HIENTHI"] = $"{row["TENDV"]} ({row["COSO"]})";
                    }

                    cmbDepartment.DataSource = dt;
                    cmbDepartment.DisplayMember = "HIENTHI";  // Hiển thị TENDV (COSO)
                    cmbDepartment.ValueMember = "TENDV";      // Nếu bạn muốn lấy lại giá trị khi chọn
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách đơn vị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string getDepartmentID(string departmentName)
        {
            string name = departmentName.Substring(0, departmentName.IndexOf('(')).Trim(); // Lấy tên phòng ban trước dấu ngoặc
            string coso = departmentName.Substring(departmentName.IndexOf('(') + 1, departmentName.IndexOf(')') - departmentName.IndexOf('(') - 1).Trim(); // Lấy cơ sở trong ngoặc
            string query = "SELECT MADV FROM PDB_ADMIN.QLDH_DONVI WHERE TENDV = :name AND COSO = :coso";
            using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
            {
                cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add(":coso", OracleDbType.Varchar2).Value = coso;
                return cmd.ExecuteScalar()?.ToString();
            }
        }
        private string getDepartmentName(string departmentID)
        {
            string query = "SELECT TENDV, COSO FROM PDB_ADMIN.QLDH_DONVI WHERE MADV = :madv";
            using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
            {
                cmd.Parameters.Add(":madv", OracleDbType.Varchar2).Value = departmentID;
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["TENDV"].ToString(); // Trả về tên phòng ban và cơ sở
                    }
                }
            }
            return null; // Trả về null nếu không tìm thấy
        }

        private void label7_Click(object sender, EventArgs e)
        {
            ViewSchedule viewDetail = new ViewSchedule(PersonnelMenu._username, PersonnelMenu._role);
            this.Hide();
            viewDetail.ShowDialog();
            this.Close();
        }

		private void pictureBox1_Click(object sender, EventArgs e)
		{
            ViewNotice viewNotification = new ViewNotice(Login.ID);
            this.Hide();
            viewNotification.ShowDialog();
            this.Close();
        }

		private void lbClasses_Click_1(object sender, EventArgs e)
		{
			SubjectManagement subjectManager = new SubjectManagement();
			this.Hide();
			subjectManager.ShowDialog();
			this.Close();
		}

		private void label8_Click(object sender, EventArgs e)
		{
            GradeManager gradeManager = new GradeManager();
            this.Hide();
            gradeManager.ShowDialog();
            this.Close();
		}
	}
}
