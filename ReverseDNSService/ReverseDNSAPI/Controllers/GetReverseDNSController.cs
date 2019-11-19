using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReverseDNSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseDNSController : ControllerBase
    {
        [HttpGet]
        public string GetReverseDNSDetails(string IPAddress = "", string Domain = "")
        {
            string Path = "https://api.viewdns.info/reversedns/?ip=#IPAddress&apikey=52849ee9c669ed4335c333addfcbda112f9152f2&output=json";
            string URLPath = Path.Replace("#IPAddress", IPAddress);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(URLPath).Result;

                string responseString;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result.ToString();
                }
                else
                {
                    responseString = "Error occured while communicating with Reverse DNS service";
                }
                return responseString;
            }
        }
    }
}