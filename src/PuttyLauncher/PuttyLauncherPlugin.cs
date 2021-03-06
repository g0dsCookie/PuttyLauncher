﻿using CookieProjects.PuttyLauncher.Putty;
using System.Collections.Generic;
using System.Linq;
using WinCommandPalette.Plugin;
using WinCommandPalette.Plugin.CommandBase;

namespace CookieProjects.PuttyLauncher
{
	public class PuttyLauncherPlugin : WCPPlugin
	{
		public override PluginMeta PluginMeta => new PluginMeta
		{
			Author = "g0dsCookie",
			Description = Localization.strings.Plugin_Description
		};

		public override List<ICommandBase> AutoRegisterCommands { get; } = new List<ICommandBase>();

		public override void OnLoad()
		{
			if (Config.GlobalConfig.LoadSessions)
			{
				var sessions = PuttyUtils.GetSessions();
				AutoRegisterCommands.AddRange(from string s in sessions select new PuttyLauncherCommand(new PuttyLoadSession(s)));
				AutoRegisterCommands.Add(new PuttyLoadPrivateKeysCommand(PuttyUtils.GetPrivateKeys(sessions)));
			}
			else
				AutoRegisterCommands.Add(new PuttyLoadPrivateKeysCommand());

			base.OnLoad();
		}

		~PuttyLauncherPlugin()
		{
			Config.GlobalConfig.Save(Config.ConfigurationFile);
		}
	}
}
