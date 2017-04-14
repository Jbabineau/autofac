using Homesite.Services.AddressBroker.Model.DTO;
using Homesite.Services.AddressBroker.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Homesite.Services.AddressBroker.API.Controllers
{
    public class ZipcodeController : ApiController
    {
        IZipcodeService _zipcodeService;

        public ZipcodeController(IZipcodeService zipcodeService)
        {
            this._zipcodeService = zipcodeService;
        }

        [HttpPost]
        public AddressResult Lookup(ZipcodeRequest request)
        {
            return _zipcodeService.Lookup(request);
        }
    }
}
