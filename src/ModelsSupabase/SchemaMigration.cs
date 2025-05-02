using System;
using System.Collections.Generic;

namespace api_transacciones.ModelsSupabase;

/// <summary>
/// Auth: Manages updates to the auth system.
/// </summary>
public partial class SchemaMigration
{
    public string Version { get; set; } = null!;
}
