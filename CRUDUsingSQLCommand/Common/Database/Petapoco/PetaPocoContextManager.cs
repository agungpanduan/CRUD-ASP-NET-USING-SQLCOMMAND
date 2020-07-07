using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UploadExcel.Common.Database;

namespace UploadExcel.Common.Database.Petapoco
{
    public class PetaPocoContextManager : BaseContextManager
    {
        public PetaPocoContextManager(ISqlLoader[] sqlLoaders, ConnectionDescriptor[] connectionDescriptors);

        public override void Close();
        public override IDBContext GetContext();
        public override IDBContext GetContext(string name);
    }
}