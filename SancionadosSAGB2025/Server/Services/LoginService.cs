using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Modulos;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Server.Services
{
	public class LoginService : ILogin
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public LoginService(HttpClient http, IConfiguration config)
		{
			_http = http;
			_url = config["UrlApi:test"]!;
		}

		public async Task<AutenticacionResponse> ConsultarInformacionDePerfil(TokenResponse AutenticacionResponse)
		{
			AutenticacionResponse autenticacionResponse = new();
			string userName = string.Empty;  // Debes usar un string aquí, no un int.
			string tokenConsulta = AutenticacionResponse.Token;

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

		public async Task<AutenticacionResponse?> LoginAsync(LoginModel loginModel)
		{
			var response = await _http.PostAsJsonAsync($"{_url}Usuarios/logon", loginModel);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<AutenticacionResponse>();			

			return result is null ? null : result;
		}
	}
}
