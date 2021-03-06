//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StarbucksScraper
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store
    {
        public Store()
        {
            this.Features = new HashSet<Feature>();
            this.RegularHours = new HashSet<RegularHour>();
        }
    
        public int Id { get; set; }
        public int StarbucksStoreID { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string StoreNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string OwnershipType { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Street3 { get; set; }
        public string City { get; set; }
        public string CountrySubdivisionCode { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public int TZOffset { get; set; }
        public string TZID { get; set; }
        public string TZOlsonID { get; set; }
        public System.DateTime LastSeen { get; set; }
        public System.DateTime FirstSeen { get; set; }
    
        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<RegularHour> RegularHours { get; set; }
    }
}
