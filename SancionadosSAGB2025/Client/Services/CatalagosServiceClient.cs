using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Client.Interfaces;
using SancionadosSAGB2025.Shared.Catalogos;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Client.Services
{
	public class CatalagosServiceClient : ICatalagosClient
	{
		private readonly HttpClient _http;

		public CatalagosServiceClient(HttpClient http)
		{
			_http = http;
		}

		public Task<List<AmbitoPublico>> ObtenerAmbitoPublico()
		{
			throw new NotImplementedException();
		}

		public Task<List<EntidadFederativaEntidad>> ObtenerEntidadFederativa()
		{
			throw new NotImplementedException();
		}

		public Task<List<FaltaCometidaEntidad>> ObtenerFaltaCometida()
		{
			throw new NotImplementedException();
		}

		public Task<List<NivelJerarquicoCat>> ObtenerNivelJerarquico()
		{
			throw new NotImplementedException();
		}

		public Task<List<NivelOrdenGobierno>> ObtenerNivelOrdenGobierno()
		{
			throw new NotImplementedException();
		}

		public Task<List<OrdenJurisdiccional>> ObtenerOrdenJurisdiccional()
		{
			throw new NotImplementedException();
		}

		public Task<List<OrigenProcedimientoCat>> ObtenerOrigenProcedimiento()
		{
			throw new NotImplementedException();
		}

		public Task<List<Sexo>> ObtenerSexo()
		{
			throw new NotImplementedException();
		}

		public Task<List<TipoAmonestacion>> ObtenerTipoAmonestacion()
		{
			throw new NotImplementedException();
		}

		public Task<List<TipoSancion>> ObtenerTipoSancion()
		{
			throw new NotImplementedException();
		}

		public Task<List<TipoVialidad>> ObtenerTipoVialidad()
		{
			throw new NotImplementedException();
		}

		public async Task<Catalogos> ObtenerTodosLosCatalogos()
		{
			var response = await _http.GetAsync($"api/Catalogos/ObtenerTodosLosCatalogos");

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<Catalogos>();
			return result;
		}
	}
}
