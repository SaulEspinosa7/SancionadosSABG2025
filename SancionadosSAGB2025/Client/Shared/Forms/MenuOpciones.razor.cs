using MudBlazor;
using MudBlazor.Charts;
using SancionadosSAGB2025.Shared.Modulos;

namespace SancionadosSAGB2025.Client.Shared.Forms
{
	partial class MenuOpciones
	{
		private List<Modulos> Modulos { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Modulos = new List<Modulos>
			{new Modulos
				{
					NombreModulo = "Faltas",
					IconModulo = "fas fa-calculator",
					SubModulos = new List<SubModulos>
					{
						//new SubModulos
						//{
						//	NombreSubModulo = "Abstenciones No Graves LGRA",
						//	UrlSubModulo = "/AbstencionesNoGraves",
						//},
						new SubModulos
						{
							NombreSubModulo = "Faltas de Servidores Públicos G",
							UrlSubModulo = "/ServidoresPúblicosG",
						}
						,
						new SubModulos
						{
							NombreSubModulo = "Faltas de Servidores Públicos N",
							UrlSubModulo = "/historial",
						}
						,
						new SubModulos
						{
							NombreSubModulo = "Faltas Graves Personas Físicas",
							UrlSubModulo = "/historial",
						}
						,
						new SubModulos
						{
							NombreSubModulo = "Faltas Graves Personas Morales",
							UrlSubModulo = "/historial",
						}
					}
				},
				new Modulos
				{
					NombreModulo = "Administración",
					IconModulo = "far fa-clone",
					SubModulos = new List<SubModulos>
					{
						new SubModulos
						{
							NombreSubModulo = "Usuarios",
							UrlSubModulo = "/usuarios",
						},
						new SubModulos
						{
							NombreSubModulo = "Roles",
							UrlSubModulo = "/roles",
						}
					}
				},
				new Modulos
				{
					NombreModulo = "Reportes",
					IconModulo = "fas fa-tools",
					SubModulos = new List<SubModulos>
					{
						new SubModulos
						{
							NombreSubModulo = "Estadísticas",
							UrlSubModulo = "/estadisticas",
						},
						new SubModulos
						{
							NombreSubModulo = "Historial",
							UrlSubModulo = "/historial",
						}
					}
				}
			};
		}


		private async Task Dirreccionar(string url)
		{
			Navigation.NavigateTo($"{url}");
		}

		private readonly Dictionary<string, string> _iconosDict = new()
		{
			{ "fas fa-calculator", Icons.Material.Filled.WarningAmber },
			{ "far fa-clone", Icons.Material.Filled.SettingsApplications},
			{ "fas fa-tools", Icons.Material.Filled.Build },
			// Agrega más según tus necesidades
		};

		private string GetIcon(string? icono)
		{
			if (string.IsNullOrWhiteSpace(icono))
				return Icons.Material.Filled.HelpOutline;

			return _iconosDict.TryGetValue(icono.ToLower(), out var iconoResult)
				? iconoResult
				: Icons.Material.Filled.HelpOutline;
		}
	}
}