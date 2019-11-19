using IpDomainInfoService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static IpDomainInfoService.Constants;
using static IpDomainInfoService.Models.StringModelExtensions;

namespace IpDomainInfoService.Business
{
    public class IpDomainBusiness
    {
        readonly string GeoIPURL = "https://localhost:44340/api/GeoIP?IpAddress=";
        readonly string ReverseDNSURL = "https://localhost:44397/api/ReverseDNS?IpAddress=";

        public Dictionary<string, string> GetIPDomainDetails(Request request)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            request.ServiceList.ForEach(/*request.ServiceList,*/ async (service) =>
            {
                string endpoint = GetServiceEndpoint(request, service);
                if (string.IsNullOrEmpty(endpoint))
                {
                    string Error = InvalidServicelist;
                    response[service] = string.Format(Error, service);
                }
                else
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        response[service] = GetAsync(httpClient, endpoint).Result.ToString();
                    }
                }
            });
            return response;
        }
        
        private string GetServiceEndpoint(Request request, string service)
        {
            string endpoint = string.Empty;
            if (Enum.TryParse<ServiceType>(service, out ServiceType serviceType))
            {
                switch (serviceType)
                {
                    case ServiceType.GeoIP:
                        endpoint = GeoIPURL + request.IpAddress;
                        break;
                    case ServiceType.ReverseDNS:
                        endpoint = ReverseDNSURL + request.IpAddress;
                        break;
                }
            }
            return endpoint;
        }

        private async Task<string> GetAsync(HttpClient httpClient, string uri)
        {
            return await await httpClient.GetAsync(uri).ContinueWith(ReadResponse());
        }

        private static Func<Task<HttpResponseMessage>, Task<string>> ReadResponse()
        {
            return async task =>
            {
                var response = await task;

                if (!response.IsSuccessStatusCode)
                {
                    throw new RestHttpResponseException(response.StatusCode, response.ReasonPhrase);
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            };
        }
    }
}
