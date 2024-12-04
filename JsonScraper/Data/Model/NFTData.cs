using System.Text.Json.Serialization;
using JsonScraper.Extension;

namespace JsonScraper.Data;
public class NFTData
{
    // public string assetName { get; set; }
    // public string assetID { get; set; }
    // public string name { get; set; }
    // [JsonConverter(typeof(FlexiblePriceConverter))]
    // // public string price { get; set; }
    // public string cnftID { get; set; }
    // public string iconurl { get; set; }
    // public string url { get; set; }
    public string Background { get; set; }
    public string Bear { get; set; }
    public string Clothes { get; set; }
    public string Face { get; set; }
    public string Handheld { get; set; }
    public string Head { get; set; }
    public string Skins { get; set; }
    [JsonPropertyName("Trait Count")]
    public string TraitCount { get; set; }
    // public string encodedName { get; set; }
    // public string buildType { get; set; }
    // public string rarityScore { get; set; }
    // public string rarityRank { get; set; }
    // public object prices { get; set; } = new object();
    // public string listingDate { get; set; }
    // public string ownerStakeKey { get; set; }
}
