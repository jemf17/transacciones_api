using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class PagesScan
{
    public Guid IdObra { get; set; }

    public int Numero { get; set; }

    public Guid IdScan { get; set; }

    public string Imagen { get; set; } = null!;

    public int Orden { get; set; }

    public virtual CapitulosScan CapitulosScan { get; set; } = null!;
}
