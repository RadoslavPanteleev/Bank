using BankServer.Models.Base;
using BankServer.Services.Base;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BankServer.Controllers.Base
{
    public class BankControllerBase<TEntity, TInputModel, TService> : ControllerBase
        where TEntity : BaseModel
        where TInputModel : class
        where TService : BaseService<TEntity, TInputModel>
    {
        private readonly TService service;
        public BankControllerBase(TService service)
        {
            this.service = service;
        }

        [SwaggerOperation(Summary = "Get all records.")]
        [HttpGet]
        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await service.GetAll();
        }

        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get([SwaggerParameter("specific id", Required = true)] int id)
        {
            var record = await service.GetRecord(id);
            if (record is null)
                return NotFound(id);

            return record;
        }

        [SwaggerOperation(Summary = "Create new record")]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "if created")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if error occured")]
        [HttpPost]
        public virtual async Task<ActionResult> CreateAsync([FromBody] TInputModel inputModel)
        {
            try
            {
                var record = await service.CreateAsync(inputModel);
                if (record is null)
                    throw new Exception("record is null");

                return CreatedAtAction(nameof(Get), new { id = service.GetID(record) }, record);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [SwaggerOperation(Summary = "Update record")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status304NotModified, Description = "if record is already updated from another instance. Please read the latest version of record before edit.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if not found")]
        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update([SwaggerParameter(Required = true)] TInputModel inputModel, [SwaggerParameter("specific id", Required = true)] int id)
        {
            try
            {
                await service.Update(inputModel, id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status304NotModified, new Response { Status = "Error", Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [SwaggerOperation(Summary = "Delete record by specific id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "if not found or other server internal error")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([SwaggerParameter("specific id", Required = true)] int id)
        {
            await service.Delete(id);
            return Ok();
        }
    }
}
