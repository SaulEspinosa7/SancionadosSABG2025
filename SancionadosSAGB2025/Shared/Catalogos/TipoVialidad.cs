using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Catalogos
{
	public class TipoVialidad : ICatalogo
	{
		public int? IdTipoVialidad { set; get; }
		public string? Descripcion { set; get; } = string.Empty;
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int? Activo { get; set; }
        public string? Token { get; set; }
    }
}
