using System.Diagnostics;

namespace CookieProjects.PuttyLauncher.Putty
{
	public class PuttyWrapper
	{
		public string Putty { get; }

		public PuttyArgumentsBase Arguments { get; set; }

		public PuttyWrapper(string putty) => this.Putty = putty;

		public void Start()
		{
			var pInfo = new ProcessStartInfo(this.Putty, this.Arguments.GetCommandline());
			Process.Start(pInfo);
		}
	}
}
