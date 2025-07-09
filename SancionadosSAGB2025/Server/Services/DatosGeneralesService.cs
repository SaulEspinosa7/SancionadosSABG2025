using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Server.Services
{
	public class DatosGeneralesService : IDatosGenerales
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public DatosGeneralesService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(DatosGenerales datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"DatosGenerales/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(DatosGenerales datosGenerales)
		{
			RegistroDatosGenerales registroDatosGenerales = new(){
			   Nombres = datosGenerales.Nombres,
			   PrimerApellido = datosGenerales.PrimerApellido,
			   SegundoApellido = datosGenerales.SegundoApellido,
			   Curp = datosGenerales.Curp,
			   Rfc = datosGenerales.Rfc,
			   IdSexoFK = datosGenerales.IdSexo
			};

			return registroDatosGenerales;
		}
	}
}
