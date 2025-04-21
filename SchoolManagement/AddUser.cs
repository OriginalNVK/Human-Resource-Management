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
    public partial class AddUser : KryptonForm
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
        private int pageSize = 20;

		TextBox txtHoTen = new TextBox();
		ComboBox cbPhai = new ComboBox();
		DateTimePicker dtpNgaySinh = new DateTimePicker();
		TextBox txtDiaChi = new TextBox(); // chỉ cho sinh viên
		TextBox txtDT = new TextBox();
		ComboBox cbTinhTrang = new ComboBox(); // chỉ cho sinh viên
		TextBox txtLuong = new TextBox(); // chỉ cho nhân viên
		TextBox txtPhuCap = new TextBox(); // chỉ cho nhân viên
		ComboBox cbVaiTro = new ComboBox(); // chỉ cho nhân viên
		ComboBox cbKhoa = new ComboBox(); // chỉ cho sinh viên
		public AddUser()
        {
            InitializeComponent();
            LoadStudents();
        }
        private void LoadStudents()
        {
            //try
            //{
            //    string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
            //    OracleConnection conn = new OracleConnection(oradb);  // C#
            //    OracleCommand cmd = new OracleCommand();
            //    cmd.Connection = conn;
            //    cmd.CommandText = "SELECT * FROM ( SELECT a.MAKHOA \"Department ID\", a.TENKHOA \"Name\", rownum r__ FROM ( SELECT * FROM SYSTEM.KHOA WHERE MAKHOA != 'OT' ORDER BY MAKHOA ASC ) a WHERE rownum < ((" + currFrom.ToString() + " * " + pageSize.ToString() + ") + 1 ) ) WHERE r__ >= (((" + currFrom.ToString() + "-1) * " + pageSize.ToString() + ") + 1)";
            //    cmd.CommandType = CommandType.Text;
            //    conn.Open();

            //    OracleDataReader reader = cmd.ExecuteReader();


            //    DataTable dataTable = new DataTable();
            //    dataTable.Load(reader);
            //    dgvStudents.DataSource = dataTable;


            //    conn.Dispose();
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void dgvStudents_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    isSelected = true;
            //    showAction();

            //    DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
            //    txtID.Text = row.Cells[0].Value.ToString();
            //    txtName.Text = row.Cells[1].Value.ToString(); 

            //}
        }
        private void showAction()
        {
            //pbStudents.Visible = true;
            //lbStudents.Visible = true;
            //pbEdit.Visible = true;
            //lbEdit.Visible = true;
            //pbDelete.Visible = true;
            //lbDelete.Visible = true;
            //pbSave.Visible = false;
            //lbSave.Visible = false;
        }

        private void pbStudents_Click(object sender, EventArgs e)
        {
            //action = 0;

            //pbStudents.Visible = false;
            //lbStudents.Visible = false;
            //pbEdit.Visible = false;
            //lbEdit.Visible = false;
            //pbDelete.Visible = false;
            //lbDelete.Visible = false;
            //pbSave.Visible = true;
            //lbSave.Visible = true;

            //txtID.Visible = true;
            //label10.Visible = true;
            //txtID.Enabled = true;

            //txtName.Text = "";
            //txtName.Enabled = true;
             
        }

        private void pbEdit_Click(object sender, EventArgs e)
        {
            //if (!isSelected)
            //{
            //    MessageBox.Show("Please choose student to edit!");
            //    return;
            //}
            //action = 1;

            //pbStudents.Visible = false;
            //lbStudents.Visible = false;
            //pbEdit.Visible = false;
            //lbEdit.Visible = false;
            //pbDelete.Visible = false;
            //lbDelete.Visible = false;
            //pbSave.Visible = true;
            //lbSave.Visible = true;

            //txtID.Enabled = false;
            //txtName.Enabled = true; 
        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            //if (action == 0)
            //{
            //    try
            //    {
            //        string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
            //        OracleConnection conn = new OracleConnection(oradb);  // C#
            //        OracleCommand cmd = new OracleCommand();
            //        cmd.Connection = conn;
            //        cmd.CommandText =  "INSERT INTO SYSTEM.KHOA VALUES('" + txtID.Text + "', N'" + txtName.Text + "')";
            //        cmd.CommandType = CommandType.Text;
            //        conn.Open();

            //        OracleDataReader reader = cmd.ExecuteReader();

            //        conn.Dispose();
            //    }
            //    catch (Exception es)
            //    {
            //        MessageBox.Show(es.Message);
            //    }
            //    MessageBox.Show("Add success");
            //}
            //else
            //{
            //    try
            //    {
            //        string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
            //        OracleConnection conn = new OracleConnection(oradb);  // C#
            //        OracleCommand cmd = new OracleCommand();
            //        cmd.Connection = conn;
            //        cmd.CommandText = "UPDATE SYSTEM.KHOA SET TENKHOA=N'" + txtName.Text + "' WHERE MAKHOA='" + txtID.Text + "'";
            //        cmd.CommandType = CommandType.Text;
            //        conn.Open();

            //        OracleDataReader reader = cmd.ExecuteReader();

            //        conn.Dispose();
            //    }
            //    catch (Exception es)
            //    {
            //        MessageBox.Show(es.Message);
            //    }
            //    MessageBox.Show("Edit success");
            //}
            //Refesh();
        }
        private void Refesh()
        {
            //pbStudents.Visible = true;
            //lbStudents.Visible = true;
            //pbEdit.Visible = true;
            //lbEdit.Visible = true;
            //pbDelete.Visible = true;
            //lbDelete.Visible = true;
            //pbSave.Visible = false;
            //lbSave.Visible = false;

            //LoadStudents();
            //txtSearch.Text = "";
            //txtID.Text = "";
            //txtName.Text = ""; 
        }

        private void pbReload_Click(object sender, EventArgs e)
        {
            Refesh();
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {
            //if (!isSelected)
            //{
            //    MessageBox.Show("Please choose department to delete!");
            //    return;
            //}

            //DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);

            //if (dialogResult == DialogResult.Yes)
            //{
            //    try
            //    {
            //        string oradb = "Data Source=localhost:1521 / ORCL21;User Id=SYSTEM;Password=123;";
            //        OracleConnection conn = new OracleConnection(oradb);  // C#
            //        OracleCommand cmd = new OracleCommand("SP_KHOA_DELETE", conn);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.Add("p_makhoa", OracleDbType.Varchar2).Value = txtID.Text;
            //        conn.Open();

            //        OracleDataAdapter da = new OracleDataAdapter(cmd);
            //        cmd.ExecuteNonQuery();

            //        MessageBox.Show("Delete success");

            //        conn.Dispose();
            //    }
            //    catch (Exception es)
            //    {
            //        MessageBox.Show(es.Message);
            //    }
            //}
            //isSelected = false;

            //Refesh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            //Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            //app.Visible = true;
            //worksheet = workbook.Sheets["Sheet1"];
            //worksheet = workbook.ActiveSheet;
            //worksheet.Name = "Data";
            //for (int i = 1; i < dgvStudents.Columns.Count + 1; i++)
            //{
            //    worksheet.Cells[1, i] = dgvStudents.Columns[i - 1].HeaderText;
            //}
            //for (int i = 0; i < dgvStudents.Rows.Count - 1; i++)
            //{
            //    for (int j = 0; j < dgvStudents.Columns.Count; j++)
            //    {
            //        worksheet.Cells[i + 2, j + 1] = dgvStudents.Rows[i].Cells[j].Value.ToString();
            //    }
            //}
            //workbook.SaveAs("Desktop\\Data.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //app.Quit();
        }

		private void DepartmentManager_Load(object sender, EventArgs e)
		{

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string role = comboBox1.SelectedItem.ToString();

			ClearDynamicFields(); // Xóa tất cả controls cũ

			AddCommonFields(); // Họ tên, Phái, Ngày sinh, Điện thoại

			if (role == "SINHVIEN")
			{
				AddSinhVienFields(); // Địa chỉ, Khoa, Tình trạng
			}
			else if (role == "GIAOVIEN" || role == "ADMIN")
			{
				AddNhanVienFields(); // Lương, Phụ cấp, Vai trò
			}
		}

		private void ClearDynamicFields()
		{
			// Duyệt qua toàn bộ các Control trên Form và xóa những cái mà mình đã thêm thủ công
			List<Control> controlsToRemove = new List<Control>();

			foreach (Control ctrl in this.Controls)
			{
				if (ctrl.Tag != null && ctrl.Tag.ToString() == "dynamic")
				{
					controlsToRemove.Add(ctrl);
				}
			}

			foreach (Control ctrl in controlsToRemove)
			{
				this.Controls.Remove(ctrl);
				ctrl.Dispose();
			}
		}

		private void AddCommonFields()
		{
			// Họ tên
			TextBox txtHoTen = new TextBox();
			txtHoTen.Location = new Point(30, 100);
			txtHoTen.Width = 200;
			txtHoTen.Name = "txtHoTen";
			txtHoTen.Tag = "dynamic";
			this.Controls.Add(txtHoTen);

			// Phái
			ComboBox cbPhai = new ComboBox();
			cbPhai.Location = new Point(30, 130);
			cbPhai.Width = 100;
			cbPhai.Name = "cbPhai";
			cbPhai.Items.AddRange(new string[] { "Nam", "Nu" });
			cbPhai.Tag = "dynamic";
			this.Controls.Add(cbPhai);

			// Ngày sinh
			DateTimePicker dtpNgaySinh = new DateTimePicker();
			dtpNgaySinh.Location = new Point(30, 160);
			dtpNgaySinh.Name = "dtpNgaySinh";
			dtpNgaySinh.Format = DateTimePickerFormat.Short;
			dtpNgaySinh.Tag = "dynamic";
			this.Controls.Add(dtpNgaySinh);

			// Điện thoại
			TextBox txtDT = new TextBox();
			txtDT.Location = new Point(30, 190);
			txtDT.Width = 150;
			txtDT.Name = "txtDT";
			txtDT.Tag = "dynamic";
			this.Controls.Add(txtDT);
		}



		private void AddSinhVienFields()
		{
			// Địa chỉ
			txtDiaChi.Location = new Point(30, 150);
			txtDiaChi.Width = 200;
			this.Controls.Add(txtDiaChi);

			// Khoa
			cbKhoa.Location = new Point(30, 180);
			cbKhoa.Width = 150;
			cbKhoa.Items.AddRange(new string[] { "CNTT", "QTKD", "KTPM" });
			this.Controls.Add(cbKhoa);

			// Tình trạng
			cbTinhTrang.Location = new Point(30, 210);
			cbTinhTrang.Width = 150;
			cbTinhTrang.Items.AddRange(new string[] { "Dang hoc", "Nghi hoc", "Bao luu" });
			this.Controls.Add(cbTinhTrang);
		}

		private void AddNhanVienFields()
		{
			txtLuong.Location = new Point(30, 150);
			txtLuong.Width = 150;
			this.Controls.Add(txtLuong);

			txtPhuCap.Location = new Point(30, 180);
			txtPhuCap.Width = 150;
			this.Controls.Add(txtPhuCap);

			cbVaiTro.Location = new Point(30, 210);
			cbVaiTro.Width = 150;
			cbVaiTro.Items.AddRange(new string[] { "GV", "NV PĐT", "NV PKT", "TRGĐV" });
			this.Controls.Add(cbVaiTro);
		}

		private void txtID_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
