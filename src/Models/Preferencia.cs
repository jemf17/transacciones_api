using System;
using System.Collections.Generic;

namespace api_transacciones.Models;

public partial class Preferencia
{
    public Guid IdUser { get; set; }

    public string Tag1 { get; set; } = null!;

    public float V1 { get; set; }

    public string Tag2 { get; set; } = null!;

    public float V2 { get; set; }

    public string Tag3 { get; set; } = null!;

    public float V3 { get; set; }

    public string? Tag4 { get; set; }

    public float? V4 { get; set; }

    public string? Tag5 { get; set; }

    public float? V5 { get; set; }

    public string? Tag6 { get; set; }

    public float? V6 { get; set; }

    public virtual Tag Tag1Navigation { get; set; } = null!;

    public virtual Tag Tag2Navigation { get; set; } = null!;

    public virtual Tag Tag3Navigation { get; set; } = null!;

    public virtual Tag? Tag4Navigation { get; set; }

    public virtual Tag? Tag5Navigation { get; set; }

    public virtual Tag? Tag6Navigation { get; set; }
}
