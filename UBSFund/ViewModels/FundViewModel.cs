namespace UBSFund.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using Enums;
    using Helpers;
    using MVVMClasses;

    public class FundViewModel : BaseViewModel
    {
        private readonly FundRepository fundRepository;
        private bool error;
        private int fundID;
        private decimal stockPrice;
        private int stockQuantity;
        private StockType stockType;

        public FundViewModel()
        {
            this.fundRepository = new FundRepository();
            this.AddCommand = new DelegateCommand(this.AddStock, this.CanAddStock);
            this.StockType = StockType.Equity;
        }

        public bool Error
        {
            get { return this.error; }
            set
            {
                if (this.error != value)
                {
                    this.error = value;
                    this.OnPropertyChanged(() => this.Error);
                }
            }
        }

        public int FundID
        {
            get { return this.fundID; }
            set
            {
                if (this.fundID != value)
                {
                    this.fundID = value;
                    this.OnPropertyChanged(() => this.FundID);
                }
            }
        }

        public StockType StockType
        {
            get { return this.stockType; }
            set
            {
                if (this.stockType != value)
                {
                    this.stockType = value;
                    this.OnPropertyChanged(() => this.StockType);
                }
            }
        }

        public decimal StockPrice
        {
            get { return this.stockPrice; }
            set
            {
                if (this.stockPrice != value)
                {
                    this.stockPrice = value;
                    this.OnPropertyChanged(() => this.StockPrice);
                    this.AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int StockQuantity
        {
            get { return this.stockQuantity; }
            set
            {
                if (this.stockQuantity != value)
                {
                    this.stockQuantity = value;
                    this.OnPropertyChanged(() => this.StockQuantity);
                    this.AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public List<StockDTO> StocksList
        {
            get { return this.fundRepository.GetAllStocks(); }
        }

        public List<Fund> FundsList
        {
            get { return this.fundRepository.GetAllFunds(); }
        }

        public int EquityTotalNumber
        {
            get { return this.fundRepository.GetStockQuantity((int)StockType.Equity); }
        }

        public decimal EquityTotalWeight
        {
            get { return this.StockTotalMarketValue == 0 ? 0 : this.EquityTotalMarketValue/this.StockTotalMarketValue; }
        }

        public decimal EquityTotalMarketValue
        {
            get { return this.fundRepository.GetStockTotalMarketValue((int)StockType.Equity); }
        }

        public int BondTotalNumber
        {
            get { return this.fundRepository.GetStockQuantity((int)StockType.Bond); }
        }

        public decimal BondTotalWeight
        {
            get { return this.StockTotalMarketValue == 0 ? 0 : this.BondTotalMarketValue/this.StockTotalMarketValue; }
        }

        public decimal BondTotalMarketValue
        {
            get { return this.fundRepository.GetStockTotalMarketValue((int)StockType.Bond); }
        }

        public int StockTotalNumber
        {
            get { return this.fundRepository.GetStockQuantity(); }
        }

        public decimal StockTotalWeight
        {
            get { return this.StockTotalNumber == 0 ? 0 : 1; }
        }

        public decimal StockTotalMarketValue
        {
            get { return this.fundRepository.GetStockTotalMarketValue(); }
        }

        public List<CultureInfo> CultureList
        {
            get
            {
                return new List<CultureInfo>
                {
                    new CultureInfo("de-CH"),
                    new CultureInfo("en-US"),
                    new CultureInfo("en-GB"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("pl-PL")
                };
            }
        }

        public DelegateCommand AddCommand { get; set; }

        private void AddStock(object parameter)
        {
            var stock = new Stock
            {
                FundID = this.FundID + 1,
                StockTypeID = (int)this.StockType,
                Price = this.StockPrice,
                Quantity = this.StockQuantity
            };

            if (this.fundRepository.Add(stock))
            {
                this.OnPropertyChanged(() => this.EquityTotalNumber);
                this.OnPropertyChanged(() => this.EquityTotalMarketValue);
                this.OnPropertyChanged(() => this.EquityTotalWeight);
                this.OnPropertyChanged(() => this.BondTotalNumber);
                this.OnPropertyChanged(() => this.BondTotalMarketValue);
                this.OnPropertyChanged(() => this.BondTotalWeight);
                this.OnPropertyChanged(() => this.StockTotalNumber);
                this.OnPropertyChanged(() => this.StockTotalMarketValue);
                this.OnPropertyChanged(() => this.StockTotalWeight);
                this.OnPropertyChanged(() => this.StocksList);
                this.Error = false;
            }
            else
            {
                this.Error = true;
            }

            this.ClearValues();
        }

        private bool CanAddStock(object parameter)
        {
            return this.StockPrice != 0 && this.StockQuantity != 0;
        }

        private void ClearValues()
        {
            this.StockPrice = 0;
            this.StockQuantity = 0;
        }
    }
}