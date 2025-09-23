using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Catalogos
{
	public class OrigenProcedimientoEntidad : ICatalogo
    {
		public int? IdOrigenProcedimiento { set; get; }
        [JsonPropertyName("Clave")]
        public string? Descripcion { get; set; } = string.Empty;
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int? Activo { get; set; }
        public string? Token { get; set; }
    }
}
