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
            => Ok(await _partidoServicio.GetPartidoPoliticos());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
            => Ok(await _partidoServicio.GetPartidoPoliticoById(id));

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PartidoPoliticoDto partido)
            => Ok(await _partidoServicio.CreatePartidoPolitico(partido));

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] PartidoPoliticoDto partido)
            => Ok(await _partidoServicio.UpdatePartidoPolitico(partido));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
            => Ok(await _partidoServicio.DeletePartidoPolitico(id));
    }
}