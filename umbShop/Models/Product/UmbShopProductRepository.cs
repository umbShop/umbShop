using System;
using System.Web.Configuration;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbShop.Models.Product
{
    public class UmbShopProductRepository
    {

        public UmbShopProduct GetProduct(string id)
        {
            IPublishedContent content = null;

            Guid uniqueId = Guid.Empty;
            Guid.TryParse(id, out uniqueId);
            if (uniqueId != Guid.Empty)
            {
                content = UmbracoContext.Current.ContentCache.GetById(uniqueId);
            }

            if (content != null)
            {
                if (content.DocumentTypeAlias == WebConfigurationManager.AppSettings["UmbShop.DocType.Product"])
                {
                    UmbShopProduct product = UmbShopProduct.GetFromContent(content);
                    return product;
                }
            }

            return null;
        }

        public UmbShopProduct[] GetProductList(string id)
        {
            IPublishedContent content = null;

            Guid uniqueId = Guid.Empty;
            Guid.TryParse(id, out uniqueId);
            if (uniqueId != Guid.Empty)
            {
                content = UmbracoContext.Current.ContentCache.GetById(uniqueId);
            }

            if (content != null)
            {
                if (content.DocumentTypeAlias == WebConfigurationManager.AppSettings["UmbShop.DocType.ProductList"])
                {
                    UmbShopProduct[] productList = content.Children.Select(UmbShopProduct.GetFromContent).ToArray();
                    return productList;
                }
            }

            return null;
        }

    }
}
