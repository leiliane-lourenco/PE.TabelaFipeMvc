using System.Collections.Generic;
using System.ComponentModel;

namespace PE.TabelaFipe.App.ViewModels
{
    public class ModeloViewModel
    {
        [DisplayName("Marca")]
        public string Nome { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }

        public class DadosModelo
        { 
            public IEnumerable<ModeloViewModel> Modelos { get; set; }
        }
    }
}
