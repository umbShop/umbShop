﻿using Newtonsoft.Json;
using System;
using Umbraco.Core.Models;
using Umbraco.Web;
using UmbShop.Models.Variant;

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

        [JsonProperty("variants")]
        public UmbShopVariant[] Variants { get; private set; }

        #endregion

        #region Constructors

        public UmbShopProduct(IPublishedContent content, bool withVariants) {
            Id = content.Id;
            UniqueId = content.GetKey();
            Name = content.Name;
            Url = content.Url;

            if (withVariants)
            {
                UmbShopVariantRepository umbShopVariantRepository = new UmbShopVariantRepository();
                Variants = umbShopVariantRepository.GetVariants(UniqueId.ToString());
            }
        }

        #endregion

        #region Statics

        public static UmbShopProduct GetFromContentWithVariants(IPublishedContent content)
        {
            if (content == null) return null;

            return new UmbShopProduct(content, true);
        }

        public static UmbShopProduct GetFromContentWithoutVariants(IPublishedContent content)
        {
            if (content == null) return null;

            return new UmbShopProduct(content, false);
        }

        #endregion

    }
}
