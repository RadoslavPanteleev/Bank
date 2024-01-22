using BankServer.Models.Reports;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services.Reports
{
    public class TransactionsHistoryReportService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public TransactionsHistoryReportService(AppDbContext appDbContext, IConfiguration configuration) 
        { 
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<IList<TransactionsHistoryModel>> GetReportHistory(DateTime dateFrom, DateTime dateTo, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await (from t in appDbContext.Transactions
                         join ac in appDbContext.Accounts on t.AccountId equals ac.AccountNumber
                         join p in appDbContext.Peoples on ac.PersonId equals p.Id where p.Id.Equals(personId)
                         where t.Date >= dateFrom && t.Date <= dateTo

                         select new TransactionsHistoryModel
                         {
                            Id = t.Id,
                            Date = t.Date,
                            Amount = t.Amount,
                         }
                         
                         ).ToListAsync();

            IOrderedEnumerable<TransactionsHistoryModel> orderedResult;
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

        public async Task<string> GetPDFReportHistory(DateTime dateFrom, DateTime dateTo, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await GetReportHistory(dateFrom, dateTo, personId, sortBy, isDescending);

            var pdfFileName = string.Format("GGetPDFReportHistory_{0}_{1}.pdf", personId, DateTime.Now.ToString("dd_MM_HH_mm_ss"));
            var pdfFileNameWithDirectory = Path.Combine(configuration["TempDirectory"], pdfFileName);

            await ExportReportHelper<TransactionsHistoryModel>.ExportToPdfAsync(result, pdfFileNameWithDirectory);

            return pdfFileNameWithDirectory;
        }

        public async Task<string> GetExcelReportHistory(DateTime dateFrom, DateTime dateTo, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await GetReportHistory(dateFrom, dateTo, personId, sortBy, isDescending);

            var excelFileName = string.Format("GetExcelReportHistory_{0}_{1}.xlsx", personId, DateTime.Now.ToString("dd_MM_HH_mm_ss"));
            var excelFileNameWithDirectory = Path.Combine(configuration["TempDirectory"], excelFileName);

            await ExportReportHelper<TransactionsHistoryModel>.ExportToExcelAsync(result, excelFileNameWithDirectory);

            return excelFileNameWithDirectory;
        }
    }
}
