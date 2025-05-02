using System;
using System.Collections.Generic;

namespace api_transacciones.ModelsSupabase;

public partial class Profile
{
    public Guid Id { get; set; }

    public string? Descripcion { get; set; }

    public string? Perfil { get; set; }

    public string? Name { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
