using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Tipospedido
{
    public string Tipo { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
