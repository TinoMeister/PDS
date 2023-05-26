using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CharmieAPI.Models;

public partial class Warning
{
    public int Id { get; set; }

    public string Message { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WarningStates? State { get; set; }

    public DateTime HourDay { get; set; }

    public int RobotId { get; set; }

    public string? IdentityId { get; set; }
}
