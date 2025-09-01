// wwwroot/js/app.js

window.formatAndValidateClave = function (id, type) {
    const input = document.getElementById(id);
    if (!input) return;

    input.addEventListener('input', function () {
        let value = input.value.toUpperCase().replace(/[^A-Z0-9Ñ&]/g, '');
        input.value = value;

        let isValid = true;
        let message = '';

        if (type === 'CURP') {
            const regex = /^[A-Z]{4}\d{6}[HM][A-Z]{2}[A-Z]{3}[0-9A-Z]\d$/;
            if (value.length !== 18 || !regex.test(value)) {
                isValid = false;
                message = 'CURP inválido. Formato esperado: 18 caracteres con estructura oficial.';
            }
        }

        if (type === 'RFC') {
            const regex = /^[A-ZÑ&]{4}\d{6}[A-Z0-9]{3}$/;
            if (value.length !== 13 || !regex.test(value)) {
                isValid = false;
                message = 'RFC inválido. Formato esperado: 13 caracteres con estructura oficial.';
            }
        }

        input.setCustomValidity(isValid ? '' : message);
        input.reportValidity();
    });
};

window.formatAndAllowSpaces = function (id) {
    const input = document.getElementById(id);
    if (!input) return;

    input.addEventListener('input', function () {
        // Permitir letras, números, Ñ, &, y espacios
        let value = input.value.toUpperCase().replace(/[^A-Z0-9Ñ&\s]/g, '');
        input.value = value;

        // No requiere validación extra, pero puedes agregar si lo deseas
        input.setCustomValidity('');
        input.reportValidity();
    });
};

