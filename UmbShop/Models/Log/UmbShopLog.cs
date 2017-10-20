using Newtonsoft.Json;
using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace UmbShop.Models.Log
{
    [TableName(TableName)]
    [PrimaryKey(PrimaryKey, autoIncrement = true)]
    [ExplicitColumns]
    public class UmbShopLog
    {

        #region Constants

        public const string TableName = "UmbShopLog";

        public const string PrimaryKey = "Id";

        #endregion

        #region Properties

        [Column(PrimaryKey)]
        [PrimaryKeyColumn(AutoIncrement = true)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("Created")]
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [Column("Text")]
        [JsonProperty("text")]
        public string Text { get; set; }

        #endregion

    }
}
