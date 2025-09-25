using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Grave;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http;
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
                WriteIndented = true
            });

            var request = new HttpRequestMessage(HttpMethod.Post, "FALTASSPN/AddAsync")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", addFaltasDeServidoresPublicosNoGraves.Token);

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"⚠️ Error en la API: {response.StatusCode}");
                Console.WriteLine(errorContent);
                return null;
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
                var request = new HttpRequestMessage(HttpMethod.Get, "FALTASSPN");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", searchFaltasDeServidoresPublicosG.Token);

                var response = await _http.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<AddFaltasDeServidoresPublicosNoGraves>>>();
                return result.Data.Where(falta => falta.Activo == 1).ToList();
                
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de obtener las Faltas de Servidores Publicos Graves. {ex.Message}");
				return null;
			}
		}
	}
}
