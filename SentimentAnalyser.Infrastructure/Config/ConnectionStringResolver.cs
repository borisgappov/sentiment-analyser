using System;
using Microsoft.Extensions.Configuration;

namespace SentimentAnalyser.Infrastructure.Config
{
    public class ConnectionStringResolver
    {
        private const string DefaultConnectionStringName = "DefaultConnection";

        private readonly IConfigurationRoot _config;
        private string _connectionString;

        public ConnectionStringResolver(IConfigurationRoot config)
        {
            _config = config;
        }

        public string GetConnectionString()
        {
            if (_connectionString == null)
            {
                var stringSettings = _config.GetConnectionString(Environment.MachineName) ??
                                     _config.GetConnectionString(DefaultConnectionStringName);

                _connectionString = stringSettings;
            }

            return _connectionString;
        }
    }
}