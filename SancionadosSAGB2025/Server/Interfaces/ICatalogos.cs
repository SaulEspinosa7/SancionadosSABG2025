using SancionadosSAGB2025.Shared.Catalogos;

namespace SancionadosSAGB2025.Server.Interfaces
{
	public interface ICatalogos
	{
		Task<Catalogos> ObtenerTodosLosCatalogos();
		Task<List<EntidadFederativa>> ObtenerEntidadFederativa();
		Task<List<FaltaCometidaCat>> ObtenerFaltaCometida();
		Task<List<NivelOrdenGobierno>> ObtenerNivelOrdenGobierno();
		Task<List<OrdenJurisdiccional>> ObtenerOrdenJurisdiccional();
		Task<List<OrigenProcedimientoCat>> ObtenerOrigenProcedimiento();
		Task<List<Sexo>> ObtenerSexo();
		Task<List<TipoAmonestacion>> ObtenerTipoAmonestacion();
		Task<List<TipoSancion>> ObtenerTipoSancion();
		Task<List<TipoVialidad>> ObtenerTipoVialidad();
		Task<List<AmbitoPublico>> ObtenerAmbitoPublico();
		Task<List<NivelJerarquicoCat>> ObtenerNivelJerarquico();
	}
}
