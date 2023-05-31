using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharmieAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
        [Authorize]
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

            // Goes element by element
            foreach (QuantityMaterial quantityMaterial in quantityMaterials)
            {
                // Verify if material name exists
                Material? tempMat = await _context.Materials.FirstOrDefaultAsync(m => m.Name.Equals(quantityMaterial.Material.Name));

                // Verify if the materiasl exists and the task id is not null
                if (tempMat is null && quantityMaterial.TaskId is not null) return BadRequest("Material does not exists");

                // If not then save it
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
                else 
                { 
                    // Get the id
                    quantityMaterial.Material.Id = tempMat.Id;

                    // Get the quatity material that exists in the DB for the material and environment
                    QuantityMaterial? qtTemp = await _context.QuantityMaterials.FirstOrDefaultAsync(qt => qt.MaterialId.Equals(tempMat.Id) &&
                                                                                                     qt.EnvironmentId.Equals(quantityMaterial.EnvironmentId) &&
                                                                                                     qt.TaskId.Equals(null));

                    if (quantityMaterial.TaskId is not null)
                    {
                        // verify if is null
                        if (qtTemp is null) return BadRequest("Material doe not exists in the environment");

                        // Verify the quatity if is lower then the existence quantity in the environment
                        if (qtTemp.Quantity < quantityMaterial.Quantity) return BadRequest("Quantity is higher then the quantity exists in the environment");
                    }
                    else
                    {
                        // verify if is not null
                        if (qtTemp is not null) return BadRequest("Material already exists in the environment");
                    }
                }

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
                                            .FirstOrDefault(q => (q.EnvironmentId.Equals(tempQuant.EnvironmentId) &&
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

            var resp;
            // Save into database
            if (!quantsAdd.IsNullOrEmpty())
            {
                resp = await PostQuantityMaterial(quantsAdd);

                // If there is an error, return the error
                if (resp.Result is BadRequestObjectResult)
                {
                    var badRequestResult = resp.Result as BadRequestObjectResult;
                    var errorDetails = badRequestResult.Value;

                    return BadRequest(errorDetails);
                }
            }

            // Delete Old Quantity Materias
            resp = await DeleteOldQuantityMaterial(tempQuant.EnvironmentId, tempQuant.TaskId, quantityMaterials);

            // If there is an error, return the error
            if (resp is BadRequestObjectResult)
            {
                var badRequestResult = resp as BadRequestObjectResult;
                var errorDetails = badRequestResult.Value;

                return BadRequest(errorDetails);
            }


            // Get Quantity and verify wich one is to update
            foreach (QuantityMaterial quant in quantityMaterials)
            {
                QuantityMaterial? temp = _context.QuantityMaterials.Include(q => q.Material)
                                            .FirstOrDefault(q => q.Id.Equals(quant.Id));

                if (temp is null) continue;

                if (quant.TaskId is not null)
                {
                    // Get the quatity material that exists in the DB for the material and environment
                    QuantityMaterial? qtTemp = await _context.QuantityMaterials.FirstOrDefaultAsync(qt => qt.MaterialId.Equals(quant.MaterialId) &&
                                                                                                 qt.EnvironmentId.Equals(quant.EnvironmentId) &&
                                                                                                 qt.TaskId.Equals(null));

                    // verify if is null
                    if (qtTemp is null) return BadRequest("Material doe not exists in the environment");

                    // Verify the quatity if is lower then the existence quantity in the environment
                    if (qtTemp.Quantity < quant.Quantity) return BadRequest("Quantity is higher then the quantity exists in the environment");
                }
                else
                {
                    // Get all the quantity materials with the environment
                    List<QuantityMaterial> qtTemps = await _context.QuantityMaterials.Where(qt => qt.MaterialId.Equals(quant.MaterialId) &&
                                                                                                 qt.EnvironmentId.Equals(quant.EnvironmentId)).ToListAsync();

                    // Verify the quantity in a task
                    if (qtTemps.Any(qt => qt.Quantity > quant.Quantity)) return BadRequest("Quantity is lower then the quantity exists in a certain task");
                }

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
        private async Task<IActionResult> DeleteOldQuantityMaterial(int envId, int? taskId, List<QuantityMaterial> quants)
        {
            List<QuantityMaterial> quantsRemove = new List<QuantityMaterial>();

            // Goes element by element and add to the list quantsRemove all the elements that doesnt have the same id in Environment
            foreach (QuantityMaterial quant in await _context.QuantityMaterials.Where(q => q.EnvironmentId.Equals(envId) &&
                                                                                           q.TaskId.Equals(taskId)).ToListAsync())
            {
                if (quants.Any(q => q.Id.Equals(quant.Id))) continue;

                quantsRemove.Add(quant);
            }

            // Put the list to be removed
            _context.RemoveRange(quantsRemove);

            // Save to the database
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

        /* A DAR 100% vale a pena?? */
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
