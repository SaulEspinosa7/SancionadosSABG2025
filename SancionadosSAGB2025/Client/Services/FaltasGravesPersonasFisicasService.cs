using SancionadosSAGB2025.Client.Interfaces;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http.Json;

namespace SancionadosSAGB2025.Client.Services
{
	public class FaltasGravesPersonasFisicasService : IFaltasGravesPersonasF
	{
		private readonly HttpClient _http;

		public FaltasGravesPersonasFisicasService(HttpClient http)
		{
			_http = http;
		}

		public async Task<RespuestaRegistroFaltasGravesPersonasFisicas> AgregarFaltasGravesPersonasFisicas(AddFaltasGravesPersonasFisicas addFaltasGravesPersonasFisicas)
		{
			var response = await _http.PostAsJsonAsync("api/FaltasGravesPersonasFisicas/AgregarFaltasGravesPersonasFisicas", addFaltasGravesPersonasFisicas);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroFaltasGravesPersonasFisicas>();

			return result;
		}

		public async Task<List<AddFaltasGravesPersonasFisicas>> ObtenerFaltasGravesPersonasFisicas(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			var response = await _http.PostAsJsonAsync("api/FaltasGravesPersonasFisicas/ObtenerFaltasGravesPersonasFisicas", searchFaltasDeServidoresPublicosG);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<AddFaltasGravesPersonasFisicas>>();

			return result;
		}
	}
}
