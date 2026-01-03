using System.Net;
using System.Text;
using System.Text.Json;

namespace Website_Invoice.Models
{
    public class AdminRepository<T> : IAdminRepository<T> where T : class
    {
        private readonly HttpClient _httpClient;

        public AdminRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetAsync(int id)
        {
            return await JsonSerializer.DeserializeAsync<T>
                (await _httpClient.GetStreamAsync($"{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<T>?> GetListAsync()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>
               (await _httpClient.GetStreamAsync(""), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> AddAsync(T entity)
        {
            string json = JsonSerializer.Serialize(entity);
            var response = await _httpClient.PostAsync("",
                new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<HttpStatusCode> UpdateAsync(int id, T entity)
        {
            string json = JsonSerializer.Serialize(entity);
            var response = await _httpClient.PutAsync($"{id}",
                new StringContent(json, Encoding.UTF8, "application/json"));
            return response.StatusCode;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{id}");
        }
    }
}
