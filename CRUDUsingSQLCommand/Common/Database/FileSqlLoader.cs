using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel.Common.Database
{
    public class FileSqlLoader : BaseFileSqlLoader
    {
        public FileSqlLoader(string rootPath);

        public override string LoadScript(string name);
    }
}