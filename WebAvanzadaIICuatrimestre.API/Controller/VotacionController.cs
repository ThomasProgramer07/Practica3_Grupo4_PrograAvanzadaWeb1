using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Votacion;

namespace WebAvanzadaIICuatrimestre.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VotacionController : ControllerBase
    {
        private readonly IVotacionServicio _votacionServicio;

        public VotacionController(IVotacionServicio votacionServicio)
        {
            _votacionServicio = votacionServicio;
        }

        // POST api/votacion/votar
        [HttpPost("votar")]
        public async Task<IActionResult> Votar([FromBody] VotoDto voto)
        {
            var respuesta = await _votacionServicio.RegistrarVoto(voto);

            if (!respuesta.esCorrecto)
            {
                return respuesta.codigo switch
                {
                    404 => NotFound(respuesta),   // no inscrito
                    _ => BadRequest(respuesta)  // ya votó / error de validación
                };
            }

            return Ok(respuesta);
        }

        // GET api/votacion/resultados
        [HttpGet("resultados")]
        public async Task<IActionResult> Resultados()
        {
            var respuesta = await _votacionServicio.GetResultados();
            return Ok(respuesta);
        }
    }
}