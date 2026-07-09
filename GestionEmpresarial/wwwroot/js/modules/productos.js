let tablaProductos;

const ProductosModule = (() => {

    let modalProducto;
    let categorias = [];
    let categoriasPromise = null;
    const btnNuevoProducto = document.getElementById("btnNuevoProducto");

    const init = async () => {

        modalProducto = new bootstrap.Modal(
            document.getElementById("productoModal"));

        categoriasPromise = cargarCategorias();

        registrarEventos();

        inicializarValidaciones();
        await inicializarTabla();

    };

    const registrarEventos = () => {

        if (btnNuevoProducto) {

            btnNuevoProducto.addEventListener("click", abrirModalNuevo);
        }

        document
            .getElementById("formProducto")
            .addEventListener("submit", guardarProducto);

        document
            .querySelector("#tablaProductos tbody")
            .addEventListener("click", manejarAccionesTabla);

        document
            .getElementById("productoModal")
            .addEventListener("hidden.bs.modal", limpiarSelectCategorias);

    };

    //#region REQUESTS

    const cargarCategorias = async () => {

        const response = await Api.get("/Categoria/ObtenerActivasCombo");

        if (!response.ok) {

            categorias = [];

            return;
        }

        categorias = response.data ?? [];

    };

    const obtenerProductos = async () => {

        const response = await Api.get("/Producto/ObtenerTodos");

        if (!response.ok)
            return [];

        return response.data;

    };

    const guardarProducto = async (e) => {

        e.preventDefault();

        const model = {

            id: document.getElementById("Id").value || null,

            codigo: document.getElementById("Codigo").value,

            nombre: document.getElementById("Nombre").value,

            marca: document.getElementById("Marca").value,

            modelo: document.getElementById("Modelo").value,

            precio: parseFloat(
                document.getElementById("Precio").value.replace(",", ".")) || 0,

            stock: parseInt(
                document.getElementById("Stock").value, 10) || 0,

            categoriaId: document.getElementById("CategoriaId").value || null

        };

        const response = await Api.post(
            "/Producto/Guardar",
            model);

        if (!response.ok) {

            await Alerts.error(response.data.message);

            return;
        }

        modalProducto.hide();

        await Alerts.success(response.data.message);

        await recargarTabla();

    };

    const editarProducto = async (id) => {

        await categoriasPromise;

        const response = await Api.get(
            `/Producto/ObtenerPorId?id=${id}`);

        if (!response.ok) {

            await Alerts.error("No fue posible obtener el producto.");

            return;
        }

        const producto = response.data;

        limpiarFormulario();
        llenarSelectCategorias(producto.categoriaId);

        document.getElementById("Id").value = producto.id;

        document.getElementById("Codigo").value = producto.codigo;

        document.getElementById("Nombre").value = producto.nombre;

        document.getElementById("Marca").value = producto.marca;

        document.getElementById("Modelo").value = producto.modelo ?? "";

        document.getElementById("Precio").value = producto.precio;

        document.getElementById("Stock").value = producto.stock;

        document.getElementById("tituloModal")
            .textContent = "Editar Producto";

        document.getElementById("btnGuardar")
            .textContent = "Guardar";

        modalProducto.show();

    };

    const eliminarProducto = async (id) => {

        const confirmacion = await Alerts.confirm(

            "Eliminar Producto",

            "¿Desea eliminar este producto?"

        );

        if (!confirmacion.isConfirmed)
            return;

        const response = await Api.post(

            "/Producto/Eliminar",

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

    const abrirModalNuevo = async () => {

        await categoriasPromise;

        limpiarFormulario();
        llenarSelectCategorias();

        document
            .getElementById("tituloModal")
            .textContent = "Registrar Producto";

        document
            .getElementById("btnGuardar")
            .textContent = "Guardar";

        modalProducto.show();

    };

    const limpiarFormulario = () => {

        document
            .getElementById("formProducto")
            .reset();

        document
            .getElementById("Id")
            .value = "";

        document
            .getElementById("tituloModal")
            .textContent = "Registrar Producto";

        document
            .getElementById("btnGuardar")
            .textContent = "Guardar";

        limpiarSelectCategorias();

    };

    const limpiarSelectCategorias = () => {

        document
            .getElementById("CategoriaId")
            .innerHTML = "";

    };

    const llenarSelectCategorias = (categoriaSeleccionada = "") => {

        const select = document.getElementById("CategoriaId");

        limpiarSelectCategorias();

        const opcionDefault = document.createElement("option");
        opcionDefault.value = "";
        opcionDefault.textContent = "Seleccione una categoría";
        select.appendChild(opcionDefault);

        categorias.forEach(categoria => {

            const option = document.createElement("option");
            option.value = categoria.id;
            option.textContent = categoria.nombre;

            if (categoria.id === categoriaSeleccionada) {
                option.selected = true;
            }

            select.appendChild(option);

        });

    };

    const manejarAccionesTabla = async (e) => {

        const botonEditar = e.target.closest(".btn-editar");

        if (botonEditar) {

            const id = botonEditar.dataset.id;

            await editarProducto(id);

            return;

        }

        const botonEliminar = e.target.closest(".btn-eliminar");

        if (botonEliminar) {

            const id = botonEliminar.dataset.id;

            await eliminarProducto(id);

        }

    };

    const inicializarValidaciones = () => {

        Validators.limitarLongitud(
            document.getElementById("Codigo"),
            20);

        Validators.limitarLongitud(
            document.getElementById("Nombre"),
            100);

        Validators.limitarLongitud(
            document.getElementById("Marca"),
            100);

        Validators.limitarLongitud(
            document.getElementById("Modelo"),
            100);

        Validators.soloDecimales(
            document.getElementById("Precio"));

        Validators.soloNumeros(
            document.getElementById("Stock"));

    };

    //#endregion


    //#region DATATABLE METHODS
    const recargarTabla = async () => {

        tablaProductos.clear();

        tablaProductos.rows.add(
            await obtenerProductos());

        tablaProductos.draw();

    };

    const construirColumnas = () => {

        const columnas = [
            {
                data: "codigo"
            },
            {
                data: "nombre"
            },
            {
                data: "marca"
            },
            {
                data: "modelo",
                defaultContent: ""
            },
            {
                data: "categoria"
            },
            {
                data: "precio",
                render: (data) => Number(data).toLocaleString("es-DO", {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                })
            },
            {
                data: "stock"
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

        tablaProductos = $("#tablaProductos").DataTable({

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

            data: await obtenerProductos(),

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
    ProductosModule.init);
