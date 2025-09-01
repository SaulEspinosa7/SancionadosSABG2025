using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Text;
using System.Text.Json;

namespace SancionadosSAGB2025.Server.Services
{
	public class FaltasServidoresPublicosNoGravesService : IFaltasServidoresPublicosNoGraves
	{
		private readonly HttpClient _http;

		public FaltasServidoresPublicosNoGravesService(HttpClient http)
		{
			_http = http;
		}

		public async Task<RespuestaRegistroNoGraves> AgregarFaltasSPG(AddFaltasDeServidoresPublicosNoGraves addFaltasDeServidoresPublicosNoGraves)
		{
			var json = JsonSerializer.Serialize(addFaltasDeServidoresPublicosNoGraves, new JsonSerializerOptions
			{
				WriteIndented = true // Opcional: para que sea más legible
			});

			Console.WriteLine(json); // o usa tu logger

			// Luego lo mandas tú mismo
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"FALTASSPN/AddAsync", addFaltasDeServidoresPublicosNoGraves);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine(errorContent);
			}			

			var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroNoGraves>();

			if (result?.Mensaje?.Contains("REGISTRO ELIMINADO CORRECTAMENTE") == true)
			{
				result.Response = true;
			}

			return result;
		}

		public async Task<List<AddFaltasDeServidoresPublicosNoGraves>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			try
			{
				var response = await _http.GetAsync($"FALTASSPN");

				if (!response.IsSuccessStatusCode)
					return null;

				var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<List<AddFaltasDeServidoresPublicosNoGraves>>>();
				var faltas = wrapper?.Data ?? new(); // Aquí tienes tu lista real
				return faltas.Where(falta => falta.Activo == 1).ToList();
				//return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de obtener las Faltas de Servidores Publicos Graves. {ex.Message}");
				return null;
			}
		}
	}
}
