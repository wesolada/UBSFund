namespace FundTests.Helpers
{
    using NUnit.Framework;
    using UBSFund.Helpers;

    [TestFixture]
    public class StockDTOTests
    {
        [Test]
        public void StockDTO_CorrectBehavior()
        {
            //Arrange
            int stockTypeID = 1;
            string name = "Test";
            decimal price = 10230.00m;
            int quantity = 123;
            decimal marketValue = price*quantity;
            decimal transactionCost = marketValue*0.005m;
            decimal weight = 45.00m;

            //Act
            var stock = new StockDTO
            {
                StockTypeID = stockTypeID,
                Name = name,
                Price = price,
                Quantity = quantity,
                MarketValue = marketValue,
                TransactionCost = transactionCost,
                Weight = weight
            };

            //Assert
            Assert.AreEqual(stockTypeID, stock.StockTypeID);
            Assert.AreEqual(name, stock.Name);
            Assert.AreEqual(price, stock.Price);
            Assert.AreEqual(quantity, stock.Quantity);
            Assert.AreEqual(marketValue, stock.MarketValue);
            Assert.AreEqual(transactionCost, stock.TransactionCost);
            Assert.AreEqual(weight, stock.Weight);
        }
    }
}