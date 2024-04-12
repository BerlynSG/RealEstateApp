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
    if (mejoras.value.includes(id)) {
        if (mejoras.value.indexOf("," + id) != -1) {
            mejoras.value = mejoras.value.replace("," + id, "");
        } else if (mejoras.value.indexOf(id + ",") != -1) {
            mejoras.value = mejoras.value.replace(id + ",", "");
        } else if (mejoras.value == id + "") {
            mejoras.value = "";
        }
        check.checked = false;
    } else {
        if (mejoras.value != "") {
            mejoras.value += ",";
        }
        mejoras.value += id;
        check.checked = true;
    }
}