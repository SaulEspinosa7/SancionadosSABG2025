using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Client.Services;
using SancionadosSAGB2025.Shared.Login;

namespace SancionadosSAGB2025.Client.Shared.Forms
{
	partial class MainForm
	{
		[Parameter]
		public bool loading { set; get; } = false;

		[Parameter]
		public RenderFragment? Main { get; set; }

		[Parameter]
		public string? MainHeight { get; set; } = "32vw";

		[Parameter]
		public RenderFragment? ContentBreadcrumbs { set; get; }

		[Parameter]
		public int? view { set; get; } = 2;

		private AutenticacionResponse InformacionPerfilBD { get; set; } = new();

		protected override async Task OnInitializedAsync()
		{
			await ConsultarIdUsuario();
		}

		private async Task ConsultarIdUsuario()
		{
			try
			{
				var token = await AuthService.GetTokenAsync();

				//Console.WriteLine($" token {token}");

				if (!string.IsNullOrEmpty(token))
				{
					TokenResponse tokenUsuario = new();
					tokenUsuario.Token = token;
					AutenticacionResponse informacionPerfil = await AuthService.ConsultarInformacionPerfil(tokenUsuario);
					InformacionPerfilBD = informacionPerfil;
					//Console.WriteLine($"Usuario: {informacionPerfil.NombreUsuario}");
				}
				else
				{
					Navigation.NavigateTo("/login", forceLoad: true);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Exception {ex.Message}");
				Navigation.NavigateTo("/login", forceLoad: true);
			}
		}
	}
}
