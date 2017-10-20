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

            int idInt = 0;
            int.TryParse(id, out idInt);
            if (idInt != 0)
            {
                content = UmbracoContext.Current.ContentCache.GetById(idInt);
            }

            Guid idGuid = Guid.Empty;
            Guid.TryParse(id, out idGuid);
            if (idGuid != Guid.Empty)
            {
                content = UmbracoContext.Current.ContentCache.GetById(idGuid);
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
