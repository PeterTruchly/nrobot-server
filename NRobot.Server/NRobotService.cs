using System;
using log4net;
using NRobot.Server.Config;
using NRobot.Server.Domain;
using NRobot.Server.Services;

namespace NRobot.Server
{
	/// <summary>
	/// The overall NRobot service
	/// </summary>
	public class NRobotService
	{
		
		private static readonly ILog Log = LogManager.GetLogger(typeof(NRobotService));
        private HttpService _httpservice;
	    private KeywordManager _keywordManager;
	    private XmlRpcService _rpcService;
	    private NRobotServerConfig _config;

		public NRobotService(NRobotServerConfig config) 
        {
            if (config == null) throw new Exception("No configuration specified");
		    _config = config;
            _keywordManager = new KeywordManager();
            _rpcService = new XmlRpcService(_keywordManager);
            _httpservice = new HttpService(_rpcService, _keywordManager, config.Port);
            LoadKeywords();
        }
		
		/// <summary>
		/// Loads the keyword libraries
		/// </summary>
		private void LoadKeywords()
		{
			Log.Debug("Loading keywords");
			try
			{
				foreach(var libraryconfig in _config.AssemblyConfigs)
				{
                    _keywordManager.AddLibrary(libraryconfig);
				}
			}
			catch (Exception e)
			{
				Log.Error($"Unable to load all configured keywords, {e.Message}");
				throw;
			}
			
		}
		
		/// <summary>
		/// Starts HTTP service
		/// </summary>
		public void StartAsync()
		{
			_httpservice.StartAsync();
            Log.Debug("HTTP listener started");
		}

	    /// <summary>
	    /// Stops the service sync
	    /// </summary>
	    public void Stop()
	    {
	        _httpservice.Stop();
	        Log.Debug("HTTP listener stopped");
	    }
	}
}
