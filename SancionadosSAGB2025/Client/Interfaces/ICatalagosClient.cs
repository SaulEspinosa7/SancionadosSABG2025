using SancionadosSAGB2025.Shared.Catalogos;

namespace SancionadosSAGB2025.Client.Interfaces
{
	public interface ICatalagosClient
	{
		Task<Catalogos> ObtenerTodosLosCatalogos();
		Task<List<EntidadFederativa>> ObtenerEntidadFederativa();
		Task<List<FaltaCometida>> ObtenerFaltaCometida();
		Task<List<NivelOrdenGobierno>> ObtenerNivelOrdenGobierno();
		Task<List<OrdenJurisdiccional>> ObtenerOrdenJurisdiccional();
		Task<List<OrigenProcedimiento>> ObtenerOrigenProcedimiento();
		Task<List<Sexo>> ObtenerSexo();
		Task<List<TipoAmonestacion>> ObtenerTipoAmonestacion();
		Task<List<TipoSancion>> ObtenerTipoSancion();
		Task<List<TipoVialidad>> ObtenerTipoVialidad();
		Task<List<AmbitoPublico>> ObtenerAmbitoPublico();
		Task<List<NivelJerarquico>> ObtenerNivelJerarquico();
	}
}
