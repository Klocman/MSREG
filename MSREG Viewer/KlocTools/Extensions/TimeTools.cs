using System;

namespace Klocman.Extensions
{
    public static class TimeTools
    {
        #region Methods

        public static string DateToFuzzyTime(DateTime source)
        {
            var difference = DateTime.Now.Subtract(source);
            var years = difference.Days/365; //no leap year accounting

            if (years > 0)
            {
                if (years == 1)
                    return "Rok temu";
                return difference.Hours + " lat temu";
            }

            var months = difference.Days%365/30; //naive guess at month size

            if (months > 0)
            {
                if (months == 1)
                    return "Miesiąc temu";
                return difference.Hours + " miesięcy temu";
            }

            if (difference.Days > 0)
            {
                if (difference.Days == 1)
                    return "Wczoraj";
                return difference.Days + " dni temu";
            }

            if (difference.Hours > 0)
            {
                if (difference.Hours == 1)
                    return "Godzinę temu";
                return difference.Hours + " godzin temu";
            }

            if (difference.Minutes > 0)
            {
                if (difference.Minutes == 1)
                    return "Minutę temu";
                return difference.Minutes + " minut temu";
            }

            return "Przed chwilą";
        }

        #endregion Methods
    }
}