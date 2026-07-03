using AutoMapper;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels.Clientes;
using GestionEmpresarial.ViewModels.Categorias;
using GestionEmpresarial.ViewModels.Productos;

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

            // Productos
            CreateMap<Producto, ProductoDetalleViewModel>()
                .ForMember(
                    dest => dest.Categoria,
                    opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nombre : string.Empty));

            CreateMap<Producto, ProductoGuardarViewModel>()
                .ReverseMap();

            CreateMap<Producto, ProductoListadoViewModel>()
                .ForMember(
                    dest => dest.Categoria,
                    opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nombre : string.Empty));
        }

    }
}
