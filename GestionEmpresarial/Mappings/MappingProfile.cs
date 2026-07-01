using AutoMapper;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels.Clientes;

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
        }

    }
}
