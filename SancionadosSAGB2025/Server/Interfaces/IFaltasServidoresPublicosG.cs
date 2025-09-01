using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Interfaces
{
	public interface IFaltasServidoresPublicosG
	{
		Task<RespuestaRegistro> AgregarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG);
		Task<RespuestaRegistro> ActualizarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG);
		Task<List<AddFaltasDeServidoresPublicosG>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG);
		Task<AddFaltasDeServidoresPublicosG> FromFaltasDeServidoresPublicosG(FaltasDeServidoresPublicosG faltas);
		Task<AddFaltasDeServidoresPublicosG> FromUpdateFaltasDeServidoresPublicosG(FaltasDeServidoresPublicosG faltas);
	}
}
