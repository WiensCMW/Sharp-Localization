using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sharp_Localization
{
    public class CSLanguage
    {
        // Private Variables
        private ConsolePrinter _printer;
        private string _currentCultureCode = "";
        private string _currentCultureName = "";

        // Public Variables
        public Dictionary<string, List<CSLanguageData>> CSLanguageList = new Dictionary<string, List<CSLanguageData>>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="printerTableWidth"></param>
        public CSLanguage(string cultureCode = "", int printerTableWidth = 80)
        {
            _printer = new ConsolePrinter(printerTableWidth);

            SetCultureCode(cultureCode);

            ReadDataFromCSV();
        }

        /// <summary>
        /// Returns the localized string for the passed in native string
        /// </summary>
        /// <param name="nativeString"></param>
        /// <returns></returns>
        public string GetLocalizedString(string nativeString)
        {

            // Returns the passed in native string if no localized entry was found.
            return nativeString;
        }

        /// <summary>
        /// Sets the culture code. If the passed in culture code is invalid, method returns false.
        /// </summary>
        /// <param name="cultureCode">The CultureInfo Name (code) you want to use.</param>
        /// <returns>Returns FALSE if passed in code is invalid.</returns>
        public bool SetCultureCode(string cultureCode)
        {
            var foundCulture = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(x => x.Name == cultureCode);
            if (foundCulture != null && !string.IsNullOrEmpty(foundCulture.Name))
            {
                _currentCultureCode = foundCulture.Name;
                _currentCultureName = foundCulture.DisplayName;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the culture code for the class. If it wasn't set, it returns the CultureInfo.CurrentCulture.Name
        /// </summary>
        /// <returns></returns>
        public string GetCultureCode()
        {
            if (!string.IsNullOrEmpty(_currentCultureCode))
                return _currentCultureCode;
            else
            {
                var foundCulture = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(x => x.Name == _currentCultureCode);
                if (foundCulture != null && !string.IsNullOrEmpty(foundCulture.Name))
                {
                    return foundCulture.Name;
                }

                return CultureInfo.CurrentCulture.Name;
            }
        }

        /// <summary>
        /// Returns the culture display name for the class. If it wasn't set, it returns the CultureInfo.CurrentCulture.DisplayName
        /// </summary>
        /// <returns></returns>
        public string GetCultureName()
        {
            if (!string.IsNullOrEmpty(_currentCultureName))
                return _currentCultureName;
            else
            {
                var foundCulture = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(x => x.Name == _currentCultureName);
                if (foundCulture != null && !string.IsNullOrEmpty(foundCulture.Name))
                {
                    return foundCulture.DisplayName;
                }

                return CultureInfo.CurrentCulture.DisplayName;
            }
        }

        /// <summary>
        /// Reads the language data from the local CSV file and parses results into local dictionary
        /// </summary>
        /// <returns>Returns FALSE if loading failed. </returns>
        private bool ReadDataFromCSV()
        {
            // Get path of csv data file
            string dataFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Path.Combine("Data", "Sharp-Localization-Data.csv"));

            // Check file exists
            if (!File.Exists(dataFilePath))
            {
                return false;
            }

            try
            {
                // Read data from CSV file
                using (CsvFileReader reader = new CsvFileReader(dataFilePath))
                {
                    CsvRow csvRow = new CsvRow();
                    List<string> hdrRow = new List<string>();

                    while (reader.ReadRow(csvRow))
                    {
                        // Read the header row into list
                        if (hdrRow.Count == 0)
                        {
                            foreach (string s in csvRow)
                                hdrRow.Add(s);
                        }
                        else
                        {
                            List<CSLanguageData> parsedLanguageList = new List<CSLanguageData>();
                            string nativeLanguageKey = "";

                            // Read row elements
                            for (int i = 0; i < csvRow.Count; i++)
                            {
                                /* Row element 0 is always the row's native language value, so we read that into a string which
                                 * will become the Dictionary's key value. */
                                if (i == 0)
                                    nativeLanguageKey = csvRow[i];

                                /* Read row element into CSLanguageData List */
                                parsedLanguageList.Add(new CSLanguageData(hdrRow[i], csvRow[i]));
                            }

                            // Add row's Name and values into dictionary
                            CSLanguageList.Add(nativeLanguageKey, parsedLanguageList);
                        }
                    }
                }

                // Return TRUE if elements were loaded into dictoinary
                return CSLanguageList.Count > 0;
            }
            catch (Exception)
            {

            }

            return false;
        }

        /// <summary>
        /// Prints the CSLanguage dictionary keys and values
        /// </summary>
        public void PrintLoadedLanguageData()
        {
            // Print loaded values
            _printer.PrintLine();
            List<string> hdr = new List<string>();
            foreach (KeyValuePair<string, List<CSLanguageData>> item in CSLanguageList)
            {
                hdr.Add("Dictionary Key");
                for (int i = 0; i < item.Value.Count; i++)
                {
                    hdr.Add(item.Value[i].CultureCode);
                }
                break;
            }
            _printer.PrintRow(hdr.ToArray());
            _printer.PrintLine();

            foreach (KeyValuePair<string, List<CSLanguageData>> item in CSLanguageList)
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
        /// Prints all system cultures to console
        /// </summary>
        public void PrintAllCultures()
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

    /// <summary>
    /// Class to hold the language culture code and translated value
    /// </summary>
    public class CSLanguageData
    {
        // https://docs.microsoft.com/en-us/bingmaps/rest-services/common-parameters-and-types/supported-culture-codes
        public string CultureCode;
        public string Value;

        public CSLanguageData(string cultureCode, string value)
        {
            CultureCode = cultureCode;
            Value = value;
        }
    }

    public class ConsolePrinter
    {
        public int TableWidth { get; set; }

        public ConsolePrinter(int tableWidth)
        {
            TableWidth = tableWidth;
        }

        /// <summary>
        /// Prints single line in console at the length of the table width
        /// </summary>
        public void PrintLine()
        {
            Console.WriteLine(new string('-', TableWidth));
        }

        /// <summary>
        /// Prints the values of the passed in string array as row values
        /// </summary>
        /// <param name="columns">The elements you want to print</param>
        public void PrintRow(params string[] columns)
        {
            int width = (TableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        /// <summary>
        /// Pads the passed in string value with spaces to center value in table column
        /// </summary>
        /// <param name="text">The text you want to align to center</param>
        /// <param name="width">The column width</param>
        /// <returns></returns>
        private string AlignCentre(string text, int width)
        {
            text = (text.Length > width) ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
