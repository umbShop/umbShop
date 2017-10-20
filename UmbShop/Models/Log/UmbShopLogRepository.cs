using System;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace UmbShop.Models.Log
{
    public class UmbShopLogRepository
    {

        public void Add(string text)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopLog.TableName))
            {
                db.CreateTable<UmbShopLog>(false);
            }

            try
            {
                UmbShopLog log = new UmbShopLog
                {
                    Created = DateTime.Now,
                    Text = text
                };

                databaseContext.Database.Insert(log);
            }
            catch
            {
            }
        }

    }
}
