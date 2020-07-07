using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel.Common.Database
{
    public class IDBContext
    {
        void AbortTransaction();
        void BeginTransaction();
        void Close();
        void CommitTransaction();
        int Execute(string sqlContext, params object[] args);
        int Execute(string sqlContext, Action<IList<SqlExtensionScript>> appenders, params object[] args);
        int Execute(string sqlContext, Action<IList<SqlExtensionScript>> prependers, Action<IList<SqlExtensionScript>> appenders, params object[] args);
        T ExecuteScalar<T>(string sqlContext, params object[] args);
        IList<T> Fetch<T>(string sqlContext, params object[] args);
        IPagedData<T> FetchByPage<T>(string sqlContext, long pageNumber, long pageSize, params object[] args);
        IPagedData<T> FetchByPage<T>(string sqlContext, long pageNumber, long pageSize, Action<IList<SqlExtensionScript>> appenders, params object[] args);
        IPagedData<T> FetchByPage<T>(string sqlContext, long pageNumber, long pageSize, Action<IList<SqlExtensionScript>> prependers, Action<IList<SqlExtensionScript>> appenders, params object[] args);
        DBContextExecutionMode GetExecutionMode();
        string GetName();
        IList<ISqlLoader> GetSqlLoaders();
        string LoadSqlScript(string name);
        IEnumerable<T> Query<T>(string sqlContext, params object[] args);
        void SetCommandTimeout(int seconds);
        void SetExecutionMode(DBContextExecutionMode executionMode);
        void SetOneTimeCommandTimeout(int seconds);
        T SingleOrDefault<T>(string sqlContext, params object[] args);
    }
}