using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    //Create our product model
    public class Product
    {
        public string? Id { get; set; }
        public string? Maker { get; set; }
        //Match Json property name to product name Image
        [JsonPropertyName("img")]
        public string? Image { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int[]? Ratings { get; set; }

        //Serialize object instantiated
        public override string ToString() => JsonSerializer.Serialize<Product>(this);
    }
}