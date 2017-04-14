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
    public class ABClientService : IABClientService
    {
        AddressBrokerFactory factory;

        public ABClientService(AddressBrokerFactory factory)
        {
            this.factory = factory;
        }

        ABClient Build(AddressConfiguration configuration)
        {
            ABClient client = AddressBrokerFactory.Make(string.Format("{0}:{1}", configuration.ServerName, configuration.Port), ABClientField.Socket.Value);


            client.SetProperty(ABClientField.KeepMultiMatch.Value, configuration.KeepMultiMatch);
            client.SetProperty(ABClientField.OffsetDistance.Value, configuration.OffSetDistance);
            client.SetProperty(ABClientField.MatchMode.Value, configuration.MatchMode);
            client.SetProperty(ABClientField.InputMode.Value, configuration.InputMode);
            client.SetProperty(ABClientField.Datum.Value, configuration.Datum);
            client.SetProperty(ABClientField.CentroidPreference.Value, configuration.CentroidPreference);
            client.SetProperty(ABClientField.MixedCase.Value, configuration.MixedCase);
            client.SetProperty(ABClientField.CoordinateType.Value, configuration.CoordinateType);
            client.SetProperty(ABClientField.InitList.Value, configuration.InitList);
            client.SetProperty(ABClientField.InputFieldList.Value, configuration.InputFieldList);
            client.SetProperty(ABClientField.OutputFieldList.Value, configuration.OutputFieldStringList);

            // TODO: Add the other fields to the client for Address call if they are present.
            return client;
        }

        ABClient IABClientService.Build(AddressConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
