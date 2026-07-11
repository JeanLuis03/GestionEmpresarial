const ReportesModule = (() => {

    const btnGenerarReporte = document.getElementById("btnGenerarReporte");

    const init = () => {

        if (!btnGenerarReporte) {

            return;
        }

        btnGenerarReporte.addEventListener("click", abrirReporteProductos);

    };

    const abrirReporteProductos = () => {

        const reportServerUrl = "http://LAPTOP-JEAN/ReportServer";
        const reporteProductosUrl = `${reportServerUrl}?/Productos&rs:Command=Render`;

        window.open(reporteProductosUrl, "_blank");

    };

    return {
        init
    };

})();

document.addEventListener("DOMContentLoaded", () => {

    ReportesModule.init();

});