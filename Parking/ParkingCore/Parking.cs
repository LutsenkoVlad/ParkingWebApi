using ParkingCore.Enums;
using ParkingCore.Interfaces;
using ParkingCore.Logger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingCore
{
    public class Parking : IParking
    {
        public IList<Car> Cars { get; set; } = new List<Car>
            {
                new Car(100,CarType.Bus),
                new Car(100,CarType.Motorcycle),
                new Car(100,CarType.Passenger),
                new Car(100,CarType.Truck),
                new Car(100,CarType.Motorcycle),
                new Car(0,CarType.Truck),
            };

        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();
        public decimal Balance { get; private set; } = 0;
        public ISettings Settings { get; private set; }
        private BaseLogger _logger;

        Timer parkingPaymentTimer = null;
        Timer logTransactionTimer = null;

        public Parking(ISettings settings, BaseLogger logger)
        {
            Transactions.CollectionChanged += AddTransaction;

            Settings = settings;
            _logger = logger;

            parkingPaymentTimer = new Timer((e) =>
            {
                GetPaymentFromCar();
            }, null, TimeSpan.FromSeconds(Settings.Timeout), TimeSpan.FromSeconds(Settings.Timeout));

            logTransactionTimer = new Timer((e) =>
            {
                LogTransactionEveryMinute();
            }, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
        }

        public void AddCar(Car car)
        {
            if (Settings.ParkingSpace > Cars.Count)
                Cars.Add(car);
        }

        public int GetFreeParkingSpaces() => Settings.ParkingSpace - Cars.Count;

        public int GetNotFreeParkingSpaces() => Cars.Count();

        public IEnumerable<Transaction> GetTransactionsForLastMinute()
            => Transactions.Where(x => x.Date_Time > DateTime.Now - TimeSpan.FromSeconds(60));

        public Transaction GetTransactionsForLastMinute(Guid carId)
            => Transactions.Where(x => x.Date_Time > DateTime.Now - TimeSpan.FromSeconds(60) && x.CarId == carId).FirstOrDefault();

        public void ShowTransactionSumForOneMinute() => GetTransactionSumForOneMinute();

        public void AddMoneyToCar(decimal money, Guid choosedCar)
            => Cars.Where(x => x.Id == choosedCar).FirstOrDefault().AddMoney(money);

        public void GetPaymentFromCar()
        {
            //It has sense when will be a lot of cars in parking
            //Parallel.ForEach(Cars, (car, state, index) =>
            //{
            //    decimal sum = Settings.Prices.Where(x => x.Key == car.CarType).Select(x => x.Value).FirstOrDefault();
            //    if (car.Balance < sum) sum = sum * Settings.Fine;
            //    var transaction = new Transaction(car.Id, sum);
            //    Transactions.Add(transaction);
            //});
            foreach (var car in Cars)
            {
                decimal sum = Settings.Prices.Where(x => x.Key == car.CarType).Select(x => x.Value).FirstOrDefault();
                if (car.Balance < sum) sum = sum * Settings.Fine;
                var transaction = new Transaction(car.Id, sum);
                Transactions.Add(transaction);
            }
        }

        public decimal GetBalance() => Balance;

        public IDictionary<CarType, decimal> ShowPrices() => Settings.Prices;

        public void LogTransactionEveryMinute()
        {
            _logger.Log(GetTransactionSumForOneMinute() + " - " + DateTime.Now);
        }

        public string[] ShowLog() => File.ReadAllLines("Transactions.log"); 

        public void RemoveCar(Guid carId)
        {
            Car car = Cars.Where(x => x.Id == carId).SingleOrDefault();
            if (car == null) throw new Exception("Car not found");
            if (car.Balance >= 0)
            {
                Cars.Remove(car);
            }
            else
            {
                throw new Exception("You have to pay parking." + Environment.NewLine + "Then you can take the car.");
            }
        }

        private void AddTransaction(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var transaction = e.NewItems.OfType<Transaction>().First();
                Balance += transaction.WriteOffs;
                Car car = Cars.Where(x => x.Id == transaction.CarId).SingleOrDefault();
                car.Balance -= transaction.WriteOffs;
            }
        }

        public decimal GetTransactionSumForOneMinute()
        {
            decimal sum = 0;
            for(int i = 0; i < Transactions.Count; i++)
            {
                if(Transactions[i].Date_Time > DateTime.Now - TimeSpan.FromSeconds(60))
                {
                    sum += Transactions[i].WriteOffs;
                }
            }
            return sum;
        }
            //=> Transactions.Where(x => x.Date_Time > DateTime.Now - TimeSpan.FromSeconds(60)).Sum(x => x.WriteOffs);

        public IEnumerable<Car> GetCars() => Cars;

        public Car GetCar(Guid carId) => Cars.Where(x => x.Id == carId).SingleOrDefault();
    }
}