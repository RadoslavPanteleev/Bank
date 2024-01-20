using Microsoft.EntityFrameworkCore;

namespace BankServer.Services.Base
{
    public abstract class RepositoryBaseService<TEntity, TInputModel>
        where TEntity : class
        where TInputModel : class
    {
        protected readonly DbContext dbContext;
        private readonly ILogger logger;

        public RepositoryBaseService(DbContext dbContext, ILogger<TEntity> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        #region Virtuals

        /// <summary> </summary>
        /// <param name="record"></param>
        /// <returns> Returns ID from record </returns>
        public abstract int GetID(TEntity record);

        protected abstract Task<TEntity> MapModel(TInputModel inputModel);

        public virtual async Task<TEntity?> GetRecordAsync(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        protected abstract void CopyValues(TEntity destination, TEntity source);

        #endregion

        #region Public methods

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await GetRecordAsync(id);
        }

        public virtual async Task<TEntity?> CreateAsync(TInputModel inputModel)
        {
            var record = await MapModel(inputModel);

            await dbContext.Set<TEntity>().AddAsync(record);
            await dbContext.SaveChangesAsync();

            return record;
        }

        public virtual async Task UpdateAsync(TInputModel inputModel, int id)
        {
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var dbContextTransaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
                try
                {
                    var record = await GetRecordAsync(id);
                    if (record is null)
                        return;

                    var updatedRecord = await MapModel(inputModel);
                    CopyValues(record, updatedRecord);

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

        public async Task DeleteAsync(int id)
        {
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var dbContextTransaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
                try
                {
                    var record = await GetRecordAsync(id);
                    if (record is null)
                        return;

                    dbContext.Set<TEntity>().Remove(record);

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
