using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Votante;


namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class VotanteController : Controller
    {
        private readonly VotanteServicio _votanteServicio;


        public VotanteController(VotanteServicio votanteServicio)
        {
            _votanteServicio = votanteServicio;
        }

        //Vamos a trabajrForma incorrecta segun la IA, se recomienda utilizar un ViewModel o DTO para pasar la informacion necesaria a la vista, en lugar de cargar la informacion directamente en el controlador y pasarla a la vista a través de ViewBag o ViewData, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
        public IActionResult Index() //Se puede llegar a cargar informacion necesario para la vista, como una lista de representanteLegals, etc. utilizando el servicio de representanteLegal para obtener los datos necesarios y pasarlos a la vista a través de un modelo o ViewBag.
        {
            //ViewBag.RepresentanteLegals = _representanteLegalServicio.GetAllRepresentanteLegals(); // Ejemplo de carga de datos para la vista, se puede utilizar un modelo o ViewBag para pasar los datos a la vista.

            //Crear un DTO  o ViewModel para la vista, que contenga la informacion necesaria para mostrar en la vista, como una lista de representanteLegals, etc. y pasar ese DTO o ViewModel a la vista.
            return View();
        }

        public async Task<IActionResult> GetVotantes() // Metodos pequeños, el controlador no sabe respuesta de negocio, solo sabe que tiene que llamar al servicio y devolver la respuesta, la logica de negocio se encuentra en el servicio, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
        {
            var respuesta = await _votanteServicio.GetVotantes();
            return Json(respuesta);
        }

        public async Task<IActionResult> CreateVotante(VotanteDto votante)// Model Binding, reicibir el objeto completo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _votanteServicio.CreateVotante(votante);
            return Json(respuesta);
        }

        public async Task<IActionResult> GetVotanteById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _votanteServicio.GetVotanteById(id);
            return Json(respuesta);
        }

        public async Task<IActionResult> UpdateVotante(VotanteDto votante) //nombres Programa con IA
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _votanteServicio.UpdateVotante(votante); //nombres Programa con IA
            return Json(respuesta);
        }

        public async Task<IActionResult> DeleteVotante(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _votanteServicio.DeleteVotante(id);
            return Json(respuesta);
        }


        //Por que lo hago así? Por la facilidad de crear la vista y mostrar la informacion, pero no es la forma recomendada por la IA, se recomienda utilizar un ViewModel o DTO para pasar la informacion necesaria a la vista, en lugar de cargar la informacion directamente en el controlador y pasarla a la vista a través de ViewBag o ViewData, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
    }
}
