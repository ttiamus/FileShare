using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.MongoDb
{
    public class MongoConfiguration
    {
        private readonly  NameValueCollection mongoConfigSection = (NameValueCollection)ConfigurationManager.GetSection("MongoConfig");

        public string ConnectionString => string.Format(mongoConfigSection["MongoConnectionString"], Username, Password);
        public string Username => mongoConfigSection["MongoUsername"];
        public string Password => mongoConfigSection["MongoPassword"];
    }
}
