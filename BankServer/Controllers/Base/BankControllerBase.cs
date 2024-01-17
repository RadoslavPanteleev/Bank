using BankServer.Models.Base;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BankServer.Controllers.Base
{
    public abstract class BankControllerBase<TEntity, TInputModel> : ControllerBase
        where TEntity : BaseModel
        where TInputModel : class
    {
        protected readonly DbSet<TEntity> Entities;
        protected readonly DbContext dbContext;

        public BankControllerBase(DbSet<TEntity> Entities, DbContext dbContext)
        {
            this.Entities = Entities;
            this.dbContext = dbContext;
        }

        protected abstract int GetID(TEntity record);
        protected abstract Task<TEntity> GetRecord(TInputModel inputModel);
        protected virtual async Task<TEntity?> GetRecord(int id)
        {
            return await Entities.FindAsync(id);
        }
        protected abstract void UpdateRecord(TEntity destination, TEntity source);

        [SwaggerOperation(Summary = "Get all records.")]
        [HttpGet]
        public virtual async Task<ActionResult<IList<TEntity>>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        [SwaggerOperation(Summary = "Get record by specific id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "record if found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get([SwaggerParameter("specific id", Required = true)] int id)
        {
            var record = await GetRecord(id);
            if (record is null)
                return NotFound(id);

            return record;
        }

        [SwaggerOperation(Summary = "Create new record")]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "if created")]
        [HttpPost]
        public virtual async Task<ActionResult> Create(TInputModel inputModel)
        {
            try
            {
                var record = await GetRecord(inputModel);

                await Entities.AddAsync(record);

                await dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = GetID(record) }, record);
            }
            catch(KeyNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [SwaggerOperation(Summary = "Update record")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status304NotModified, Description = "if record is already updated from another instance. Please read the latest version of record before edit.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update([SwaggerParameter(Required = true)] TInputModel inputModel, [SwaggerParameter("specific id", Required = true)] int id)
        {
            using (var dbContextTransaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                var record = await GetRecord(id);
                if (record is null)
                    return NotFound();

                try
                {
                    var updatedRecord = await GetRecord(inputModel);

                    if (record.UpdateCounter != updatedRecord.UpdateCounter)
                        return StatusCode(StatusCodes.Status304NotModified);

                    UpdateRecord(record, updatedRecord);
                }
                catch (KeyNotFoundException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
                }

                record.UpdateCounter++;

                await dbContext.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();

                return CreatedAtAction(nameof(Get), new { id = GetID(record) }, record);
            }

            
        }

        [SwaggerOperation(Summary = "Delete record by specific id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([SwaggerParameter("specific id", Required = true)] int id)
        {
            var record = await GetRecord(id);
            if (record is null)
                return NotFound();

            Entities.Remove(record);

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
