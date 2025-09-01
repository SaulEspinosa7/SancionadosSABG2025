using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Interfaces
{
	public interface IDatosGenerales
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(DatosGenerales datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(DatosGenerales datosGenerales);
	}

	public interface IEmpleoCargoComision
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(EmpleoCargoComision datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(EmpleoCargoComision datosGenerales);
	}
	public interface INivelJerarquico
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(NivelJerarquico datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(NivelJerarquico datosGenerales);
	}
	public interface IOrigenProcedimiento
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(OrigenProcedimiento datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(OrigenProcedimiento datosGenerales);
	}
	public interface IFaltaCometida
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(FaltaCometida datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(FaltaCometida datosGenerales);
	}
	public interface IResolucion
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(Resolucion datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(Resolucion datosGenerales);
	}
	public interface ITipoSancion
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(TipoSancion datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(TipoSancion datosGenerales);
	}

	public interface ISuspension
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(Suspension datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(Suspension datosGenerales);
	}

	public interface IDestitucionEmpleo
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(DestitucionEmpleo datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(DestitucionEmpleo datosGenerales);
	}

	public interface ISancionEconomica
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(SancionEconomica datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(SancionEconomica datosGenerales);
	}

	public interface ISancionEfectivamenteCobrada
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(SancionEfectivamenteCobrada datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(SancionEfectivamenteCobrada datosGenerales);
	}

	public interface IInhabilitacion
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(Inhabilitacion datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(Inhabilitacion datosGenerales);
	}

	public interface IOtro
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(Otro datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(Otro datosGenerales);
	}
}
