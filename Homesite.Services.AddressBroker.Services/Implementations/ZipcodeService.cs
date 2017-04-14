using Centrus.AddressBroker;
using Homesite.Services.AddressBroker.Model.DTO;
using Homesite.Services.AddressBroker.Services.Common;
using Homesite.Services.AddressBroker.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Services.Implementations
{
    /// <summary>
    /// The ZipcodeService.
    /// </summary>
    public class ZipcodeService : IZipcodeService
    {
        private ABClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipcodeService"/> class. 
        /// </summary>
        /// <param name="client">The ABClient.</param>
        public ZipcodeService(ABClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Does a lookup on the zipcode.
        /// </summary>
        /// <param name="request">The ZipcodeRequest.</param>
        /// <returns>The AddressResult with Address data.</returns>
        public AddressResult Lookup(ZipcodeRequest request)
        {
            AddressResult result = new AddressResult();

            client.SetField(ABClientField.LastLine.Value, request.Zipcode);
            client.SetRecord();
            client.ProcessRecords();
            result = ParseResponse();

            return result;
        }

        /// <summary>
        /// Parses the response of the client.
        /// </summary>
        /// <param name="client">The ABClient which made the lookup.</param>
        /// <returns>The AddressResult with address data.</returns>
        private AddressResult ParseResponse()
        {
            AddressResult result = new AddressResult();
            if(client.GetRecord())
            {
                result.LocationQualityCode = client.GetField(ABClientField.LocationQualityCode.Value);
                result.MatchCode = client.GetField(ABClientField.MatchCode.Value);
                result.City = client.GetField(ABClientField.City.Value);
                result.State = client.GetField(ABClientField.State.Value);
                result.Zip = client.GetField(ABClientField.Zipcode.Value);
            }

            result.ErrorOccured = string.IsNullOrEmpty(result.State);
            if(result.ErrorOccured)
            {
                result.ErrorMessage = "Couldn't find city and state for this zip code.";
            }
            return result;
        }
    }
}
