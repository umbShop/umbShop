using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using System.Web.Http;
using Umbraco.Web.WebApi;
using UmbShop.Models.Basket;
using UmbShop.Models.Stock;

namespace UmbShop.Controllers.Api
{
    [JsonOnlyConfiguration]
    public class UmbShopBasketController : UmbracoApiController
    {

        [HttpGet]
        public object GetBasket()
        {
            UmbShopBasketRepository UmbShopBasketRepository = new UmbShopBasketRepository();
            string UmbShopBasket = UmbShopBasketRepository.GetBasket();
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(UmbShopBasket));
        }

        [HttpGet]
        public object GetBasketContent(string basketId)
        {
            UmbShopBasketRepository UmbShopBasketRepository = new UmbShopBasketRepository();
            UmbShopStock[] UmbShopBasketContent = UmbShopBasketRepository.GetBasketContent(basketId);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(UmbShopBasketContent));
        }

        [HttpGet]
        public object AddProductsToBasket(string basketId, string productId, string variantId, string count)
        {
            UmbShopBasketRepository UmbShopBasketRepository = new UmbShopBasketRepository();
            bool approved = UmbShopBasketRepository.AddProductsToBasket(basketId, productId, variantId, count);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

        [HttpGet]
        public object RemoveProductsFromBasket(string basketId, string productId, string variantId, string count)
        {
            UmbShopBasketRepository UmbShopBasketRepository = new UmbShopBasketRepository();
            bool approved = UmbShopBasketRepository.RemoveProductsFromBasket(basketId, productId, variantId, count);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

    }
}
