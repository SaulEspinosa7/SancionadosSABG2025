using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Sanciones;
using SancionadosSAGB2025.Shared.Validadores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Moral
{
    public class PersonaMoralEntidad
    {
        public int? Id { get; set; } 
        public DateTime? Fecha { get; set; }
        public string? Expediente { get; set; }
        public int? IdDatosGeneralesFK { get; set; }      

        public DatosGeneralesMorales? DatosGenerales { get; set; } = new DatosGeneralesMorales();

        public int? IdDirectorGeneralFK { get; set; }
        public DirectorGeneral? DirectorGeneral { get; set; } = new DirectorGeneral();

        public int? IdEmpleoCargoComisionFK { get; set; }
        public EmpleoCargoComision? EmpleoCargoComision { get; set; } = new EmpleoCargoComision();

        public int? IdOrigenProcedimientoFK { get; set; }
        public OrigenProcedimiento? OrigenProcedimiento { get; set; } = new OrigenProcedimiento();

        public int? IdFaltaCometidaFK { get; set; }
        public FaltaCometida? FaltaCometida { get; set; } = new FaltaCometida();

        public int? IdResolucion { get; set; }
        public Resolucion Resolucion { get; set; } = new Resolucion();

        public int? IdTipoSancionFK { get; set; }

        public int? IdInhabilitacionFK { get; set; }
        public Inhabilitacion? Inhabilitacion { get; set; } = new Inhabilitacion();

        public int? IdIndeminizacionFK { get; set; }
        public IndemnizacionMoral? Indeminizacion { get; set; } = new IndemnizacionMoral();

        public int? IdPlazoPagoFK { get; set; }
        public PlazoPago? PlazoPago { get; set; } = new PlazoPago();

        public int? IdSancionEfectivamenteCobradaFK { get; set; }
        public SancionEfectivamenteCobradaMoral? SancionEfectivamenteCobrada { get; set; } = new SancionEfectivamenteCobradaMoral();

        public int? IdSancionEconomicaFK { get; set; }
        public  SancionEconomica? SancionEconomica { get; set; } = new SancionEconomica();

        public int? IdSuspensionActividadesFK { get; set; }
        public SuspensionActividades? SuspensionActividades { get; set; } = new SuspensionActividades();

        public int? IdDisolucionSociedadFK { get; set; }
        public DisolucionSociedad? DisolucionSociedad { get; set; } = new DisolucionSociedad();

        public int? IdOtro { get; set; }
        public Otro? Otro { get; set; } = new Otro();

        public int? Activo { get; set; } = 1;
        public int? IdUsuarioFK { get; set; }
        public string? MultipleSancion { get; set; }
    }

    public class DirectorGeneral
    {
        public int? IdDirectorGeneral { get; set; }
        public string? Nombres { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        [RfcValidationCharactersFisica(ErrorMessage = "RFC del Director General no es válido")]
        public string? Rfc { get; set; }
        [CurpValidation(ErrorMessage = "La CURP debe contener 18 caracteres")]
        [RegularExpression(@"\b[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]$", ErrorMessage = "CURP no valido")]
        public string? Curp { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }   
        public int? IdRepresentanteLegalFK { get; set; }
        public RepresentanteLegal? RepresentanteLegal { get; set; } = new();
    }
    public class RepresentanteLegal
    {
        public int? idRepresentanteLegal { get; set; }
        public string? Nombres { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        [RfcValidationCharactersFisica(ErrorMessage = "RFC del Representante Legal no es válido")]
        public string? Rfc { get; set; }
        [CurpValidation(ErrorMessage = "La CURP debe contener 18 caracteres")]
        [RegularExpression(@"\b[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]$", ErrorMessage = "CURP no valido")]
        public string? Curp { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class DisolucionSociedad
    {
        public int? Id { get; set; }
        public DateTime? FechaDisolucion { get; set; }
    }

    public class SuspensionActividades
    {
        public int? Id { get; set; }
        public int? PlazoSuspensionAnios { get; set; }
        public int? PlazoSuspensionMeses { get; set; }
        public int? PlazoSuspensionDias { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
    }

    public class DatosGeneralesMorales
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
        [RfcValidationCharactersMoral(ErrorMessage = "El RFC debe contener 12 caracteres alfanuméricos y el formato correcto")]
        public string? Rfc { get; set; }

        [JsonPropertyName("idSexoFk")]
        public int? IdSexoFk { set; get; }

        [JsonPropertyName("sexo")]
        [JsonIgnore]
        public Sexo? Sexo { get; set; }

        [JsonPropertyName("idDomicilioMexicoFK")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? IdDomicilioMexicoFK { set; get; }

        [JsonPropertyName("domicilioMexico")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DomicilioMexicoMorales? DomicilioMexico { get; set; }

        [JsonPropertyName("idDomicilioExtranjeroFK")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? IdDomicilioExtranjeroFK { set; get; }

        [JsonPropertyName("domicilioExtranjero")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DomicilioExtranjeroMorales? DomicilioExtranjero { get; set; }

        [JsonPropertyName("objetoSocial")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ObjetoSocial { get; set; }
    }
    public class DomicilioMexicoMorales
    {

        public int? IdDomicilioMexico { set; get; }

        public int? IdTipoVialidadFK { set; get; }

        public TipoVialidad? TipoVialidad { get; set; }


        public string? NombreVialidad { get; set; }


        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
        public string? NumeroExterior { get; set; }

  
        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
        public string? NumeroInterior { get; set; }


        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
        public string? ColoniaLocalidad { get; set; }

        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
        public string? MunicipioAlcaldia { get; set; }


        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
        public string? CodigoPostal { get; set; }

  
        public int? IdEntidadFederativaFK { set; get; }

        public EntidadFederativa? EntidadFederativa { get; set; }
    }

    public class DomicilioExtranjeroMorales
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("idDomicilioExtranjero")]
        public int? IdDomicilioExtranjero { set; get; }

        [JsonPropertyName("ciudad")]
        public string? Ciudad { get; set; }

        [JsonPropertyName("provincia")]
        public string? Provincia { get; set; }

        [JsonPropertyName("calle")]
        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
        public string? Calle { get; set; }

        [JsonPropertyName("numeroExterior")]
        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
        public string? NumeroExterior { get; set; }

        [JsonPropertyName("numeroInterior")]
        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
        public string? NumeroInterior { get; set; }

        [JsonPropertyName("codigoPostal")]
        [LetrasNumerosEspacios(ErrorMessage = "Número exterior contiene caracteres inválidos")]
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
}
