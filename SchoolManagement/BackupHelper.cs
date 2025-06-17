using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SchoolManagement
{
	public static class BackupHelper
	{
		private const string BackupLogFile = "backup_history.txt";
		private const string OracleSID = "XE";
		private const string RmanPath = @"C:\app\LK47\product\21c\dbhomeXE\bin\rman.exe";
		private static readonly string BackupDir = @"D:\App\Oracle\Backup";

		// Backup theo timestamp tag
		public static bool PerformBackup(string timestamp)
		{
			try
			{
				Directory.CreateDirectory(BackupDir);

				string backupFileName = $"BACKUP_{timestamp}_%U.BKP"; // %U = unique name per piece
				string backupPath = Path.Combine(BackupDir, backupFileName);
				string scriptPath = "backup_script.rman";

				File.WriteAllText(scriptPath, $@"
connect target /
run {{
    backup database plus archivelog
        format '{backupPath}'
        tag='{timestamp}';
}}");

				return RunRmanScript(scriptPath);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Backup Error: " + ex.Message);
				return false;
			}
		}

		// Restore theo timestamp + thời gian mong muốn
		public static bool PerformRestore(string timestamp, DateTime untilTime)
		{
			try
			{
				string scriptPath = "restore_script.rman";

				string untilTimeFormatted = untilTime.ToString("yyyy-MM-dd HH:mm:ss");

				File.WriteAllText(scriptPath, $@"
connect target /
shutdown immediate;
startup mount;
catalog start with '{BackupDir}';
run {{
    SET UNTIL TIME ""TO_DATE('{untilTimeFormatted}', 'YYYY-MM-DD HH24:MI:SS')"";
    restore database;
    recover database;
}}
alter database open resetlogs;
");

				return RunRmanScript(scriptPath);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Restore Error: " + ex.Message);
				return false;
			}
		}

		// Chạy script RMAN
		private static bool RunRmanScript(string scriptPath)
		{
			var psi = new ProcessStartInfo
			{
				FileName = RmanPath,
				Arguments = $"cmdfile={scriptPath}",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};

			using (var process = Process.Start(psi))
			{
				string output = process.StandardOutput.ReadToEnd();
				process.WaitForExit();

				File.WriteAllText("rman_output.log", output);

				return output.Contains("Finished backup") ||
					   output.Contains("Finished restore") ||
					   output.Contains("database opened");
			}
		}

		public static void AppendBackupLog(string tag)
		{
			File.AppendAllText(BackupLogFile, tag + Environment.NewLine);
		}

		public static List<string> GetBackupLog()
		{
			if (!File.Exists(BackupLogFile)) return new List<string>();
			return File.ReadAllLines(BackupLogFile)
					   .Where(line => !string.IsNullOrWhiteSpace(line))
					   .ToList();
		}
	}
}
