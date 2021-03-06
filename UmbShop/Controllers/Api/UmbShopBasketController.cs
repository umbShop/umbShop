﻿using Skybrud.WebApi.Json;
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
            UmbShopBasketRepository umbShopBasketRepository = new UmbShopBasketRepository();
            string umbShopBasket = umbShopBasketRepository.GetBasket();
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(umbShopBasket));
        }

        [HttpGet]
        public object KeepBasketAlive(string basketId)
        {
            UmbShopBasketRepository umbShopBasketRepository = new UmbShopBasketRepository();
            bool approved = umbShopBasketRepository.KeepBasketAlive(basketId);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

        [HttpGet]
        public object GetBasketContent(string basketId)
        {
            UmbShopBasketRepository umbShopBasketRepository = new UmbShopBasketRepository();
            UmbShopStock[] umbShopBasketContent = umbShopBasketRepository.GetBasketContent(basketId);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(umbShopBasketContent));
        }

        [HttpPost]
        public object UpdateBasketDetails(string basketId, string shipping, string invoice, string payment)
        {
            UmbShopBasketRepository umbShopBasketRepository = new UmbShopBasketRepository();
            UmbShopBasket umbShopBasketDetails = umbShopBasketRepository.UpdateBasketDetails(basketId, shipping, invoice, payment);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(umbShopBasketDetails));
        }

        [HttpGet]
        public object AddProductsToBasket(string basketId, string productId, string variantId, string count)
        {
            UmbShopBasketRepository umbShopBasketRepository = new UmbShopBasketRepository();
            bool approved = umbShopBasketRepository.AddProductsToBasket(basketId, productId, variantId, count);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

        [HttpGet]
        public object RemoveProductsFromBasket(string basketId, string productId, string variantId, string count)
        {
            UmbShopBasketRepository umbShopBasketRepository = new UmbShopBasketRepository();
            bool approved = umbShopBasketRepository.RemoveProductsFromBasket(basketId, productId, variantId, count);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

        [HttpGet]
        public object ClearUnusedBaskets()
        {
            UmbShopBasketRepository umbShopBasketRepository = new UmbShopBasketRepository();
            UmbShopBasket[] baskets = umbShopBasketRepository.ClearUnusedBaskets();
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(baskets));
        }

    }
}
