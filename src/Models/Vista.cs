using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Vista
{
    public Guid IdObra { get; set; }

    public int Visualizacion { get; set; }

    public int Favoritos { get; set; }

    public int Guardados { get; set; }

    public int Comentarios { get; set; }

    public TimeSpan Retencion { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
