﻿using Homesite.Services.AddressBroker.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Services.Contracts
{
    public interface IZipcodeService
    {
        AddressResult Lookup(ZipcodeRequest request);
    }
}
