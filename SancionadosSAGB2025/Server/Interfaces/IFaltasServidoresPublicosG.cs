using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Interfaces
{
	public interface IFaltasServidoresPublicosG
	{
		Task<RespuestaRegistro> AgregarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG);
		Task<RespondeUpdateFaltas> ActualizarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG);
		Task<RegistroFaltasSPG> FromFaltasDeServidoresPublicosG(FaltasDeServidoresPublicosG faltas);
		Task<UpdateFaltasSPG> FromUpdateFaltasDeServidoresPublicosG(FaltasDeServidoresPublicosG faltas);
		Task<int?> AgregarDatosGenerales(DatosGenerales datosGenerales);
	}
}
