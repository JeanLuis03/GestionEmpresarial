namespace GestionEmpresarial.Helpers
{
    public static class SystemGuids
    {
        // Roles

        public static readonly Guid IdRolAdministrador =
            Guid.Parse("6c7d6f54-9e85-4fd0-9c0e-9f1d1e3f9c21");

        public static readonly Guid IdRolSupervisor =
            Guid.Parse("b2f1b4d8-7e7b-4e4f-9a4a-31a4e2a0e8f7");

        public static readonly Guid IdRolEjecutor =
            Guid.Parse("d0d8b7c2-3d42-4f26-bb6d-7a0f6c5a9b13");

        // Permisos

        public static readonly Guid IdPermisoAgregar =
            Guid.Parse("f5d9b1c6-8d3a-4f45-a1cb-2d5c3f7a6d44");

        public static readonly Guid IdPermisoEditar =
            Guid.Parse("a1c3d5e7-2f48-4e6c-8a2d-4f9b7c1e0d55");

        public static readonly Guid IdPermisoEliminar =
            Guid.Parse("7e2a9c1f-5b6d-4a3f-9c8e-1d2b3f4a5c66");

        public static readonly Guid IdPermisoConsultar =
            Guid.Parse("3c9f1a7e-6d2b-4b8f-9a1c-5e7d2f3b4a77");
    }
}
