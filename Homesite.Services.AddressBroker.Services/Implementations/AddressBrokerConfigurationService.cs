using Homesite.Services.AddressBroker.DAL.Contracts;
using Homesite.Services.AddressBroker.Model.DTO;
using Homesite.Services.AddressBroker.Repository;
using Homesite.Services.AddressBroker.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Services.Implementations
{
    public class AddressBrokerConfigurationService : IAddressBrokerConfigurationService
    {
        IUnitOfWork unitOfWork;
        ILookupTypeRepository lookupTypeRepository;
        IMapFormatRepository mapFormatRepository;
        IMapOutputFieldsRepository mapOutputFieldsRepository;
        IMapRepository mapRepository;
        IStateMapRepository stateMapRepository;
        IStateRespository stateRepository;
        IOutputRepository outputRepository;

        public AddressBrokerConfigurationService(IUnitOfWork unitOfWork,
                               ILookupTypeRepository lookupTypeRepository,
                               IMapFormatRepository mapFormatRepository,
                               IMapOutputFieldsRepository mapOutputFieldsRepository,
                               IMapRepository mapRepository,
                               IStateMapRepository stateMapRepository,
                               IStateRespository stateRepository,
                               IOutputRepository outputRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lookupTypeRepository = lookupTypeRepository;
            this.mapFormatRepository = mapFormatRepository;
            this.mapOutputFieldsRepository = mapOutputFieldsRepository;
            this.mapRepository = mapRepository;
            this.stateRepository = stateRepository;
            this.stateMapRepository = stateMapRepository;
            this.outputRepository = outputRepository;
        }

        public AddressConfiguration GetConfiguration()
        {
            AddressConfiguration configuration = new AddressConfiguration();
            configuration.MapList = BuildMapList();
            configuration.ServerName = GetServerName();
            configuration.Port = GetPort();
            configuration.KeepMultiMatch = GetKeepMultiMatch();
            configuration.OffSetDistance = GetOffsetDistance();
            configuration.MatchMode = GetMatchMode();
            configuration.InputMode = GetInputMode();
            configuration.Datum = GetDatum();
            configuration.CentroidPreference = GetCentroidPreference();
            configuration.MixedCase = GetMixedCase();
            configuration.CoordinateType = GetCoordinateType();
            configuration.InitList = GetInitList();
            configuration.InputFieldList = GetInputFieldList(string.Empty);
            configuration.OutputFieldStringList = GetOutputFieldList();
            return configuration;
        }

        public AddressConfiguration GetConfigurationByState(string stateCode)
        {
            AddressConfiguration configuration = new AddressConfiguration();

            configuration.MapList = BuildMapListByState(stateCode);
            configuration.InitList = BuildInitListByState(configuration.MapList);
            configuration.OutputFieldStringList = BuildOuputListByState(configuration.MapList);

            List<Map> mapList = configuration.MapList.Values.ToList();
            configuration.LoadedProtectionClassMap = mapList.Where(m => m.Name.Contains("ProtClass")).Count() > 0 ? true : false;
            configuration.LoadedMunicipalityMap = mapList.Where(m => m.Name.Contains("Municipality")).Count() > 0 ? true : false;
            configuration.LoadedFireDistrictMap = mapList.Where(m => m.Name.Contains("FireDistrict")).Count() > 0 ? true : false;
            
            // Not a Jimmy babs comment
            //// figure out if we need to do the genx wildfire simulation hack. 
            configuration.LoadedBrushFireComplete = mapList.Where(m => m.Name.Contains("BrushFireComplete")).Count() > 0 ? true : false;
            configuration.LoadedBrushFireHighRisk = mapList.Where(m => m.Name.Contains("BrushFireHighRisk")).Count() > 0 ? true : false;
            configuration.LoadedFireBreakComplete = mapList.Where(m => m.Name.Contains("FireBreakComplete")).Count() > 0 ? true : false;
            configuration.LoadedFireBreakWildland = mapList.Where(m => m.Name.Contains("FireBreakWildland")).Count() > 0 ? true : false;

            configuration.CalculateWildfireScore = configuration.LoadedBrushFireComplete && configuration.LoadedBrushFireHighRisk && configuration.LoadedFireBreakComplete && configuration.LoadedFireBreakWildland;

            configuration.ServerName = GetServerName();
            configuration.Port = GetPort();
            configuration.KeepMultiMatch = GetKeepMultiMatch();
            configuration.OffSetDistance = GetOffsetDistance();
            configuration.MatchMode = GetMatchMode();
            configuration.InputMode = GetInputMode();
            configuration.Datum = GetDatum();
            configuration.CentroidPreference = GetCentroidPreference();
            configuration.MixedCase = GetMixedCase();
            configuration.CoordinateType = GetCoordinateType();
            configuration.InitList = BuildInitListByState(configuration.MapList);
            configuration.InputFieldList = GetInputFieldList(stateCode);
            configuration.OutputFieldStringList = BuildOuputListByState(configuration.MapList);

            string bufferTable = BuildBufferTableByState(configuration.MapList, stateCode);
            configuration.BufferTable = string.IsNullOrEmpty(bufferTable) ? string.Empty : bufferTable;
            return configuration;
        }

        private Dictionary<string, Map> BuildMapListByState(string stateCode)
        {
            Dictionary<string, Map> mapList = new Dictionary<string, Map>();
            // Setting up the Map list for the state
            Map map;
            foreach (var stateMap in stateMapRepository.GetAll().Where(m => m.JB_States.StateCode == stateCode.ToUpper()))
            {
                map = new Map();
                map.OutputFields = new List<string>();
                foreach (var blahMap in mapRepository.GetAll().Where(m => m.Name.Equals(stateMap.JB_Maps.Name)))
                {
                    foreach (var output in blahMap.JB_Maps_OutputFields)
                    {
                        map.OutputFields.Add(output.JB_OutputField.Value);
                    }
                }
                map.Name = stateMap.JB_Maps.Name;
                map.LookupType = stateMap.JB_Maps.JB_LookupType.Name;
                map.Format = string.Format(stateMap.JB_Maps.JB_MapFormat.Format, stateCode.ToUpper());
                mapList.Add(map.Name, map);
            }
            return mapList;
        }

        private string BuildInitListByState(Dictionary<string, Map> mapList)
        {
            StringBuilder initList = new StringBuilder();

            foreach (var map in mapList)
            {
                initList.Append("|");
                initList.Append(map.Value.Format);
            }

            return initList.ToString().Substring(1);
        }

        private string BuildOuputListByState(Dictionary<string, Map> mapList)
        {
            StringBuilder outputList = new StringBuilder();

            foreach (var map in mapList)
            {
                foreach (var output in map.Value.OutputFields)
                {
                    outputList.Append("|");
                    outputList.Append(output);

                    outputList.Append("[");
                    outputList.Append(map.Value.Format);
                    outputList.Append("]");
                }
            }

            return outputList.ToString().Substring(1);

        }

        private string BuildBufferTableByState(Dictionary<string, Map> mapList, string stateCode)
        {
            StringBuilder bufferTable = new StringBuilder();
            foreach(var map in mapList)
            {
                if (map.Value.Name.Equals("Quake"))
                {
                    bufferTable.Append(string.Format("|*:{0}[{1}]-Quake", GetQuakeDistance(), stateCode));
                }

                if (map.Value.Name.Equals("RetroQuake"))
                {
                    bufferTable.Append(string.Format("|*:{0}[{1}]-RetroQuake", GetQuakeDistance(), stateCode));
                }

                if (map.Value.Name.Equals("FireBates"))
                {
                    bufferTable.Append(string.Format("|*:{0}[{1}]-FireBates", GetFireBatesDistance(), stateCode));
                }

                if (map.Value.Name.Equals("Mine"))
                {
                    bufferTable.Append(string.Format("|*:{0}[{1}]-Mine", GetMiningSubsidenceDistance(), stateCode));
                }

                if (map.Value.Name.Equals("Rating"))
                {
                    bufferTable.Append(string.Format("|*:{0}[{1}]-Rating", GetRatingDistance(), stateCode));
                }

                if (map.Value.Name.Equals("RatingAAIS"))
                {
                    bufferTable.Append(string.Format("|*:{0}[{1}]-RatingAAIS", GetRatingDistance(), stateCode));
                }
            }
            return bufferTable.ToString();
        }

        private Dictionary<string, Map> BuildMapList()
        {
            Dictionary<string, Map> mapList = new Dictionary<string, Map>();
            Map map;
            foreach (var repoMap in mapRepository.GetAll())
            {
                map = new Map();
                map.OutputFields = new List<string>();

                foreach (var output in repoMap.JB_Maps_OutputFields)
                {
                    map.OutputFields.Add(output.JB_OutputField.Value);
                }
                map.Name = repoMap.Name;
                map.Format = repoMap.JB_MapFormat.Format;
                map.LookupType = repoMap.JB_LookupType.Name;

                mapList.Add(map.Name, map);
            }

            return mapList;
        }

        #region Generic Settings
        // TODO: move these all to a config file or a db
        private string GetServerName()
        {
            return "CAMDEVAB03";
        }

        private string GetPort()
        {
            return "4660";
        }

        private bool GetKeepMultiMatch()
        {
            return false;
        }

        private int GetOffsetDistance()
        {
            return 50;
        }

        private int GetMatchMode()
        {
            return 2;
        }

        private int GetInputMode()
        {
            return 0;
        }

        private int GetDatum()
        {
            return 1;
        }

        private int GetCentroidPreference()
        {
            return 1;
        }

        private bool GetMixedCase()
        {
            return true;
        }

        private int GetCoordinateType()
        {
            return 1;
        }

        private string GetInitList()
        {
            return "Geostan|Geostan_Z9";
        }

        private string GetInputFieldList(string stateCode)
        {
            return string.IsNullOrEmpty(stateCode) ? "Lastline" : "addressline|lastline";

        }

        private string GetOutputFieldList()
        {
            return "locationqualitycode|matchcode|city|state|zip10|zip";
        }

        private string GetFireBatesDistance()
        {
            return "1000";
        }

        private string GetRatingDistance()
        {
            return "5280";
        }

        private string GetQuakeDistance()
        {
            return "5280";
        }

        private string GetMiningSubsidenceDistance()
        {
            return "5280";
        }
        #endregion
    }
}
