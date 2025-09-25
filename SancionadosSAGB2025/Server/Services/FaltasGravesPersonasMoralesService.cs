using SancionadosSAGB2025.Shared;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Server.Services
{
    public class FaltasGravesPersonasMoralesService(HttpClient httpClient, IConfiguration configuration)
    {
        public async Task<RespuestaRegistroFaltasGravesPersonasMorales> AgregarFaltasGravesPersonasMorales(PersonaMoralEntidad addFaltasGravesPersonasMorales)
        {

            var json = JsonSerializer.Serialize(addFaltasGravesPersonasMorales, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            var request = new HttpRequestMessage(HttpMethod.Post, "FGPMD/AddAsync")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", addFaltasGravesPersonasMorales.Token);

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"⚠️ Error en la API: {response.StatusCode}");
                Console.WriteLine(errorContent);
                return null;
            }
            var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroFaltasGravesPersonasMorales>();
            if (result?.Mensaje?.Contains("REGISTRO ELIMINADO CORRECTAMENTE") == true)
            {
                result.Response = true;
            }
            return result;
           
        }

        public async Task<List<PersonaMoralEntidad>> ObtenerFaltasGravesPersonasMorales(string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "FGPMD/Get");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token);

                var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<List<PersonaMoralEntidad>>();
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
