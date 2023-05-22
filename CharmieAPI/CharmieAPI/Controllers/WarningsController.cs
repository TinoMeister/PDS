using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharmieAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace CharmieAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class WarningsController : ControllerBase
    {
        private readonly RobotDbContext _context;

        public WarningsController(RobotDbContext context) => _context = context;

        /* A DAR 100% */
        /// <summary>
        /// This method gets all the warnings
        /// </summary>
        /// <returns>Warning List</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Warning>>> GetWarnings()
        {
            if (_context.Warnings.IsNullOrEmpty()) return NotFound();

            return await _context.Warnings.ToListAsync();
        }

        /* A DAR 100% */
        /// <summary>
        /// This method gets all the warning by a certain robot
        /// </summary>
        /// <param name="robotId">Robot's Id</param>
        /// <returns>Warning List</returns>
        [HttpGet("{robotId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Warning>>> GetAllWarnings(int robotId)
        {
            if (_context.Warnings.IsNullOrEmpty()) return NotFound();

            return await _context.Warnings.Where(w => w.RobotId.Equals(robotId)).ToListAsync();
        }

        /* A DAR 100% */
        /// <summary>
        /// This method creates a new Warning
        /// </summary>
        /// <param name="warning"></param>
        /// <returns>Action Result</returns>
        [HttpPost]
        public async Task<ActionResult<Warning>> PostWarning(Warning? warning)
        {
            // Verify if the warning receibed is null
            if (warning is null || warning.IdentityId is not null) return BadRequest();

            // Set state of warning to CREATED
            warning.State = WarningStates.CREATED;

            // Add the warning to an entity entry to insert into the database
            _context.Warnings.Add(warning);

            // Try to save the entry to the database
            try
            {
                await _context.SaveChangesAsync();

                // Reload object with the info from database
                await _context.Entry(warning).ReloadAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                return BadRequest(ModelState);
            }

            // If is successfully then returns OK
            return Ok(warning);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method updates an Warning
        /// </summary>
        /// <param name="id">Warning's Id</param>
        /// <param name="warning">Warning Object</param>
        /// <returns>Action Result</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutWarning(int id, Warning? warning)
        {
            // Verify if the warning receibed is not null or if the warning id are the same
            if (warning is null || !id.Equals(warning.Id) || warning.HourDay < DateTime.UtcNow) return BadRequest();

            // Get warning
            Warning? temp = await _context.Warnings.FindAsync(id);

            // Verify if exists
            if (temp is null) return BadRequest();

            // Verify if the states are not equal to DENY or COMPLETED
            if (!warning.State.Equals(WarningStates.DENY) && !warning.State.Equals(WarningStates.COMPLETED))
            {
                // DEpednding of the conditions get the state
                if (temp.IdentityId is null || temp.IdentityId.Equals(warning.IdentityId)) warning.State = WarningStates.AWAITING;
                else if (!temp.HourDay.Equals(warning.HourDay)) warning.State = WarningStates.PROPOSAL;
                else warning.State = WarningStates.CONFIRMED;
            }

            // Put the new info on the temp variable
            temp.Message = warning.Message;
            temp.HourDay = warning.HourDay;
            temp.IdentityId = warning.IdentityId;
            temp.State = warning.State;

            // Put the environment as an entry an set the sate as modified to update the database
            _context.Entry(temp).State = EntityState.Modified;

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
        /// This method removes an warning from the database
        /// </summary>
        /// <param name="id">Warning's Id</param>
        /// <returns>Action Result</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteWarning(int id)
        {
            // Verify if the are any warnings in the database or if the warning is null
            if (_context.Warnings.IsNullOrEmpty()) return BadRequest();

            Warning? warning = await _context.Warnings.FindAsync(id);

            if (warning is null) return BadRequest();

            // Put the environment as an entry an set the sate as remove from database
            _context.Warnings.Remove(warning);

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
