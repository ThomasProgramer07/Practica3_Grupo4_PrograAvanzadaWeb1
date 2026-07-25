using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.RepresentanteLegal;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class RepresentanteLegalController : Controller
    {
        private readonly IRepresentanteLegalServicio _representanteLegalServicio;


        public RepresentanteLegalController(IRepresentanteLegalServicio representanteLegalServicio)
        {
            _representanteLegalServicio = representanteLegalServicio;
        }

        //Vamos a trabajrForma incorrecta segun la IA, se recomienda utilizar un ViewModel o DTO para pasar la informacion necesaria a la vista, en lugar de cargar la informacion directamente en el controlador y pasarla a la vista a través de ViewBag o ViewData, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
        public IActionResult Index() //Se puede llegar a cargar informacion necesario para la vista, como una lista de representanteLegals, etc. utilizando el servicio de representanteLegal para obtener los datos necesarios y pasarlos a la vista a través de un modelo o ViewBag.
        {
            //ViewBag.RepresentanteLegals = _representanteLegalServicio.GetAllRepresentanteLegals(); // Ejemplo de carga de datos para la vista, se puede utilizar un modelo o ViewBag para pasar los datos a la vista.

            //Crear un DTO  o ViewModel para la vista, que contenga la informacion necesaria para mostrar en la vista, como una lista de representanteLegals, etc. y pasar ese DTO o ViewModel a la vista.
            return View();
        }

        public async Task<IActionResult> GetRepresentanteLegals() // Metodos pequeños, el controlador no sabe respuesta de negocio, solo sabe que tiene que llamar al servicio y devolver la respuesta, la logica de negocio se encuentra en el servicio, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
        {
            var respuesta = await _representanteLegalServicio.GetRepresentanteLegals();
            return Json(respuesta);
        }

        public async Task<IActionResult> CreateRepresentanteLegal(RepresentanteLegalDto representanteLegal)// Model Binding, reicibir el objeto completo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _representanteLegalServicio.CreateRepresentanteLegal(representanteLegal);
            return Json(respuesta);
        }

        public async Task<IActionResult> GetRepresentanteLegalById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _representanteLegalServicio.GetRepresentanteLegalById(id);
            return Json(respuesta);
        }

        public async Task<IActionResult> UpdateRepresentanteLegal(RepresentanteLegalDto representanteLegal) //nombres Programa con IA
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _representanteLegalServicio.UpdateRepresentanteLegal(representanteLegal); //nombres Programa con IA
            return Json(respuesta);
        }

        public async Task<IActionResult> DeleteRepresentanteLegal(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _representanteLegalServicio.DeleteRepresentanteLegal(id);
            return Json(respuesta);
        }


        //Por que lo hago así? Por la facilidad de crear la vista y mostrar la informacion, pero no es la forma recomendada por la IA, se recomienda utilizar un ViewModel o DTO para pasar la informacion necesaria a la vista, en lugar de cargar la informacion directamente en el controlador y pasarla a la vista a través de ViewBag o ViewData, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
    }
}
