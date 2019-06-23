using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sharp_Localization
{
    class Program
    {
        private static ConsolePrinter _printer = new ConsolePrinter(100);
        private static CSLanguage _language;

        static void Main(string[] args)
        {
            _language = new CSLanguage();

            //PrintAllCultures();

            // Print loaded values
            _printer.PrintLine();
            List<string> hdr = new List<string>();
            foreach (KeyValuePair<string, List<CSLanguageData>> item in _language.CSLanguageList)
            {
                hdr.Add("Name");
                for (int i = 0; i < item.Value.Count; i++)
                {
                    hdr.Add(item.Value[i].CultureCode);
                }
                break;
            }
            _printer.PrintRow(hdr.ToArray());
            _printer.PrintLine();

            foreach (KeyValuePair<string, List<CSLanguageData>> item in _language.CSLanguageList)
            {
                List<string> langValues = new List<string>();
                langValues.Add(item.Key);

                for (int i = 0; i < item.Value.Count; i++)
                {
                    langValues.Add(item.Value[i].Value.Trim());
                }

                _printer.PrintRow(langValues.ToArray());
            }
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