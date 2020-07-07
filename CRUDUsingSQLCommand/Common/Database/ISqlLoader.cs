using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel.Common.Database
{
    public interface ISqlLoader
    {
        string LoadScript(string name);
    }
}