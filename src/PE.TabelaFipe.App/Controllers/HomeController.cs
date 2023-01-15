using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PE.TabelaFipe.App.ViewModels;
using PE.TabelaFipe.Application.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PE.TabelaFipe.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITabelaFipeService _tabelaFipeService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, 
                              ITabelaFipeService tabelaFipeService, 
                              IMapper mapper)
        {
            _logger = logger;
            _tabelaFipeService = tabelaFipeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fipeReport = new FipeReport();

            await Task.CompletedTask;

            return View(fipeReport);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string marca, int codigoMarca = default, int codigoModelo = default, string codigoAno = "")
        {
            var fipeReport = new FipeReport();

            if (!string.IsNullOrWhiteSpace(marca))
            {
                var marcasObtidas = _mapper.Map<List<MarcaViewModel>>(await _tabelaFipeService.ObterMarcas(marca));
                fipeReport.Marcas = marcasObtidas;
                fipeReport.Marca = marca;

            }
            if (codigoMarca > default(int))
            {
                var modelosObtidos = _mapper.Map<List<ModeloViewModel>>(await _tabelaFipeService.ObterModelos(marca, codigoMarca));
                fipeReport.Modelos = modelosObtidos;
                fipeReport.CodigoModelo = codigoMarca;
            }
            if (codigoModelo > default(int))
            {
                var modelosPorAno = _mapper.Map<List<ModeloViewModel>>(await _tabelaFipeService.ObterModelosPorAno(marca, codigoMarca, codigoModelo));
                fipeReport.ModelosPorAno = modelosPorAno;
                fipeReport.CodigoModelo = codigoModelo;
            }
            if (!string.IsNullOrWhiteSpace(codigoAno))
            {
                var precoObtido = _mapper.Map<FipeViewModel>(await _tabelaFipeService.ObterPreco(marca, codigoMarca, codigoModelo, codigoAno));
                fipeReport.Fipe = precoObtido;
            }
            return View("Index", fipeReport);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
