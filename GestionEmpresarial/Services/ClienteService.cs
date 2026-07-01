using AutoMapper;
using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels.Clientes;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Private Methods
        private async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<bool> ExisteCorreoAsync(string correo, Guid? clienteId = null)
        {
            return await _context.Clientes.AnyAsync(c =>
                c.Correo == correo &&
                c.Activo &&
                (!clienteId.HasValue || c.Id != clienteId.Value));
        }

        private async Task<ApiResponse> CrearClienteAsync(ClienteGuardarViewModel model)
        {
            var cliente = _mapper.Map<Cliente>(model);

            cliente.Id = Guid.NewGuid();

            cliente.Activo = true;

            cliente.FechaCreacion = DateTime.Now;

            cliente.FechaModificacion = null;

            await _context.Clientes.AddAsync(cliente);

            await GuardarCambiosAsync();

            return ApiResponse.Ok("Cliente registrado correctamente.");
        }

        private async Task<ApiResponse> ActualizarClienteAsync(ClienteGuardarViewModel model)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c =>
                c.Id == model.Id &&
                c.Activo);
            if (cliente is null)
            {
                return ApiResponse.Fail("No se encontró el cliente.");
            }
            _mapper.Map(model, cliente);
            cliente.FechaModificacion = DateTime.Now;
            await GuardarCambiosAsync();
            return ApiResponse.Ok("Cliente actualizado correctamente.");
        }

        #endregion

        public async Task<ApiResponse> CambiarEstadoAsync(Guid id)
        {
            var cliente = await _context.Clientes
               .FirstOrDefaultAsync(c =>
                   c.Id == id &&
                   c.Activo);

            if (cliente is null)
            {
                return ApiResponse.Fail("El cliente no fue encontrado.");
            }

            cliente.Activo = false;

            cliente.FechaModificacion = DateTime.Now;

            await GuardarCambiosAsync();

            return ApiResponse.Ok("Cliente eliminado correctamente.");
        }

        public async Task<ApiResponse> GuardarAsync(ClienteGuardarViewModel model)
        {
            if (await ExisteCorreoAsync(model.Correo, model.Id))
            {
                return ApiResponse.Fail("Ya existe un cliente con ese correo.");
            }

            if (model.Id.HasValue)
            {
                return await ActualizarClienteAsync(model);
            }

            return await CrearClienteAsync(model);
        }

        public async Task<IEnumerable<ClienteListadoViewModel>> ObtenerListadoAsync()
        {
            var clientes = await _context.Clientes
                .AsNoTracking()
                .Where(c => c.Activo)
                .OrderBy(c => c.Nombre)
                .ThenBy(c => c.Apellido)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ClienteListadoViewModel>>(clientes);
        }

        public async Task<ClienteDetalleViewModel?> ObtenerPorIdAsync(Guid id)
        {
            var cliente = await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.Activo);

            if (cliente is null)
                return null;

            return _mapper.Map<ClienteDetalleViewModel>(cliente);
        }
    }
}
