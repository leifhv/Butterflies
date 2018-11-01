using Butterflies.Shared;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Butterflies.Client.Services
{
    public interface IButterflyApi
    {
        Task<List<ButterflyDto>> GetAllAsync();
        Task<ButterflyDto> GetAsync(string id);
        Task<ButterflyDto> SaveAsync(ButterflyDto butterfly);
        Task DeleteAsync(string id);
    }

    public class ButterflyApi : IButterflyApi
    {
        [Inject]
        protected HttpClient Http { get; set; }

        private const string _url = "https://toppbutterflyapi.azurewebsites.net/api/butterflies";
//        private const string _url = "https://localhost:44357/api/butterflies/";

        public ButterflyApi(HttpClient httpClient)
        {
            Http = httpClient;
        }

        public async Task DeleteAsync(string id)
        {
            await Http.DeleteAsync(_url+$"/{id}");
        }

        public async Task<ButterflyDto> GetAsync(string id)
        {
            return await Http.GetJsonAsync<ButterflyDto>(_url+$"/{id}");
        }

        public async Task<List<ButterflyDto>> GetAllAsync()
        {
            return await Http.GetJsonAsync<List<ButterflyDto>>(_url);
        }

        public async Task<ButterflyDto> SaveAsync(ButterflyDto butterfly)
        {            
            return await Http.PostJsonAsync<ButterflyDto>(_url, butterfly);
        }
    }
}
