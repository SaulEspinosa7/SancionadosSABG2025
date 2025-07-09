using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Catalogos
{
	public class Catalogos
	{
		public List<EntidadFederativa>? EntidadFederativas { get; set; }
		public List<FaltaCometida>? FaltaCometidas { get; set; }
		public List<NivelOrdenGobierno>? NivelOrdenGobierno { get; set; }
		public List<OrdenJurisdiccional>? OrdenJurisdiccional { get; set; }
		public List<OrigenProcedimiento>? OrigenProcedimiento { get; set; }
		public List<Sexo>? Sexo { get; set; }
		public List<TipoAmonestacion>? TipoAmonestacion { get; set; }
		public List<TipoSancion>? TipoSancion { get; set; }
		public List<TipoVialidad>? TipoVialidad { get; set; }
		public List<AmbitoPublico>? AmbitoPublico { get; set; }
		public List<NivelJerarquico>? NivelJerarquico { get; set; }
	}
}
