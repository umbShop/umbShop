using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using System.Web.Http;
using Umbraco.Web.WebApi;
using UmbShop.Models.Stock;

namespace UmbShop.Controllers.BackOffice
{
    [JsonOnlyConfiguration]
    public class UmbShopBackOfficeStockController : UmbracoAuthorizedApiController
    {

        [HttpGet]
        public object AddProductsToStock(string productId, string variantId, string count)
        {
            UmbShopStockRepository UmbShopStockRepository = new UmbShopStockRepository();
            bool approved = UmbShopStockRepository.AddProductsToStock(productId, variantId, count);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

        [HttpGet]
        public object RemoveProductsFromStock(string productId, string variantId, string count)
        {
            UmbShopStockRepository UmbShopStockRepository = new UmbShopStockRepository();
            bool approved = UmbShopStockRepository.RemoveProductsFromStock(productId, variantId, count);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

        [HttpGet]
        public object CountProductsInStock(string productId, string variantId)
        {
            UmbShopStockRepository UmbShopStockRepository = new UmbShopStockRepository();
            int count = UmbShopStockRepository.CountProductsInStock(productId, variantId);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(count));
        }

    }
}
