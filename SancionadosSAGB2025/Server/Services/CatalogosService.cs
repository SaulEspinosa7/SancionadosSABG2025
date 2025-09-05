using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Registros;

namespace SancionadosSAGB2025.Server.Services
{
	public class CatalogosService : ICatalogos
	{
		private readonly HttpClient _http;

		public CatalogosService(HttpClient http)
		{
			_http = http;
		}

		public async Task<Catalogos> ObtenerTodosLosCatalogos()
		{
			try
			{
				Catalogos catalogos = new();

				catalogos.Sexo = await ObtenerSexo();
				catalogos.EntidadFederativas = await ObtenerEntidadFederativa();
				catalogos.NivelOrdenGobierno = await ObtenerNivelOrdenGobierno();
				catalogos.AmbitoPublico = await ObtenerAmbitoPublico();
				catalogos.FaltaCometidas = await ObtenerFaltaCometida();
				catalogos.NivelJerarquico = await ObtenerNivelJerarquico();
				catalogos.OrdenJurisdiccional = await ObtenerOrdenJurisdiccional();
				catalogos.OrigenProcedimiento = await ObtenerOrigenProcedimiento();
				catalogos.TipoAmonestacion = await ObtenerTipoAmonestacion();
				catalogos.TipoSancion = await ObtenerTipoSancion();
				catalogos.TipoVialidad = await ObtenerTipoVialidad();
				catalogos.Monedas = await ObtenerMonedas();
				catalogos.Paises = await ObtenerPaises();
                return catalogos;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta de los catalogos. {ex.Message}");
				return null;
			}
		}

		public async Task<List<MonedaCat>> ObtenerMonedas()
		{
			try
			{
				var response = await _http.GetAsync($"Moneda");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<MonedaCat>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Ambito Publico. {ex.Message}");
				return null;
			}

		}

		public async Task<List<AmbitoPublico>> ObtenerAmbitoPublico()
		{
			try
			{
				var response = await _http.GetAsync($"AmbitoPublico");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<AmbitoPublico>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Ambito Publico. {ex.Message}");
				return null;
			}
			
		}		

		public async Task<List<EntidadFederativaEntidad>> ObtenerEntidadFederativa()
		{
			try
			{
				var response = await _http.GetAsync($"EntidadFederativaCat");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<EntidadFederativaEntidad>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Entidad Federativa. {ex.Message}");
				return null;
			}		
		}

		public async Task<List<FaltaCometidaEntidad>> ObtenerFaltaCometida()
		{
			try
			{
				var response = await _http.GetAsync($"FaltaCometida");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<FaltaCometidaEntidad>>();
				return result.Where(f => f.Activo == 1).ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Falta Cometida. {ex.Message}");
				return null;
			}		
		}

		public async Task<List<NivelJerarquicoCat>> ObtenerNivelJerarquico()
		{
			try
			{
				var response = await _http.GetAsync($"NivelJerarquico");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<NivelJerarquicoCat>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Nivel Jerarquico. {ex.Message}");
				return null;
			}
		
		}

		public async Task<List<NivelOrdenGobierno>> ObtenerNivelOrdenGobierno()
		{
			try
			{
				var response = await _http.GetAsync($"NivelOrdenGobierno");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<NivelOrdenGobierno>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Nivel Orden Gobierno. {ex.Message}");
				return null;
			}		
		}

		public async Task<List<OrdenJurisdiccional>> ObtenerOrdenJurisdiccional()
		{
			try
			{
				var response = await _http.GetAsync($"OrdenJurisdiccionalCat");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<OrdenJurisdiccional>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Orden Jurisdiccional. {ex.Message}");
				return null;
			}
			
		}

		public async Task<List<OrigenProcedimientoCat>> ObtenerOrigenProcedimiento()
		{
			try
			{
				var response = await _http.GetAsync($"OrigenProcedimientoCat");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<OrigenProcedimientoCat>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Origen Procedimiento {ex.Message}");
				return null;
			}
		
		}

		public async Task<List<Sexo>> ObtenerSexo()
		{
			try
			{
				var response = await _http.GetAsync($"Sexo");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<Sexo>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Sexo {ex.Message}");
				return null;
			}			
		}
		public async Task<List<PaisCat>> ObtenerPaises()
		{
			try
			{
				var response = await _http.GetAsync($"Paises");
				if (!response.IsSuccessStatusCode)
					return null;
				var result = await response.Content.ReadFromJsonAsync<List<PaisCat>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago paises {ex.Message}");
				return null;
			}
        }

        public async Task<List<TipoAmonestacion>> ObtenerTipoAmonestacion()
		{
			try
			{
				var response = await _http.GetAsync($"TipoAmonestacion");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<TipoAmonestacion>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Tipo Amonestacion {ex.Message}");
				return null;
			}
			
		}

		public async Task<List<TipoSancion>> ObtenerTipoSancion()
		{
			try
			{
				var response = await _http.GetAsync($"TipoSancionCat");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<TipoSancion>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Tipo Sancion {ex.Message}");
				return null;
			}
			
		}

		public async Task<List<TipoVialidad>> ObtenerTipoVialidad()
		{
			try
			{
				var response = await _http.GetAsync($"TipoVialidad");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<TipoVialidad>>();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Tipo Vialidad {ex.Message}");
				return null;
			}		
		}
	}
}
