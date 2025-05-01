using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Retencione
{
    public Guid ObraId { get; set; }

    public DateOnly FechaMensual { get; set; }

    public float Promedio { get; set; }

    public virtual Obra Obra { get; set; } = null!;
}
