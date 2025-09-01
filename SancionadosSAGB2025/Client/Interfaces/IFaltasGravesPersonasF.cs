using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Interfaces
{
	public interface IFaltasGravesPersonasF
	{
		Task<RespuestaRegistroFaltasGravesPersonasFisicas> AgregarFaltasGravesPersonasFisicas(AddFaltasGravesPersonasFisicas addFaltasGravesPersonasFisicas);
		Task<List<AddFaltasGravesPersonasFisicas>> ObtenerFaltasGravesPersonasFisicas(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG);
	}
}
