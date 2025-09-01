using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SancionadosSAGB2025.Server.Services;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Modulos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

		[HttpPost("consultarInformacionPerfil")]
		public async Task<IActionResult> ConsultarInformacionPerfil([FromBody] TokenResponse token)
		{
			var usuario = await _loginService.ConsultarInformacionDePerfil(token);

			if (usuario == null)
				return Unauthorized(new { message = "Hubo un error al consultar la informacion de Perfil" });

			return Ok(new { usuario });
		}
	}
}
