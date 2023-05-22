using System;
using System.Collections.Generic;

namespace CharmieAPI.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string InitHour { get; set; } = null!;

    public string EndHour { get; set; } = null!;

    public string WeekDays { get; set; } = null!;

    public bool? Repeat { get; set; }

    public bool? Execution { get; set; }

    public bool? Stop { get; set; }

    public virtual ICollection<TaskRobot> TasksRobots { get; set; } = new List<TaskRobot>();
}
