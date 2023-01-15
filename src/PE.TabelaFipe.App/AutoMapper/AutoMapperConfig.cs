using AutoMapper;
using PE.TabelaFipe.App.ViewModels;
using PE.TabelaFipe.Repository.Models;

namespace PE.TabelaFipe.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<MarcaViewModel, Marca>().ReverseMap();
            CreateMap<ModeloViewModel, Modelo>().ReverseMap();
            CreateMap<FipeViewModel, Fipe>().ReverseMap();
        }
    }
}
