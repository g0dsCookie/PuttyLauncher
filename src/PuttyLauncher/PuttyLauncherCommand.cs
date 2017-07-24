using CookieProjects.PuttyLauncher.Putty;
using WinCommandPalette.Plugin.CommandBase;

namespace CookieProjects.PuttyLauncher
{
	public class PuttyLauncherCommand : ICommandBase
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public System.Drawing.Image Icon => null;

		public bool RunInUIThread => false;

		public PuttyArgumentsBase Arguments { get; set; }

		public PuttyLauncherCommand() { }

		public PuttyLauncherCommand(PuttyLoadSession session)
		{
			this.Name = Localization.strings.PuttyLauncher_Name.Replace("{{session}}", session.Session);
			this.Description = Localization.strings.PuttyLauncher_Description.Replace("{{session}}", session.Session);
			this.Arguments = session;
		}

		public PuttyLauncherCommand(PuttyArgumentsBase arguments, string name, string description)
		{
			this.Arguments = arguments;
			this.Name = name;
			this.Description = description;
		}

		public void Execute()
		{
			var wrapper = new PuttyWrapper(PuttyUtils.PuttyPath);
			wrapper.Arguments = this.Arguments;
			wrapper.Start();
		}
	}
}
