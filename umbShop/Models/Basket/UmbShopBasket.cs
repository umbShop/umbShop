using Newtonsoft.Json;
using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace UmbShop.Models.Basket
{
    [TableName(TableName)]
    [PrimaryKey(PrimaryKey, autoIncrement = true)]
    [ExplicitColumns]
    public class UmbShopBasket
    {

        #region Constants

        public const string TableName = "UmbShopBasket";

        public const string PrimaryKey = "Id";

        #endregion

        #region Properties

        [Column(PrimaryKey)]
        [PrimaryKeyColumn(AutoIncrement = true)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("UniqueId")]
        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [Column("LastUsed")]
        [JsonProperty("lastUsed")]
        public DateTime LastUsed { get; set; }

        [Column("Status")]
        [JsonProperty("status")]
        public int Status { get; set; }

        #endregion

    }
}
