namespace Stocks.Application.Common
{
    public static class PriceFormatter
    {
        public static string FormatPrice(int price)
        {
            // 250000 -> Rs. 2.5 Lakh
            string suffix = "";
            double formattedNumber = price;

            if (price >= 10000000) // Crores
            {
                formattedNumber = price / 10000000;
                suffix = "Crores";
            }
            else if (price >= 100000) // Lakhs
            {
                formattedNumber = price / 100000;
                suffix = "Lakhs";
            }
            else if (price >= 1000) // Thousands
            {
                formattedNumber = price / 1000;
                suffix = "Thousand";
            }

            //1.123213.... -> 1.1
            if (formattedNumber != (int)formattedNumber)
            {
                formattedNumber = Math.Round(formattedNumber, 1);
            }

            return $"Rs. {formattedNumber} {suffix}";

        }
    }
}