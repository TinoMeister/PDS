using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharmieAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using NuGet.Protocol;

namespace CharmieAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class QuantityMaterialsController : ControllerBase
    {
        private readonly RobotDbContext _context;

        public QuantityMaterialsController(RobotDbContext context) => _context = context;

        /* A DAR 100% */
        /// <summary>
        /// This method search in the database for all QuantityMaterial that has the same environment id
        /// </summary>
        /// <param name="environmentId">Environment's Id</param>
        /// <returns>List of QuantityMaterial</returns>
        [HttpGet("Environment/{environmentId}")]
        public async Task<ActionResult<IEnumerable<QuantityMaterial>>> GetQuantityMaterialsByEnvironment(int environmentId)
        {
            // Verify in the database if there are any materials
            if (_context.QuantityMaterials.IsNullOrEmpty()) return NotFound();

            // Get all the quantityMaterials that as the same environment Id
            List<QuantityMaterial> quantityMaterials = await _context.QuantityMaterials.Include(q => q.Material)
                                                            .Where(q => q.EnvironmentId.Equals(environmentId)).ToListAsync();

            // Verify if the quantityMaterials is null
            if (quantityMaterials.IsNullOrEmpty()) return NotFound();

            // Return quantityMaterials
            return Ok(quantityMaterials);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method search in the database for all QuantityMaterial that has the same task id
        /// </summary>
        /// <param name="taskId">Task's Id</param>
        /// <returns>List of QuantityMaterial</returns>
        [HttpGet("Task/{taskId}")]
        public async Task<ActionResult<IEnumerable<QuantityMaterial>>> GetQuantityMaterialsByTask(int taskId)
        {
            // Verify in the database if there are any materials
            if (_context.QuantityMaterials.IsNullOrEmpty()) return NotFound();

            // Get all the quantityMaterials that as the same task Id
            List<QuantityMaterial> quantityMaterials = await _context.QuantityMaterials.Where(q => q.TaskId.Equals(taskId)).ToListAsync();

            // Verify if the quantityMaterials is null
            if (quantityMaterials.IsNullOrEmpty()) return NotFound();

            // Return quantityMaterials
            return Ok(quantityMaterials);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method creates a new QuantityMaterial
        /// </summary>
        /// <param name="quantityMaterials">QuantityMaterial List</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<QuantityMaterial>>> PostQuantityMaterial(List<QuantityMaterial> quantityMaterials)
        {
            // Verify if the quantityMaterial receibed is null
            if (quantityMaterials.IsNullOrEmpty()) return BadRequest();

            foreach (QuantityMaterial quantityMaterial in quantityMaterials)
            {
                Material? tempMat = await _context.Materials.FirstOrDefaultAsync(m => m.Name.Equals(quantityMaterial.Material.Name));

                if (tempMat is null)
                {
                    _context.Materials.Add(quantityMaterial.Material);

                    try
                    {
                        // save the material
                        await _context.SaveChangesAsync();
                        // get the material inserted with all the values
                        await _context.Entry(quantityMaterial.Material).ReloadAsync();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                        return BadRequest(ModelState);
                    }
                }
                else quantityMaterial.Material.Id = tempMat.Id;

                // update material id
                quantityMaterial.MaterialId = quantityMaterial.Material.Id;

                // Create quantity
                QuantityMaterial newQuantityMaterial = new QuantityMaterial
                {
                    Quantity = quantityMaterial.Quantity,
                    MaterialId = quantityMaterial.MaterialId,
                    EnvironmentId = quantityMaterial.EnvironmentId,
                    TaskId = quantityMaterial.TaskId
                };

                _context.QuantityMaterials.Add(newQuantityMaterial);

                try
                {
                    // save the quantity
                    await _context.SaveChangesAsync();
                    // get the material inserted with all the values
                    await _context.Entry(newQuantityMaterial).ReloadAsync();
                    quantityMaterial.Id = newQuantityMaterial.Id;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", $"There was an error saving changes: {ex.Message}");
                    return BadRequest(ModelState);
                }
            }

            return Ok(quantityMaterials);
        }

        /* A DAR 100% */
        /// <summary>
        /// This method updates an QuantityMaterial
        /// </summary>
        /// <param name="quantityMaterials">QuantityMaterial List</param>
        /// <returns>Action Result</returns>
        [HttpPut]
        public async Task<IActionResult> PutQuantityMaterial(List<QuantityMaterial> quantityMaterials)
        {
            // Verify if the quantityMaterial receibed is not null or if the quantityMaterial id are the same
            if (quantityMaterials.IsNullOrEmpty()) return BadRequest();

            QuantityMaterial tempQuant = quantityMaterials.First();
            List<QuantityMaterial> quantsAdd = new List<QuantityMaterial>();

            // Get Info of quantities materials and if not exists then insert
            foreach (QuantityMaterial quant in quantityMaterials)
            {
                QuantityMaterial? temp = _context.QuantityMaterials.Include(q => q.Material)
                                            .FirstOrDefault(q => (q.EnvironmentId.Equals(tempQuant.EnvironmentId) ||
                                                q.TaskId.Equals(tempQuant.TaskId)) &&
                                                q.Material.Name.Equals(quant.Material.Name)
                                            );
                // Add to list to insert
                if (temp is null)
                {
                    quantsAdd.Add(quant);
                    continue;
                }

                // Update the info
                quant.Id = temp.Id;
                quant.MaterialId = temp.MaterialId;
                quant.TaskId = temp.TaskId;
                quant.Material = temp.Material;
            }

            // Save into database
            if (!quantsAdd.IsNullOrEmpty()) await PostQuantityMaterial(quantsAdd);

            // Delete Old Quantity Materias
            await DeleteOldQuantityMaterial(tempQuant.EnvironmentId, tempQuant.TaskId, quantityMaterials);

            // Get Quantity and verify wich one is to update
            foreach (QuantityMaterial quant in quantityMaterials)
            {
                QuantityMaterial? temp = _context.QuantityMaterials.Include(q => q.Material)
                                            .FirstOrDefault(q => q.Id.Equals(quant.Id));

                if (temp is null) continue;

                // Update the Quantity
                if (!quant.Quantity.Equals(temp.Quantity))
                {
                    temp.Quantity = quant.Quantity;

                    _context.Entry(temp).State = EntityState.Modified;
                }
            }

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
        /// This method Deletes all the old Quantity Materials
        /// </summary>
        /// <param name="envId">Environment's Id</param>
        /// <param name="taskId">Task's Id</param>
        /// <param name="quants">List Quantities</param>
        /// <returns>Action Result</returns>
        private async Task<IActionResult> DeleteOldQuantityMaterial(int? envId, int? taskId, List<QuantityMaterial> quants)
        {
            List<QuantityMaterial> quantsRemove = new List<QuantityMaterial>();

            foreach (QuantityMaterial quant in await _context.QuantityMaterials.Where(q => q.EnvironmentId.Equals(envId)).ToListAsync())
            {
                if (quants.Any(q => q.Id.Equals(quant.Id))) continue;

                quantsRemove.Add(quant);
            }

            foreach (QuantityMaterial quant in await _context.QuantityMaterials.Where(q => q.TaskId.Equals(taskId)).ToListAsync())
            {
                if (quants.Any(q => q.Id.Equals(quant.Id))) continue;

                quantsRemove.Add(quant);
            }

            if (quantsRemove.IsNullOrEmpty()) return Ok();

            _context.RemoveRange(quantsRemove);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"There was an error removing: {ex.Message}");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        /* A DAR 70% vale a pena?? */
        /// <summary>
        /// This method removes an QuantityMaterial from the database
        /// </summary>
        /// <param name="id">QuantityMaterial's Id</param>
        /// <returns>Action Result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuantityMaterial(int id)
        {
            // Verify if the are any QuantityMaterials in the database or if the quantityMaterial is null
            if (_context.QuantityMaterials.IsNullOrEmpty()) return BadRequest();

            QuantityMaterial? quantityMaterial = await _context.QuantityMaterials.FindAsync(id);

            if (quantityMaterial is null) return BadRequest();

            // Put the quantityMaterial as an entry an set the sate as remove from database
            _context.QuantityMaterials.Remove(quantityMaterial);

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
