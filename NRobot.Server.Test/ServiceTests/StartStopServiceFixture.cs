using NRobot.Server.Config;
using NRobot.Server.Domain;
using NUnit.Framework;
using System.Collections.Generic;

namespace NRobot.Server.Test.ServiceTests
{

#pragma warning disable 1591

    /// <summary>
    /// Tests to start and stop the service with different configurations
    /// </summary>
    [TestFixture]
    public class StartStopServiceFixture
    {

        private NRobotService _service;

        [TearDown]
        public void TearDown()
        {
            if (_service != null)
            {
                _service.Stop();
            }
        }


        [Test]
        public void StartService_SingleType()
        {
            var config = new NRobotServerConfig(new LibraryConfig() {
                Assembly = "NRobot.Server.Test",
                TypeName = "NRobot.Server.Test.Keywords.TestKeywords",
                Documentation = "NRobot.Server.Test.xml"
            }, 8270);            
            _service = new NRobotService(config);
            _service.StartAsync();
        }

        [Test]
        public void StartService_MultipleTypes()
        {
            var config = new NRobotServerConfig(new List<LibraryConfig>(){
                new LibraryConfig() {
                    Assembly = "NRobot.Server.Test",
                    TypeName = "NRobot.Server.Test.Keywords.TestKeywords",
                    Documentation = "NRobot.Server.Test.xml"
                },
                new LibraryConfig() {
                    Assembly = "NRobot.Server.Test",
                    TypeName = "NRobot.Server.Test.Keywords.RunKeyword",
                    Documentation = "NRobot.Server.Test.xml"
                }
            }, 8270);            
            _service = new NRobotService(config);
            _service.StartAsync();
        }

        [Test]
        public void StartService_NoDocumentation()
        {
            var config = new NRobotServerConfig(new LibraryConfig() {
                Assembly = "NRobot.Server.Test",
                TypeName = "NRobot.Server.Test.Keywords.TestKeywords"
            }, 8270);            
            _service = new NRobotService(config);
            _service.StartAsync();
        }

        [ExpectedException(typeof(KeywordLoadingException))]
        [Test]
        public void StartService_InvalidAssembly()
        {
            var config = new NRobotServerConfig(new LibraryConfig() {
                Assembly = "NRobot.Server.TestUnknown",
                TypeName = "NRobot.Server.Test.Keywords.TestKeywords",
                Documentation = "NRobot.Server.Test.xml"
            }, 8270);            
            _service = new NRobotService(config);
            _service.StartAsync();
        }

        [ExpectedException(typeof(KeywordLoadingException))]
        [Test]
        public void StartService_InvalidType()
        {
            var config = new NRobotServerConfig(new LibraryConfig() {
                Assembly = "NRobot.Server.Test",
                TypeName = "NRobot.Server.Test.Keywords.TestKeywordsUnknown",
                Documentation = "NRobot.Server.Test.xml"
            }, 8270);            
            _service = new NRobotService(config);
            _service.StartAsync();
        }

        [ExpectedException(typeof(KeywordLoadingException))]
        [Test]
        public void StartService_InvalidDocumentation()
        {
            var config = new NRobotServerConfig(new LibraryConfig() {
                Assembly = "NRobot.Server.Test",
                TypeName = "NRobot.Server.Test.Keywords.TestKeywords",
                Documentation = "NRobot.Server.TestUnknown.XML"
            }, 8270);           
            _service = new NRobotService(config);
            _service.StartAsync();
        }


    }

#pragma warning restore 1591

}
