using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using NRobot.Server.XmlConfig;

namespace NRobot.Server.Config
{
	/// <summary>
	/// Main configuration for NRobot service
	/// </summary>
	public class NRobotServerConfig
	{
	    public const int PortDefaultValue = 8271;
	    public int Port { get; }

	    private readonly List<LibraryConfig> assemblyConfigs = new List<LibraryConfig>();
        public IEnumerable<LibraryConfig> AssemblyConfigs => assemblyConfigs;

	    public NRobotServerConfig(Assembly assembly, Type t, int port = PortDefaultValue)
	    {
	        Port = port;
	        assemblyConfigs.Add(new LibraryConfig() { Assembly = assembly.FullName, TypeName = t.FullName });
        }

	    public NRobotServerConfig(IEnumerable<Tuple<Assembly, Type>> libraries, int port = PortDefaultValue)
	    {
	        Port = port;
	        foreach (var lib in libraries)
            {
                assemblyConfigs.Add(new LibraryConfig() { Assembly = lib.Item1.FullName, TypeName = lib.Item2.FullName });
            }
	    }

        public NRobotServerConfig(Type t, int port = PortDefaultValue)
        {
            Port = port;
	        assemblyConfigs.Add(new LibraryConfig() { Assembly = Assembly.GetExecutingAssembly().FullName, TypeName = t.FullName });
        }

	    public NRobotServerConfig(IEnumerable<Type> types, int port = PortDefaultValue)
	    {
	        Port = port;
	        foreach (var type in types)
            {
                assemblyConfigs.Add(new LibraryConfig() { Assembly = Assembly.GetExecutingAssembly().FullName, TypeName = type.FullName });
            }
	    }

	    public NRobotServerConfig(LibraryConfig library, int port = PortDefaultValue)
	    {
	        Port = port;
            assemblyConfigs.Add(library);
	    }

        public NRobotServerConfig(IEnumerable<LibraryConfig> libraries, int port = PortDefaultValue)
	    {
	        Port = port;
	        foreach (var lib in libraries)
            {
	            assemblyConfigs.Add(lib);
	        }
	    }

        public NRobotServerConfig(NRobotServerConfiguration xmlconfig)
        {
            //get port number
            Port = int.Parse(xmlconfig.Port.Number);

            //get keyword assemblies
            foreach (AssemblyElement xmlasm in xmlconfig.Assemblies)
            {
                LibraryConfig config = new LibraryConfig() { Assembly = xmlasm.Name, TypeName = xmlasm.Type, Documentation = xmlasm.DocFile };
                if (string.IsNullOrEmpty(config.TypeName)) throw new ConfigurationErrorsException("Config has not Type defined");
                assemblyConfigs.Add(config);
            }
        }
	}
}
