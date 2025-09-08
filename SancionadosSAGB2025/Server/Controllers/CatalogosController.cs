using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SancionadosSAGB2025.Server.Services;
using SancionadosSAGB2025.Shared;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CatalogosController : ControllerBase
	{
		private readonly CatalogosService _catalogosService;  // Inyección de servicio	

		public CatalogosController(CatalogosService catalogosService)
		{
			_catalogosService = catalogosService;
		}

		[HttpGet("ObtenerTodosLosCatalogos")]
		public async Task<IActionResult> ObtenerTodosLosCatalogos()
		{
			var catalogos = await _catalogosService.ObtenerTodosLosCatalogos();

			if (catalogos == null)
				return Unauthorized(new { message = "Error al obtener los catalogos." });

			return Ok(catalogos);
		}

		[HttpGet("ObtenerEntidadFederativa")]
		public async Task<IActionResult> ObtenerEntidadFederativa()
		{
			var entidades = await _catalogosService.ObtenerEntidadFederativa();

			if (entidades == null)
				return Unauthorized(new { message = "Error al obtener entidades." });

			return Ok(entidades);
		}

		[HttpGet("ObtenerAmbitoPublico")]
		public async Task<IActionResult> ObtenerAmbitoPublico()
		{
			var ambitoPublicos = await _catalogosService.ObtenerAmbitoPublico();

			if (ambitoPublicos == null)
				return Unauthorized(new { message = "Error al obtener  ambito Publicos." });

			return Ok(ambitoPublicos);
		}

		[HttpGet("ObtenerFaltaCometida")]
		public async Task<IActionResult> ObtenerFaltaCometida()
		{
			var faltaCometidas = await _catalogosService.ObtenerFaltaCometida();

			if (faltaCometidas == null)
				return Unauthorized(new { message = "Error al obtener falta cometidas." });

			return Ok(faltaCometidas);
		}

		[HttpGet("ObtenerNivelJerarquico")]
		public async Task<IActionResult> ObtenerNivelJerarquico()
		{
			var nivelJerarquicos = await _catalogosService.ObtenerNivelJerarquico();

			if (nivelJerarquicos == null)
				return Unauthorized(new { message = "Error al obtener nivel Jerarquicos." });

			return Ok(nivelJerarquicos);
		}

		[HttpGet("ObtenerNivelOrdenGobierno")]
		public async Task<IActionResult> ObtenerNivelOrdenGobierno()
		{
			var nivelOrdenGobiernos = await _catalogosService.ObtenerNivelOrdenGobierno();

			if (nivelOrdenGobiernos == null)
				return Unauthorized(new { message = "Error al obtener nivel Orden Gobiernos." });

			return Ok(nivelOrdenGobiernos);
		}

		[HttpGet("ObtenerOrdenJurisdiccional")]
		public async Task<IActionResult> ObtenerOrdenJurisdiccional()
		{
			var ordenJurisdiccionals = await _catalogosService.ObtenerOrdenJurisdiccional();

			if (ordenJurisdiccionals == null)
				return Unauthorized(new { message = "Error al obtener orden Jurisdiccionals." });

			return Ok(ordenJurisdiccionals);
		}

		[HttpGet("ObtenerOrigenProcedimiento")]
		public async Task<IActionResult> ObtenerOrigenProcedimiento()
		{
			var origenProcedimientos = await _catalogosService.ObtenerOrigenProcedimiento();

			if (origenProcedimientos == null)
				return Unauthorized(new { message = "Error al obtener origen Procedimientos." });

			return Ok(origenProcedimientos);
		}

		[HttpGet("ObtenerSexo")]
		public async Task<IActionResult> ObtenerSexo()
		{
			var sexos = await _catalogosService.ObtenerSexo();

			if (sexos == null)
				return Unauthorized(new { message = "Error al obtener sexos." });

			return Ok(sexos);
		}

		[HttpGet("ObtenerTipoAmonestacion")]
		public async Task<IActionResult> ObtenerTipoAmonestacion()
		{
			var tipoAmonestacions = await _catalogosService.ObtenerTipoAmonestacion();

			if (tipoAmonestacions == null)
				return Unauthorized(new { message = "Error al obtener tipo Amonestacions." });

			return Ok(tipoAmonestacions);
		}

		[HttpGet("ObtenerTipoSancion")]
		public async Task<IActionResult> ObtenerTipoSancion()
		{
			var tipoSancions = await _catalogosService.ObtenerTipoSancion();

			if (tipoSancions == null)
				return Unauthorized(new { message = "Error al obtener tipo Sancions." });

			return Ok(tipoSancions);
		}

		[HttpGet("ObtenerTipoVialidad")]
		public async Task<IActionResult> ObtenerTipoVialidad()
		{
			var tipoVialidads = await _catalogosService.ObtenerTipoVialidad();

			if (tipoVialidads == null)
				return Unauthorized(new { message = "Error al obtener tipo Vialidads." });

			return Ok(tipoVialidads);
		}
		[HttpPost("ActulizarAmbito")]
		public async Task<RespuestaApiActualizar> ActulizarAmbito(AmbitoPublico ambitoPublico)
		{
			var ambitoActualizado = await _catalogosService.ActualizarAmbitoPublico(ambitoPublico);
			if (ambitoActualizado == null)
				return new();
			return ambitoActualizado;
        }

        [HttpPost("ActulizarEntidadFederativa")]
        public async Task<RespuestaApiActualizar> ActulizarEntidadFederativa(EntidadFederativaEntidad entidadFederativaEntidad)
        {
            var ambitoActualizado = await _catalogosService.ActualizarEntidadFederativa(entidadFederativaEntidad);
            if (ambitoActualizado == null)
                return new();
            return ambitoActualizado;
        }
        [HttpPost("ActulizarFaltaCometida")]
        public async Task<RespuestaApiActualizar> ActulizarFaltaCometida(FaltaCometidaEntidad faltaCometidaEntidad)
        {
            var ambitoActualizado = await _catalogosService.ActualizarFaltaCometida(faltaCometidaEntidad);
            if (ambitoActualizado == null)
                return new();
            return ambitoActualizado;
        }
        [HttpPost("ActualizarNivelJerarquico")]
        public async Task<RespuestaApiActualizar> ActualizarNivelJerarquico(NivelJerarquicoEntidad nivelJerarquico)
        {
            var ambitoActualizado = await _catalogosService.ActualizarNivelJerarquico(nivelJerarquico);
            if (ambitoActualizado == null)
                return new();
            return ambitoActualizado;
        }
        [HttpPost("ActualizarOrdenGobierno")]
        public async Task<RespuestaApiActualizar> ActualizarOrdenGobierno(NivelOrdenGobierno nivelOrdenGobierno)
        {
            var ambitoActualizado = await _catalogosService.ActualizarOrdenGobierno(nivelOrdenGobierno);
            if (ambitoActualizado == null)
                return new();
            return ambitoActualizado;
        }
        [HttpPost("ActualizaOrdenJurisdiccional")]
        public async Task<RespuestaApiActualizar> ActualizaOrdenJurisdiccional(OrdenJurisdiccional ordenJurisdiccional)
        {
            var ambitoActualizado = await _catalogosService.ActualizaOrdenJurisdiccional(ordenJurisdiccional);
            if (ambitoActualizado == null)
                return new();
            return ambitoActualizado;
        }
    }
}
