using System;
using System.Globalization;

namespace Tester
{
    internal class Program
    {
        public enum RegulationResult
        {
            DoNothing = 0,
            Disable = 1,
            AboveSetting = 2,
            AboveSettingAlarm = 3,
            BelowSetting = 4,
            BelowSettingAlarm = 5
        }

        private static readonly CultureInfo ParseCultureInfo = new CultureInfo("en-US");

        private static void Main()
        {
            OnMeasurementReceived("T013.7 H072.7 G4 N2");
            Console.ReadKey();
        }

        private static void OnMeasurementReceived(string reply)
        {
            var replyParts = reply.Split(new[] {' ', '_'}, StringSplitOptions.RemoveEmptyEntries);
            double t = -1000, h = -1000;
            RegulationResult tr = (RegulationResult) 1000, hr = (RegulationResult) 1000;
            foreach (var part in replyParts)
            {
                switch (part[0])
                {
                    case 'T':
                        t = double.Parse(part.Substring(1), ParseCultureInfo);
                        break;
                    case 'H':
                        h = double.Parse(part.Substring(1), ParseCultureInfo);
                        break;
                    case 'G':
                        tr = (RegulationResult) int.Parse(part.Substring(1), ParseCultureInfo);
                        break;
                    case 'N':
                        hr = (RegulationResult) int.Parse(part.Substring(1), ParseCultureInfo);
                        break;
                }
            }

            var newMeasurement = new Msr33Measurement(t, h, tr, hr);
            Console.WriteLine(newMeasurement.ToString());
        }

        public class Msr33Measurement
        {
            #region Constructors

            public Msr33Measurement(double temp, double hum, RegulationResult tempReg, RegulationResult humReg)
            {
                Temperature = temp;
                Humidity = hum;
                TemperatureRegulationResult = tempReg;
                HumidityRegulationResult = humReg;
            }

            #endregion Constructors

            #region Properties

            public double Humidity { get; set; }

            public RegulationResult HumidityRegulationResult { get; set; }

            public bool IsValid => Temperature > -80 && Temperature < 200
                                   && Humidity >= 0 && Humidity <= 100
                                   && TemperatureRegulationResult >= 0 &&
                                   TemperatureRegulationResult < (RegulationResult) 200
                                   && HumidityRegulationResult >= 0 && HumidityRegulationResult < (RegulationResult) 200
                ;

            public double Temperature { get; set; }

            public RegulationResult TemperatureRegulationResult { get; set; }

            #endregion Properties
        }
    }
}