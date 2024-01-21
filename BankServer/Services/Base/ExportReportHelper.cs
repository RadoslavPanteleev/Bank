using iText.Kernel.Pdf;
using iText.Layout;
using OfficeOpenXml;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BankServer.Services.Base
{
    public class ExportReportHelper<TExportModel>
        where TExportModel : class
    {
        public static async Task ExportToPdfAsync(IList<TExportModel> data, string pdfFilePath)
        {
            await Task.Run(() =>
            {
                using (var pdfWriter = new PdfWriter(pdfFilePath))
                {
                    using (var pdfDocument = new PdfDocument(pdfWriter))
                    {
                        var document = new Document(pdfDocument);
                        AddTableToDocument(document, data);
                    }
                }
            });
        }

        private static void AddTableToDocument(Document document, IList<TExportModel> reportData)
        {
            var columnNames = GetColumnsNamesFromModelType();
            var table = new iText.Layout.Element.Table(columnNames.Count(), true);

            foreach (var column in columnNames)
            {
                // Add headers to the table
                table.AddHeaderCell(column);
            }

            foreach (var exportModel in reportData)
            {
                var values = GetValuesFromModel(exportModel);
                foreach (var value in values)
                    table.AddCell(value);
            }

            document.Add(table);
        }

        public static async Task ExportToExcelAsync(IList<TExportModel> reportData, string excelFilePath)
        {
            await Task.Run(() =>
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var excelPackage = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Report");
                    AddTableToWorksheet(worksheet, reportData);
                    excelPackage.Save();
                }
            });
        }

        private static void AddTableToWorksheet(ExcelWorksheet worksheet, IList<TExportModel> reportData)
        {
            var columnNames = GetColumnsNamesFromModelType();
            for (int col = 0; col < columnNames.Count; col++)
            {
                worksheet.Cells[1, col + 1].Value = columnNames[col];
            }

            int row = 2;
            foreach (var exportModel in reportData)
            {
                var values = GetValuesFromModel(exportModel);
                for (int col = 0; col < values.Count; col++)
                {
                    worksheet.Cells[row, col + 1].Value = values[col];
                }

                row++;
            }
        }

        private static IList<string> GetColumnsNamesFromModelType()
        {
            List<string> results = new List<string>();
            PropertyInfo[] propertyArray = typeof(TExportModel).GetProperties();

            foreach (var property in propertyArray)
            {
                var displayNameAttr = property.GetCustomAttribute<DisplayNameAttribute>();
                string displayName = displayNameAttr != null ? displayNameAttr.DisplayName : "";

                results.Add(displayName);
            }

            return results;
        }

        private static IList<string> GetValuesFromModel(TExportModel model)
        {
            List<string> results = new List<string>();

            PropertyInfo[] propertyArray = typeof(TExportModel).GetProperties();
            foreach (var property in propertyArray)
            {
                var displayFormatAttr = property.GetCustomAttribute<DisplayFormatAttribute>();
                string? displayFormat = displayFormatAttr != null ? displayFormatAttr.DataFormatString : "";
                string? nullDisplayFormat = displayFormatAttr != null ? displayFormatAttr.NullDisplayText : "";

                var value = property.GetValue(model);

                if (property.GetMethod != null && property.GetMethod.ReturnType == typeof(DateTime) && displayFormat != null)
                {
                    if (value != null)
                        value = ((DateTime)value).ToString(displayFormat);
                    else
                        value = nullDisplayFormat;
                }

                if (value is null)
                    value = string.Empty;

                results.Add(value.ToString()!);
            }

            return results;
        }

    }
}
