using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement
{
	public static class DatabaseSession
	{
		public static OracleConnection Connection { get; private set; }
		public static string CurrentUser { get; private set; }
		public static string UserType { get; private set; }

		public static bool InitializeSession(string username, string password)
		{
			try
			{
				string baseConnStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
				OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder(baseConnStr)
				{
					UserID = username,
					Password = password
				};

				var conn = new OracleConnection(builder.ToString());
				conn.Open();

				string userType = DetermineUserType(conn, username);
				if (userType == null)
				{
					conn.Close();
					return false; // Không xác định được loại người dùng
				}

				// Gán vào các biến static
				Connection = conn;
				CurrentUser = username;
				UserType = userType;

				return true;
			}
			catch
			{
				return false;
			}
		}

		private static string DetermineUserType(OracleConnection conn, string username)
		{
			string checkAdmin = "SELECT 1 FROM SYS.QLDH_ADMIN WHERE MAAD = :username";
			using (var cmd = new OracleCommand(checkAdmin, conn))
			{
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
				object result = cmd.ExecuteScalar();
				if (result != null) return "Admin";
			}

			string checkEmployee = "SELECT 1 FROM SYS.QLDH_NHANVIEN WHERE MANV = :username";
			using (var cmd = new OracleCommand(checkEmployee, conn))
			{
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
				object result = cmd.ExecuteScalar();
				if (result != null) return "NhanVien";
			}

			string checkStudent = "SELECT 1 FROM SYS.QLDH_SINHVIEN WHERE MASV = :username";
			using (var cmd = new OracleCommand(checkStudent, conn))
			{
				cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
				object result = cmd.ExecuteScalar();
				if (result != null) return "SinhVien";
			}

			return null;
		}
	}

}
