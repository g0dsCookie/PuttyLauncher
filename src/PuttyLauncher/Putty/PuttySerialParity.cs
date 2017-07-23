namespace CookieProjects.PuttyLauncher.Putty
{
	public enum PuttySerialParity
	{
		None,
		Odd,
		Even,
		Mark,
		Space
	}

	public static class PuttySerialParityExtensions
	{
		public static string GetParity(this PuttySerialParity parity)
		{
			switch (parity)
			{
				case PuttySerialParity.None:
					return "n";
				case PuttySerialParity.Odd:
					return "o";
				case PuttySerialParity.Even:
					return "e";
				case PuttySerialParity.Mark:
					return "m";
				case PuttySerialParity.Space:
					return "s";
			}
			return string.Empty;
		}
	}
}
