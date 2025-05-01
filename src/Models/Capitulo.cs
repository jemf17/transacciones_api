using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Capitulo
{
    public Guid IdObra { get; set; }

    public int Numero { get; set; }

    public string IdIdioma { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public float Price { get; set; }

    public virtual ICollection<AutorizacionesCapitulosPago> AutorizacionesCapitulosPagos { get; set; } = new List<AutorizacionesCapitulosPago>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Historiale> Historiales { get; set; } = new List<Historiale>();

    public virtual Idioma IdIdiomaNavigation { get; set; } = null!;

    public virtual Obra IdObraNavigation { get; set; } = null!;

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
