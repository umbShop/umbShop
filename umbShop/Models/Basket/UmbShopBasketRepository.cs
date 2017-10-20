using System;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using umbShop.Models.Stock;

namespace umbShop.Models.Basket
{
    public class UmbShopBasketRepository
    {

        public UmbShopStock[] GetBasketContent(string basketId)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopStock.TableName))
            {
                db.CreateTable<UmbShopStock>(false);
            }

            Guid basketUniqueId = Guid.Empty;
            Guid.TryParse(basketId, out basketUniqueId);

            UmbShopStock[] stockList = databaseContext.Database.Fetch<UmbShopStock>("SELECT * FROM " + UmbShopStock.TableName + " WHERE BasketUniqueId = @0;", basketUniqueId).ToArray();

            return stockList;
        }

        public bool AddProductsToBasket(string basketId, string productId, string variantId, string count)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopStock.TableName))
            {
                db.CreateTable<UmbShopStock>(false);
            }

            Guid basketUniqueId = Guid.Empty;
            Guid.TryParse(basketId, out basketUniqueId);

            Guid productUniqueId = Guid.Empty;
            Guid.TryParse(productId, out productUniqueId);

            Guid variantUniqueId = Guid.Empty;
            Guid.TryParse(variantId, out variantUniqueId);

            int countInt = 0;
            int.TryParse(count, out countInt);

            for (int i = 1; i <= countInt; i++)
            {
                UmbShopStock stock = databaseContext.Database.Fetch<UmbShopStock>("SELECT TOP 1 * FROM " + UmbShopStock.TableName + " WHERE ProductUniqueId = @0 AND VariantUniqueId = @1 AND BasketUniqueId = '';", productUniqueId, variantUniqueId).FirstOrDefault();
                stock.BasketUniqueId = basketUniqueId.ToString();
                databaseContext.Database.Update(stock);
            }

            return true;
        }

        public bool RemoveProductsFromBasket(string basketId, string productId, string variantId, string count)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopStock.TableName))
            {
                db.CreateTable<UmbShopStock>(false);
            }

            Guid basketUniqueId = Guid.Empty;
            Guid.TryParse(basketId, out basketUniqueId);

            Guid productUniqueId = Guid.Empty;
            Guid.TryParse(productId, out productUniqueId);

            Guid variantUniqueId = Guid.Empty;
            Guid.TryParse(variantId, out variantUniqueId);

            int countInt = 0;
            int.TryParse(count, out countInt);

            for (int i = 1; i <= countInt; i++)
            {
                UmbShopStock stock = databaseContext.Database.Fetch<UmbShopStock>("SELECT TOP 1 * FROM " + UmbShopStock.TableName + " WHERE ProductUniqueId = @0 AND VariantUniqueId = @1 AND BasketUniqueId = @2;", productUniqueId, variantUniqueId, basketUniqueId).FirstOrDefault();
                stock.BasketUniqueId = "";
                databaseContext.Database.Update(stock);
            }

            return true;
        }

    }
}
