using BankServer.Services.Base;

namespace BankServer.Services.Reports
{
    public class TransactionsByLocationReportService : BaseReportService
    {
        private readonly AppDbContext appDbContext;

        public TransactionsByLocationReportService(AppDbContext appDbContext) 
        { 
            this.appDbContext = appDbContext;
        }

        public async Task GetReportByLocationName(string locationName, string personId = "")
        {
           //var reportQuery = (from t in appDbContext.Transactions
           //                   join l in appDbContext.Locations on t.LocationId equals l.Id where l.Name!.Equals(locationName)
           //                   join ad in appDbContext.Addresses on l.AddressId equals ad.Id
           //                   join a in appDbContext.Accounts on t.AccountId equals a.
           //
           //                   select new
           //                   {
           //                       LocationName = l.Name,
           //                       LocationAddress = a.Description,
           //                       LocationLatidude = l.Latitude, 
           //                       LocationLonidude = l.Longitude, 
           //                   }).ToList();
           //
           //
        }
    }
}
