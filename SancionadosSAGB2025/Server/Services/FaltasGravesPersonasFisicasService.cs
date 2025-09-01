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
			addFaltasGravesPersonasFisicas = await ConstruirFaltasGravesPersonasFisicas(addFaltasGravesPersonasFisicas);

			var json = JsonSerializer.Serialize(addFaltasGravesPersonasFisicas, new JsonSerializerOptions
			{
				WriteIndented = true // Opcional: para que sea más legible
			});

			Console.WriteLine(json); // o usa tu logger

			// Luego lo mandas tú mismo
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"FGPF/AddAsync", addFaltasGravesPersonasFisicas);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine(errorContent);
			}

			var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroFaltasGravesPersonasFisicas>();

			if (result?.Mensaje?.Contains("REGISTRO ELIMINADO CORRECTAMENTE") == true)
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
				var response = await _http.GetAsync($"FGPF");

				if (!response.IsSuccessStatusCode)
					return null;

                var wrapper = await response.Content.ReadFromJsonAsync<List<AddFaltasGravesPersonasFisicas>>();

               // var faltas = wrapper?.Data ?? new(); // Aquí tienes tu lista real
				return wrapper.Where(falta => falta.Activo == 1).ToList(); ;
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
