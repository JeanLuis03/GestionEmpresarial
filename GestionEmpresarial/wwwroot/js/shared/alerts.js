const Alerts = (() => {

    const success = async (message) => {

        await Swal.fire({

            icon: "success",

            title: "Éxito",

            text: message,

            confirmButtonText: "Aceptar"

        });

    };

    const error = async (message) => {

        await Swal.fire({

            icon: "error",

            title: "Error",

            html: message,

            confirmButtonText: "Aceptar"

        });

    };

    const confirm = async (
        title,
        text) => {

        return await Swal.fire({

            icon: "warning",

            title,

            text,

            showCancelButton: true,

            confirmButtonText: "Eliminar",

            cancelButtonText: "Cancelar",

            reverseButtons: true

        });

    };

    return {

        success,

        error,

        confirm
    };

})();