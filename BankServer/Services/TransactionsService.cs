namespace BankServer.Services
{
    public class TransactionsService
    {
        private readonly AppDbContext bankContext;
        private readonly ILogger logger;

        public TransactionsService(AppDbContext bankContext, ILogger logger)
        {
            this.bankContext = bankContext;
            this.logger = logger;
        }


    }
}
