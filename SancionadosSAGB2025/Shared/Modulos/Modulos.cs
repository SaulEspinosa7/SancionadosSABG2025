using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Modulos
{
	public class Modulos
	{
		public string NombreModulo {  get; set; }
		public string IconModulo {  get; set; }
		public List<SubModulos> SubModulos { get; set; }
	}

	public class SubModulos
	{
		public string NombreSubModulo { get; set; }
		public string UrlSubModulo { get; set; }
	}
}
