using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.PartidoPolitico;

namespace WebAvanzadaIICuatrimestre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidoPoliticoController : ControllerBase
    {
        private readonly IPartidoPoliticoServicio _partidoServicio;

        public PartidoPoliticoController(IPartidoPoliticoServicio partidoServicio)
        {
            _partidoServicio = partidoServicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var respuesta = await _partidoServicio.GetPartidoPoliticos();
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var respuesta = await _partidoServicio.GetPartidoPoliticoById(id);
            if (!respuesta.esCorrecto)
                return StatusCode(respuesta.codigo, respuesta);
            return Ok(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PartidoPoliticoDto partido)
        {
            var respuesta = await _partidoServicio.CreatePartidoPolitico(partido);
            if (!respuesta.esCorrecto)
                return StatusCode(respuesta.codigo, respuesta);
            return Ok(respuesta);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] PartidoPoliticoDto partido)
        {
            var respuesta = await _partidoServicio.UpdatePartidoPolitico(partido);
            if (!respuesta.esCorrecto)
                return StatusCode(respuesta.codigo, respuesta);
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _partidoServicio.DeletePartidoPolitico(id);
            if (!respuesta.esCorrecto)
                return StatusCode(respuesta.codigo, respuesta);
            return Ok(respuesta);
        }
    }
}