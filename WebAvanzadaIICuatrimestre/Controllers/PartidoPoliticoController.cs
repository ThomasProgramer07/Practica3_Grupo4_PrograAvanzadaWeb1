using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class PartidoPoliticoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PartidoPoliticoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient Api => _httpClientFactory.CreateClient("Api");

        public async Task<IActionResult> Index()
        {
            var resp = await Api.GetFromJsonAsync<Respuesta<List<RepresentanteLegalDto>>>("api/RepresentanteLegal");
            ViewBag.RepresentanteLegals = resp?.Dato;
            return View();
        }

        public async Task<IActionResult> GetPartidoPoliticos()
        {
            var respuesta = await Api.GetFromJsonAsync<Respuesta<List<PartidoPoliticoDto>>>("api/PartidoPolitico");
            return Json(respuesta);
        }

        public async Task<IActionResult> GetPartidoPoliticoById(int id)
        {
            var respuesta = await Api.GetFromJsonAsync<Respuesta<PartidoPoliticoDto>>($"api/PartidoPolitico/{id}");
            return Json(respuesta);
        }

        public async Task<IActionResult> CreatePartidoPolitico(PartidoPoliticoDto partidoPolitico)
        {
            var http = await Api.PostAsJsonAsync("api/PartidoPolitico", partidoPolitico);
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<PartidoPoliticoDto>>();
            return Json(respuesta);
        }

        public async Task<IActionResult> UpdatePartidoPolitico(PartidoPoliticoDto partidoPolitico)
        {
            var http = await Api.PutAsJsonAsync("api/PartidoPolitico", partidoPolitico);
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<PartidoPoliticoDto>>();
            return Json(respuesta);
        }

        public async Task<IActionResult> DeletePartidoPolitico(int id)
        {
            var http = await Api.DeleteAsync($"api/PartidoPolitico/{id}");
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<PartidoPoliticoDto>>();
            return Json(respuesta);
        }
    }
}