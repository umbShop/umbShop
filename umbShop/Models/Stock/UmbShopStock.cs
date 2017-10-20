using Newtonsoft.Json;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace umbShop.Models.Stock
{
    [TableName(TableName)]
    [PrimaryKey(PrimaryKey, autoIncrement = true)]
    [ExplicitColumns]
    public class UmbShopStock
    {

        #region Constants

        public const string TableName = "UmbShopStock";

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

        [Column("ProductUniqueId")]
        [JsonProperty("productUniqueId")]
        public string ProductUniqueId { get; set; }

        [Column("VariantUniqueId")]
        [JsonProperty("variantUniqueId")]
        public string VariantUniqueId { get; set; }

        [Column("BasketUniqueId")]
        [JsonProperty("basketUniqueId")]
        public string BasketUniqueId { get; set; }

        #endregion

    }
}
