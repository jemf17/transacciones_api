using System;
using System.Collections.Generic;

namespace api_transacciones.ModelsSupabase;

/// <summary>
/// usuarios pendientes para ascender de rol
/// </summary>
public partial class Pendiente
{
    public Guid IdUser { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Manga { get; set; }

    public string? Sinopsis { get; set; }

    public string? Contacto { get; set; }

    public string? Redes { get; set; }

    public int? Estado { get; set; }

    public bool? AS { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
