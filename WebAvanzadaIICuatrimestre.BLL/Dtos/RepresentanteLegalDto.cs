using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class RepresentanteLegalDto
    {
        public int ?Id { get; set; } //Dejar los identificados de los DTS o ViewModels obligatoriamente es un error, ya que el DTO o ViewModel es un objeto que se utiliza para transferir datos entre capas, y no necesariamente tiene que tener un identificador, esto para mantener una buena separacion de responsabilidades y evitar que el DTO o ViewModel tenga demasiada logica de negocio, sin embargo, en este caso se deja el Id para poder identificar al representanteLegal en caso de ser necesario, pero no es obligatorio.

        [Required(ErrorMessage ="El nombre es requerido")] //Viejito
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        [Required(ErrorMessage = "El Apellido1 es requerido")]
        public string Apellido1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Apellido2 es requerido")]
        public string Apellido2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "La identificación es requerida")]
        public string Identificacion { get; set; } = string.Empty;

        public int Sexo { get; set; }

        public List<TelefonoDto> Telefonos { get; set; } = new List<TelefonoDto>();
        public List<CorreoDto> Correos { get; set; } = new List<CorreoDto>();
        //public List<PartidoPoliticoDto> PartidoPoliticos { get; set; } = new List<PartidoPoliticoDto>();
    }
}
