using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Client.Interfaces;
using SancionadosSAGB2025.Shared.Grave;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SancionadosSAGB2025.Client.Services
{
	public class FaltasSPGService 
	{
		private readonly HttpClient _http;

		public FaltasSPGService(HttpClient http)
		{
			_http = http;
		}

		public async Task<RespuestaRegistro> ActualizarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{
			try
			{
				var response = await _http.PostAsJsonAsync("api/FaltasServidoresPublicosG/ActualizarFaltasSPG", faltasDeServidoresPublicosG);

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<RespuestaRegistro>();

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
			
		}

		public async Task<RespuestaRegistro> AgregarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{
			var response = await _http.PostAsJsonAsync("api/FaltasServidoresPublicosG/AgregarFaltasSPG", faltasDeServidoresPublicosG);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespuestaRegistro>();
			
			return result;
		}

		public async Task<List<FaltasGravesEntidad>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			var response = await _http.PostAsJsonAsync("api/FaltasServidoresPublicosG/ObtenerFaltasSPG",searchFaltasDeServidoresPublicosG);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<List<FaltasGravesEntidad>>();

			return result;
		}
	}
}
