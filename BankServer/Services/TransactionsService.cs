using BankServer.Entities;
using BankServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankServer.Services
{
    public class TransactionsService
    {
        private readonly AppDbContext appDdContext;
        private readonly TransactionTypesService transactionTypesService;
        private readonly BanksService banksService;
        private readonly LocationsService locationsService;
        private readonly AccountsService accountsService;
        private readonly CategoriesService categoriesService;
        private readonly ILogger logger;

        public TransactionsService(
            AppDbContext appDdContext,
            TransactionTypesService transactionTypesService, 
            BanksService banksService, 
            LocationsService locationsService,
            AccountsService accountsService,
            CategoriesService categoriesService, 
            ILogger<TransactionsService> logger)
        {
            this.appDdContext = appDdContext;
            this.transactionTypesService = transactionTypesService;
            this.banksService = banksService;
            this.locationsService = locationsService;
            this.accountsService = accountsService;
            this.categoriesService = categoriesService;
            this.logger = logger;
        }

        public async Task<IList<TransactionOutputModel?>> GetAllAsync(string personId)
        {
            return await appDdContext.Transactions
                .Include(t => t.TransactionType)
                .Include(t => t.Bank)
                .Include(t => t.Bank!.PhoneNumber)
                .Include(t => t.Location)
                .Include(t => t.Location!.Address)
                .Include(t => t.Account)
                .Include(t => t.Category)
                .Where(t => t.Account!.PersonId == personId)
                
                .Select(t => FromModel(t))
                .ToListAsync();
        }

        public async Task<TransactionOutputModel?> GetTransactionByIdAsync(int transactionId, string personId)
        {
            return FromModel(
                await appDdContext.Transactions
                .Include(t => t.TransactionType)
                .Include(t => t.Bank)
                .Include(t => t.Bank!.PhoneNumber)
                .Include(t => t.Location)
                .Include(t => t.Location!.Address)
                .Include(t => t.Account)
                //.Include(t => t.Account!.Person)
                //.Include(t => t.Account!.Person!.Address)
                .Include(t => t.Category)
                .Where(t => t.Account!.PersonId == personId)
                
                .SingleOrDefaultAsync(t => t.Id == transactionId));
        }

        public async Task<Transaction> CreateTransactionAsync(string personId, AddTransactionModel addTransactionModel)
        {
            var strategy = appDdContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var dbContextTransaction = await appDdContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);
                try
                {
                    if (addTransactionModel.Amount <= 0.00)
                        throw new Exception($"Transaction amount can't be zero or negative!");

                    var transactionType = await transactionTypesService.Get(addTransactionModel.TypeId);
                    if (transactionType is null)
                        throw new Exception($"Transaction type with id {addTransactionModel.BankId} was not found!");

                    var bank = await banksService.Get(addTransactionModel.BankId);
                    if (bank is null)
                        throw new Exception($"Bank with id {addTransactionModel.BankId} was not found!");

                    var location = await locationsService.Get(addTransactionModel.LocationId);
                    if (location is null)
                        throw new Exception($"Location with id {addTransactionModel.LocationId} was not found!");

                    var account = await accountsService.GetRecord(addTransactionModel.AccountNumber, personId);
                    if (account is null)
                        throw new Exception($"Account with number {addTransactionModel.AccountNumber} was not found!");

                    var category = await categoriesService.Get(addTransactionModel.CategoryId);
                    if (category is null)
                        throw new Exception($"Category with id '{addTransactionModel.CategoryId}' was not found!");

                    var transaction = new Transaction
                    {
                        Date = addTransactionModel.Date,
                        Amount = addTransactionModel.Amount,
                        TransactionType = transactionType,
                        Bank = bank,
                        Location = location,
                        Account = account,
                        Category = category
                    };

                    await appDdContext.Transactions.AddAsync(transaction);

                    await appDdContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    return transaction;
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    this.logger.LogError(ex.Message);

                    throw;
                }
            });
        }

        public async Task<Transaction> UpdateTransactionAsync(string personId, int transactionId, AddTransactionModel addTransactionModel)
        {
            var strategy = appDdContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var dbContextTransaction = await appDdContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);
                try
                {
                    var transaction = await appDdContext.Transactions
                                        .Include(t => t.TransactionType)
                                        .Include(t => t.Bank)
                                        .Include(t => t.Location)
                                        .Include(t => t.Account)
                                        .Include(t => t.Category)
                                        .Where(t => t.Account!.PersonId == personId)

                                        .SingleOrDefaultAsync(t => t.Id == transactionId);
                    if (transaction is null)
                        throw new Exception($"Transaction id {transactionId} was not found!");

                    transaction.Date = addTransactionModel.Date;
                    transaction.Amount = addTransactionModel.Amount;

                    if (addTransactionModel.Amount <= 0.00)
                        throw new Exception($"Transaction amount can't be zero or negative!");

                    if(transaction.TransactionType!.Id != addTransactionModel.TypeId)
                    {
                        var transactionType = await transactionTypesService.Get(addTransactionModel.TypeId);
                        if (transactionType is null)
                            throw new Exception($"Transaction type with id {addTransactionModel.BankId} was not found!");

                        transaction.TransactionType = transactionType;
                    }

                    if(transaction.Bank!.Id != addTransactionModel.BankId)
                    {
                        var bank = await banksService.Get(addTransactionModel.BankId);
                        if (bank is null)
                            throw new Exception($"Bank with id {addTransactionModel.BankId} was not found!");

                        transaction.Bank = bank;
                    }

                    if(transaction.Location!.Id != addTransactionModel.LocationId)
                    {
                        var location = await locationsService.Get(addTransactionModel.LocationId);
                        if (location is null)
                            throw new Exception($"Location with id {addTransactionModel.LocationId} was not found!");

                        transaction.Location = location;
                    }

                    if(transaction.Account!.AccountNumber != addTransactionModel.AccountNumber)
                    {
                        var account = await accountsService.GetRecord(addTransactionModel.AccountNumber, personId);
                        if (account is null)
                            throw new Exception($"Account with number {addTransactionModel.AccountNumber} was not found!");

                        transaction.Account = account;
                    }

                    if(transaction.Category!.Id != addTransactionModel.CategoryId)
                    {
                        var category = await categoriesService.Get(addTransactionModel.CategoryId);
                        if (category is null)
                            throw new Exception($"Category with id '{addTransactionModel.CategoryId}' was not found!");

                        transaction.Category = category;
                    }

                    await appDdContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    return transaction;
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    this.logger.LogError(ex.Message);

                    throw;
                }
            });
        }

        public async Task DeleteTransaction(string personId, int transactionId)
        {
            var strategy = appDdContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var dbContextTransaction = await appDdContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);
                try
                {
                    var transaction = await appDdContext.Transactions
                                        .Include(t => t.TransactionType)
                                        .Include(t => t.Bank)
                                        .Include(t => t.Location)
                                        .Include(t => t.Account)
                                        .Include(t => t.Category)
                                        .Where(t => t.Account!.PersonId == personId)

                                        .SingleOrDefaultAsync(t => t.Id == transactionId);
                    if (transaction is null)
                        throw new Exception($"Transaction id {transactionId} was not found!");

                    appDdContext.Transactions.Remove(transaction);

                    await appDdContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    this.logger.LogError(ex.Message);

                    throw;
                }
            });
        }

        private static TransactionOutputModel? FromModel(Transaction? transaction)
        {
            if (transaction is null)
                return null;

            return new TransactionOutputModel
            {
                TransactionId = transaction.Id,
                Date = transaction.Date,
                Amount = transaction.Amount,
                TransactionType = transaction.TransactionType,
                Bank = transaction.Bank,
                Location = transaction.Location,
                AccountNumber = transaction.Account!.AccountNumber,
                Category = transaction.Category
            };
        }
    }
}
