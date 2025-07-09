using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SancionadosSAGB2025.Server.Services;
using SancionadosSAGB2025.Shared.Login;

namespace SancionadosSAGB2025.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly LoginService _loginService;  // Inyección de servicio

		public LoginController(LoginService loginService)
		{
			_loginService = loginService;
		}

		[HttpPost("Authenticate")]
		public async Task<IActionResult> Authenticate([FromBody] LoginModel loginModel)
		{
			var usuario = await _loginService.LoginAsync(loginModel);

			if (usuario == null)
				return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

			return Ok(new { usuario.Token });
		}
	}
}
