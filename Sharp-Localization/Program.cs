using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Sharp_Localization
{
    class Program
    {
        #region Private Variables
        private static ConsolePrinter _printer = new ConsolePrinter(77);
        private static CSLanguage _language;

        private static string _selectedCultureCode = "";
        private static string _selectedCultureName = "";
        #endregion

        #region Public Variables
        public static string SelectedCultureCode { get { return _selectedCultureCode; } }
        public static string SelectedCultureName { get { return _selectedCultureName; } }
        #endregion

        static void Main(string[] args)
        {
            _language = new CSLanguage();

            while (true)
            {
                Console.Clear();

                _printer.PrintLine();
                Console.WriteLine($"Sharp-Localization App");
                Console.WriteLine($"Version: 1.0.0");

                // Display the name of the current thread culture.
                Console.WriteLine("CurrentCulture is {0} ({1}).",
                    CurrentCultureCode(),
                    CurrentCultureName());

                // Print available commands:
                Console.WriteLine("Commands:");
                Console.WriteLine("change\tChange Language");
                Console.WriteLine("print\tPrints list of Localized Strings");
                Console.WriteLine("print c\tPrints list of system cultures");
                Console.WriteLine("exit\tExits App");

                _printer.PrintLine();
                Console.WriteLine();

                //_language.PrintLoadedLanguageData();
                //_language.PrintAllCultures();

                #region Get use input
                Console.Write("Input:");
                string input = Console.ReadLine();
                #endregion

                #region Exit app if called for
                if (ExitApp(input))
                {
                    Console.WriteLine("Exiting App...");
                    break;
                }
                #endregion

                #region Process commands
                ProcessCMD(input);
                #endregion
            }
        }

        private static bool ExitApp(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.ToLower() == "exit")
                    return true;
            }

            return false;
        }

        private static void ProcessCMD(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            switch (input)
            {
                case "change":
                    {
                        while (true)
                        {
                            if (ChangeLanguage())
                                break;
                        }
                        break;
                    }
                case "print":
                    {
                        _language.PrintLoadedLanguageData();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                case "print c":
                    {
                        _language.PrintAllCultures();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                default:
                    break;
            }
        }

        private static string CurrentCultureCode()
        {
            return (!string.IsNullOrEmpty(_selectedCultureCode)) ? _selectedCultureCode : CultureInfo.CurrentCulture.Name;
        }

        private static string CurrentCultureName()
        {
            return (!string.IsNullOrEmpty(_selectedCultureCode)) ? _selectedCultureName : CultureInfo.CurrentCulture.DisplayName;
        }

        private static bool ChangeLanguage()
        {
            Console.WriteLine();
            Console.Write("Enter culture code:");
            string inputCode = Console.ReadLine();

            // Get all cultures and loop through them.
            CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var foundCulture = allCultures.FirstOrDefault(x => x.Name == inputCode);
            if (foundCulture != null)
            {
                Console.WriteLine($"Culture changed to {foundCulture.Name}");
                _selectedCultureCode = foundCulture.Name;
                _selectedCultureName = foundCulture.DisplayName;
                return true;
            }

            return false;
        }
    }
}