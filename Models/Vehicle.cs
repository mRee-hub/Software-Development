//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoTicket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            this.Payments = new HashSet<Payment>();
            this.Tickets = new HashSet<Ticket>();
        }
    
        public int vehicle_id { get; set; }
        public string vehicle_type { get; set; }
        public string vehicle_condition { get; set; }
        public string vehicle_attribute { get; set; }
        public string departure_location { get; set; }
        public string arrival_location { get; set; }
        public System.DateTime arrival_date { get; set; }
        public System.DateTime departure_date { get; set; }
        public int total_amount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
