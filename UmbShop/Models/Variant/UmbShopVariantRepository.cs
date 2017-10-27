using System;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using UmbShop.Models.Stock;

namespace UmbShop.Models.Variant
{
    public class UmbShopVariantRepository
    {

        public UmbShopVariant[] GetVariants(string productId)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopVariant.TableName))
            {
                db.CreateTable<UmbShopVariant>(false);
            }

            Guid productUniqueId = Guid.Empty;
            Guid.TryParse(productId, out productUniqueId);

            UmbShopVariant[] variants = databaseContext.Database.Fetch<UmbShopVariant>("SELECT * FROM " + UmbShopVariant.TableName + " WHERE ProductUniqueId = @0;", productUniqueId).ToArray();

            UmbShopStockRepository umbShopStockRepository = new UmbShopStockRepository();
            foreach (UmbShopVariant variant in variants)
            {
                var count = umbShopStockRepository.CountProductsInStock(variant.ProductUniqueId, variant.UniqueId);
                variant.Count = count;
            }

            return variants;
        }

        public bool AddVariant(string productId, string name)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopVariant.TableName))
            {
                db.CreateTable<UmbShopVariant>(false);
            }

            Guid productUniqueId = Guid.Empty;
            Guid.TryParse(productId, out productUniqueId);

            try
            {
                LogHelper.Info<UmbShopVariantRepository>("BEGIN AddVariant productId:" + productId + " name:" + name);

                var uniqueId = Guid.NewGuid().ToString();
                while (databaseContext.Database.ExecuteScalar<int>("SELECT COUNT(*) FROM " + UmbShopVariant.TableName + " WHERE UniqueId = @0;", uniqueId) > 0)
                {
                    uniqueId = Guid.NewGuid().ToString();
                }

                UmbShopVariant variant = new UmbShopVariant
                {
                    UniqueId = uniqueId,
                    ProductUniqueId = productUniqueId.ToString(),
                    Name = name
                };

                databaseContext.Database.Insert(variant);
                LogHelper.Info<UmbShopVariantRepository>("END AddVariant productId:" + productId + " name:" + name);
            }
            catch (Exception exception)
            {
                LogHelper.Info<UmbShopVariantRepository>("ERROR AddVariant " + exception.Message);
                return false;
            }

            return true;
        }

        public bool UpdateVariant(string productId, string variantId, string name)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(databaseContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, databaseContext.SqlSyntax);

            if (!db.TableExist(UmbShopVariant.TableName))
            {
                db.CreateTable<UmbShopVariant>(false);
            }

            Guid productUniqueId = Guid.Empty;
            Guid.TryParse(productId, out productUniqueId);

            Guid variantUniqueId = Guid.Empty;
            Guid.TryParse(variantId, out variantUniqueId);

            try
            {
                LogHelper.Info<UmbShopVariantRepository>("BEGIN UpdateVariant productId:" + productId + " variantId:" + variantId + " name:" + name);

                UmbShopVariant variant = databaseContext.Database.Fetch<UmbShopVariant>("SELECT TOP 1 * FROM " + UmbShopVariant.TableName + " WHERE UniqueId = @0 AND ProductUniqueId = @1;", variantUniqueId, productUniqueId).FirstOrDefault();
                variant.Name = name;
                databaseContext.Database.Update(variant);

                LogHelper.Info<UmbShopVariantRepository>("END UpdateVariant productId:" + productId + " variantId:" + variantId + " name:" + name);
            }
            catch (Exception exception)
            {
                LogHelper.Info<UmbShopVariantRepository>("ERROR UpdateVariant " + exception.Message);
                return false;
            }

            return true;
        }

        public bool RemoveVariant(string productId, string variantId)
        {
            //MISSING
            return true;
        }

    }
}
