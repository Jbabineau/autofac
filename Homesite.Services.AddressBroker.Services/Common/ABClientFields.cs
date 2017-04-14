using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Services.Common
{
    public class ABClientField
    {
        private ABClientField(string value) { Value = value;  }

        public string Value { get; set; }

        public static ABClientField AddressLine { get { return new ABClientField("AddressLine"); } }
        public static ABClientField LastLine { get { return new ABClientField("LastLine"); } }
        public static ABClientField LocationQualityCode { get { return new ABClientField("LOCATIONQUALITYCODE"); } }
        public static ABClientField MatchCode { get { return new ABClientField("MATCHCODE"); } }
        public static ABClientField City { get { return new ABClientField("CITY"); } }
        public static ABClientField State { get { return new ABClientField("STATE"); } }
        public static ABClientField Zipcode { get { return new ABClientField("ZIP"); } }
        public static ABClientField Latitude { get { return new ABClientField("LATITUDE"); } }
        public static ABClientField Longitude { get { return new ABClientField("LONGITUDE"); } }
        public static ABClientField AddressLineOutput { get { return new ABClientField("ADDRESSLINE"); } }
        public static ABClientField CensusBlockId { get { return new ABClientField("CENSUSBLOCKID"); } }
        public static ABClientField DeliveryPointBarCode { get { return new ABClientField("DeliveryPointBarCode"); } }
        public static ABClientField CheckDigit { get { return new ABClientField("CheckDigit"); } }
        public static ABClientField FIPSCountyCode { get { return new ABClientField("FIPSCountyCode"); } }
        public static ABClientField CountyName { get { return new ABClientField("CountyName"); } }
        public static ABClientField HouseNumber { get { return new ABClientField("HouseNumber"); } }
        public static ABClientField PrefixDirection { get { return new ABClientField("PrefixDirection"); } }
        public static ABClientField PostfixDirection { get { return new ABClientField("PostfixDirection"); } }
        public static ABClientField StreetName { get { return new ABClientField("StreetName"); } }
        public static ABClientField StreetType { get { return new ABClientField("StreetType"); } }
        public static ABClientField UnitType { get { return new ABClientField("UnitType"); } }
        public static ABClientField UnitNumber { get { return new ABClientField("UnitNumber"); } }
        public static ABClientField ZipClass { get { return new ABClientField("ZipClass"); } }
        public static ABClientField ZipExtension { get { return new ABClientField("ZIP4"); } }


        public static ABClientField Socket { get { return new ABClientField("SOCKET"); } }
        public static ABClientField InitList { get { return new ABClientField("INIT_LIST"); } }
        public static ABClientField InputFieldList { get { return new ABClientField("INPUTFIELDLIST"); } }
        public static ABClientField OutputFieldList { get { return new ABClientField("OUTPUTFIELDLIST"); } }
        public static ABClientField BufferRadiusTable { get { return new ABClientField("Buffer Radius Table"); } }

        public static ABClientField KeepMultiMatch { get { return new ABClientField("Keep_multimatch"); } }
        public static ABClientField OffsetDistance { get { return new ABClientField("OFFSET_DISTANCE"); } }
        public static ABClientField MatchMode { get { return new ABClientField("MATCH_MODE"); } }
        public static ABClientField InputMode { get { return new ABClientField("Input_Mode"); } }
        public static ABClientField Datum { get { return new ABClientField("DATUM"); } }
        public static ABClientField CentroidPreference { get { return new ABClientField("CENTROID_PREFERENCE"); } }
        public static ABClientField MixedCase { get { return new ABClientField("MIXED_CASE"); } }
        public static ABClientField CoordinateType { get { return new ABClientField("COORDINATE_TYPE"); } }
        
        
    }
}
