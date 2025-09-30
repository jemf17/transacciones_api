using System;
using System.Collections.Generic;

namespace DBModels;

public partial class ObrasArtista
{
    public Guid IdObra { get; set; }

    public Guid IdArtist { get; set; }

    public bool Notificado { get; set; }

    public DateTime Createdat { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
