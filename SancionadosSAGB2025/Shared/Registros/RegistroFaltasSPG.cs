using SancionadosSAGB2025.Shared.Grave;
using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Sanciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Registros
{
	public class RegistroFaltasSPG
	{
		public DateTime? Fecha { get; set; }
		public string? Expediente { get; set; }	
		public int? IdDatosGeneralesFK { get; set; }
		public int? IdEntidadFederativaFK { get; set; }
		public int? IdNivelOrdenGobiernoFK { get; set; }
		public int? IdAmbitoPublicoFK { get; set; }
		public string? NombreEntePublico { get; set; }
		public string? SiglasEntePublico { get; set; }
		public int? IdNivelJerarquicoCatFK { get; set; }
		public string? ValorNJ { get; set; }
		public string? DenominacionNJ { get; set; }
		public string? AreaAdscripcionNJ { get; set; }
		public int? IdOrigenProcedimientoFK { get; set; }
		public string? ValorOP { get; set; }
		public int? IdFaltaCometidaCatFK { get; set; }
		public string? ValorFC { get; set; }
		public string? NombreNormatividadFC { get; set; }
		public string? ArticuloFC { get; set; }
		public string? FraccionFC { get; set; }
		public string? DescripcionHechosFC { get; set; }
		public string? TituloDocumento { get; set; }
		public DateTime? FechaResolucion { get; set; }
		public DateTime? FechaNotificacion { get; set; }
		public string? UrlResolucion { get; set; }
		public DateTime? FechaResolucionFirme { get; set; }
		public DateTime? FechaNotificacionFirme { get; set; }
		public string? UrlResolucionFirme { get; set; }
		public DateTime? FechaEjecucion { get; set; }
		public int? IdOrdenJurisdiccionalFK { get; set; }
		public string? AutoridadResolutora { get; set; }
		public string? AutoridadInvestigadora { get; set; }
		public string? AutoridadSubstanciadora { get; set; }
		public int? IdTipoSancionCatFK { get; set; }
		public int? PlazoMesesTS { get; set; }
		public int? PlazoDiasTS { get; set; }
		public DateTime? FechaInicialTS { get; set; }
		public DateTime? FechaFinalTS { get; set; }
		public DateTime? FechaDestitucionDE { get; set; }
		public decimal? MontoSE { get; set; }
		public string? MonedaSE { get; set; }
		public int? AniosSE { get; set; }
		public int? MesesSE { get; set; }
		public int? DiasSE { get; set; }		
		public decimal? MontoSEC { get; set; }
		public string? MonedaSEC { get; set; }
		public DateTime? FechaCobroSEC { get; set; }
		public DateTime? FechaPagoTotalSEC { get; set; }
		public DateTime? PlazoAnios { get; set; }
		public DateTime? PlazoMeses { get; set; }
		public DateTime? PlazoDias { get; set; }
		public DateTime? FechaInicial { get; set; }
		public DateTime? FechaFinal { get; set; }
		public string? DenominacionSancion { get; set; }
		public string? Observaciones { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int Activo { get; set; }
	}

	public class UpdateFaltasSPG
	{
		public int? idFaltasServidoresPG { get; set; }
		public DateTime? Fecha { get; set; }
		public string? Expediente { get; set; }
		public int? IdDatosGeneralesFK { get; set; }
		public int? IdEntidadFederativaFK { get; set; }
		public int? IdNivelOrdenGobiernoFK { get; set; }
		public int? IdAmbitoPublicoFK { get; set; }
		public string? NombreEntePublico { get; set; }
		public string? SiglasEntePublico { get; set; }
		public int? IdNivelJerarquicoCatFK { get; set; }
		public string? ValorNJ { get; set; }
		public string? DenominacionNJ { get; set; }
		public string? AreaAdscripcionNJ { get; set; }
		public int? IdOrigenProcedimientoFK { get; set; }
		public string? ValorOP { get; set; }
		public int? IdFaltaCometidaCatFK { get; set; }
		public string? ValorFC { get; set; }
		public string? NombreNormatividadFC { get; set; }
		public string? ArticuloFC { get; set; }
		public string? FraccionFC { get; set; }
		public string? DescripcionHechosFC { get; set; }
		public string? TituloDocumento { get; set; }
		public DateTime? FechaResolucion { get; set; }
		public DateTime? FechaNotificacion { get; set; }
		public string? UrlResolucion { get; set; }
		public DateTime? FechaResolucionFirme { get; set; }
		public DateTime? FechaNotificacionFirme { get; set; }
		public string? UrlResolucionFirme { get; set; }
		public DateTime? FechaEjecucion { get; set; }
		public int? IdOrdenJurisdiccionalFK { get; set; }
		public string? AutoridadResolutora { get; set; }
		public string? AutoridadInvestigadora { get; set; }
		public string? AutoridadSubstanciadora { get; set; }
		public int? IdTipoSancionCatFK { get; set; }
		public int? PlazoMesesTS { get; set; }
		public int? PlazoDiasTS { get; set; }
		public DateTime? FechaInicialTS { get; set; }
		public DateTime? FechaFinalTS { get; set; }
		public DateTime? FechaDestitucionDE { get; set; }
		public decimal? MontoSE { get; set; }
		public string? MonedaSE { get; set; }
		public int? AniosSE { get; set; }
		public int? MesesSE { get; set; }
		public int? DiasSE { get; set; }
		public decimal? MontoSEC { get; set; }
		public string? MonedaSEC { get; set; }
		public DateTime? FechaCobroSEC { get; set; }
		public DateTime? FechaPagoTotalSEC { get; set; }
		public DateTime? PlazoAnios { get; set; }
		public DateTime? PlazoMeses { get; set; }
		public DateTime? PlazoDias { get; set; }
		public DateTime? FechaInicial { get; set; }
		public DateTime? FechaFinal { get; set; }
		public string? DenominacionSancion { get; set; }
		public string? Observaciones { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int Activo { get; set; }
	}

	public class DTORegistroFaltasSPG 
	{
		public int? idFaltasServidoresPG { get; set; }
		public RegistroFaltasSPG? Sancion { get; set; }
	}

	public class RespuestaRegistro
	{
		public bool? Response { get; set; }
		public string? Mensaje { get; set; }
	//	public FaltasDeServidoresPublicosG? Data { get; set; }
		public FaltasGravesEntidad? Data { get; set; } = new();
    }

	public class RespuestaRegistroNoGraves
	{
		public bool? Response { get; set; }
		public string? Mensaje { get; set; }
		public AddFaltasDeServidoresPublicosNoGraves? Data { get; set; }
	}

	public class RespuestaRegistroFaltasGravesPersonasFisicas
	{
		public bool? Response { get; set; }
		public string? Mensaje { get; set; }
		public AddFaltasGravesPersonasFisicas? Data { get; set; }
	}

    public class RespuestaRegistroFaltasGravesPersonasMorales
    {
        public bool? Response { get; set; }
        public string? Mensaje { get; set; }
        public PersonaMoralEntidad? Data { get; set; }
    }

    public class DTODatosGenerales
	{
		[JsonPropertyName("idDatosGenerales")]
		public int? IdDatosGenerales { set; get; }

		public DatosGenerales? Data { get; set; }

		[JsonPropertyName("fechaCreacion")]
		public DateTime FechaCreacion { get; set; }

		[JsonPropertyName("fechaModificacion")]
		public DateTime FechaModificacion { get; set; }

		[JsonPropertyName("idDomicilioMexicoFk")]
		public int? IdDomicilioMexicoFk { get; set; }

		[JsonPropertyName("domicilioMexico")]
		public object? DomicilioMexico { get; set; }

		[JsonPropertyName("idDomicilioExtranjeroFk")]
		public int? IdDomicilioExtranjeroFk { get; set; }

		[JsonPropertyName("objetoSocial")]
		public object? ObjetoSocial { get; set; }
	}

	public class RegistroDatosGenerales 
	{
		[JsonPropertyName("nombres")]
		public string? Nombres { get; set; }

		[JsonPropertyName("primerApellido")]
		public string? PrimerApellido { get; set; }

		[JsonPropertyName("segundoApellido")]
		public string? SegundoApellido { get; set; }

		[JsonPropertyName("curp")]
		public string? Curp { get; set; }

		[JsonPropertyName("rfc")]
		public string? Rfc { get; set; }

		[JsonPropertyName("idSexoFK")]
		public int? IdSexoFK { set; get; }
	}
	
	public class RespondeDatosGenerales
	{
		public string? Mensaje { get; set; }
		public DTODatosGenerales? Data { get; set; }
	}

}
