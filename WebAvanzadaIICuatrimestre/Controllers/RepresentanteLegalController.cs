using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class RepresentanteLegalController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RepresentanteLegalController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient Api => _httpClientFactory.CreateClient("Api");

        public IActionResult Index() => View();

        public async Task<IActionResult> GetRepresentanteLegals()
        {
            var respuesta = await Api.GetFromJsonAsync<Respuesta<List<RepresentanteLegalDto>>>("api/RepresentanteLegal");
            return Json(respuesta);
        }

        public async Task<IActionResult> GetRepresentanteLegalById(int id)
        {
            var respuesta = await Api.GetFromJsonAsync<Respuesta<RepresentanteLegalDto>>($"api/RepresentanteLegal/{id}");
            return Json(respuesta);
        }

        public async Task<IActionResult> CreateRepresentanteLegal(RepresentanteLegalDto representanteLegal)
        {
            var http = await Api.PostAsJsonAsync("api/RepresentanteLegal", representanteLegal);
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<RepresentanteLegalDto>>();
            return Json(respuesta);
        }

        public async Task<IActionResult> UpdateRepresentanteLegal(RepresentanteLegalDto representanteLegal)
        {
            var http = await Api.PutAsJsonAsync("api/RepresentanteLegal", representanteLegal);
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<RepresentanteLegalDto>>();
            return Json(respuesta);
        }

        public async Task<IActionResult> DeleteRepresentanteLegal(int id)
        {
            var http = await Api.DeleteAsync($"api/RepresentanteLegal/{id}");
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<RepresentanteLegalDto>>();
            return Json(respuesta);
        }
    }
}