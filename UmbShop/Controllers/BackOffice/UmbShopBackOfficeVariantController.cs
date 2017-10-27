using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using System.Web.Http;
using Umbraco.Web.WebApi;
using UmbShop.Models.Variant;

namespace UmbShop.Controllers.BackOffice
{
    [JsonOnlyConfiguration]
    public class UmbShopBackOfficeVariantController : UmbracoAuthorizedApiController
    {

        [HttpGet]
        public object AddVariant(string productId, string name)
        {
            UmbShopVariantRepository umbShopVariantRepository = new UmbShopVariantRepository();
            bool approved = umbShopVariantRepository.AddVariant(productId, name);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

        [HttpGet]
        public object UpdateVariant(string productId, string variantId, string name)
        {
            UmbShopVariantRepository umbShopVariantRepository = new UmbShopVariantRepository();
            bool approved = umbShopVariantRepository.UpdateVariant(productId, variantId, name);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

        [HttpGet]
        public object RemoveVariant(string productId, string variantId)
        {
            UmbShopVariantRepository umbShopVariantRepository = new UmbShopVariantRepository();
            bool approved = umbShopVariantRepository.RemoveVariant(productId, variantId);
            return Request.CreateResponse(JsonMetaResponse.GetSuccess(approved));
        }

    }
}
