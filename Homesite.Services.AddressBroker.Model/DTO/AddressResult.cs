using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Model.DTO
{
    /// <summary>
    /// Enum for the GenX scrub codes
    /// </summary>
    public enum GenXScrubCode
    {
        ALL_OK = 0,
        SCRUBBED_OK = 4,
        CENTROID_OK = 8,
        NOT_FOUND = 12,
        INTERNAL_ERROR = 16
    }

    /// <summary>
    /// Enum for the GenX return codes.
    /// </summary>
    public enum GenXReturnCode
    {
        LOOKUP_SUCCESS = 1,
        LOOKUP_NOT_FOUND = 6,
        LOOKUP_ERROR = -1
    }

    /// <summary>
    /// Address Result Data Contract
    /// </summary>
    public class AddressResult
    {
        /** META **/

        public AddressResult()
        {
            // initialize to error/default values, app must indicate success.
            LegacyScrubCode = GenXScrubCode.INTERNAL_ERROR;
            LegacyReturnCode = GenXReturnCode.LOOKUP_ERROR;
            CentroidUsed = false;

            // legacy defaults, yuck.
            ShoreLineDistance = 99999;
            GEOWFFireStDist = "99999";
            Slope = (decimal)9.99999;
            GEOWFFireStDist = "999999";
            QuakeFaultDistance = 99999;

            // CInt props from GenX, so defaulting to "0"
            GEOAspect = "0";
            GEOPCTRock = "0";
            GEOPCTShrub = "0";
            GEOPCTForest = "0";
            GEOWindScore = "0";
            GEOLULCScore = 0;
            GEOWFScore = (decimal)0.00000;
            GEOSlopeScore = "0";
            GEOWFFireStationType = 0;
            GEODrought = "0";
            CatZone = null;
            ISOTerritory = 0;
        }

        public bool ErrorOccured { get; set; }
        public string ErrorMessage { get; set; }
        public GenXScrubCode LegacyScrubCode { get; set; }
        public GenXReturnCode LegacyReturnCode { get; set; }

        /** DIAGNOSTICS **/
        public Int64 ConstructorMilliseconds { get; set; }
        public Int64 ClientConnectionOpenMilliseconds { get; set; }
        public Int64 CallTotalTimeMilliseconds { get; set; }
        public Int64 CallServiceTimeMilliseconds { get; set; }

        /** DB PROPERTIES BELOW **/
        public string AddressLine { get; set; }
        public short? ATerritory { get; set; }

        public string BuildingCodeEffectivenessGradeCd { get; set; }

        public int? CatZone { get; set; }
        public string County { get; set; }
        public string CensusBlock { get; set; }
        public bool? CentroidUsed { get; set; }
        public int? CheckDigit { get; set; }
        public string City { get; set; }
        public string DeliveryPointBarCode { get; set; }
        public string DisasterDNQ { get; set; }
        public short? Exposure { get; set; }
        public string FipsCode { get; set; }
        public string FireProtectionClassCd { get; set; }
        public string FloodPanel { get; set; }
        public string FloodZone { get; set; }
        public string FloodSFHA { get; set; }
        public string FloodData { get; set; }
        public string FireDistrict { get; set; }
        public string FireBates { get; set; }
        public int? GeoCodePointer { get; set; }
        public string GeoFireTax { get; set; }
        public string GeoPoliceTax { get; set; }
        public string GEOSinkhole { get; set; }
        public string GEOAspect { get; set; }
        public string GEODrought { get; set; }
        public string GEOWFFireStDist { get; set; }
        public short? GEOWFFireStationType { get; set; }
        public short? GEOLULCScore { get; set; }
        public string GEOWindScore { get; set; }
        public string GEOSlopeScore { get; set; }
        public string GEOPCTForest { get; set; }
        public string GEOPCTShrub { get; set; }
        public string GEOPCTRock { get; set; }
        public decimal? GEOWFScore { get; set; }
        public string GEORetroQuake { get; set; }
        public short? HailScale { get; set; }
        public short? HailPercentile { get; set; }
        public string HouseNumber { get; set; }
        public short? HOTerritory { get; set; }
        public short? ISOTerritory { get; set; }
        public string LandSlide { get; set; }
        public decimal? Latitude { get; set; }
        public string LocationQualityCode { get; set; }
        public decimal? Longitude { get; set; }
        public string Liquefaction { get; set; }
        public string MatchCode { get; set; }

        // This is not used. It exists in the Session DB schema, but is never written to.
        //public string MSANumber { get; set; }

        public string Municipality { get; set; }
        public short? MoldTerritory { get; set; }
        public string PrefixDirection { get; set; }
        public string PostfixDirection { get; set; }
        public string QuakeZone { get; set; }
        public int? QuakeFaultDistance { get; set; }
        public string QuakeFaultId { get; set; }

        /// <summary>
        /// NOTE:
        /// If you are dealing with program type 4, you should store the value in
        /// AddressResult.IsoTer to the Address Broker Output table's RatingTerritory column.
        /// Legacy stuff.
        /// </summary>
        public string RatingTerritory { get; set; }


        public string RiskTax { get; set; }
        public string State { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string SRA { get; set; }
        public int? ShoreLineDistance { get; set; }
        public string Subsidence { get; set; }
        public decimal? Slope { get; set; }
        public string SlopeRange { get; set; }
        public string StreetDirectionPre { get; set; }
        public string StreetDirectionPost { get; set; }
        public short? TornadoScale { get; set; }
        public short? TornadoPercentile { get; set; }
        public string UnitType { get; set; }
        public string UnitNumber { get; set; }
        public string WindPool { get; set; }
        public string Zip { get; set; }
        public string Zip4 { get; set; }
        public string ZipClass { get; set; }

        #region Multivariate
        public decimal? PopulationDensity { get; set; }
        public decimal? GeoInsScore { get; set; }
        public decimal? ChildrenUnder18Percent { get; set; }
        public decimal? RMSWind { get; set; }
        #endregion

        //ITR#7884
        public string CBGroupID2010 { get; set; }
        public int? GridId { get; set; }
        public string GEOWindSpeed { get; set; }
        public string GEOMitiExposure { get; set; }
        public string GEOWBDRegion { get; set; }
    }
}
