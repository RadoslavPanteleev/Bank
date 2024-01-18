namespace BankServer.Services
{
    public class ProfileService
    {
        private readonly AppDbContext appDbContext;
        private readonly ILogger logger;

        public ProfileService(AppDbContext appDbContext, ILogger logger)
        {
            this.appDbContext = appDbContext;
            this.logger = logger;
        }

        public async Task GetProfileInfo()
        {

        }
    }
}
