using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Componentes.FaltasDeServidoresPúblicosN.Components
{
    partial class VerFaltasSPNG
    {
        [Parameter] public AddFaltasDeServidoresPublicosNoGraves faltasDeServidoresPublicosNG { get; set; } = new();

        protected override void OnParametersSet()
        {
            // si viene null el objeto completo, lo inicializamos
            if (faltasDeServidoresPublicosNG is null)
                faltasDeServidoresPublicosNG = new AddFaltasDeServidoresPublicosNoGraves();

            // aquí inicializamos solo lo necesario, pero sin pisar Id
            faltasDeServidoresPublicosNG.DatosGenerales ??= new DatosGenerales();
            faltasDeServidoresPublicosNG.Inhabilitacion ??= new Inhabilitacion();
            faltasDeServidoresPublicosNG.TipoAmonestacion ??= new SancionadosSAGB2025.Shared.Catalogos.TipoAmonestacion();
            faltasDeServidoresPublicosNG.Resolucion ??= new Resolucion();
            faltasDeServidoresPublicosNG.Suspension ??= new Suspension();
            faltasDeServidoresPublicosNG.EmpleoCargoComision ??= new EmpleoCargoComision();
            faltasDeServidoresPublicosNG.OrigenProcedimiento ??= new OrigenProcedimiento();
            faltasDeServidoresPublicosNG.Otro ??= new Otro();
            faltasDeServidoresPublicosNG.FaltaCometida ??= new FaltaCometida();
            faltasDeServidoresPublicosNG.DestitucionEmpleo ??= new DestitucionEmpleo();
            faltasDeServidoresPublicosNG.NivelJerarquico ??= new NivelJerarquico();
        }

    }
}
