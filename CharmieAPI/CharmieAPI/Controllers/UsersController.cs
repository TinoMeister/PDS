using Microsoft.AspNetCore.Mvc;
using CharmieAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using CharmieAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CharmieAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly RobotDbContext _context;

        public UsersController(UserManager<IdentityUser> userManager, JwtService jwtService, RobotDbContext context)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _context = context;
        }

        /// <summary>
        /// This method search in the database for a User that has the same username and password
        /// </summary>
        /// <param name="userName">UserName of the User</param>
        /// <param name="password">Password of the User</param>
        /// <returns>User</returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound();

            Identity? tempUser = _context.Identities.Include(i => i.Clients).Include(i => i.Companies)
                                            .FirstOrDefault(i => i.Id.Equals(user.Id));

            if (tempUser == null) return NotFound();

            return new User
            { 
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Identity = tempUser
            };
        }

        /// <summary>
        /// This method creates a new User
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>User Object</returns>
        [HttpPost("Client")]
        public async Task<ActionResult<User>> PostUserClient(User? user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // If the userName alredy exists return BadRequest
            if (await _userManager.Users.AnyAsync(u => u.UserName.Equals(user.UserName))) return BadRequest();

            var result = await _userManager.CreateAsync(
                new IdentityUser() { UserName = user.UserName, Email = user.Email },
                user.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            IdentityUser tempUser = await _userManager.FindByNameAsync(user.UserName);

            user.Id = tempUser.Id;

            user.Identity = new Identity
            {
                Id = tempUser.Id,
                Name = user.Identity.Name
            };

            _context.Identities.Add(user.Identity);

            Client client = new Client { IdentityId = tempUser.Id };

            _context.Clients.Add(client);

            try
            {
                await _context.SaveChangesAsync();

                await _context.Entry(client).ReloadAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                return BadRequest(ModelState);
            }

            user.Password = null;
            return Ok(user);
        }

        /// <summary>
        /// This method creates a new User
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>User Object</returns>
        [HttpPost("Company")]
        public async Task<ActionResult<User>> PostUserCompany(User? user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // If the userName alredy exists return BadRequest
            if (await _userManager.Users.AnyAsync(u => u.UserName.Equals(user.UserName))) return BadRequest();

            var result = await _userManager.CreateAsync(
                new IdentityUser() { UserName = user.UserName, Email = user.Email },
                user.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            IdentityUser tempUser = await _userManager.FindByNameAsync(user.UserName);

            user.Id = tempUser.Id;

            user.Identity = new Identity
            {
                Id = tempUser.Id,
                Name = user.Identity.Name
            };

            _context.Identities.Add(user.Identity);

            Company company = new Company { IdentityId = tempUser.Id };

            _context.Companies.Add(company);

            try
            {
                await _context.SaveChangesAsync();

                await _context.Entry(company).ReloadAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                return BadRequest(ModelState);
            }

            user.Password = null;
            return Ok(user);
        }

        [HttpPost("BearerToken")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtService.CreateToken(user);

            return Ok(token);
        }

        /*

        [HttpGet("{userName}/{password}")]
        public async Task<ActionResult<User>> GetUsers(string userName, string password)
        {
            // Verify in the database if there are any uses with the userName and password
            User? user = await _context.Users.Include(u => u.Clients).Include(u => u.Companies)
                                .FirstOrDefaultAsync(u => u.Username.Equals(userName) &&
                                 u.Password.Equals(password));

            // Verify if the user receibed is null
            if (user is null) return NotFound();

            // Return the user
            return Ok(user);
        }

        
        /// <summary>
        /// This method updates an User
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <param name="user">User object</param>
        /// <returns>Action Result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User? user)
        {
            // Verify if the user receibed is not null or if the user id are the same
            if (user is null || id != user.Id) return BadRequest();

            // If the userName alredy exists then return BadRequest
            if (await _context.Users.FirstOrDefaultAsync(u => !u.Id.Equals(user.Id) && u.Username.Equals(user.Username)) is not null)
                return BadRequest();

            // Put the user as an entry an set the sate as modified to update the database
            _context.Entry(user).State = EntityState.Modified;

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

        
        /// <summary>
        /// This method removes an User from the database
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>Action Result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Verify if the are any users in the database or if the user is null
            if (_context.Users.IsNullOrEmpty()) return BadRequest();

            User? user = await _context.Users.FindAsync(id);

            if (user is null) return BadRequest();

            Client? client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId.Equals(user.Id));
            Company? company = await _context.Companies.FirstOrDefaultAsync(c => c.UserId.Equals(user.Id));

            // Put the user as an entry an set the sate as remove from database
            if (client is not null)
            {
                List<Environment>? environments = await _context.Environments.Where(e => e.ClientId.Equals(client.Id)).ToListAsync();
                List<Robot> robots = new List<Robot>();
                List<TaskRobot> taskRobots = new List<TaskRobot>();
                List<Task> tasks = new List<Task>();
                List<Warning> warnings = new List<Warning>();

                foreach (Environment environment in environments)
                {
                    robots.AddRange(_context.Robots.Where(r => r.EnvironmentId.Equals(environment.Id)).ToList());
                }

                foreach (Robot robot in robots)
                {
                    taskRobots.AddRange(_context.TasksRobots.Where(t => t.RobotId.Equals(robot.Id)).ToList());
                    warnings.AddRange(_context.Warnings.Where(w => w.RobotId.Equals(robot.Id)).ToList());
                }

                foreach (TaskRobot taskR in taskRobots)
                {
                    if (_context.TasksRobots.Where(tr => tr.TaskId.Equals(taskR.TaskId)).Count().Equals(1))
                        tasks.Add(_context.Tasks.First(t => t.Id.Equals(taskR.TaskId)));
                }

                _context.Warnings.RemoveRange(warnings);

                _context.TasksRobots.RemoveRange(taskRobots);

                _context.Tasks.RemoveRange(tasks);

                _context.Robots.RemoveRange(robots);

                _context.Environments.RemoveRange(environments);

                _context.Clients.Remove(client);
            }

            if (company is not null)
            {
                //_context.Warnings.RemoveRange(_context.Warnings.Where(w => w.UserId.Equals(user.Id)).ToList());
                _context.Companies.Remove(company);
            }

            _context.Users.Remove(user);

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
        */
    }
}
