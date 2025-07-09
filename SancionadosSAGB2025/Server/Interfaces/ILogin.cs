using SancionadosSAGB2025.Shared.Login;

namespace SancionadosSAGB2025.Server.Interfaces
{
	public interface ILogin
	{
		Task<AutenticacionResponse> LoginAsync(LoginModel loginModel);
	}
}
