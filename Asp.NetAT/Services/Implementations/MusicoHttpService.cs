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
        public MusicoHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<MusicoViewModel>> GetAllAsync(bool orderAscendat, string search = null)
        {
            var musicos = await _httpClient
                .GetFromJsonAsync<IEnumerable<MusicoViewModel>>($"{orderAscendat}/{search}");

            return musicos;
        }

        public async Task<MusicoViewModel> GetByIdAsync(int id)
        {
            var musico = await _httpClient
                .GetFromJsonAsync<MusicoViewModel>($"{id}");

            return musico;
        }

        public async Task<MusicoViewModel> CreateAsync(MusicoViewModel musicoViewModel)
        {
            var httpReponseMessage = await _httpClient
                .PostAsJsonAsync("", musicoViewModel);
            httpReponseMessage.EnsureSuccessStatusCode();
            await using var contentStream = await httpReponseMessage.Content.ReadAsStreamAsync();

            var criarMusico = await JsonSerializer.DeserializeAsync<MusicoViewModel>(contentStream, _jsonSerializerOptions);

            return criarMusico;
        }

        public async Task<MusicoViewModel> EditAsync(MusicoViewModel musicoViewModel)
        {
            var httpReponseMessage = await _httpClient
                .PutAsJsonAsync($"{musicoViewModel.Id}", musicoViewModel);
            httpReponseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await httpReponseMessage.Content.ReadAsStreamAsync();

            var editarMusico = await JsonSerializer.DeserializeAsync<MusicoViewModel>(contentStream, _jsonSerializerOptions);

            return editarMusico;
        }

        public async Task DeleteAsync(int id)
        {
            var httpReponseMessage = await _httpClient
                .DeleteAsync($"{id}");

            httpReponseMessage.EnsureSuccessStatusCode();
        }
    }
}
