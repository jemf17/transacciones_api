using System;
using System.Collections.Generic;

namespace DBModels;

public partial class Donacione
{
    public int Id { get; set; }

    public Guid IdDonador { get; set; }

    public float Cantidad { get; set; }

    public string Idioma { get; set; } = null!;

    public Guid IdPedido { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Idioma IdiomaNavigation { get; set; } = null!;
}
