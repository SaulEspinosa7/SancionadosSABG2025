using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Text;
using System.Text.Json;

namespace SancionadosSAGB2025.Server.Services
{
	public class FaltasGravesPersonasFisicasService : IFaltasGravesPersonasFisicas
	{
		private readonly HttpClient _http;

		public FaltasGravesPersonasFisicasService(HttpClient http)
		{
			_http = http;
		}

		public async Task<RespuestaRegistroFaltasGravesPersonasFisicas> AgregarFaltasGravesPersonasFisicas(AddFaltasGravesPersonasFisicas addFaltasGravesPersonasFisicas)
		{
			//addFaltasGravesPersonasFisicas = await ConstruirFaltasGravesPersonasFisicas(addFaltasGravesPersonasFisicas);
            var json = JsonSerializer.Serialize(addFaltasGravesPersonasFisicas, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            var request = new HttpRequestMessage(HttpMethod.Post, "FGPF/AddAsync")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", addFaltasGravesPersonasFisicas.Token);

            var response = await _http.SendAsync(request);
             var respuesta = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var respuestaerror = await response.Content.ReadFromJsonAsync<RespuestaRegistroFaltasGravesPersonasFisicas>();              
                return respuestaerror;
            }
            var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroFaltasGravesPersonasFisicas>();
            if (result.Mensaje?.Contains("REGISTRO ELIMINADO CORRECTAMENTE") == true)
            {
                result.Response = true;
            }
            return result;
          
		}

		public async Task<AddFaltasGravesPersonasFisicas> ConstruirFaltasGravesPersonasFisicas(AddFaltasGravesPersonasFisicas addFaltasGravesPersonasFisicas)
		{
			addFaltasGravesPersonasFisicas.DatosGenerales.DomicilioMexico.TipoVialidad = null;
			addFaltasGravesPersonasFisicas.DatosGenerales.DomicilioMexico.EntidadFederativa = null;
			addFaltasGravesPersonasFisicas.Indeminizacion.Moneda = null;
			addFaltasGravesPersonasFisicas.SancionEfectivamenteCobrada.Moneda = null;
			addFaltasGravesPersonasFisicas.SancionEconomica.Moneda = null;
			return addFaltasGravesPersonasFisicas;
		}

		public async Task<List<AddFaltasGravesPersonasFisicas>> ObtenerFaltasGravesPersonasFisicas(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "FGPF");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", searchFaltasDeServidoresPublicosG.Token);

                var response = await _http.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<List<AddFaltasGravesPersonasFisicas>>();
                return result.Where(falta => falta.Activo == 1).ToList();
               
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de obtener las Faltas de Servidores Publicos Graves. {ex.Message}");
				return null;
			}
		}
	}
}
