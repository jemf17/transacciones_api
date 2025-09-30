using System;
using System.Collections.Generic;

namespace DBModels;

public partial class HistorialesObra
{
    public DateOnly Fecha { get; set; }

    public Guid IdObra { get; set; }

    public TimeSpan Retencion { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
