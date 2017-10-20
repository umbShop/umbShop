﻿using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using System.Web.Http;
using Umbraco.Web.WebApi;
using umbShop.Models.Stock;

namespace umbShop.Controllers.BackOffice
{
    [JsonOnlyConfiguration]
    public class UmbShopBackOfficeStockController : UmbracoAuthorizedApiController
    {

        [HttpGet]
        public object AddProductsToStock(string productId, string variantId, string count)
        {
            UmbShopStockRepository umbShopStockRepository = new UmbShopStockRepository();
            string output = umbShopStockRepository.AddProductsToStock(productId, variantId, count);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(output));
        }

        [HttpGet]
        public object CountProductsInStock(string productId, string variantId)
        {
            UmbShopStockRepository umbShopStockRepository = new UmbShopStockRepository();
            int count = umbShopStockRepository.CountProductsInStock(productId, variantId);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(count));
        }

    }
}
