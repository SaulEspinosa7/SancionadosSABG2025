using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SancionadosSAGB2025.Server.Services;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FaltasServidoresPublicosNoGravesController : ControllerBase
	{
		private readonly FaltasServidoresPublicosNoGravesService _faltasServidoresPublicosNoGravesService;  // Inyección de servicio	

		public FaltasServidoresPublicosNoGravesController(FaltasServidoresPublicosNoGravesService faltasServidoresPublicosNoGravesService)
		{
			_faltasServidoresPublicosNoGravesService = faltasServidoresPublicosNoGravesService;
		}

		[HttpPost("AgregarFaltasSPNoGraves")]
		public async Task<IActionResult> AgregarFaltasSPNoGraves([FromBody] AddFaltasDeServidoresPublicosNoGraves faltasDeServidoresPublicosG)
		{
			var faltaagregada = await _faltasServidoresPublicosNoGravesService.AgregarFaltasSPG(faltasDeServidoresPublicosG);

			if (faltaagregada == null)
				return Unauthorized(new { message = "Huvo un error al agregar la falta SPG" });

			return Ok(faltaagregada);
		}	

		[HttpPost("ObtenerFaltasSPNoGraves")]
		public async Task<IActionResult> ObtenerFaltasSPNoGraves([FromBody] SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			var faltasDeServidoresPublicosGs = await _faltasServidoresPublicosNoGravesService.ObtenerFaltasSPG(searchFaltasDeServidoresPublicosG);

			if (faltasDeServidoresPublicosGs == null)
				return Unauthorized(new { message = "Huvo un error al obtener las faltas SPG." });

			return Ok(faltasDeServidoresPublicosGs);
		}
	}
}
