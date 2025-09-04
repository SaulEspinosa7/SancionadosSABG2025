using Microsoft.AspNetCore.Components;
using MudBlazor;
using SancionadosSAGB2025.Client.Services;
using SancionadosSAGB2025.Client.Shared.Partial.Dialog;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Client.Componentes.FaltasGravesPersonasMorales.Componentes
{
    partial class FormFaltasGPM
    {
        [Parameter] public PersonaMoralEntidad? personaMoralEntidadVistaEdicion { get; set; }
        [Parameter] public bool modoVista { get; set; }
        private int? _activePanel = 0;
        private PersonaMoralEntidad? _modelo = new PersonaMoralEntidad
        {
            
            SancionEconomica = new SancionEconomica
            {
                Moneda = new MonedaCat(),
                PlazoPago = new PlazoPago()
            },
            SuspensionActividades = new SuspensionActividades(),
            DisolucionSociedad = new DisolucionSociedad(),
            Otro = new Otro(),
            // Continúa inicializando cualquier otra propiedad anidada que pueda ser nula en tu formulario
        };

        private MudForm? _formFecha;
        private MudForm? _formExpediente;
        private MudForm? _formDatosGenerales;
        private MudForm? _formDatosDirectorGeneral;
        private MudForm? _formDatosEntePublico;

        private MudForm? _formOrigenProcedimiento;

        private MudForm? _formTipoFaltaCometida;

        private MudForm? _formResolucionSancionatoria;

        private MudForm? _formTipoSancionImpuesta;

        private MudForm? _formInhabilitacionTemporal;

        private MudForm? _formIndemnizacion;

        private MudForm? _formSancionEconomica;

        private MudForm? _formSuspensionActividades;

        private MudForm? _formDisolucion;

        private MudForm? _formOtro;

        private bool[] _isStep1Valid = new bool[16];
        private TipoDomiclio TipoDomicilioSeleccionado { get; set; } = new();
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
        private List<TipoDomiclio> TipoDomicilios { get; set; } = new();
        private IEnumerable<string> _optionsFaltaComedia { get; set; } = new HashSet<string>();
        private IEnumerable<string> _options { get; set; } = new HashSet<string>();
        private int _currentPanelIndex = 0;
        private int index = -1;
        private bool loading;
        private bool obsevaciones;

        protected override async Task OnInitializedAsync()
        {
            if(personaMoralEntidadVistaEdicion is not null)
            {
                _modelo = personaMoralEntidadVistaEdicion;
                if (_modelo?.DatosGenerales?.DomicilioMexico != null)
                    TipoDomicilioSeleccionado = TipoDomiclio.MEXICO;
                else if (_modelo?.DatosGenerales?.DomicilioExtranjero != null)
                    TipoDomicilioSeleccionado = TipoDomiclio.EXTRANJERO;
            }
            _modelo.SancionEfectivamenteCobrada ??= new SancionEfectivamenteCobradaMoral();
            _modelo.Otro ??= new Otro();
            await ObtenerCatalogosFormulario();
            await MostrarOpcionCatalogos();
            await InicializarVariables();
        }
        private async Task InicializarVariables()
        {
            TipoDomicilios = Enum.GetValues(typeof(TipoDomiclio)).Cast<TipoDomiclio>().ToList();
            //_modelo.DatosGenerales.DomicilioMexico = new();
            //_modelo.DatosGenerales.DomicilioExtranjero = new();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {            

                _options = _modelo.MultipleSancion?
                            .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim())
                            .ToHashSet()!;

                _optionsFaltaComedia = _modelo.FaltaCometida?.MultipleFalta?
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim()) // elimina espacios antes/después
                        .ToHashSet()!; // crea un HashSet<string>
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
                        TipoSancionClaves = CatalogosBD.TipoSancion!.Where(c => c.Bandera == 0 && c.IdTipoSancionCat != 1 && c.IdTipoSancionCat != 2 && c.IdTipoSancionCat != 11 || c.Bandera == 3 || c.Bandera == 1 || c.Bandera == 4).ToList();
                        TipoMonedas = CatalogosBD.Monedas!;
                        Paises = CatalogosBD.Paises!;
                        TiposVialidades = CatalogosBD.TipoVialidad!;
                    }
                }
                else
                {
                    //Snackbar.Add($"Error al consultar los catalogos", Severity.Error);
                }
            }
            catch (Exception ex)
            {
              //  Snackbar.Add($"Error en el proceso {ex.Message}", Severity.Error);
                //Snackbar.Add("Se guardó la información previa.", Severity.Success);
                //RespuestaRegistro = result!;
            }
        }
        private async Task MostrarOpcionCatalogos()
        {
            _modelo.DatosGenerales!.Sexo = Sexos.FirstOrDefault(x => x.IdSexo == _modelo.DatosGenerales.IdSexoFk);
            _modelo.EmpleoCargoComision!.EntidadFederativa = EntidadesFederativas.FirstOrDefault(x => x.IdEntidadFederativa == _modelo.EmpleoCargoComision.IdEntidadFederativaFK);
            _modelo.EmpleoCargoComision!.NivelOrdenGobierno = NivelOrdenGobierno.FirstOrDefault(x => x.IdNivelOrdenGobierno == _modelo.EmpleoCargoComision.IdNivelOrdenGobiernoFK);
            _modelo.EmpleoCargoComision!.AmbitoPublico = AmbitoPublico.FirstOrDefault(x => x.IdAmbitoPublico == _modelo.EmpleoCargoComision.IdAmbitoPublicoFK);
            _modelo.OrigenProcedimiento!.Clave = ListaOrigenesInvestigacion.FirstOrDefault(x => x.IdOrigenProcedimiento == _modelo.OrigenProcedimiento.IdOrigenProcedimientoCatFK);
            _modelo.Resolucion!.OrdenJurisdiccional = OrdenJurisdiccional.FirstOrDefault(x => x.Id == _modelo.Resolucion.IdOrdenJurisdiccionalFK);
            _modelo.SancionEconomica.Moneda = TipoMonedas.FirstOrDefault(x => x.IdMoneda == _modelo.SancionEconomica.IdMonedaFK);
            //_modelo.SancionEfectivamenteCobrada.Moneda = TipoMonedas.FirstOrDefault(x => x.IdMoneda == _modelo.SancionEfectivamenteCobrada.Moneda.IdMonedaFK);
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
                        _modelo.IdUsuarioFK = informacionPerfil.Usuario.Id;
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

        private void Regresar()
        {
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }
        private async Task<bool> SwitchToPanel()
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
                    bool esValidoEstaOpcion = _isStep1Valid[10];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("INDEMNIZACION"))
                {
                    await _formIndemnizacion!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[11];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("SANCION ECONOMICA"))
                {
                    await _formSancionEconomica!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[12];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("SUSPENSION ACTIVIDADES"))
                {
                    await _formSuspensionActividades!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[13];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("DISOLUCION SOCIEDAD"))
                {
                    await _formDisolucion!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[14];

                    if (esValidoEstaOpcion)
                    {
                        esValidoEstaOpcion = await GuardarTemporal();
                    }

                    todasLasOpcionesSonValidas &= esValidoEstaOpcion;
                }

                if (_options.Contains("OTRO (Especifique)"))
                {
                    await _formOtro!.Validate();
                    bool esValidoEstaOpcion = _isStep1Valid[15];

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

        private bool EsValido(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return true; // permitir vacío si es opcional
            return Regex.IsMatch(valor, @"^[a-zA-ZÁÉÍÓÚáéíóúÑñ0-9 ]+$");
        }

        private List<PlazoPago> PlazoPagos { get; set; } = new List<PlazoPago>
        {
            new PlazoPago { Clave = "1", Valor = "Años" },
            new PlazoPago { Clave = "2", Valor = "Meses" },
            new PlazoPago { Clave = "3", Valor = "Dias" },
        };
        private async Task<bool> GuardarTemporal()
        {            

            await ConsultarIdUsuario();
            if (_options is not null && _options.Any())
            {
                _modelo.MultipleSancion = string.Join(", ", _options);
            }

            var response = await _http.PostAsJsonAsync<PersonaMoralEntidad>(
                "api/FaltasGravesPersonasMorales/AgregarFaltasGravesPersonasMorales",
                _modelo);

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

            var result = await response.Content.ReadFromJsonAsync<RespuestaRegistroFaltasGravesPersonasMorales>();
            if (result?.Response == true && result.Mensaje is null)
            {
                _modelo = result.Data!;               
                Snackbar.Add("INFORMACIÓN GUARDADA CORRECTAMENTE", Severity.Success);
                return true;
            }else if (result?.Response == true && result.Mensaje is not null)
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
                    if (guardado && personaMoralEntidadVistaEdicion is null)
                    {
                        Snackbar.Add("REGISTRO GUARDADO CORRECTAMENTE", Severity.Success);
                        Navigation.NavigateTo("/BuscarServidoresPublicosGravesMoral");
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

        private async Task CancelarForm()
        {

            var parameters = new DialogParameters<ModalCancelarForm>();

            var options = new DialogOptions { CloseOnEscapeKey = false, CloseButton = false, MaxWidth = MaxWidth.Small, BackdropClick = false };

            var dialog = await DialogService.ShowAsync<ModalCancelarForm>("", parameters, options);
            var result = await dialog.Result;


            if (!result.Canceled)
            {
                _modelo.Activo = 0;
                await GuardarTemporal();
                Navigation.NavigateTo("/FaltasGravesPersonasMorales");
            }
                
        }


    }
}
