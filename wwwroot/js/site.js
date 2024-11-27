
document.addEventListener("DOMContentLoaded", function() {
    const form = document.querySelector("form");
    const fechaInicioInput = document.getElementById("Fecha_Inicio");
    const fechaFinInput = document.getElementById("Fecha_Fin");
    const errorMessage = document.createElement("span");

    errorMessage.style.color = "red";
    errorMessage.style.display = "block";
    fechaFinInput.parentNode.appendChild(errorMessage);

    form.addEventListener("submit", function(event) {
        const fechaInicio = new Date(fechaInicioInput.value);
        const fechaFin = new Date(fechaFinInput.value);

        // Validar que la fecha de fin no sea anterior a la fecha de inicio
        if (fechaFin < fechaInicio) {
            errorMessage.textContent = "La fecha final debe ser después de la fecha de inicio.";
            event.preventDefault();
        } else {
            errorMessage.textContent = "";
        }
    });
});