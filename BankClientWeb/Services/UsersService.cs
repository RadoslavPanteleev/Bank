using Newtonsoft.Json;

namespace BankClientWeb.Services
{
    public class UsersService
    {
        public UsersService()
        {

        }

        public async Task<bool> IsUserNameExistsAsync(string userName)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(Globals.API_BASE_URL + "Users/" + userName);
                request.Method = HttpMethod.Get;
                //request.Headers.Add("SecureApiKey", "12345");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBool = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                    return responseBool;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
