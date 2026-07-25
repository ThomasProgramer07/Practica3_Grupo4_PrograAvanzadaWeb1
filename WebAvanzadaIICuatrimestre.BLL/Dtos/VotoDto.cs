public class VotoDto
{
    public string Identificacion { get; set; } = string.Empty; // cédula del votante
    public int FkpartidoPolitico { get; set; }                  // partido elegido
}

public class ResultadoDto
{
    public int PartidoPoliticoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;
    public int CantidadVotos { get; set; }
}