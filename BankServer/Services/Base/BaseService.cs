using BankServer.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services.Base
{
    public abstract class BaseService<TEntity, TInputModel>
        where TEntity : BaseModel
        where TInputModel : class
    {
        protected readonly DbSet<TEntity> entities;
        protected readonly DbContext dbContext;

        public BaseService(DbSet<TEntity> entities, DbContext dbContext)
        {
            this.entities = entities;
            this.dbContext = dbContext;
        }

        #region Virtuals

        /// <summary> </summary>
        /// <param name="record"></param>
        /// <returns> Returns ID from record </returns>
        public abstract int GetID(TEntity record);

        protected abstract Task<TEntity> GetRecord(TInputModel inputModel);

        public virtual async Task<TEntity?> GetRecord(int id)
        {
            return await entities.FindAsync(id);
        }

        protected abstract void UpdateRecord(TEntity destination, TEntity source);

        #endregion

        #region Public methods

        public virtual async Task<IList<TEntity>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public virtual async Task<TEntity?> Get(int id)
        {
            return await GetRecord(id);
        }

        public virtual async Task<TEntity?> CreateAsync(TInputModel inputModel)
        {
            var record = await GetRecord(inputModel);

            await entities.AddAsync(record);
            await dbContext.SaveChangesAsync();

            return record;
        }

        public virtual async Task Update(TInputModel inputModel, int id)
        {
            using (var dbContextTransaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                var record = await GetRecord(id);
                if (record is null)
                    return;

                var updatedRecord = await GetRecord(inputModel);
                if (record.UpdateCounter != updatedRecord.UpdateCounter)
                    throw new DbUpdateConcurrencyException();

                UpdateRecord(record, updatedRecord);

                record.UpdateCounter++;

                await dbContext.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (var dbContextTransaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                var record = await GetRecord(id);
                if (record is null)
                    return;

                entities.Remove(record);

                await dbContext.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
        }

        #endregion
    }
}
