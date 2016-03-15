namespace FundTests.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using NUnit.Framework;
    using TypeMock.ArrangeActAssert;
    using UBSFund;
    using UBSFund.Helpers;
    using UBSFund.ViewModels;
    using StockType = UBSFund.Enums.StockType;

    [TestFixture]
    public class FundViewModelTests
    {
        [SetUp]
        public void Init()
        {
            this.fakeFundEntities = Isolate.Fake.NextInstance<FundEntities>(Members.ReturnRecursiveFakes);
            this.fundViewModel = new FundViewModel();
        }

        private FundEntities fakeFundEntities;
        private FundViewModel fundViewModel;

        private readonly List<Stock> stocksList = new List<Stock>
        {
            new Stock
            {
                ID = 1,
                StockTypeID = (int)StockType.Equity,
                Name = "Equity",
                Price = 123.00m,
                Quantity = 10,
                MarketValue = 1230.00m,
                TransactionCost = 6.15m,
                Weight = 4.87m
            },
            new Stock
            {
                ID = 2,
                StockTypeID = (int)StockType.Bond,
                Name = "Bond",
                Price = 240.00m,
                Quantity = 100,
                MarketValue = 24000.00m,
                TransactionCost = 480.00m,
                Weight = 95.13m
            }
        };

        private readonly List<StockDTO> stocksDTOList = new List<StockDTO>
        {
            new StockDTO
            {
                StockTypeID = (int)StockType.Equity,
                Name = "Equity",
                Price = 123.00m,
                Quantity = 10,
                MarketValue = 1230.00m,
                TransactionCost = 6.15m,
                Weight = 4.87m
            },
            new StockDTO
            {
                StockTypeID = (int)StockType.Bond,
                Name = "Bond",
                Price = 240.00m,
                Quantity = 100,
                MarketValue = 24000.00m,
                TransactionCost = 480.00m,
                Weight = 95.13m
            }
        };

        private readonly List<Fund> fundsList = new List<Fund>
        {
            new Fund
            {
                FundID = 1,
                Description = "UBS investments",
                Value = 10000m
            }
        };

        private readonly List<UBSFund.StockType> stockTypesList = new List<UBSFund.StockType>
        {
            new UBSFund.StockType
            {
                ID = 1,
                Description = "Equity"
            },
            new UBSFund.StockType
            {
                ID = 2,
                Description = "Bond"
            }
        };

        private int GetStockTotalNumber(int type = 0)
        {
            return type != 0
                ? this.stocksList.Where(x => x.StockTypeID == type).Sum(x => x.Quantity)
                : this.stocksList.Sum(x => x.Quantity);
        }

        private decimal GetStockTotalWeight(int type = 0)
        {
            return type != 0
                ? this.GetStockTotalMarketValue(type)/this.GetStockTotalMarketValue()
                : this.GetStockTotalNumber() == 0 ? 0 : 1;
        }

        private decimal GetStockTotalMarketValue(int type = 0)
        {
            return type != 0
                ? this.stocksList.Where(x => x.StockTypeID == type).Sum(x => x.Quantity*x.Price)
                : this.stocksList.Sum(x => x.Quantity*x.Price);
        }

        [Test, Isolated]
        public void AddStock_ItemAdded_EventsFired()
        {
            //Arrange
            this.fundViewModel.StockPrice = 10.00M;
            this.fundViewModel.StockQuantity = 10;
            var counter = 0;

            var fakeFundRepository = Isolate.Fake.AllInstances<FundRepository>();

            Isolate.WhenCalled(() => fakeFundRepository.Add<Stock>(null)).WillReturn(true);

            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());

            Isolate.WhenCalled(() => this.fakeFundEntities.StockTypes)
                .WillReturnCollectionValuesOf(this.stockTypesList.AsQueryable());

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.EquityTotalNumber)
                    || args.PropertyName == nameof(this.fundViewModel.EquityTotalMarketValue)
                    || args.PropertyName == nameof(this.fundViewModel.EquityTotalWeight)
                    || args.PropertyName == nameof(this.fundViewModel.BondTotalNumber)
                    || args.PropertyName == nameof(this.fundViewModel.BondTotalMarketValue)
                    || args.PropertyName == nameof(this.fundViewModel.BondTotalWeight)
                    || args.PropertyName == nameof(this.fundViewModel.BondTotalWeight)
                    || args.PropertyName == nameof(this.fundViewModel.StockTotalNumber)
                    || args.PropertyName == nameof(this.fundViewModel.StockTotalMarketValue)
                    || args.PropertyName == nameof(this.fundViewModel.StockTotalWeight)
                    || args.PropertyName == nameof(this.fundViewModel.StocksList))
                {
                    counter++;
                }
            };

            //Act
            this.fundViewModel.AddCommand.Execute(null);

            //Assert
            Assert.AreEqual(10, counter);
            Assert.AreEqual(0M, this.fundViewModel.StockPrice);
            Assert.AreEqual(0, this.fundViewModel.StockQuantity);
            Assert.IsFalse(this.fundViewModel.AddCommand.CanExecute(null));
            Assert.IsFalse(this.fundViewModel.Error);
        }

        [Test, Isolated]
        public void AddStock_ItemNotAdded_EventsNotFired()
        {
            //Arrange
            this.fundViewModel.StockPrice = 10.00M;
            this.fundViewModel.StockQuantity = 10;
            var counter = 0;

            var fakeFundRepository = Isolate.Fake.AllInstances<FundRepository>();

            Isolate.WhenCalled(() => fakeFundRepository.Add<Stock>(null)).WillReturn(false);

            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(new List<Stock>().AsQueryable());

            Isolate.WhenCalled(() => this.fakeFundEntities.StockTypes)
                .WillReturnCollectionValuesOf(this.stockTypesList.AsQueryable());

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.EquityTotalNumber)
                    || args.PropertyName == nameof(this.fundViewModel.EquityTotalMarketValue)
                    || args.PropertyName == nameof(this.fundViewModel.EquityTotalWeight)
                    || args.PropertyName == nameof(this.fundViewModel.BondTotalNumber)
                    || args.PropertyName == nameof(this.fundViewModel.BondTotalMarketValue)
                    || args.PropertyName == nameof(this.fundViewModel.BondTotalWeight)
                    || args.PropertyName == nameof(this.fundViewModel.BondTotalWeight)
                    || args.PropertyName == nameof(this.fundViewModel.StockTotalNumber)
                    || args.PropertyName == nameof(this.fundViewModel.StockTotalMarketValue)
                    || args.PropertyName == nameof(this.fundViewModel.StockTotalWeight)
                    || args.PropertyName == nameof(this.fundViewModel.StocksList))
                {
                    counter++;
                }
            };

            //Act
            this.fundViewModel.AddCommand.Execute(null);

            //Assert
            Assert.AreEqual(0, counter);
            Assert.AreEqual(0M, this.fundViewModel.StockPrice);
            Assert.AreEqual(0, this.fundViewModel.StockQuantity);
            Assert.IsFalse(this.fundViewModel.AddCommand.CanExecute(null));
            Assert.IsTrue(this.fundViewModel.Error);
        }

        [Test, Isolated]
        public void BondTotalMarketValue()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            decimal value = this.GetStockTotalMarketValue((int)StockType.Bond);

            //Act
            decimal result = this.fundViewModel.BondTotalMarketValue;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void BondTotalNumber_CorrectBehavior()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            int value = this.GetStockTotalNumber((int)StockType.Bond);

            //Act
            int result = this.fundViewModel.BondTotalNumber;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void BondTotalWeight_CorrectBehavior()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            decimal value = this.GetStockTotalWeight((int)StockType.Bond);

            //Act
            decimal result = this.fundViewModel.BondTotalWeight;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void Constructor_Test()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());

            //Assert
            Assert.IsNotNull(this.fakeFundEntities);
            Assert.IsNotNull(this.fundViewModel.AddCommand);
            Assert.AreEqual(StockType.Equity, this.fundViewModel.StockType);
        }

        [Test, Isolated]
        public void CultureList_Test()
        {
            //Act
            List<CultureInfo> result = this.fundViewModel.CultureList;

            //Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("de-CH", result[0].Name);
            Assert.AreEqual("en-US", result[1].Name);
            Assert.AreEqual("en-GB", result[2].Name);
            Assert.AreEqual("de-DE", result[3].Name);
            Assert.AreEqual("pl-PL", result[4].Name);
        }

        [Test, Isolated]
        public void EquityTotalMarketValue()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            decimal value = this.GetStockTotalMarketValue((int)StockType.Equity);

            //Act
            decimal result = this.fundViewModel.EquityTotalMarketValue;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void EquityTotalNumber_CorrectBehavior()
        {
            //Arrange
            var value = 10;

            //Act
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            int result = this.fundViewModel.EquityTotalNumber;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void EquityTotalWeight_CorrectBehavior()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            decimal value = this.GetStockTotalWeight((int)StockType.Equity);

            //Act
            decimal result = this.fundViewModel.EquityTotalWeight;

            //Assert
            Assert.AreEqual(value, result);
        }


        [Test, Isolated]
        public void Error_DifferentValue_PropertyChangedCalled()
        {
            //Arrange
            var value = true;
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.Error))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.Error = value;

            //Assert
            Assert.IsTrue(propertyChangedCalled);
            Assert.AreEqual(value, this.fundViewModel.Error);
        }

        [Test, Isolated]
        public void Error_SameValue_PropertyChangedNotCalled()
        {
            //Arrange
            var value = false;
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.Error))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.Error = value;

            //Assert
            Assert.IsFalse(propertyChangedCalled);
            Assert.AreEqual(value, this.fundViewModel.Error);
        }


        [Test, Isolated]
        public void FundID_DifferentValue_PropertyChangedCalled()
        {
            //Arrange
            var value = 1;
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.FundID))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.FundID = value;

            //Assert
            Assert.IsTrue(propertyChangedCalled);
            Assert.AreEqual(value, this.fundViewModel.FundID);
        }


        [Test, Isolated]
        public void FundID_SameValue_PropertyChangedNotCalled()
        {
            //Arrange
            var value = 0;
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.FundID))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.FundID = value;

            //Assert
            Assert.IsFalse(propertyChangedCalled);
            Assert.AreEqual(value, this.fundViewModel.FundID);
        }

        [Test, Isolated]
        public void FundsList_CorrectBehavior()
        {
            //Arrange
            var fakeFundRepository = Isolate.Fake.AllInstances<FundRepository>();

            Isolate.WhenCalled(() => fakeFundRepository.GetAllFunds()).WillReturn(this.fundsList);

            var value = 1;

            //Act
            List<Fund> result = this.fundViewModel.FundsList;

            //Assert
            Assert.AreEqual(value, result.Count);
            Assert.AreEqual(this.fundsList[0].FundID, result[0].FundID);
            Assert.AreEqual(this.fundsList[0].Description, result[0].Description);
            Assert.AreEqual(this.fundsList[0].Value, result[0].Value);
        }

        [Test, Isolated]
        public void StockPrice_DifferentValue_PropertyChangedCalled()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fundViewModel.AddCommand.RaiseCanExecuteChanged()).CallOriginal();
            var value = 1234.45M;
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.StockPrice))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.StockPrice = value;

            //Assert
            Assert.IsTrue(propertyChangedCalled);
            Assert.AreEqual(value, this.fundViewModel.StockPrice);
            Isolate.Verify.WasCalledWithAnyArguments(() => this.fundViewModel.AddCommand.RaiseCanExecuteChanged());
        }

        [Test, Isolated]
        public void StockPrice_SameValue_PropertyChangedNotCalled()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fundViewModel.AddCommand.RaiseCanExecuteChanged()).CallOriginal();
            var value = 0M;
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.StockPrice))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.StockPrice = value;

            //Assert
            Assert.IsFalse(propertyChangedCalled);
            Assert.AreEqual(value, this.fundViewModel.StockPrice);
            Isolate.Verify.WasNotCalled(() => this.fundViewModel.AddCommand.RaiseCanExecuteChanged());
        }

        [Test, Isolated]
        public void StockQuantity_DifferentValue_PropertyChangedCalled()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fundViewModel.AddCommand.RaiseCanExecuteChanged()).CallOriginal();
            var value = 123;
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.StockQuantity))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.StockQuantity = value;

            //Assert
            Assert.IsTrue(propertyChangedCalled);
            Assert.AreEqual(value, this.fundViewModel.StockQuantity);
            Isolate.Verify.WasCalledWithAnyArguments(() => this.fundViewModel.AddCommand.RaiseCanExecuteChanged());
        }

        [Test, Isolated]
        public void StockQuantity_SameValue_PropertyChangedNotCalled()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fundViewModel.AddCommand.RaiseCanExecuteChanged()).CallOriginal();
            var value = 0;
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.StockQuantity))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.StockQuantity = value;

            //Assert
            Assert.IsFalse(propertyChangedCalled);
            Assert.AreEqual(value, this.fundViewModel.StockQuantity);
            Isolate.Verify.WasNotCalled(() => this.fundViewModel.AddCommand.RaiseCanExecuteChanged());
        }

        [Test, Isolated]
        public void StocksList_CorrectBehavior()
        {
            //Arrange
            var fakeFundRepository = Isolate.Fake.AllInstances<FundRepository>();

            Isolate.WhenCalled(() => fakeFundRepository.GetAllStocks()).WillReturn(this.stocksDTOList);

            var value = 2;

            //Act
            List<StockDTO> result = this.fundViewModel.StocksList;

            //Assert
            Assert.AreEqual(value, result.Count);
        }


        [Test, Isolated]
        public void StockTotalMarketValue()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            decimal value = this.GetStockTotalMarketValue();

            //Act
            decimal result = this.fundViewModel.StockTotalMarketValue;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void StockTotalNumber_CorrectBehavior()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            int value = this.GetStockTotalNumber();

            //Act
            int result = this.fundViewModel.StockTotalNumber;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void StockTotalWeight_FundEmpty_Return0()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(new List<Stock>().AsQueryable());
            var value = 0M;

            //Act
            decimal result = this.fundViewModel.StockTotalWeight;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void StockTotalWeight_FundNotEmpty_Return1()
        {
            //Arrange
            Isolate.WhenCalled(() => this.fakeFundEntities.Stocks)
                .WillReturnCollectionValuesOf(this.stocksList.AsQueryable());
            var value = 1M;

            //Act
            decimal result = this.fundViewModel.StockTotalWeight;

            //Assert
            Assert.AreEqual(value, result);
        }

        [Test, Isolated]
        public void StockType_DifferentValue_PropertyChangedCalled()
        {
            //Arrange
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.StockType))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.StockType = StockType.Bond;

            //Assert
            Assert.IsTrue(propertyChangedCalled);
            Assert.AreEqual(StockType.Bond, this.fundViewModel.StockType);
        }

        [Test, Isolated]
        public void StockType_SameValue_PropertyChangedNotCalled()
        {
            //Arrange
            var propertyChangedCalled = false;

            this.fundViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.fundViewModel.StockType))
                {
                    propertyChangedCalled = true;
                }
            };

            //Act
            this.fundViewModel.StockType = StockType.Equity;

            //Assert
            Assert.IsFalse(propertyChangedCalled);
            Assert.AreEqual(StockType.Equity, this.fundViewModel.StockType);
        }
    }
}