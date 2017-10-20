using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace umbShop.Models.Stock
{
    public class UmbShopStockRepository
    {

        public string AddProductsToStock(string productId, string variantId, string count)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopStock.TableName))
            {
                db.CreateTable<UmbShopStock>(false);
            }

            Guid productUniqueId = Guid.Empty;
            Guid.TryParse(productId, out productUniqueId);

            Guid variantUniqueId = Guid.Empty;
            Guid.TryParse(variantId, out variantUniqueId);

            int countInt = 0;
            int.TryParse(count, out countInt);

            for (int i = 1; i <= countInt; i++)
            {
                UmbShopStock stock = new UmbShopStock
                {
                    UniqueId = Guid.NewGuid().ToString(),
                    ProductUniqueId = productUniqueId.ToString(),
                    VariantUniqueId = variantUniqueId.ToString(),
                    BasketUniqueId = ""
                };

                databaseContext.Database.Insert(stock);
            }

            return "done";
        }

        public string RemoveProductsFromStock(string productId, string variantId, string count)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopStock.TableName))
            {
                db.CreateTable<UmbShopStock>(false);
            }

            Guid productUniqueId = Guid.Empty;
            Guid.TryParse(productId, out productUniqueId);

            Guid variantUniqueId = Guid.Empty;
            Guid.TryParse(variantId, out variantUniqueId);

            int countInt = 0;
            int.TryParse(count, out countInt);

            List<UmbShopStock> stocks = databaseContext.Database.Fetch<UmbShopStock>("SELECT TOP @2 * FROM " + UmbShopStock.TableName + " WHERE ProductUniqueId = @0 AND VariantUniqueId = @1 AND BasketUniqueId = '';", productUniqueId, variantUniqueId, count);

            databaseContext.Database.Delete<UmbShopStock>(stocks);

            //for (int i = 1; i <= countInt; i++)
            //{
            //}

            return "done";
        }

        public int CountProductsInStock(string productId, string variantId)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopStock.TableName))
            {
                db.CreateTable<UmbShopStock>(false);
            }

            Guid productUniqueId = Guid.Empty;
            Guid.TryParse(productId, out productUniqueId);

            Guid variantUniqueId = Guid.Empty;
            Guid.TryParse(variantId, out variantUniqueId);

            int count = databaseContext.Database.ExecuteScalar<int>("SELECT COUNT(*) FROM " + UmbShopStock.TableName + " WHERE ProductUniqueId = @0 AND VariantUniqueId = @1 AND BasketUniqueId = '';", productUniqueId, variantUniqueId);

            return count;
        }

    }
}
