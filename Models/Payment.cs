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
    
    public partial class Payment
    {
        public int payment_id { get; set; }
        public int payment_amount { get; set; }
        public string payment_type { get; set; }
        public int vehicle_id { get; set; }
        public int customer_id { get; set; }
        public int card_number { get; set; }
        public int card_cvc { get; set; }
        public int card_expiration { get; set; }
    
        public virtual Registration Registration { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
