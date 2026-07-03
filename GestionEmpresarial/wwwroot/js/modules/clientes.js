let tablaClientes;

const ClientesModule = (() => {

    let modalCliente;
    const btnNuevoCliente = document.getElementById("btnNuevoCliente");

    const init = () => {

        modalCliente = new bootstrap.Modal(
            document.getElementById("clienteModal"));

        registrarEventos();

        inicializarValidaciones();
        inicializarTabla();

    };

    const registrarEventos = () => {

        if (btnNuevoCliente) {

            btnNuevoCliente.addEventListener("click", abrirModalNuevo);
        }

        document
            .getElementById("formCliente")
            .addEventListener("submit", guardarCliente);

        document
            .querySelector("#tablaClientes tbody")
            .addEventListener("click", manejarAccionesTabla);

    };

    //#region REQUESTS

    const obtenerClientes = async () => {

        const response = await Api.get("/Cliente/ObtenerListado");

        if (!response.ok)
            return [];

        return response.data;

    };

    const guardarCliente = async (e) => {

        e.preventDefault();

        const model = {

            id: document.getElementById("Id").value || null,

            nombre: document.getElementById("Nombre").value,

            apellido: document.getElementById("Apellido").value,

            telefono: document.getElementById("Telefono").value,

            correo: document.getElementById("Correo").value,

            direccion: document.getElementById("Direccion").value

        };

        const response = await Api.post(
            "/Cliente/Guardar",
            model);

        if (!response.ok) {

            await Alerts.error(response.data.message);

            return;

        }

        modalCliente.hide();

        await Alerts.success(response.data.message);

        await recargarTabla();

    };

    const editarCliente = async (id) => {

        const response = await Api.get(
            `/Cliente/ObtenerPorId?id=${id}`);

        if (!response.ok) {

            await Alerts.error("No fue posible obtener el cliente.");

            return;

        }

        const cliente = response.data;

        document.getElementById("Id").value = cliente.id;

        document.getElementById("Nombre").value = cliente.nombre;

        document.getElementById("Apellido").value = cliente.apellido;

        document.getElementById("Telefono").value = cliente.telefono;

        document.getElementById("Correo").value = cliente.correo;

        document.getElementById("Direccion").value = cliente.direccion;

        document.getElementById("tituloModal")
            .textContent = "Editar Cliente";

        modalCliente.show();

    };

    const eliminarCliente = async (id) => {

        const confirmacion = await Alerts.confirm(

            "Eliminar Cliente",

            "¿Desea eliminar este cliente?"

        );

        if (!confirmacion.isConfirmed)
            return;

        const response = await Api.post(

            "/Cliente/Eliminar",

            {

                id: id

            }

        );

        if (!response.ok) {

            await Alerts.error(response.data.message);

            return;

        }

        await Alerts.success(response.data.message);

        await recargarTabla();

    };

    //#endregion


    //#region FORM METHODS

    const abrirModalNuevo = () => {

        limpiarFormulario();

        document
            .getElementById("tituloModal")
            .textContent = "Nuevo Cliente";

        document
            .getElementById("btnGuardar")
            .textContent = "Guardar";

        modalCliente.show();

    };

    const cerrarModal = () => {

        modalCliente.hide();

    };

    const limpiarFormulario = () => {

        document
            .getElementById("formCliente")
            .reset();

        document
            .getElementById("Id")
            .value = "";

        document
            .getElementById("tituloModal")
            .textContent = "Nuevo Cliente";

        document
            .getElementById("btnGuardar")
            .textContent = "Guardar";

    };

    const manejarAccionesTabla = async (e) => {

        const botonEditar = e.target.closest(".btn-editar");

        if (botonEditar) {

            const id = botonEditar.dataset.id;

            await editarCliente(id);

            return;

        }

        const botonEliminar = e.target.closest(".btn-eliminar");

        if (botonEliminar) {

            const id = botonEliminar.dataset.id;

            await eliminarCliente(id);

        }

    };

    const inicializarValidaciones = () => {

        Validators.soloLetras(
            document.getElementById("Nombre"));

        Validators.soloLetras(
            document.getElementById("Apellido"));

        Validators.mascaraTelefono(
            document.getElementById("Telefono"));

        Validators.limitarLongitud(
            document.getElementById("Nombre"),
            100);

        Validators.limitarLongitud(
            document.getElementById("Apellido"),
            100);

        Validators.limitarLongitud(
            document.getElementById("Correo"),
            150);

        Validators.limitarLongitud(
            document.getElementById("Direccion"),
            250);

    };

    //#endregion


    //#region DATATABLE METHODS
    const recargarTabla = async () => {

        tablaClientes.clear();

        tablaClientes.rows.add(
            await obtenerClientes());

        tablaClientes.draw();

    };

    const construirColumnas = () => {

        const columnas = [
            {
                data: "nombreCompleto"
            },
            {
                data: "telefono"
            },
            {
                data: "correo"
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

    const renderAcciones = (data, type, row) => {

        let acciones = "";

        if (puedeEditar) {

            acciones += `
            <button
                class="btn btn-warning btn-sm btn-editar"
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
                class="btn btn-danger btn-sm btn-eliminar"
                data-id="${row.id}">

                <span class="material-symbols-outlined">
                    delete
                </span>

            </button>
        `;

        }

        return acciones;

    };

    const inicializarTabla = async () => {

        const columnas = construirColumnas();

        tablaClientes = $("#tablaClientes").DataTable({

            responsive: true,

            language: {
                url: "https://cdn.datatables.net/plug-ins/2.3.2/i18n/es-ES.json"
            },

            data: await obtenerClientes(),

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
    ClientesModule.init);