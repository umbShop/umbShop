using System;
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

            int productIdInt = 0;
            int.TryParse(productId, out productIdInt);

            int variantIdInt = 0;
            int.TryParse(variantId, out variantIdInt);

            int countInt = 0;
            int.TryParse(count, out countInt);

            UmbShopStock stock = new UmbShopStock
            {
                UniqueId = Guid.NewGuid().ToString(),
                ProductId = productIdInt,
                ProductUniqueId = "",
                VariantId = variantIdInt,
                VariantUniqueId = "",
                BasketId = 0,
                BasketUniqueId = ""
            };

            for (int i = 1; i <= countInt; i++)
            {
                databaseContext.Database.Insert(stock);
            }

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

            int productIdInt = 0;
            int.TryParse(productId, out productIdInt);

            int variantIdInt = 0;
            int.TryParse(variantId, out variantIdInt);

            int count = databaseContext.Database.ExecuteScalar<int>("SELECT COUNT(*) FROM " + UmbShopStock.TableName + " WHERE ProductId = @0 AND VariantId = @1;" , productIdInt, variantIdInt);

            return count;
        }

    }
}
