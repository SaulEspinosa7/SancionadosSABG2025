using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Componentes.FaltasGravesPersonasFísicas.Componentes
{
    partial class EditarFaltasGPF
    {
        [Parameter] public AddFaltasGravesPersonasFisicas? faltasDeServidoresPublicosFisicas { get; set; }

        protected override void OnParametersSet()
        {
            // Si el objeto principal es null, lo inicializamos.
            faltasDeServidoresPublicosFisicas ??= new AddFaltasGravesPersonasFisicas();

            // Inicializamos cada una de las propiedades anidadas una sola vez.
            faltasDeServidoresPublicosFisicas.DatosGenerales ??= new DatosGenerales();
            faltasDeServidoresPublicosFisicas.DatosGenerales.DomicilioExtranjero ??= new DomicilioExtranjero();
            faltasDeServidoresPublicosFisicas.DatosGenerales.DomicilioMexico ??= new DomicilioMexico();
            faltasDeServidoresPublicosFisicas.EmpleoCargoComision ??= new EmpleoCargoComision();
            faltasDeServidoresPublicosFisicas.OrigenProcedimiento ??= new OrigenProcedimiento();
            faltasDeServidoresPublicosFisicas.FaltaCometida ??= new FaltaCometida();
            faltasDeServidoresPublicosFisicas.Resolucion ??= new Resolucion();
            faltasDeServidoresPublicosFisicas.Inhabilitacion ??= new Inhabilitacion();
            faltasDeServidoresPublicosFisicas.Indeminizacion ??= new Indemnizacion();
            faltasDeServidoresPublicosFisicas.PlazoPago ??= new PlazoPagos(); // ¡Esta faltaba!
            faltasDeServidoresPublicosFisicas.SancionEconomica ??= new SancionEconomica();
            faltasDeServidoresPublicosFisicas.SancionEconomica.SancionEfectivamenteCobrada ??= new();
            faltasDeServidoresPublicosFisicas.Indeminizacion.IndeminizacionECobrada ??= new();
            faltasDeServidoresPublicosFisicas.SancionEfectivamenteCobrada ??= new SancionEfectivamenteCobrada();
            faltasDeServidoresPublicosFisicas.Otro ??= new Otro();

            // Si tu modelo tiene una propiedad como esta, también inicialízala
            // faltasDeServidoresPublicosFisicas.DestitucionEmpleo ??= new DestitucionEmpleo();
        }
    }
}
