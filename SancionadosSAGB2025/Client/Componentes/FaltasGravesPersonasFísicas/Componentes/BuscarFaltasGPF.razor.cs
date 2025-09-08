using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SancionadosSAGB2025.Client.Services;
using SancionadosSAGB2025.Shared.Login;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Componentes.FaltasGravesPersonasFísicas.Componentes
{
    partial class BuscarFaltasGPF
    {
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IJSRuntime JS { get; set; }
        private SearchFaltasDeServidoresPublicosG searchFaltasDeServidoresPublicosG { get; set; } = new();
        private List<AddFaltasGravesPersonasFisicas> faltasDeServidoresPublicosGs { get; set; } = new();
        private AddFaltasGravesPersonasFisicas faltasDeServidoresPublicosGsSeleccionadas { get; set; } = new();
        private TipoEvento TiposEventos { get; set; } = TipoEvento.Principal;
        private int IdUsuario { get; set; } = 0;

        private List<AddFaltasGravesPersonasFisicas> Elements = new List<AddFaltasGravesPersonasFisicas>();
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

        public async Task MostrarVistaActualizarFalta(AddFaltasGravesPersonasFisicas addFaltasDeServidoresPublicosG)
        {
            TiposEventos = TipoEvento.Actualizar;
            faltasDeServidoresPublicosGsSeleccionadas = addFaltasDeServidoresPublicosG;
        }

        public async Task MostrarVisualizacionFalta(AddFaltasGravesPersonasFisicas addFaltasDeServidoresPublicosG)
        {
            TiposEventos = TipoEvento.Ver;
            faltasDeServidoresPublicosGsSeleccionadas = addFaltasDeServidoresPublicosG;
        }

        public async Task BuscarFaltasServidoresPG()
        {
            try
            {
                var result = await FaltasGravesPersonasFisicasService.ObtenerFaltasGravesPersonasFisicas(searchFaltasDeServidoresPublicosG);

                if (result.Count() > 0)
                {
                    faltasDeServidoresPublicosGs = result;
                    Elements = result;
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
        // quick filter - filter globally across multiple columns with the same input
        private Func<AddFaltasGravesPersonasFisicas, bool> _quickFilter => x =>
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
        void RowClicked(DataGridRowClickEventArgs<AddFaltasGravesPersonasFisicas> args)
        {
            _events.Insert(0, $"Event = RowClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void RowRightClicked(DataGridRowClickEventArgs<AddFaltasGravesPersonasFisicas> args)
        {
            _events.Insert(0, $"Event = RowRightClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void SelectedItemsChanged(HashSet<AddFaltasGravesPersonasFisicas> items)
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
                var worksheet = workbook.Worksheets.Add("Personas Físicas");

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromArgb(0x2C3E50);
                headerRow.Style.Font.FontColor = XLColor.White;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRow.Height = 20; // Aumentamos un poco la altura de la fila del encabezado

            
                worksheet.Cell(1, 1).Value = "Fecha";
                worksheet.Cell(1, 2).Value = "Expediente";
                worksheet.Cell(1, 3).Value = "Nombre Completo";
                worksheet.Cell(1, 4).Value = "RFC";
                worksheet.Cell(1, 5).Value = "Falta (s) Cometidas"; 
                worksheet.Column(1).Style.DateFormat.Format = "dd/MM/yyyy";
                worksheet.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; 


              
                for (int i = 0; i < Elements.Count; i++)
                {
                    var row = i + 2;

          
                    worksheet.Cell(row, 1).Value = Elements[i].Fecha;
                    worksheet.Cell(row, 2).Value = Elements[i].Expediente ?? "";
                    var partesDelNombre = new[]
                     {
                        Elements[i].DatosGenerales?.Nombres,
                        Elements[i].DatosGenerales?.PrimerApellido,
                        Elements[i].DatosGenerales?.SegundoApellido // Asumo que también tienes un segundo apellido
                    };

                    // 2. Usamos string.Join para unir solo las partes que NO son nulas o vacías.
                    //    Automáticamente pone un espacio entre cada parte válida.
                    var nombreCompleto = string.Join(" ", partesDelNombre.Where(p => !string.IsNullOrWhiteSpace(p)));

                    // 3. Asignamos el resultado a la celda.
                    worksheet.Cell(row, 3).Value = nombreCompleto;

                    worksheet.Cell(row, 4).Value = Elements[i].DatosGenerales?.Rfc ?? "";
                    worksheet.Cell(row, 5).Value = Elements[i].MultipleSancion ?? ""; // Dejo tu línea original para que la corrijas.
                }


                worksheet.Columns().AdjustToContents();

                worksheet.SheetView.FreezeRows(1);


               
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                await JS.InvokeVoidAsync("BlazorDownloadFile",
                    "Reporte_Personas_Fisicas.xlsx", // Nombre de archivo mejorado
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
