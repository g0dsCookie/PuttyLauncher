namespace CookieProjects.PuttyLauncher.Putty
{
	public enum PuttyIPVersion
	{
		Auto,
		IPv4,
		IPv6
	}

	public static class PuttyIPVersionExtensions
	{
		public static string GetCommandLine(this PuttyIPVersion ipVersion)
		{
			switch (ipVersion)
			{
				case PuttyIPVersion.IPv4:
					return "-4";
				case PuttyIPVersion.IPv6:
					return "-6";
			}
			return string.Empty;
		}
	}
}
