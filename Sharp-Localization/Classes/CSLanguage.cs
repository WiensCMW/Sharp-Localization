using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sharp_Localization
{
    public class CSLanguage
    {
        public Dictionary<string, List<CSLanguageData>> CSLanguageList = new Dictionary<string, List<CSLanguageData>>();
        
        public CSLanguage()
        {
            ReadDataFromCSV();
        }

        /// <summary>
        /// Reads the language data from the local CSV file.
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
                    CsvRow row = new CsvRow();
                    bool hdrRead = false;
                    List<string> hdrRow = new List<string>();

                    while (reader.ReadRow(row))
                    {
                        // Read the header row into list
                        if (!hdrRead)
                        {
                            foreach (string s in row)
                                hdrRow.Add(s);

                            hdrRead = true;
                        }
                        else
                        {
                            List<CSLanguageData> langData = new List<CSLanguageData>();
                            string rowName = "";

                            // Read row elements
                            for (int i = 0; i < row.Count; i++)
                            {
                                /* Row element 0 is always the row's Name, so we read that into a string which
                                 * will become the Dictionary's key value. */
                                if (i == 0)
                                    rowName = row[i];
                                else
                                {
                                    /* Read row element into CSLanguageData List */
                                    langData.Add(new CSLanguageData(hdrRow[i], row[i]));
                                }
                            }

                            // Add row's Name and values into dictionary
                            CSLanguageList.Add(rowName, langData);
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
    }
}
