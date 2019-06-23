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
                #region Print Header to console
                Console.Clear();

                _printer.PrintLine();
                Console.WriteLine($"Sharp-Localization App");
                Console.WriteLine($"Version: 1.0.0");

                // Display the name of the current thread culture.
                Console.WriteLine("CurrentCulture is {0} ({1}).",
                    _language.GetCultureCode(),
                    _language.GetCultureName());

                // Print available commands:
                Console.WriteLine("Commands:");
                Console.WriteLine("change\tChange Language");
                Console.WriteLine("print\tPrints list of Localized Strings");
                Console.WriteLine("print c\tPrints list of system cultures");
                Console.WriteLine("exit\tExits App");

                _printer.PrintLine();
                Console.WriteLine();
                #endregion

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

        /// <summary>
        /// Check if input calls for exit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ExitApp(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.ToLower() == "exit")
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check the input for valid commands and process them
        /// </summary>
        /// <param name="input"></param>
        private static void ProcessCMD(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            switch (input.ToLower())
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
                case "loc":
                    {
                        while (true)
                        {
                            Console.WriteLine("Enter word to localize (Enter cancel to cancel and go back):");
                            string inputStr = Console.ReadLine();
                            if (!string.IsNullOrEmpty(inputStr) && inputStr.ToLower() == "cancel")
                                break;
                            else
                            {
                                string localized = _language.GetLocalizedString(inputStr);
                                Console.WriteLine($"Localized String: {localized}");
                                Console.ReadKey();
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private static bool ChangeLanguage()
        {
            Console.WriteLine();
            Console.WriteLine("Enter culture code:");
            string input = Console.ReadLine();

            // Check if user commanded to cancel
            if (!string.IsNullOrEmpty(input) && input.ToLower() == "cancel")
                return true;

            if (!_language.SetCultureCode(input))
            {
                Console.WriteLine("Invalid culture code. (Enter cancel to cancel and go back.)");
                return false;
            }
            else
                return true;
        }
    }
}