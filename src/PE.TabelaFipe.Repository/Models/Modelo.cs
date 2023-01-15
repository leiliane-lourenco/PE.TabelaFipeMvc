using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PE.TabelaFipe.Repository.Models
{
    public class Modelo
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
    }

    public class DadosDoModelo
    {
        [JsonPropertyName("modelos")]
        public IEnumerable<Modelo> Modelos { get; set; }
    }
}
