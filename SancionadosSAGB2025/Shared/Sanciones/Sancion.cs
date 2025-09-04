using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Validadores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		[JsonPropertyName("idDatosGenerales")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? IdDatosGenerales { get; set; }

		[JsonPropertyName("nombres")]
		public string? Nombres { get; set; }

		[JsonPropertyName("primerApellido")]
		public string? PrimerApellido { get; set; }

		[JsonPropertyName("segundoApellido")]
		public string? SegundoApellido { get; set; }

		[JsonPropertyName("curp")]
        [CurpValidation(ErrorMessage = "La CURP debe contener 18 caracteres")]
      //  [RegularExpression(@"\b[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]$", ErrorMessage = "CURP no valido")]
        public string? Curp { get; set; }

		[JsonPropertyName("rfc")]
		public string? Rfc { get; set; }

		[JsonPropertyName("idSexoFk")]
		public int? IdSexoFk { set; get; }

		[JsonPropertyName("sexo")]
		[JsonIgnore]
		public Sexo? Sexo { get; set; } = new();

		[JsonPropertyName("idDomicilioMexicoFK")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? IdDomicilioMexicoFK { set; get; }

		public DomicilioMexico? DomicilioMexico { get; set; } = new();

        [JsonPropertyName("idDomicilioExtranjeroFK")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? IdDomicilioExtranjeroFK { set; get; }

		[JsonPropertyName("domicilioExtranjero")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public DomicilioExtranjero? DomicilioExtranjero { get; set; } = new();

        [JsonPropertyName("objetoSocial")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? ObjetoSocial { get; set; }
	}

	public class DomicilioMexico 
	{
		public int? IdDomicilioMexico { set; get; }

		public int? IdTipoVialidadFK { set; get; }
		public TipoVialidad? TipoVialidad { get; set; }

		[JsonPropertyName("nombreVialidad")]
		public string? NombreVialidad { get; set; }

		[JsonPropertyName("numeroExterior")]
        public string? NumeroExterior { get; set; }

		[JsonPropertyName("numeroInterior")]
        public string? NumeroInterior { get; set; }

		[JsonPropertyName("coloniaLocalidad")]
        public string? ColoniaLocalidad { get; set; }

		[JsonPropertyName("municipioAlcaldia")]
        public string? MunicipioAlcaldia { get; set; }

		[JsonPropertyName("codigoPostal")]     
        public string? CodigoPostal { get; set; }

		public int? IdEntidadFederativaFK { set; get; }
		public EntidadFederativa? EntidadFederativa { get; set; } = new();
    }

	public class DomicilioExtranjero
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("idDomicilioExtranjero")]
		public int? IdDomicilioExtranjero { set; get; }	

		[JsonPropertyName("ciudad")]
		public string? Ciudad { get; set; }

		[JsonPropertyName("provincia")]
		public string? Provincia { get; set; }

		[JsonPropertyName("calle")]
		public string? Calle { get; set; }

		[JsonPropertyName("numeroExterior")]
		public string? NumeroExterior { get; set; }

		[JsonPropertyName("numeroInterior")]
		public string? NumeroInterior { get; set; }

		[JsonPropertyName("codigoPostal")]
		public int? CodigoPostal { get; set; }

		[JsonPropertyName("idPaisFK")]
		public int? IdPaisFK { set; get; }

		//[JsonPropertyName("pais")]
		//[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		//public PaisCat? Pais { get; set; }

		[JsonPropertyName("pais")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Pais { get; set; }
	}

    public class EmpleoCargoComision
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("idEntidadFederativaFK")]
		public int? IdEntidadFederativaFK { set; get; }

		[JsonPropertyName("entidadFederativa")]
		[JsonIgnore]
		public EntidadFederativa? EntidadFederativa { get; set; } = new();

        [JsonPropertyName("idNivelOrdenGobiernoFK")]
		public int? IdNivelOrdenGobiernoFK { set; get; }

		[JsonPropertyName("nivelOrdenGobierno")]
		[JsonIgnore]
		public NivelOrdenGobierno? NivelOrdenGobierno { get; set; } = new();

        [JsonPropertyName("idAmbitoPublicoFK")]
		public int? IdAmbitoPublicoFK { set; get; }

		[JsonPropertyName("ambitoPublico")]
		[JsonIgnore]
		public AmbitoPublico? AmbitoPublico { get; set; } = new();

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

	public class NivelJerarquico
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("idNivelJerarquicoFK")]
		public int? IdNivelJerarquicoFK { get; set; }

		[JsonPropertyName("clave")]
		public NivelJerarquicoCat? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }

		[JsonPropertyName("denominacion")]
		public string? Denominacion { get; set; }

		[JsonPropertyName("areaAdscripcion")]
		public string? AreaAdscripcion { get; set; }	
	}

	public class OrigenProcedimiento
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("idOrigenProcedimientoCatFK")]
		public int? IdOrigenProcedimientoCatFK { get; set; }

		[JsonPropertyName("clave")]
		public OrigenProcedimientoCat? Clave { get; set; } = new();

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }
	}

	public class FaltaCometida
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("idFaltaCometidaCatFK")]
		public int? IdFaltaCometidaCatFK { get; set; }

		[JsonPropertyName("clave")]
		[JsonIgnore]
		public FaltaCometidaCat? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }

		[JsonPropertyName("nombreNormatividad")]
		public string? NombreNormatividad { get; set; }

		[JsonPropertyName("articulo")]
		public string? Articulo { get; set; }

		[JsonPropertyName("fraccion")]
		public string? FraccionNormatividad { get; set; }

		[JsonPropertyName("descripcionHechos")]
		public string? DescripcionHechos { get; set; }

		[JsonPropertyName("multipleFalta")]
		public string? MultipleFalta { get; set; }

	}

	public class Resolucion
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

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

		[JsonPropertyName("idOrdenJurisdiccionalFK")]
		public int? IdOrdenJurisdiccionalFK { get; set; }

		[JsonPropertyName("ordenJurisdiccional")]
		[JsonIgnore]
		public OrdenJurisdiccional? OrdenJurisdiccional { get; set; }	

		[JsonPropertyName("autoridadResolutora")]
		public string? AutoridadResolutora { get; set; }

		[JsonPropertyName("autoridadInvestigadora")]
		public string? AutoridadInvestigadora { get; set; }

		[JsonPropertyName("autoridadSubstanciadora")]
		public string? AutoridadSubstanciadora { get; set; }	
	}

	public class AbstencionesNoGravesLGRA
	{
		public Sancion? Sancion { get; set; }
		public DatosGenerales? DatosGenerales { get; set; }
		public EmpleoCargoComision? EmpleoCargoComision { get; set; }
		public OrigenProcedimiento? OrigenProcedimiento { get; set; }
		public FaltaCometida? FaltaCometida { get; set; }
		public Resolucion? Resolucion { get; set; }
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
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

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
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("fechaDestitucion")]
		public DateTime? FechaDestitucion { get; set; }
	}

	public class SancionEconomica
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("monto")]
		public decimal? Monto { get; set; }

		[JsonPropertyName("idMonedaFK")]
		public int? IdMonedaFK { get; set; }

		[JsonPropertyName("moneda")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public MonedaCat? Moneda { get; set; }

		[JsonPropertyName("plazoPago")]
		public PlazoPago? PlazoPago { get; set; }

		[JsonPropertyName("anios")]
		public int? Anio { get; set; }

		[JsonPropertyName("meses")]
		public int? Mes { get; set; }

		[JsonPropertyName("dias")]
		public int? Dia { get; set; }
	}

	public class IndemnizacionMoral
	{
		public int? Id { get; set; }
		public decimal? Monto { get; set; }
		public int? IdTipoMonedaFK { get; set; }
		//public Moneda? Moneda { get; set; } = new();
		//public DateTime? FechaCreacion { get; set; }
		//public DateTime? FechaModificacion { get; set; }
		//public int? Activo { get; set; }
		//public int? idSancionEfectivamenteCobradaFK { get; set; }
		//public SancionEfectivamenteCobradaMoral? SancionEfectivamenteCobrada { get; set; } = new();
    }
	public class Moneda
	{
		public int? Id { get; set; }
		public string? Descripcion { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int? Activo { get; set; }
    }

    public class Indemnizacion
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("monto")]
		public decimal? Monto { get; set; }

		[JsonPropertyName("idTipoMonedaFK")]
		public int? IdTipoMonedaFK { get; set; }

		[JsonPropertyName("moneda")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public MonedaCat? Moneda { get; set; }

		[JsonPropertyName("plazoPago")]
		public PlazoPago? PlazoPago { get; set; } 

		[JsonPropertyName("anios")]
		public int? Anio { get; set; }

		[JsonPropertyName("meses")]
		public int? Mes { get; set; }

		[JsonPropertyName("dias")]
		public int? Dia { get; set; }

		[JsonPropertyName("idSancionEfectivamenteCobradaFK")]
		public int? IdSancionEfectivamenteCobradaFK { get; set; }

		[JsonPropertyName("efectivamenteCobrada")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public SancionEfectivamenteCobrada? EfectivamenteCobrada { get; set; } = new();
	}

	public class PlazoPago
	{
		public int? Id { get; set; }
        [JsonPropertyName("clave")]
		public string? Clave { get; set; }

		[JsonPropertyName("valor")]
		public string? Valor { get; set; }

        [JsonPropertyName("anios")]
        public string? Anios { get; set; }

        [JsonPropertyName("meses")]
        public string? Meses { get; set; }

        [JsonPropertyName("dias")]
        public string? Dias { get; set; }
    }

	public class SancionEfectivamenteCobrada
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("monto")]
		public decimal? Monto { get; set; }

		[JsonPropertyName("moneda")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public MonedaCat? Moneda { get; set; }

		[JsonPropertyName("idMonedaFK")]
		public int? IdMonedaFK { get; set; }

		[JsonPropertyName("fechaCobro")]
		public DateTime? FechaCobro { get; set; }

		[JsonPropertyName("fechaPagoTotal")]
		public DateTime? FechaPagoTotal { get; set; }
	}

    public class SancionEfectivamenteCobradaMoral
	{
        public int? Id { get; set; }
        public decimal? Monto { get; set; }
		public int? IdMonedaFK { get; set; }
        public Moneda? Moneda { get; set; } = new();
        public DateTime? FechaCobro { get; set; }
		public DateTime? FechaPagoTotal { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int? Activo { get; set; }
	}

    public class PlazoPagos
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("anios")]
		public string? Anios { get; set; }

		[JsonPropertyName("meses")]
		public string? Meses { get; set; }

		[JsonPropertyName("dias")]
		public string? Dias { get; set; }
			
		[JsonPropertyName("fechaCreacion")]
		public DateTime? FechaCreacion { get; set; }

		[JsonPropertyName("fechaModificacion")]
		public DateTime? FechaModificacion { get; set; }
	}

	public class Inhabilitacion
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("plazoAnios")]
		public string? Anio { get; set; }

		[JsonPropertyName("plazoMeses")]
		public string? Mes { get; set; }

		[JsonPropertyName("plazoDias")]
		public string? Dia { get; set; }

		[JsonPropertyName("fechaInicial")]
		public DateTime? FechaInicial { get; set; }

		[JsonPropertyName("fechaFinal")]
		public DateTime? FechaFinal { get; set; }
	}

	public class Otro
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("denominacionSancion")]
		public string? DenominacionSancion { get; set; }

		[JsonPropertyName("observaciones")]
		public string? Observaciones { get; set; }
	}

	public class FaltasDeServidoresPublicosG
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }
		public Sancion? Sancion { get; set; } = new();
		public int? IdDatosGeneralesFK { get; set; }
		public DatosGenerales? DatosGenerales { get; set; } = new();	
		public int? IdEmpleoCargoComisionFK { get; set; }
		public EmpleoCargoComision? EmpleoCargoComision { get; set; } = new();
		public int? IdNivelJerarquicoFK { get; set; }
		public NivelJerarquico? NivelJerarquico { get; set; } = new();
		public int? IdOrigenProcedimientoFK { get; set; }
		public OrigenProcedimiento? OrigenProcedimiento { get; set; } = new();
		public int? IdFaltaCometidaFK { get; set; }
		public FaltaCometida? FaltaCometida { get; set; } = new();
		public int? IdResolucion { get; set; }
		public Resolucion? Resolucion { get; set; } = new();
		public int? IdTipoSancionFK { get; set; }
		public TipoSancion? TipoSancion { get; set; } = new();
		public int? IdSuspension { get; set; }
		public Suspension? Suspension { get; set; } = new();
		public int? IdDestitucionEmpleo { get; set; }
		public DestitucionEmpleo? DestitucionEmpleo { get; set; } = new();
		public int? IdSancionEconomicaFK { get; set; }
		public SancionEconomica? SancionEconomica { get; set; } = new();
		public int? IdSancionEfectivamenteCobradaFK { get; set; }
		public SancionEfectivamenteCobrada? SancionEfectivamenteCobrada { get; set; } = new();
		public int? IdInhabilitacionFK { get; set; }
		public Inhabilitacion? Inhabilitacion { get; set; } = new();
		public int? IdOtro { get; set; }
		public Otro? Otro { get; set; } = new();
		public int? Activo { get; set; } = 1;
		public string? MultipleSancion { get; set; }
		public int? IdUsuarioFK { get; set; }
	}

	public class AddFaltasDeServidoresPublicosG
	{
		[JsonPropertyName("id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }

		[JsonPropertyName("fecha")]
		public DateTime? Fecha { get; set; }

		[JsonPropertyName("expediente")]
		public string? Expediente { get; set; }

		[JsonPropertyName("idDatosGeneralesFK")]
		public int? IdDatosGeneralesFK { get; set; }

		[JsonPropertyName("datosGenerales")]
		public DatosGenerales? DatosGenerales { get; set; } = new();

		[JsonPropertyName("idEmpleoCargoComisionFK")]
		public int? IdEmpleoCargoComisionFK { get; set; }

		[JsonPropertyName("empleoCargoComision")]
		public EmpleoCargoComision? EmpleoCargoComision { get; set; } = new();

		[JsonPropertyName("idNivelJerarquicoFK")]
		public int? IdNivelJerarquicoFK { get; set; }

		[JsonPropertyName("nivelJerarquico")]
		public NivelJerarquico? NivelJerarquico { get; set; } = new();

		[JsonPropertyName("idOrigenProcedimientoFK")]
		public int? IdOrigenProcedimientoFK { get; set; }

		[JsonPropertyName("origenProcedimiento")]
		public OrigenProcedimiento? OrigenProcedimiento { get; set; } = new();

		[JsonPropertyName("idFaltaCometidaFK")]
		public int? IdFaltaCometidaFK { get; set; }

		[JsonPropertyName("faltaCometida")]
		public FaltaCometida? FaltaCometida { get; set; } = new();

		[JsonPropertyName("idResolucion")]
		public int? IdResolucion { get; set; }

		[JsonPropertyName("resolucion")]
		public Resolucion? Resolucion { get; set; } = new();

		[JsonPropertyName("idTipoSancionFK")]
		public int? IdTipoSancionFK { get; set; }

		[JsonPropertyName("idSuspension")]
		public int? IdSuspension { get; set; }

		[JsonPropertyName("suspension")]
		public Suspension? Suspension { get; set; } = new();

		[JsonPropertyName("idDestitucionEmpleo")]
		public int? IdDestitucionEmpleo { get; set; }

		[JsonPropertyName("destitucionEmpleo")]
		public DestitucionEmpleo? DestitucionEmpleo { get; set; } = new();

		[JsonPropertyName("idSancionEconomicaFK")]
		public int? IdSancionEconomicaFK { get; set; }

		[JsonPropertyName("sancionEconomica")]
		public SancionEconomica? SancionEconomica { get; set; } = new();

		[JsonPropertyName("idSancionEfectivamenteCobradaFK")]
		public int? IdSancionEfectivamenteCobradaFK { get; set; }

		[JsonPropertyName("sancionEfectivamenteCobrada")]
		public SancionEfectivamenteCobrada? SancionEfectivamenteCobrada { get; set; } = new();

		[JsonPropertyName("idInhabilitacionFK")]
		public int? IdInhabilitacionFK { get; set; }

		[JsonPropertyName("inhabilitacion")]
		public Inhabilitacion? Inhabilitacion { get; set; } = new();

		[JsonPropertyName("idOtro")]
		public int? IdOtro { get; set; }

		[JsonPropertyName("otro")]
		public Otro? Otro { get; set; } = new();

		[JsonPropertyName("fechaCreacion")]
		public DateTime? FechaCreacion { get; set; }

		[JsonPropertyName("fechaModificacion")]
		public DateTime? FechaModificacion { get; set; }

		[JsonPropertyName("activo")]
		public int? Activo { get; set; }

		[JsonPropertyName("multipleSancion")]
		public string? MultipleSancion { get; set; }

		[JsonPropertyName("idUsuarioFK")]
		public int? IdUsuarioFK { get; set; }
	}
    public class FaltasDeServidoresPublicosNoGraves
    {
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }

        public DateTime? Fecha { get; set; }
        public string? Expediente { get; set; }

        public int? IdDatosGeneralesFK { get; set; }
        public DatosGenerales? DatosGenerales { get; set; } = new();

        public int? IdEmpleoCargoComisionFK { get; set; }
        public EmpleoCargoComision? EmpleoCargoComision { get; set; } = new();

        public int? IdNivelJerarquicoFK { get; set; }
        public NivelJerarquico? NivelJerarquico { get; set; } = new();

        public int? IdOrigenProcedimientoFK { get; set; }
        public OrigenProcedimiento? OrigenProcedimiento { get; set; } = new();

        public int? IdFaltaCometidaFK { get; set; }
        public FaltaCometida? FaltaCometida { get; set; } = new();

        public int? IdResolucion { get; set; }
        public Resolucion? Resolucion { get; set; } = new();

        public int? IdTipoSancionFK { get; set; }

        public int? IdTipoAmonestacionFK { get; set; }
        public TipoAmonestacion? TipoAmonestacion { get; set; } = new();

        public int? IdSuspension { get; set; }
        public Suspension? Suspension { get; set; } = new();

        public int? IdDestitucionEmpleo { get; set; }
        public DestitucionEmpleo? DestitucionEmpleo { get; set; } = new();

        public int? IdInhabilitacionFK { get; set; }
        public Inhabilitacion? Inhabilitacion { get; set; } = new();

        public int? IdOtro { get; set; }
        public Otro? Otro { get; set; } = new();

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public int? Activo { get; set; } = 1;
        public string? MultipleSancion { get; set; }

        public int? IdUsuarioFK { get; set; }
    }


    public class AddFaltasDeServidoresPublicosNoGraves
		{
			[JsonPropertyName("id")]
			[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
			public int? Id { get; set; }

			[JsonPropertyName("fecha")]
			public DateTime? Fecha { get; set; }

			[JsonPropertyName("expediente")]
			public string? Expediente { get; set; }

			[JsonPropertyName("idDatosGeneralesFK")]
			public int? IdDatosGeneralesFK { get; set; }

			[JsonPropertyName("datosGenerales")]
			public DatosGenerales? DatosGenerales { get; set; } = new();

			[JsonPropertyName("idEmpleoCargoComisionFK")]
			public int? IdEmpleoCargoComisionFK { get; set; }

			[JsonPropertyName("empleoCargoComision")]
			public EmpleoCargoComision? EmpleoCargoComision { get; set; } = new();

			[JsonPropertyName("idNivelJerarquicoFK")]
			public int? IdNivelJerarquicoFK { get; set; }

			[JsonPropertyName("nivelJerarquico")]
			public NivelJerarquico? NivelJerarquico { get; set; } = new();

			[JsonPropertyName("idOrigenProcedimientoFK")]
			public int? IdOrigenProcedimientoFK { get; set; }

			[JsonPropertyName("origenProcedimiento")]
			public OrigenProcedimiento? OrigenProcedimiento { get; set; } = new();

			[JsonPropertyName("idFaltaCometidaFK")]
			public int? IdFaltaCometidaFK { get; set; }

			[JsonPropertyName("faltaCometida")]
			public FaltaCometida? FaltaCometida { get; set; } = new();

			[JsonPropertyName("idResolucion")]
			public int? IdResolucion { get; set; }

			[JsonPropertyName("resolucion")]
			public Resolucion? Resolucion { get; set; } = new();

			[JsonPropertyName("idTipoSancionFK")]
			public int? IdTipoSancionFK { get; set; }

			[JsonPropertyName("idTipoAmonestacionFK")]
			public int? IdTipoAmonestacionFK { get; set; }

			[JsonPropertyName("tipoAmonestacion")]
			public TipoAmonestacion? TipoAmonestacion { get; set; } = new();

        [JsonPropertyName("idSuspension")]
			public int? IdSuspension { get; set; }

			[JsonPropertyName("suspension")]
			public Suspension? Suspension { get; set; } = new();

			[JsonPropertyName("idDestitucionEmpleo")]
			public int? IdDestitucionEmpleo { get; set; }

			[JsonPropertyName("destitucionEmpleo")]
			public DestitucionEmpleo? DestitucionEmpleo { get; set; } = new();	

			[JsonPropertyName("idInhabilitacionFK")]
			public int? IdInhabilitacionFK { get; set; }

			[JsonPropertyName("inhabilitacion")]
			public Inhabilitacion? Inhabilitacion { get; set; } = new();

			[JsonPropertyName("idOtro")]
			public int? IdOtro { get; set; }

			[JsonPropertyName("otro")]
			public Otro? Otro { get; set; } = new();

			[JsonPropertyName("fechaCreacion")]
			public DateTime? FechaCreacion { get; set; }

			[JsonPropertyName("fechaModificacion")]
			public DateTime? FechaModificacion { get; set; }

			[JsonPropertyName("activo")]
			public int? Activo { get; set; } = 1;

			[JsonPropertyName("multipleSancion")]
			public string? MultipleSancion { get; set; }	

			[JsonPropertyName("idUsuarioFK")]
			public int? IdUsuarioFK { get; set; }
		}

		public class AddFaltasGravesPersonasFisicas
		{
			[JsonPropertyName("id")]
			[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
			public int? Id { get; set; }
			
			public DateTime? Fecha { get; set; }

			[JsonPropertyName("expediente")]
			public string? Expediente { get; set; }

			[JsonPropertyName("idDatosGeneralesFK")]
			public int? IdDatosGeneralesFK { get; set; }

			[JsonPropertyName("datosGenerales")]
			public DatosGenerales? DatosGenerales { get; set; } = new();

			[JsonPropertyName("idEmpleoCargoComisionFK")]
			public int? IdEmpleoCargoComisionFK { get; set; }

			[JsonPropertyName("empleoCargoComision")]
			public EmpleoCargoComision? EmpleoCargoComision { get; set; } = new();	

			[JsonPropertyName("idOrigenProcedimientoFK")]
			public int? IdOrigenProcedimientoFK { get; set; }

			[JsonPropertyName("origenProcedimiento")]
			public OrigenProcedimiento? OrigenProcedimiento { get; set; } = new();

			[JsonPropertyName("idFaltaCometidaFK")]
			public int? IdFaltaCometidaFK { get; set; }

			[JsonPropertyName("faltaCometida")]
			public FaltaCometida? FaltaCometida { get; set; } = new();

			[JsonPropertyName("idResolucion")]
			public int? IdResolucion { get; set; }

			[JsonPropertyName("resolucion")]
			public Resolucion? Resolucion { get; set; } = new();

			[JsonPropertyName("idTipoSancionFK")]
			public int? IdTipoSancionFK { get; set; }

			[JsonPropertyName("idIndeminizacionFK")]
			public int? IdIndeminizacionFK { get; set; }

			[JsonPropertyName("indeminizacion")]
			public Indemnizacion? Indeminizacion { get; set; } = new();

			[JsonPropertyName("idPlazoPagoFK")]
			public int? IdPlazoPagoFK { get; set; }

			[JsonPropertyName("plazoPago")]
			public PlazoPagos? PlazoPago { get; set; } = new();

			[JsonPropertyName("idInhabilitacionFK")]
			public int? IdInhabilitacionFK { get; set; }

			[JsonPropertyName("inhabilitacion")]
			public Inhabilitacion? Inhabilitacion { get; set; } = new();

			[JsonPropertyName("idSancionEconomicaFK")]
			public int? IdSancionEconomicaFK { get; set; }

			[JsonPropertyName("sancionEconomica")]
			public SancionEconomica? SancionEconomica { get; set; } = new();

			[JsonPropertyName("idSancionEfectivamenteCobradaFK")]
			public int? IdSancionEfectivamenteCobradaFK { get; set; }

			[JsonPropertyName("sancionEfectivamenteCobrada")]
			public SancionEfectivamenteCobrada? SancionEfectivamenteCobrada { get; set; } = new();

			[JsonPropertyName("idOtro")]
			public int? IdOtro { get; set; }

			[JsonPropertyName("otro")]
			public Otro? Otro { get; set; } = new();

			[JsonPropertyName("fechaCreacion")]
			public DateTime? FechaCreacion { get; set; }

			[JsonPropertyName("fechaModificacion")]
			public DateTime? FechaModificacion { get; set; }

			[JsonPropertyName("activo")]
			public int? Activo { get; set; } = 1;

			[JsonPropertyName("multipleSancion")]
			public string? MultipleSancion { get; set; }

			[JsonPropertyName("idUsuarioFK")]
			public int? IdUsuarioFK { get; set; }
		}

	public class SearchFaltasDeServidoresPublicosG
	{
		[JsonPropertyName("fecha")]
		public DateTime? Fecha { get; set; }

		[JsonPropertyName("expediente")]
		public string? Expediente { get; set; }

		[JsonPropertyName("nombres")]
		public string? Nombres { get; set; }

		[JsonPropertyName("primerApellido")]
		public string? PrimerApellido { get; set; }

		[JsonPropertyName("segundoApellido")]
		public string? SegundoApellido { get; set; }
	}

	public class ApiResponse<T>
	{
		public T Data { get; set; }
	}


	public enum SesionesFaltasDeServidoresPublicosG 
	{
		Fecha = 0,
        Sancion,
        DatosGenerales,
		EmpleoCargoComision,
		OrigenProcedimiento,
		FaltaCometida,
		Resolucion,
		TipoSancion,
		Suspension,
		DestitucionEmpleo,
		SancionEconomica,
		Inhabilitacion,
		Otro,
		Observaciones
	}

	public enum SesionesFaltasDeServidoresPublicosNoGraves
	{
		Fecha = 0,
        Expediente,
        DatosGenerales,
		EmpleoCargoComision,
		NivelJerarquico,
		OrigenProcedimiento,
		FaltaCometida,
		Resolucion,
		TipoSancion,
		Suspension,
		DestitucionEmpleo,
		Amonestacion,
		Inhabilitacion,
		Otro,
		Observaciones
    }

	public enum SesionesFaltasGravesPersonasFisicas
	{
		Fecha = 0,
		Expediente,
		DatosGenerales,
		EmpleoCargoComision,
		OrigenProcedimiento,
		FaltaCometida,
		Resolucion,
		TipoSancion,
		Indeminizacion,
		SancionEconomica,
		Inhabilitacion,
		Otro,
		Observaciones
	}
    public enum SesionesFaltasGravesPersonasMorales
    {
        Fecha = 0,
		Expediente,
		DatosGenerales,
		DirectorGenral,
		DatosEnte,
		OrigenProcedimiento,
		FaltaCometida,
		Resolucion,
		TipoSancion,
		Inhabilitacion,
		Indeminizacion,
		SancionEconomica,
		Suspension,
		Disolucion,
		Otro,
        Observaciones
    }

    public enum TipoDomiclio 
	{
		SELECCIONA = 0,
		MEXICO ,
		EXTRANJERO
	}
}
