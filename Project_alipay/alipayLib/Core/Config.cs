using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace alipay.Lib.Core
{
    public class Config
    {
        public Config() : this("")
        {

        }

        public Config(string sign)
        {
            
            app_id = ConfigurationManager.AppSettings[sign + "app_id"];
            privateKey = ConfigurationManager.AppSettings[sign + "privateKey"];
            publicKey = ConfigurationManager.AppSettings[sign + "publicKey"];
        }

        public string app_id { get; private set; }
        public string privateKey { get; private set; }
        public string publicKey { get; private set; }
    }
}
