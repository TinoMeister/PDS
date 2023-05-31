using System;
using System.Collections.Generic;

namespace CharmieAPI.Models;

public partial class QuantityMaterial
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int MaterialId { get; set; }

    public int EnvironmentId { get; set; }

    public int? TaskId { get; set; }

    public virtual Material Material { get; set; } = null!;
}
