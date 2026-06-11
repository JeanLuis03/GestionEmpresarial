
const logoutForm = document.getElementById("logoutForm");

document.addEventListener("DOMContentLoaded", function () {

    eventos();
        
});


const eventos = () => { 

    logoutForm.addEventListener("submit", function (e) {

        e.preventDefault();

        Swal.fire({
            title: "Cerrar sesión",
            text: "¿Desea cerrar sesión?",
            icon: "question",
            showCancelButton: true,
            confirmButtonText: "Sí",
            cancelButtonText: "No",
            reverseButtons: true
        })
            .then((result) => {
                if (result.isConfirmed) {
                    logoutForm.submit();
                }
            });
    });

}

