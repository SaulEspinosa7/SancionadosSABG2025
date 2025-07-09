using Intersoft.Crosslight.Mobile;
using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Shared.Login;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace SancionadosSAGB2025.Client.Services
{
	public class AuthService
	{
		private readonly HttpClient _http;
		private readonly Blazored.LocalStorage.ILocalStorageService _localStorage;
		private readonly NavigationManager _navigation;


		public AuthService(HttpClient http, Blazored.LocalStorage.ILocalStorageService localStorage, NavigationManager navigation)
		{
			_http = http;
			_localStorage = localStorage;
			_navigation = navigation;
		}

		public async Task<bool> LoginAsync(LoginModel loginModel)
		{
			var response = await _http.PostAsJsonAsync("api/login/Authenticate", loginModel);

			if (!response.IsSuccessStatusCode)
				return false;

			var result = await response.Content.ReadFromJsonAsync<AutenticacionResponse>();

			if (result != null && !string.IsNullOrEmpty(result.Token))
			{
				await _localStorage.SetItemAsync("authToken", result.Token);
				_http.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", result.Token);

				// Notificar autenticación al sistema de Blazor
				//(_authStateProvider as CustomAuthStateProvider)?.NotifyUserAuthentication(result.Token);

				return true;
			}

			return false;
		}
	}
}
