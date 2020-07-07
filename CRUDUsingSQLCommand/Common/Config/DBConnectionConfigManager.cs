using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace UploadExcel.Common.Config
{
    public class DBConnectionConfigManager
    {
        private readonly static string XML_NODE_CONFIGURATION = "configuration";
        private readonly static string XML_NODE_VALUE = "value";
        private readonly static string XML_NODE_KEY = "key";
        private readonly static string XML_NODE_CONNECTION_STRING = "ConnectionString";
        private readonly static string XML_NODE_PROVIDER = "Provider";
        private readonly static string XML_NODE_IS_DEFAULT = "IsDefault";
        private readonly static string XML_NODE_DESCRIPTION = "description";

        public string ConfigFile { get; set; }

        public IDictionary<string, DBConnectionConfig> ConfigDictionary { get; set; }

        private static DBConnectionConfigManager instance;

        public static DBConnectionConfigManager Instance
        {
            get
            {
                if (instance == null)
                {
                    string fileName = "Database.config";
                    // store the file inside ~/App_Data/uploads folder
                    string path = Path.Combine(HttpRuntime.AppDomainAppPath + ("/Configurations/"), fileName);
                    instance = new DBConnectionConfigManager(path);
                    //instance = new DBConnectionConfigManager("Configuration/Database.config");
                }

                return instance;
            }
        }

        private DBConnectionConfigManager() { }

        private DBConnectionConfigManager(string configFile)
        {
            this.ConfigFile = configFile;
            this.ConfigDictionary = new Dictionary<string, DBConnectionConfig>();
            this.Load();
        }

        public void Reload()
        {
            this.ConfigDictionary.Clear();
            this.Load();
        }

        private void Load()
        {
            if (String.IsNullOrEmpty(ConfigFile))
            {
                throw new NullReferenceException("parameter ConfigFile is null/empty");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFile);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/" + XML_NODE_CONFIGURATION);

            foreach (XmlNode nodeLoop in node.ChildNodes)
            {
                DBConnectionConfig config = new DBConnectionConfig();

                string key = nodeLoop.SelectSingleNode(XML_NODE_KEY).InnerText;
                config.Key = key;

                XmlNode nodeValue = nodeLoop.SelectSingleNode(XML_NODE_VALUE);
                config.ConnectionString = nodeValue.SelectSingleNode(XML_NODE_CONNECTION_STRING).InnerText;
                config.Provider = nodeValue.SelectSingleNode(XML_NODE_PROVIDER).InnerText;
                config.IsDefault = String.Equals(Boolean.TrueString, nodeValue.SelectSingleNode(XML_NODE_IS_DEFAULT).InnerText, StringComparison.OrdinalIgnoreCase);
                config.Description = nodeLoop.SelectSingleNode(XML_NODE_DESCRIPTION).InnerText;

                ConfigDictionary.Add(key, config);
            }
        }
    }
}