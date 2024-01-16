using Microsoft.EntityFrameworkCore;

namespace BankServer.Services
{
    public class UsersService
    {
        private readonly BankContext bankContext;

        public UsersService(BankContext bankContext)
        {
            this.bankContext = bankContext;
        }

        public async Task<bool> IsUserNameExists(string userName)
        {
            var result = await bankContext.Peoples.FirstOrDefaultAsync(x => x.UserName.CompareTo(userName) == 1);
            return result != null;
        }
    }
}
