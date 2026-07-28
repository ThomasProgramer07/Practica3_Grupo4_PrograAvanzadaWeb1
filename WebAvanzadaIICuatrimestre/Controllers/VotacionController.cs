using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class VotacionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VotacionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient Api => _httpClientFactory.CreateClient("Api");

        public async Task<IActionResult> Index()
        {
            var respPartidos = await Api.GetFromJsonAsync<Respuesta<List<PartidoPoliticoDto>>>("api/PartidoPolitico");
            ViewBag.Partidos = respPartidos?.Dato ?? new List<PartidoPoliticoDto>();
            return View();
        }

        public async Task<IActionResult> Votar(VotoDto voto)
        {
            if (voto == null || string.IsNullOrWhiteSpace(voto.Identificacion) || voto.FkpartidoPolitico <= 0)
            {
                return Json(new Respuesta<VotoDto>
                {
                    esCorrecto = false,
                    mensaje = "Por favor ingrese la cédula y seleccione un partido válido.",
                    codigo = 400
                });
            }

            var http = await Api.PostAsJsonAsync("api/Votacion/votar", voto);
            if (!http.IsSuccessStatusCode)
            {
                return Json(new Respuesta<VotoDto>
                {
                    esCorrecto = false,
                    mensaje = $"Error al procesar el voto: {http.StatusCode}",
                    codigo = (int)http.StatusCode
                });
            }
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<VotoDto>>();
            return Json(respuesta);
        }

        public async Task<IActionResult> Resultados()
        {
            var response = await Api.GetFromJsonAsync<Respuesta<List<ResultadoDto>>>("api/Votacion/resultados");
            var resultados = response?.Dato ?? new List<ResultadoDto>();
            return View(resultados);
        }
    }
}