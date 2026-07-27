namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class ResultadoDto
    {
        public int PartidoPoliticoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public int CantidadVotos { get; set; }
    }
}