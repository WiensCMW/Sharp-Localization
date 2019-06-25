using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Sharp_Localization
{
    class Program
    {
        // Private Variables
        private static ConsolePrinter _printer = new ConsolePrinter(77);
        private static CSLanguage _lang;

        static void Main(string[] args)
        {
            // Instantiate CSLanguage class
            _lang = new CSLanguage();

            // Load localization data
            _lang.LoadLocalizationData();

            while (true)
            {
                #region Print Header to console
                Console.Clear();

                _printer.PrintLine();
                Console.WriteLine($"Sharp-Localization {_lang.GetLocalizedString("App")}");
                Console.WriteLine($"{_lang.GetLocalizedString("Version")}: 1.0.0");

                // Display the name of the current thread culture.
                Console.WriteLine($"{_lang.GetLocalizedString("Current Culture is")} " +
                    $"{_lang.GetCultureCode()} ({_lang.GetCultureName()}).");

                // Print available commands:
                Console.WriteLine($"{_lang.GetLocalizedString("Commands")}:");
                Console.WriteLine($"change\t{_lang.GetLocalizedString("Change Language")}");
                Console.WriteLine($"print\t{_lang.GetLocalizedString("Prints list of Localized Strings")}");
                Console.WriteLine($"print c\t{_lang.GetLocalizedString("Prints list of system cultures")}");
                Console.WriteLine($"test\t{_lang.GetLocalizedString("Simulate outside error message")}");
                Console.WriteLine($"exit\t{_lang.GetLocalizedString("Exits App")}");

                _printer.PrintLine();
                #endregion

                #region Get use input
                Console.Write($"{_lang.GetLocalizedString("Input")}:");
                string input = Console.ReadLine();
                #endregion

                #region Exit app if called for
                if (ExitApp(input))
                {
                    Console.WriteLine($"{_lang.GetLocalizedString("Exiting App")}...");
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
                        _lang.PrintLoadedLanguageData();
                        Console.WriteLine($"{_lang.GetLocalizedString("Press any key to continue")}...");
                        Console.ReadKey();
                        break;
                    }
                case "print c":
                    {
                        _lang.PrintAllCultures();
                        Console.WriteLine($"{_lang.GetLocalizedString("Press any key to continue")}...");
                        Console.ReadKey();
                        break;
                    }
                case "test":
                    {
                        string sqlException = "Unable to delete Devices that contain Inventory\r\n Rows Affected: 30";
                        Console.WriteLine($"{_lang.ReplaceLocalizedString("Unable to delete Devices that contain Inventory", sqlException)}");
                        Console.WriteLine($"{_lang.GetLocalizedString("Press any key to continue")}...");
                        Console.ReadKey();
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Command to change the selected language.
        /// </summary>
        /// <returns>Returns TRUE if the entered culture code was valid.</returns>
        private static bool ChangeLanguage()
        {
            // Get new culture code from user
            Console.Write($"{_lang.GetLocalizedString("Enter culture code")}:");
            string input = Console.ReadLine();

            // Check if user commanded to cancel
            if (!string.IsNullOrEmpty(input) && input.ToLower() == "cancel")
                return true;

            // Attempt to change culture code, if entered code was invalid alert the user
            if (!_lang.SetCultureCode(input))
            {
                Console.WriteLine($"{_lang.GetLocalizedString("Invalid culture code.")} " +
                    $"({_lang.GetLocalizedString("To go back enter")} cancel)");
                Console.WriteLine();
                return false;
            }
            else
                return true;
        }
    }
}