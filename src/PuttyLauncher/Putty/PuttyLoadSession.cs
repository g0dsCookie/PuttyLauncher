namespace CookieProjects.PuttyLauncher.Putty
{
	public class PuttyLoadSession : PuttyArgumentsBase
	{
		public string Session { get; set; }

		public PuttyLoadSession(string session) : base(PuttyProtocol.LoadSession) => this.Session = session;

		public override string GetCommandline()
		{
			return string.Format("-load \"{0}\"", this.Session);
		}
	}
}
