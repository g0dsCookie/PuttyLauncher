using System;
using System.Text;

namespace CookieProjects.PuttyLauncher.Putty
{
	public class PuttySerialConfiguration
	{
		public uint BaudRate { get; set; }

		public byte DataBits { get; set; }

		public float StopBits { get; set; }

		public PuttySerialParity Parity { get; set; }

		public PuttySerialFlowControl FlowControl { get; set; }

		public PuttySerialConfiguration()
		{ }

		public PuttySerialConfiguration(uint baud, byte dataBits, float stopBits, PuttySerialParity parity, PuttySerialFlowControl flowControl)
		{
			if (!ValidateDataBits(dataBits))
				throw new ArgumentOutOfRangeException(nameof(dataBits), dataBits, $"{nameof(dataBits)} must be between 5 and 9.");
			if (!ValidateStopBits(stopBits))
				throw new ArgumentOutOfRangeException(nameof(stopBits), stopBits, $"{nameof(stopBits)} must be 1, 1.5 or 2.");

			this.BaudRate = baud;
			this.DataBits = dataBits;
			this.StopBits = stopBits;
			this.Parity = parity;
			this.FlowControl = flowControl;
		}

		internal string GetStopBits()
		{
			switch (this.StopBits)
			{
				case 1.0F:
					return "1";
				case 1.5F:
					return "1.5";
				case 2.0F:
					return "2";
			}
			return string.Empty;
		}

		public override string ToString()
		{
			return string.Format("-sercfg {0},{1},{2},{3},{4}",
				this.BaudRate, this.DataBits, this.Parity.GetParity(),
				this.GetStopBits(), this.FlowControl.GetFlowControl());
		}

		public static bool ValidateDataBits(byte dataBits)
		{
			return dataBits > 5 && dataBits < 9;
		}

		public static bool ValidateStopBits(float stopBits)
		{
			return stopBits == 1 || stopBits == 1.5 || stopBits == 2;
		}
	}
}
