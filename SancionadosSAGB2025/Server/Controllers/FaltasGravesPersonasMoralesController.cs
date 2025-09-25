using Microsoft.AspNetCore.Mvc;
using SancionadosSAGB2025.Server.Services;
using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaltasGravesPersonasMoralesController(FaltasGravesPersonasMoralesService faltasGravesPersonasMoralesService) : ControllerBase
    {
        

        [HttpPost("AgregarFaltasGravesPersonasMorales")]
        public async Task<IActionResult> AgregarFaltasGravesPersonasMorales([FromBody] PersonaMoralEntidad addFaltasGravesPersonasFisicas)
        {
            var faltaagregada = await faltasGravesPersonasMoralesService.AgregarFaltasGravesPersonasMorales(addFaltasGravesPersonasFisicas);

            if (faltaagregada == null)
                return Unauthorized(new { message = "Huvo un error al agregar la falta SPG" });

            return Ok(faltaagregada);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerFaltasGravesPersonasMorales(string token)
        {
            var faltasDeServidoresPublicosGs = await faltasGravesPersonasMoralesService.ObtenerFaltasGravesPersonasMorales(token);

            if (faltasDeServidoresPublicosGs == null)
                return Unauthorized(new { message = "Huvo un error al obtener las faltas SPG." });

            return Ok(faltasDeServidoresPublicosGs);
        }
    }
}
