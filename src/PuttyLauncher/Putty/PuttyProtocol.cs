namespace CookieProjects.PuttyLauncher.Putty
{
	public enum PuttyProtocol
	{
		LoadSession,
		SSH,
		Telnet,
		Rlogin,
		Raw,
		Serial
	}

	public static class PuttyProtocolExtensions
	{
		public static string GetCommandLine(this PuttyProtocol protocol)
		{
			switch (protocol)
			{
				case PuttyProtocol.SSH:
					return "-ssh";
				case PuttyProtocol.Telnet:
					return "-telnet";
				case PuttyProtocol.Rlogin:
					return "-rlogin";
				case PuttyProtocol.Raw:
					return "-raw";
				case PuttyProtocol.Serial:
					return "-serial";
			}
			return string.Empty;
		}
	}
}
