using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Catalogos
{
	public class Catalogos
	{
		public List<EntidadFederativaEntidad>? EntidadFederativas { get; set; }
		public List<FaltaCometidaEntidad>? FaltaCometidas { get; set; }
		public List<NivelOrdenGobierno>? NivelOrdenGobierno { get; set; }
		public List<OrdenJurisdiccional>? OrdenJurisdiccional { get; set; }
		public List<OrigenProcedimientoEntidad>? OrigenProcedimiento { get; set; }
		public List<Sexo>? Sexo { get; set; }
		public List<TipoAmonestacion>? TipoAmonestacion { get; set; }
		public List<TipoSancion>? TipoSancion { get; set; }
		public List<TipoVialidad>? TipoVialidad { get; set; }
		public List<AmbitoPublico>? AmbitoPublico { get; set; }
		public List<NivelJerarquicoEntidad>? NivelJerarquico { get; set; }
		public List<MonedaCat>? Monedas { get; set; }
		public List<PaisCat>? Paises { get; set; }
    }
}
