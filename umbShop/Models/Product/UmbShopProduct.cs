using Newtonsoft.Json;
using System;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbShop.Models.Product
{
    public class UmbShopProduct
    {

        #region Properties

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("uniqueId")]
        public Guid UniqueId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        #endregion

        #region Constructors

        public UmbShopProduct(IPublishedContent content) {
            Id = content.Id;
            UniqueId = content.GetKey();
            Name = content.Name;
            Url = content.Url;
        }

        #endregion

        #region Statics

        public static UmbShopProduct GetFromContent(IPublishedContent content)
        {
            if (content == null) return null;

            return new UmbShopProduct(content);
        }

        #endregion

    }
}
