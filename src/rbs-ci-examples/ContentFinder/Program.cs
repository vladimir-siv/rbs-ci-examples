using System;
using System.Collections.Specialized;
using System.Text;
using System.Diagnostics;

namespace ContentFinder
{
	public static class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.Error.WriteLine("No arguments specified. Use \"ContentFinder /help\" fore more info.");
				return;
			}

			if (args[0] == "/help")
			{
				Console.WriteLine("Usage: ContentFinder <string> [path1] [path2] [path3]...");
				Console.WriteLine("It is possible through config to also use standard input. Look for the setting \"UseStdIn\" in config file.");
				Console.WriteLine("Moreover, the config file also includes additional default files which are included in search.");
				Console.WriteLine("The default, if any, are searched first. Then, files from cli are searched, if any. Lastly, files from stdin are searched.");
				return;
			}
			
			var files = Properties.Settings.Default.DefaultFiles ?? new StringCollection();
			for (var i = 1; i < args.Length; ++i) files.Add(args[i]);
			if (Properties.Settings.Default.UseStdIn) files.AddRange(Console.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

			var sb = new StringBuilder();
			foreach (var file in files) sb.Append($" \"{file}\"");

			Console.WriteLine("EXECUTING: FIND /N \"" + args[0] + "\"" + sb.ToString());

			var info = new ProcessStartInfo()
			{
				FileName = "CMD",
				Arguments = "/C FIND /N \"" + args[0] + "\"" + sb.ToString(),
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				UseShellExecute = false,
			};

			var proc = Process.Start(info);
			proc.WaitForExit();

			var result = proc.StandardOutput.ReadToEnd();
			Console.WriteLine(result);
		}
	}
}
