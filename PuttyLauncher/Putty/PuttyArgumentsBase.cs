using System.Text;
using System.Xml.Serialization;

namespace CookieProjects.PuttyLauncher.Putty
{
	[XmlInclude(typeof(PuttySSH))]
	[XmlInclude(typeof(PuttyTelnet))]
	[XmlInclude(typeof(PuttySerial))]
	[XmlInclude(typeof(PuttyRaw))]
	[XmlInclude(typeof(PuttyRlogin))]
	public class PuttyArgumentsBase
	{
		public string Hostname { get; set; }

		public ushort Port { get; set; }

		public string Username { get; set; }

		public PuttyProtocol Protocol { get; }

		public bool Verbose { get; set; }

		public PuttyIPVersion IPVersion { get; set; }

		public PuttyArgumentsBase() { }

		protected PuttyArgumentsBase(PuttyProtocol protocol) => this.Protocol = protocol;

		public virtual string GetCommandline()
		{
			var sb = new StringBuilder();

			sb.AppendFormat("{0} ", this.Protocol.GetCommandLine());
			if (this.Protocol != PuttyProtocol.Serial) sb.AppendFormat("{0} ", this.IPVersion.GetCommandLine());

			if (this.Verbose) sb.Append("-v ");
			if (!string.IsNullOrWhiteSpace(this.Username) &&
				(this.Protocol != PuttyProtocol.Serial && this.Protocol != PuttyProtocol.Raw))
				sb.AppendFormat("-l \"{0}\" ", this.Username);

			if (this.Protocol == PuttyProtocol.Serial)
				sb.Append(this.Hostname);
			else
				sb.AppendFormat("{0} {1}", this.Hostname, this.Port);

			return sb.ToString();
		}
	}
}
