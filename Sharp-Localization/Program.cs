using System;
using System.Globalization;
using System.Text;

namespace Sharp_Localization
{
    class Program
    {
        private static ConsolePrinter _printer = new ConsolePrinter(100);
        static void Main(string[] args)
        {
            PrintAllCultures();
        }

        /// <summary>
        /// Prints all cultures to console
        /// </summary>
        private static void PrintAllCultures()
        {
            // Print table header
            _printer.PrintLine();
            _printer.PrintRow(new string[] { "Code", "English Name", "Type" });
            _printer.PrintLine();

            // Get all cultures and loop through them.
            var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (var ci in allCultures)
            {
                // Get the culture type.
                string cultureType = "";
                if (ci.CultureTypes.HasFlag(CultureTypes.NeutralCultures))
                    cultureType = "NeutralCulture";
                else if (ci.CultureTypes.HasFlag(CultureTypes.SpecificCultures))
                    cultureType = " SpecificCulture";

                // Print the code, name and type to console. (Name is the code, EnglishName the desc)
                _printer.PrintRow(new string[] {
                    ci.Name,
                    ci.EnglishName,
                    cultureType
                });
            }
            _printer.PrintLine();
        }
    }
}

/*
https://docs.microsoft.com/en-us/bingmaps/rest-services/common-parameters-and-types/supported-culture-codes
 */
