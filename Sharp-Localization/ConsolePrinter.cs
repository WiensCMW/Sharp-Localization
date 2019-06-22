using System;

namespace Sharp_Localization
{
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
