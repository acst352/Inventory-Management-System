// Limit user input to only numbers
function soloNumeros(inputSelector) {
    var input = document.querySelector(inputSelector);
    if (input) {
        input.addEventListener('input', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
        });
    }
}
// Shorten too long strings
function nombreCorto(nombre) {
    if (typeof nombre !== "string") return "";
    return nombre.length > 25 ? nombre.substring(0, 25) + "..." : nombre;
}