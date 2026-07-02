const Validators = (() => {

    const soloLetras = (input) => {

        input.addEventListener("input", () => {

            input.value = input.value.replace(
                /[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g,
                "");

        });

    };

    const soloNumeros = (input) => {

        input.addEventListener("input", () => {

            input.value = input.value.replace(/\D/g, "");

        });

    };

    const limitarLongitud = (input, longitud) => {

        input.addEventListener("input", () => {

            input.value = input.value.substring(0, longitud);

        });

    };

    const mascaraTelefono = (input) => {

        input.addEventListener("input", () => {

            let valor = input.value.replace(/\D/g, "");

            valor = valor.substring(0, 10);

            if (valor.length > 6)
                valor = valor.replace(
                    /(\d{3})(\d{3})(\d+)/,
                    "$1-$2-$3");

            else if (valor.length > 3)
                valor = valor.replace(
                    /(\d{3})(\d+)/,
                    "$1-$2");

            input.value = valor;

        });

    };

    return {

        soloLetras,

        soloNumeros,

        limitarLongitud,

        mascaraTelefono

    };

})();