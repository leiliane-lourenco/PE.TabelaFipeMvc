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


        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}
