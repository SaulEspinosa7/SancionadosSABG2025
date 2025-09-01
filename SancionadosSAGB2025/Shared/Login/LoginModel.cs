using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SancionadosSAGB2025.Shared.Login
{
	public class LoginModel
	{
		[Required(ErrorMessage = "El usuario es requerido")]
		public string User { get; set; } = "annabel@hotmail.com";

		[Required(ErrorMessage = "El password es requerido")]
		public string Password { get; set; } = "12345";
	}

	public class AutenticacionResponse
	{
		public bool Autenticado { get; set; }
		public string Mensaje { get; set; }
		public Usuario Usuario { get; set; }
		public string Token { get; set; }
	}
	public class TokenResponse
	{
		public string Token { get; set; }
	}

	public class Usuario
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string User { get; set; }
		public string Password { get; set; }
		public DateTime FechaRegistro { get; set; }
		public DateTime FechaActualizacion { get; set; }
		public int Activo { get; set; }
	}

}
