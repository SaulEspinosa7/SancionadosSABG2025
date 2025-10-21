using Microsoft.AspNetCore.Diagnostics;
using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Grave;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Server.Services
{
	public class FaltasServidoresPublicosGService(HttpClient httpClient, IConfiguration configuration) 
    {

        public async Task<RespuestaRegistro?> AgregarFaltasSPG(FaltasGravesEntidad faltasDeServidoresPublicosG)
        {
          
            var json = JsonSerializer.Serialize(faltasDeServidoresPublicosG, new JsonSerializerOptions
            {
                WriteIndented = true 
            });

            var request = new HttpRequestMessage(HttpMethod.Post, "FALTASSPG/AddAsync")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", faltasDeServidoresPublicosG.Token);

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var respuestaerror = await response.Content.ReadFromJsonAsync<RespuestaRegistro>();
                return respuestaerror;
            }            
            var result = await response.Content.ReadFromJsonAsync<RespuestaRegistro>();
            if (result?.Mensaje?.Contains("REGISTRO ELIMINADO CORRECTAMENTE") == true)
            {
                result.Response = true;
            }
            return result;
        }




        public async Task<List<FaltasGravesEntidad>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "FALTASSPG");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", faltasDeServidoresPublicosG.Token);

                var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<List<FaltasGravesEntidad>>();
                return result.Where(x => x.Activo == 1).ToList();                
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de obtener las Faltas de Servidores Publicos Graves. {ex.Message}");
				return null;
			}
		}
	}
}
