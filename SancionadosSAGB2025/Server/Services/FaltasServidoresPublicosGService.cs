using Microsoft.AspNetCore.Diagnostics;
using SancionadosSAGB2025.Server.Interfaces;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Text;
using System.Text.Json;

namespace SancionadosSAGB2025.Server.Services
{
	public class FaltasServidoresPublicosGService : IFaltasServidoresPublicosG
	{
		private readonly HttpClient _http;


		public FaltasServidoresPublicosGService(HttpClient http)
		{
			_http = http;
		}

		public async Task<RespuestaRegistro> ActualizarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{
			try
			{
				var UpdateFalta = await FromUpdateFaltasDeServidoresPublicosG(faltasDeServidoresPublicosG);
				var json = JsonSerializer.Serialize(UpdateFalta, new JsonSerializerOptions
				{
					WriteIndented = true // Opcional: para que sea más legible
				});
				Console.WriteLine(json); // o usa tu logger

				// Luego lo mandas tú mismo
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				//var registroFalta = new RegistroFaltasSPG();
				var response = await _http.PostAsJsonAsync($"FALTASSPG/AddAsync", UpdateFalta);

				if (!response.IsSuccessStatusCode)
				{
					var errorContent = await response.Content.ReadAsStringAsync();
					Console.WriteLine(errorContent);
				}

				var result = await response.Content.ReadFromJsonAsync<RespuestaRegistro>();

				if (result?.Mensaje?.Contains("REGISTRO ELIMINADO CORRECTAMENTE") == true) 
				{
					result.Response = true;
				}
				//result.Sancion = UpdateFalta;
				//result.Sancion = new();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ha ocurrio un error al momento de realizar la actualización del Expediente {faltasDeServidoresPublicosG.Sancion!.Expediente} - {ex.Message}");
				return null;
			}
		}

		public async Task<RespuestaRegistro> AgregarFaltasSPG(FaltasDeServidoresPublicosG faltasDeServidoresPublicosG)
		{

			var registroFalta = await FromFaltasDeServidoresPublicosG(faltasDeServidoresPublicosG);
			var json = JsonSerializer.Serialize(registroFalta, new JsonSerializerOptions
			{
				WriteIndented = true // Opcional: para que sea más legible
			});

			Console.WriteLine(json); // o usa tu logger

			// Luego lo mandas tú mismo
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			//var registroFalta = new RegistroFaltasSPG();
			var response = await _http.PostAsJsonAsync($"FALTASSPG/AddAsync", registroFalta);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine(errorContent);
			}
			//return await response.Content.ReadFromJsonAsync<RespuestaRegistro>();

			var result = await response.Content.ReadFromJsonAsync<RespuestaRegistro>();
			return result;
		}		

		public async Task<AddFaltasDeServidoresPublicosG> FromFaltasDeServidoresPublicosG(FaltasDeServidoresPublicosG faltas)
		{
			AddFaltasDeServidoresPublicosG registroAdd = new();

			registroAdd.Fecha = faltas?.Sancion?.Fecha;
			registroAdd.Expediente = faltas?.Sancion?.Expediente;

			registroAdd.DatosGenerales = new DatosGenerales
			{
				Nombres = "",
				PrimerApellido = "",
				// los campos requeridos mínimos...
			};

			// Fechas del sistema
			registroAdd.FechaCreacion = DateTime.Now;
			registroAdd.FechaModificacion = DateTime.Now;
			registroAdd.Activo = 1;
			registroAdd.IdUsuarioFK = faltas.IdUsuarioFK;

			return registroAdd;
		}

