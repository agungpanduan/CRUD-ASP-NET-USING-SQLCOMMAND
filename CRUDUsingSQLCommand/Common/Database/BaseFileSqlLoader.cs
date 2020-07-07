using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel.Common.Database
{
    public abstract class BaseFileSqlLoader : ISqlLoader
    {
        protected const string EXTENSION = "sql";

        public BaseFileSqlLoader(string rootPath);

        public string RootPath { get; set; }

        public abstract string LoadScript(string name);
    }
}