let tablaUsuarios;

const UsuariosModule = (() => {

    let modalUsuario;
    let roles = [];
    let rolesPromise = null;
    const btnNuevoUsuario = document.getElementById("btnNuevoUsuario");

    const init = async () => {

        modalUsuario = new bootstrap.Modal(
            document.getElementById("usuarioModal"));

        rolesPromise = cargarRoles();

        registrarEventos();

        inicializarValidaciones();
        await inicializarTabla();

    };

    const registrarEventos = () => {

        if (btnNuevoUsuario) {

            btnNuevoUsuario.addEventListener("click", abrirModalNuevo);
        }

        document
            .getElementById("formUsuario")
            .addEventListener("submit", guardarUsuario);

        document
            .querySelector("#tablaUsuarios tbody")
            .addEventListener("click", manejarAccionesTabla);

        document
            .getElementById("usuarioModal")
            .addEventListener("hidden.bs.modal", limpiarSelectRoles);

    };

    //#region REQUESTS

    const cargarRoles = async () => {

        const response = await Api.get("/Usuario/ObtenerRolesActivosCombo");

        if (!response.ok) {

            roles = [];

            return;
        }

        roles = response.data ?? [];

    };

    const obtenerUsuarios = async () => {

        const response = await Api.get("/Usuario/ObtenerTodos");

        if (!response.ok)
            return [];

        return response.data;

    };

    const guardarUsuario = async (e) => {

        e.preventDefault();

        const model = {

            id: document.getElementById("Id").value || null,

            nombreUsuario: document.getElementById("NombreUsuario").value,

            correo: document.getElementById("Correo").value,

            contrasena: document.getElementById("Contrasena").value,

            idRol: document.getElementById("IdRol").value || null

        };

        const response = await Api.post(
            "/Usuario/Guardar",
            model);

        if (!response.ok) {

            await Alerts.error(response.data.message);

            return;
        }

        modalUsuario.hide();

        await Alerts.success(response.data.message);

        await recargarTabla();

    };

    const editarUsuario = async (id) => {

        await rolesPromise;

        const response = await Api.get(
            `/Usuario/ObtenerPorId?id=${id}`);

        if (!response.ok) {

            await Alerts.error("No fue posible obtener el usuario.");

            return;
        }

        const usuario = response.data;

        limpiarFormulario();
        llenarSelectRoles(usuario.idRol);

        document.getElementById("Id").value = usuario.id;

        document.getElementById("NombreUsuario").value = usuario.nombreUsuario;

        document.getElementById("Correo").value = usuario.correo;

        document.getElementById("Contrasena").value = "";

        document.getElementById("tituloModal")
            .textContent = "Editar Usuario";

        document.getElementById("btnGuardar")
            .textContent = "Guardar";

        if (usuario.id === usuarioActualId) {

            bloquearCamposSoloPassword();
        }
        else {

            habilitarCamposEdicion();
        }

        modalUsuario.show();

    };

    const cambiarEstadoUsuario = async (id, activo) => {

        const textoAccion = activo ? "Inactivar" : "Activar";

        const confirmacion = await Alerts.confirm(
            `${textoAccion} Usuario`,
            `¿Desea ${textoAccion.toLowerCase()} este usuario?`);

        if (!confirmacion.isConfirmed)
            return;

        const response = await Api.post(
            "/Usuario/CambiarEstado",
            {
                id: id
            });

        if (!response.ok) {

            await Alerts.error(response.data.message);

            return;
        }

        await Alerts.success(response.data.message);

        await recargarTabla();

    };

    //#endregion


    //#region FORM METHODS

    const abrirModalNuevo = async () => {

        await rolesPromise;

        limpiarFormulario();
        habilitarCamposEdicion();
        llenarSelectRoles();

        document
            .getElementById("tituloModal")
            .textContent = "Nuevo Usuario";

        document
            .getElementById("btnGuardar")
            .textContent = "Guardar";

        modalUsuario.show();

    };

    const limpiarFormulario = () => {

        document
            .getElementById("formUsuario")
            .reset();

        document
            .getElementById("Id")
            .value = "";

        document
            .getElementById("tituloModal")
            .textContent = "Nuevo Usuario";

        document
            .getElementById("btnGuardar")
            .textContent = "Guardar";

        habilitarCamposEdicion();
        limpiarSelectRoles();

    };

    const limpiarSelectRoles = () => {

        document
            .getElementById("IdRol")
            .innerHTML = "";

    };

    const llenarSelectRoles = (rolSeleccionado = "") => {

        const select = document.getElementById("IdRol");

        limpiarSelectRoles();

        const opcionDefault = document.createElement("option");
        opcionDefault.value = "";
        opcionDefault.textContent = "Seleccione un rol";
        select.appendChild(opcionDefault);

        roles.forEach(rol => {

            const option = document.createElement("option");
            option.value = rol.id;
            option.textContent = rol.nombre;

            if (rol.id === rolSeleccionado) {
                option.selected = true;
            }

            select.appendChild(option);

        });

    };

    const habilitarCamposEdicion = () => {

        document.getElementById("NombreUsuario").readOnly = false;
        document.getElementById("Correo").readOnly = false;
        document.getElementById("IdRol").disabled = false;
        document.getElementById("Contrasena").readOnly = false;

    };

    const bloquearCamposSoloPassword = () => {

        document.getElementById("NombreUsuario").readOnly = true;
        document.getElementById("Correo").readOnly = true;
        document.getElementById("IdRol").disabled = true;
        document.getElementById("Contrasena").readOnly = false;

    };

    const manejarAccionesTabla = async (e) => {

        const botonEditar = e.target.closest(".btn-editar");

        if (botonEditar) {

            const id = botonEditar.dataset.id;

            await editarUsuario(id);

            return;

        }

        const botonEstado = e.target.closest(".btn-estado");

        if (botonEstado) {

            const id = botonEstado.dataset.id;
            const activo = botonEstado.dataset.activo === "true";

            await cambiarEstadoUsuario(id, activo);

        }

    };

    const inicializarValidaciones = () => {

        Validators.soloUsuario(
            document.getElementById("NombreUsuario"));

        Validators.limitarLongitud(
            document.getElementById("NombreUsuario"),
            50);

        Validators.soloCorreo(
            document.getElementById("Correo"));

        Validators.limitarLongitud(
            document.getElementById("Correo"),
            150);

        Validators.limitarLongitud(
            document.getElementById("Contrasena"),
            100);

    };

    //#endregion


    //#region DATATABLE METHODS
    const recargarTabla = async () => {

        tablaUsuarios.clear();

        tablaUsuarios.rows.add(
            await obtenerUsuarios());

        tablaUsuarios.draw();

    };

    const construirColumnas = () => {

        const columnas = [
            {
                data: "nombreUsuario"
            },
            {
                data: "correo"
            },
            {
                data: "rol"
            },
            {
                data: "estado",
                render: (data) => renderEstado(data)
            },
            {
                data: "ultimaFecha",
                render: (data) => renderFecha(data)
            }
        ];

        if (puedeEditar || puedeEliminar) {

            columnas.push({
                data: null,
                orderable: false,
                searchable: false,
                render: renderAcciones
            });

        }

        return columnas;

    };

    const renderEstado = (estado) => {

        const esActivo = estado === "Activo";

        return `
            <span class="badge ${esActivo ? "text-bg-success" : "text-bg-secondary"}">
                ${estado}
            </span>
        `;

    };

    const renderFecha = (valor) => {

        if (!valor)
            return "";

        return new Date(valor).toLocaleString("es-DO", {
            dateStyle: "short",
            timeStyle: "short"
        });

    };

    const renderAcciones = (data, type, row) => {

        let acciones = "";

        if (puedeEditar) {

            acciones += `
            <button
                class="btn btn-outline-warning btn-sm btn-editar"
                data-id="${row.id}">

                <span class="material-symbols-outlined">
                    edit
                </span>

            </button>
        `;

        }

        if (puedeEliminar) {

            acciones += `
            <button
                class="btn ${row.activo ? "btn-success" : "btn-danger"} btn-sm btn-estado"
                data-id="${row.id}"
                data-activo="${row.activo}">

                <span class="material-symbols-outlined">
                    ${row.activo ? "toggle_on" : "toggle_off"}
                </span>

            </button>
        `;

        }

        return acciones;

    };

    const inicializarTabla = async () => {

        const columnas = construirColumnas();

        tablaUsuarios = $("#tablaUsuarios").DataTable({

            responsive: true,
            autoWidth: false,
            scrollX: true,
            language: {
                url: "https://cdn.datatables.net/plug-ins/2.3.2/i18n/es-ES.json"
            },

            data: await obtenerUsuarios(),

            columns: columnas

        });

    };

    //#endregion

    return {

        init

    };

})();

document.addEventListener(
    "DOMContentLoaded",
    UsuariosModule.init);
