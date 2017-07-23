using System.Text;

namespace CookieProjects.PuttyLauncher.Putty
{
	public struct PuttyForward
	{
		public string BindAddress { get; }

		public ushort BindPort { get; }

		public string HostAddress { get; }

		public ushort HostPort { get; }

		public PuttyForward(string bindAddress, ushort bindPort, string hostAddress, ushort hostPort)
		{
			this.BindAddress = bindAddress;
			this.BindPort = bindPort;
			this.HostAddress = hostAddress;
			this.HostPort = hostPort;
		}

		public override string ToString()
		{
			var sb = new StringBuilder(this.BindAddress.Length + this.HostAddress.Length + 32);
			if (!string.IsNullOrWhiteSpace(this.BindAddress))
				sb.AppendFormat("{0}:", this.BindAddress);
			sb.AppendFormat("{0}:{1}:{2}", this.BindPort, this.HostAddress, this.HostPort);
			return sb.ToString();
		}
	}
}
