using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Client.Interfaces;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SancionadosSAGB2025.Client.Services
{
	public class FaltasSPGService : IFaltasSPG
	{
		private readonly HttpClient _http;
		private readonly Blazored.LocalStorage.ILocalStorageService _localStorage;
		private readonly NavigationManager _navigation;

		public FaltasSPGService(HttpClient http, Blazored.LocalStorage.ILocalStorageService localStorage, NavigationManager navigation)
		{
			_http = http;
			_localStorage = localStorage;
			_navigation = navigation;
		}

		public async Task<RespondeUpdateFaltas> ActualizarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{
			try
			{
				var response = await _http.PostAsJsonAsync("api/FaltasServidoresPublicosG/ActualizarFaltasSPG", faltasDeServidoresPublicosG);

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<RespondeUpdateFaltas>();

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
	}
}
