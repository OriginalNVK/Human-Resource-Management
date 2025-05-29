using System;
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
    public partial class AddEmployee : KryptonForm
    {
        public AddEmployee()
        {
            InitializeComponent();
            LoadDepartment();
        }

        private void LoadDepartment()
        {
            try
            {
                string query = "SELECT TENDV FROM PDB_ADMIN.QLDH_DONVI";
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cmbDepartment.DataSource = dt;
                    cmbDepartment.DisplayMember = "TENDV";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng ban: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            AdminProfile myProfile = new AdminProfile();
            this.Hide();
            myProfile.ShowDialog();
            this.Close();

        }

        private void label2_Click(object sender, EventArgs e)
        {

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
            string donvi = cmbDepartment.Text.ToString();
            string luong = txtSalary.Text.Trim();
            string phucap = txtBonus.Text.Trim();
            MessageBox.Show(ngaysinh);

            try
            {
                string queryMaDV = "SELECT MADV FROM PDB_ADMIN.QLDH_DONVI WHERE TENDV = :donvi";

                using (OracleCommand cmd = new OracleCommand(queryMaDV, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(":donvi", OracleDbType.Varchar2).Value = donvi;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            donvi = reader["MADV"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy đơn vị!");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm đơn vị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(manv) || string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(diachi) || string.IsNullOrEmpty(dienthoai) ||
                string.IsNullOrEmpty(luong) || string.IsNullOrEmpty(phucap) || string.IsNullOrEmpty(donvi))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"  
        INSERT INTO PDB_ADMIN.QLDH_NHANVIEN   
           (MANV, HOTEN, NGSINH, PHAI, DCHI, DT, LUONG, PHUCAP, VAITRO, MADV)  
        VALUES   
           (:manv, :hoten, TO_DATE(:ngaysinh, 'DD-MON-YYYY'), :gioitinh, :diachi, :dienthoai, :luong, :phucap, :vaitro, :donvi)";

            try
            {
                using (OracleCommand cmd = new OracleCommand(query, DatabaseSession.Connection))
                {
                    cmd.Parameters.Add(":manv", OracleDbType.Varchar2).Value = manv;
                    cmd.Parameters.Add(":hoten", OracleDbType.Varchar2).Value = hoten;
                    cmd.Parameters.Add(":ngaysinh", OracleDbType.Varchar2).Value = ngaysinh;
                    cmd.Parameters.Add(":gioitinh", OracleDbType.Varchar2).Value = gioitinh;
                    cmd.Parameters.Add(":diachi", OracleDbType.Varchar2).Value = diachi;
                    cmd.Parameters.Add(":dienthoai", OracleDbType.Varchar2).Value = dienthoai;
                    cmd.Parameters.Add(":luong", OracleDbType.Decimal).Value = luong;
                    cmd.Parameters.Add(":phucap", OracleDbType.Decimal).Value = phucap;
                    cmd.Parameters.Add(":vaitro", OracleDbType.Varchar2).Value = vaitro;
                    cmd.Parameters.Add(":donvi", OracleDbType.Varchar2).Value = donvi;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonPalette1_PalettePaint(object sender, PaletteLayoutEventArgs e)
        {

        }

        private void Dashboard_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtHello_Click(object sender, EventArgs e)
        {

        }

        private void lbSalary_Click(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbGender_Click(object sender, EventArgs e)
        {

        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbAddress_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbBirth_Click(object sender, EventArgs e)
        {

        }

        private void lbClass_Click(object sender, EventArgs e)
        {

        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lb01_Click(object sender, EventArgs e)
        {

        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lb02_Click(object sender, EventArgs e)
        {

        }

        private void txtBonus_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblID_Click(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblRole_Click(object sender, EventArgs e)
        {

        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pbLogout_Click(object sender, EventArgs e)
        {

        }

        private void pbSection_Click(object sender, EventArgs e)
        {

        }

        private void pbProfile_Click(object sender, EventArgs e)
        {

        }

        private void pbClasses_Click(object sender, EventArgs e)
        {

        }

        private void pbTeachers_Click(object sender, EventArgs e)
        {

        }

        private void pbStudents_Click(object sender, EventArgs e)
        {

        }

        private void pbGrade_Click(object sender, EventArgs e)
        {

        }

        private void pbCalendar_Click(object sender, EventArgs e)
        {

        }

        private void pbDetail_Click(object sender, EventArgs e)
        {

        }
    }
}
