using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Model.DTO
{
    public class AddressConfiguration
    {
        public Dictionary<string, Map> MapList
        {
            get;
            set;
        }

        public string InitList { get; set; }
        public string OutputFieldStringList { get; set; }
        public bool LoadedProtectionClassMap { get; set; }
        public bool LoadedMunicipalityMap { get; set; }
        public bool LoadedFireDistrictMap { get; set; }
        public bool LoadedBrushFireComplete { get; set; }
        public bool LoadedBrushFireHighRisk { get; set; }
        public bool LoadedFireBreakComplete { get; set; }
        public bool LoadedFireBreakWildland { get; set; }
        public bool CalculateWildfireScore { get; set; }

        public bool KeepMultiMatch { get; set; }
        public int OffSetDistance { get; set; }
        public int MatchMode { get; set; }
        public int InputMode { get; set; }
        public int Datum { get; set; }
        public int CentroidPreference { get; set; }
        public bool MixedCase { get; set; }
        public int CoordinateType { get; set; }
        public string InputFieldList { get; set; }
        public string BufferTable { get; set; }
        public string ServerName { get; set; }
        public string Port { get; set; }

    }
}
