using Application.Common.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;

namespace Infrastructure.Repositories
{
    public class ExternalService<T> : IExternalService<T>
        where T : notnull
    {
        public async Task<bool> Create(T model)
        {
            HttpClient client = new HttpClient();
            var path = "https://secondmicroservice.azurewebsites.net/api/File/Create";
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(path, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<T?> GetList()
        {
            T resultData = default;
            HttpClient client = new HttpClient();
            var path = "https://secondmicroservice.azurewebsites.net/api/File/GetAll";
            HttpResponseMessage response = await client.GetAsync(path);
            
            if (response.IsSuccessStatusCode)
            {
                resultData = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            return resultData;
        }
    }
}
