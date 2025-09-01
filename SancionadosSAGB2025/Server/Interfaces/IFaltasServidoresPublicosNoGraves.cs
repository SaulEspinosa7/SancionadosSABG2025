using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Interfaces
{
	public interface IFaltasServidoresPublicosNoGraves
	{
		Task<RespuestaRegistroNoGraves> AgregarFaltasSPG(AddFaltasDeServidoresPublicosNoGraves addFaltasDeServidoresPublicosNoGraves);
		Task<List<AddFaltasDeServidoresPublicosNoGraves>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG);
	}
}
