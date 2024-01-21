using BankServer.Models.Reports;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace BankServer.Services.Reports
{
    public class TransactionsByLocationReportService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public TransactionsByLocationReportService(AppDbContext appDbContext, IConfiguration configuration) 
        { 
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<IList<TransactionsByLocationNameModel>> GetReportByLocationName(string ?locationName, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await (from t in appDbContext.Transactions
                         join ac in appDbContext.Accounts on t.AccountId equals ac.AccountNumber
                         join p in appDbContext.Peoples on ac.PersonId equals p.Id where p.Id.Equals(personId)
                         join l in appDbContext.Locations on t.LocationId equals l.Id where l.Name!.Contains(locationName ?? l.Name!)
                         join la in appDbContext.Addresses on l.AddressId equals la.Id
                         join tt in appDbContext.TransactionsTypes on t.TransactionTypeId equals tt.Id
                         join b in appDbContext.Banks on t.BankId equals b.Id
                         join c in appDbContext.Categories on t.CategoryId equals c.Id

                         select new TransactionsByLocationNameModel
                         {
                             Id = t.Id,
                             Date = t.Date,
                             Amount = t.Amount,
                             Type = tt.Type,
                             BankName = b.Name,
                             LocationName = l.Name,
                             LocationAddress = la.Description,
                             AccountNumber = ac.AccountNumber,
                             AccountName = ac.AccountName,
                             CategoryName = c.Name
                         }
                         ).ToListAsync();

            IOrderedEnumerable<TransactionsByLocationNameModel> orderedResult;
            if(sortBy.ToLower().Equals("amount"))
            {
                if (!isDescending)
                    orderedResult = result.OrderBy(t => t.Amount);
                else
                    orderedResult = result.OrderByDescending(t => t.Amount);
            }
            else if(sortBy.ToLower().Equals("dateamount"))
            {
                if (!isDescending)
                    orderedResult = result.OrderBy(t => t.Date).ThenBy(t => t.Amount);
                else
                    orderedResult = result.OrderByDescending(t => t.Date).ThenByDescending(t => t.Amount);
            }
            else
            {
                if (!isDescending)
                    orderedResult = result.OrderBy(t => t.Date);
                else
                    orderedResult = result.OrderByDescending(t => t.Date);
            }

            return orderedResult.ToList();
        }

        public async Task<string> GetPDFReportByLocationName(string? locationName, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await GetReportByLocationName(locationName, personId, sortBy, isDescending);

            var pdfFileName = string.Format("GetPDFReportByLocationName_{0}_{1}.pdf", personId, DateTime.Now.ToString("dd_MM_HH_mm_ss"));
            var pdfFileNameWithDirectory = Path.Combine(configuration["TempDirectory"], pdfFileName);

            await ExportReportHelper<TransactionsByLocationNameModel>.ExportToPdfAsync(result, pdfFileNameWithDirectory);

            return pdfFileNameWithDirectory;
        }

        public async Task<string> GetExcelReportByLocationName(string? locationName, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await GetReportByLocationName(locationName, personId, sortBy, isDescending);

            var excelFileName = string.Format("GetExcelReportByLocationName_{0}_{1}.xlsx", personId, DateTime.Now.ToString("dd_MM_HH_mm_ss"));
            var excelFileNameWithDirectory = Path.Combine(configuration["TempDirectory"], excelFileName);

            await ExportReportHelper<TransactionsByLocationNameModel>.ExportToExcelAsync(result, excelFileNameWithDirectory);

            return excelFileNameWithDirectory;
        }
    }
}
