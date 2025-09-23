using Microsoft.AspNetCore.Components;
using SancionadosSAGB2025.Shared.Catalogos;
using SancionadosSAGB2025.Shared.Grave;
using SancionadosSAGB2025.Shared.Sanciones;

namespace SancionadosSAGB2025.Client.Componentes.FaltasDeServidoresPúblicosG.Components
{
	partial class EditarFaltasSPG
	{
		[Parameter] public FaltasGravesEntidad faltasDeServidoresPublicosG { get; set; } = new();

		//private FaltasDeServidoresPublicosG? _faltasDeServidoresPublicosG { set; get; } = new();

		protected override async Task OnParametersSetAsync()
		{
			//_faltasDeServidoresPublicosG = await ConvertToEntity(faltasDeServidoresPublicosG);
            if (faltasDeServidoresPublicosG is null)
            {
                faltasDeServidoresPublicosG = new();
            }

            faltasDeServidoresPublicosG.DatosGenerales ??= new();
            faltasDeServidoresPublicosG.EmpleoCargoComision ??= new EmpleoCargoComision();
            faltasDeServidoresPublicosG.NivelJerarquico ??= new NivelJerarquico();
            faltasDeServidoresPublicosG.OrigenProcedimiento ??= new OrigenProcedimiento();
            faltasDeServidoresPublicosG.FaltaCometida ??= new FaltaCometida();
            faltasDeServidoresPublicosG.Resolucion ??= new Resolucion();
            faltasDeServidoresPublicosG.Suspension ??= new Suspension();
            faltasDeServidoresPublicosG.DestitucionEmpleo ??= new DestitucionEmpleo();
            faltasDeServidoresPublicosG.SancionEconomica ??= new SancionEconomica();
            faltasDeServidoresPublicosG.SancionEconomica.SancionEfectivamenteCobrada ??= new();
            faltasDeServidoresPublicosG.Inhabilitacion ??= new Inhabilitacion();
			faltasDeServidoresPublicosG.SancionEconomica.SancionEfectivamenteCobrada ??= new();
            faltasDeServidoresPublicosG.Otro ??= new Otro();
         //   _faltasDeServidoresPublicosG.Sancion ??= new Sancion();
        }

		public async Task<FaltasDeServidoresPublicosG> ConvertToEntity(AddFaltasDeServidoresPublicosG input)
		{
			return new FaltasDeServidoresPublicosG
			{
				Id = input.Id,
				Fecha = input.Fecha,
				Expediente = input.Expediente,
                IdDatosGeneralesFK = input.IdDatosGeneralesFK,
				DatosGenerales = input.DatosGenerales,
				IdEmpleoCargoComisionFK = input.IdEmpleoCargoComisionFK,
				EmpleoCargoComision = input.EmpleoCargoComision,
				IdNivelJerarquicoFK = input.IdNivelJerarquicoFK,
				NivelJerarquico = input.NivelJerarquico,
				IdOrigenProcedimientoFK = input.IdOrigenProcedimientoFK,
				OrigenProcedimiento = input.OrigenProcedimiento,
				IdFaltaCometidaFK = input.IdFaltaCometidaFK,
				FaltaCometida = input.FaltaCometida,
				IdResolucion = input.IdResolucion,
				Resolucion = input.Resolucion,
				IdTipoSancionFK = input.IdTipoSancionFK,
				IdSuspension = input.IdSuspension,
				Suspension = input.Suspension,
				IdDestitucionEmpleo = input.IdDestitucionEmpleo,
				DestitucionEmpleo = input.DestitucionEmpleo,
				IdSancionEconomicaFK = input.IdSancionEconomicaFK,
				SancionEconomica = input.SancionEconomica,
				IdSancionEfectivamenteCobradaFK = input.IdSancionEfectivamenteCobradaFK,
				SancionEfectivamenteCobrada = input.SancionEfectivamenteCobrada,
				IdInhabilitacionFK = input.IdInhabilitacionFK,
				Inhabilitacion = input.Inhabilitacion,
				IdOtro = input.IdOtro,
				Otro = input.Otro,
				Activo = input.Activo ?? 1,
				MultipleSancion = input.MultipleSancion,

				// Mapeo especial: se crea Sancion con Fecha y Expediente desde input
				//Sancion = new Sancion
				//{
				//	Fecha = input.Fecha,
				//	Expediente = input.Expediente
				//	// puedes completar con otros campos si los necesitas
				//}

			};
		}
	}
}
