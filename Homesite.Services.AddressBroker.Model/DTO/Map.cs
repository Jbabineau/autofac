using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Model.DTO
{
    public class Map
    {
        /// <summary>
        /// The map name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// String.Format uses this value to assemble State and Map into a 
        /// file name, ie: MA-Rating, Shoreline, CO-Hail
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// The target property of the AddressResult class that should 
        /// store the result of this address query.
        /// </summary>
        public string ResultTarget { get; set; }

        /// <summary>
        /// The type of lookup to perform (Spatial or GDL)
        /// </summary>
        public string LookupType { get; set; }

        /// <summary>
        /// a list of all output fields defined in the XML for this map.
        /// </summary>
        public List<string> OutputFields { get; set; }
    }
}
