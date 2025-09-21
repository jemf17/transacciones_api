using System;
using System.Collections.Generic;

namespace api_transacciones.ModelsSupabase;

public partial class TypesUser
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
