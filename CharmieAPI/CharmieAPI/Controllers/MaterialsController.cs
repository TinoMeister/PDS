using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharmieAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;

namespace CharmieAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly RobotDbContext _context;

        public MaterialsController(RobotDbContext context) => _context = context;

        /* ??? */
        /// <summary>
        /// This method gets all the materials
        /// </summary>
        /// <returns>Material List</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            // Verify in the database if there are any materials
            if (_context.Materials.IsNullOrEmpty()) return NotFound();

            // Return materials
            return Ok(await _context.Materials.ToListAsync());
        }

        /* A DAR 100% */
        /// <summary>
        /// This method search in the database for Material that has the same name
        /// </summary>
        /// <param name="name">Material Name</param>
        /// <returns>Material Object</returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<Material>> GetMaterials(string name)
        {
            // Verify in the database if there are any materials
            if (_context.Materials.IsNullOrEmpty()) return NotFound();

            // Get the material that as the same name
            Material? material = await _context.Materials.FirstOrDefaultAsync(m => m.Name.Equals(name));

            // Verify if the material is null
            if (material is null) return NotFound();

            // Return material
            return Ok(material);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method creates a Materials
        /// </summary>
        /// <param name="material">Material</param>
        /// <returns>List Materials</returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Material>>> PostMaterial(Material material)
        {
            // Verify if the material receibed is null
            if (material is null) return BadRequest();

            // Verify if the material already exists
            Material? temp = await _context.Materials.FirstOrDefaultAsync(m => m.Name.Equals(material.Name));

            if (temp is not null) return BadRequest();

            // Add material
            _context.Materials.Add(material);

            try
            {
                // save the material and quantity
                await _context.SaveChangesAsync();
                // get the material inserted with all the values
                await _context.Entry(material).ReloadAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                return BadRequest(ModelState);
            }

            // If is successfully then returns the OK
            return Ok(material);
        }

        /* A DAR 100% mas será util?? */
        /// <summary>
        /// This method updates an Material
        /// </summary>
        /// <param name="id">Material's Id</param>
        /// <param name="material">Material Object</param>
        /// <returns>Action Result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(int id, Material? material)
        {
            // Verify if the material receibed is not null or if the material id are the same
            if (material is null || id != material.Id) return BadRequest();

            // Verify if exists
            Material? temp = await _context.Materials.FirstOrDefaultAsync(m => !m.Id.Equals(id) && m.Name.Equals(material.Name));

            if (temp is not null) return BadRequest();

            // Put the material as an entry an set the sate as modified to update the database
            _context.Entry(material).State = EntityState.Modified;

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

        /* A DAR 100%*/
        /// <summary>
        /// This method removes an material from the database
        /// </summary>
        /// <param name="id">Material's Id</param>
        /// <returns>Action Result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            // Verify if the are any environments in the database or if the environment is null
            if (_context.Materials.IsNullOrEmpty()) return BadRequest();

            // Get material
            Material? material = await _context.Materials.FindAsync(id);

            if (material is null) return BadRequest();

            // Get all the quantity material of that material
            List<QuantityMaterial> quantMaterials = await _context.QuantityMaterials.Where(qt => qt.MaterialId.Equals(id)).ToListAsync();

            // Delete all the quantity material
            _context.QuantityMaterials.RemoveRange(quantMaterials);


            // Try to save to database
            try
            {
                await _context.SaveChangesAsync();

                // Put the material as an entry an set the sate as remove from database
                _context.Materials.Remove(material);

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
