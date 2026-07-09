using AutoMapper;
using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels.Roles;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Services
{
    public class RolService : IRolService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse> ObtenerActivosComboAsync()
        {
            var roles = await _context.Roles
                .AsNoTracking()
                .Where(r => r.Activo && r.Nombre != "Administrador")
                .OrderBy(r => r.Nombre)
                .ToListAsync();

            var resultado = _mapper.Map<IEnumerable<RolComboViewModel>>(roles);

            return ApiResponse.Ok(data: resultado);
        }
    }
}
