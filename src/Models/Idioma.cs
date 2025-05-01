using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Idioma
{
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Capitulo> Capitulos { get; set; } = new List<Capitulo>();

    public virtual ICollection<CapitulosScan> CapitulosScans { get; set; } = new List<CapitulosScan>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
