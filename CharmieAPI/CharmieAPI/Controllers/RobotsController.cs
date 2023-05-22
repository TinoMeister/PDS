using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharmieAPI.Models;
using Environment = CharmieAPI.Models.Environment;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using NuGet.Packaging;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CharmieAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class RobotsController : ControllerBase
    {
        private readonly RobotDbContext _context;

        public RobotsController(RobotDbContext context) => _context = context;

        /* A DAR 100% */
        /// <summary>
        /// This method search in the database for all Robots that has the same client id
        /// </summary>
        /// <param name="clientId">Client's Id</param>
        /// <returns>List of Robots</returns>
        [HttpGet("Client/{clientId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Robot>>> GetRobotsByClient(int clientId)
        {
            // Verify in the database if there are any Robots
            if (_context.Robots.IsNullOrEmpty()) return NotFound();

            // Get all the environments
            List<Environment> environments = await _context.Environments.Where(e => e.ClientId.Equals(clientId)).ToListAsync();

            // Verify is the list is null
            if (environments.IsNullOrEmpty()) return NotFound();

            // Get all the robots
            List<Robot> robots = new List<Robot>();

            foreach (Environment environment in environments)
            {
                robots.AddRange(await _context.Robots.Where(r => r.EnvironmentId.Equals(environment.Id)).ToListAsync());
            }

            // Verify if the robots is null
            if (robots.IsNullOrEmpty()) return NotFound();

            // Return robots
            return Ok(robots);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method search in the database for all Robots that has the same environment id
        /// </summary>
        /// <param name="environmentId">Environment's Id</param>
        /// <returns>List of Robots</returns>
        [HttpGet("Environment/{environmentId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Robot>>> GetRobotsByEnvironment(int environmentId)
        {
            // Verify in the database if there are any Robots
            if (_context.Robots.IsNullOrEmpty()) return NotFound();

            // Get all the Robots that as the same environmentId
            List<Robot> robots = await _context.Robots.Where(r => r.EnvironmentId.Equals(environmentId)).ToListAsync();

            // Verify if the robots is null
            if (robots.IsNullOrEmpty()) return NotFound();

            // Return robots
            return Ok(robots);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method creates a new Robot
        /// </summary>
        /// <param name="robot">Robot Object</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Robot>> PostRobot(Robot? robot)
        {
            // Verify if the robot receibed is null
            if (robot is null) return BadRequest();

            // Add the robot to an entity entry to insert into the database
            _context.Robots.Add(robot);

            // Try to save the entry to the database
            try
            {
                await _context.SaveChangesAsync();

                // Reload object with the info from database
                await _context.Entry(robot).ReloadAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                return BadRequest(ModelState);
            }

            // If is successfully then returns OK
            return Ok(robot);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method updates an Robot
        /// </summary>
        /// <param name="id">Robot's Id</param>
        /// <param name="robot">Robot Object</param>
        /// <returns>Action Result</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutRobot(int id, Robot? robot)
        {
            // Verify if the robot receibed is not null or if the robot id are the same
            if (robot is null || id != robot.Id) return BadRequest();

            // Put the robot as an entry an set the sate as modified to update the database
            _context.Entry(robot).State = EntityState.Modified;

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
        /// This method removes an robot from the database
        /// </summary>
        /// <param name="id">Robot's Id</param>
        /// <returns>Action Result</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRobot(int id)
        {
            // Verify if the are any robots in the database or if the robot is null
            if (_context.Robots.IsNullOrEmpty()) return BadRequest();

            // Get robot temp
            Robot? robot = await _context.Robots.FindAsync(id);

            if (robot is null) return BadRequest();

            List<Warning> warnings = _context.Warnings.Where(w => w.RobotId.Equals(id)).ToList();
            _context.Warnings.RemoveRange(warnings);

            List<TaskRobot> taskRobots = _context.TasksRobots.Where(tr => tr.RobotId.Equals(id)).ToList();
            _context.TasksRobots.RemoveRange(taskRobots);

            // Try to save to database
            try
            {
                await _context.SaveChangesAsync();

                // Put the robot as an entry an set the sate as remove from database
                _context.Robots.Remove(robot);

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
