using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Registros;

namespace SancionadosSAGB2025.Server.Services
{
	public class CatalogosService : ICatalogos
	{
		private readonly HttpClient _http;
		private readonly RegistroFaltasSPG registroFaltasSPG;

		public CatalogosService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<Catalogos> ObtenerTodosLosCatalogos()
		{
			try
			{
				Catalogos catalogos = new();

				catalogos.Sexo = await ObtenerSexo();
				catalogos.EntidadFederativas = await ObtenerEntidadFederativa();
				catalogos.NivelOrdenGobierno = await ObtenerNivelOrdenGobierno();
				catalogos.AmbitoPublico = await ObtenerAmbitoPublico();
				catalogos.FaltaCometidas = await ObtenerFaltaCometida();
				catalogos.NivelJerarquico = await ObtenerNivelJerarquico();

				return catalogos;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta de los catalogos.");
				return null;
			}
		}

		public async Task<List<AmbitoPublico>> ObtenerAmbitoPublico()
		{
			var response = await _http.GetAsync($"AmbitoPublico");

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<AmbitoPublico>>();
			return result;
		}		

		public async Task<List<EntidadFederativa>> ObtenerEntidadFederativa()
		{
			var response = await _http.GetAsync($"EntidadFederativaCat");

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<EntidadFederativa>>();
			return result;
		}

		public async Task<List<FaltaCometida>> ObtenerFaltaCometida()
		{
			var response = await _http.GetAsync($"FaltaCometida");

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<FaltaCometida>>();
			return result;
		}

		public async Task<List<NivelJerarquico>> ObtenerNivelJerarquico()
		{
			var response = await _http.GetAsync($"NivelJerarquico");

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<NivelJerarquico>>();
			return result;
		}

		public async Task<List<NivelOrdenGobierno>> ObtenerNivelOrdenGobierno()
		{
			var response = await _http.GetAsync($"NivelOrdenGobierno");

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<NivelOrdenGobierno>>();
			return result;
		}

		public async Task<List<OrdenJurisdiccional>> ObtenerOrdenJurisdiccional()
		{
			var response = await _http.PostAsync($"FaltasServidoresPG/AddAsync", null);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<OrdenJurisdiccional>>();
			return result;
		}

		public async Task<List<OrigenProcedimiento>> ObtenerOrigenProcedimiento()
		{
			var response = await _http.PostAsync($"FaltasServidoresPG/AddAsync", null);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<OrigenProcedimiento>>();
			return result;
		}

		public async Task<List<Sexo>> ObtenerSexo()
		{
			var response = await _http.PostAsync($"FaltasServidoresPG/AddAsync", null);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<Sexo>>();
			return result;
		}

		public async Task<List<TipoAmonestacion>> ObtenerTipoAmonestacion()
		{
			var response = await _http.PostAsync($"FaltasServidoresPG/AddAsync", null);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<TipoAmonestacion>>();
			return result;
		}

		public async Task<List<TipoSancion>> ObtenerTipoSancion()
		{
			var response = await _http.PostAsync($"FaltasServidoresPG/AddAsync", null);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<TipoSancion>>();
			return result;
		}

		public async Task<List<TipoVialidad>> ObtenerTipoVialidad()
		{
			var response = await _http.PostAsync($"FaltasServidoresPG/AddAsync", null);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<TipoVialidad>>();
			return result;
		}
	}
}
