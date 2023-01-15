using System.ComponentModel;

namespace PE.TabelaFipe.App.ViewModels
{
    public class MarcaViewModel
    {
        [DisplayName("Marca")]
        public string Nome { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }
    }
}
