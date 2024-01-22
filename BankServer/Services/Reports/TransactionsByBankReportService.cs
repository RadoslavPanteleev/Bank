using BankServer.Models.Reports;
using BankServer.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace BankServer.Services.Reports
{
    public class TransactionsByBankReportService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public TransactionsByBankReportService(AppDbContext appDbContext, IConfiguration configuration) 
        { 
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<IList<TransactionsByBankNameModel>> GetReportByBankName(string ?bankName, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await (from t in appDbContext.Transactions
                         join ac in appDbContext.Accounts on t.AccountId equals ac.AccountNumber
                         join p in appDbContext.Peoples on ac.PersonId equals p.Id where p.Id.Equals(personId)
                         join b in appDbContext.Banks on t.BankId equals b.Id where b.Name!.Contains(bankName ?? b.Name!)

                         select new TransactionsByBankNameModel
                         {
                             Id = t.Id,
                             Date = t.Date,
                             Amount = t.Amount,
                             BankName = b.Name,
                             BankPhone = b.Phone,
                             BankAddress = b.Address,
                             AccountNumber = ac.AccountNumber,
                             AccountName = ac.AccountName
                         }
                         ).ToListAsync();

            IOrderedEnumerable<TransactionsByBankNameModel> orderedResult;
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

        public async Task<string> GetPDFReportByBankName(string? bankName, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await GetReportByBankName(bankName, personId, sortBy, isDescending);

            var pdfFileName = string.Format("GetPDFReportByBankName_{0}_{1}.pdf", personId, DateTime.Now.ToString("dd_MM_HH_mm_ss"));
            var pdfFileNameWithDirectory = Path.Combine(configuration["TempDirectory"], pdfFileName);

            await ExportReportHelper<TransactionsByBankNameModel>.ExportToPdfAsync(result, pdfFileNameWithDirectory);

            return pdfFileNameWithDirectory;
        }

        public async Task<string> GetExcelReportByBankName(string? bankName, string personId, string sortBy = "date", bool isDescending = false)
        {
            var result = await GetReportByBankName(bankName, personId, sortBy, isDescending);

            var excelFileName = string.Format("GetExcelReportByBankName_{0}_{1}.xlsx", personId, DateTime.Now.ToString("dd_MM_HH_mm_ss"));
            var excelFileNameWithDirectory = Path.Combine(configuration["TempDirectory"], excelFileName);

            await ExportReportHelper<TransactionsByBankNameModel>.ExportToExcelAsync(result, excelFileNameWithDirectory);

            return excelFileNameWithDirectory;
        }
    }
}
