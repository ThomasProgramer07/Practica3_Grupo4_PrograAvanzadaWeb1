using System.ComponentModel.DataAnnotations;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class CorreoDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
