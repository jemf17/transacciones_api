using System;
using System.Collections.Generic;

namespace DBModels;

public partial class Solicitude
{
    public Guid IdTraductor { get; set; }

    public Guid IdPedido { get; set; }

    public string Descripcion { get; set; } = null!;

    public float Price { get; set; }

    public bool Aceptado { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
