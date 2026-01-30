using backend.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class ProductService
    {
        private readonly HttpClient _client;
        private const string BASE_URL = "https://retoolapi.dev/0PCAof/products";

        public ProductService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Product>> GetAll()
        {
            var response = await _client.GetStringAsync(BASE_URL);
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task Create(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync(BASE_URL, content);
        }

        public async Task Update(int id, Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PutAsync($"{BASE_URL}/{id}", content);
        }

        public async Task Delete(int id)
        {
            await _client.DeleteAsync($"{BASE_URL}/{id}");
        }
    }
}
