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
    public class MusicoHttpService : IMusicoHttpService
    {
        private readonly HttpClient _httpClient;
        private static JsonSerializerOptions _jsonSerializerOptions = new()  //JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };
        public MusicoHttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44312");
        }
        public async Task<IEnumerable<MusicoViewModel>> GetAllAsync(bool orderAscendat, string search = null)
        {
            var musicos = await _httpClient
                .GetFromJsonAsync<IEnumerable<MusicoViewModel>>("/api/v1/MusicoApi");

            return musicos;
        }

        public async Task<MusicoViewModel> GetByIdAsync(int id)
        {
            var musico = await _httpClient
                .GetFromJsonAsync<MusicoViewModel>($"/api/v1/MusicoApi/{id}");

            return musico;
        }

        public async Task<MusicoViewModel> CreateAsync(MusicoViewModel musicoViewModel)
        {
            var httpReponseMessage = await _httpClient
                .PostAsJsonAsync("/api/v1/MusicoApi/", musicoViewModel);
            httpReponseMessage.EnsureSuccessStatusCode();
            var contentStream = await httpReponseMessage.Content.ReadAsStreamAsync();

            var criarMusico = await JsonSerializer.DeserializeAsync<MusicoViewModel>(contentStream, _jsonSerializerOptions);

            return criarMusico;
        }

        public async Task<MusicoViewModel> EditAsync(MusicoViewModel musicoViewModel)
        {
            var httpReponseMessage = await _httpClient
                .PutAsJsonAsync($"/api/v1/MusicoApi/{musicoViewModel.Id}", musicoViewModel);
            httpReponseMessage.EnsureSuccessStatusCode();
            var contentStream = await httpReponseMessage.Content.ReadAsStreamAsync();

            var editarMusico = await JsonSerializer.DeserializeAsync<MusicoViewModel>(contentStream, _jsonSerializerOptions);

            return editarMusico;
        }

        public async Task DeleteAsync(int id)
        {
            var httpReponseMessage = await _httpClient
                .DeleteAsync($"/api/v1/MusicoApi/{id}");

            httpReponseMessage.EnsureSuccessStatusCode();
        }
    }
}
