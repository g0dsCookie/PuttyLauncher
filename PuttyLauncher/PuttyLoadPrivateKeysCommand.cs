using CookieProjects.PuttyLauncher.Putty;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using WinCommandPalette.Plugin.CommandBase;

namespace CookieProjects.PuttyLauncher
{
	public class PuttyLoadPrivateKeysCommand : ICommandBase
	{
		public string Name => "PuTTY: Load private keys";

		public string Description => "Loads all private keys that are registered to your sessions.";

		public Image Icon => null;

		public bool RunInUIThread => false;

		public string[] PrivateKeys { get; }

		public PuttyLoadPrivateKeysCommand() => this.PrivateKeys = new string[] {};

		public PuttyLoadPrivateKeysCommand(string[] pKeys) => this.PrivateKeys = pKeys;

		public void Execute()
		{
			var sb = new StringBuilder();
			foreach (var pKey in Config.GlobalConfig.PrivateKeys)
				sb.AppendFormat("\"{0}\" ", pKey);
			foreach (var pKey in this.PrivateKeys)
				if (!Config.GlobalConfig.PrivateKeys.Contains(pKey))
					sb.AppendFormat("\"{0}\" ", pKey);
			
			var pInfo = new ProcessStartInfo(PuttyUtils.PageAnt, sb.ToString());
			Process.Start(pInfo);
		}
	}
}
