namespace UBSFund.Helpers
{
    public class StockDTO
    {
        public int StockTypeID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal MarketValue { get; set; }

        public decimal TransactionCost { get; set; }

        public decimal Weight { get; set; }
    }
}