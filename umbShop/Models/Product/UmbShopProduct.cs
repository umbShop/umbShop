using Newtonsoft.Json;
using Umbraco.Core.Models;

namespace umbShop.Models.Product
{
    public class UmbShopProduct
    {

        #region Properties

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        #endregion

        #region Constructors

        public UmbShopProduct(IPublishedContent content) {
            Id = content.Id;
            Name = content.Name;
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
