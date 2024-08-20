function ConfigEmailValidation() {
    var form = document.getElementById('contactForm');
    var emailInput = document.getElementById('email');
    var submitButton = document.getElementById('submitButton');
    var successMessage = document.getElementById('submitSuccessMessage');
    var errorMessage = document.getElementById('submitErrorMessage');
    var errorMessageMessage = document.getElementById('submitErrorMessageMessage');
    var emailRequiredFeedback = document.querySelector('[data-sb-feedback="email:required"]');
    var emailInvalidFeedback = document.querySelector('[data-sb-feedback="email:email"]');
    var submitButtonContainer = submitButton.parentElement; // Obtiene el contenedor del botón de envío

    // Función para validar el correo electrónico
    function validateEmail(email) {
        return email.match(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@(([^<>()[\]\\.,;:\s@"]+\.)+[^<>()[\]\\.,;:\s@"]{2,})$/i);
    }

    // Función para manejar la validación en tiempo real
    emailInput.addEventListener('input', function () {
        var email = emailInput.value;
        var isValid = validateEmail(email);
        submitButton.classList.toggle('disabled', !isValid || !email);

        // Manejo de mensajes de validación
        emailRequiredFeedback.style.display = email ? 'none' : 'block';
        emailInvalidFeedback.style.display = isValid || !email ? 'none' : 'block';

        // Muestra el botón de envío si el usuario está corrigiendo el correo
        submitButtonContainer.style.display = '';
        successMessage.classList.add('d-none');
        errorMessage.classList.add('d-none');
    });

    // Manejar el envío del formulario
    form.addEventListener('submit', function (event) {
        event.preventDefault();
        var email = emailInput.value;

        var saveEmail = new SaveEmail();
        saveEmail.save(email, (data) => {
            successMessage.classList.remove('d-none');
            errorMessage.classList.add('d-none');
            // Oculta el botón de envío después del envío
            submitButtonContainer.classList.add("d-none");
        }, (error) => {
            successMessage.classList.add('d-none');
            errorMessage.classList.remove('d-none');
            errorMessageMessage.textContent = error.message;
        });
    });
    console.log('ConfigEmailValidation');
};