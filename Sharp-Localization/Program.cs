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

            _language.PrintLoadedLanguageData();

            //_language.PrintAllCultures();
        }
    }
}