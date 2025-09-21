using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Pedido
{
    public string IdIdiomaTraducir { get; set; } = null!;

    public Guid IdObra { get; set; }

    public int NumeroCap { get; set; }

    public Guid IdArtista { get; set; }

    public string Tipo { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string? Panel { get; set; }

    public string? Texto { get; set; }

    public Guid Id { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Capitulo Capitulo { get; set; } = null!;

    public virtual EstadosPedido EstadoNavigation { get; set; } = null!;

    public virtual Idioma IdIdiomaTraducirNavigation { get; set; } = null!;

    public virtual Tipospedido TipoNavigation { get; set; } = null!;
}
