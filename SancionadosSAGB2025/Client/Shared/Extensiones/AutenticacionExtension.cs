using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SancionadosSAGB2025.Shared.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace SancionadosSAGB2025.Client.Shared.Extensiones
{
    public class AutenticacionExtension(ILocalStorageService localStorageService, IConfiguration configuration, HttpClient httpClient) : AuthenticationStateProvider
    {

        private readonly NavigationManager navigationManager;
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

       

        public async Task ActualizarEstadoAutenticacion(AutenticacionResponse? sesionUsuario)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal;

                if (sesionUsuario != null)
                {
                    claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Role,sesionUsuario.Usuario.User)
                }, "JwtAuth"));

                    await localStorageService.SetItemAsync("sesionUsuario", sesionUsuario.Token);
                }
                else
                {
                    claimsPrincipal = _sinInformacion;
                    await localStorageService.RemoveItemAsync("sesionUsuario");
                }

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            } 

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AutenticacionResponse? sesionUsuario = null;

            try
            {
                sesionUsuario = await localStorageService.GetItemAsync<AutenticacionResponse>("sesionUsuario");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializando sesión: {ex.Message}");
                await localStorageService.RemoveItemAsync("sesionUsuario");
            }

            if (sesionUsuario?.Usuario != null)
            {
                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Role, sesionUsuario.Usuario.User ?? string.Empty)
        }, "JwtAuth"));

                return new AuthenticationState(claimPrincipal);
            }

            return new AuthenticationState(_sinInformacion);
        }


        //    //public async Task<bool> CheckTokenExpiration(string token)
        //    //{
        //    //    try
        //    //    {
        //    //        if (DateTime.Now <= GetTimeExpiredToken(token))
        //    //            return true;

        //    //        await Logout(token);
        //    //        return false;
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        Console.WriteLine(ex.Message);
        //    //        await _sessionStorage.RemoveItemAsync("sesionUsuario");
        //    //        return false;
        //    //    }
        //    //}

        //    //private static DateTime GetTimeExpiredToken(string token)
        //    //{
        //    //    Claim expiration = ParsearClaimsDelJwt(token)
        //    //                            .First(claim => claim.Type == "exp");
        //    //    return DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expiration.Value)).LocalDateTime;
        //    //}

        //    //private static IEnumerable<Claim> ParsearClaimsDelJwt(string? token)
        //    //{
        //    //    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //    //    var tokenDeserializado = jwtSecurityTokenHandler.ReadJwtToken(token);

        //    //    return tokenDeserializado.Claims.Append(
        //    //        new Claim(ClaimTypes.Role, tokenDeserializado.Claims.First(claim => claim.Type == "tipo_usuario").Value)
        //    //    );
        //    //}
        //    //public async Task<AuthenticationState> BuildAuthenticationState(string token)
        //    //{
        //    //    token = await RefreshToken(token);
        //    //    var res = await CheckTokenExpiration(token);
        //    //    if (!res)
        //    //    {
        //    //        return null;
        //    //    }
        //    //    else
        //    //    {
        //    //        var listClaims = ParsearClaimsDelJwt(token);

        //    //        IEnumerable<Claim> claims = listClaims.ToList();

        //    //        return new AuthenticationState(
        //    //            new ClaimsPrincipal(
        //    //                new ClaimsIdentity(
        //    //                    claims,
        //    //                    "jwt"
        //    //                )
        //    //            )
        //    //        );
        //    //    }
        //    //}
        //    //private async Task<string> RefreshToken(string token)
        //    //{
        //    //    if (GetTimeExpiredToken(token).Subtract(DateTime.Now).Minutes > 5) return token;

        //    //    var sesionUsuario = await _sessionStorage.GetItemAsync<UserDTO>("sesionUsuario");

        //    //    var loginResponse = await _httpclient.PostAsJsonAsync("/api/User/RefreshToken", sesionUsuario);
        //    //    var responseObject = await loginResponse.Content.ReadFromJsonAsync<ResponseRefreshToken>();

        //    //    if (responseObject!.Code == 200)
        //    //    {
        //    //        sesionUsuario.RefreshToken = responseObject.Data?.RefreshToken;
        //    //        sesionUsuario.token = responseObject.Data?.AccessToken;
        //    //        await _sessionStorage.SetItemAsync("sesionUsuario", sesionUsuario);

        //    //    }

        //    //    return string.IsNullOrEmpty(responseObject.Data?.AccessToken) ? token : responseObject.Data?.AccessToken!;
        //    //}

        private async Task Logout(string token)
        {
            AutenticacionResponse userDTO = new();
            userDTO.Token = token;
            var loginResponse = await httpClient.PostAsJsonAsync("/api/User/VerificarLogout", userDTO);
            await localStorageService.RemoveItemAsync("sesionUsuario");
        }
    }
}
