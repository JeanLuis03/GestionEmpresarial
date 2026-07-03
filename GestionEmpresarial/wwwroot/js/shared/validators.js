const Validators = (() => {

    const soloLetras = (input) => {

        input.addEventListener("input", () => {

            input.value = input.value.replace(
                /[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g,
                "");

        });

    };

    const soloDecimales = (input) => {

        input.addEventListener("input", () => {

            let valor = input.value.replace(/[^0-9.,]/g, "");

            valor = valor.replace(",", ".");

            const partes = valor.split(".");

            if (partes.length > 2) {

                valor = `${partes[0]}.${partes.slice(1).join("")}`;

            }

            const [entero, decimal] = valor.split(".");

            if (decimal !== undefined) {

                valor = `${entero}.${decimal.substring(0, 2)}`;

            }

            input.value = valor;

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

        soloDecimales,

        limitarLongitud,

        mascaraTelefono

    };

})();