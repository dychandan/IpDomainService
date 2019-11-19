using IpDomainInfoService.Controllers;
using IpDomainInfoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using static IpDomainInfoService.Constants;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        readonly IPDomainInfoController _controller = new IPDomainInfoController();

        [TestMethod]
        public void Get_WhenCalled_ReturnsInvalidRequest()
        {
            Request request = null;

            var output = _controller.GetIPDomainDetails(request);
            var expected = new BadRequestObjectResult(InvalidRequest);
            Assert.AreEqual(
            JsonConvert.SerializeObject(expected),
            JsonConvert.SerializeObject(output));
        }

        [TestMethod]
        public void Get_WhenCalled_ReturnsOkRequest()
        {

            Request request = new Request();
            request.IpAddress = "121.244.55.130";
            request.Domain = "Indecomm.net";
            request.ServiceList = new System.Collections.Generic.List<string>();
            request.ServiceList.Add("Test");
            request.ServiceList.Add("GeoIP");
            request.ServiceList.Add("ReverseDNS");

            var req = JsonConvert.SerializeObject(request);
            var output = _controller.GetIPDomainDetails(request);
            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected["Test"] = "Provided service Test is not supported by application";
            expected["GeoIP"] = "{\"ip\":\"121.244.55.130\",\"type\":\"ipv4\",\"continent_code\":\"AS\",\"continent_name\":\"Asia\",\"country_code\":\"IN\",\"country_name\":\"India\",\"region_code\":\"KA\",\"region_name\":\"Karnataka\",\"city\":\"Bengaluru\",\"zip\":\"560007\",\"latitude\":12.964400291442871,\"longitude\":77.62110137939453,\"location\":{\"geoname_id\":1277333,\"capital\":\"New Delhi\",\"languages\":[{\"code\":\"hi\",\"name\":\"Hindi\",\"native\":\"\\u0939\\u093f\\u0928\\u094d\\u0926\\u0940\"},{\"code\":\"en\",\"name\":\"English\",\"native\":\"English\"}],\"country_flag\":\"http:\\/\\/assets.ipstack.com\\/flags\\/in.svg\",\"country_flag_emoji\":\"\\ud83c\\uddee\\ud83c\\uddf3\",\"country_flag_emoji_unicode\":\"U+1F1EE U+1F1F3\",\"calling_code\":\"91\",\"is_eu\":false}}";
            expected["ReverseDNS"] = "{\"query\" : {\"tool\" : \"reversedns_PRO\",\"ip\" : \"121.244.55.130\"},\"response\" : {\"rdns\" : \"121.244.55.130.STATIC-Bangalore.vsnl.net.in.\"}}";
            var expectedresult = new OkObjectResult(expected);
            Assert.AreEqual(
             JsonConvert.SerializeObject(expectedresult),
             JsonConvert.SerializeObject(output));
        }
    }
}
