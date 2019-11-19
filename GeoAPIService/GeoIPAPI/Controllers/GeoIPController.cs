using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoIPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoIPController : ControllerBase
    {
        [HttpGet]
        public string GetGeoIP(string IPAddress = "", string Domain = "")
        {
            string Path = "http://api.ipstack.com/#IPAddress?access_key=04d5137a6a330512341a47b355995cf6";

            string responsestring = string.Empty;
            Path = Path.Replace("#IPAddress", IPAddress);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(Path).Result;
                if (response.IsSuccessStatusCode)
                {
                    responsestring = response.Content.ReadAsStringAsync().Result.ToString();
                }
                else
                {
                    responsestring = "Error occured while communicating with GeoIP service";
                }
                return responsestring;
            }
        }
    }
}