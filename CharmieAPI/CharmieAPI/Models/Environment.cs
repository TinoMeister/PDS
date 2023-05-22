using System;
using System.Collections.Generic;

namespace CharmieAPI.Models;

public partial class Environment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Length { get; set; }

    public int Width { get; set; }

    public int ClientId { get; set; }
}
