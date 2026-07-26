using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.RepresentanteLegal;

namespace WebAvanzadaIICuatrimestre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentanteLegalController : ControllerBase
    {
        private readonly IRepresentanteLegalServicio _servicio;

        public RepresentanteLegalController(IRepresentanteLegalServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
            => Ok(await _servicio.GetRepresentanteLegals());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
            => Ok(await _servicio.GetRepresentanteLegalById(id));

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RepresentanteLegalDto dto)
            => Ok(await _servicio.CreateRepresentanteLegal(dto));

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] RepresentanteLegalDto dto)
            => Ok(await _servicio.UpdateRepresentanteLegal(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
            => Ok(await _servicio.DeleteRepresentanteLegal(id));
    }
}