using Blazored.LocalStorage;
using Intersoft.Crosslight.Mobile;
using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Shared.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

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

		public async Task LogoutAsync()
		{
			await _localStorage.RemoveItemAsync("authToken");
			_http.DefaultRequestHeaders.Authorization = null;
			_navigation.NavigateTo("/login", forceLoad: true);
		}

		public async Task<string> GetTokenAsync()
		{
			return await _localStorage.GetItemAsync<string>("authToken");
		}

		public async Task<AutenticacionResponse> ConsultarInformacionPerfil(TokenResponse token)
		{
			//var response = await _http.PostAsJsonAsync("api/Login/ConsultarInformacionPerfil", token);

			//if (!response.IsSuccessStatusCode)
			//	return null;

			//var result = await response.Content.ReadFromJsonAsync<AutenticacionResponse>();
			//return result;

			AutenticacionResponse autenticacionResponse = new();
			string userName = string.Empty;  // Debes usar un string aquí, no un int.
			string tokenConsulta = token.Token;

			if (!string.IsNullOrEmpty(tokenConsulta))
			{
				autenticacionResponse.Usuario = new();
				var handler = new JwtSecurityTokenHandler();
				var jsonToken = handler.ReadToken(tokenConsulta) as JwtSecurityToken;

				// Extraemos el claim y lo asignamos a userName
				var claims = jsonToken?.Claims;

				var idClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				var nameClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
				var emailClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

				userName = idClaim;
				autenticacionResponse.Usuario.Id = Convert.ToInt32(userName);
				autenticacionResponse.Usuario.Nombre = nameClaim ?? string.Empty;
				autenticacionResponse.Usuario.User = emailClaim ?? string.Empty;


				// Si userName es null o vacío, puedes manejar el caso de error aquí.
				if (string.IsNullOrEmpty(userName))
				{
					return null;
				}
			}
			return autenticacionResponse;
		}

	}
}
