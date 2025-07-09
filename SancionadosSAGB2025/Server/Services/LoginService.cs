using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Login;
using System.Net.Http.Headers;
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
		public async Task<AutenticacionResponse> LoginAsync(LoginModel loginModel)
		{
			var response = await _http.PostAsJsonAsync($"{_url}Usuarios/logon", loginModel);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<AutenticacionResponse>();			

			return result;
		}
	}
}
