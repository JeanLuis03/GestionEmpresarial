let tablaCategorias;

const CategoriasModule = (() => {

    let modalCategoria;
    const btnNuevoCategoria = document.getElementById("btnNuevoCategoria");

    const init = () => {

        modalCategoria = new bootstrap.Modal(
            document.getElementById("categoriaModal"));

        registrarEventos();

        inicializarValidaciones();
        inicializarTabla();

    };

    const registrarEventos = () => {

        if (btnNuevoCategoria) {

            btnNuevoCategoria.addEventListener("click", abrirModalNuevo);
        }

        document
            .getElementById("formCategoria")
            .addEventListener("submit", guardarCategoria);

        document
            .querySelector("#tablaCategorias tbody")
            .addEventListener("click", manejarAccionesTabla);

    };

    //#region REQUESTS

    const obtenerCategorias = async () => {

        const response = await Api.get("/Categoria/ObtenerTodos");

        if (!response.ok)
            return [];

        return response.data;

    };

    const guardarCategoria = async (e) => {

        e.preventDefault();

        const model = {

            id: document.getElementById("Id").value || null,

            nombre: document.getElementById("Nombre").value

        };

        const response = await Api.post(
            "/Categoria/Guardar",
            model);

        if (!response.ok) {

            await Alerts.error(response.data.message);

            return;

        }

        modalCategoria.hide();

        await Alerts.success(response.data.message);

        await recargarTabla();

    };

    const editarCategoria = async (id) => {

        const response = await Api.get(
            `/Categoria/ObtenerPorId?id=${id}`);

        if (!response.ok) {

            await Alerts.error("No fue posible obtener la categoría.");

            return;

        }

        const categoria = response.data;

        document.getElementById("Id").value = categoria.id;

        document.getElementById("Nombre").value = categoria.nombre;

        document.getElementById("tituloModal")
            .textContent = "Editar Categoría";

        modalCategoria.show();

    };

    const eliminarCategoria = async (id) => {

        const confirmacion = await Alerts.confirm(

            "Eliminar Categoría",

            "¿Desea eliminar esta categoría?"

        );

        if (!confirmacion.isConfirmed)
            return;

        const response = await Api.post(

            "/Categoria/Eliminar",

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
            .textContent = "Registrar Categoría";

        document
            .getElementById("btnGuardar")
            .textContent = "Guardar";

        modalCategoria.show();

    };

    const cerrarModal = () => {

        modalCategoria.hide();

    };

    const limpiarFormulario = () => {

        document
            .getElementById("formCategoria")
            .reset();

        document
            .getElementById("Id")
            .value = "";

        document
            .getElementById("tituloModal")
            .textContent = "Registrar Categoría";

        document
            .getElementById("btnGuardar")
            .textContent = "Guardar";

    };

    const manejarAccionesTabla = async (e) => {

        const botonEditar = e.target.closest(".btn-editar");

        if (botonEditar) {

            const id = botonEditar.dataset.id;

            await editarCategoria(id);

            return;

        }

        const botonEliminar = e.target.closest(".btn-eliminar");

        if (botonEliminar) {

            const id = botonEliminar.dataset.id;

            await eliminarCategoria(id);

        }

    };

    const inicializarValidaciones = () => {

        Validators.soloLetras(
            document.getElementById("Nombre"));

        Validators.limitarLongitud(
            document.getElementById("Nombre"),
            100);

    };

    //#endregion


    //#region DATATABLE METHODS
    const recargarTabla = async () => {

        tablaCategorias.clear();

        tablaCategorias.rows.add(
            await obtenerCategorias());

        tablaCategorias.draw();

    };

    const construirColumnas = () => {

        const columnas = [
            {
                data: "nombre"
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
                class="btn btn-outline-danger btn-sm btn-eliminar"
                data-id="${row.id}">

                <span class="material-symbols-outlined">
                    delete
                </span>

            </button>
        `;

        }

        return `
            <div class="d-flex justify-content-center align-items-center gap-2">

                ${acciones}

            </div>
        `;

    };

    const inicializarTabla = async () => {

        const columnas = construirColumnas();

        tablaCategorias = $("#tablaCategorias").DataTable({

            responsive: true,
            autoWidth: false,
            scrollX: true,
            processing: true,
            pageLength: 10,
            lengthMenu: [
                [10, 25, 50, 100],
                [10, 25, 50, 100]
            ],
            language: {
                url: "https://cdn.datatables.net/plug-ins/2.3.2/i18n/es-ES.json"
            },

            data: await obtenerCategorias(),

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
    CategoriasModule.init);