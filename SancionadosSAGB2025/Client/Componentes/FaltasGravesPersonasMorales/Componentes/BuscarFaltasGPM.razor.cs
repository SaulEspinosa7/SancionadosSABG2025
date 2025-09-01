using Microsoft.AspNetCore.Components;
using MudBlazor;
using SancionadosSAGB2025.Client.Services;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Registros;
using SancionadosSAGB2025.Shared.Sanciones;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace SancionadosSAGB2025.Client.Componentes.FaltasGravesPersonasMorales.Componentes
{
    partial class BuscarFaltasGPM
    {
        [Inject] private NavigationManager Navigation { get; set; }
        private SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG { get; set; } = new();
        private List<PersonaMoralEntidad> faltasDeServidoresPublicosGs { get; set; } = new();
        private PersonaMoralEntidad faltasDeServidoresPublicosGsSeleccionadas { get; set; } = new();
        private TipoEvento TiposEventos { get; set; } = TipoEvento.Principal;
        private int IdUsuario { get; set; } = 0;

        private IEnumerable<PersonaMoralEntidad> Elements = new List<PersonaMoralEntidad>();
        private string _searchString;
        private bool _sortNameByLength;
        private List<string> _events = new();

        protected override async Task OnInitializedAsync()
        {
            await BuscarFaltasServidoresPG();
            await ConsultarIdUsuario();
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
                        IdUsuario = informacionPerfil.Usuario.Id;
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

        public async Task MostrarVistaActualizarFalta(PersonaMoralEntidad addFaltasDeServidoresPublicosG)
        {
            TiposEventos = TipoEvento.Actualizar;
            faltasDeServidoresPublicosGsSeleccionadas = addFaltasDeServidoresPublicosG;
        }

        public async Task MostrarVisualizacionFalta(PersonaMoralEntidad addFaltasDeServidoresPublicosG)
        {
            TiposEventos = TipoEvento.Ver;
            faltasDeServidoresPublicosGsSeleccionadas = addFaltasDeServidoresPublicosG;
        }
  


        public async Task BuscarFaltasServidoresPG()
        {
            try
            {

                var response = await Http.GetFromJsonAsync<List<PersonaMoralEntidad>>("api/FaltasGravesPersonasMorales");


                if (response.Count() > 0)
                {
                    faltasDeServidoresPublicosGs = response;
                    Elements = response;
                }
                else
                {
                    Snackbar.Add("Hubo un error al guardar la información previa.", Severity.Error);                   
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
        // quick filter - filter globally across multiple columns with the same input
        private Func<PersonaMoralEntidad, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Expediente?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            if (x.DatosGenerales?.Curp?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            if (x.DatosGenerales?.Rfc?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            if (x.DatosGenerales?.Nombres?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            if (x.DatosGenerales?.PrimerApellido?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            if (x.DatosGenerales?.SegundoApellido?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
                return true;


            return false;
        };

        // events
        void RowClicked(DataGridRowClickEventArgs<PersonaMoralEntidad> args)
        {
            _events.Insert(0, $"Event = RowClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void RowRightClicked(DataGridRowClickEventArgs<PersonaMoralEntidad> args)
        {
            _events.Insert(0, $"Event = RowRightClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void SelectedItemsChanged(HashSet<PersonaMoralEntidad> items)
        {
            _events.Insert(0, $"Event = SelectedItemsChanged, Data = {System.Text.Json.JsonSerializer.Serialize(items)}");
        }

        public enum TipoEvento
        {
            Principal = 0,
            Ver = 1,
            Actualizar = 2,
            Eliminar = 3
        }
    }
}
