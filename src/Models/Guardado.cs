using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Guardado
{
    public Guid IdUser { get; set; }

    public Guid IdObra { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
