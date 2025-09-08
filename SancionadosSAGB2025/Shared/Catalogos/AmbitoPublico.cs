using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Catalogos
{
	public class AmbitoPublico : ICatalogo
    {
		public int? IdAmbitoPublico { set; get; }
		public string? Descripcion { get; set; } = string.Empty;
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int? Activo { get; set; }
	}
}
