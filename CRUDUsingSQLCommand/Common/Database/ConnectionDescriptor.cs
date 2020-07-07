using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel.Common.Database
{
    public class ConnectionDescriptor
    {
        public ConnectionDescriptor();
        public ConnectionDescriptor(string name, string connectionString);
        public ConnectionDescriptor(string name, string connectionString, string providerName);

        public string ConnectionString { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }

        public override string ToString();
    }
}