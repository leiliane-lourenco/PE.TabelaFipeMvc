using Microsoft.Extensions.Caching.Memory;
using PE.TabelaFipe.Repository.Models;
using PE.TabelaFipe.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PE.TabelaFipe.Application.Services
{
    public class TabelaFipeService : ITabelaFipeService
    {
        private readonly ITabelaFipeRepository _tabelaFipeRepository;
        private readonly IMemoryCache _memoryCache;

        public TabelaFipeService(ITabelaFipeRepository tabelaFipeRepository,
            IMemoryCache memoryCache)
        {
            _tabelaFipeRepository = tabelaFipeRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Marca>> ObterMarcas(string marca)
        {          
            var key = marca;
            if (!_memoryCache.TryGetValue<IEnumerable<Marca>>(key, out var marcas))
            {
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(90)
                };

                marcas = await _tabelaFipeRepository.ObterMarcas(marca);

                _memoryCache.Set<IEnumerable<Marca>>(key, marcas, cacheOptions);
            }

            return marcas;
        }

        public async Task<IEnumerable<Modelo>> ObterModelos(string marca, int codigoMarca)
        {
            var Key = $"{marca}/{codigoMarca}";
            if(!_memoryCache.TryGetValue<IEnumerable<Modelo>>(Key, out var modelos))
            {
                var cacheOptions = new MemoryCacheEntryOptions
                { 
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(90)
                };

                modelos = await _tabelaFipeRepository.ObterModelos(marca, codigoMarca);

                _memoryCache.Set<IEnumerable<Modelo>>(Key, modelos, cacheOptions);
            }
            return modelos;  
        }

        public async Task<IEnumerable<Modelo>> ObterModelosPorAno(string marca, int codigoMarca, int codigoModelo)
        {
            var key = $"{marca}/{codigoMarca}/{codigoModelo}";
            if (!_memoryCache.TryGetValue<IEnumerable<Modelo>>(key, out var codigoModelosPorAno))
            {
                var cacheOption = new MemoryCacheEntryOptions 
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(90)
                };

                codigoModelosPorAno = await _tabelaFipeRepository.ObterModelosPorAno(marca, codigoMarca, codigoModelo);

                _memoryCache.Set<IEnumerable<Modelo>>(key, codigoModelosPorAno, cacheOption);
            }
            return codigoModelosPorAno;
          
        }

        public async Task<Fipe> ObterPreco(string marca, int codigoMarca, int codigoModelo, string codigoAno)
        {
            var key = $"{marca}/{codigoMarca}/{codigoModelo}/{codigoAno}";
            if (!_memoryCache.TryGetValue<Fipe>(key, out var codigoModeloAno))
            {
                var caheOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(90)
                };

                codigoModeloAno = await _tabelaFipeRepository.ObterPreco(marca, codigoMarca, codigoModelo, codigoAno);
                _memoryCache.Set<Fipe>(key, codigoModeloAno, caheOption);
            }

            return codigoModeloAno;
        }
    }
}
