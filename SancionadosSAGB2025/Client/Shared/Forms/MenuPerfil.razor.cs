using Microsoft.AspNetCore.Components;

namespace SancionadosSAGB2025.Client.Shared.Forms
{
	partial class MenuPerfil
	{
		[Parameter] public string? UsuarioNombre { get; set; }

		private async Task CerrarSesion()
		{
			await Auth.LogoutAsync();
		}

		private async Task ConsultarIdUsuario()
		{
			Navigation.NavigateTo("/profile");
		}
	}
}
