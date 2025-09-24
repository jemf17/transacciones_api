using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Colaboladore
{
    public Guid IdObra { get; set; }

    public Guid IdArts { get; set; }

    public bool Push { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
