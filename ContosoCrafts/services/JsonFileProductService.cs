using ContosoCrafts.WebSite.Models;
using System.Text.Json;

namespace ContosoCrafts.services
{
    public class JsonFileProductService
    {
        //Constructor
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            //webHostEnvironment is a service offering a service to our JsonFileProductService
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        //Creates a path to the JsonFile on the webroot
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "product.json");

        //Retrives product items from our Json file - Deserializing to make our product instances

        //List that we can be able to foreach
        public IEnumerable<Product> GetProducts()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            Product[]? products = JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true,
                            });
            return products;
        }
    }
}
