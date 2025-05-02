using System;
using System.Collections.Generic;

namespace api_transacciones.ModelsSupabase;

public partial class Role
{
    public Guid IdUser { get; set; }

    public string? IdTypeUser { get; set; }

    public bool? Rechazado { get; set; }

    public virtual TypesUser? IdTypeUserNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
