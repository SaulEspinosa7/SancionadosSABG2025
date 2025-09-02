using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Moral;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Componentes.FaltasGravesPersonasMorales.Componentes
{
    partial class VerFaltasGPM
    {
        [Parameter] public PersonaMoralEntidad? faltasDeServidoresPublicosMorales { get; set; }

        protected override void OnParametersSet()
        {
            if (faltasDeServidoresPublicosMorales is null)
            {
                faltasDeServidoresPublicosMorales = new PersonaMoralEntidad();
            }

            faltasDeServidoresPublicosMorales.DatosGenerales ??= new DatosGeneralesMorales();
            faltasDeServidoresPublicosMorales.DirectorGeneral ??= new DirectorGeneral();
            faltasDeServidoresPublicosMorales.DirectorGeneral.RepresentanteLegal ??= new RepresentanteLegal();
            faltasDeServidoresPublicosMorales.EmpleoCargoComision ??= new EmpleoCargoComision();
            faltasDeServidoresPublicosMorales.EmpleoCargoComision.EntidadFederativa ??= new EntidadFederativa();
            faltasDeServidoresPublicosMorales.EmpleoCargoComision.AmbitoPublico ??= new AmbitoPublico();
            faltasDeServidoresPublicosMorales.EmpleoCargoComision.NivelOrdenGobierno ??= new NivelOrdenGobierno();
            faltasDeServidoresPublicosMorales.OrigenProcedimiento ??= new OrigenProcedimiento();
            faltasDeServidoresPublicosMorales.FaltaCometida ??= new FaltaCometida();
            faltasDeServidoresPublicosMorales.Resolucion ??= new Resolucion();
            faltasDeServidoresPublicosMorales.Inhabilitacion ??= new Inhabilitacion();

            faltasDeServidoresPublicosMorales.Indeminizacion ??= new IndemnizacionMoral();
            faltasDeServidoresPublicosMorales.Indeminizacion.SancionEfectivamenteCobrada ??= new SancionEfectivamenteCobradaMoral();
            faltasDeServidoresPublicosMorales.Indeminizacion.SancionEfectivamenteCobrada.Moneda ??= new Moneda();

            faltasDeServidoresPublicosMorales.PlazoPago ??= new PlazoPago();
            faltasDeServidoresPublicosMorales.SancionEfectivamenteCobrada ??= new SancionEfectivamenteCobrada();
            faltasDeServidoresPublicosMorales.SancionEconomica ??= new SancionEconomica();
            faltasDeServidoresPublicosMorales.SuspensionActividades ??= new SuspensionActividades();
            faltasDeServidoresPublicosMorales.DisolucionSociedad ??= new DisolucionSociedad();
            faltasDeServidoresPublicosMorales.Otro ??= new Otro();
        }

    }
}

