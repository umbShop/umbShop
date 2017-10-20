using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using System.Web.Http;
using Umbraco.Web.WebApi;
using umbShop.Models.Product;

namespace umbShop.Controllers.Api
{
    [JsonOnlyConfiguration]
    public class UmbShopProductController : UmbracoApiController
    {

        [HttpGet]
        public object GetProduct(string id)
        {
            UmbShopProductRepository umbShopProductRepository = new UmbShopProductRepository();
            UmbShopProduct product = umbShopProductRepository.GetProduct(id);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(product));
        }

        [HttpGet]
        public object GetProductList(string id)
        {
            UmbShopProductRepository umbShopProductRepository = new UmbShopProductRepository();
            UmbShopProduct[] productList = umbShopProductRepository.GetProductList(id);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(productList));
        }

    }
}
