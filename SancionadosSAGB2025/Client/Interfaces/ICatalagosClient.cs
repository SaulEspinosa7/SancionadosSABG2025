using SancionadosSAGB2025.Shared.Catalogos;

namespace SancionadosSAGB2025.Client.Interfaces
{
	public interface ICatalagosClient
	{
		Task<Catalogos> ObtenerTodosLosCatalogos();
		Task<List<EntidadFederativaEntidad>> ObtenerEntidadFederativa();
		Task<List<FaltaCometidaEntidad>> ObtenerFaltaCometida();
		Task<List<NivelOrdenGobierno>> ObtenerNivelOrdenGobierno();
		Task<List<OrdenJurisdiccional>> ObtenerOrdenJurisdiccional();
		Task<List<OrigenProcedimientoEntidad>> ObtenerOrigenProcedimiento();
		Task<List<Sexo>> ObtenerSexo();
		Task<List<TipoAmonestacion>> ObtenerTipoAmonestacion();
		Task<List<TipoSancion>> ObtenerTipoSancion();
		Task<List<TipoVialidad>> ObtenerTipoVialidad();
		Task<List<AmbitoPublico>> ObtenerAmbitoPublico();
		Task<List<NivelJerarquicoEntidad>> ObtenerNivelJerarquico();
	}
}
