//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prolabson.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Randevular
    {
        public int RandevuID { get; set; }
        public Nullable<System.DateTime> RandevuTarihi { get; set; }
        public Nullable<int> HastaID { get; set; }
        public Nullable<int> DoktorID { get; set; }
    
        public virtual Doktorlar Doktorlar { get; set; }
        public virtual Hastalar Hastalar { get; set; }
    }
}
