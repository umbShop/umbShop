using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using UmbShop.Models.Stock;

namespace UmbShop.Models.Basket
{
    public class UmbShopBasketRepository
    {

        public string GetBasket()
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopBasket.TableName))
            {
                db.CreateTable<UmbShopBasket>(false);
            }

            var uniqueId = Guid.NewGuid().ToString();
            try
            {
                LogHelper.Info<UmbShopStockRepository>("BEGIN GetBasket");
                while (databaseContext.Database.ExecuteScalar<int>("SELECT COUNT(*) FROM " + UmbShopBasket.TableName + " WHERE UniqueId = @0;", uniqueId) > 0)
                {
                    uniqueId = Guid.NewGuid().ToString();
                }

                UmbShopBasket basket = new UmbShopBasket
                {
                    UniqueId = uniqueId,
                    LastUsed = DateTime.Now,
                    Status = 0
                };

                databaseContext.Database.Insert(basket);
                LogHelper.Info<UmbShopStockRepository>("END GetBasket");
            }
            catch (Exception exception)
            {
                LogHelper.Info<UmbShopStockRepository>("ERROR GetBasket " + exception.Message);
                return "";
            }

            return uniqueId;
        }

        public bool KeepBasketAlive(string basketId)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopBasket.TableName))
            {
                db.CreateTable<UmbShopBasket>(false);
            }

            Guid basketUniqueId = Guid.Empty;
            Guid.TryParse(basketId, out basketUniqueId);

            try
            {
                LogHelper.Info<UmbShopBasketRepository>("BEGIN KeepBasketAlive basketId:" + basketId);
                UmbShopBasket basket = databaseContext.Database.Fetch<UmbShopBasket>("SELECT TOP 1 * FROM " + UmbShopBasket.TableName + " WHERE UniqueId = @0;", basketUniqueId).FirstOrDefault();
                basket.LastUsed = DateTime.Now;
                databaseContext.Database.Update(basket);
                LogHelper.Info<UmbShopBasketRepository>("END KeepBasketAlive basketId:" + basketId);
            }
            catch (Exception exception)
            {
                LogHelper.Info<UmbShopBasketRepository>("ERROR KeepBasketAlive " + exception.Message);
                return false;
            }

            return true;
        }

        public UmbShopStock[] GetBasketContent(string basketId)
        {
            if (!KeepBasketAlive(basketId))
            {
                return null;
            }

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
            if (!KeepBasketAlive(basketId))
            {
                return false;
            }

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

            try
            {
                LogHelper.Info<UmbShopBasketRepository>("BEGIN AddProductsToBasket basketId:" + basketId + " productId:" + productId + " variantId:" + variantId + " count:" + count);
                for (int i = 1; i <= countInt; i++)
                {
                    UmbShopStock stock = databaseContext.Database.Fetch<UmbShopStock>("SELECT TOP 1 * FROM " + UmbShopStock.TableName + " WHERE ProductUniqueId = @0 AND VariantUniqueId = @1 AND BasketUniqueId = '';", productUniqueId, variantUniqueId).FirstOrDefault();
                    stock.BasketUniqueId = basketUniqueId.ToString();
                    databaseContext.Database.Update(stock);
                }
                LogHelper.Info<UmbShopBasketRepository>("END AddProductsToBasket basketId:" + basketId + " productId:" + productId + " variantId:" + variantId + " count:" + count);
            }
            catch (Exception exception)
            {
                LogHelper.Info<UmbShopBasketRepository>("ERROR AddProductsToBasket " + exception.Message);
                return false;
            }

            return true;
        }

        public bool RemoveProductsFromBasket(string basketId, string productId, string variantId, string count)
        {
            if (!KeepBasketAlive(basketId))
            {
                return false;
            }

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

            try
            {
                LogHelper.Info<UmbShopBasketRepository>("BEGIN RemoveProductsFromBasket basketId:" + basketId + " productId:" + productId + " variantId:" + variantId + " count:" + count);
                for (int i = 1; i <= countInt; i++)
                {
                    UmbShopStock stock = databaseContext.Database.Fetch<UmbShopStock>("SELECT TOP 1 * FROM " + UmbShopStock.TableName + " WHERE ProductUniqueId = @0 AND VariantUniqueId = @1 AND BasketUniqueId = @2;", productUniqueId, variantUniqueId, basketUniqueId).FirstOrDefault();
                    stock.BasketUniqueId = "";
                    databaseContext.Database.Update(stock);
                }
                LogHelper.Info<UmbShopBasketRepository>("END RemoveProductsFromBasket basketId:" + basketId + " productId:" + productId + " variantId:" + variantId + " count:" + count);
            }
            catch (Exception exception)
            {
                LogHelper.Info<UmbShopBasketRepository>("ERROR RemoveProductsFromBasket " + exception.Message);
                return false;
            }

            return true;
        }

        public UmbShopBasket[] ClearUnusedBaskets()
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopBasket.TableName))
            {
                db.CreateTable<UmbShopBasket>(false);
            }

            List<UmbShopBasket> basketsToDelete = new List<UmbShopBasket>();
            try
            {
                LogHelper.Info<UmbShopBasketRepository>("BEGIN ClearUnusedBaskets");

                double minutes = 30;
                double.TryParse(WebConfigurationManager.AppSettings["UmbShop.ClearUnusedBaskets.Minutes"], out minutes);

                UmbShopBasket[] baskets = databaseContext.Database.Fetch<UmbShopBasket>("SELECT * FROM " + UmbShopBasket.TableName + " WHERE Status = 0;").ToArray();
                foreach (UmbShopBasket basket in baskets)
                {
                    if (basket.LastUsed < DateTime.Now.AddMinutes(-minutes))
                    {
                        basketsToDelete.Add(basket);
                    }
                }

                foreach (UmbShopBasket basket in basketsToDelete)
                {
                    List<UmbShopStock> stockList = databaseContext.Database.Fetch<UmbShopStock>("SELECT * FROM " + UmbShopStock.TableName + " WHERE BasketUniqueId = @0;", basket.UniqueId);
                    foreach (UmbShopStock stock in stockList)
                    {
                        stock.BasketUniqueId = "";
                        databaseContext.Database.Update(stock);
                    }

                    databaseContext.Database.Delete<UmbShopBasket>(basket);
                }

                LogHelper.Info<UmbShopBasketRepository>("END ClearUnusedBaskets");
            }
            catch (Exception exception)
            {
                LogHelper.Info<UmbShopBasketRepository>("ERROR ClearUnusedBaskets " + exception.Message);
                return null;
            }

            return basketsToDelete.ToArray();
        }

    }
}
