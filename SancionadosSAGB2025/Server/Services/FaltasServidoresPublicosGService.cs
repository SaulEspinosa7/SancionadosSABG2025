using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Services
{
	public class FaltasServidoresPublicosGService : IFaltasServidoresPublicosG
	{
		private readonly HttpClient _http;
		private readonly RegistroFaltasSPG registroFaltasSPG;
		private readonly string _url;

		private readonly DatosGeneralesService _datosGenerales;

		public FaltasServidoresPublicosGService(HttpClient http, IConfiguration config, DatosGeneralesService datosGenerales)
		{
			_http = http;
			_url = config["UrlApi:test"]!;
			_datosGenerales = datosGenerales;
		}

		public async Task<RespondeUpdateFaltas> ActualizarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{
			try
			{
				var UpdateFalta = await FromUpdateFaltasDeServidoresPublicosG(faltasDeServidoresPublicosG);
				//var registroFalta = new RegistroFaltasSPG();
				var response = await _http.PostAsJsonAsync($"FaltasServidoresPG/UpdateAsync", UpdateFalta);

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<RespondeUpdateFaltas>();
				result.Sancion = UpdateFalta;
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ha ocurrio un error al momento de realizar la actualización del Expediente {faltasDeServidoresPublicosG.Sancion!.Expediente} - {ex.Message}");
				return null;
			}

		}

		public async Task<int?> AgregarDatosGenerales(DatosGenerales datosGenerales)
		{
			RespondeDatosGenerales? responde = await _datosGenerales.AgregarDatoaGenerales(datosGenerales);

			return responde?.Data?.IdDatosGenerales;
		}

		public async Task<RespuestaRegistro> AgregarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{
			var registroFalta = await FromFaltasDeServidoresPublicosG(faltasDeServidoresPublicosG);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"FaltasServidoresPG/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespuestaRegistro>();
			return result;
		}

		public async Task<RegistroFaltasSPG> FromFaltasDeServidoresPublicosG(FaltasDeServidoresPublicosG faltas)
		{
			RegistroFaltasSPG registroAdd = new();

			registroAdd.Fecha = faltas?.Sancion?.Fecha;
			registroAdd.Expediente = faltas?.Sancion?.Expediente;

			// Fechas del sistema
			registroAdd.FechaCreacion = DateTime.Now;
			registroAdd.FechaModificacion = DateTime.Now;
			registroAdd.Activo = 1;

			return registroAdd;
		}

		public async Task<UpdateFaltasSPG> FromUpdateFaltasDeServidoresPublicosG(FaltasDeServidoresPublicosG faltas)
		{
			UpdateFaltasSPG registroAdd = new();

			registroAdd.idFaltasServidoresPG = faltas.idFaltasServidoresPG;

			registroAdd.Fecha = faltas?.Sancion?.Fecha;
			registroAdd.Expediente = faltas?.Sancion?.Expediente;

			if (faltas!.IdDatosGeneralesFK is null) registroAdd.IdDatosGeneralesFK = await AgregarDatosGenerales(faltas!.DatosGenerales!);
			else { 
				registroAdd.IdDatosGeneralesFK = faltas.IdDatosGeneralesFK; 
			}

			registroAdd.IdEntidadFederativaFK = faltas.IdEntidadFederativaFK;
			registroAdd.IdNivelOrdenGobiernoFK = faltas.IdNivelOrdenGobiernoFK;
			registroAdd.IdAmbitoPublicoFK = faltas.IdAmbitoPublicoFK;

			registroAdd.IdNivelJerarquicoCatFK = faltas.IdNivelJerarquicoCatFK;
			registroAdd.IdFaltaCometidaCatFK = faltas.IdFaltaCometidaCatFK;
			registroAdd.IdOrdenJurisdiccionalFK = faltas.IdOrdenJurisdiccionalFK;
			registroAdd.IdOrigenProcedimientoFK = faltas.IdOrigenProcedimientoFK;
			registroAdd.IdTipoSancionCatFK = faltas.IdTipoSancionCatFK;

			// Empleo; Ente
			//registroAdd.NombreEntePublico = faltas?.EmpleoCargoComision?.NombreEnte;
			registroAdd.NombreEntePublico = faltas?.EmpleoCargoComision?.NombreEnte;
			registroAdd.SiglasEntePublico = faltas?.EmpleoCargoComision?.SiglasEntePublico;

			// Nivel jerárquico
			registroAdd.ValorNJ = faltas?.NivelJerarquico?.Valor;
			registroAdd.DenominacionNJ = faltas?.NivelJerarquico?.Denominacion;
			registroAdd.AreaAdscripcionNJ = faltas?.NivelJerarquico?.AreaAdscripcion;

			// Origen del procedimiento
			registroAdd.ValorOP = faltas?.OrigenProcedimiento?.Valor;

			// Falta cometida
			registroAdd.ValorFC = faltas?.FaltaCometida?.Valor;
			registroAdd.NombreNormatividadFC = faltas?.FaltaCometida?.NombreNormatividad;
			registroAdd.ArticuloFC = faltas?.FaltaCometida?.Articulo;
			registroAdd.FraccionFC = faltas?.FaltaCometida?.FraccionNormatividad;
			registroAdd.DescripcionHechosFC = faltas?.FaltaCometida?.DescripcionHechos;

			// Resolución
			registroAdd.TituloDocumento = faltas?.Resolucion?.TituloDocumento;
			registroAdd.FechaResolucion = faltas?.Resolucion?.FechaResolucion;
			registroAdd.FechaNotificacion = faltas?.Resolucion?.FechaNotificacion;
			registroAdd.UrlResolucion = faltas?.Resolucion?.UrlResolucion;
			registroAdd.FechaResolucionFirme = faltas?.Resolucion?.FechaResolucionFirme;
			registroAdd.FechaNotificacionFirme = faltas?.Resolucion?.FechaNotificacionFirme;
			registroAdd.UrlResolucionFirme = faltas?.Resolucion?.UrlResolucionFirme;
			registroAdd.FechaEjecucion = faltas?.Resolucion?.FechaEjecucion;
			registroAdd.AutoridadResolutora = faltas?.Resolucion?.AutoridadResolutora;
			registroAdd.AutoridadInvestigadora = faltas?.Resolucion?.AutoridadInvestigadora;
			registroAdd.AutoridadSubstanciadora = faltas?.Resolucion?.AutoridadSubstanciadora;

			// Tipo sanción
			// IdTipoSancionCatFK = ObtenerIdPorClave(faltas?.TipoSancion?.Clave);

			// Suspensión
			registroAdd.FechaInicialTS = faltas?.Suspension?.FechaInicial;
			registroAdd.FechaFinalTS = faltas?.Suspension?.FechaFinal;
			registroAdd.PlazoMesesTS = faltas?.Suspension?.PlazoMeses;
			registroAdd.PlazoDiasTS = faltas?.Suspension?.PlazoDias;

			// Destitución
			registroAdd.FechaDestitucionDE = faltas?.DestitucionEmpleo?.FechaDestitucion;

			// Sanción económica
			registroAdd.MontoSE = faltas?.SancionEconomica?.Monto;
			registroAdd.MonedaSE = faltas?.SancionEconomica?.Moneda?.Valor;
			registroAdd.AniosSE = faltas?.SancionEconomica?.Anio;
			registroAdd.MesesSE = faltas?.SancionEconomica?.Mes;
			registroAdd.DiasSE = faltas?.SancionEconomica?.Dia;

			// Efectivamente cobrada
			registroAdd.MontoSEC = faltas?.SancionEfectivamenteCobrada?.Monto;
			registroAdd.MonedaSEC = faltas?.SancionEfectivamenteCobrada?.Moneda?.Valor;
			registroAdd.FechaCobroSEC = faltas?.SancionEfectivamenteCobrada?.FechaCobro;
			registroAdd.FechaPagoTotalSEC = faltas?.SancionEfectivamenteCobrada?.FechaPagoTotal;

			// Inhabilitación
			registroAdd.FechaInicial = faltas?.Inhabilitacion?.FechaInicial;
			registroAdd.FechaFinal = faltas?.Inhabilitacion?.FechaFinal;

			// Otro
			registroAdd.DenominacionSancion = faltas?.Otro?.DenominacionSancion;
			registroAdd.Observaciones = faltas?.Otro?.Observaciones;

			// Fechas del sistema
			registroAdd.FechaCreacion = DateTime.Now;
			registroAdd.FechaModificacion = DateTime.Now;
			registroAdd.Activo = (int)faltas.Activo!;

			return registroAdd;

		}
	}
}
