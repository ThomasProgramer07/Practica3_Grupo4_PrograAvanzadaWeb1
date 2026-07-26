using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class VotanteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VotanteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // atajo para no repetir CreateClient en cada método
        private HttpClient Api => _httpClientFactory.CreateClient("Api");

        public IActionResult Index() => View();

        public async Task<IActionResult> GetVotantes()
        {
            var respuesta = await Api.GetFromJsonAsync<Respuesta<List<VotanteDto>>>("api/Votante");
            return Json(respuesta);
        }

        public async Task<IActionResult> GetVotanteById(int id)
        {
            var respuesta = await Api.GetFromJsonAsync<Respuesta<VotanteDto>>($"api/Votante/{id}");
            return Json(respuesta);
        }

        public async Task<IActionResult> CreateVotante(VotanteDto votante)
        {
            var http = await Api.PostAsJsonAsync("api/Votante", votante);
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<VotanteDto>>();
            return Json(respuesta);
        }

        public async Task<IActionResult> UpdateVotante(VotanteDto votante)
        {
            var http = await Api.PutAsJsonAsync("api/Votante", votante);
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<VotanteDto>>();
            return Json(respuesta);
        }

        public async Task<IActionResult> DeleteVotante(int id)
        {
            var http = await Api.DeleteAsync($"api/Votante/{id}");
            var respuesta = await http.Content.ReadFromJsonAsync<Respuesta<VotanteDto>>();
            return Json(respuesta);
        }
    }
}