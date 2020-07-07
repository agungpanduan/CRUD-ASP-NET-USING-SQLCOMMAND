using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UploadExcel.Common.Config;

namespace UploadExcel.Common.Database
{
    public class BaseContextManager : IDBContextManager
    {
        public BaseContextManager(ISqlLoader[] sqlLoaders, ConnectionDescriptor[] connectionDescriptors);

        public void AddConnectionDescriptor(ConnectionDescriptor connectionDescriptor);
        public void AddSqlLoader(ISqlLoader queryLoader);
        public abstract void Close();
        public ConnectionDescriptor GetConnectionDescriptor(string name);
        public IList<ConnectionDescriptor> GetConnectionDescriptors();
        public abstract IDBContext GetContext();
        public abstract IDBContext GetContext(string name);
        public DBContextExecutionMode GetContextExecutionMode();
        public ConnectionDescriptor GetDefaultConnectionDescriptor();
        public IList<ISqlLoader> GetSqlLoaders();
        public string LoadSqlScript(string name);
        public void RemoveSqlLoader(ISqlLoader sqlLoader);
        public void SetConnectionDescriptor(params ConnectionDescriptor[] connectionDescriptors);
        public void SetContextExecutionMode(DBContextExecutionMode contextExecutionMode);
        public void SetDefaultConnectionDescriptor(ConnectionDescriptor connectionDescriptor);
        public void SetDefaultConnectionDescriptor(string name);
    }
}