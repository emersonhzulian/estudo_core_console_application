using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleUI
{


    public class Test : ITest
    {
        private readonly ILogger<Test> _log;
        private readonly IConfiguration _config;

        public Test(ILogger<Test> log, IConfiguration config)
        {
            this._log = log;
            this._config = config;
        }

        public void Run()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                _log.LogInformation("xpto {i}", i);
            }
        }
    }
}
