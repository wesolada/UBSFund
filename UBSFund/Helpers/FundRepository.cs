namespace UBSFund.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Interfaces;

    public class FundRepository : IRepository
    {
        private readonly FundEntities fundEntities;

        public FundRepository()
        {
            this.fundEntities = new FundEntities();
            this.fundEntities.Stocks.Load();
        }

        public bool Add<T>(T item) where T : class
        {
            try
            {
                this.fundEntities.Set<T>().Add(item);
                this.fundEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<StockDTO> GetAllStocks()
        {
            var result = this.fundEntities.Stocks.Join(
                this.fundEntities.Funds,
                s => s.FundID,
                tmv => tmv.FundID,
                (s, tmv) => new StockDTO()
                    {
                        StockTypeID = s.StockTypeID,
                        Name = s.Name,
                        Price = s.Price,
                        Quantity = s.Quantity,
                        MarketValue = s.MarketValue,
                        TransactionCost = s.TransactionCost,
                        Weight = s.MarketValue/tmv.Value
                    }
                );

            return result.ToList();
        }

        public List<Fund> GetAllFunds()
        {
            return this.fundEntities.Funds.ToList();
        } 

        public int GetStockQuantity(int type = 0)
        {
            if (type != 0 && this.fundEntities.Stocks.Any(x => x.StockTypeID == type))
            {
                return this.fundEntities.Stocks.Where(x => x.StockTypeID == type).Sum(x => x.Quantity);
            }

            if (type == 0 && this.fundEntities.Stocks.Any())
            {
                return this.fundEntities.Stocks.Sum(x => x.Quantity);
            }

            return 0;
        }

        public decimal GetStockTotalMarketValue(int type = 0)
        {
            if (type != 0 && this.fundEntities.Stocks.Any(x => x.StockTypeID == type))
            {
                return this.fundEntities.Stocks.Where(x => x.StockTypeID == type).Sum(x => x.Quantity*x.Price);
            }

            if (type == 0 && this.fundEntities.Stocks.Any())
            {
                return this.fundEntities.Stocks.Sum(x => x.Quantity*x.Price);
            }

            return 0;
        }
    }
}