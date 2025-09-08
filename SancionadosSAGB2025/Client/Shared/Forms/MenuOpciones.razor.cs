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
							NombreSubModulo = "Faltas de Servidores Públicos Graves",
							UrlSubModulo = "/ServidoresPúblicosG",
						}
						,
						new SubModulos
						{
							NombreSubModulo = "Faltas de Servidores Públicos No Graves",
							UrlSubModulo = "/ServidoresPúblicosN",
						}
						,
						new SubModulos
						{
							NombreSubModulo = "Faltas Graves Personas Físicas",
							UrlSubModulo = "/FaltasGravesPersonasFísicas",
						}
						,
						new SubModulos
						{
							NombreSubModulo = "Faltas Graves Personas Morales",
							UrlSubModulo = "/FaltasGravesPersonasMorales",
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
							NombreSubModulo = "Cambio de Contraseña",
							UrlSubModulo = "/cambiocontraseña",
						}
					}
				//},
				//new Modulos
				//{
				//	NombreModulo = "Reportes",
				//	IconModulo = "fas fa-tools",
				//	SubModulos = new List<SubModulos>
				//	{
				//		new SubModulos
				//		{
				//			NombreSubModulo = "Estadísticas",
				//			UrlSubModulo = "/estadisticas",
				//		},
				//		new SubModulos
				//		{
				//			NombreSubModulo = "Historial",
				//			UrlSubModulo = "/historial",
				//		}
				//	}
				},
                new Modulos
                {
                    NombreModulo = "Catalogos",
                    IconModulo = "fas fa-book",
                    SubModulos = new List<SubModulos>
                    {
                        new SubModulos
                        {
                            NombreSubModulo = "Ambito Público",
                            UrlSubModulo = "/AmbitoPublico",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Entidad Federativa",
                            UrlSubModulo = "/EntidadFederativa",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Falta Cometida",
                            UrlSubModulo = "/FaltaCometida",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Nivel Jerarquico",
                            UrlSubModulo = "/NivelJerarquico",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Nivel Orden Gobierno",
                            UrlSubModulo = "/NivelOrdenGobierno",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Orden Jurisdiccional",
                            UrlSubModulo = "/OrdenJurisdiccional",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Origen Procedimiento",
                            UrlSubModulo = "/OrigenProcedimiento",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Tipo Amonestacion",
                            UrlSubModulo = "/TipoAmonestacion",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Tipo Sanción",
                            UrlSubModulo = "/TipoSancion",
                        },
                        new SubModulos
                        {
                            NombreSubModulo = "Tipo Vialidad",
                            UrlSubModulo = "/TipoVialidad",
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
			{ "fas fa-book", Icons.Material.Filled.LibraryBooks }
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