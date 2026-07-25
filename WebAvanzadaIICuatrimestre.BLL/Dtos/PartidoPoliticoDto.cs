using System;

//DataAnotations
namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class PartidoPoliticoDto
    {
        public int ?Id { get; set; }

        public string Identificacion { get; set; } = null!;

        public int Estado { get; set; }

        public string Sigla { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public int? FkrepresentanteLegal { get; set; }
        public RepresentanteLegalDto? RepresentanteLegal { get; set; }
    }
}
