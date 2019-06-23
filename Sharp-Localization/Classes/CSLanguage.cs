using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Sharp_Localization
{
    public class CSLanguage
    {
        private Dictionary<string, List<CSLanguage>> CSLanguageList = new Dictionary<string, List<CSLanguage>>();
        private ConsolePrinter _printer = new ConsolePrinter(100);

        public CSLanguage()
        {
            string dataFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Path.Combine("Data", "Sharp-Localization-Data.csv"));

            // Read data from CSV file
            using (CsvFileReader reader = new CsvFileReader(dataFilePath))
            {
                CsvRow row = new CsvRow();
                bool hdrPrinted = false;
                while (reader.ReadRow(row))
                {
                    if (!hdrPrinted)
                    {
                        _printer.PrintLine();
                        _printer.PrintRow(row.ToArray());
                        _printer.PrintLine();
                        hdrPrinted = true;
                    }
                    else
                        _printer.PrintRow(row.ToArray());
                }
            }
        }
    }
}
