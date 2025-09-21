using System;
using System.Collections.Generic;

namespace api_transacciones.ModelsSupabase;

public partial class Migration
{
    public string Version { get; set; } = null!;

    public DateTime InsertedAt { get; set; }
}
