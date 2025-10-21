using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SancionadosSAGB2025.Client.Componentes.FaltasDeServidoresPúblicosG;
using SancionadosSAGB2025.Client.Componentes.FaltasDeServidoresPúblicosG.Components;
using SancionadosSAGB2025.Client.Shared.Partial.Dialog;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Client.Componentes.FaltasDeServidoresPúblicosN.Components
{
	partial class FormAgregarFaltasServidoresPNG
	{
		[Parameter] public AddFaltasDeServidoresPublicosNoGraves? FaltasDeServidoresPublicosG { set; get; } = new();
		[Parameter] public int IsEditMode { set; get; } = (int)TipoVistaComponentes.Agregar;
		[Parameter] public bool modoVista { set; get; } = false;	
        [Inject] private NavigationManager Navigation { get; set; }
		private int ActivePanel { set; get; } = 0; // 0: Expediente, 1: Datos Generales, etc.
		private bool ResultadoCorrecto { set; get; } // 0: Expediente, 1: Datos Generales, etc.
		private List<int> ListActivePanel { set; get; } = new();
		private List<int> BloqueoSesiones { get; set; } = new();
		private RespuestaRegistro RespuestaRegistro { set; get; } = new();
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
		private List<TipoAmonestacion> TiposAmonestaciones { get; set; } = new();
		private IEnumerable<string> _options { get; set; } = new HashSet<string>();
        private MudForm? _formFecha;
        private MudForm? _formExpediente;
        private MudForm? _formDatosGenerales;
        private MudForm? _formDatosEmpleo;

        private MudForm? _formOrigenProcedimiento;

        private MudForm? _formTipoFaltaCometida;

        private MudForm? _formResolucionSancionatoria;

        private MudForm? _formTipoSancionImpuesta;

        private MudForm? _formAmonestacion;

        private MudForm? _formSuspension;

        private MudForm? _formInhabilitacion;
        private MudForm? _formDestitucion;

        private MudForm? _formOtro;
		private bool loading;
        private bool[] _isStep1Valid = new bool[16];
        private IEnumerable<string> _optionsFaltaComedia { get; set; } = new HashSet<string>();
		private Dictionary<SesionesFaltasDeServidoresPublicosNoGraves, bool> BloquearBotonSiguientePorSesion { get; set; } =
						Enum.GetValues(typeof(SesionesFaltasDeServidoresPublicosNoGraves))
							.Cast<SesionesFaltasDeServidoresPublicosNoGraves>()
							.ToDictionary(key => key, value => false); // o true si quieres bloquear todos inicialmente
        private int index;

        private DotNetObjectReference<FormAgregarFaltasServidoresPG>? objRef;
		private bool expediente { get; set; } = false;

        protected override async Task OnInitializedAsync()
		{
			await ObtenerCatalogosFormulario();		
            await MostrarOpcionCatalogos();
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
						await _formDatosEmpleo!.Validate();
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

                if (_options.Contains("AMONESTACIÓN PÚBLICA O PRIVADA"))
                {
                    await _formAmonestacion!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[8];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("SUSPENSIÓN DEL EMPLEO,CARGO O COMISIÓN"))
                {
                    await _formSuspension!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[9];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("DESTITUCIÓN DEL EMPLEO,CARGO O COMISIÓN"))
                {
                    await _formDestitucion!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[10];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }
                if (_options.Contains("INHABILITACIÓN TEMPORAL PARA DESEMPEÑAR EMPLEOS, CARGOS O COMISIONES EN EL SERVICIO PÚBLICO Y PARA PARTICIPAR EN ADQUISICIONES, ARRENDAMIENTOS, SERVICIOS U OBRAS PÚBLICAS"))
                {
                    await _formInhabilitacion!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[11];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }
                if (_options.Contains("OTRO (Especifique)"))
                {
                    await _formOtro!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[12];

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
                    if (guardado && FaltasDeServidoresPublicosG is not null)
                    {
                        Snackbar.Add("REGISTRO GUARDADO CORRECTAMENTE", Severity.Success);
                        Navigation.NavigateTo("/BuscarServidoresPúblicosNoGraves");
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

            var response = await _http.PostAsJsonAsync<AddFaltasDeServidoresPublicosNoGraves>(
                "api/FaltasServidoresPublicosNoGraves/AgregarFaltasSPNoGraves",
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

            var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroNoGraves>();
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
                Navigation.NavigateTo("/ServidoresPúblicosN");
            }

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
			FaltasDeServidoresPublicosG.NivelJerarquico!.Clave = NivelJerarquico.FirstOrDefault(x => x.IdNivelJerarquicoCat == FaltasDeServidoresPublicosG.NivelJerarquico.IdNivelJerarquicoFK);
			FaltasDeServidoresPublicosG.OrigenProcedimiento!.Clave = ListaOrigenesInvestigacion.FirstOrDefault(x => x.IdOrigenProcedimiento == FaltasDeServidoresPublicosG.OrigenProcedimiento.IdOrigenProcedimientoCatFK);
			FaltasDeServidoresPublicosG.Resolucion!.OrdenJurisdiccional = OrdenJurisdiccional.FirstOrDefault(x => x.Id == FaltasDeServidoresPublicosG.Resolucion.IdOrdenJurisdiccionalFK);
		}


        private async Task ObtenerCatalogosFormulario()
        {
            try
            {
                var token = await AuthService.GetTokenAsync();
                var response = await _http.PostAsync($"api/Catalogos/ObtenerTodosLosCatalogos?token={token}", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Catalogos>();
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
                            FaltasCometidas = CatalogosBD.FaltaCometidas!.Where(c => c.Bandera == 2).ToList();
                            Sexos = CatalogosBD.Sexo!;
                            ListaOrigenesInvestigacion = CatalogosBD.OrigenProcedimiento!;
                            TiposAmonestaciones = CatalogosBD.TipoAmonestacion!;
                            TipoSancionClaves = CatalogosBD.TipoSancion!.Where(c => c.Bandera == 0 && c.IdTipoSancionCat != 4 || c.Bandera == 2).ToList();
                            TipoMonedas = CatalogosBD.Monedas!;
                        }
                        Console.WriteLine($" CatalogosBD {CatalogosBD.TipoVialidad}");
                    }
                    else
                    {
                        Snackbar.Add($"Error al consultar los catalogos", Severity.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error en el proceso {ex.Message}", Severity.Error);
            }
        }
	
		public async Task GuardarInformacionDeFaltasTemporales()
		{
			try
			{
				await ConsultarIdUsuario();
				var result = await FaltasSPNoGravesService.AgregarFaltasSPG(FaltasDeServidoresPublicosG);

				if (result.Data?.Id > 0)
				{
					Snackbar.Add("Se guardó la información previa.", Severity.Success);
					FaltasDeServidoresPublicosG = result.Data!;
					ResultadoCorrecto = true;
				}
				else
				{
					if (result.Mensaje is not null) Snackbar.Add($"{result.Mensaje}", Severity.Error);
					else Snackbar.Add($"Hubo un error al guardar la información previa.", Severity.Error);
					//FaltasDeServidoresPublicosG = result!.Data;
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
				var result = await FaltasSPNoGravesService.ActualizarFaltasSPG(FaltasDeServidoresPublicosG);

				if (result.Response == true)
				{
					Snackbar.Add("Se guardó la información previa.", Severity.Success);
				    if(result.Data is not null) await ConstruirSPNG(result.Data!);
					ResultadoCorrecto = true;
					if (finalizar) Navigation.NavigateTo("/ServidoresPúblicosN");
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

		private async Task ConstruirSPNG(AddFaltasDeServidoresPublicosNoGraves addFaltasDeServidoresPublicosNoGraves)
		{
			FaltasDeServidoresPublicosG.Id = addFaltasDeServidoresPublicosNoGraves.Id;
			FaltasDeServidoresPublicosG.IdDatosGeneralesFK = addFaltasDeServidoresPublicosNoGraves.IdDatosGeneralesFK;
			FaltasDeServidoresPublicosG.DatosGenerales.IdDatosGenerales = addFaltasDeServidoresPublicosNoGraves.IdDatosGeneralesFK;
			FaltasDeServidoresPublicosG.IdDestitucionEmpleo = addFaltasDeServidoresPublicosNoGraves.IdDestitucionEmpleo;
			FaltasDeServidoresPublicosG.DestitucionEmpleo.Id = addFaltasDeServidoresPublicosNoGraves.IdDestitucionEmpleo;
			FaltasDeServidoresPublicosG.EmpleoCargoComision.Id = addFaltasDeServidoresPublicosNoGraves.IdEmpleoCargoComisionFK;
			FaltasDeServidoresPublicosG.IdEmpleoCargoComisionFK = addFaltasDeServidoresPublicosNoGraves.IdEmpleoCargoComisionFK;
			FaltasDeServidoresPublicosG.FaltaCometida.Id = addFaltasDeServidoresPublicosNoGraves.IdFaltaCometidaFK;
			FaltasDeServidoresPublicosG.IdFaltaCometidaFK = addFaltasDeServidoresPublicosNoGraves.IdFaltaCometidaFK;
			FaltasDeServidoresPublicosG.IdInhabilitacionFK = addFaltasDeServidoresPublicosNoGraves.IdInhabilitacionFK;
			FaltasDeServidoresPublicosG.Inhabilitacion.Id = addFaltasDeServidoresPublicosNoGraves.IdInhabilitacionFK;
			FaltasDeServidoresPublicosG.IdNivelJerarquicoFK = addFaltasDeServidoresPublicosNoGraves.IdNivelJerarquicoFK;
			FaltasDeServidoresPublicosG.NivelJerarquico.Id = addFaltasDeServidoresPublicosNoGraves.IdNivelJerarquicoFK;
			FaltasDeServidoresPublicosG.IdOrigenProcedimientoFK = addFaltasDeServidoresPublicosNoGraves.IdOrigenProcedimientoFK;
			FaltasDeServidoresPublicosG.OrigenProcedimiento.Id = addFaltasDeServidoresPublicosNoGraves.IdOrigenProcedimientoFK;
			FaltasDeServidoresPublicosG.IdOtro = addFaltasDeServidoresPublicosNoGraves.IdOtro;
			FaltasDeServidoresPublicosG.Otro.Id = addFaltasDeServidoresPublicosNoGraves.IdOtro;
			FaltasDeServidoresPublicosG.IdResolucion = addFaltasDeServidoresPublicosNoGraves.IdResolucion;
			FaltasDeServidoresPublicosG.Resolucion.Id = addFaltasDeServidoresPublicosNoGraves.IdResolucion;
			FaltasDeServidoresPublicosG.IdSuspension = addFaltasDeServidoresPublicosNoGraves.IdSuspension;
			FaltasDeServidoresPublicosG.Suspension.Id = addFaltasDeServidoresPublicosNoGraves.IdSuspension;
			FaltasDeServidoresPublicosG.IdTipoSancionFK = addFaltasDeServidoresPublicosNoGraves.IdTipoSancionFK;
			FaltasDeServidoresPublicosG.DatosGenerales.IdSexoFk = addFaltasDeServidoresPublicosNoGraves?.DatosGenerales?.Sexo?.IdSexo;
			FaltasDeServidoresPublicosG.EmpleoCargoComision.IdEntidadFederativaFK = addFaltasDeServidoresPublicosNoGraves?.EmpleoCargoComision?.EntidadFederativa?.IdEntidadFederativa;
			FaltasDeServidoresPublicosG.EmpleoCargoComision.IdNivelOrdenGobiernoFK = addFaltasDeServidoresPublicosNoGraves?.EmpleoCargoComision?.NivelOrdenGobierno?.IdNivelOrdenGobierno;
			FaltasDeServidoresPublicosG.EmpleoCargoComision.IdAmbitoPublicoFK = addFaltasDeServidoresPublicosNoGraves?.EmpleoCargoComision?.AmbitoPublico?.IdAmbitoPublico;
			FaltasDeServidoresPublicosG.NivelJerarquico.IdNivelJerarquicoFK = addFaltasDeServidoresPublicosNoGraves?.NivelJerarquico?.Clave?.IdNivelJerarquicoCat;
			FaltasDeServidoresPublicosG.OrigenProcedimiento.IdOrigenProcedimientoCatFK = addFaltasDeServidoresPublicosNoGraves?.OrigenProcedimiento?.Clave?.IdOrigenProcedimiento;
		}

		private async Task ConstruirFaltasSPG()
		{
			if (FaltasDeServidoresPublicosG!.Id is not null)
			{
				FaltasDeServidoresPublicosG.DatosGenerales.IdSexoFk = FaltasDeServidoresPublicosG?.DatosGenerales?.Sexo?.IdSexo;
				FaltasDeServidoresPublicosG.EmpleoCargoComision.IdEntidadFederativaFK = FaltasDeServidoresPublicosG?.EmpleoCargoComision?.EntidadFederativa?.IdEntidadFederativa;
				FaltasDeServidoresPublicosG.EmpleoCargoComision.IdNivelOrdenGobiernoFK = FaltasDeServidoresPublicosG?.EmpleoCargoComision?.NivelOrdenGobierno?.IdNivelOrdenGobierno;
				FaltasDeServidoresPublicosG.EmpleoCargoComision.IdAmbitoPublicoFK = FaltasDeServidoresPublicosG?.EmpleoCargoComision?.AmbitoPublico?.IdAmbitoPublico;
				FaltasDeServidoresPublicosG.NivelJerarquico.IdNivelJerarquicoFK = FaltasDeServidoresPublicosG?.NivelJerarquico?.Clave?.IdNivelJerarquicoCat;
				FaltasDeServidoresPublicosG.OrigenProcedimiento.IdOrigenProcedimientoCatFK = FaltasDeServidoresPublicosG?.OrigenProcedimiento?.Clave?.IdOrigenProcedimiento;			
				FaltasDeServidoresPublicosG.IdTipoAmonestacionFK = FaltasDeServidoresPublicosG?.TipoAmonestacion?.IdTipoAmonestacion;			
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
				FaltasDeServidoresPublicosG.Token = await AuthService.GetTokenAsync();

				//Console.WriteLine($" token {token}");

				if (!string.IsNullOrEmpty(FaltasDeServidoresPublicosG.Token))
				{
					TokenResponse tokenUsuario = new();
					tokenUsuario.Token = FaltasDeServidoresPublicosG.Token;
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
