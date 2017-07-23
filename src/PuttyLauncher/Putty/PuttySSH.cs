using System.Collections.Generic;
using System.Text;

namespace CookieProjects.PuttyLauncher.Putty
{
	public class PuttySSH : PuttyArgumentsBase
	{
		public List<PuttyForward> LocalForwards { get; set; } = new List<PuttyForward>();

		public List<PuttyForward> RemoteForwards { get; set; } = new List<PuttyForward>();

		public List<ushort> SOCKSForward { get; set; } = new List<ushort>();

		public string RemoteCommandFile { get; set; }

		public bool UsePageant { get; set; }

		public bool ForwardAgent { get; set; }

		public bool EnableCompression { get; set; }

		public bool UseSSHv1 { get; set; }

		public string PrivateKey { get; set; }

		public PuttySSH() : base(PuttyProtocol.SSH)
		{
		}

		public override string GetCommandline()
		{
			var sb = new StringBuilder();

			sb.Append(this.UseSSHv1 ? "-1 " : "-2 ");
			sb.Append(this.UsePageant ? "-agent " : "-noagent ");
			sb.Append(this.ForwardAgent ? "-A " : "-a ");
			if (this.EnableCompression) sb.Append("-C ");

			if (!string.IsNullOrWhiteSpace(this.PrivateKey))
				sb.AppendFormat("-i \"{0}\" ", this.PrivateKey);

			if (!string.IsNullOrWhiteSpace(this.RemoteCommandFile))
				sb.AppendFormat("-m \"{0}\" ", this.RemoteCommandFile);

			foreach (var fwd in this.LocalForwards)
				sb.AppendFormat("-L {0} ", fwd);
			foreach (var fwd in this.RemoteForwards)
				sb.AppendFormat("-R {0} ", fwd);
			foreach (var fwd in this.SOCKSForward)
				sb.AppendFormat("-D {0} ", fwd);

			sb.Append(base.GetCommandline());
			return sb.ToString();
		}
	}
}
