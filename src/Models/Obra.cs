using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Obra
{
    public Guid Id { get; set; }

    public string Portada { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public bool Oneshot { get; set; }

    public bool Madure { get; set; }

    public string? TituloSecundario { get; set; }

    public Guid Artista { get; set; }

    public virtual ICollection<Capitulo> Capitulos { get; set; } = new List<Capitulo>();

    public virtual ICollection<CapitulosScan> CapitulosScans { get; set; } = new List<CapitulosScan>();

    public virtual ICollection<Colaboladore> Colaboladores { get; set; } = new List<Colaboladore>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<Guardado> Guardados { get; set; } = new List<Guardado>();

    public virtual ICollection<HistorialesObra> HistorialesObras { get; set; } = new List<HistorialesObra>();

    public virtual ICollection<ObrasArtista> ObrasArtista { get; set; } = new List<ObrasArtista>();

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual ICollection<Retencione> Retenciones { get; set; } = new List<Retencione>();

    public virtual Vista? Vista { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
