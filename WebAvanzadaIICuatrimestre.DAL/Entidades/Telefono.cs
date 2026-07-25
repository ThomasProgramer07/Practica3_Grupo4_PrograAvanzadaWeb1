using System;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class Telefono
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public int FkrepresentanteLegal { get; set; }

    public int Fkvotante { get; set; }

    public virtual RepresentanteLegal? FkrepresentanteLegalNavigation { get; set; }

    public virtual Votante? FkvotanteNavigation { get; set; }
}
