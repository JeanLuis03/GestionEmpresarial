(function () {
    let counter = 0;

    const obtenerOverlay = () => document.getElementById("globalLoadingOverlay");

    window.mostrarLoading = () => {
        counter += 1;

        const overlay = obtenerOverlay();

        if (!overlay) {
            return;
        }

        overlay.classList.add("is-visible");
        overlay.setAttribute("aria-hidden", "false");
    };

    window.ocultarLoading = () => {
        counter = Math.max(0, counter - 1);

        if (counter > 0) {
            return;
        }

        const overlay = obtenerOverlay();

        if (!overlay) {
            return;
        }

        overlay.classList.remove("is-visible");
        overlay.setAttribute("aria-hidden", "true");
    };
})();