using System;
using System.Collections.Generic;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class PartidoPolitico
{
    public int Id { get; set; }

    public string Identificacion { get; set; } = null!;

    // Stored as integer in DB (0 = Inactivo, 1 = Activo)
    public int Estado { get; set; }

    public string Sigla { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int? FkrepresentanteLegal { get; set; }

    public virtual RepresentanteLegal? FkrepresentanteLegalNavigation { get; set; }
}
