using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Interfaces
{
	public interface IFaltasSPG
	{
		Task<RespuestaRegistro> AgregarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG);
		Task<RespuestaRegistro> ActualizarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG);
		Task<List<AddFaltasDeServidoresPublicosG>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG);
	}
}
