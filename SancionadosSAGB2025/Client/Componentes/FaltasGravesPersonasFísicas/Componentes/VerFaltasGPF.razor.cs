using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Componentes.FaltasGravesPersonasFísicas.Componentes
{
    partial class VerFaltasGPF
    {
        [Parameter] public AddFaltasGravesPersonasFisicas? faltasDeServidoresPublicosFisicas { get; set; }
        protected override void OnParametersSet()
        {
            // Si el objeto principal es null, lo inicializamos.
            faltasDeServidoresPublicosFisicas ??= new AddFaltasGravesPersonasFisicas();

            // Inicializamos cada una de las propiedades anidadas una sola vez.
            faltasDeServidoresPublicosFisicas.DatosGenerales ??= new DatosGenerales();
            faltasDeServidoresPublicosFisicas.EmpleoCargoComision ??= new EmpleoCargoComision();
            faltasDeServidoresPublicosFisicas.EmpleoCargoComision.EntidadFederativa ??= new SancionadosSAGB2025.Shared.Catalogos.EntidadFederativa();
            faltasDeServidoresPublicosFisicas.EmpleoCargoComision.NivelOrdenGobierno ??= new SancionadosSAGB2025.Shared.Catalogos.NivelOrdenGobierno();
            faltasDeServidoresPublicosFisicas.EmpleoCargoComision.AmbitoPublico ??= new SancionadosSAGB2025.Shared.Catalogos.AmbitoPublico();
            faltasDeServidoresPublicosFisicas.OrigenProcedimiento ??= new OrigenProcedimiento();
            faltasDeServidoresPublicosFisicas.FaltaCometida ??= new FaltaCometida();
            faltasDeServidoresPublicosFisicas.Resolucion ??= new Resolucion();
            faltasDeServidoresPublicosFisicas.Inhabilitacion ??= new Inhabilitacion();
            faltasDeServidoresPublicosFisicas.Indeminizacion ??= new Indemnizacion();
            faltasDeServidoresPublicosFisicas.PlazoPago ??= new PlazoPagos(); // ¡Esta faltaba!
            faltasDeServidoresPublicosFisicas.SancionEconomica ??= new SancionEconomica();
            faltasDeServidoresPublicosFisicas.SancionEfectivamenteCobrada ??= new SancionEfectivamenteCobrada();
            faltasDeServidoresPublicosFisicas.Otro ??= new Otro();

            // Si tu modelo tiene una propiedad como esta, también inicialízala
            // faltasDeServidoresPublicosFisicas.DestitucionEmpleo ??= new DestitucionEmpleo();
        }
    }
}
