using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SancionadosSAGB2025.Client.Shared.Partial.Dialog;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Text.RegularExpressions;

namespace SancionadosSAGB2025.Client.Componentes.FaltasGravesPersonasFísicas.Componentes
{
	partial class FormAgregarFaltasGravesPersonasFisicas
	{
		[Parameter] public AddFaltasGravesPersonasFisicas FaltasDeServidoresPublicosG { set; get; } = new();
		[Parameter] public int IsEditMode { set; get; } = (int)TipoVistaComponentes.Agregar;
		[Inject] private NavigationManager Navigation { get; set; }
		private int ActivePanel { set; get; } = 0; // 0: Expediente, 1: Datos Generales, etc.
		private bool ResultadoCorrecto { set; get; } // 0: Expediente, 1: Datos Generales, etc.
		private List<int> ListActivePanel { set; get; } = new();
		private List<int> BloqueoSesiones { get; set; } = new();
		private RespuestaRegistroFaltasGravesPersonasFisicas RespuestaRegistro { set; get; } = new();
		private Catalogos CatalogosBD { get; set; } = new();
		private List<EntidadFederativa> EntidadesFederativas { get; set; } = new();
		private List<NivelOrdenGobierno> NivelOrdenGobierno { get; set; } = new();
		private List<AmbitoPublico> AmbitoPublico { get; set; } = new();
		private List<OrdenJurisdiccional> OrdenJurisdiccional { get; set; } = new();
		private List<NivelJerarquicoCat> NivelJerarquico { get; set; } = new();
		private List<FaltaCometidaCat> FaltasCometidas { get; set; } = new();
		private List<MonedaCat> TipoMonedas { get; set; } = new();
		private List<OrigenProcedimientoCat> ListaOrigenesInvestigacion { get; set; } = new();
		private List<Sexo> Sexos { get; set; } = new();
		private List<TipoSancion> TipoSancionClaves { get; set; } = new();

        private List<TipoVialidad> TiposVialidades { get; set; } = new();
		private List<PaisCat> Paises { get; set; } = new();
		private IEnumerable<string> _options { get; set; } = new HashSet<string>();
		private IEnumerable<string> _optionsFaltaComedia { get; set; } = new HashSet<string>();
		private Dictionary<SesionesFaltasGravesPersonasFisicas, bool> BloquearBotonSiguientePorSesion { get; set; } =
						Enum.GetValues(typeof(SesionesFaltasGravesPersonasFisicas))
							.Cast<SesionesFaltasGravesPersonasFisicas>()
							.ToDictionary(key => key, value => false); // o true si quieres bloquear todos inicialmente

		private TipoDomiclio TipoDomicilioSeleccionado { get; set; } = new ();
		private List<TipoDomiclio> TipoDomicilios { get; set; } = new ();
		private bool expediente { get; set; } = false;
		private bool inhabilitacion { get; set; } = false;
        protected override async Task OnInitializedAsync()
		{
			await ObtenerCatalogosFormulario();
			await MostrarOpcionCatalogos();
			await InicializarVariables();
		}
        void Fecha()
        {
            expediente = true;
        }

        private async Task InicializarVariables() 
		{
			TipoDomicilios = Enum.GetValues(typeof(TipoDomiclio)).Cast<TipoDomiclio>().ToList();
			FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico = new();
			FaltasDeServidoresPublicosG.DatosGenerales.DomicilioExtranjero = new();
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.IdTipoVialidadFK = 1; // Asignar un valor por defecto
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.IdEntidadFederativaFK = 1; // Asignar un valor por defecto
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.NombreVialidad = ""; // Asignar un valor por defecto
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.NumeroExterior = ""; // Asignar un valor por defecto
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.NumeroInterior = ""; // Asignar un valor por defecto
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.ColoniaLocalidad = ""; // Asignar un valor por defecto
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.MunicipioAlcaldia = ""; // Asignar un valor por defecto
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.CodigoPostal = ""; // Asignar un valor por defecto
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (IsEditMode != (int)TipoVistaComponentes.Agregar)
			{
				foreach (var key in BloquearBotonSiguientePorSesion.Keys.ToList())
				{
					BloquearBotonSiguientePorSesion[key] = true;
				}

                _options = FaltasDeServidoresPublicosG.MultipleSancion?
                            .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim())
                            .ToHashSet()!;

                _optionsFaltaComedia = FaltasDeServidoresPublicosG.FaltaCometida?.MultipleFalta?
						.Split(',', StringSplitOptions.RemoveEmptyEntries)
						.Select(s => s.Trim()) // elimina espacios antes/después
						.ToHashSet()!; // crea un HashSet<string>

				BloqueoSesiones = Enum.GetValues(typeof(SesionesFaltasGravesPersonasFisicas))
					.Cast<int>()
					.ToList();

			}
			//if (firstRender)
			//{
			//	objRef = DotNetObjectReference.Create(this);
			//	await JS.InvokeVoidAsync("registerBeforeUnload", objRef);
			//	await JS.InvokeVoidAsync("registerGeneralBeforeUnload", objRef);
			//}
		}

