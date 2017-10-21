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
            UmbShopProductRepository umbShopProductRepository = new UmbShopProductRepository();
            UmbShopProduct umbShopProduct = umbShopProductRepository.GetProduct(id);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(umbShopProduct));
        }

        [HttpGet]
        public object GetProductList(string id)
        {
            UmbShopProductRepository umbShopProductRepository = new UmbShopProductRepository();
            UmbShopProduct[] umbShopProductList = umbShopProductRepository.GetProductList(id);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(umbShopProductList));
        }

    }
}
