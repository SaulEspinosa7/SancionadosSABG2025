using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Catalogos;
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
			   IdSexoFK = datosGenerales.IdSexoFk
			};

			return registroDatosGenerales;
		}
	}

	public class EmpleoCargoComisionService : IEmpleoCargoComision
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public EmpleoCargoComisionService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(EmpleoCargoComision datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}		

		public async Task<RegistroDatosGenerales> FromDatosGenerales(EmpleoCargoComision datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class OrigenProcedimientoService : IOrigenProcedimiento
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public OrigenProcedimientoService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(OrigenProcedimiento datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(OrigenProcedimiento datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class NivelJerarquicoService : INivelJerarquico
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public NivelJerarquicoService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(NivelJerarquico datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(NivelJerarquico datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class FaltaCometidaService : IFaltaCometida
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public FaltaCometidaService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(FaltaCometida datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(FaltaCometida datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class ResolucionService : IResolucion
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public ResolucionService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(Resolucion datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(Resolucion datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class TipoSancionService : ITipoSancion
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public TipoSancionService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(TipoSancion datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(TipoSancion datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class SuspensionService : ISuspension
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public SuspensionService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(Suspension datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(Suspension datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class DestitucionEmpleoService : IDestitucionEmpleo
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public DestitucionEmpleoService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(DestitucionEmpleo datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(DestitucionEmpleo datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class SancionEconomicaService : ISancionEconomica
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public SancionEconomicaService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(SancionEconomica datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(SancionEconomica datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class SancionEfectivamenteCobradaService : ISancionEfectivamenteCobrada
	{
		private readonly HttpClient _http;

		public SancionEfectivamenteCobradaService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(SancionEfectivamenteCobrada datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(SancionEfectivamenteCobrada datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class InhabilitacionService : IInhabilitacion
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public InhabilitacionService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(Inhabilitacion datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(Inhabilitacion datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}

	public class OtroService : IOtro
	{
		private readonly HttpClient _http;
		private readonly string _url;

		public OtroService(HttpClient http, IConfiguration config)
		{
			_http = http;
		}

		public async Task<RespondeDatosGenerales> AgregarDatoaGenerales(Otro datosGenerales)
		{
			var registroFalta = await FromDatosGenerales(datosGenerales);
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"EmpleoCargoComision/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
				return null;

			var result = await response.Content.ReadFromJsonAsync<RespondeDatosGenerales>();
			return result;
		}

		public async Task<RegistroDatosGenerales> FromDatosGenerales(Otro datosGenerales)
		{
			//RegistroDatosGenerales registroDatosGenerales = new()
			//{
			//	Nombres = datosGenerales.Nombres,
			//	PrimerApellido = datosGenerales.PrimerApellido,
			//	SegundoApellido = datosGenerales.SegundoApellido,
			//	Curp = datosGenerales.Curp,
			//	Rfc = datosGenerales.Rfc,
			//	IdSexoFK = datosGenerales.IdSexo
			//};

			return null;
		}
	}
}