		[JSInvokable]
		public async Task OnBeforeUnload()
		{
			Console.WriteLine("F5 fue presionado");
			Console.WriteLine("El usuario está recargando o cerrando la página.");
			await MostrarModalDeCancelarRegistro();
			// Aquí puedes realizar alguna lógica, como mostrar un mensaje o guardar estado
		}

		private async Task MostrarOpcionCatalogos()
		{
			FaltasDeServidoresPublicosG.DatosGenerales!.Sexo = Sexos.FirstOrDefault(x => x.IdSexo == FaltasDeServidoresPublicosG.DatosGenerales.IdSexoFk);
			FaltasDeServidoresPublicosG.EmpleoCargoComision!.EntidadFederativa = EntidadesFederativas.FirstOrDefault(x => x.IdEntidadFederativa == FaltasDeServidoresPublicosG.EmpleoCargoComision.IdEntidadFederativaFK);
			FaltasDeServidoresPublicosG.EmpleoCargoComision!.NivelOrdenGobierno = NivelOrdenGobierno.FirstOrDefault(x => x.IdNivelOrdenGobierno == FaltasDeServidoresPublicosG.EmpleoCargoComision.IdNivelOrdenGobiernoFK);
			FaltasDeServidoresPublicosG.EmpleoCargoComision!.AmbitoPublico = AmbitoPublico.FirstOrDefault(x => x.IdAmbitoPublico == FaltasDeServidoresPublicosG.EmpleoCargoComision.IdAmbitoPublicoFK);
			FaltasDeServidoresPublicosG.OrigenProcedimiento!.Clave = ListaOrigenesInvestigacion.FirstOrDefault(x => x.IdOrigenProcedimiento == FaltasDeServidoresPublicosG.OrigenProcedimiento.IdOrigenProcedimientoCatFK);
			FaltasDeServidoresPublicosG.Resolucion!.OrdenJurisdiccional = OrdenJurisdiccional.FirstOrDefault(x => x.Id == FaltasDeServidoresPublicosG.Resolucion.IdOrdenJurisdiccionalFK);
			FaltasDeServidoresPublicosG.SancionEconomica.Moneda = TipoMonedas.FirstOrDefault(x => x.IdMoneda == FaltasDeServidoresPublicosG.SancionEconomica.IdMonedaFK);
			FaltasDeServidoresPublicosG.SancionEfectivamenteCobrada.Moneda = TipoMonedas.FirstOrDefault(x => x.IdMoneda == FaltasDeServidoresPublicosG.SancionEfectivamenteCobrada.IdMonedaFK);
		}

		private async Task ValidarCampos()
		{
			await JS.InvokeVoidAsync("formatAndValidateClave", "curpInput", "CURP");
			await JS.InvokeVoidAsync("formatAndValidateClave", "rfcInput", "RFC");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "nombresInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "primerApellidoInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "segundoApellidoInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "nombreEnteInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "siglasEntePublicoInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "valorInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "denominacionInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "areaAdscripcionInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "ValorOrigenInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "ValorFaltaCometidaInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "normatividadInfringidaInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "nombreNormatividadInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "articuloInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "descripcionHechosInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "tituloDocumentoInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "autoridadResolutoraInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "autoridadInvestigadoraInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "autoridadSubstanciadoraInput", "");
			await JS.InvokeVoidAsync("formatAndAllowSpaces", "denominacionSancionInput", "");
			//await JS.InvokeVoidAsync("formatAndValidateInputUrl", "urlResolucionInput", "url");
			//await JS.InvokeVoidAsync("formatAndValidateInputUrl", "urlResolucionFirmeInput", "url");			
		}

