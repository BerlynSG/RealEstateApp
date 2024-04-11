// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function mostrarModal(modal) {
    var miModal = new bootstrap.Modal(modal);
    miModal.show();
}

function mostrarEliminar(id) {
    mostrarModal("#eliminarModal");
    document.getElementById('EliminarCodigo').value = id;
}

function añadirMejora(id) {
    var mejoras = document.getElementById('mejoras');
    var check = document.getElementById('m-' + id);
    var nuevo = id + ",";
    if (mejoras.value.includes(nuevo)) {
        mejoras.value = mejoras.value.replace(nuevo, "");
        check.checked = false;
    } else {
        mejoras.value += id + ",";
        check.checked = true;
    }
}