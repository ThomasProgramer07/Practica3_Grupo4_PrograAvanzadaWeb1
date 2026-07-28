namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class VotoDto
    {
        public string Identificacion { get; set; } = string.Empty; // cédula del votante
        public int FkpartidoPolitico { get; set; }                  // partido elegido
    }
}