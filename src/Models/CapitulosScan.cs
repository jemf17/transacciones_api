using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class CapitulosScan
{
    public Guid IdObra { get; set; }

    public int Numero { get; set; }

    public Guid IdScan { get; set; }

    public DateOnly Fecha { get; set; }

    public string IdIdioma { get; set; } = null!;

    public float Price { get; set; }

    public virtual ICollection<Historiale> Historiales { get; set; } = new List<Historiale>();

    public virtual Idioma IdIdiomaNavigation { get; set; } = null!;

    public virtual Obra IdObraNavigation { get; set; } = null!;

    public virtual ICollection<PagesScan> PagesScans { get; set; } = new List<PagesScan>();
}
