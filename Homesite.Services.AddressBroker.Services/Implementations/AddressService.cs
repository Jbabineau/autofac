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
    /// 
    /// </summary>
    public class AddressService : IAddressService
    {
        private ABClient client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public AddressService(ABClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AddressResult Lookup(AddressRequest request)
        {
            StringBuilder addressLine = new StringBuilder();
            addressLine.Append(request.AddressLine1);
            if(!string.IsNullOrEmpty(request.AddressLine2))
            {
                addressLine.Append(string.Format(" {0}", request.AddressLine2));
            }

            StringBuilder lastLine = new StringBuilder();
            lastLine.Append(string.Format("{0}, {1} {2}", request.City, request.State, request.Zipcode));
            if(!string.IsNullOrEmpty(request.ZipcodeExtension))
            {
                lastLine.Append(string.Format("-{0}", request.ZipcodeExtension));
            }

            client.SetField(ABClientField.AddressLine.Value, addressLine.ToString());
            client.SetField(ABClientField.LastLine.Value, lastLine.ToString());
            client.SetRecord();

            client.ProcessRecords();
            ParseResponse(request.State);
            //set legacy return code
            return new AddressResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AddressResult ParseResponse(string requestStateCode)
        {
            AddressResult result = new AddressResult();
            if(client.GetRecord())
            {
                result.LocationQualityCode = client.GetField(ABClientField.LocationQualityCode.Value);
                result.MatchCode = client.GetField(ABClientField.MatchCode.Value);
                result.City = client.GetField(ABClientField.City.Value);
                result.State = client.GetField(ABClientField.State.Value);
                result.Zip = client.GetField(ABClientField.Zipcode.Value);
                result.ErrorOccured = result.LocationQualityCode.Equals("E");

                if(result.ErrorOccured)
                {
                    string errorMessage;
                    if(result.MatchCode.Equals("E020"))
                    {
                        errorMessage = "MatchCode E020: No matching street.";
                    }
                    else
                    {
                        errorMessage = string.Format("MatchCode: {0} LocationQuality: {1}", result.MatchCode, result.LocationQualityCode);
                    }

                    result.ErrorMessage = errorMessage;
                }

                // Parse Latitude and Longitude
                decimal latitude, longitude;
                if(decimal.TryParse(client.GetField(ABClientField.Latitude.Value), out latitude) && 
                   decimal.TryParse(client.GetField(ABClientField.Longitude.Value), out longitude))
                {
                    result.Latitude = latitude;
                    result.Longitude = longitude;

                    result.AddressLine = client.GetField(ABClientField.AddressLineOutput.Value);
                    result.CensusBlock = client.GetField(ABClientField.CensusBlockId.Value);
                    result.DeliveryPointBarCode = client.GetField(ABClientField.DeliveryPointBarCode.Value);
                    int checkDigit;
                    if(int.TryParse(client.GetField(ABClientField.CheckDigit.Value), out checkDigit))
                    {
                        result.CheckDigit = checkDigit;
                    }

                    result.FipsCode = client.GetField(ABClientField.FIPSCountyCode.Value);
                    result.County = client.GetField(ABClientField.CountyName.Value).Replace("County", string.Empty);
                    result.HouseNumber = client.GetField(ABClientField.HouseNumber.Value);
                    result.PrefixDirection = result.StreetDirectionPre = client.GetField(ABClientField.PrefixDirection.Value);
                    result.PostfixDirection = result.StreetDirectionPost = client.GetField(ABClientField.PostfixDirection.Value);
                    result.StreetName = client.GetField(ABClientField.StreetName.Value);
                    result.StreetType = client.GetField(ABClientField.StreetType.Value);
                    result.UnitType = client.GetField(ABClientField.UnitType.Value);
                    result.UnitNumber = client.GetField(ABClientField.UnitNumber.Value);
                    result.ZipClass = client.GetField(ABClientField.ZipClass.Value);
                    result.Zip4 = client.GetField(ABClientField.ZipExtension.Value);

                    if(requestStateCode.Equals(result.State) /* StateMapList != null */)
                    {

                    }
                    else if(!requestStateCode.Equals(result.State))
                    {
                        result.ErrorMessage = "Could not get state based information for this query because the provided state does not match the zip code. Maps for the wrong state were loaded during initialization.";
                    }
                    else
                    {
                        result.ErrorMessage = "Their was either and error or no match for this address. See the MatchCode for more details.";
                    }
                }
            }

            if(!result.ErrorOccured)
            {
                if(result.LocationQualityCode.ToLower()[0].Equals("z") &&
                   !result.LocationQualityCode.ToLower()[2].Equals("9"))
                {
                    result.ErrorMessage = "Skipped WildFire Calculation because of Location Quality Code.";
                    //LogError
                }
                // set protection class codes

            }
            return result;
        }
    }
}
