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
    
    public partial class Kullanicilar
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string KullaniciTipi { get; set; }
    
        public virtual Doktorlar Doktorlar { get; set; }
        public virtual Hastalar Hastalar { get; set; }
        public virtual Yonetici Yonetici { get; set; }
    }
}