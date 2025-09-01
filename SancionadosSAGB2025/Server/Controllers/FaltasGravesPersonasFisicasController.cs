using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SancionadosSAGB2025.Server.Services;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FaltasGravesPersonasFisicasController : ControllerBase
	{
		private readonly FaltasGravesPersonasFisicasService _faltasGravesPersonasFisicasService;  // Inyección de servicio	

		public FaltasGravesPersonasFisicasController(FaltasGravesPersonasFisicasService faltasGravesPersonasFisicasService)
		{
			_faltasGravesPersonasFisicasService = faltasGravesPersonasFisicasService;
		}

		[HttpPost("AgregarFaltasGravesPersonasFisicas")]
		public async Task<IActionResult> AgregarFaltasGravesPersonasFisicas([FromBody] AddFaltasGravesPersonasFisicas addFaltasGravesPersonasFisicas)
		{
			var faltaagregada = await _faltasGravesPersonasFisicasService.AgregarFaltasGravesPersonasFisicas(addFaltasGravesPersonasFisicas);

			if (faltaagregada == null)
				return Unauthorized(new { message = "Huvo un error al agregar la falta SPG" });

			return Ok(faltaagregada);
		}

		[HttpPost("ObtenerFaltasGravesPersonasFisicas")]
		public async Task<IActionResult> ObtenerFaltasGravesPersonasFisicas([FromBody] SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			var faltasDeServidoresPublicosGs = await _faltasGravesPersonasFisicasService.ObtenerFaltasGravesPersonasFisicas(searchFaltasDeServidoresPublicosG);

			if (faltasDeServidoresPublicosGs == null)
				return Unauthorized(new { message = "Huvo un error al obtener las faltas SPG." });

			return Ok(faltasDeServidoresPublicosGs);
		}
	}
}
