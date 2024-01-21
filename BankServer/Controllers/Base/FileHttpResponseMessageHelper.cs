using System.Net;
using System.Net.Http.Headers;

namespace BankServer.Controllers.Base
{
    public class FileHttpResponseMessageHelper
    {
        public static async Task<HttpResponseMessage> GetMessageWithFileContent(string filePath)
        {
            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));

            var httpResult = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = fileContent
            };
            httpResult.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(filePath)
                };
            httpResult.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return httpResult;
        }
    }
}
