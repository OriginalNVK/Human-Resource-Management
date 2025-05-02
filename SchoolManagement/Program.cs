using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagement
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			//string connStr = ConfigurationManager
			//	   .ConnectionStrings["SchoolDB"]
			//	   .ConnectionString;

			//using (var conn = new OracleConnection(connStr))
			//{
			//	try
			//	{
			//		conn.Open();
			//		MessageBox.Show("✔ Oracle connection successful!");
			//	}
			//	catch (OracleException ox)
			//	{
			//		MessageBox.Show($"Oracle error: {ox.Message}");
			//	}
			//}


			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
