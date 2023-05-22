using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharmieAPI.Models;
using Task = CharmieAPI.Models.Task;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using Environment = CharmieAPI.Models.Environment;
using System.Diagnostics;
using NuGet.Packaging;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
        [Authorize]
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

        /// <summary>
        /// This method verifies if the new time is in the range of others time and if so then return TRUE
        /// </summary>
        /// <param name="timeInit">New time Init</param>
        /// <param name="timeEnd">New Time End</param>
        /// <param name="tInit">Older Time Init</param>
        /// <param name="tEnd">Older Time End</param>
        /// <returns>True if exists, False if not</returns>
        private bool TimeExists(TimeSpan timeInit, TimeSpan timeEnd, TimeSpan tInit, TimeSpan tEnd)
        {
            // Verify if the new time enters in the rage of others times
            if ((tInit <= timeInit && tEnd > timeInit) ||
                (tInit < timeEnd && tEnd >= timeEnd) ||
                (tInit >= timeInit && tEnd <= timeEnd))
                return true;

            return false;
        }

        /// <summary>
        /// This method verify if exists any schedule alredy with that time
        /// </summary>
        /// <param name="initHour"></param>
        /// <param name="endHour"></param>
        /// <param name="tInitHour"></param>
        /// <param name="tEndHour"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool ExistsScheduleTime(string initHour, string endHour, List<string> tInitHour, List<string> tEndHour, List<int> index)
        {
            bool exists = false;

            // Convert the time to time Span
            TimeSpan timeInit = TimeSpan.Parse(initHour);
            TimeSpan timeEnd = TimeSpan.Parse(endHour);

            // Get the a list with all the time in the index list
            List<TimeSpan> tInit = tInitHour.Where((t, i) => index.Contains(i) && TimeSpan.TryParse(t, out _))
                                            .Select(t => TimeSpan.Parse(t)).ToList();

            List<TimeSpan> tEnd = tEndHour.Where((t, i) => index.Contains(i) && TimeSpan.TryParse(t, out _))
                                          .Select(t => TimeSpan.Parse(t)).ToList();

            // Verify if exists
            for (int i = 0; i < tInit.Count && !exists; i++)
            {
                if (TimeExists(timeInit, timeEnd, tInit[i], tEnd[i]))
                    exists = true;
            }

            return exists;
        }

        /// <summary>
        /// This method verify if the schedule exists
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="initHour"></param>
        /// <param name="endHour"></param>
        /// <param name="weekDay"></param>
        /// <returns></returns>
        private bool ExistsSchedule(List<Task> tasks, string initHour, string endHour, string weekDay)
        {
            bool exists = false;
            List<string> tWeekDays, tInitHour, tEndHour;
            List<int> tIndex, indexes = new List<int>();

            // Goes taks by task
            foreach (Task t in tasks)
            {
                // If exists exit
                if (exists) break;

                // Split the weekdays and times by comma
                tWeekDays = t.WeekDays.Split(',').ToList();
                tInitHour = t.InitHour.Split(',').ToList();
                tEndHour = t.EndHour.Split(',').ToList();

                // Get the index of the weekeday corresponding to the weekday that is searching
                tIndex = tWeekDays.Select((w, i) => w.Equals(weekDay) ? i : -1).Where(i => i != -1).ToList();

                // Verify if the schedule exists for that time
                if (ExistsScheduleTime(initHour, endHour, tInitHour, tEndHour, tIndex))
                    exists = true;
            }

            return exists;
        }

        /// <summary>
        /// This method gets the best Time for a certain day
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="task"></param>
        /// <param name="initHour"></param>
        /// <param name="endHour"></param>
        /// <param name="weekDay"></param>
        private void GetBestTime(List<Task> tasks, Task task, string initHour, string endHour, string weekDay)
        {
            bool exists;
            List<string> tWeekDays, tInitHour, tEndHour;
            List<int> index;

            // Convert the time ti timespan
            TimeSpan initH = TimeSpan.Parse(initHour);
            TimeSpan endH = TimeSpan.Parse(endHour);
            TimeSpan diff = endH - initH;

            // Goes from 8AM to 8PM and incresse by the diff
            for (TimeSpan h = TimeSpan.FromHours(8); h < TimeSpan.FromHours(20); h += diff)
            {
                // Goes task by task
                foreach (Task t in tasks)
                {
                    exists = false;
                    tWeekDays = t.WeekDays.Split(',').ToList();
                    tInitHour = t.InitHour.Split(',').ToList();
                    tEndHour = t.EndHour.Split(',').ToList();

                    // Get the weekday index that has the same weekday
                    index = tWeekDays.Select((w, i) => w.Equals(weekDay) ? i : -1).Where(i => i != -1).ToList();


                    // Get a list of all the init time and end time on the index
                    List<TimeSpan> tInit = tInitHour.Where((t, i) => index.Contains(i) && TimeSpan.TryParse(t, out _))
                                            .Select(t => TimeSpan.Parse(t)).ToList();

                    List<TimeSpan> tEnd = tEndHour.Where((t, i) => index.Contains(i) && TimeSpan.TryParse(t, out _))
                                                  .Select(t => TimeSpan.Parse(t)).ToList();

                    // Verify if time already exists
                    for (int i = 0; i < tInit.Count && !exists; i++)
                    {
                        if (TimeExists(h, (h + diff), tInit[i], tEnd[i])) exists = true;
                    }

                    // If so then continue
                    if (exists) continue;

                    // If not then save the time
                    task.InitHour += (h + diff > TimeSpan.FromHours(20)) ? h.ToString() : h.ToString() + ",";
                    task.EndHour += (h + diff > TimeSpan.FromHours(20)) ? (h + diff).ToString() : (h + diff).ToString() + ",";
                }
            }
        }

        /// <summary>
        /// This method verify the Task and return the one task if all the times and day are correct if not then an list with the best time
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private async Task<List<Task>> VerifyTask(Task task)
        {
            List<Task> newTasks = new List<Task>();
            List<Task?> tempTasks;
            List<string> weekdays = task.WeekDays.Split(',').ToList();
            List<string> initHour = task.InitHour.Split(',').ToList();
            List<string> endHour = task.EndHour.Split(',').ToList();
            List<int> index;
            Task newTask;


            // Goes weekday by weekday
            for (int i = 0; i < weekdays.Count; i++)
            {
                // Verify if the list not null and if there is already a weekday
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

                    if (tempTasks.IsNullOrEmpty()) continue;

                    // Get only the tasks that has the same week day
                    tempTasks = tempTasks.Where(t => t!.WeekDays.Split(',').Any(w => w.Equals(weekdays[i]))).ToList();

                    // verify if the list is not null or empty
                    if (!tempTasks.IsNullOrEmpty())
                    {
                        for (int j = 0; j < index.Count; j++)
                        {
                            if (!ExistsSchedule(tempTasks!, initHour[index[j]], endHour[index[j]], weekdays[i])) continue;

                            GetBestTime(tempTasks!, newTask, initHour[index[j]], endHour[index[j]], weekdays[i]);
                        }
                    }
                }

                if (newTask.InitHour.Equals("")) continue;

                Debug.WriteLine(newTask.InitHour);
                Debug.WriteLine(newTask.EndHour);

                // Remove repetead dates
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

                // Save the dates
                newTask.InitHour = string.Join(',', tempInitHour);
                newTask.EndHour = string.Join(',', tempEndHour);
                newTask.TasksRobots.AddRange(task.TasksRobots);

                // Add task to list
                newTasks.Add(newTask);
            }

            // If the list is emty the this mean that everthing went right and save the task inserted
            if (newTasks.IsNullOrEmpty())
                newTasks.Add(task);

            // Return the list
            return newTasks;
        }

        /* A DAR 100% */
        /// <summary>
        /// This method creates a new Task
        /// </summary>
        /// <param name="task">Task Object</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Task>> PostTask(Task? task)
        {
            // Verify if the task receibed is null
            if (task is null) return BadRequest();

            // Verify if the dates are actual dates and if can parse to timespan
            List<TimeSpan> totalInit = task.InitHour.Split(',').Where(t => TimeSpan.TryParse(t, out _))
                                                    .Select(t => TimeSpan.Parse(t)).ToList();

            List<TimeSpan> totalEnd = task.EndHour.Split(',').Where(t => TimeSpan.TryParse(t, out _))
                                                    .Select(t => TimeSpan.Parse(t)).ToList();

            if (totalInit.Count < task.InitHour.Split(',').Count() || totalEnd.Count < task.EndHour.Split(',').Count()) 
                return BadRequest();

            // Verify if all end dates are higher then the init dates
            if (!totalEnd.Zip(totalInit, (end, init) => end >= init).All(c => c)) return BadRequest();

            // Verify tasks
            List<Task> newTasks = await VerifyTask(task);

            // If count is 1 then it passes and can save otherwise return NotFound
            if (!newTasks.Count.Equals(1) || !newTasks.First().InitHour.Equals(task.InitHour)) return NotFound(newTasks);

            // Get the first
            Task newTask = newTasks.First();

            // Save into database
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

            // return task
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
        [Authorize]
        public async Task<IActionResult> PutTask(int id, Task? task)
        {
            // Verify if the task receibed is not null or if the task id are the same
            if (task is null || id != task.Id) return BadRequest();

            // Verify if the dates are actual dates and if can parse to timespan
            List<TimeSpan> totalInit = task.InitHour.Split(',').Where(t => TimeSpan.TryParse(t, out _))
                                                    .Select(t => TimeSpan.Parse(t)).ToList();

            List<TimeSpan> totalEnd = task.EndHour.Split(',').Where(t => TimeSpan.TryParse(t, out _))
                                                    .Select(t => TimeSpan.Parse(t)).ToList();

            if (totalInit.Count < task.InitHour.Split(',').Count() || totalEnd.Count < task.EndHour.Split(',').Count())
                return BadRequest();

            // Verify if all end dates are higher then the init dates
            if (!totalEnd.Zip(totalInit, (end, init) => end >= init).All(c => c)) return BadRequest();

            // Verify task
            List<Task> updateTasks = await VerifyTask(task);

            if (!updateTasks.Count.Equals(1)) return NotFound(updateTasks);

            // Get first
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
        [Authorize]
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