        private async Task ValidarInformacionFecha()
        {
            if (IsEditMode == (int)TipoVistaComponentes.Ver)
                return;

            if (FaltasDeServidoresPublicosG!.Fecha is not null)
            {
                BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Fecha] = true;
            }
            else
            {
                BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Fecha] = false;
            }
        }
		private async Task ValidarInformacionExpediente()
		{
            if (IsEditMode == (int)TipoVistaComponentes.Ver)
                return;

            if (!string.IsNullOrEmpty(FaltasDeServidoresPublicosG.Expediente))
            {
                BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Expediente] = true;
            }
            else
            {
                BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Expediente] = false;
            }
        }
        private bool EsValido(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return true; // permitir vacío si es opcional
            return Regex.IsMatch(valor, @"^[a-zA-ZÁÉÍÓÚáéíóúÑñ0-9 ]+$");
        }

        private async Task ValidarInformacionDatosPersonales()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var datos = FaltasDeServidoresPublicosG!.DatosGenerales;
			
			bool esValido =
				!string.IsNullOrWhiteSpace(datos!.Nombres) &&
				!string.IsNullOrWhiteSpace(datos.PrimerApellido) &&
				!string.IsNullOrWhiteSpace(datos.Curp) && datos.Curp.Length == 18 &&
				!string.IsNullOrWhiteSpace(datos.Rfc) && datos.Rfc.Length == 13;

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.DatosGenerales] = esValido;

		}

		private async Task ValidarInformacionEmpleoCargoComision()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			if (FaltasDeServidoresPublicosG!.EmpleoCargoComision!.EntidadFederativa is not null && FaltasDeServidoresPublicosG!.EmpleoCargoComision!.NivelOrdenGobierno is not null )
			{
				BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.EmpleoCargoComision] = true;
			}
			else
			{
				BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.EmpleoCargoComision] = false;
			}
		}		

		private async Task ValidarInformacionOrigenProcedimiento()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var origen = FaltasDeServidoresPublicosG?.OrigenProcedimiento;
			var clave = origen?.Clave?.Clave;

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.OrigenProcedimiento] = origen?.Clave is not null && FaltasDeServidoresPublicosG!.OrigenProcedimiento?.Clave?.Clave?.Contains("OTRO (Especifique)") == true && FaltasDeServidoresPublicosG!.OrigenProcedimiento!.Valor is not null || origen?.Clave is not null && FaltasDeServidoresPublicosG!.OrigenProcedimiento?.Clave?.Clave?.Contains("OTRO (Especifique)") == false; 
		}

		private async Task ValidarInformacionFaltaCometida()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var falta = FaltasDeServidoresPublicosG!.FaltaCometida;

			bool datosCompletos =
				_optionsFaltaComedia.Any() &&
				!string.IsNullOrWhiteSpace(falta?.NombreNormatividad) &&
				!string.IsNullOrWhiteSpace(falta?.Articulo) &&
				!string.IsNullOrWhiteSpace(falta?.DescripcionHechos);			

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.FaltaCometida] =
				datosCompletos;
		}

		private async Task ValidarInformacionResolucion()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var resolucion = FaltasDeServidoresPublicosG!.Resolucion;

			bool datosCompletos =
				!string.IsNullOrWhiteSpace(resolucion?.TituloDocumento) &&
				resolucion?.FechaResolucion != null &&
				resolucion?.FechaNotificacion != null &&
				!string.IsNullOrWhiteSpace(resolucion?.UrlResolucion) &&
				resolucion?.FechaResolucionFirme != null &&
				resolucion?.FechaNotificacionFirme != null &&
				!string.IsNullOrWhiteSpace(resolucion?.UrlResolucionFirme) &&
				resolucion?.OrdenJurisdiccional != null &&
				!string.IsNullOrWhiteSpace(resolucion?.AutoridadResolutora) &&
				!string.IsNullOrWhiteSpace(resolucion?.AutoridadInvestigadora) &&
				!string.IsNullOrWhiteSpace(resolucion?.AutoridadSubstanciadora);

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Resolucion] = datosCompletos;
		}

		private async Task ValidarInformacionTipoSancion()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			bool datosCompletos = false;
			if (_options.Count() > 0) datosCompletos = true;
			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.TipoSancion] = datosCompletos;
		}

		private async Task ValidarInformacionSancionEconomica()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var suspension = FaltasDeServidoresPublicosG.SancionEconomica;

			bool datosCompletos =
				suspension!.Moneda != null && suspension.Monto != null;

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.SancionEconomica] = datosCompletos;
		}

		private async Task ValidarInformacionIndeminizacion()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var indeminizacion = FaltasDeServidoresPublicosG.Indeminizacion;

			bool datosCompletos = indeminizacion!.Moneda != null && indeminizacion.Monto != null;
			//bool datosCompletos = true;

			//Console.WriteLine($"ValidarInformacionIndeminizacion: Moneda: {indeminizacion!.Moneda}, Monto: {indeminizacion.Monto}, Datos Completos: {datosCompletos}");				

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Indeminizacion] = datosCompletos;

			//foreach (var activelPanel in ListActivePanel) 
			//{
			//	Console.WriteLine($"ValidarInformacionIndeminizacion: ActivelPanel: {activelPanel}");
			//}
		}

		private async Task ValidarInformacionInhabilitacion()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var inhabilitacion = FaltasDeServidoresPublicosG.Inhabilitacion;

			bool datosCompletos =
				inhabilitacion!.Anio != null && inhabilitacion.Anio != string.Empty &&
				inhabilitacion!.Mes != null && inhabilitacion.Mes != string.Empty &&
				inhabilitacion!.Dia != null && inhabilitacion.Dia != string.Empty &&
				inhabilitacion!.FechaInicial != null && inhabilitacion!.FechaFinal != null;

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Inhabilitacion] = datosCompletos;
		}

		private async Task ValidarInformacionOtro()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var inhabilitacion = FaltasDeServidoresPublicosG.Otro;

			bool datosCompletos =
				inhabilitacion!.DenominacionSancion != null && inhabilitacion.DenominacionSancion != string.Empty;

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Otro] = datosCompletos;
		}

		private async Task ValidarInformacionObservaciones()
		{
			if (IsEditMode == (int)TipoVistaComponentes.Ver) return;
			var inhabilitacion = FaltasDeServidoresPublicosG.Otro;

			bool datosCompletos = true;

			BloquearBotonSiguientePorSesion[SesionesFaltasGravesPersonasFisicas.Observaciones] = datosCompletos;
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
						FaltasCometidas = CatalogosBD.FaltaCometidas!.Where(c => c.Bandera == 0 || c.Bandera == 3).ToList();
						Sexos = CatalogosBD.Sexo!;
						ListaOrigenesInvestigacion = CatalogosBD.OrigenProcedimiento!;
                        TipoSancionClaves = CatalogosBD.TipoSancion!.Where(c => c.Bandera == 0 && c.IdTipoSancionCat != 1 && c.IdTipoSancionCat != 2 || c.Bandera == 3 || c.Bandera == 1).ToList();										
                        TipoMonedas = CatalogosBD.Monedas!;
						Paises = CatalogosBD.Paises!;
                        TiposVialidades = CatalogosBD.TipoVialidad!;
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

		private List<PlazoPago> PlazoPagos { get; set; } = new List<PlazoPago>
		{
			new PlazoPago { Clave = "1", Valor = "Años" },
			new PlazoPago { Clave = "2", Valor = "Meses" },
			new PlazoPago { Clave = "3", Valor = "Dias" },
		};

		private async Task MostrarSiguientePanel(int posicionPanel)
		{
			Console.WriteLine($"MostrarSiguientePanel - ActivePanel: {ActivePanel}, posicionPanel: {posicionPanel}");
			if (IsEditMode != (int)TipoVistaComponentes.Ver)
			{
				if (RespuestaRegistro is not null)
				{
					if (RespuestaRegistro!.Data is null && IsEditMode == (int)TipoVistaComponentes.Agregar)
						await GuardarInformacionDeFaltasTemporales();
					else
						await ActualizarInformacionDeFaltasTemporales();

				}
			}
            if (ResultadoCorrecto && IsEditMode == (int)TipoVistaComponentes.Agregar) await MostrarPanel(posicionPanel);
            else if (IsEditMode != (int)TipoVistaComponentes.Agregar) await MostrarPanel(posicionPanel);
        
		}

        private async Task MostrarPanel(int posicionPanel)
        {
            // Lógica para la sección de tipo de sanción
            if ((int)SesionesFaltasGravesPersonasFisicas.TipoSancion == posicionPanel)
            {
                ListActivePanel.Clear();

                if (_options.Contains("INDEMNIZACION"))
                {
                    ListActivePanel.Add((int)SesionesFaltasGravesPersonasFisicas.Indeminizacion);
                }
                if (_options.Contains("SANCION ECONOMICA"))
                {
                    ListActivePanel.Add((int)SesionesFaltasGravesPersonasFisicas.SancionEconomica);
                }
                if (_options.Contains("INHABILITACIÓN TEMPORAL PARA PARTICIPAR EN ADQUISICIONES,ARRENDAMIENTOS,SERVICIOS U OBRAS PÚBLICAS"))
                {
                    ListActivePanel.Add((int)SesionesFaltasGravesPersonasFisicas.Inhabilitacion);
                }
                if (_options.Contains("OTRO (Especifique)"))
                {
                    ListActivePanel.Add((int)SesionesFaltasGravesPersonasFisicas.Otro);
                }

                BloqueoSesiones.Add(ActivePanel);

                // AÑADE ESTA LÍNEA para verificar si la lista tiene elementos
                if (ListActivePanel.Any())
                {
                    // Si hay elementos, navegamos al panel con el valor más bajo
                    ActivePanel = ListActivePanel.Min();
                    BloqueoSesiones.Add(ActivePanel);
                }
                else
                {
                    // Si la lista está vacía, no se seleccionó ninguna sanción.
                    // Por lo tanto, saltamos directamente al panel de Observaciones.
                    ActivePanel = (int)SesionesFaltasGravesPersonasFisicas.Observaciones;
                    BloqueoSesiones.Add(ActivePanel);
                }
            }
            // Lógica para avanzar entre los paneles de sanciones (dinámicos)
            else if (posicionPanel >= (int)SesionesFaltasGravesPersonasFisicas.Indeminizacion && posicionPanel < (int)SesionesFaltasGravesPersonasFisicas.Observaciones)
            {
                var listaOrdenada = ListActivePanel.OrderBy(x => x).ToList();
                int index = listaOrdenada.IndexOf(posicionPanel);

                if (index >= 0 && index < listaOrdenada.Count - 1)
                {
                    ActivePanel = listaOrdenada[index + 1];
                }
                else
                {
                    // Si es el último panel de sanción, pasamos a Observaciones
                    ActivePanel = (int)SesionesFaltasGravesPersonasFisicas.Observaciones;
                }

                BloqueoSesiones.Add(ActivePanel);
            }
            // Lógica para avanzar en los paneles iniciales (antes de sanciones)
            else
            {
                ActivePanel = posicionPanel + 1;
                BloqueoSesiones.Add(ActivePanel);
            }
        }

        private async Task MostrarAnteriorPanel(int posicionPanel)
		{
			if (posicionPanel > (int)SesionesFaltasGravesPersonasFisicas.TipoSancion)
			{
				var listaOrdenada = ListActivePanel.OrderBy(x => x).ToList();
				int index = listaOrdenada.IndexOf(posicionPanel);
				int? anterior = (index > 0 && index < listaOrdenada.Count)
						? listaOrdenada[index - 1]
						: null;
				ActivePanel = anterior ?? 7;
			}
			else
			{
				ActivePanel = posicionPanel;
				ActivePanel--;
			}
			//await GuardarInformacionDeFaltasTemporales();
		}

		public async Task GuardarInformacionDeFaltasTemporales()
		{
			try
			{
				await ConsultarIdUsuario();
				var result = await FaltasGravesPersonasFisicasService.AgregarFaltasGravesPersonasFisicas(FaltasDeServidoresPublicosG);

				if (result.Data?.Id > 0)
				{
					Snackbar.Add("Se guardó la información previa.", Severity.Success);
					RespuestaRegistro = result!;
					ResultadoCorrecto = true;
				}
				else
				{
					if (result.Mensaje is not null) Snackbar.Add($"{result.Mensaje}", Severity.Error);
					else Snackbar.Add($"Hubo un error al guardar la información previa.", Severity.Error);
					RespuestaRegistro = result!;
					ResultadoCorrecto = false;
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add($"Error en el proceso {ex.Message}", Severity.Error);
			}
		}

		public async Task ActualizarInformacionDeFaltasTemporales(bool finalizar = false)
		{
			try
			{
				await ConstruirFaltasSPG();
				var result = await FaltasGravesPersonasFisicasService.AgregarFaltasGravesPersonasFisicas(FaltasDeServidoresPublicosG);

				if (result.Response == true)
				{
					Snackbar.Add("Se guardó la información previa.", Severity.Success);
					RespuestaRegistro = result!;
					ResultadoCorrecto = true;
					if (finalizar) Navigation.NavigateTo("/FaltasGravesPersonasFísicas");
				}
				else
				{
					ResultadoCorrecto = false;
					if (result.Mensaje is not null) Snackbar.Add($"{result.Mensaje}", Severity.Error);
					else Snackbar.Add($"Hubo un error al guardar la información previa.", Severity.Error);
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add($"Error en el proceso {ex.Message}", Severity.Error);
			}
		}

		private async Task ConstruirFaltasSPG()
		{
			if (RespuestaRegistro!.Data is not null)
			{
				FaltasDeServidoresPublicosG.Id = RespuestaRegistro.Data!.Id;
				FaltasDeServidoresPublicosG.IdDatosGeneralesFK = RespuestaRegistro.Data!.IdDatosGeneralesFK;
				FaltasDeServidoresPublicosG.DatosGenerales.IdDatosGenerales = RespuestaRegistro.Data!.IdDatosGeneralesFK;
				FaltasDeServidoresPublicosG.EmpleoCargoComision.Id = RespuestaRegistro.Data!.IdEmpleoCargoComisionFK;
				FaltasDeServidoresPublicosG.IdEmpleoCargoComisionFK = RespuestaRegistro.Data!.IdEmpleoCargoComisionFK;
				FaltasDeServidoresPublicosG.FaltaCometida.Id = RespuestaRegistro.Data!.IdFaltaCometidaFK;
				FaltasDeServidoresPublicosG.IdFaltaCometidaFK = RespuestaRegistro.Data!.IdFaltaCometidaFK;
				FaltasDeServidoresPublicosG.IdInhabilitacionFK = RespuestaRegistro.Data!.IdInhabilitacionFK;
				FaltasDeServidoresPublicosG.Inhabilitacion.Id = RespuestaRegistro.Data!.IdInhabilitacionFK;
				FaltasDeServidoresPublicosG.IdOrigenProcedimientoFK = RespuestaRegistro.Data!.IdOrigenProcedimientoFK;
				FaltasDeServidoresPublicosG.OrigenProcedimiento.Id = RespuestaRegistro.Data!.IdOrigenProcedimientoFK;
				FaltasDeServidoresPublicosG.IdOtro = RespuestaRegistro.Data!.IdOtro;
				FaltasDeServidoresPublicosG.Otro.Id = RespuestaRegistro.Data!.IdOtro;
				FaltasDeServidoresPublicosG.IdResolucion = RespuestaRegistro.Data!.IdResolucion;
				FaltasDeServidoresPublicosG.Resolucion.Id = RespuestaRegistro.Data!.IdResolucion;
				FaltasDeServidoresPublicosG.IdSancionEconomicaFK = RespuestaRegistro.Data!.IdSancionEconomicaFK;
				FaltasDeServidoresPublicosG.SancionEconomica.Id = RespuestaRegistro.Data!.IdSancionEconomicaFK;
				FaltasDeServidoresPublicosG.IdSancionEfectivamenteCobradaFK = RespuestaRegistro.Data!.IdSancionEfectivamenteCobradaFK;
				FaltasDeServidoresPublicosG.SancionEfectivamenteCobrada.Id = RespuestaRegistro.Data!.IdSancionEfectivamenteCobradaFK;
				FaltasDeServidoresPublicosG.IdTipoSancionFK = RespuestaRegistro.Data!.IdTipoSancionFK;
				FaltasDeServidoresPublicosG.DatosGenerales.IdSexoFk = FaltasDeServidoresPublicosG?.DatosGenerales?.Sexo?.IdSexo;
				FaltasDeServidoresPublicosG.EmpleoCargoComision.IdEntidadFederativaFK = FaltasDeServidoresPublicosG?.EmpleoCargoComision?.EntidadFederativa?.IdEntidadFederativa;
				FaltasDeServidoresPublicosG.EmpleoCargoComision.IdNivelOrdenGobiernoFK = FaltasDeServidoresPublicosG?.EmpleoCargoComision?.NivelOrdenGobierno?.IdNivelOrdenGobierno;
				FaltasDeServidoresPublicosG.EmpleoCargoComision.IdAmbitoPublicoFK = FaltasDeServidoresPublicosG?.EmpleoCargoComision?.AmbitoPublico?.IdAmbitoPublico;
				FaltasDeServidoresPublicosG.OrigenProcedimiento.IdOrigenProcedimientoCatFK = FaltasDeServidoresPublicosG?.OrigenProcedimiento?.Clave?.IdOrigenProcedimiento;
				FaltasDeServidoresPublicosG.Resolucion.IdOrdenJurisdiccionalFK = FaltasDeServidoresPublicosG?.Resolucion?.OrdenJurisdiccional?.Id;
				FaltasDeServidoresPublicosG.SancionEconomica.IdMonedaFK = FaltasDeServidoresPublicosG?.SancionEconomica?.Moneda?.IdMoneda;
				FaltasDeServidoresPublicosG.Indeminizacion.IdTipoMonedaFK = FaltasDeServidoresPublicosG?.SancionEconomica?.Moneda?.IdMoneda;
				FaltasDeServidoresPublicosG.SancionEfectivamenteCobrada.IdMonedaFK = FaltasDeServidoresPublicosG?.SancionEfectivamenteCobrada?.Moneda?.IdMoneda;
				FaltasDeServidoresPublicosG.Indeminizacion.IdTipoMonedaFK = FaltasDeServidoresPublicosG?.Indeminizacion?.Moneda?.IdMoneda;
				FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.IdTipoVialidadFK = FaltasDeServidoresPublicosG?.DatosGenerales?.DomicilioMexico?.TipoVialidad?.IdTipoVialidad;
				FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico.IdEntidadFederativaFK = FaltasDeServidoresPublicosG?.DatosGenerales?.DomicilioMexico?.EntidadFederativa?.IdEntidadFederativa;
				FaltasDeServidoresPublicosG.DatosGenerales.IdDomicilioMexicoFK = FaltasDeServidoresPublicosG?.DatosGenerales?.DomicilioMexico?.IdDomicilioMexico;
				FaltasDeServidoresPublicosG.DatosGenerales.IdDomicilioExtranjeroFK = FaltasDeServidoresPublicosG?.DatosGenerales?.DomicilioExtranjero?.IdDomicilioExtranjero;
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
				await ActualizarInformacionDeFaltasTemporales(true);
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
				await ActualizarInformacionDeFaltasTemporales(true);
			}
		}

		private async Task ConsultarIdUsuario()
		{
			try
			{
				var token = await AuthService.GetTokenAsync();

				//Console.WriteLine($" token {token}");

				if (!string.IsNullOrEmpty(token))
				{
					TokenResponse tokenUsuario = new();
					tokenUsuario.Token = token;
					AutenticacionResponse informacionPerfil = await AuthService.ConsultarInformacionPerfil(tokenUsuario);
					if (informacionPerfil.Usuario is not null)
					{
						FaltasDeServidoresPublicosG.IdUsuarioFK = informacionPerfil.Usuario.Id;
					}
				}
				else
				{
					Navigation.NavigateTo("/login", forceLoad: true);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Exception {ex.Message}");
				Navigation.NavigateTo("/login", forceLoad: true);
			}
		}

		public enum TipoVistaComponentes
		{
			Agregar = 1,
			Ver,
			Actualizar
		}
	}
}
