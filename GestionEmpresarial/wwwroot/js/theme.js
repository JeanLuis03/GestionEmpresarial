(function () {
    const storageKey = "theme";
    const darkClass = "dark-theme";
    const buttonId = "themeToggleButton";
    const iconId = "themeToggleIcon";
    const textId = "themeToggleText";

    const obtenerTemaGuardado = () => {
        const tema = localStorage.getItem(storageKey);

        return tema === "dark" ? "dark" : "light";
    };

    const actualizarBoton = (tema) => {
        const button = document.getElementById(buttonId);
        const icon = document.getElementById(iconId);
        const text = document.getElementById(textId);

        if (!button || !icon || !text) {
            return;
        }

        const esOscuro = tema === "dark";

        icon.textContent = esOscuro ? "light_mode" : "dark_mode";
        text.textContent = esOscuro ? "Modo Claro" : "Modo Oscuro";
        button.setAttribute("aria-pressed", esOscuro ? "true" : "false");
    };

    const aplicarTema = (tema) => {
        const esOscuro = tema === "dark";

        document.body.classList.toggle(darkClass, esOscuro);
        localStorage.setItem(storageKey, tema);
        actualizarBoton(tema);

        document.dispatchEvent(new CustomEvent("themechange", {
            detail: { theme: tema }
        }));
    };

    const alternarTema = () => {
        const temaActual = document.body.classList.contains(darkClass) ? "dark" : "light";
        aplicarTema(temaActual === "dark" ? "light" : "dark");
    };

    const init = () => {
        aplicarTema(obtenerTemaGuardado());

        const button = document.getElementById(buttonId);

        if (button) {
            button.addEventListener("click", alternarTema);
        }
    };

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    }
    else {
        init();
    }
})();