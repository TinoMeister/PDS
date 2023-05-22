using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharmieAPI.Models;
using Environment = CharmieAPI.Models.Environment;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;

namespace CharmieAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        private readonly RobotDbContext _context;

        public EnvironmentsController(RobotDbContext context) => _context = context;

        /* A DAR 100% */
        /// <summary>
        /// This method search in the database for all Environments that has the same client id
        /// </summary>
        /// <param name="clientId">Client's Id</param>
        /// <returns>List of Environment</returns>
        [HttpGet("{clientId}")]
        public async Task<ActionResult<IEnumerable<Environment>>> GetEnvironments(int clientId)
        {
            // Verify in the database if there are any environments
            if (_context.Environments.IsNullOrEmpty()) return NotFound();

            // Get all the environments that as the same clientId
            List<Environment> environment = await _context.Environments.Where(e => e.ClientId.Equals(clientId)).ToListAsync();

            // Verify if the environment is null
            if (environment.IsNullOrEmpty()) return NotFound();

            // Return environment
            return Ok(environment);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method creates a new Environment
        /// </summary>
        /// <param name="environment">Environment Object</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        public async Task<ActionResult<Environment>> PostEnvironment(Environment? environment)
        {
            // Verify if the environment receibed is null
            if (environment is null) return BadRequest();

            // Add the environment to an entity entry to insert into the database
            _context.Environments.Add(environment);

            // Try to save the entry to the database
            try
            {
                await _context.SaveChangesAsync();

                // Reload object with the info from database
                await _context.Entry(environment).ReloadAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                return BadRequest(ModelState);
            }

            // If is successfully then returns OK
            return Ok(environment);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method updates an Environment
        /// </summary>
        /// <param name="id">Environment's Id</param>
        /// <param name="environment">Environment Object</param>
        /// <returns>Action Result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnvironment(int id, Environment? environment)
        {
            // Verify if the environment receibed is not null or if the environment id are the same
            if (environment is null || id != environment.Id) return BadRequest();

            // Put the environment as an entry an set the sate as modified to update the database
            _context.Environments.Entry(environment).State = EntityState.Modified;

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

        /* A DAR 50% */
        /// <summary>
        /// This method removes an environment from the database
        /// </summary>
        /// /// <param name="id">Environment's Id</param>
        /// <returns>Action Result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvironment(int id)
        {
            // Verify if the are any environments in the database or if the environment is null
            if (_context.Environments.IsNullOrEmpty()) return BadRequest();

            Environment? environment = await _context.Environments.FindAsync(id);

            if (environment is null) return BadRequest();

            List<QuantityMaterial> materials = await _context.QuantityMaterials.Where(q => q.EnvironmentId.Equals(id)).ToListAsync();

            _context.QuantityMaterials.RemoveRange(materials);

            List<Robot> robots = await _context.Robots.Where(r => r.EnvironmentId.Equals(id)).ToListAsync();

            _context.Robots.RemoveRange(robots);

            // Put the environment as an entry an set the sate as remove from database
            _context.Environments.Remove(environment);

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
