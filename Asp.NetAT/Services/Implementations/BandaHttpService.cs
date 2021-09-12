using Asp.NetAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Asp.NetAT.Services.Implementations
{
    public class BandaHttpService : IBandaHttpService
    {
        private readonly HttpClient _httpClient;
        private static JsonSerializerOptions _jsonSerializerOptions = new()  //JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };
        public BandaHttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44312");
        }
        public async Task<IEnumerable<BandaViewModel>> GetAllAsync(bool orderAscendat, string search = null)
        {
            var bandas = await _httpClient
                .GetFromJsonAsync<IEnumerable<BandaViewModel>>("/api/v1/BandaApi");

            return bandas;
        }
         
        public async Task<BandaViewModel> GetByIdAsync(int id)
        {
            var banda = await _httpClient
                .GetFromJsonAsync<BandaViewModel>($"/api/v1/BandaApi/{id}");

            return banda;
        }

        public async Task<BandaViewModel> CreateAsync(BandaViewModel bandaViewModel)
        {
            var httpReponseMessage = await _httpClient
                .PostAsJsonAsync("/api/v1/BandaApi/", bandaViewModel);
            httpReponseMessage.EnsureSuccessStatusCode();
            var contentStream = await httpReponseMessage.Content.ReadAsStreamAsync();

            var criarBanda = await JsonSerializer.DeserializeAsync<BandaViewModel>(contentStream, _jsonSerializerOptions);

            return criarBanda;
        }

        public async Task<BandaViewModel> EditAsync(BandaViewModel bandaViewModel)
        {
            var httpReponseMessage = await _httpClient
                .PutAsJsonAsync($"/api/v1/BandaApi/{bandaViewModel.Id}", bandaViewModel);
            httpReponseMessage.EnsureSuccessStatusCode();
            var contentStream = await httpReponseMessage.Content.ReadAsStreamAsync();

            var editarBanda = await JsonSerializer.DeserializeAsync<BandaViewModel>(contentStream, _jsonSerializerOptions);

            return editarBanda;
        }

        public async Task DeleteAsync(int id)
        {
            var httpReponseMessage = await _httpClient
                .DeleteAsync($"/api/v1/BandaApi/{id}");

            httpReponseMessage.EnsureSuccessStatusCode(); 
        }

        public async Task<bool> IsNomeValidAsync(string nome, int id)
        {
            var isNomeValid = await _httpClient
                .GetFromJsonAsync<bool>($"/api/v1/BandaApi/IsNomeValid/{nome}/{id}");

            return isNomeValid;
        }
    }
}
