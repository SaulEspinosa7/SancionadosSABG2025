using SancionadosSAGB2025.Client.Services;
using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Registros;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SancionadosSAGB2025.Server.Services
{
	public class CatalogosService(HttpClient _http, IConfiguration configuration) : ICatalogos
	{
		//private readonly HttpClient _http;

		//public CatalogosService(HttpClient http)
		//{
		//	_http = http;
		//}

		public async Task<Catalogos> ObtenerTodosLosCatalogos( string token)
		{
			try
			{
				Catalogos catalogos = new();

				catalogos.Sexo = await ObtenerSexo(token);
				catalogos.EntidadFederativas = await ObtenerEntidadFederativa(token);
				catalogos.NivelOrdenGobierno = await ObtenerNivelOrdenGobierno(token);
				catalogos.AmbitoPublico = await ObtenerAmbitoPublico(token);
				catalogos.FaltaCometidas = await ObtenerFaltaCometida(token);
				catalogos.NivelJerarquico = await ObtenerNivelJerarquico(token);
				catalogos.OrdenJurisdiccional = await ObtenerOrdenJurisdiccional(token);
				catalogos.OrigenProcedimiento = await ObtenerOrigenProcedimiento(token);
				catalogos.TipoAmonestacion = await ObtenerTipoAmonestacion(token);
				catalogos.TipoSancion = await ObtenerTipoSancion(token);
				catalogos.TipoVialidad = await ObtenerTipoVialidad(token);
				catalogos.Monedas = await ObtenerMonedas(token);
				catalogos.Paises = await ObtenerPaises(token);
                return catalogos;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta de los catalogos. {ex.Message}");
				return null;
			}
		}

		public async Task<List<MonedaCat>> ObtenerMonedas(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "Moneda");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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

        public async Task<List<AmbitoPublico>> ObtenerAmbitoPublico(string token)
        {
            try
            {                            

                var request = new HttpRequestMessage(HttpMethod.Get, "AmbitoPublico");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<List<AmbitoPublico>>();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hubo un error al momento de realizar la consulta del catálogo Ambito Publico. {ex.Message}");
                return null;
            }
        }


        public async Task<List<EntidadFederativaEntidad>> ObtenerEntidadFederativa(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "EntidadFederativaCat");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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

		public async Task<List<FaltaCometidaEntidad>> ObtenerFaltaCometida(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "FaltaCometida");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<List<FaltaCometidaEntidad>>();
                return result;
               
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Falta Cometida. {ex.Message}");
				return null;
			}		
		}

		public async Task<List<NivelJerarquicoEntidad>> ObtenerNivelJerarquico(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "NivelJerarquico");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<List<NivelJerarquicoEntidad>>();
                return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Nivel Jerarquico. {ex.Message}");
				return null;
			}
		
		}

		public async Task<List<NivelOrdenGobierno>> ObtenerNivelOrdenGobierno(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "NivelOrdenGobierno");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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

		public async Task<List<OrdenJurisdiccional>> ObtenerOrdenJurisdiccional(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "OrdenJurisdiccionalCat");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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

		public async Task<List<OrigenProcedimientoEntidad>> ObtenerOrigenProcedimiento(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "OrigenProcedimientoCat");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<List<OrigenProcedimientoEntidad>>();
                return result;
               
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de realizar la consulta del catalago Origen Procedimiento {ex.Message}");
				return null;
			}
		
		}

		public async Task<List<Sexo>> ObtenerSexo(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "Sexo");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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
		public async Task<List<PaisCat>> ObtenerPaises(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "Paises");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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

        public async Task<List<TipoAmonestacion>> ObtenerTipoAmonestacion(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "TipoAmonestacion");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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

		public async Task<List<TipoSancion>> ObtenerTipoSancion(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "TipoSancionCat");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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

		public async Task<List<TipoVialidad>> ObtenerTipoVialidad(string token)
		{
			try
			{
                var request = new HttpRequestMessage(HttpMethod.Get, "TipoVialidad");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _http.SendAsync(request);

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
		public async Task<RespuestaApiActualizar> ActualizarAmbitoPublico(AmbitoPublico ambitoPublico)
		{
			try
			{
                var data = new AmbitoPublico { IdAmbitoPublico = ambitoPublico.IdAmbitoPublico, Descripcion = ambitoPublico.Descripcion};
                var dataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                HttpContent dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {ambitoPublico.Token}");
                var url = configuration["UrlApi:ActualizarAmbito"];

                var response = await _http.PostAsync(url, dataContent);
                var responsestring = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<RespuestaApiActualizar>(responsestring);
                return res!;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al intentar actualizar {ex.Message}");
				return null;
			}
		}
		public async Task<RespuestaApiActualizar> ActualizarEntidadFederativa(EntidadFederativaEntidad entidadFederativaEntidad)
		{
			try
			{
                var data = new EntidadFederativaEntidad { IdEntidadFederativa = entidadFederativaEntidad.IdEntidadFederativa, Descripcion = entidadFederativaEntidad.Descripcion};
                var dataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                HttpContent dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {entidadFederativaEntidad.Token}");
                var url = configuration["UrlApi:ActualizarEntidad"];

                var response = await _http.PostAsync(url, dataContent);
                var responsestring = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<RespuestaApiActualizar>(responsestring);
                return res!;
			}
			catch (Exception ex)
			{
                Console.WriteLine($"Hubo un error al intentar actualizar {ex.Message}");
                return null;
			}
		}
		public async Task<RespuestaApiActualizar> ActualizarFaltaCometida(FaltaCometidaEntidad faltaCometidaEntidad)
		{
			try
			{
                var data = new FaltaCometidaEntidad { IdFaltaCometida = faltaCometidaEntidad.IdFaltaCometida, Descripcion = faltaCometidaEntidad.Descripcion, Activo= faltaCometidaEntidad.Activo};
                var dataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                HttpContent dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {faltaCometidaEntidad.Token}");
                var url = configuration["UrlApi:ActualizarFaltaCometida"];

                var response = await _http.PutAsync(url, dataContent);
                var responsestring = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<RespuestaApiActualizar>(responsestring);
                return res!;
			}
			catch (Exception ex)
			{
                Console.WriteLine($"Hubo un error al intentar actualizar {ex.Message}");
                return null;
			}
		}
		public async Task<RespuestaApiActualizar> ActualizarNivelJerarquico(NivelJerarquicoEntidad nivelJerarquicoEntidad)
		{
			try
			{
                var data = new  { Id = nivelJerarquicoEntidad.IdNivelJerarquicoCat, Clave = nivelJerarquicoEntidad.Descripcion, Activo = nivelJerarquicoEntidad.Activo};
                var dataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                HttpContent dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {nivelJerarquicoEntidad.Token}");
                var url = configuration["UrlApi:ActualizarNivelJerarquico"];

                var response = await _http.PostAsync(url, dataContent);
                var responsestring = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<RespuestaApiActualizar>(responsestring);
                return res!;
			}
			catch (Exception ex)
			{
                Console.WriteLine($"Hubo un error al intentar actualizar {ex.Message}");
                return null;
			}
		}
		public async Task<RespuestaApiActualizar> ActualizarOrdenGobierno(NivelOrdenGobierno nivelOrdenGobierno)
		{
			try
			{
                var data = new NivelOrdenGobierno { IdNivelOrdenGobierno = nivelOrdenGobierno.IdNivelOrdenGobierno, Descripcion = nivelOrdenGobierno.Descripcion};
                var dataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                HttpContent dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {nivelOrdenGobierno.Token}");
                var url = configuration["UrlApi:ActualizarOrdenGobierno"];

                var response = await _http.PostAsync(url, dataContent);
                var responsestring = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<RespuestaApiActualizar>(responsestring);
                return res!;
			}
			catch (Exception ex)
			{
                Console.WriteLine($"Hubo un error al intentar actualizar {ex.Message}");
                return null;
			}
		}
		public async Task<RespuestaApiActualizar> ActualizaOrdenJurisdiccional(OrdenJurisdiccional ordenJurisdiccional)
		{
			try
			{
                var data = new OrdenJurisdiccional { Id = ordenJurisdiccional.Id, Descripcion = ordenJurisdiccional.Descripcion};
                var dataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                HttpContent dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {ordenJurisdiccional.Token}");
                var url = configuration["UrlApi:ActualizarOrdenGobierno"];

                var response = await _http.PostAsync(url, dataContent);
                var responsestring = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<RespuestaApiActualizar>(responsestring);
                return res!;
			}
			catch (Exception ex)
			{
                Console.WriteLine($"Hubo un error al intentar actualizar {ex.Message}");
                return null;
			}
		}
	}
}
