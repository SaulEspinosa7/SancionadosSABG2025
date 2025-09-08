using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        [Inject] private IJSRuntime JS { get; set; }
        private SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG { get; set; } = new();
        private List<PersonaMoralEntidad> faltasDeServidoresPublicosGs { get; set; } = new();
        private PersonaMoralEntidad faltasDeServidoresPublicosGsSeleccionadas { get; set; } = new();
        private TipoEvento TiposEventos { get; set; } = TipoEvento.Principal;
        private int IdUsuario { get; set; } = 0;

        private List<PersonaMoralEntidad> Elements = new List<PersonaMoralEntidad>();
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
        private async Task GenerarExcel()
        {
            try
            {
                if (Elements == null || !Elements.Any())
                    return; // No hay datos para procesar

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Personas Morales");

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromArgb(0x2C3E50); 
                headerRow.Style.Font.FontColor = XLColor.White;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRow.Height = 20; // Aumentamos un poco la altura de la fila del encabezado

                // Escribimos los valores en los encabezados
                worksheet.Cell(1, 1).Value = "Fecha";
                worksheet.Cell(1, 2).Value = "Expediente";
                worksheet.Cell(1, 3).Value = "Denominación o Razón Social";
                worksheet.Cell(1, 4).Value = "RFC";
                worksheet.Cell(1, 5).Value = "Objeto Social"; // ⚠️ ¡Revisa el punto 5 más abajo!
                worksheet.Cell(1, 6).Value = "Falta (s) Cometidas"; // ⚠️ ¡Revisa el punto 5 más abajo!

                // --- 2. FORMATO DE COLUMNAS (antes de llenarlas) ---
                // Aplicamos el formato de fecha a toda la primera columna.
                worksheet.Column(1).Style.DateFormat.Format = "dd/MM/yyyy";
                worksheet.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Centramos las fechas


                // --- 3. LLENADO DE DATOS ---
                for (int i = 0; i < Elements.Count; i++)
                {
                    var row = i + 2;

                    // Asignamos el valor directamente. Si es null, la celda quedará vacía.
                    worksheet.Cell(row, 1).Value = Elements[i].Fecha;

                    worksheet.Cell(row, 2).Value = Elements[i].Expediente ?? "";
                    worksheet.Cell(row, 3).Value = Elements[i].DatosGenerales?.Nombres ?? "";
                    worksheet.Cell(row, 4).Value = Elements[i].DatosGenerales?.Rfc ?? "";
                    worksheet.Cell(row, 5).Value = Elements[i].DatosGenerales?.PrimerApellido ?? ""; // Dejo tu línea original para que la corrijas.
                    worksheet.Cell(row, 6).Value = Elements[i].MultipleSancion ?? ""; // Dejo tu línea original para que la corrijas.
                }


                // --- 4. AJUSTES FINALES (¡La magia ocurre aquí!) ---
                // Autoajustamos el ancho de todas las columnas según su contenido.
                worksheet.Columns().AdjustToContents();

                // Congelamos la primera fila para que los encabezados siempre estén visibles al hacer scroll.
                worksheet.SheetView.FreezeRows(1);


                // --- 5. GUARDADO Y DESCARGA ---
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                await JS.InvokeVoidAsync("BlazorDownloadFile",
                    "Reporte_Personas_Morales.xlsx", // Nombre de archivo mejorado
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
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
