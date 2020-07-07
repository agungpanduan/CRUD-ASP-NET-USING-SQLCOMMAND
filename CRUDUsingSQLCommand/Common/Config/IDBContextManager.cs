using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UploadExcel.Common.Database;

namespace UploadExcel.Common.Config
{
    public interface IDBContextManager
    {
        void AddConnectionDescriptor(ConnectionDescriptor connectionDescriptor);
        void AddSqlLoader(ISqlLoader sqlLoader);
        void Close();
        ConnectionDescriptor GetConnectionDescriptor(string name);
        IList<ConnectionDescriptor> GetConnectionDescriptors();
        IDBContext GetContext();
        IDBContext GetContext(string name);
        DBContextExecutionMode GetContextExecutionMode();
        ConnectionDescriptor GetDefaultConnectionDescriptor();
        IList<ISqlLoader> GetSqlLoaders();
        string LoadSqlScript(string name);
        void RemoveSqlLoader(ISqlLoader sqlLoader);
        void SetConnectionDescriptor(params ConnectionDescriptor[] connectionDescriptors);
        void SetContextExecutionMode(DBContextExecutionMode contextExecutionMode);
        void SetDefaultConnectionDescriptor(ConnectionDescriptor connectionDescriptor);
        void SetDefaultConnectionDescriptor(string name);
    }
}