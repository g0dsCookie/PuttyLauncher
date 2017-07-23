using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CookieProjects.PuttyLauncher
{
	public class Config
	{
		static Config _GlobalConfig = null;

		public static Config GlobalConfig
		{
			get
			{
				if (_GlobalConfig == null)
				{
					_GlobalConfig = Load("PuttyLauncher.xml");
				}
				return _GlobalConfig;
			}
		}

		public bool LoadSessions { get; set; }

		public List<string> IgnoreSessions { get; set; } = new List<string>();

		public List<string> PrivateKeys { get; set; } = new List<string>();

		public void Save(string file)
		{
			var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
			var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

			var serializer = new XmlSerializer(typeof(Config));
			using (var xml = XmlWriter.Create(file, settings))
				serializer.Serialize(xml, this);
		}

		public static Config Load(string file)
		{
			if (!File.Exists(file))
			{
				var config = new Config
				{
					LoadSessions = true
				};
				return config;
			}

			var serializer = new XmlSerializer(typeof(Config));
			using (var stream = new StreamReader(file))
				return serializer.Deserialize(stream) as Config;
		}
	}
}
