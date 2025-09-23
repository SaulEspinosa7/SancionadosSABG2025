using SancionadosSAGB2025.Shared.Sanciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Grave
{
    public class FaltasGravesEntidad
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Expediente { get; set; }

        [JsonPropertyName("idDatosGeneralesFK")]
        public int? IdDatosGeneralesFK { get; set; }

        [JsonPropertyName("datosGenerales")]
        public DatosGeneralesGraves? DatosGenerales { get; set; } = new();

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
        public string? MultipleSancion { get; set; } = string.Empty;

        [JsonPropertyName("bandera")]
        public int? Bandera { get; set; } = 0;

        [JsonPropertyName("idUsuarioFK")]
        public int? IdUsuarioFK { get; set; }

      //  public SancionEconomicaEfectivamenteCobrada? SancionEfectivamenteCobrada { get; set; } = new();

        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }
    }
    public class DatosGeneralesGrave
    {
        [JsonPropertyName("idDatosGenerales")]
        public int? IdDatosGenerales { get; set; }

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

        [JsonPropertyName("idSexoFk")]
        public int? IdSexoFk { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("objetoSocial")]
        public string? ObjetoSocial { get; set; }
    }

    public class EmpleoCargoComisionGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("idEntidadFederativaFK")]
        public int? IdEntidadFederativaFK { get; set; }

        [JsonPropertyName("idNivelOrdenGobiernoFK")]
        public int? IdNivelOrdenGobiernoFK { get; set; }

        [JsonPropertyName("idAmbitoPublicoFK")]
        public int? IdAmbitoPublicoFK { get; set; }

        [JsonPropertyName("nombreEntePublico")]
        public string? NombreEntePublico { get; set; }

        [JsonPropertyName("siglasEntePublico")]
        public string? SiglasEntePublico { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }

    public class NivelJerarquicoGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("idNivelJerarquicoFK")]
        public int? IdNivelJerarquicoFK { get; set; }

        [JsonPropertyName("valor")]
        public string? Valor { get; set; }

        [JsonPropertyName("denominacion")]
        public string? Denominacion { get; set; }

        [JsonPropertyName("areaAdscripcion")]
        public string? AreaAdscripcion { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }

    public class OrigenProcedimientoGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("idOrigenProcedimientoCatFK")]
        public int? IdOrigenProcedimientoCatFK { get; set; }

        [JsonPropertyName("valor")]
        public string? Valor { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }

    public class FaltaCometidaGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("idFaltaCometidaCatFK")]
        public int? IdFaltaCometidaCatFK { get; set; }

        [JsonPropertyName("valor")]
        public string? Valor { get; set; }

        [JsonPropertyName("nombreNormatividad")]
        public string? NombreNormatividad { get; set; }

        [JsonPropertyName("articulo")]
        public string? Articulo { get; set; }

        [JsonPropertyName("fraccion")]
        public string? Fraccion { get; set; }

        [JsonPropertyName("descripcionHechos")]
        public string? DescripcionHechos { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }

        [JsonPropertyName("multipleFalta")]
        public string? MultipleFalta { get; set; }
    }

    public class ResolucionGrave
    {
        [JsonPropertyName("id")]
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

        [JsonPropertyName("autoridadResolutora")]
        public string? AutoridadResolutora { get; set; }

        [JsonPropertyName("autoridadInvestigadora")]
        public string? AutoridadInvestigadora { get; set; }

        [JsonPropertyName("autoridadSubstanciadora")]
        public string? AutoridadSubstanciadora { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }

    public class SuspensionGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("plazoMeses")]
        public int? PlazoMeses { get; set; }

        [JsonPropertyName("plazoDias")]
        public int? PlazoDias { get; set; }

        [JsonPropertyName("fechaInicial")]
        public DateTime? FechaInicial { get; set; }

        [JsonPropertyName("fechaFinal")]
        public DateTime? FechaFinal { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }

    public class DestitucionEmpleoGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("fechaDestitucion")]
        public DateTime? FechaDestitucion { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }

    public class SancionEconomicaGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("monto")]
        public decimal? Monto { get; set; }

        [JsonPropertyName("idMonedaFK")]
        public int? IdMonedaFK { get; set; }

        [JsonPropertyName("anios")]
        public int? Anios { get; set; }

        [JsonPropertyName("meses")]
        public int? Meses { get; set; }

        [JsonPropertyName("dias")]
        public int? Dias { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }

        [JsonPropertyName("idSancionECobradaFK")]
        public int? IdSancionECobradaFK { get; set; }

        [JsonPropertyName("sancionEfectivamenteCobrada")]
        public SancionEfectivamenteCobrada? SancionEfectivamenteCobrada { get; set; }
    }

    public class SancionEfectivamenteCobradaGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("monto")]
        public decimal? Monto { get; set; }

        [JsonPropertyName("idMonedaFK")]
        public int? IdMonedaFK { get; set; }

        [JsonPropertyName("moneda")]
        public MonedaGrave? Moneda { get; set; }

        [JsonPropertyName("fechaCobro")]
        public DateTime? FechaCobro { get; set; }

        [JsonPropertyName("fechaPagoTotal")]
        public DateTime? FechaPagoTotal { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }

    public class MonedaGrave
    {
        [JsonPropertyName("idMoneda")]
        public int? IdMoneda { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }

    public class InhabilitacionGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("plazoAnios")]
        public string? PlazoAnios { get; set; }

        [JsonPropertyName("plazoMeses")]
        public string? PlazoMeses { get; set; }

        [JsonPropertyName("plazoDias")]
        public string? PlazoDias { get; set; }

        [JsonPropertyName("fechaInicial")]
        public DateTime? FechaInicial { get; set; }

        [JsonPropertyName("fechaFinal")]
        public DateTime? FechaFinal { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }

        [JsonPropertyName("bandera")]
        public int? Bandera { get; set; }
    }

    public class OtroGrave
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("denominacionSancion")]
        public string? DenominacionSancion { get; set; }

        [JsonPropertyName("observaciones")]
        public string? Observaciones { get; set; }

        [JsonPropertyName("fechaFinal")]
        public DateTime? FechaFinal { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("activo")]
        public int? Activo { get; set; }
    }
}



