using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Votacion;

namespace WebAvanzadaIICuatrimestre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotacionController : ControllerBase
    {
        private readonly IVotacionServicio _votacionServicio;

        public VotacionController(IVotacionServicio votacionServicio)
        {
            _votacionServicio = votacionServicio;
        }

        [HttpPost("votar")]
        public async Task<IActionResult> Votar([FromBody] VotoDto voto)
            => Ok(await _votacionServicio.RegistrarVoto(voto));

        [HttpGet("resultados")]
        public async Task<IActionResult> GetResultados()
            => Ok(await _votacionServicio.GetResultados());
    }
}