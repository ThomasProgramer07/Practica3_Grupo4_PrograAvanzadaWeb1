using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.PartidoPolitico;
using WebAvanzadaIICuatrimestre.BLL.Services.RepresentanteLegal;
using WebAvanzadaIICuatrimestre.Models;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class PartidoPoliticoController : Controller
    {
        private readonly IPartidoPoliticoServicio _partidoPoliticoServicio;
        private readonly IRepresentanteLegalServicio _representanteLegalServicio;

        public PartidoPoliticoController(IPartidoPoliticoServicio partidoPoliticoServicio, IRepresentanteLegalServicio representanteLegalServicio)
        {
            _partidoPoliticoServicio = partidoPoliticoServicio;
            _representanteLegalServicio = representanteLegalServicio;
        }       

        /*public async Task<IActionResult> Index()
        {
            var vm = new PartidoPoliticoViewModel
            {
                PartidoPolitico = new PartidoPoliticoDto()
            };

            var resp = await _representanteLegalServicio.GetRepresentanteLegals();
            var representanteLegals = resp.Dato ?? new List<RepresentanteLegalDto>();

            vm.RepresentanteLegals = representanteLegals
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Nombre })
                .ToList();

            return View(vm);
        }-*/       

        public async Task<IActionResult> Index() //Que puede llegar a ser m'as facil de entender e implementar, aunque el ViewModel es una buena práctica para mantener la lógica de presentación separada de la lógica de negocio.
        {
            var resp = await _representanteLegalServicio.GetRepresentanteLegals(); 
            ViewBag.RepresentanteLegals = resp.Dato;
            return View();
        }
        //Separacion de responsabilidad, capacidad para darle mantenimiento al codigo facilmente y claridad de lo que se hace

        public async Task<IActionResult> GetPartidoPoliticos()
        {
            var respuesta = await _partidoPoliticoServicio.GetPartidoPoliticos();
            return Json(respuesta);
        }

        public async Task<IActionResult> CreatePartidoPolitico(PartidoPoliticoDto partidoPolitico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _partidoPoliticoServicio.CreatePartidoPolitico(partidoPolitico);
            return Json(respuesta);
        }

        public async Task<IActionResult> GetPartidoPoliticoById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _partidoPoliticoServicio.GetPartidoPoliticoById(id);
            return Json(respuesta);
        }

        public async Task<IActionResult> UpdatePartidoPolitico(PartidoPoliticoDto partidoPolitico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _partidoPoliticoServicio.UpdatePartidoPolitico(partidoPolitico);
            return Json(respuesta);
        }

        public async Task<IActionResult> DeletePartidoPolitico(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _partidoPoliticoServicio.DeletePartidoPolitico(id);
            return Json(respuesta);
        }
    }
}
