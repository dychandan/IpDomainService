using System;
using IpDomainInfoService.Business;
using IpDomainInfoService.Models;
using Microsoft.AspNetCore.Mvc;
using static IpDomainInfoService.Constants;

namespace IpDomainInfoService.Controllers
{
    public class IPDomainInfoController : Controller
    {
        [HttpGet]
        public ActionResult GetIPDomainDetails(Request request)
        {
            try
            {
                var errorResponse = ValidateRequest(request);
                if (!string.IsNullOrEmpty(errorResponse))
                {
                    return BadRequest(errorResponse);
                }
                else
                {
                    var iPDomainImplementation = new IpDomainBusiness();
                    return Ok(iPDomainImplementation.GetIPDomainDetails(request));
                }
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        private string ValidateRequest(Request request)
        {
            if (request == null)
            {
                return InvalidRequest;
            }
            if (!String.IsNullOrEmpty(request.IpAddress) && !request.IpAddress.IsValidIP())
            {
                return InvalidIP;
            }
            if (!String.IsNullOrEmpty(request.Domain) && !request.Domain.IsValidDomainName())
            {
                return InvalidDomain;
            }
            return string.Empty;
        }
    }
}