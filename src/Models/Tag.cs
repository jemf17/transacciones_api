using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Tag
{
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Madure { get; set; }

    public string? NameJapanese { get; set; }

    public int Id { get; set; }

    public virtual ICollection<Preferencia> PreferenciaTag1Navigations { get; set; } = new List<Preferencia>();

    public virtual ICollection<Preferencia> PreferenciaTag2Navigations { get; set; } = new List<Preferencia>();

    public virtual ICollection<Preferencia> PreferenciaTag3Navigations { get; set; } = new List<Preferencia>();

    public virtual ICollection<Preferencia> PreferenciaTag4Navigations { get; set; } = new List<Preferencia>();

    public virtual ICollection<Preferencia> PreferenciaTag5Navigations { get; set; } = new List<Preferencia>();

    public virtual ICollection<Preferencia> PreferenciaTag6Navigations { get; set; } = new List<Preferencia>();

    public virtual ICollection<Obra> IdObras { get; set; } = new List<Obra>();
}
