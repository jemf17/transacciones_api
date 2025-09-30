using System;
using System.Collections.Generic;

namespace DBModels;

public partial class Comentario
{
    public Guid IdUser { get; set; }

    public Guid IdObra { get; set; }

    public DateTime Fecha { get; set; }

    public Guid Id { get; set; }

    public string Comentario1 { get; set; } = null!;

    public int Capitulo { get; set; }

    public virtual Capitulo CapituloNavigation { get; set; } = null!;
}
