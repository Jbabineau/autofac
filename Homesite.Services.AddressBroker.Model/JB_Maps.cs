//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homesite.Services.AddressBroker.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class JB_Maps
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JB_Maps()
        {
            this.JB_Maps_OutputFields = new HashSet<JB_Maps_OutputFields>();
            this.JB_State_Maps = new HashSet<JB_State_Maps>();
        }
    
        public int MapId { get; set; }
        public string Name { get; set; }
        public int FormatId { get; set; }
        public int LookupId { get; set; }
    
        public virtual JB_LookupType JB_LookupType { get; set; }
        public virtual JB_MapFormat JB_MapFormat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JB_Maps_OutputFields> JB_Maps_OutputFields { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JB_State_Maps> JB_State_Maps { get; set; }
    }
}
