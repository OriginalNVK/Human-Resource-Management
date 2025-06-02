using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace SchoolManagement
{
	public static class DatabaseSession
	{
		public static OracleConnection Connection { get; private set; }
		public static string CurrentUser { get; private set; }
		public static string UserType { get; private set; }

		public static string UserRole { get; private set; }

		public static bool InitializeSession(string username, string password)
		{
			try
			{
				string baseConnStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
				var builder = new OracleConnectionStringBuilder(baseConnStr)
				{
					UserID = username,
					Password = password
				};

				// Mở kết nối ban đầu để kiểm tra loại user
				using (var tempConn = new OracleConnection(builder.ToString()))
				{
					
                    tempConn.Open();
					
                    string userType = DetermineUserType(tempConn, username);					
					
                    if (userType == null)
					{
						return false; // Không xác định được loại người dùng
					}

					// Đóng kết nối tạm
					tempConn.Close();
				}

				// Tạo kết nối chính thức với cấu hình đã điều chỉnh
				Connection = new OracleConnection(builder.ToString());
				Connection.Open();

				CurrentUser = username;
				UserType = DetermineUserType(Connection, username); // Kiểm tra lại
				UserRole = DetermineUserRole(Connection, username);
				

				return true;
			}
			catch (Exception ex)
			{
				// Xử lý lỗi (có thể log lại ex.Message)
				return false;
			}
		}

		public static string DetermineUserType(OracleConnection conn, string username)
		{
            string checkStudent = "SELECT 1 FROM pdb_admin.QLDH_SINHVIEN WHERE MASV = :username";
            using (var cmd = new OracleCommand(checkStudent, conn))
            {
                cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                if (cmd.ExecuteScalar() != null) return "SinhVien";
            }
            string checkEmployee = "SELECT 1 FROM pdb_admin.QLDH_NHANVIEN WHERE MANV = :username";
            using (var cmd = new OracleCommand(checkEmployee, conn))
			{
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
				if (cmd.ExecuteScalar() != null) return "NhanVien";
			}

            string checkAdmin = "SELECT 1 FROM pdb_admin.QLDH_ADMIN WHERE MAAD = :username";
			using (var cmd = new OracleCommand(checkAdmin, conn))
			{
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
				if (cmd.ExecuteScalar() != null) return "Admin";
			}

			

			return null;
		}

		public static string DetermineUserRole(OracleConnection conn, string username)
		{
			string checkRole = "SELECT VAITRO FROM PDB_ADMIN.QLDH_NHANVIEN WHERE MANV = :username";
            using (var cmd = new OracleCommand(checkRole, conn))
            {
                cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string grantedRole = result.ToString().ToUpper();

                    return grantedRole;
                }
            }
			return null;
        }

		public static void CloseSession()
		{
			if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
			{
				Connection.Close();
				Connection.Dispose();
				Connection = null;
			}
			CurrentUser = null;
			UserType = null;
		}
	}
}