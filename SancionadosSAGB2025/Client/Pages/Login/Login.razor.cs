using Microsoft.AspNetCore.Components;
using MudBlazor;
using SancionadosSAGB2025.Shared.Login;

namespace SancionadosSAGB2025.Client.Pages.Login
{
	partial class Login
	{
		private LoginModel loginModel { set; get; } = new();

		private bool MostrarSpinner { get; set; }

		// Nuevas variables para el diseño y la validación
		private MudForm form; // Para validar el formulario
		private bool success; // Estado de validez del formulario
		private string[] errors = { }; // Errores de validación
		private bool showPassword = false; // Para alternar la visibilidad de la contraseña
		private bool isSubmitting = false; // Para controlar el estado del botón al enviar
		private string errorMessage = string.Empty; // Para mostrar mensajes de error específicos

		// Tus servicios inyectados
		[Inject] private HttpClient Http { get; set; }
		[Inject] private NavigationManager Navigation { get; set; }
		[Inject] private ISnackbar Snackbar { get; set; }

		// Tu lógica existente para OnInitializedAsync
		protected override async Task OnInitializedAsync()
		{

			// Aquí podrías inicializar MostrarSpinner si lo necesitas al cargar la página
			// MostrarSpinner = true; // Por ejemplo, si hay una carga inicial
		}

		// Nueva función para alternar la visibilidad de la contraseña
		private void TogglePasswordVisibility()
		{
			showPassword = !showPassword;
		}
		private async Task Submit()
		{
			await form.Validate();

			if (!success)
			{
				errorMessage = "Por favor, complete todos los campos requeridos.";
				return; // Detener la ejecución si la validación falla
			}

			errorMessage = string.Empty; // Limpiar cualquier error previo
			isSubmitting = true; // Activar el estado de envío del botón
			MostrarSpinner = true; // Activar el spinner de pantalla completa (si lo deseas mantener)

			try
			{
				var isSuccess = await AuthService.LoginAsync(loginModel);
				if (isSuccess)
				{
					//Snackbar.Add("Inicio de sesión exitoso!", Severity.Success);
					Navigation.NavigateTo("/dashboard");
				}
				else
				{
					errorMessage = "El nombre de usuario o contraseña es incorrecto";
					//await MensajeSweet.MostrarError(errorMessage);
				}
			}
			catch (Exception ex)
			{
				//Logger.LogError("Error al intentar iniciar sesión", ex);
				errorMessage = "Hubo un error en el servidor. Intente de nuevo más tarde.";
				//await MensajeSweet.MostrarError(errorMessage);
			}
			finally
			{
				isSubmitting = false; // Desactivar el estado de envío del botón
				MostrarSpinner = false; // Desactivar el spinner de pantalla completa
			}
		}
	}
}
