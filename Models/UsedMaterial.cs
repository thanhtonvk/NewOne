//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsedMaterial
    {
        public int ID { get; set; }
        public int IDFoodDetails { get; set; }
        public int IDMaterial { get; set; }
        public Nullable<double> Quantity { get; set; }
    
        public virtual FoodDetail FoodDetail { get; set; }
        public virtual Material Material { get; set; }
    }
}
