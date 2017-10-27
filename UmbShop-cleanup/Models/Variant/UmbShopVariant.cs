using Newtonsoft.Json;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace UmbShop.Models.Variant
{
    [TableName(TableName)]
    [PrimaryKey(PrimaryKey, autoIncrement = true)]
    [ExplicitColumns]
    public class UmbShopVariant
    {

        #region Constants

        public const string TableName = "UmbShopVariant";

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

        [Column("Name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Ignore]
        [JsonProperty("count")]
        public int Count { get; set; }

        #endregion

    }
}
