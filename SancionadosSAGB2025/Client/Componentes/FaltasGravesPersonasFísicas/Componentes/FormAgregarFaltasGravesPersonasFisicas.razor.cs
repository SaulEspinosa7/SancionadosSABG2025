using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SancionadosSAGB2025.Client.Shared.Partial.Dialog;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Client.Componentes.FaltasGravesPersonasFísicas.Componentes
{
	partial class FormAgregarFaltasGravesPersonasFisicas
	{
		[Parameter] public AddFaltasGravesPersonasFisicas FaltasDeServidoresPublicosG { set; get; } = new();
		[Parameter] public int IsEditMode { set; get; } = (int)TipoVistaComponentes.Agregar;
		[Parameter] public bool modoVista { set; get; } = false;	
        [Inject] private NavigationManager Navigation { get; set; }
		private int ActivePanel { set; get; } = 0; // 0: Expediente, 1: Datos Generales, etc.
		private bool ResultadoCorrecto { set; get; } // 0: Expediente, 1: Datos Generales, etc.
		private List<int> ListActivePanel { set; get; } = new();
		private List<int> BloqueoSesiones { get; set; } = new();
		private RespuestaRegistroFaltasGravesPersonasFisicas RespuestaRegistro { set; get; } = new();
		private Catalogos CatalogosBD { get; set; } = new();
		private List<EntidadFederativaEntidad> EntidadesFederativas { get; set; } = new();
		private List<NivelOrdenGobierno> NivelOrdenGobierno { get; set; } = new();
		private List<AmbitoPublico> AmbitoPublico { get; set; } = new();
		private List<OrdenJurisdiccional> OrdenJurisdiccional { get; set; } = new();
		private List<NivelJerarquicoEntidad> NivelJerarquico { get; set; } = new();
		private List<FaltaCometidaEntidad> FaltasCometidas { get; set; } = new();
		private List<MonedaCat> TipoMonedas { get; set; } = new();
		private List<OrigenProcedimientoEntidad> ListaOrigenesInvestigacion { get; set; } = new();
		private List<Sexo> Sexos { get; set; } = new();
		private List<TipoSancion> TipoSancionClaves { get; set; } = new();

        private List<TipoVialidad> TiposVialidades { get; set; } = new();
		private List<PaisCat> Paises { get; set; } = new();
		private IEnumerable<string> _options { get; set; } = new HashSet<string>();
		private IEnumerable<string> _optionsFaltaComedia { get; set; } = new HashSet<string>();

        private MudForm? _formFecha;
        private MudForm? _formExpediente;
        private MudForm? _formDatosGenerales;
        private MudForm? _formDatosEntePublico;

        private MudForm? _formOrigenProcedimiento;

        private MudForm? _formTipoFaltaCometida;

        private MudForm? _formResolucionSancionatoria;

        private MudForm? _formTipoSancionImpuesta;

        private MudForm? _formInhabilitacionTemporal;

        private MudForm? _formIndemnizacion;

        private MudForm? _formSancionEconomica;

        private MudForm? _formOtro;

        private bool[] _isStep1Valid = new bool[16];
        private bool[] _isPanelCompleted = new bool[16];
		private int index;
		private bool loading = false;
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
            if (FaltasDeServidoresPublicosG?.DatosGenerales?.DomicilioMexico != null)
                TipoDomicilioSeleccionado = TipoDomiclio.MEXICO;
            else if (FaltasDeServidoresPublicosG?.DatosGenerales?.DomicilioExtranjero != null)
                TipoDomicilioSeleccionado = TipoDomiclio.EXTRANJERO;
            await ObtenerCatalogosFormulario();
			await MostrarOpcionCatalogos();
			await InicializarVariables();
		}
        void Fecha()
        {
            expediente = true;
        }
        private async Task OnPreviewInteraction(StepperInteractionEventArgs arg)
        {
            if (arg.Action == StepAction.Complete)
            {
                // occurrs when clicking next
                await ControlStepCompletion(arg);
            }
        }

        private async Task ControlStepCompletion(StepperInteractionEventArgs arg)
        {
            // 1. Inicia el estado de carga
            loading = true;

            try
            {
				Console.WriteLine(FaltasDeServidoresPublicosG);
                bool isFormValid = false;

                switch (arg.StepIndex)
                {
                    case 0:
                        await _formFecha!.Validate();
                        isFormValid = _isStep1Valid[0];
                        if (!isFormValid && !modoVista)
                            arg.Cancel = true;
                        break;
                    case 1:
                        await _formExpediente!.Validate();
                        isFormValid = _isStep1Valid[1];
                        if (!isFormValid && !modoVista)
                            arg.Cancel = true;
                        else if (!modoVista)
                        {
                            isFormValid = await GuardarTemporal();
                            if (!isFormValid)
                                arg.Cancel = true;
                        }
                        break;
                    case 2:
                        await _formDatosGenerales!.Validate();
                        isFormValid = _isStep1Valid[2];
                        if (!isFormValid && !modoVista)
                            arg.Cancel = true;
                        else if (!modoVista)
                        {
                            isFormValid = await GuardarTemporal();
                            if (!isFormValid)
                                arg.Cancel = true;
                        }
                        break;
                    case 3:
                        await _formDatosEntePublico!.Validate();
                        isFormValid = _isStep1Valid[3];
                        if (!isFormValid && !modoVista)
                            arg.Cancel = true;
                        else if (!modoVista)
                        {
                            isFormValid = await GuardarTemporal();
                            if (!isFormValid)
                                arg.Cancel = true;
                        }
                        break;
                    case 4:
                        await _formOrigenProcedimiento!.Validate();
                        isFormValid = _isStep1Valid[4];
                        if (!isFormValid && !modoVista)
                            arg.Cancel = true;
                        else if (!modoVista)
                        {
                            isFormValid = await GuardarTemporal();
                            if (!isFormValid)
                                arg.Cancel = true;
                        }
                        break;
                    case 5:
                        await _formTipoFaltaCometida!.Validate();
                        isFormValid = _isStep1Valid[5];
                        if (!isFormValid && !modoVista)
                            arg.Cancel = true;
                        else if (!modoVista)
                        {
                            isFormValid = await GuardarTemporal();
                            if (!isFormValid)
                                arg.Cancel = true;
                        }
                        break;
                    case 6:
                        await _formResolucionSancionatoria!.Validate();
                        isFormValid = _isStep1Valid[6];
                        if (!isFormValid && !modoVista)
                            arg.Cancel = true;
                        else if (!modoVista)
                        {
                            isFormValid = await GuardarTemporal();
                            if (!isFormValid)
                                arg.Cancel = true;
                        }
                        break;
                    case 7:
                        await _formTipoSancionImpuesta!.Validate();
                        isFormValid = _isStep1Valid[7];
                        if (!isFormValid && !modoVista)
                            arg.Cancel = true;
                        else
                        {
                            if (!modoVista)
                            {
                                var resp = await OpcionesTipoSancion();
                                if (!resp)
                                    arg.Cancel = true;
                                else
                                {
                                    isFormValid = await GuardarTemporal();
                                    if (!isFormValid)
                                        arg.Cancel = true;
                                }
                            }

                        }
                        break;                  
                    
                }
            }
            finally
            {
                // 2. Detiene el estado de carga al finalizar la ejecución del método
                loading = false;
            }
        }
        private async Task<bool> OpcionesTipoSancion()
        {
            try
            {
                // REGLA 1: Si es modo vista, salimos inmediatamente.
                if (modoVista)
                {
                    return true;
                }

                bool todasLasOpcionesSonValidas = true;

                if (_options.Contains("INHABILITACIÓN TEMPORAL PARA PARTICIPAR EN ADQUISICIONES,ARRENDAMIENTOS,SERVICIOS U OBRAS PÚBLICAS"))
                {
                    await _formInhabilitacionTemporal!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[8];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("INDEMNIZACION"))
                {
                    await _formIndemnizacion!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[9];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("SANCION ECONOMICA"))
                {
                    await _formSancionEconomica!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[10];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }           

                if (_options.Contains("OTRO (Especifique)"))
                {
                    await _formOtro!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[11];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                return todasLasOpcionesSonValidas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private async Task FinalizarRegistro()
        {
            try
            {
                loading = true;
                if (modoVista)
                {
                    Regresar();
                }
                else
                {
                    bool guardado = await GuardarTemporal();
                    if (guardado && FaltasDeServidoresPublicosG is null)
                    {
                        Snackbar.Add("REGISTRO GUARDADO CORRECTAMENTE", Severity.Success);
                        Navigation.NavigateTo("/BuscarServidoresPublicosGravesFisica");
                    }
                    else
                    {
                        Regresar();
                    }
                }
            }
            finally
            {
                loading = false;
            }

        }
        private void Regresar()
        {
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }

        private async Task<bool> GuardarTemporal()
        {

            await ConsultarIdUsuario();
            if (_options is not null && _options.Any())
            {
                FaltasDeServidoresPublicosG.MultipleSancion = string.Join(", ", _options);
            }

            var response = await _http.PostAsJsonAsync<AddFaltasGravesPersonasFisicas>(
                "api/FaltasGravesPersonasFisicas/AgregarFaltasGravesPersonasFisicas",
                FaltasDeServidoresPublicosG);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error en el servidor: {error}");
                Snackbar.Add($"Error al guardar temporalmente: {error}", Severity.Error, config =>
                {
                    config.ShowCloseIcon = true;
                });
                // Snackbar.Add($"Error al guardar temporalmente: {error}", Severity.Error);
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroFaltasGravesPersonasFisicas>();
            if (result?.Response == true && result.Mensaje is null)
            {
                FaltasDeServidoresPublicosG = result.Data!;
                Snackbar.Add("INFORMACIÓN GUARDADA CORRECTAMENTE", Severity.Success);
                return true;
            }
            else if (result?.Response == true && result.Mensaje is not null)
            {

                Snackbar.Add("INFORMACIÓN ELIMINADA CORRECTAMENTE", Severity.Success);
                return true;

            }
            else
            {
                Snackbar.Add(result.Mensaje, Severity.Error);
                return false;
            }
        }

        private async Task InicializarVariables() 
		{
			TipoDomicilios = Enum.GetValues(typeof(TipoDomiclio)).Cast<TipoDomiclio>().ToList();
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioMexico = new();
			//FaltasDeServidoresPublicosG.DatosGenerales.DomicilioExtranjero = new();
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
			

                _options = FaltasDeServidoresPublicosG.MultipleSancion?
                            .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim())
                            .ToHashSet()!;

                _optionsFaltaComedia = FaltasDeServidoresPublicosG.FaltaCometida?.MultipleFalta?
						.Split(',', StringSplitOptions.RemoveEmptyEntries)
						.Select(s => s.Trim()) // elimina espacios antes/después
						.ToHashSet()!; // crea un HashSet<string>


			
			//if (firstRender)
			//{
			//	objRef = DotNetObjectReference.Create(this);
			//	await JS.InvokeVoidAsync("registerBeforeUnload", objRef);
			//	await JS.InvokeVoidAsync("registerGeneralBeforeUnload", objRef);
			//}
		}
        private async Task CancelarForm()
        {

            var parameters = new DialogParameters<ModalCancelarForm>();

            var options = new DialogOptions { CloseOnEscapeKey = false, CloseButton = false, MaxWidth = MaxWidth.Small, BackdropClick = false };

            var dialog = await DialogService.ShowAsync<ModalCancelarForm>("", parameters, options);
            var result = await dialog.Result;


            if (!result.Canceled)
            {
                FaltasDeServidoresPublicosG.Activo = 0;
                await GuardarTemporal();
                Navigation.NavigateTo("/FaltasGravesPersonasFísicas");
            }

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

	
        private bool EsValido(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return true; // permitir vacío si es opcional
            return Regex.IsMatch(valor, @"^[a-zA-ZÁÉÍÓÚáéíóúÑñ0-9 ]+$");
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
        private List<PlazoPago> PlazoPagos { get; set; } = new List<PlazoPago>
        {
            new PlazoPago { Clave = "1", Valor = "Años" },
            new PlazoPago { Clave = "2", Valor = "Meses" },
            new PlazoPago { Clave = "3", Valor = "Dias" },
        };
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
                        TipoSancionClaves = CatalogosBD.TipoSancion!.Where(c => c.Bandera == 0 && c.IdTipoSancionCat != 1 && c.IdTipoSancionCat != 2 && c.IdTipoSancionCat != 11 || c.Bandera == 3 || c.Bandera == 1).ToList();
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
