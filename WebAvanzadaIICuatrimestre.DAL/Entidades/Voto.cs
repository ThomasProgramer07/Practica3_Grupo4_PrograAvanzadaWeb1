namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class Voto
{
    public int Id { get; set; }
    public int Fkvotante { get; set; }
    public int FkpartidoPolitico { get; set; }
    public DateTime Fecha { get; set; }

    public virtual Votante FkvotanteNavigation { get; set; } = null!;
    public virtual PartidoPolitico FkpartidoPoliticoNavigation { get; set; } = null!;
}