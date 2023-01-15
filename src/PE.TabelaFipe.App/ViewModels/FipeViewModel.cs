using System.ComponentModel;
using System.Text.Json.Serialization;

namespace PE.TabelaFipe.App.ViewModels
{
    public class FipeViewModel
    {
        [DisplayName("Preço FIPE")]
        public string Valor { get; set; }

        [DisplayName("Fabricante")]
        public string Marca { get; set; }

        [DisplayName("Modelo")]
        public string Modelo { get; set; }

        [DisplayName("Ano")]
        public int AnoModelo { get; set; }

        [DisplayName("Combustível")]
        public string Combustivel { get; set; }

        [DisplayName("Codigo Fipe")]
        public string CodigoFipe { get; set; }

        [DisplayName("Mês de Referência")]
        public string MesReferencia { get; set; }

        [DisplayName("Tipo do Veículo")]
        public int TipoVeiculo { get; set; }

        [DisplayName("Codigo do Combustível ")]
        public char SiglaCombustivel { get; set; }
    }
}
