using SancionadosSAGB2025.Shared.Catalogos;

namespace SancionadosSAGB2025.Server.Interfaces
{
	public interface ICatalogos
	{
		Task<Catalogos> ObtenerTodosLosCatalogos(string token);
		Task<List<EntidadFederativaEntidad>> ObtenerEntidadFederativa(string token);
		Task<List<FaltaCometidaEntidad>> ObtenerFaltaCometida(string token);
		Task<List<NivelOrdenGobierno>> ObtenerNivelOrdenGobierno(string token);
		Task<List<OrdenJurisdiccional>> ObtenerOrdenJurisdiccional(string token);
		Task<List<OrigenProcedimientoEntidad>> ObtenerOrigenProcedimiento(string token);
		Task<List<Sexo>> ObtenerSexo(string token);
		Task<List<TipoAmonestacion>> ObtenerTipoAmonestacion(string token);
		Task<List<TipoSancion>> ObtenerTipoSancion(string token);
		Task<List<TipoVialidad>> ObtenerTipoVialidad(string token);
		Task<List<AmbitoPublico>> ObtenerAmbitoPublico(string token);
		Task<List<NivelJerarquicoEntidad>> ObtenerNivelJerarquico(string token);
	}
}
