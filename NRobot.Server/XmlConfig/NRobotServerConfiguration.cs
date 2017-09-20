using System.Configuration;

namespace NRobot.Server.XmlConfig
{
	/// <inheritdoc />
	/// <summary>
	/// Configuration section handler
	/// </summary>
	public class NRobotServerConfiguration : ConfigurationSection
	{
		
		private const string CConfigSection = "NRobotServerConfiguration";
		
		public static NRobotServerConfiguration GetConfig()
		{
			return (NRobotServerConfiguration)ConfigurationManager.GetSection(CConfigSection) ?? new NRobotServerConfiguration();
		}
		
		[ConfigurationProperty("assemblies")]
		public AssemblyElementCollection Assemblies => (AssemblyElementCollection)this["assemblies"] ?? new AssemblyElementCollection();

	    [ConfigurationProperty("port")]
		public PortElement Port => (PortElement)this["port"] ?? new PortElement();
	}
}
