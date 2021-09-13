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
        public BandaHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<BandaViewModel>> GetAllAsync(bool orderAscendat, string search = null)
        {
            var bandas = await _httpClient
                .GetFromJsonAsync<IEnumerable<BandaViewModel>>($"{orderAscendat}/{search}");

            return bandas;
        }
         
        public async Task<BandaViewModel> GetByIdAsync(int id)
        {
            var banda = await _httpClient
                .GetFromJsonAsync<BandaViewModel>($"{id}");

            return banda;
        }

        public async Task<BandaViewModel> CreateAsync(BandaViewModel bandaViewModel)
        {
            var httpReponseMessage = await _httpClient
                .PostAsJsonAsync("", bandaViewModel);
            httpReponseMessage.EnsureSuccessStatusCode();
            await using var contentStream = await httpReponseMessage.Content.ReadAsStreamAsync();

            var criarBanda = await JsonSerializer.DeserializeAsync<BandaViewModel>(contentStream, _jsonSerializerOptions);

            return criarBanda;
        }

        public async Task<BandaViewModel> EditAsync(BandaViewModel bandaViewModel)
        {
            var httpReponseMessage = await _httpClient
                .PutAsJsonAsync($"{bandaViewModel.Id}", bandaViewModel);
            httpReponseMessage.EnsureSuccessStatusCode();
            await using var contentStream = await httpReponseMessage.Content.ReadAsStreamAsync();

            var editarBanda = await JsonSerializer.DeserializeAsync<BandaViewModel>(contentStream, _jsonSerializerOptions);

            return editarBanda;
        }

        public async Task DeleteAsync(int id)
        {
            var httpReponseMessage = await _httpClient
                .DeleteAsync($"{id}");

            httpReponseMessage.EnsureSuccessStatusCode(); 
        }

        public async Task<bool> IsNomeValidAsync(string nome, int id)
        {
            var isNomeValid = await _httpClient
                .GetFromJsonAsync<bool>($"IsNomeValid/{nome}/{id}");

            return isNomeValid;
        }
    }
}
