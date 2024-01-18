using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services.Base
{
    public abstract class BaseService<TEntity, TInputModel>
        where TEntity : class
        where TInputModel : class
    {
        protected readonly DbSet<TEntity> entities;
        protected readonly DbContext dbContext;
        private readonly ILogger logger;

        public BaseService(DbSet<TEntity> entities, DbContext dbContext, ILogger<TEntity> logger)
        {
            this.entities = entities;
            this.dbContext = dbContext;
            this.logger = logger;
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
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var dbContextTransaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
                try
                {
                    var record = await GetRecord(id);
                    if (record is null)
                        return;

                    var updatedRecord = await GetRecord(inputModel);
                    UpdateRecord(record, updatedRecord);

                    await dbContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    this.logger.LogError(ex.Message);
                }
            });
        }

        public async Task Delete(int id)
        {
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var dbContextTransaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
                try
                {
                    var record = await GetRecord(id);
                    if (record is null)
                        return;

                    entities.Remove(record);

                    await dbContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    this.logger.LogError(ex.Message);
                }
            });
        }

        #endregion
    }
}
