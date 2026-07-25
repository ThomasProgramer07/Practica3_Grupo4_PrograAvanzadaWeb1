using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class Correo
{
    public int Id { get; set; }
    
    [Column("CorreoElectronico")]  // Matches your DB column name
    public string CorreoElectronico { get; set; } = null!;
    
    [Column("FKRepresentanteLegal")]  // Specify exact DB column name
    public int FkrepresentanteLegal { get; set; }

    public int Fkvotante { get; set; }

    public virtual RepresentanteLegal? FkrepresentanteLegalNavigation { get; set; }

    public virtual Votante? FkvotanteNavigation { get; set; }
}
