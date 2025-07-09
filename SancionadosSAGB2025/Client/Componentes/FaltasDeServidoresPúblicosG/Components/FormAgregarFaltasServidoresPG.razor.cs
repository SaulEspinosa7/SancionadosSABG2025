using MudBlazor;
using SancionadosSAGB2025.Client.Shared.Partial.Dialog;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SancionadosSAGB2025.Client.Componentes.FaltasDeServidoresPúblicosG.Components
{
	partial class FormAgregarFaltasServidoresPG
	{
		private int ActivePanel { set; get; } = 0; // 0: Expediente, 1: Datos Generales, etc.

		private FaltasDeServidoresPublicosG FaltasDeServidoresPublicosG { set; get; } = new();
		private List<int> BloqueoSesiones { get; set; } = new();	
		private RespuestaRegistro RespuestaRegistro { set; get; } = new();
		private Catalogos CatalogosBD { get; set; } = new();
		private List<EntidadFederativa> EntidadesFederativas { get; set; } = new();
		private List<NivelOrdenGobierno> NivelOrdenGobierno { get; set; } = new();
		private List<AmbitoPublico> AmbitoPublico { get; set; } = new();
		private List<OrdenJurisdiccional> OrdenJurisdiccional { get; set; } = new();
		private List<SancionadosSAGB2025.Shared.Catalogos.NivelJerarquico> NivelJerarquico { get; set; } = new();
		private List<SancionadosSAGB2025.Shared.Catalogos.FaltaCometida> FaltasCometidas { get; set; } = new();
		private string _value { get; set; } = string.Empty;
		private IEnumerable<string> _options { get; set; } = new HashSet<string>();
		protected override async Task OnInitializedAsync()
		{
			await ObtenerCatalogosFormulario();
		}

		private async Task ObtenerCatalogosFormulario()
		{
			try
			{
				var result = await CatalagosService.ObtenerTodosLosCatalogos();

				if (result is not null)
				{
					CatalogosBD = result;
					if (CatalogosBD is not null)
					{
						EntidadesFederativas = CatalogosBD.EntidadFederativas!;
						NivelOrdenGobierno = CatalogosBD.NivelOrdenGobierno!;
						AmbitoPublico = CatalogosBD.AmbitoPublico!;
						OrdenJurisdiccional = CatalogosBD.OrdenJurisdiccional!;
						NivelJerarquico = CatalogosBD.NivelJerarquico!;
						FaltasCometidas = CatalogosBD.FaltaCometidas!;
					}
				}
				else
				{
					Snackbar.Add($"Error al consultar los catalogos", Severity.Error);
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add($"Error en el proceso {ex.Message}", Severity.Error);
				//Snackbar.Add("Se guardó la información previa.", Severity.Success);
				//RespuestaRegistro = result!;
			}
		}

		private List<SancionadosSAGB2025.Shared.Sanciones.Sexo> Sexos { get; set; } = new()
		{
			new SancionadosSAGB2025.Shared.Sanciones.Sexo { Clave = "2", Valor = "Mujer" },
			new SancionadosSAGB2025.Shared.Sanciones.Sexo { Clave = "1", Valor = "Hombre" }
		};		

		private List<SancionadosSAGB2025.Shared.Sanciones.CargoComision> CargosComision { get; set; } = new()
		{
			new CargoComision { Clave = "OPERATIVO_HOMOLOGO", Valor = "Operativo Homólogo" },
			new CargoComision { Clave = "ENLACE_HOMOLOGO", Valor = "Enlace Homólogo" },
			new CargoComision { Clave = "JEFATURA_DEPTO_HOMOLOGO", Valor = "Jefatura de Departamento Homólogo" },
			new CargoComision { Clave = "SUBDIRECCION_HOMOLOGO", Valor = "Subdirección Homólogo" },
			new CargoComision { Clave = "DIRECCION_HOMOLOGO", Valor = "Dirección Homólogo" },
			new CargoComision { Clave = "DG_HOMOLOGO", Valor = "Dirección General Homólogo" },
			new CargoComision { Clave = "JEFATURA_UNIDAD_HOMOLOGO", Valor = "Jefatura de Unidad Homólogo" },
			new CargoComision { Clave = "SUBSECRETARIA_HOMOLOGO", Valor = "Subsecretaría Homólogo" },
			new CargoComision { Clave = "SECRETARIA_HOMOLOGO", Valor = "Secretaría Homólogo" },
			new CargoComision { Clave = "OTRO", Valor = "Otro" }
		};

		private List<SancionadosSAGB2025.Shared.Sanciones.Origenes> ListaOrigenesInvestigacion { get; set; } = new()
		{
			new Origenes { Clave = "ASF_ENTIDADES_FISCALIZACION", Valor = "ASF o Entidades de Fiscalización" },
			new Origenes { Clave = "AUDITORIA_OIC", Valor = "Auditoría del Órgano Interno de Control (OIC)" },
			new Origenes { Clave = "DENUNCIA", Valor = "Denuncia" },
			new Origenes { Clave = "DE_OFICIO", Valor = "De oficio" },
			new Origenes { Clave = "OTRO", Valor = "Otro" }
		};

		private List<TipoSancionClave> TipoSancionClaves { get; set; } = new List<TipoSancionClave>
		{
			new TipoSancionClave { Clave = "SUSPENSION", Valor = "SUSPENSIÓN" },
			new TipoSancionClave { Clave = "DESTITUCION", Valor = "DESTITUCIÓN" },
			new TipoSancionClave { Clave = "SANCION_ECONOMICA", Valor = "SANCIÓN ECONÓMICA" },
			new TipoSancionClave { Clave = "INHABILITACION", Valor = "INHABILITACIÓN" },
			new TipoSancionClave { Clave = "OTRO", Valor = "OTRO" }
		};

		private List<Moneda> TipoMonedas { get; set; } = new List<Moneda>
		{
			new Moneda { Clave = "MXN", Valor = "Peso mexicano" },
			new Moneda { Clave = "USD", Valor = "Dólar estadounidense" },
			new Moneda { Clave = "EUR", Valor = "Euro" },
			new Moneda { Clave = "GBP", Valor = "Libra esterlina" },
			new Moneda { Clave = "JPY", Valor = "Yen japonés" },
			new Moneda { Clave = "CAD", Valor = "Dólar canadiense" },
			new Moneda { Clave = "BRL", Valor = "Real brasileño" },
			new Moneda { Clave = "CNY", Valor = "Yuan chino" },
			new Moneda { Clave = "CHF", Valor = "Franco suizo" },
			new Moneda { Clave = "AUD", Valor = "Dólar australiano" }
		};

		private List<PlazoPago> PlazoPagos { get; set; } = new List<PlazoPago>
		{
			new PlazoPago { Clave = "1", Valor = "Años" },
			new PlazoPago { Clave = "2", Valor = "Meses" },
			new PlazoPago { Clave = "3", Valor = "Dias" },
		};

		private async Task MostrarSiguientePanel(int posicionPanel)
		{
			ActivePanel = posicionPanel;
			ActivePanel++;
			BloqueoSesiones.Add(ActivePanel);
			if (RespuestaRegistro is not null) 
			{
				if (RespuestaRegistro!.Sancion is null)
				{
					await GuardarInformacionDeFaltasTemporales();								
				}
				else 
				{
					await ActualizarInformacionDeFaltasTemporales();
				}				
			}		
		}

		private async Task MostrarAnteriorPanel(int posicionPanel)
		{
			ActivePanel = posicionPanel;
			ActivePanel--;
			//await GuardarInformacionDeFaltasTemporales();
		}

		public async Task GuardarInformacionDeFaltasTemporales() 
		{
			try
			{
				var result = await FaltasSPGService.AgregarFaltasSPG(FaltasDeServidoresPublicosG);

				if (result.Sancion!.idFaltasServidoresPG > 0)
				{
					Snackbar.Add("Se guardó la información previa.", Severity.Success);
					RespuestaRegistro = result!;
				}
				else 
				{
					Snackbar.Add("Hubo un error al guardar la información previa.", Severity.Error);
					//Snackbar.Add("Se guardó la información previa.", Severity.Success);
					RespuestaRegistro = result!;
					Console.WriteLine($"hubo un error.");
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add($"Error en el proceso {ex.Message}", Severity.Error);
				//Snackbar.Add("Se guardó la información previa.", Severity.Success);
				//RespuestaRegistro = result!;
			}
		}

		public async Task ActualizarInformacionDeFaltasTemporales()
		{
			try
			{
				FaltasDeServidoresPublicosG.idFaltasServidoresPG = RespuestaRegistro.Sancion!.idFaltasServidoresPG;
				FaltasDeServidoresPublicosG.DatosGenerales!.IdSexo = Convert.ToInt32(FaltasDeServidoresPublicosG.DatosGenerales.Sexo?.Clave);
				FaltasDeServidoresPublicosG.IdEntidadFederativaFK = FaltasDeServidoresPublicosG.EmpleoCargoComision?.EntidadFederativa?.IdEntidadFederativa;
				FaltasDeServidoresPublicosG.IdNivelOrdenGobiernoFK = FaltasDeServidoresPublicosG.EmpleoCargoComision?.NivelOrdenGobierno?.IdNivelOrdenGobierno;
				FaltasDeServidoresPublicosG.IdAmbitoPublicoFK = FaltasDeServidoresPublicosG.EmpleoCargoComision?.AmbitoPublico?.IdAmbitoPublico;

				FaltasDeServidoresPublicosG.IdNivelJerarquicoCatFK = FaltasDeServidoresPublicosG.NivelJerarquico?.Clave?.IdNivelJerarquicoCat;
				FaltasDeServidoresPublicosG.IdFaltaCometidaCatFK = FaltasDeServidoresPublicosG.FaltaCometida?.Clave?.IdFaltaCometida;
				FaltasDeServidoresPublicosG.IdOrdenJurisdiccionalFK = FaltasDeServidoresPublicosG.Resolucion?.OrdenJurisdiccional?.IdOrdenJurisdiccional;
				//FaltasDeServidoresPublicosG.IdOrigenProcedimientoFK = FaltasDeServidoresPublicosG.OrigenProcedimiento.Clave.Valor;
				//FaltasDeServidoresPublicosG.IdTipoSancionCatFK = FaltasDeServidoresPublicosG.TipoSancion.;
				var result = await FaltasSPGService.ActualizarFaltasSPG(FaltasDeServidoresPublicosG);

				if (result.Sancion!.idFaltasServidoresPG > 0)
				{
					Snackbar.Add("Se guardó la información previa.", Severity.Success);
					//RespuestaRegistro = result!;
					FaltasDeServidoresPublicosG.IdDatosGeneralesFK = result.Sancion.IdDatosGeneralesFK;
				}
				else
				{
					Snackbar.Add("Hubo un error al guardar la información previa.", Severity.Error);
					//Snackbar.Add("Se guardó la información previa.", Severity.Success);
					//RespuestaRegistro = result!;
					Console.WriteLine($"hubo un error.");
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add($"Error en el proceso {ex.Message}", Severity.Error);
				//Snackbar.Add("Se guardó la información previa.", Severity.Success);
				//RespuestaRegistro = result!;
			}
		}

		private async Task OpenDialogAsync()
		{
			var options = new DialogOptions { CloseOnEscapeKey = false, CloseButton = false, MaxWidth = MaxWidth.Small, BackdropClick = false };

			//return DialogService.ShowAsync<ModalFinalizarRegistroFaltasSPG>("Finalizar registro", options);

			var dialog = await DialogService.ShowAsync<ModalFinalizarRegistroFaltasSPG>("Finalizar registro", options);
			var result = await dialog.Result;

			if (!result.Canceled)
			{
				await ActualizarInformacionDeFaltasTemporales();
			}
		}

		private async Task MostrarModalDeCancelarRegistro()
		{
			var options = new DialogOptions { CloseOnEscapeKey = false, CloseButton = false, MaxWidth = MaxWidth.Small, BackdropClick = false };

			var dialog = await DialogService.ShowAsync<ModalCancelarRegistroFaltasSPG>("Cancelar registro", options);
			var result = await dialog.Result;

			if (!result.Canceled)
			{
				Console.WriteLine($"Data  - {result.Data}");
				bool responde = (bool)result.Data!;
				if (responde) FaltasDeServidoresPublicosG.Activo = 0;
				else FaltasDeServidoresPublicosG.Activo = 1;
				await ActualizarInformacionDeFaltasTemporales();
			}
		}


	}
}
