using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using System.Web.Http;
using Umbraco.Web.WebApi;
using UmbShop.Models.Product;

namespace UmbShop.Controllers.Api
{
    [JsonOnlyConfiguration]
    public class UmbShopProductController : UmbracoApiController
    {

        [HttpGet]
        public object GetProduct(string id)
        {
            UmbShopProductRepository UmbShopProductRepository = new UmbShopProductRepository();
            UmbShopProduct product = UmbShopProductRepository.GetProduct(id);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(product));
        }

        [HttpGet]
        public object GetProductList(string id)
        {
            UmbShopProductRepository UmbShopProductRepository = new UmbShopProductRepository();
            UmbShopProduct[] productList = UmbShopProductRepository.GetProductList(id);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(productList));
        }

    }
}
