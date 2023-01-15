using Newtonsoft.Json;
using PE.TabelaFipe.Repository.Models;
using PE.TabelaFipe.Repository.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PE.TabelaFipe.Repository.Repositories
{
    public class TabelaFipeRepository : ITabelaFipeRepository
    {
        private readonly HttpClient _httpClient;
        private readonly Endpoints _endpoints;

        public TabelaFipeRepository(
            HttpClient httpClient, 
            Endpoints endpoints)
        {
            _httpClient = httpClient;
            _endpoints = endpoints;
        }

        public async Task<IEnumerable<Marca>> ObterMarcas(string marca)
        {
            var serviceUrl = $"{_endpoints.BaseUrl}/{string.Format(_endpoints.Marcas, marca)}";
            var response = await _httpClient.GetAsync($"{serviceUrl}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var marcasViewModel = JsonConvert.DeserializeObject<IEnumerable<Marca>>(content);
                return marcasViewModel;

            }

            throw new ErrorRequest(content, response.StatusCode, content);

        }

        public async Task<IEnumerable<Modelo>> ObterModelos(string marca, int codigoMarca)
        {
            var serviceUrl = $"{_endpoints.BaseUrl}/{string.Format(_endpoints.Modelos, marca, codigoMarca)}";
            var response = await _httpClient.GetAsync($"{serviceUrl}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var dadosModeloView = JsonConvert.DeserializeObject<DadosDoModelo>(content);
                return dadosModeloView.Modelos;

            }

            throw new ErrorRequest(content, response.StatusCode, content);
        }

        public async Task<IEnumerable<Modelo>> ObterModelosPorAno(string marca, int codigoMarca, int codigoModelo)
        {
            var serviceUrl = $"{_endpoints.BaseUrl}/{string.Format(_endpoints.ModelosPorAno, marca, codigoMarca, codigoModelo)}";
            var response = await _httpClient.GetAsync($"{serviceUrl}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var modelosPorAno = JsonConvert.DeserializeObject<IEnumerable<Modelo>>(content);
                return modelosPorAno;

            }

            throw new ErrorRequest(content, response.StatusCode, content);
        }

        public async Task<Fipe> ObterPreco(string marca, int codigoMarca, int codigoModelo, string codigoAno)
        {
            var serviceUrl = $"{_endpoints.BaseUrl}/{string.Format(_endpoints.Fipe, marca, codigoMarca, codigoModelo, codigoAno)}";
            var response = await _httpClient.GetAsync($"{serviceUrl}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var tabelaFipeViewModel = JsonConvert.DeserializeObject<Fipe>(content);
                return tabelaFipeViewModel;

            }
            throw new ErrorRequest(content, response.StatusCode, content);
        }
    }
}
