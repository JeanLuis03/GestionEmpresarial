using AutoMapper;
using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly ICurrentUserService _currentUserService;

        public UsuarioService(
            ApplicationDbContext context,
            IMapper mapper,
            IPasswordService passwordService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _passwordService = passwordService;
            _currentUserService = currentUserService;
        }

        #region Private Methods
        private async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        private static string Normalizar(string? valor)
        {
            return valor?.Trim() ?? string.Empty;
        }

        private async Task<Usuario?> ObtenerUsuarioAsync(Guid id)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        private async Task<Rol?> ObtenerRolAsync(Guid id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == id && r.Activo);
        }

        private async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, Guid? usuarioId = null)
        {
            return await _context.Usuarios.AnyAsync(u =>
                u.NombreUsuario == nombreUsuario &&
                (!usuarioId.HasValue || u.Id != usuarioId.Value));
        }

        private async Task<bool> ExisteCorreoAsync(string correo, Guid? usuarioId = null)
        {
            return await _context.Usuarios.AnyAsync(u =>
                u.Correo == correo &&
                (!usuarioId.HasValue || u.Id != usuarioId.Value));
        }

        private async Task<ApiResponse> CrearUsuarioAsync(UsuarioGuardarViewModel model, Rol rol)
        {
            if (!_currentUserService.EsAdministrador)
            {
                return ApiResponse.Fail("No tiene permisos para administrar usuarios.");
            }

            if (string.IsNullOrWhiteSpace(model.Contrasena))
            {
                return ApiResponse.Fail("La contraseña es obligatoria.");
            }

            var usuario = _mapper.Map<Usuario>(model);

            usuario.Id = Guid.NewGuid();
            usuario.Activo = true;
            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaModificacion = null;
            usuario.IntentosFallidos = 0;
            usuario.Bloqueado = false;
            usuario.IdRol = rol.Id;
            usuario.ContrasenaHash = _passwordService.HashPassword(model.Contrasena);

            await _context.Usuarios.AddAsync(usuario);
            await GuardarCambiosAsync();

            return ApiResponse.Ok("Usuario registrado correctamente.");
        }

        private async Task<ApiResponse> ActualizarUsuarioAsync(UsuarioGuardarViewModel model, Rol rol)
        {
            if (!_currentUserService.EsAdministrador)
            {
                return ApiResponse.Fail("No tiene permisos para administrar usuarios.");
            }

            var usuario = await ObtenerUsuarioAsync(model.Id!.Value);

            if (usuario is null)
            {
                return ApiResponse.Fail("No se encontró el usuario.");
            }

            var esMismoUsuario = usuario.Id == _currentUserService.UsuarioId;
            var esAdministrador = usuario.Rol.Nombre == RolesSistema.Administrador;

            if (esMismoUsuario)
            {
                if (!esAdministrador)
                {
                    return ApiResponse.Fail("Solo un administrador puede editar su propio usuario.");
                }

                if (string.IsNullOrWhiteSpace(model.Contrasena))
                {
                    return ApiResponse.Ok("Usuario actualizado correctamente.");
                }

                usuario.ContrasenaHash = _passwordService.HashPassword(model.Contrasena);
                usuario.FechaModificacion = DateTime.Now;

                await GuardarCambiosAsync();

                return ApiResponse.Ok("Contraseña actualizada correctamente.");
            }

            if (esAdministrador)
            {
                return ApiResponse.Fail("No puede editar otro administrador.");
            }

            if (rol.Nombre == RolesSistema.Administrador)
            {
                return ApiResponse.Fail("No puede cambiar un usuario a administrador.");
            }

            _mapper.Map(model, usuario);

            if (!string.IsNullOrWhiteSpace(model.Contrasena))
            {
                usuario.ContrasenaHash = _passwordService.HashPassword(model.Contrasena);
            }

            usuario.IdRol = rol.Id;
            usuario.FechaModificacion = DateTime.Now;

            await GuardarCambiosAsync();

            return ApiResponse.Ok("Usuario actualizado correctamente.");
        }

        private async Task<bool> EsUnicoAdministradorActivoAsync(Guid usuarioId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario is null || usuario.Rol.Nombre != RolesSistema.Administrador || !usuario.Activo)
            {
                return false;
            }

            var administradoresActivos = await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Rol)
                .CountAsync(u => u.Activo && u.Rol.Nombre == RolesSistema.Administrador);

            return administradoresActivos == 1;
        }
        #endregion

        public async Task<ApiResponse> ObtenerTodosAsync()
        {
            var usuarios = await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Rol)
                .OrderBy(u => u.NombreUsuario)
                .ToListAsync();

            var resultado = _mapper.Map<IEnumerable<UsuarioListadoViewModel>>(usuarios);

            return ApiResponse.Ok(data: resultado);
        }

        public async Task<ApiResponse> ObtenerPorIdAsync(Guid id)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario is null)
            {
                return ApiResponse.Fail("No se encontró el usuario.");
            }

            return ApiResponse.Ok(data: _mapper.Map<UsuarioDetalleViewModel>(usuario));
        }

        public async Task<ApiResponse> GuardarAsync(UsuarioGuardarViewModel model)
        {
            model.NombreUsuario = Normalizar(model.NombreUsuario);
            model.Correo = Normalizar(model.Correo);
            model.Contrasena = Normalizar(model.Contrasena);

            if (string.IsNullOrWhiteSpace(model.NombreUsuario))
            {
                return ApiResponse.Fail("El usuario es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(model.Correo))
            {
                return ApiResponse.Fail("El correo es obligatorio.");
            }

            if (!model.IdRol.HasValue || model.IdRol == Guid.Empty)
            {
                return ApiResponse.Fail("El rol es obligatorio.");
            }

            var rol = await ObtenerRolAsync(model.IdRol.Value);

            if (rol is null)
            {
                return ApiResponse.Fail("El rol seleccionado no existe.");
            }

            if (await ExisteNombreUsuarioAsync(model.NombreUsuario, model.Id))
            {
                return ApiResponse.Fail("Ya existe un usuario con ese nombre de usuario.");
            }

            if (await ExisteCorreoAsync(model.Correo, model.Id))
            {
                return ApiResponse.Fail("Ya existe un usuario con ese correo.");
            }

            if (model.Id.HasValue)
            {
                return await ActualizarUsuarioAsync(model, rol);
            }

            return await CrearUsuarioAsync(model, rol);
        }

        public async Task<ApiResponse> CambiarEstadoAsync(Guid id)
        {
            if (!_currentUserService.EsAdministrador)
            {
                return ApiResponse.Fail("No tiene permisos para administrar usuarios.");
            }

            var usuario = await ObtenerUsuarioAsync(id);

            if (usuario is null)
            {
                return ApiResponse.Fail("El usuario no fue encontrado.");
            }

            if (usuario.Id == _currentUserService.UsuarioId)
            {
                return ApiResponse.Fail("No puede cambiar su propio estado.");
            }

            if (usuario.Rol.Nombre == RolesSistema.Administrador)
            {
                if (await EsUnicoAdministradorActivoAsync(usuario.Id))
                {
                    return ApiResponse.Fail("No se puede inactivar el único administrador activo.");
                }

                return ApiResponse.Fail("No puede activar o inactivar otro administrador.");
            }

            usuario.Activo = !usuario.Activo;
            usuario.FechaModificacion = DateTime.Now;

            await GuardarCambiosAsync();

            return ApiResponse.Ok(usuario.Activo
                ? "Usuario activado correctamente."
                : "Usuario inactivado correctamente.");
        }
    }
}
