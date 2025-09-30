using System;
using System.Collections.Generic;

namespace DBModels;

public partial class Page
{
    public int Orden { get; set; }

    public string Imagen { get; set; } = null!;

    public Guid IdObra { get; set; }

    public int NumeroCap { get; set; }

    public virtual Capitulo Capitulo { get; set; } = null!;

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
