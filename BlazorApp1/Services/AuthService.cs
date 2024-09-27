using BlazorApp1.DTO;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorApp1.Services
{
    public class AuthService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly HttpClient _httpClient;
        private string? _token;

        public AuthService(ProtectedLocalStorage localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        //Enviar datos a endpoint login
        public async Task<string> Login(UsuarioLoginDTO userSession)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", userSession);
                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadFromJsonAsync<string>();
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }


                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Guardar token en el navegador
        public async Task SetToken(string token)
        {
            _token = token;
            await _localStorage.SetAsync("token", token);
        }

        //Obtener token del navegador
        public async Task<string> GetToken()
        {

            var localStorageResult = await _localStorage.GetAsync<string>("token");

            if (string.IsNullOrEmpty(_token))
            {
                if (!localStorageResult.Success || string.IsNullOrEmpty(localStorageResult.Value))
                {
                    _token = null;
                    return null;
                }

                _token = localStorageResult.Value;
            }

            return _token;
        }

        //Verificar si el usuario está autenticado
        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();

            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }

        //verificar si el token ha expirado
        public bool IsTokenExpired(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            return jwtToken.ValidTo < DateTime.UtcNow;
        }

        //Cerrar sesión
        public async Task Logout()
        {
            _token = null;
            await _localStorage.DeleteAsync("token");
        }
    }
}
