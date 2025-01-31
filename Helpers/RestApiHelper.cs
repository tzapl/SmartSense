using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SmartSense.API.ServiceModule.Helpers
{
    public static class RestApiHelper
    {
        public static void DeleteToken(string technicianId, string raToken, string raAddress)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", raToken);

                var response = client.DeleteAsync($"{raAddress}tokens/{technicianId}").Result;

                if (!response.IsSuccessStatusCode || (int)response.StatusCode != (int)ResponseStatus.ApiResponseOK)
                {
                    throw new Exception("Failed to delete token");
                }
            }
        }

        public static string GetUserTokenAsync(string handle, string raToken, string raAddress)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", raToken);
                var jsonContent = "{\"handle\":\"" + handle + "\"}";
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{raAddress}tokens", content).Result;

                if (!response.IsSuccessStatusCode || (int)response.StatusCode != (int)ResponseStatus.ApiResponseOK)
                {
                    throw new Exception("Failed to create token");
                }

                
                string responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic obj = JObject.Parse(responseContent);
                return obj?.data?.items?[0].accessToken ?? String.Empty ;
            }
        }
    }
}
