﻿using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class AutorizacionesCapitulosPago
{
    public Guid IdObra { get; set; }

    public int Numero { get; set; }

    public Guid IdUser { get; set; }

    public DateOnly FechaDeCompra { get; set; }

    public virtual Capitulo Capitulo { get; set; } = null!;
}
