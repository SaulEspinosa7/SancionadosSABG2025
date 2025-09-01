using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Catalogos
{
	public class PaisCat
	{
		public int id { set; get; }
		public string nombrePais { get; set; } = string.Empty;
		public string codigoISO { get; set; } = string.Empty;

    }
}
