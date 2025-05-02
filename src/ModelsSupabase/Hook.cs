﻿using System;
using System.Collections.Generic;

namespace api_transacciones.ModelsSupabase;

/// <summary>
/// Supabase Functions Hooks: Audit trail for triggered hooks.
/// </summary>
public partial class Hook
{
    public long Id { get; set; }

    public int HookTableId { get; set; }

    public string HookName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public long? RequestId { get; set; }
}
