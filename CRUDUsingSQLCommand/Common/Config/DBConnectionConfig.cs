using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel.Common.Config
{
    public class DBConnectionConfig
    {
        public string Key { get; set; }
        public string Description { get; set; }

        // value
        public string Provider { get; set; }
        public string ConnectionString { get; set; }
        public bool IsDefault { get; set; }
    }
}