using AutoMapper;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels.Clientes;
using GestionEmpresarial.ViewModels.Categorias;

namespace GestionEmpresarial.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Clientes
            CreateMap<Cliente, ClienteDetalleViewModel>();

            CreateMap<Cliente, ClienteGuardarViewModel>()
                .ReverseMap();

            CreateMap<Cliente, ClienteListadoViewModel>()
                .ForMember(
                    dest => dest.NombreCompleto,
                    opt => opt.MapFrom(src =>
                        $"{src.Nombre} {src.Apellido}"));

            // Categorías
            CreateMap<Categoria, CategoriaDetalleViewModel>();

            CreateMap<Categoria, CategoriaGuardarViewModel>()
                .ReverseMap();

            CreateMap<Categoria, CategoriaListadoViewModel>();
        }

    }
}
