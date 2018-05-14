using ParkingCore.Logger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ParkingCore.Interfaces
{
    public interface IParking
    {
        IList<Car> Cars { get; set; }
        ObservableCollection<Transaction> Transactions { get; set; }
        decimal Balance { get; }
        ISettings Settings { get; }

        void AddCar(Car car);

        int GetFreeParkingSpaces();

        int GetNotFreeParkingSpaces();

        IEnumerable<Transaction> GetTransactionsForLastMinute();

        void ShowTransactionSumForOneMinute();

        void AddMoneyToCar(decimal money, Guid choosedCar);

        void GetPaymentFromCar();

        decimal GetBalance();

        void LogTransactionEveryMinute();

        string[] ShowLog();

        decimal GetTransactionSumForOneMinute();

        IEnumerable<Car> GetCars();

        Car GetCar(Guid carId);

        void RemoveCar(Guid carId);

        Transaction GetTransactionsForLastMinute(Guid carId);
    }
}
