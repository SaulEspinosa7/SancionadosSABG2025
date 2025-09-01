using Microsoft.AspNetCore.Components;
using MudBlazor;
using SancionadosSAGB2025.Client.Services;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http;
using static MudBlazor.CategoryTypes;

namespace SancionadosSAGB2025.Client.Componentes.FaltasDeServidoresPúblicosG.Components
{
	partial class BuscarFaltasSPG
	{
		[Inject] private NavigationManager Navigation { get; set; }
		private SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG { get; set; } = new();
		private List<AddFaltasDeServidoresPublicosG> faltasDeServidoresPublicosGs { get; set; } = new();
		private AddFaltasDeServidoresPublicosG faltasDeServidoresPublicosGsSeleccionadas { get; set; } = new();
		private TipoEvento TiposEventos { get; set; } = TipoEvento.Principal;
		private int IdUsuario { get; set; } = 0;

		private IEnumerable<AddFaltasDeServidoresPublicosG> Elements = new List<AddFaltasDeServidoresPublicosG>();
		private string _searchString;
		private bool _sortNameByLength;
		private List<string> _events = new();

		protected override async Task OnInitializedAsync()
		{
			await BuscarFaltasServidoresPG();
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
					if (informacionPerfil.Usuario is not null)
					{
						IdUsuario = informacionPerfil.Usuario.Id;
					}
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

		public async Task MostrarVistaActualizarFalta(AddFaltasDeServidoresPublicosG addFaltasDeServidoresPublicosG) 
		{
			TiposEventos = TipoEvento.Actualizar;
			faltasDeServidoresPublicosGsSeleccionadas = addFaltasDeServidoresPublicosG;
		}

		public async Task MostrarVisualizacionFalta(AddFaltasDeServidoresPublicosG addFaltasDeServidoresPublicosG)
		{
			TiposEventos = TipoEvento.Ver;
			faltasDeServidoresPublicosGsSeleccionadas = addFaltasDeServidoresPublicosG;
		}

		public async Task BuscarFaltasServidoresPG()
		{
			try
			{
				var result = await FaltasSPGService.ObtenerFaltasSPG(searchFaltasDeServidoresPublicosG);

				if (result.Count () > 0)
				{
					faltasDeServidoresPublicosGs = result;
					Elements = result;
				}
				else
				{
					Snackbar.Add("Hubo un error al guardar la información previa.", Severity.Error);
					//Snackbar.Add("Se guardó la información previa.", Severity.Success);
					//RespuestaRegistro = result!;
					Console.WriteLine($"hubo un error.");
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add($"Error en el proceso {ex.Message}", Severity.Error);
				//Snackbar.Add("Se guardó la información previa.", Severity.Success);
				//RespuestaRegistro = result!;
			}
		}

		private Func<AddFaltasDeServidoresPublicosG, object> _sortBy => x =>
		{
			if (_sortNameByLength)
				return x.Expediente.Length;
			else
				return x.Expediente;
		};
		// quick filter - filter globally across multiple columns with the same input
		private Func<AddFaltasDeServidoresPublicosG, bool> _quickFilter => x =>
		{
			if (string.IsNullOrWhiteSpace(_searchString))
				return true;

			if (x.Expediente?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
				return true;

			if (x.DatosGenerales?.Curp?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
				return true;

			if (x.DatosGenerales?.Rfc?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
				return true;

			if (x.DatosGenerales?.Nombres?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
				return true;

			if (x.DatosGenerales?.PrimerApellido?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
				return true;

			if (x.DatosGenerales?.SegundoApellido?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
				return true;

			return false;
		};		

		// events
		void RowClicked(DataGridRowClickEventArgs<AddFaltasDeServidoresPublicosG> args)
		{
			_events.Insert(0, $"Event = RowClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
		}

		void RowRightClicked(DataGridRowClickEventArgs<AddFaltasDeServidoresPublicosG> args)
		{
			_events.Insert(0, $"Event = RowRightClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
		}

		void SelectedItemsChanged(HashSet<AddFaltasDeServidoresPublicosG> items)
		{
			_events.Insert(0, $"Event = SelectedItemsChanged, Data = {System.Text.Json.JsonSerializer.Serialize(items)}");
		}

		private Func<AddFaltasDeServidoresPublicosG, string> _cellStyleFunc => x =>
		{
			string style = "";

			if (x.Expediente == "Expediente")
				style += "background-color:#8CED8C";

			//else if (x.DatosGenerales == 2)
			//	style += "background-color:#E5BDE5";

			//else if (x.Number == 3)
			//	style += "background-color:#EACE5D";

			//else if (x.Number == 4)
			//	style += "background-color:#F1F165";

			//if (x.Molar > 5)
			//	style += ";font-weight:bold";

			return style;
		};

		public enum TipoEvento
		{
			Principal = 0,
			Ver = 1,
			Actualizar = 2,
			Eliminar = 3
		}	
	}
}
