using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Interfaces
{
	public interface IDatosGenerales
	{
		Task<RespondeDatosGenerales> AgregarDatoaGenerales(DatosGenerales datosGenerales);
		Task<RegistroDatosGenerales> FromDatosGenerales(DatosGenerales datosGenerales);
	}
}
