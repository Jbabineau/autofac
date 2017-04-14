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
    public class AddressController : ApiController
    {
        IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            this._addressService = addressService;
        }

        [HttpPost]
        public AddressResult Lookup(AddressRequest request)
        {
            return _addressService.Lookup(request);
        }
    }
}
