using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ParkingCore.Enums;
using System;

namespace ParkingCore
{
    public class Car
    {
        public decimal Balance { get; set; }

        public Guid Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CarType CarType { get; set; }

        public void AddMoney(decimal money)
        {
            if (money > 0)
                Balance += money;
        }

        public Car()
        {
            Id = Guid.NewGuid();
        }

        public Car(decimal balance, CarType carType)
        {
            Id = Guid.NewGuid();
            Balance = balance;
            CarType = carType;
        }
    }
}