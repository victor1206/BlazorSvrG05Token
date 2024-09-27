using BlazorApp1.DTO.CategoriaDTO;
using System.Net.Http.Headers;

namespace BlazorApp1.Services
{
    public class CategoriaService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public CategoriaService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<CategoriaSalidaDTO>> GetCategoria()
        {
            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<List<CategoriaSalidaDTO>>("api/categorias/lista");

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener productos. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener productos.");
            }

        }

        public async Task<CategoriaSalidaDTO> GetCategoriaId(Int32 pId)
        {
            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<CategoriaSalidaDTO>("api/categorias/" + pId);

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener productos. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener productos.");
            }

        }

        public async Task<bool> PostCategoria(CategoriaGuardarDTO categoriaGuardar)
        {
            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync("api/categorias", categoriaGuardar);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al guardaar la categoria. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al guardar la categoria.");
            }

        }

        public async Task<bool> PutCategoria(CategoriaModificarDTO categoriaModificar)
        {
            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PutAsJsonAsync("api/categorias/" + categoriaModificar.id, categoriaModificar);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al modificar la categoria. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al guardar la categoria.");
            }

        }

        public async Task<bool> DeleteCategoria(Int32 pId)
        {
            try
            {
                var token = await _authService.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync("api/categorias/" + pId);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al eliminar categoria. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al eliminar categoria.");
            }

        }
    }
}
