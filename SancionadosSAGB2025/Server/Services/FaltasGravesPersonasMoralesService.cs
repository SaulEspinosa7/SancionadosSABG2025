using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Server.Services
{
    public class FaltasGravesPersonasMoralesService(HttpClient httpClient)
    {
        public async Task<RespuestaRegistroFaltasGravesPersonasMorales> AgregarFaltasGravesPersonasMorales(PersonaMoralEntidad addFaltasGravesPersonasFisicas)
        {      

            var json = JsonSerializer.Serialize(addFaltasGravesPersonasFisicas, new JsonSerializerOptions
            {
                WriteIndented = true // Opcional: para que sea más legible
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");          
            var response = await httpClient.PostAsJsonAsync($"FGPMD/AddAsync", addFaltasGravesPersonasFisicas);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorContent);
            }

            var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroFaltasGravesPersonasMorales>();

            if (result?.Mensaje?.Contains("REGISTRO ELIMINADO CORRECTAMENTE") == true)
            {
                result.Response = true;
            }

            return result;
        }

        public async Task<List<PersonaMoralEntidad>> ObtenerFaltasGravesPersonasMorales()
        {
            try
            {
                var response = await httpClient.PostAsync($"FGPMD/Get", null);

                if (!response.IsSuccessStatusCode)
                    return null;

                var wrapper = await response.Content.ReadFromJsonAsync<List<PersonaMoralEntidad>>();

                // var faltas = wrapper?.Data ?? new(); // Aquí tienes tu lista real
                return wrapper.Where(falta => falta.Activo == 1).ToList(); 
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
