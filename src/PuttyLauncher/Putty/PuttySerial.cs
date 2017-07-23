using System.Text;

namespace CookieProjects.PuttyLauncher.Putty
{
	public class PuttySerial : PuttyArgumentsBase
	{
		public PuttySerialConfiguration SerialConfiguration { get; set; }

		public PuttySerial() : base(PuttyProtocol.Serial)
		{ }

		public override string GetCommandline()
		{
			var sb = new StringBuilder();
			sb.Append(this.SerialConfiguration.ToString());
			sb.Append(" ");
			sb.Append(base.GetCommandline());
			return sb.ToString();
		}
	}
}
