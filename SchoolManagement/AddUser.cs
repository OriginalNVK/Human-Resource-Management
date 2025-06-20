﻿using System;
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
		public AddUser()
        {
            InitializeComponent();
			LoadAllRoles();
			getAllDepartment();
        }

        private void getAllDepartment()
        {
            try
            {
                // Lấy danh sách các đơn vị theo tên duy nhất, lấy MADV nhỏ nhất cho mỗi tên đơn vị
                string departmentQuery = @"SELECT MIN(MADV) AS MADV, TENDV
										   FROM pdb_admin.QLDH_DONVI
										   GROUP BY TENDV
										   ORDER BY TENDV";

                using (OracleCommand cmd = new OracleCommand(departmentQuery, DatabaseSession.Connection))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Không cần thêm cột hiển thị đầy đủ nữa, chỉ hiển thị TENDV
                    comboDepartment.DataSource = dt;
                    comboDepartment.DisplayMember = "TENDV"; // Hiển thị tên khoa (đơn vị) duy nhất
                    comboDepartment.ValueMember = "MADV";   // Lưu MADV (lấy MADV đầu tiên của đơn vị đó)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
		{
			try
			{
				// Lấy thông tin từ các controls trên form
				string username = txtUsername.Text.Trim().ToUpper();
				string password = txtPassword.Text.Trim();
				string selectedGrantedRole = null; // Chỉ cho phép 1 role (1 dòng được check)
				string fullname = txtFullname.Text.Trim();
				string gender = GenderDropdown.SelectedItem?.ToString();
				string phoNum = txtPhoneNum.Text.Trim();
				string address = txtAddress.Text.Trim();
				string dob = dtpDOB.Value.ToString("dd-MMM-yyyy");
				string department = comboDepartment.Text.ToString();
				string location = locationList.Text.ToString();

				// Duyệt và lấy đúng 1 role từ DataGridView
				foreach (DataGridViewRow row in dgvUser.Rows)
				{
					bool isChecked = Convert.ToBoolean(row.Cells["chk"].Value ?? false);
					if (isChecked)
					{
						string roleName = row.Cells["Role"].Value?.ToString();
						if (!string.IsNullOrEmpty(roleName))
						{
							selectedGrantedRole = roleName;
							break;
						}
					}
				}

				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(selectedGrantedRole))
				{
					MessageBox.Show("Vui lòng nhập đầy đủ Username, Password và chọn một Role.");
					return;
				}
				
				var conn = DatabaseSession.Connection;
				if (conn.State != ConnectionState.Open)
					conn.Open();

				OracleTransaction transaction = conn.BeginTransaction();

				try
				{
					// Tạo user
					string createUserQuery = $"CREATE USER {username} IDENTIFIED BY \"{password}\"";
					using (OracleCommand cmdCreateUser = new OracleCommand(createUserQuery, conn))
					{
						cmdCreateUser.Transaction = transaction;
						cmdCreateUser.ExecuteNonQuery();
					}

					// Grant connect và LOGIN_ROLE
					string grantConnectQuery = $"GRANT CONNECT TO {username}";
					using (OracleCommand cmdGrantConnect = new OracleCommand(grantConnectQuery, conn))
					{
						cmdGrantConnect.Transaction = transaction;
						cmdGrantConnect.ExecuteNonQuery();
					}

					string grantLoginRoleQuery = $"GRANT LOGIN_ROLE TO {username}";
					using (OracleCommand cmdGrantLoginRole = new OracleCommand(grantLoginRoleQuery, conn))
					{
						cmdGrantLoginRole.Transaction = transaction;
						cmdGrantLoginRole.ExecuteNonQuery();
					}

					// Grant vai trò phụ
					string grantRoleQuery = $"BEGIN EXECUTE IMMEDIATE 'GRANT {selectedGrantedRole} TO {username}'; END;";
					using (OracleCommand cmdGrantRole = new OracleCommand(grantRoleQuery, conn))
					{
						cmdGrantRole.Transaction = transaction;
						cmdGrantRole.ExecuteNonQuery();
					}

                    // Grant quyền READ_NOTICE
                    string readNoticeQuery = $"GRANT READ_NOTICE TO {username}";
                    using (OracleCommand cmdGrantConnect = new OracleCommand(readNoticeQuery, conn))
                    {
                        cmdGrantConnect.Transaction = transaction;
                        cmdGrantConnect.ExecuteNonQuery();
                    }

                    // Gán nhãn OLS cho user bằng procedure
                    using (OracleCommand cmdGrantLabel = new OracleCommand("PDB_ADMIN.GRANT_LABEL_FOR_USER", conn))
                    {
                        cmdGrantLabel.Transaction = transaction;
                        cmdGrantLabel.CommandType = CommandType.StoredProcedure;
                        cmdGrantLabel.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;

                        // Map role thành p_role theo procedure
                        string roleParam = "";
                        if (selectedGrantedRole == "SV")
                            roleParam = "SV";
                        else if (selectedGrantedRole == "NV_TRGDV")
                            roleParam = "TRGDV";
                        else if (selectedGrantedRole.StartsWith("NV"))
                            roleParam = "NV";
                        else
                            roleParam = "NV";  // Mặc định

                        string depParam = "";

                        if (department == "Khoa Toán")
                        {
                            depParam = "TOAN";
                        }

                        if (department == "Khoa Lý")
                        {
                            depParam = "LY";
                        }

                        if (department == "Khoa Hóa")
                        {
                            depParam = "HOA";
                        }

                        if (department == "Phòng Hành Chính")
                        {
                            depParam = "HANHCHINH";
                        }

					cmdGrantLabel.Parameters.Add("p_role", OracleDbType.Varchar2).Value = roleParam;
					cmdGrantLabel.Parameters.Add("p_khoa", OracleDbType.Varchar2).Value = depParam;
					cmdGrantLabel.Parameters.Add("p_coso", OracleDbType.Varchar2).Value = location;
					cmdGrantLabel.ExecuteNonQuery();
				}

					// Lấy MADV từ tên đơn vị
					string queSeDepID = null;
					string getIdQuery = @"SELECT MADV 
                                      FROM PDB_ADMIN.QLDH_DONVI 
                                      WHERE TENDV = :dep 
                                      AND COSO = :location";

					using (OracleCommand cmdGetMADV = new OracleCommand(getIdQuery, DatabaseSession.Connection))
					{
						cmdGetMADV.Transaction = transaction;
						cmdGetMADV.Parameters.Add(new OracleParameter("dep", OracleDbType.Varchar2)).Value = department;
						cmdGetMADV.Parameters.Add(new OracleParameter("location", OracleDbType.Varchar2)).Value = location;

						using (OracleDataReader reader = cmdGetMADV.ExecuteReader())
						{
							if (reader.Read())
							{
								queSeDepID = reader.GetString(0);
							}
							else
							{
								transaction.Rollback();
								MessageBox.Show("Không tìm thấy đơn vị phù hợp.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
						}
					}


					if (selectedGrantedRole == "SV")
					{
						// Tạo câu lệnh insert vào bảng SINHVIEN
						string insertSinhVienQuery = "INSERT INTO PDB_ADMIN.QLDH_SINHVIEN (MASV, HOTEN, PHAI, NGSINH, DCHI, DT, TINHTRANG, KHOA) " +
													 "VALUES (:masv, :hoten, :phai, TO_DATE(:ngsinh, 'DD-MON-YYYY'), :dchi, :dt, 'Dang hoc', :madv)";

						using (OracleCommand cmdInsertSV = new OracleCommand(insertSinhVienQuery, conn))
						{
							cmdInsertSV.Transaction = transaction;
							cmdInsertSV.Parameters.Add("masv", OracleDbType.Varchar2).Value = username;
							cmdInsertSV.Parameters.Add("hoten", OracleDbType.Varchar2).Value = fullname;
							cmdInsertSV.Parameters.Add("phai", OracleDbType.Varchar2).Value = gender;
							cmdInsertSV.Parameters.Add("ngsinh", OracleDbType.Varchar2).Value = dob;
							cmdInsertSV.Parameters.Add("dchi", OracleDbType.Varchar2).Value = address;
							cmdInsertSV.Parameters.Add("dt", OracleDbType.Varchar2).Value = phoNum;
							cmdInsertSV.Parameters.Add("madv", OracleDbType.Varchar2).Value = queSeDepID;

							int resultSV = cmdInsertSV.ExecuteNonQuery();
							if (resultSV <= 0)
							{
								transaction.Rollback();
								MessageBox.Show("Không thể thêm thông tin chi tiết sinh viên.");
								return;
							}
						}
					}
					else
					{
						// Ánh xạ từ grantedRoles -> Vai trò Nhân viên
						string vaiTroNhanVien = null;

						switch (selectedGrantedRole)
						{
							case "NV_NVCB":
								vaiTroNhanVien = "NVCB";
								break;
							case "NV_GV":
								vaiTroNhanVien = "GV";
								break;
							case "NV_PDT":
								vaiTroNhanVien = "NV PĐT";
								break;
							case "NV_PKT":
								vaiTroNhanVien = "NV PKT";
								break;
							case "NV_TCHC":
								vaiTroNhanVien = "NV TCHC";
								break;
							case "NV_CTSV":
								vaiTroNhanVien = "NV CTSV";
								break;
							case "NV_TRGDV":
								vaiTroNhanVien = "TRGĐV";
								break;
							default:
								MessageBox.Show("Vai trò nhân viên không hợp lệ.");
								return;
						}

						if (vaiTroNhanVien == null)
						{
							MessageBox.Show("Vai trò không hợp lệ.");
							return;
						}

						string roleInsertQuery = "INSERT INTO PDB_ADMIN.QLDH_NHANVIEN (MANV, HOTEN, PHAI, NGSINH, DCHI, DT, LUONG, PHUCAP, VAITRO, MADV) " +
											 "VALUES (:username, :fullname, :gender, TO_DATE(:dob, 'DD-MON-YYYY'), :address, :phoNum, 0, 0, :vaiTro, :madv)";

						using (OracleCommand cmdInsert = new OracleCommand(roleInsertQuery, conn))
						{
							cmdInsert.Transaction = transaction;
							cmdInsert.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
							cmdInsert.Parameters.Add("fullname", OracleDbType.Varchar2).Value = fullname;
							cmdInsert.Parameters.Add("gender", OracleDbType.Varchar2).Value = gender;
							cmdInsert.Parameters.Add("dob", OracleDbType.Varchar2).Value = dob;
							cmdInsert.Parameters.Add("address", OracleDbType.Varchar2).Value = address;
							cmdInsert.Parameters.Add("phoNum", OracleDbType.Varchar2).Value = phoNum;
							cmdInsert.Parameters.Add("vaiTro", OracleDbType.Varchar2).Value = vaiTroNhanVien;
							cmdInsert.Parameters.Add("madv", OracleDbType.Varchar2).Value = queSeDepID;

							int result = cmdInsert.ExecuteNonQuery();
							if (result <= 0)
							{
								transaction.Rollback();
								MessageBox.Show("Không thể thêm thông tin chi tiết nhân viên.");
								return;
							}
						}
					}

					transaction.Commit();
					MessageBox.Show("Tạo người dùng và phân quyền thành công!");

					UsersManager userManager = new UsersManager();
					this.Hide();
					userManager.ShowDialog();
					this.Close();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					MessageBox.Show("Lỗi: " + ex.Message);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi kết nối hoặc xử lý: " + ex.Message);
			}
		}

		private void DgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex != 0) return; // Chỉ xử lý click vào cột checkbox

			// Toggle trạng thái checkbox
			bool currentValue = Convert.ToBoolean(dgvUser.Rows[e.RowIndex].Cells["chk"].Value ?? false);
			dgvUser.Rows[e.RowIndex].Cells["chk"].Value = !currentValue;

			// Bỏ chọn các dòng khác
			foreach (DataGridViewRow row in dgvUser.Rows)
			{
				if (row.Index != e.RowIndex)
				{
					row.Cells["chk"].Value = false;
				}
			}
		}

		private void LoadAllRoles()
		{
			try
			{
				var conn = DatabaseSession.Connection;

				if (conn.State != ConnectionState.Open)
					conn.Open();

				// Lấy danh sách các roles được định nghĩa
				string query = "SELECT ROLE FROM DBA_ROLES WHERE ROLE LIKE 'NV_%' OR ROLE = 'SV' OR ROLE LIKE 'TEST_%' OR ROLE = 'ADMIN_ROLE'";
				OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
				DataTable allRolesTable = new DataTable();
				adapter.Fill(allRolesTable);

				// Setup DataGridView
				dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
				dgvUser.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
				dgvUser.DataSource = null;
				dgvUser.Columns.Clear();

				DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
				{
					Name = "chk",
					HeaderText = "",
					Width = 40,
					ReadOnly = false,
					FalseValue = false,
					TrueValue = true,
					IndeterminateValue = false
				};
				dgvUser.Columns.Add(chk);
				dgvUser.Columns.Add("Role", "Role");

				// Thêm từng dòng (không đánh dấu role nào sẵn)
				foreach (DataRow row in allRolesTable.Rows)
				{
					string roleName = row["ROLE"].ToString();
					int index = dgvUser.Rows.Add();
					dgvUser.Rows[index].Cells["Role"].Value = roleName;
				}

				// Cấu hình thêm cho DataGridView
				dgvUser.RowHeadersVisible = false;
				dgvUser.AllowUserToAddRows = false;
				dgvUser.MultiSelect = false;
				dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgvUser.EditMode = DataGridViewEditMode.EditOnEnter;
				dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
				dgvUser.CellClick += DgvUser_CellClick;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi hiển thị role:\n" + ex.Message);
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

        private void addNoticeBtn_Click(object sender, EventArgs e)
        {
            AddNotification addNotification = new AddNotification();
            this.Hide();
            addNotification.ShowDialog();
            this.Close();
        }

		private void label9_Click(object sender, EventArgs e)
		{
			Backup backup = new Backup();
			this.Hide();
			backup.ShowDialog();
			this.Close();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}
