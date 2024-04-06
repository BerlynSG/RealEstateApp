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