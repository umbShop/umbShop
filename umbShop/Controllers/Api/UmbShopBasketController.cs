using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using System.Web.Http;
using Umbraco.Web.WebApi;
using umbShop.Models.Basket;

namespace umbShop.Controllers.Api
{
    [JsonOnlyConfiguration]
    public class UmbShopBasketController : UmbracoApiController
    {

        [HttpGet]
        public object AddProductsToBasket(string productId, string variantId, string count)
        {
            UmbShopBasketRepository umbShopBasketRepository = new UmbShopBasketRepository();
            bool approved = umbShopBasketRepository.AddProductsToBasket(productId, variantId, count);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

    }
}
