using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharmieAPI.Models;
using Task = CharmieAPI.Models.Task;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using Environment = CharmieAPI.Models.Environment;
using System.Diagnostics;
using NuGet.Packaging;

namespace CharmieAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly RobotDbContext _context;

        public TasksController(RobotDbContext context) => _context = context;

        /* A DAR 100% */
        /// <summary>
        /// This method search in the database for all Tasks that has the same client id
        /// </summary>
        /// <param name="clientId">Robot's Id</param>
        /// <returns>List of Task</returns>
        [HttpGet("Client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasksByClient(int clientId)
        {
            // Verify in the database if there are any Task
            if (_context.Tasks.IsNullOrEmpty()) return NotFound();

            // Get all the environments
            List<Environment> environments = await _context.Environments.Where(e => e.ClientId.Equals(clientId)).ToListAsync();

            // Verify is the list is null
            if (environments.IsNullOrEmpty()) return NotFound();

            // Get all the robots
            List<Robot> robots = new List<Robot>();

            foreach (Environment environment in environments)
            {
                robots.AddRange(_context.Robots.Where(r => r.EnvironmentId.Equals(environment.Id)).ToList());
            }

            // Verify if the robots is null
            if (robots.IsNullOrEmpty()) return NotFound();

            List<Task> tasks = new List<Task>();
            List<Task> temp = new List<Task>();

            foreach (Robot robot in robots)
            {
                return await GetTasksByRobot(robot.Id);
            }

            return NotFound();
        }

        /* A DAR 100% */
        /// <summary>
        /// This method search in the database for all Tasks that has the same robot id
        /// </summary>
        /// <param name="robotId">Robot's Id</param>
        /// <returns>List of Task</returns>
        [HttpGet("Robot/{robotId}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasksByRobot(int robotId)
        {
            // Verify in the database if there are any Task
            if (_context.Tasks.IsNullOrEmpty()) return NotFound();

            List<Task> tasks = new List<Task>();

            // Get all the Task that as the same clientId
            List<Task> temp = await _context.Tasks.Include(t => t.TasksRobots)
                                                    .Where(t => t.TasksRobots.Any(tr => tr.RobotId.Equals(robotId)))
                                                    .ToListAsync();

            foreach (Task task in temp)
            {
                if (!tasks.Any(ts => ts.Id.Equals(task.Id))) tasks.Add(task);
            }

            // Verify if the tasks is null
            if (tasks.IsNullOrEmpty()) return NotFound();

            // Return tasks
            return Ok(tasks);
        }

        private bool TimeExists(TimeSpan timeInit, TimeSpan timeEnd, TimeSpan tInit, TimeSpan tEnd)
        {
            if ((tInit <= timeInit && tEnd > timeInit) ||
                (tInit < timeEnd && tEnd >= timeEnd) ||
                (tInit >= timeInit && tEnd <= timeEnd))
                return true;

            return false;
        }

        private bool ExistsScheduleTime(string initHour, string endHour, List<string> tInitHour, List<string> tEndHour, List<int> index)
        {
            bool exists = false;

            TimeSpan timeInit = TimeSpan.Parse(initHour);
            TimeSpan timeEnd = TimeSpan.Parse(endHour);

            List<TimeSpan> tInit = tInitHour.Where((t, i) => index.Contains(i) && TimeSpan.TryParse(t, out _))
                                            .Select(t => TimeSpan.Parse(t)).ToList();

            List<TimeSpan> tEnd = tEndHour.Where((t, i) => index.Contains(i) && TimeSpan.TryParse(t, out _))
                                          .Select(t => TimeSpan.Parse(t)).ToList();

            for (int i = 0; i < tInit.Count; i++)
            {
                if (TimeExists(timeInit, timeEnd, tInit[i], tEnd[i]))
                    exists = true;
            }

            return exists;
        }

        private bool ExistsSchedule(List<Task> tasks, string initHour, string endHour, string weekDay)
        {
            bool exists = false;
            List<string> tWeekDays, tInitHour, tEndHour;
            List<int> tIndex, indexes = new List<int>();

            foreach (Task t in tasks)
            {
                if (exists) break;

                tWeekDays = t.WeekDays.Split(',').ToList();
                tInitHour = t.InitHour.Split(',').ToList();
                tEndHour = t.EndHour.Split(',').ToList();

                tIndex = tWeekDays.Select((w, i) => w.Equals(weekDay) ? i : -1).Where(i => i != -1).ToList();

                if (ExistsScheduleTime(initHour, endHour, tInitHour, tEndHour, tIndex))
                    exists = true;
            }

            return exists;
        }

        private void GetBestTime(List<Task> tasks, Task task, string initHour, string endHour, string weekDay)
        {
            bool exists;
            List<string> tWeekDays, tInitHour, tEndHour;
            List<int> index;

            TimeSpan initH = TimeSpan.Parse(initHour);
            TimeSpan endH = TimeSpan.Parse(endHour);
            TimeSpan diff = endH - initH;

            for (TimeSpan h = TimeSpan.FromHours(8); h < TimeSpan.FromHours(20); h += diff)
            {
                foreach (Task t in tasks)
                {
                    exists = false;
                    tWeekDays = t.WeekDays.Split(',').ToList();
                    tInitHour = t.InitHour.Split(',').ToList();
                    tEndHour = t.EndHour.Split(',').ToList();

                    index = tWeekDays.Select((w, i) => w.Equals(weekDay) ? i : -1).Where(i => i != -1).ToList();


                    List<TimeSpan> tInit = tInitHour.Where((t, i) => index.Contains(i) && TimeSpan.TryParse(t, out _))
                                            .Select(t => TimeSpan.Parse(t)).ToList();

                    List<TimeSpan> tEnd = tEndHour.Where((t, i) => index.Contains(i) && TimeSpan.TryParse(t, out _))
                                                  .Select(t => TimeSpan.Parse(t)).ToList();

                    for (int i = 0; i < tInit.Count && !exists; i++)
                    {
                        if (TimeExists(h, (h + diff), tInit[i], tEnd[i])) exists = true;
                    }

                    if (exists) continue;

                    task.InitHour += (h + diff > TimeSpan.FromHours(20)) ? h.ToString() : h.ToString() + ",";
                    task.EndHour += (h + diff > TimeSpan.FromHours(20)) ? (h + diff).ToString() : (h + diff).ToString() + ",";
                }
            }
        }

        private async Task<List<Task>> VerifyTask(Task task)
        {
            List<Task> newTasks = new List<Task>();
            List<Task?> tempTasks;
            List<string> weekdays = task.WeekDays.Split(',').ToList();
            List<string> initHour = task.InitHour.Split(',').ToList();
            List<string> endHour = task.EndHour.Split(',').ToList();
            List<int> index;
            Task newTask;


            for (int i = 0; i < weekdays.Count; i++)
            {
                if (!newTasks.IsNullOrEmpty() && newTasks.Any(t => t.WeekDays.Equals(weekdays[i]))) 
                    continue;

                newTask = new Task
                {
                    Id = task.Id,
                    Name = task.Name,
                    InitHour = "",
                    EndHour = "",
                    WeekDays = weekdays[i],
                    Repeat = task.Repeat,
                    Execution = task.Execution,
                    Stop = task.Stop,
                    TasksRobots = new List<TaskRobot>()
                };

                // Get index of the weekdays
                index = weekdays.Select((w, index) => w.Equals(weekdays[i]) ? index : -1).Where(index => index != -1).ToList();

                foreach (TaskRobot taskRobot in task.TasksRobots)
                {
                    tempTasks = await _context.TasksRobots.Where(t => !t.TaskId.Equals(task.Id) &&
                                                            t.RobotId.Equals(taskRobot.RobotId))
                                                          .Select(tr => tr.Task).ToListAsync();

                    // Get only the tasks that has the same week day
                    tempTasks = tempTasks.Where(t => t.WeekDays.Split(',').Any(w => w.Equals(weekdays[i]))).ToList();

                    // verify if the list is not null or empty
                    if (!tempTasks.IsNullOrEmpty())
                    {
                        for (int j = 0; j < index.Count; j++)
                        {
                            if (!ExistsSchedule(tempTasks, initHour[index[j]], endHour[index[j]], weekdays[i])) continue;

                            GetBestTime(tempTasks, newTask, initHour[index[j]], endHour[index[j]], weekdays[i]);
                        }
                    }
                }

                if (newTask.InitHour.Equals("")) continue;

                Debug.WriteLine(newTask.InitHour);
                Debug.WriteLine(newTask.EndHour);

                List<string> tempInitHour = newTask.InitHour.Split(',').ToList();
                tempInitHour = tempInitHour.GroupBy(i => i)
                           .OrderByDescending(g => g.Count())
                           .TakeWhile(g => g.Count() == tempInitHour.Max(x => tempInitHour.Count(y => y == x)))
                           .Select(g => g.Key)
                           .ToList();

                List<string> tempEndHour = newTask.EndHour.Split(',').ToList();
                tempEndHour = tempEndHour.GroupBy(i => i)
                           .OrderByDescending(g => g.Count())
                           .TakeWhile(g => g.Count() == tempEndHour.Max(x => tempEndHour.Count(y => y == x)))
                           .Select(g => g.Key)
                           .ToList();


                newTask.InitHour = string.Join(',', tempInitHour);
                newTask.EndHour = string.Join(',', tempEndHour);
                newTask.TasksRobots.AddRange(task.TasksRobots);

                newTasks.Add(newTask);
            }

            if (newTasks.IsNullOrEmpty())
                newTasks.Add(task);

            return newTasks;
        }

        /* A DAR 100% */
        /// <summary>
        /// This method creates a new Task
        /// </summary>
        /// <param name="task">Task Object</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(Task? task)
        {
            // Verify if the task receibed is null
            if (task is null) return BadRequest();

            List<TimeSpan> totalInit = task.InitHour.Split(',').Where(t => TimeSpan.TryParse(t, out _))
                                                    .Select(t => TimeSpan.Parse(t)).ToList();

            List<TimeSpan> totalEnd = task.EndHour.Split(',').Where(t => TimeSpan.TryParse(t, out _))
                                                    .Select(t => TimeSpan.Parse(t)).ToList();

            if (totalInit.Count < task.InitHour.Split(',').Count() || totalEnd.Count < task.EndHour.Split(',').Count()) 
                return BadRequest();

            if (!totalEnd.Zip(totalInit, (end, init) => end >= init).All(c => c)) return BadRequest();

            List<Task> newTasks = await VerifyTask(task);

            if (!newTasks.Count.Equals(1) || !newTasks.First().InitHour.Equals(task.InitHour)) return NotFound(newTasks);

            Task newTask = newTasks.First();

            await _context.Tasks.AddAsync(newTask);

            try
            {
                await _context.SaveChangesAsync();

                // Reload object with the info from database
                await _context.Entry(newTask).ReloadAsync();

                newTask.TasksRobots = await _context.TasksRobots.Include(tr => tr.Robot).Where(tr => tr.TaskId.Equals(newTask.Id)).ToListAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                return BadRequest(ModelState);
            }

            return Ok(newTask);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method updates an Task
        /// </summary>
        /// <param name="id">Task's Id</param>
        /// <param name="task">Task Object</param>
        /// <returns>Action Result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Task? task)
        {
            // Verify if the task receibed is not null or if the task id are the same
            if (task is null || id != task.Id) return BadRequest();

            List<TimeSpan> totalInit = task.InitHour.Split(',').Where(t => TimeSpan.TryParse(t, out _))
                                                    .Select(t => TimeSpan.Parse(t)).ToList();

            List<TimeSpan> totalEnd = task.EndHour.Split(',').Where(t => TimeSpan.TryParse(t, out _))
                                                    .Select(t => TimeSpan.Parse(t)).ToList();

            if (totalInit.Count < task.InitHour.Split(',').Count() || totalEnd.Count < task.EndHour.Split(',').Count())
                return BadRequest();

            if (!totalEnd.Zip(totalInit, (end, init) => end >= init).All(c => c)) return BadRequest();

            List<Task> updateTasks = await VerifyTask(task);

            if (!updateTasks.Count.Equals(1)) return NotFound(updateTasks);

            Task updateTask = updateTasks.First();

            // Remove all existing TaskRobots for this task
            List<TaskRobot> existingTaskRobots = await _context.TasksRobots.Where(tr => tr.TaskId == task.Id).ToListAsync();
            _context.TasksRobots.RemoveRange(existingTaskRobots);

            // Add the new TaskRobots to the task
            foreach (TaskRobot taskRobot in updateTask.TasksRobots)
            {
                taskRobot.TaskId = task.Id;
                _context.TasksRobots.Add(taskRobot);
            }

            // Put the environment as an entry an set the sate as modified to update the database
            _context.Entry(updateTask).State = EntityState.Modified;

            // Try to save to database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error updating changes: {ex.Message}");
                return BadRequest(ModelState);
            }

            // If is successfully then returns an OK
            return Ok();
        }

        /* A DAR 100% */
        /// <summary>
        /// This method removes an task from the database
        /// </summary>
        /// <param name="id">Task's Id</param>
        /// <returns>Action Result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            // Verify if the are any tasks in the database or if the task is null
            if (_context.Tasks.IsNullOrEmpty()) return BadRequest();

            // Get task as temp
            Task? task = await _context.Tasks.FindAsync(id);

            if (task is null) return BadRequest();

            // Remove all existing TaskRobots for this task
            List<TaskRobot> existingTaskRobots = await _context.TasksRobots.Where(tr => tr.TaskId == task.Id).ToListAsync();
            _context.TasksRobots.RemoveRange(existingTaskRobots);

            // Put the task as an entry an set the sate as remove from database
            _context.Tasks.Remove(task);

            // Try to save to database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error removing: {ex.Message}");
                return BadRequest(ModelState);
            }

            // If is successfully then returns an OK
            return Ok();
        }
    }
}
