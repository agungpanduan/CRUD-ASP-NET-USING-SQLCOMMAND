using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel.Common.Database
{
    public class SqlExtensionScript
    {
        public SqlExtensionScript();
        public SqlExtensionScript(bool enabled, string context);
        public SqlExtensionScript(bool enabled, string context, bool appendNewLine);

        public bool AppendNewLine { get; set; }
        public string Context { get; set; }
        public bool Enabled { get; set; }
    }
}