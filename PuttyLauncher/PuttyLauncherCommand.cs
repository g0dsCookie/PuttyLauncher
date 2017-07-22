using CookieProjects.PuttyLauncher.Putty;
using System.Diagnostics;
using WinCommandPalette.Plugin.CommandBase;

namespace CookieProjects.PuttyLauncher
{
	public class PuttyLauncherCommand : ICommandBase
	{
		public string Name { get; }

		public string Description { get; }

		public System.Drawing.Image Icon => null;

		public bool RunInUIThread => false;

		public string Session { get; }

		public PuttyLauncherCommand(string session)
		{
			this.Name = $"PuTTY: {session}";
			this.Description = $"Launch PuTTY session \"{session}\"";
			this.Session = session;
		}

		public void Execute()
		{
			var pInfo = new ProcessStartInfo(PuttyUtils.PuttyPath, $"-load \"{this.Session}\"");
			Process.Start(pInfo);
		}
	}
}
