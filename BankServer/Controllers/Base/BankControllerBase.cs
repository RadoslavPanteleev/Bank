using BankServer.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BankServer.Controllers.Base
{
    public abstract class BankControllerBase<TEntity> : ControllerBase
        where TEntity : BaseModel
    {
        protected readonly DbSet<TEntity> Entities;
        protected readonly DbContext dbContext;

        public BankControllerBase(DbSet<TEntity> Entities, DbContext dbContext)
        {
            this.Entities = Entities;
            this.dbContext = dbContext;
        }

        protected abstract bool CompareByID(TEntity record, int Id);
        protected abstract int GetID(TEntity record);
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
            var record = await Entities.FindAsync(id);
            if (record is null)
                return NotFound(id);

            return record;
        }

        [SwaggerOperation(Summary = "Create new record")]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "if created")]
        [HttpPost]
        public virtual async Task<ActionResult> Create(TEntity record)
        {
            await Entities.AddAsync(record);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = GetID(record) }, record);
        }

        [SwaggerOperation(Summary = "Update record")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status304NotModified, Description = "if record is already updated from another instance. Please read the latest version of record before edit.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update([SwaggerParameter(Required = true)] TEntity updatedRecord, [SwaggerParameter("specific id", Required = true)] int id)
        {
            var record = await Entities.FindAsync(id);
            if (record is null)
                return NotFound();

            UpdateRecord(record, updatedRecord);

            if (record.UpdateCounter != updatedRecord.UpdateCounter)
                return new StatusCodeResult(StatusCodes.Status304NotModified);

            record.UpdateCounter++;

            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = GetID(record) }, record);
        }

        [SwaggerOperation(Summary = "Delete record by specific id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "if found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "if not found")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([SwaggerParameter("specific id", Required = true)] int id)
        {
            var record = await Entities.FindAsync(id);
            if (record is null)
                return NotFound();

            Entities.Remove(record);

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
