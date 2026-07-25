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
        {
            var respuesta = await _votanteServicio.GetVotantes();
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var respuesta = await _votanteServicio.GetVotanteById(id);
            if (!respuesta.esCorrecto)
                return StatusCode(respuesta.codigo, respuesta);
            return Ok(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VotanteDto votante)
        {
            var respuesta = await _votanteServicio.CreateVotante(votante);
            if (!respuesta.esCorrecto)
                return StatusCode(respuesta.codigo, respuesta);
            return Ok(respuesta);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] VotanteDto votante)
        {
            var respuesta = await _votanteServicio.UpdateVotante(votante);
            if (!respuesta.esCorrecto)
                return StatusCode(respuesta.codigo, respuesta);
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _votanteServicio.DeleteVotante(id);
            if (!respuesta.esCorrecto)
                return StatusCode(respuesta.codigo, respuesta);
            return Ok(respuesta);
        }
    }
}