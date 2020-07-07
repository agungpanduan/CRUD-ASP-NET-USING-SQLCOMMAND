using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UploadExcel.Common.Config;
using UploadExcel.Common.Database.Petapoco;

namespace UploadExcel.Common.Database
{
    public class DBConnectionManager
    {
        private readonly static string SQL_FILE_FOLDER = "SQL";

        public DBConnectionConfig DbConfig { get; set; }

        public IDBContextManager DBContextManager { get; set; }

        private static IDictionary<string, DBConnectionManager> instanceDic = new Dictionary<string, DBConnectionManager>();

        public static DBConnectionManager GetInstance(string dbKey)
        {
            if (!instanceDic.ContainsKey(dbKey))
            {
                instanceDic.Add(dbKey, new DBConnectionManager(dbKey));
            }

            return instanceDic[dbKey];
        }

        private DBConnectionManager() { }

        private DBConnectionManager(string dbKey)
        {
            this.Load(dbKey);
        }

        private void Load(string dbKey)
        {
            if (String.IsNullOrEmpty(dbKey))
            {
                throw new NullReferenceException("parameter dbKey is null/empty");
            }

            DBConnectionConfigManager dbConfigManager = DBConnectionConfigManager.Instance;

            DbConfig = dbConfigManager.ConfigDictionary[dbKey];

            ConnectionDescriptor conDesc = new ConnectionDescriptor();
            conDesc.ProviderName = DbConfig.Provider;
            conDesc.IsDefault = DbConfig.IsDefault;
            conDesc.ConnectionString = DbConfig.ConnectionString;
            conDesc.Name = DbConfig.Key;

            ISqlLoader sqlLoader = new FileSqlLoader(SQL_FILE_FOLDER);

            DBContextManager = new PetaPocoContextManager(
                new ISqlLoader[] { sqlLoader }, new ConnectionDescriptor[] { conDesc });
        }
    }
}