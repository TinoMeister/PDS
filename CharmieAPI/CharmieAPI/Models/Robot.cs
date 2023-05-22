using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CharmieAPI.Models;

public partial class Robot
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RobotStates State { get; set; }

    public int EnvironmentId { get; set; }
}
