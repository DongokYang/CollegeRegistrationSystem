using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Utility
{
    /// <summary>
    /// Numeric:  Provides functionality for working
    /// with numeric data.
    /// </summary>
    public static class Numeric
    {

        /// <summary>
        /// IsNumeric:  C# does not have a built in isnumeric method.
        /// This method is created to allow for isnumeric checks that
        /// take into consideration $ , and %.
        /// </summary>        
        public static bool IsNumeric(string val, NumberStyles NumberStyle)
        {
            Double result;
            //attempt to parse the string argument to a numeric based on numberstyle given)
            return Double.TryParse(val, NumberStyle, CultureInfo.CurrentCulture, out result);

            //usage:
            //if (isNumeric(Value, System.Globalization.NumberStyles.<T>))
            //{
            //    //code for numeric data here
            //}


        }

        /// <summary>
        /// ClearFormatting: Clears formatting characters from a string (e.g. $ or %)
        /// </summary>
        /// <param name="formatted">the formatted text (e.g. $100.00)</param>
        /// <param name="format">the character(s) to replace (e.g. $)</param>
        /// <returns></returns>
        public static string ClearFormatting(string formatted, string format)
        {
            //return string without format
            return formatted.Replace(format, "");

            //usage:
            //String formatted = "123-456-789";
            //formatted = ClearFormatting(formatted);
            //**result: formatted = 123456789
        }
    }
}