		public async Task<AddFaltasDeServidoresPublicosG> FromUpdateFaltasDeServidoresPublicosG(FaltasDeServidoresPublicosG faltas)
		{
			AddFaltasDeServidoresPublicosG registroAdd = new();

			registroAdd.Id = faltas.Id;

			registroAdd.Fecha = faltas?.Sancion?.Fecha;
			registroAdd.Expediente = faltas?.Sancion?.Expediente;

			//Datos generales
			registroAdd.IdDatosGeneralesFK = faltas?.DatosGenerales?.IdDatosGenerales;
			registroAdd.DatosGenerales = faltas?.DatosGenerales;
			//registroAdd.DatosGenerales.IdSexoFk = faltas?.DatosGenerales?.Sexo?.IdSexo;

			// Empleo, cargo o comisión
			registroAdd.IdEmpleoCargoComisionFK = faltas?.EmpleoCargoComision?.Id;
			registroAdd.EmpleoCargoComision = faltas?.EmpleoCargoComision;

			// Nivel jerárquico
			registroAdd.IdNivelJerarquicoFK = faltas?.NivelJerarquico?.Id;
			registroAdd.NivelJerarquico = faltas?.NivelJerarquico;

			// Origen del procedimiento
			registroAdd.IdOrigenProcedimientoFK = faltas?.OrigenProcedimiento?.Id;
			registroAdd.OrigenProcedimiento = faltas?.OrigenProcedimiento;

			// Falta cometida
			registroAdd.IdFaltaCometidaFK = faltas?.FaltaCometida?.Id;
			registroAdd.FaltaCometida = faltas?.FaltaCometida;

			// Resolución
			registroAdd.IdResolucion = faltas?.Resolucion?.Id;
			registroAdd.Resolucion = faltas?.Resolucion;

			// Tipo de sanción
			registroAdd.IdTipoSancionFK = faltas?.TipoSancion?.IdTipoSancionCat == 0 ? null : faltas?.TipoSancion?.IdTipoSancionCat;
			registroAdd.MultipleSancion = faltas?.MultipleSancion;
			//registroAdd.IdTipoSancionFK = faltas?.TipoSancion?.IdTipoSancion;

			//Suspensión
			registroAdd.IdSuspension = faltas?.Suspension?.Id;
			registroAdd.Suspension = faltas?.Suspension;

			// Destitución de empleo
			registroAdd.IdDestitucionEmpleo = faltas?.DestitucionEmpleo?.Id;
			registroAdd.DestitucionEmpleo = faltas?.DestitucionEmpleo;

			// Sanción económica
			registroAdd.IdSancionEconomicaFK = faltas?.SancionEconomica?.Id;
			registroAdd.SancionEconomica = faltas?.SancionEconomica;

			// Sanción efectivamente cobrada
			registroAdd.IdSancionEfectivamenteCobradaFK = faltas?.SancionEfectivamenteCobrada?.Id;
			registroAdd.SancionEfectivamenteCobrada = faltas?.SancionEfectivamenteCobrada;

			// Inhabilitación
			registroAdd.IdInhabilitacionFK = faltas?.Inhabilitacion?.Id;
			registroAdd.Inhabilitacion = faltas?.Inhabilitacion;

			// Otro
			registroAdd.IdOtro = faltas?.Otro?.Id;
			registroAdd.Otro = faltas?.Otro;

			// Fechas del sistema
			registroAdd.FechaCreacion = DateTime.Now;
			registroAdd.FechaModificacion = DateTime.Now;
			registroAdd.Activo = faltas?.Activo;
			registroAdd.IdUsuarioFK = faltas?.IdUsuarioFK;

			return registroAdd;

		}

		public async Task<List<AddFaltasDeServidoresPublicosG>> ObtenerFaltasSPG(SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG)
		{
			try
			{
				var response = await _http.GetAsync($"FALTASSPG");

				if (!response.IsSuccessStatusCode)
					return null;

				var result = await response.Content.ReadFromJsonAsync<List<AddFaltasDeServidoresPublicosG>>();
				return result.Where(falta => falta.Activo == 1).ToList();
				//return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hubo un error al momento de obtener las Faltas de Servidores Publicos Graves. {ex.Message}");
				return null;
			}
		}
	}
}
