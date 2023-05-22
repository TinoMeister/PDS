namespace CharmieAPI.Models;

public partial class TaskRobot
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public int RobotId { get; set; }

    public virtual Robot Robot { get; set; } = null!;

    public virtual Task? Task { get; set; }
}