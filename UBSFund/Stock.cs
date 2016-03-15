//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UBSFund
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Stock
    {
        public long ID { get; set; }
        public int FundID { get; set; }
        public int StockTypeID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal MarketValue { get; set; }
        public decimal TransactionCost { get; set; }

        [NotMapped]
        public decimal Weight { get; set; }
    
        public virtual StockType StockTypes { get; set; }
        public virtual Fund Fund { get; set; }
    }
}
