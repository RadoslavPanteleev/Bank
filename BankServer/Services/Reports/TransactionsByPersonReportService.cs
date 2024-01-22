using BankServer.Models.Reports;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services.Reports
{
    public class TransactionsByPersonReportService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public TransactionsByPersonReportService(AppDbContext appDbContext, IConfiguration configuration) 
        { 
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<IList<TransactionsByPersonNameModel>> GetReportByPersonName(string ?personUserName, string? personFirstName, string? personLastName, string ?personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await (from t in appDbContext.Transactions
                         join ac in appDbContext.Accounts on t.AccountId equals ac.AccountNumber
                         join p in appDbContext.Peoples on ac.PersonId equals p.Id
                                where
                                   (personId == null || p.Id.Contains(personId)) &&
                                   (personUserName == null || p.UserName!.Contains(personUserName)) &&
                                   (personFirstName == null || p.FirstName!.Contains(personFirstName)) &&
                                   (personLastName == null || p.LastName!.Contains(personLastName))

                                select new TransactionsByPersonNameModel
                         {
                             Id = t.Id,
                             Date = t.Date,
                             Amount = t.Amount,
                             AccountNumber = ac.AccountNumber,
                             AccountName = ac.AccountName,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             Phone = p.Phone,
                         }
                         ).ToListAsync();

            IOrderedEnumerable<TransactionsByPersonNameModel> orderedResult;
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

        public async Task<string> GetPDFReportByPersonName(string? personUserName, string? personFirstName, string? personLastName, string? personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await GetReportByPersonName(personUserName, personFirstName, personLastName, personId, sortBy, isDescending);

            var pdfFileName = string.Format("GetExcelReportByPersonName_{0}_{1}.pdf", personId, DateTime.Now.ToString("dd_MM_HH_mm_ss"));
            var pdfFileNameWithDirectory = Path.Combine(configuration["TempDirectory"], pdfFileName);

            await ExportReportHelper<TransactionsByPersonNameModel>.ExportToPdfAsync(result, pdfFileNameWithDirectory);

            return pdfFileNameWithDirectory;
        }

        public async Task<string> GetExcelReportByPersonName(string? personUserName, string? personFirstName, string? personLastName, string? personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await GetReportByPersonName(personUserName, personFirstName, personLastName, personId, sortBy, isDescending);

            var excelFileName = string.Format("GetExcelReportByPersonName_{0}_{1}.xlsx", personId, DateTime.Now.ToString("dd_MM_HH_mm_ss"));
            var excelFileNameWithDirectory = Path.Combine(configuration["TempDirectory"], excelFileName);

            await ExportReportHelper<TransactionsByPersonNameModel>.ExportToExcelAsync(result, excelFileNameWithDirectory);

            return excelFileNameWithDirectory;
        }
    }
}
