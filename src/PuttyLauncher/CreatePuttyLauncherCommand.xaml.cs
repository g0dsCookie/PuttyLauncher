using CookieProjects.PuttyLauncher.Putty;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using WinCommandPalette.Plugin.CommandBase;
using WinCommandPalette.Plugin.CreateCommand;

namespace CookieProjects.PuttyLauncher
{
	/// <summary>
	/// Interaction logic for CreatePuttyLauncherCommand.xaml
	/// </summary>
	public partial class CreatePuttyLauncherCommand : UserControl, ICreateCommand
	{
		public string CommandTypeName => "PuTTY";

		public string CommandDescription => "Add a new PuTTY command.";

		public CreatePuttyLauncherCommand()
		{
			InitializeComponent();
		}

		public void ClearAll()
		{
			// Protocol
			this.rbSSH.IsChecked = true;

			// Command
			this.tbName.Text = string.Empty;
			this.tbDescription.Text = string.Empty;

			// Base
			this.tbHostname.Text = string.Empty;
			this.numericPort.Value = 22;
			this.tbUsername.Text = string.Empty;
			this.rbIPAuto.IsChecked = true;
			this.cbVerbose.IsChecked = false;

			// Serial
			this.tbSerialLine.Text = "COM1";
			this.numericBaudRate.Value = 9600;
			this.numericDataBits.Value = this.numericDataBits.Minimum;
			this.rbStopBits1.IsChecked = true;
			this.rbParityNone.IsChecked = true;
			this.rbFlowNone.IsChecked = true;

			// SSH
			this.tbPrivateKey.Text = string.Empty;
			this.cbForwardAgent.IsChecked = false;
		}

		public ICommandBase GetCommand()
		{
			PuttyArgumentsBase args = null;

			if (rbSSH.IsChecked.Value)
			{
				args = new PuttySSH
				{
					PrivateKey = this.tbPrivateKey.Text,
					ForwardAgent = this.cbForwardAgent.IsChecked.Value
				};
			}
			else if (rbTelnet.IsChecked.Value)
			{
				args = new PuttyTelnet();
			}
			else if (rbSerial.IsChecked.Value)
			{
				float stopBits;
				if (rbStopBits1.IsChecked.Value) stopBits = 1.0F;
				else if (rbStopBits15.IsChecked.Value) stopBits = 1.5F;
				else stopBits = 2.0F;

				PuttySerialParity parity;
				if (rbParityNone.IsChecked.Value) parity = PuttySerialParity.None;
				else if (rbParityOdd.IsChecked.Value) parity = PuttySerialParity.Odd;
				else if (rbParityEven.IsChecked.Value) parity = PuttySerialParity.Even;
				else if (rbParityMark.IsChecked.Value) parity = PuttySerialParity.Mark;
				else parity = PuttySerialParity.Space;

				PuttySerialFlowControl flow;
				if (rbFlowNone.IsChecked.Value) flow = PuttySerialFlowControl.None;
				else if (rbFlowRTS_CTS.IsChecked.Value) flow = PuttySerialFlowControl.RTS_CTS;
				else if (rbFlowXON_XOFF.IsChecked.Value) flow = PuttySerialFlowControl.XON_XOFF;
				else flow = PuttySerialFlowControl.DSR_DTR;

				args = new PuttySerial
				{
					Hostname = this.tbSerialLine.Text,
					Verbose = this.cbVerbose.IsChecked.Value,
					SerialConfiguration = new PuttySerialConfiguration(
						Convert.ToUInt16(numericBaudRate.Value),
						Convert.ToByte(numericDataBits.Value),
						stopBits, parity, flow)
				};

				return new PuttyLauncherCommand(args, this.tbName.Text, this.tbDescription.Text);
			}
			else if (rbRaw.IsChecked.Value)
			{
				args = new PuttyRaw();
			}
			else if (rbRlogin.IsChecked.Value)
			{
				args = new PuttyRlogin();
			}

			args.Hostname = this.tbHostname.Text;
			args.Port = Convert.ToUInt16(this.numericPort.Value);
			args.Username = this.tbUsername.Text;
			args.Verbose = this.cbVerbose.IsChecked.Value;

			if (rbIPAuto.IsChecked.Value) args.IPVersion = PuttyIPVersion.Auto;
			else if (rbIPv4.IsChecked.Value) args.IPVersion = PuttyIPVersion.IPv4;
			else args.IPVersion = PuttyIPVersion.IPv6;

			return new PuttyLauncherCommand(args, this.tbName.Text, this.tbDescription.Text);
		}

