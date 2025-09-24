using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Historiale
{
    public Guid IdUser { get; set; }

    public Guid IdObra { get; set; }

    public TimeOnly Tiempo { get; set; }

    public DateOnly Fecha { get; set; }

    public int Numero { get; set; }

    public Guid? IdScan { get; set; }

    public long Id { get; set; }

    public virtual Capitulo Capitulo { get; set; } = null!;

    public virtual CapitulosScan? CapitulosScan { get; set; }
}
