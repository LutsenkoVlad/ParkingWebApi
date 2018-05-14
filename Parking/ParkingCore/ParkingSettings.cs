using ParkingCore.Enums;
using ParkingCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ParkingCore
{
    /// <summary>
    /// Customizing parking data
    /// </summary>
    public class ParkingSettings : ISettings
    {
        /// <summary>
        /// Every Timeout seconds charges money for parking space
        /// </summary>
        public int Timeout { get; private set; }

        /// <summary>
        /// Prices for parking for all cars
        /// </summary>
        public Dictionary<CarType, decimal> Prices { get; private set; }

        /// <summary>
        /// Amount of parking spaces
        /// </summary>
        public int ParkingSpace { get; private set; }

        /// <summary>
        /// Coefficient of fine
        /// </summary>
        public decimal Fine { get; private set; }

        public string LogFilePath { get; set; }

        public int LogTimeout { get; set; }

        private static readonly Lazy<ParkingSettings> lazy = new Lazy<ParkingSettings>(() => new ParkingSettings());

        public static ParkingSettings Instance => lazy.Value;

        private ParkingSettings() { }


        /// <summary>
        /// Set settings for parking data
        /// </summary>
        /// <param name="prices">Prices for parking for all cars</param>
        /// <param name="parkingSpace">Amount of parking spaces</param>
        /// <param name="fine">Coefficient of fine</param>
        /// <param name="timeout">Every Timeout seconds charges money for parking space</param>
        public void SetSettings(Dictionary<CarType, decimal> prices, int parkingSpace,
                                decimal fine, string logFilePath, int timeout = 3, int logTimeout = 60)
        {
            Prices = prices;
            ParkingSpace = parkingSpace;
            Fine = fine;
            Timeout = timeout;
            LogFilePath = logFilePath;
            LogTimeout = logTimeout;
        }
    }
}