using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SancionadosSAGB2025.Server.Services;
using SancionadosSAGB2025.Shared.Grave;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FaltasServidoresPublicosGController : ControllerBase
	{
		private readonly FaltasServidoresPublicosGService _faltasServidoresPublicosGService;  // Inyección de servicio	

		public FaltasServidoresPublicosGController(FaltasServidoresPublicosGService faltasServidoresPublicosGService)
		{
			_faltasServidoresPublicosGService = faltasServidoresPublicosGService;			
		}

		[HttpPost("AgregarFaltasSPG")]
		public async Task<IActionResult> AgregarFaltasSPG([FromBody] FaltasGravesEntidad faltasDeServidoresPublicosG)
		{
			var faltaagregada = await _faltasServidoresPublicosGService.AgregarFaltasSPG(faltasDeServidoresPublicosG);

			if (faltaagregada == null)
				return Unauthorized(new { message = "Huvo un error al agregar la falta SPG" });

			return Ok(faltaagregada);
		}

		//[HttpPost("AgregarFaltasSPG")]
		//public async Task<IActionResult> AgregarFaltasSPG([FromBody] FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		//{
		//	var faltaagregada = await _faltasServidoresPublicosGService.AgregarFaltasSPG(faltasDeServidoresPublicosG);

		//	if (faltaagregada == null)
		//		return Unauthorized(new { message = "Huvo un error al agregar la falta SPG" });

		//	return Ok(faltaagregada);
		//}

		[HttpPost("ActualizarFaltasSPG")]
		public async Task<IActionResult> ActualizarFaltasSPG([FromBody] FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{
			var faltaActualizada = await _faltasServidoresPublicosGService.ActualizarFaltasSPG(faltasDeServidoresPublicosG);

			if (faltaActualizada == null)
				return Unauthorized(new { message = "Huvo un error al actualizar la falta SPG." });

			return Ok(faltaActualizada);
		}

		[HttpPost("ObtenerFaltasSPG")]
		public async Task<IActionResult> ObtenerFaltasSPG([FromBody] SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			var faltasDeServidoresPublicosGs = await _faltasServidoresPublicosGService.ObtenerFaltasSPG(searchFaltasDeServidoresPublicosG);

			if (faltasDeServidoresPublicosGs == null)
				return Unauthorized(new { message = "Huvo un error al obtener las faltas SPG." });

			return Ok(faltasDeServidoresPublicosGs);
		}
		//[HttpPost("ObtenerFaltasSPG")]
		//public async Task<IActionResult> ObtenerFaltasSPG([FromBody] SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		//{
		//	var faltasDeServidoresPublicosGs = await _faltasServidoresPublicosGService.ObtenerFaltasSPG(searchFaltasDeServidoresPublicosG);

		//	if (faltasDeServidoresPublicosGs == null)
		//		return Unauthorized(new { message = "Huvo un error al obtener las faltas SPG." });

		//	return Ok(faltasDeServidoresPublicosGs);
		//}
	}
}
