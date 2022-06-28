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
        //Add ratings to our app and return a json filr with new data.
        //If no rating return null
        public void AddRating(string productId, int rating)
        { 
            var products = GetProducts();
            var query = products.First(x=> x.Id == productId);

            if(query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            //Write ratings to the Json file
            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Product>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }),
                products
            );
        }
    }
}
