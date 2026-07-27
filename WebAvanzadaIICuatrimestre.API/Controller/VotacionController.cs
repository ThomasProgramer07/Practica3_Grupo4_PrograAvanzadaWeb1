using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class VotacionController : Controller
    {
        private readonly HttpClient _httpClient;

        public VotacionController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5063/") // URL exacta de tu API
            };
        }

        [HttpGet]
        public async Task<IActionResult> Resultados()
        {
            var response = await _httpClient.GetAsync("api/Votacion/resultados");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                // Mapea la respuesta genérica de la API (Respuesta.cs)
                using var doc = JsonDocument.Parse(content);
                var root = doc.RootElement;
                
                if (root.TryGetProperty("dato", out var datoElement))
                {
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resultados = JsonSerializer.Deserialize<List<ResultadoDto>>(datoElement.GetRawText(), opciones);
                    return View(resultados ?? new List<ResultadoDto>());
                }
            }

            return View(new List<ResultadoDto>());
        }
    }
}