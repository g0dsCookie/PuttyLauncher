namespace CookieProjects.PuttyLauncher.Putty
{
	public enum PuttySerialFlowControl
	{
		None,
		XON_XOFF,
		RTS_CTS,
		DSR_DTR
	}

	public static class PuttySerialFlowControlExtensions
	{
		public static string GetFlowControl(this PuttySerialFlowControl flowControl)
		{
			switch (flowControl)
			{
				case PuttySerialFlowControl.None:
					return "N";
				case PuttySerialFlowControl.XON_XOFF:
					return "X";
				case PuttySerialFlowControl.RTS_CTS:
					return "R";
				case PuttySerialFlowControl.DSR_DTR:
					return "D";
			}
			return string.Empty;
		}
	}
}
