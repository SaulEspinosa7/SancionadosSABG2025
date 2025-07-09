using SancionadosSAGB2025.Shared.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Sanciones
{
	public class Sancion
	{
		[JsonPropertyName("fecha")]
		public DateTime? Fecha { get; set; }

		[JsonPropertyName("expediente")]
		public string? Expediente { get; set; }
	}

	public class DatosGenerales
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

		[JsonPropertyName("idSexo")]
		public int? IdSexo { set; get; }

		[JsonPropertyName("sexo")]
		public Sexo? Sexo { get; set; }
	}

	public class Sexo
	{
		[JsonPropertyName("valor")]
		public string? Valor { get; set; }

		[JsonPropertyName("clave")]
		public string? Clave { get; set; }
	}

	//public class EntidadFederativa
	//{
	//	[JsonPropertyName("valor")]
	//	public string? Valor { get; set; }

	//	[JsonPropertyName("clave")]
	//	public string? Clave { get; set; }
	//}

	//public class NivelOrdenGobierno
	//{
	//	[JsonPropertyName("clave")]
	//	public string? Clave { get; set; }

	//	[JsonPropertyName("valor")]
	//	public string? Valor { get; set; }
	//}

	public class EmpleoCargoComision
	{
		[JsonPropertyName("entidadFederativa")]
		public EntidadFederativa? EntidadFederativa { get; set; }

		[JsonPropertyName("nivelOrdenGobierno")]
		public NivelOrdenGobierno? NivelOrdenGobierno { get; set; }

		[JsonPropertyName("ambitoPublico")]
		public AmbitoPublico? AmbitoPublico { get; set; }

		[JsonPropertyName("nombreEntePublico")]
		public string? NombreEnte { get; set; }

		[JsonPropertyName("siglasEntePublico")]
		public string? SiglasEntePublico { get; set; }	
	}

	public class NivelGobierno
	{
		[JsonPropertyName("clave")]
		public string? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }

	}

	//public class AmbitoPublico
	//{
	//	[JsonPropertyName("clave")]
	//	public string? Clave { get; set; }

	//	[JsonPropertyName("valor")]
	//	public string? Valor { get; set; }

	//}

	public class NivelJerarquico
	{
		[JsonPropertyName("clave")]
		public SancionadosSAGB2025.Shared.Catalogos.NivelJerarquico? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }

		[JsonPropertyName("denominacion")]
		public string? Denominacion { get; set; }

		[JsonPropertyName("areaAdscripcion")]
		public string? AreaAdscripcion { get; set; }	
	}

	public class OrigenProcedimiento
	{
		[JsonPropertyName("clave")]
		public Origenes? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }
	}

	public class Origenes
	{
		[JsonPropertyName("clave")]
		public string? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }
	}

	public class CargoComision
	{
		[JsonPropertyName("clave")]
		public string? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }
	}

	public class FaltaCometida
	{
		[JsonPropertyName("clave")]
		public SancionadosSAGB2025.Shared.Catalogos.FaltaCometida? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }

		[JsonPropertyName("normatividadInfringida")]
		public string? NormatividadInfringida { get; set; }

		[JsonPropertyName("nombreNormatividad")]
		public string? NombreNormatividad { get; set; }

		[JsonPropertyName("articulo")]
		public string? Articulo { get; set; }

		[JsonPropertyName("fraccion")]
		public string? FraccionNormatividad { get; set; }

		[JsonPropertyName("descripcionHechos")]
		public string? DescripcionHechos { get; set; }

	}

	public class FaltaCometidaClaveCatalogo 
	{
		[JsonPropertyName("clave")]
		public string? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }
	}

	public class Resolucion
	{
		[JsonPropertyName("tituloDocumento")]
		public string? TituloDocumento { get; set; }

		[JsonPropertyName("fechaResolucion")]
		public DateTime? FechaResolucion { get; set; }

		[JsonPropertyName("fechaNotificacion")]
		public DateTime? FechaNotificacion { get; set; }

		[JsonPropertyName("urlResolucion")]
		public string? UrlResolucion { get; set; }

		[JsonPropertyName("fechaResolucionFirme")]
		public DateTime? FechaResolucionFirme { get; set; }

		[JsonPropertyName("fechaNotificacionFirme")]
		public DateTime? FechaNotificacionFirme { get; set; }

		[JsonPropertyName("urlResolucionFirme")]
		public string? UrlResolucionFirme { get; set; }

		[JsonPropertyName("fechaEjecucion")]
		public DateTime? FechaEjecucion { get; set; }

		[JsonPropertyName("ordenJurisdiccional")]
		public OrdenJurisdiccional? OrdenJurisdiccional { get; set; }	

		[JsonPropertyName("autoridadResolutora")]
		public string? AutoridadResolutora { get; set; }

		[JsonPropertyName("autoridadInvestigadora")]
		public string? AutoridadInvestigadora { get; set; }

		[JsonPropertyName("autoridadSubstanciadora")]
		public string? AutoridadSubstanciadora { get; set; }	
	}

	//public class OrdenJurisdiccional
	//{
	//	[JsonPropertyName("clave")]
	//	public string? Clave { get; set; }

	//	[JsonPropertyName("valor")]
	//	public string? Valor { get; set; }
	//}

	public class AbstencionesNoGravesLGRA
	{
		public Sancion? Sancion { get; set; }
		public DatosGenerales? DatosGenerales { get; set; }
		public EmpleoCargoComision? EmpleoCargoComision { get; set; }
		public OrigenProcedimiento? OrigenProcedimiento { get; set; }
		public FaltaCometida? FaltaCometida { get; set; }
		public Resolucion? Resolucion { get; set; }
	}

	public class TipoSancion
	{
		[JsonPropertyName("clave")]
		public TipoSancionClave? Clave { get; set; }
	}

	public class TipoSancionClave
	{
		[JsonPropertyName("clave")]
		public string? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }
	}

	public class Suspension
	{
		[JsonPropertyName("fechaInicial")]
		public DateTime? FechaInicial { get; set; }

		[JsonPropertyName("fechaFinal")]
		public DateTime? FechaFinal { get; set; }

		[JsonPropertyName("plazoMeses")]
		public int? PlazoMeses { get; set; }

		[JsonPropertyName("plazoDias")]
		public int? PlazoDias { get; set; }
	}

	public class DestitucionEmpleo
	{
		[JsonPropertyName("fechaDestitucion")]
		public DateTime? FechaDestitucion { get; set; }
	}

	public class SancionEconomica
	{
		[JsonPropertyName("monto")]
		public decimal? Monto { get; set; }

		[JsonPropertyName("moneda")]
	    public Moneda? Moneda { get; set; }

		[JsonPropertyName("plazoPago")]
		public PlazoPago? PlazoPago { get; set; }

		[JsonPropertyName("anios")]
		public int? Anio { get; set; }

		[JsonPropertyName("meses")]
		public int? Mes { get; set; }

		[JsonPropertyName("dias")]
		public int? Dia { get; set; }
	}

	public class Moneda
	{
		[JsonPropertyName("clave")]
		public string? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }
	}

	public class PlazoPago
	{
		[JsonPropertyName("clave")]
		public string? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }
	}

	public class SancionEfectivamenteCobrada
	{
		[JsonPropertyName("monto")]
		public decimal? Monto { get; set; }

		[JsonPropertyName("moneda")]
		public Moneda? Moneda { get; set; }

		[JsonPropertyName("fechaCobro")]
		public DateTime? FechaCobro { get; set; }

		[JsonPropertyName("fechaPagoTotal")]
		public DateTime? FechaPagoTotal { get; set; }
	}

	public class Inhabilitacion
	{
		[JsonPropertyName("plazoAnios")]
		public int? Anio { get; set; }

		[JsonPropertyName("plazoMeses")]
		public int? Mes { get; set; }

		[JsonPropertyName("plazoDias")]
		public int? Dia { get; set; }

		[JsonPropertyName("fechaInicial")]
		public DateTime? FechaInicial { get; set; }

		[JsonPropertyName("fechaFinal")]
		public DateTime? FechaFinal { get; set; }
	}

	public class Otro
	{
		[JsonPropertyName("denominacionSancion")]
		public string? DenominacionSancion { get; set; }

		[JsonPropertyName("observaciones")]
		public string? Observaciones { get; set; }
	}

	public class FaltasDeServidoresPublicosG
	{
		public int? idFaltasServidoresPG { get; set; }
		public Sancion? Sancion { get; set; } = new();
		public int? IdDatosGeneralesFK { get; set; }
		public DatosGenerales? DatosGenerales { get; set; } = new();
		public int? IdEntidadFederativaFK { get; set; }
		public int? IdNivelOrdenGobiernoFK { get; set; }
		public int? IdAmbitoPublicoFK { get; set; }
		public EmpleoCargoComision? EmpleoCargoComision { get; set; } = new();
		public int? IdNivelJerarquicoCatFK { get; set; }
		public NivelJerarquico? NivelJerarquico { get; set; } = new();
		public int? IdOrigenProcedimientoFK { get; set; }
		public OrigenProcedimiento? OrigenProcedimiento { get; set; } = new();
		public int? IdFaltaCometidaCatFK { get; set; }
		public FaltaCometida? FaltaCometida { get; set; } = new();
		public int? IdOrdenJurisdiccionalFK { get; set; }
		public Resolucion? Resolucion { get; set; } = new();
		public int? IdTipoSancionCatFK { get; set; }
		public TipoSancion? TipoSancion { get; set; } = new();
		public Suspension? Suspension { get; set; } = new();
		public DestitucionEmpleo? DestitucionEmpleo { get; set; } = new();
		public SancionEconomica? SancionEconomica { get; set; } = new();
		public SancionEfectivamenteCobrada? SancionEfectivamenteCobrada { get; set; } = new();
		public Inhabilitacion? Inhabilitacion { get; set; } = new();
		public Otro? Otro { get; set; } = new();
		public int? Activo { get; set; }
	}

	public enum SesionesFaltasDeServidoresPublicosG 
	{
		Sancion = 0,
		DatosGenerales,
		EmpleoCargoComision,
		NivelJerarquico,
		OrigenProcedimiento,
		FaltaCometida,
		Resolucion,
		TipoSancion,
		Suspension,
		DestitucionEmpleo,
		SancionEconomica,
		SancionEfectivamenteCobrada,
		Inhabilitacion,
		Otro
	}
}
