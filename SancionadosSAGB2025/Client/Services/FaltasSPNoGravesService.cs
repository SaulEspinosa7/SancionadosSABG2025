using SancionadosSAGB2025.Client.Interfaces;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http.Json;

namespace SancionadosSAGB2025.Client.Services
{
	public class FaltasSPNoGravesService : IFaltasSPNoGraves
	{
		private readonly HttpClient _http;

		public FaltasSPNoGravesService(HttpClient http)
		{
			_http = http;
		}

		public async Task<RespuestaRegistroNoGraves> ActualizarFaltasSPG(AddFaltasDeServidoresPublicosNoGraves addFaltasDeServidoresPublicosNoGraves)
		{
			var response = await _http.PostAsJsonAsync("api/FaltasServidoresPublicosNoGraves/AgregarFaltasSPNoGraves", addFaltasDeServidoresPublicosNoGraves);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroNoGraves>();

			return result;
		}

		public async Task<RespuestaRegistroNoGraves> AgregarFaltasSPG(AddFaltasDeServidoresPublicosNoGraves addFaltasDeServidoresPublicosNoGraves)
		{
			var response = await _http.PostAsJsonAsync("api/FaltasServidoresPublicosNoGraves/AgregarFaltasSPNoGraves", addFaltasDeServidoresPublicosNoGraves);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroNoGraves>();

			return result;
		}

		public async Task<List<AddFaltasDeServidoresPublicosNoGraves>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			var response = await _http.PostAsJsonAsync("api/FaltasServidoresPublicosNoGraves/ObtenerFaltasSPNoGraves", searchFaltasDeServidoresPublicosG);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<AddFaltasDeServidoresPublicosNoGraves>>();

			return result;
		}
	}
}
