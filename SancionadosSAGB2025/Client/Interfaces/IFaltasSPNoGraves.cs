using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Interfaces
{
	public interface IFaltasSPNoGraves
	{
		Task<RespuestaRegistroNoGraves> AgregarFaltasSPG(AddFaltasDeServidoresPublicosNoGraves addFaltasDeServidoresPublicosNoGraves);
		Task<RespuestaRegistroNoGraves> ActualizarFaltasSPG(AddFaltasDeServidoresPublicosNoGraves addFaltasDeServidoresPublicosNoGraves);
		Task<List<AddFaltasDeServidoresPublicosNoGraves>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG);
	}
}
