using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Votante;

namespace WebAvanzadaIICuatrimestre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotanteController : ControllerBase
    {
        private readonly IVotanteServicio _votanteServicio;

        public VotanteController(IVotanteServicio votanteServicio)
        {
            _votanteServicio = votanteServicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
            => Ok(await _votanteServicio.GetVotantes());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
            => Ok(await _votanteServicio.GetVotanteById(id));

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VotanteDto votante)
            => Ok(await _votanteServicio.CreateVotante(votante));

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] VotanteDto votante)
            => Ok(await _votanteServicio.UpdateVotante(votante));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
            => Ok(await _votanteServicio.DeleteVotante(id));
    }
}