using System;
using System.Collections.Generic;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class RepresentanteLegal
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    // Stored as integer in DB (1 = Hombre, 2 = Mujer)
    public int Sexo { get; set; }

    public virtual ICollection<PartidoPolitico> PartidosPoliticos { get; set; } = new List<PartidoPolitico>();

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();

    public virtual ICollection<Correo> Correos { get; set; } = new List<Correo>();
}