		public void ShowCommand(ICommandBase command)
		{
			if (command is PuttyLauncherCommand cmd)
			{
				var args = cmd.Arguments;

				// Command
				this.tbName.Text = cmd.Name;
				this.tbDescription.Text = cmd.Description;

				// Base
				if (args.Protocol != PuttyProtocol.Serial)
				{
					this.tbHostname.Text = args.Hostname;
					this.numericPort.Value = args.Port;
					this.tbUsername.Text = args.Username;
					this.cbVerbose.IsChecked = args.Verbose;

					switch (args.IPVersion)
					{
						case PuttyIPVersion.IPv4:
							this.rbIPv4.IsChecked = true;
							break;

						case PuttyIPVersion.IPv6:
							this.rbIPv6.IsChecked = true;
							break;

						default:
							this.rbIPAuto.IsChecked = true;
							break;
					}
				}

				// Protocol
				switch (args.Protocol)
				{
					case PuttyProtocol.SSH:
						this.rbSSH.IsChecked = true;
						var sshArgs = args as PuttySSH;
						this.tbPrivateKey.Text = sshArgs.PrivateKey;
						this.cbForwardAgent.IsChecked = sshArgs.ForwardAgent;
						break;

					case PuttyProtocol.Telnet:
						this.rbTelnet.IsChecked = true;
						break;

					case PuttyProtocol.Serial:
						this.rbSerial.IsChecked = true;
						var serialArgs = args as PuttySerial;
						this.tbSerialLine.Text = serialArgs.Hostname;
						this.numericBaudRate.Value = (int)serialArgs.SerialConfiguration.BaudRate;
						this.numericDataBits.Value = (int)serialArgs.SerialConfiguration.DataBits;

						// Stop Bits
						switch (serialArgs.SerialConfiguration.StopBits)
						{
							case 1.0F:
								this.rbStopBits1.IsChecked = true;
								break;

							case 1.5F:
								this.rbStopBits15.IsChecked = true;
								break;

							case 2.0F:
								this.rbStopBits2.IsChecked = true;
								break;
						}

						// Parity
						switch (serialArgs.SerialConfiguration.Parity)
						{
							case PuttySerialParity.None:
								this.rbParityNone.IsChecked = true;
								break;
							case PuttySerialParity.Odd:
								this.rbParityOdd.IsChecked = true;
								break;
							case PuttySerialParity.Even:
								this.rbParityEven.IsChecked = true;
								break;
							case PuttySerialParity.Mark:
								this.rbParityMark.IsChecked = true;
								break;
							case PuttySerialParity.Space:
								this.rbParitySpace.IsChecked = true;
								break;
						}

						// Flow Control
						switch (serialArgs.SerialConfiguration.FlowControl)
						{
							case PuttySerialFlowControl.None:
								this.rbFlowNone.IsChecked = true;
								break;
							case PuttySerialFlowControl.RTS_CTS:
								this.rbFlowRTS_CTS.IsChecked = true;
								break;
							case PuttySerialFlowControl.XON_XOFF:
								this.rbFlowXON_XOFF.IsChecked = true;
								break;
							case PuttySerialFlowControl.DSR_DTR:
								this.rbFlowDSR_DTR.IsChecked = true;
								break;
						}

						break;

					case PuttyProtocol.Rlogin:
						this.rbSerial.IsChecked = true;
						break;

					case PuttyProtocol.Raw:
						this.rbRaw.IsChecked = true;
						break;

					case PuttyProtocol.LoadSession:
						throw new ArgumentException("LoadSession is not supported!", nameof(args.Protocol));
				}
			}
		}

		private void rbSSH_Checked(object sender, RoutedEventArgs e)
		{
			this.numericPort.Value = 22;
		}

		private void rbTelnet_Checked(object sender, RoutedEventArgs e)
		{
			this.numericPort.Value = 23;
		}

		private void rbSerial_Checked(object sender, RoutedEventArgs e)
		{
			this.tbSerialLine.Text = "COM1";
		}

		private void rbRlogin_Checked(object sender, RoutedEventArgs e)
		{
			this.numericPort.Value = 513;
		}

		private void rbRaw_Checked(object sender, RoutedEventArgs e)
		{
			this.numericPort.Value = this.numericPort.Minimum;
		}

		private void btnOpenPrivateKey_Click(object sender, RoutedEventArgs e)
		{
			var ofd = new OpenFileDialog
			{
				Filter = "PuTTY Private Key (*.ppk)|*.ppk",
				Multiselect = false,
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};
			if (ofd.ShowDialog().Value)
				this.tbPrivateKey.Text = ofd.FileName;
		}
	}
}
