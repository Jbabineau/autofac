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
    
    public partial class JB_States
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JB_States()
        {
            this.JB_State_Maps = new HashSet<JB_State_Maps>();
        }
    
        public int StateId { get; set; }
        public string Name { get; set; }
        public string StateCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JB_State_Maps> JB_State_Maps { get; set; }
    }
}
