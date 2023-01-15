using PE.TabelaFipe.Repository.Models;
using PE.TabelaFipe.Repository.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PE.TabelaFipe.Application.Services
{
    public class TabelaFipeService : ITabelaFipeService
    {
        private readonly ITabelaFipeRepository _tabelaFipeRepository;

        public TabelaFipeService(ITabelaFipeRepository tabelaFipeRepository)
        {
            _tabelaFipeRepository = tabelaFipeRepository;
        }

        public async Task<IEnumerable<Marca>> ObterMarcas(string marca)
        {
            var marcas = await _tabelaFipeRepository.ObterMarcas(marca);
            return marcas;
        }

        public async Task<IEnumerable<Modelo>> ObterModelos(string marca, int codigoMarca)
        {
            var modelos = await _tabelaFipeRepository.ObterModelos(marca, codigoMarca);
            return modelos;
        }

        public async Task<IEnumerable<Modelo>> ObterModelosPorAno(string marca, int codigoMarca, int codigoModelo)
        {
            var modelosAno = await _tabelaFipeRepository.ObterModelosPorAno(marca, codigoMarca, codigoModelo);
            return modelosAno;
        }

        public async Task<Fipe> ObterPreco(string marca, int codigoMarca, int codigoModelo, string codigoAno)
        {
            var preco = await _tabelaFipeRepository.ObterPreco(marca, codigoMarca, codigoModelo, codigoAno);

            return preco;
        }
    }
}
