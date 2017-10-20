using System;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbShop.Models.Product
{
    public class UmbShopProductRepository
    {

        public UmbShopProduct GetProductById(string id)
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
                // Check if content is product documenttype (web.config)

                UmbShopProduct product = UmbShopProduct.GetFromContent(content);
                return product;
            }

            return null;
        }

        //public UmbShopProduct[] GetProductListById(string id)
        //{
        //    return null;
        //}

    }
}
