using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    static class CommandLineUI
    {
        /// <summary>
        /// Writes an empty line to the command line.
        /// </summary>
        public static void DisplayMessage()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Overload the DisplayMessage method and write a string to the command line.
        /// </summary>
        /// <param name="message">
        /// String to write.
        /// </param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Write a message to the command line, surrounded by an identifying error message.
        /// </summary>
        /// <param name="message">
        /// String to write.
        /// </param>
        public static void DisplayError(string message)
        {
            Console.WriteLine("#####");
            Console.WriteLine($"#Error - {message}.");
            Console.WriteLine("#####");
        }

        /// <summary>
        /// Write a message to the command line, surrounded by an identifying error message. Prompt the user to try again.
        /// </summary>
        /// <param name="message">
        /// String to write.
        /// </param>
        public static void DisplayErrorAgain(string message)
        {
            Console.WriteLine("#####");
            Console.WriteLine($"#Error - {message}, please try again.");
            Console.WriteLine("#####");
        }

        /// <summary>
        /// Retrieve an input string from the user.
        /// </summary>
        /// <returns>
        /// Returns the input string.
        /// </returns>
        public static string GetString()
        {
            // Error handling incase string is null or empty
            try
            {
                string input = Console.ReadLine();

                // If string is null or empty, throw an exception.
                if (string.IsNullOrEmpty(input))
                {
                    throw new ArgumentNullException("Input cannot be null or empty");
                }
                // If string is not null or empty, return the input string.
                return input;
            }
            catch (ArgumentNullException)
            {
                // Return an empty string if input null or empty excetion was thrown.
                return string.Empty;
            }
        }

        /// <summary>
        /// Retrieve an input int from the user.
        /// </summary>
        /// <returns>
        /// Returns the input int if valid.
        /// </returns>
        /// <exception cref="FormatException">
        /// If user inputs a non-integer value, throw an exception that the input is not an integer.
        /// </exception>
        public static int GetInt()
        {
            int i;
            string input = Console.ReadLine();
            bool parsed = int.TryParse(input, out i);

            // If parsing fails and is false, throw an exception.
            if (parsed == false)
            {
                throw new FormatException("Supplied value is not an integer");
            }

            // Return integer value if parsed is true.
            return i;
        }

        /// <summary>
        /// Retrieve an input int from the user and write a string message to the command line.
        /// </summary>
        /// <param name="message">
        /// String to write.
        /// </param>
        /// <returns>
        /// Returns the input integer if valid.
        /// </returns>
        /// <exception cref="FormatException">
        /// If user inputs a non-integer value, throw an exception that the input is not an integer.
        /// </exception>
        public static int GetInt(string message)
        {
            int i;
            string input;
            bool parsed;

            // While input integer is not parsed, continuously prompt the string message for an integer input.
            do
            {
                Console.WriteLine($"{message}");
                input = Console.ReadLine();
                parsed = int.TryParse(input, out i);

                // If parsing is failing, throw an exception.
                if (parsed == false)
                {
                    throw new FormatException("Supplied value is not an integer");
                }
            }
            while (parsed == false);

            // Return integer if the loop is escaped, which means parsed == true.
            return i;
        }

        /// <summary>
        /// Sets up a vertical numbered list where the user can select one of the options by inputting an integer. 
        /// </summary>
        /// <param name="title">
        /// Displays the title of the options.
        /// </param>
        /// <param name="options">
        /// Takes a collection of options to display to the user in a vertical numbered list.
        /// </param>
        /// <returns>
        /// Returns the integer option selected by the user to utilise the selected option, -1 as collections count from 0.
        /// </returns>
        public static int GetOption(string title, params object[] options)
        {
            // Check if any parameters are supplied
            if (options.Length <= 0)
            {
                return -1;
            }

            // Display the title of the options
            Console.WriteLine(title);
            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Length)));
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)}. {options[i]}");
            }

            // Enter a choice between 1 and the amount of parameters.
            int option = GetInt($"Please enter a choice between 1 and {options.Length}.");

            // -1 as collections count from 0.
            return option - 1;
        }

        /// <summary>
        /// Retrieve a DateTime from the user.
        /// </summary>
        /// <returns>
        /// Return the input DateTime.
        /// </returns>
        /// <exception cref="FormatException">
        /// If user inputs a non-correctly formatted DateTime value, throw an exception that the input is not a DateTime in the correct format.
        /// </exception>
        public static DateTime GetDateTime()
        {
            string input = Console.ReadLine();
            DateTime result;
            string format = GPHConstants.DATETIMEFORMAT; // Expected format is "HH:mm dd/MM/yyyy"
            bool dtWorked = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            // If boolean indicating DateTime has been parsed correctly is false, throw an exception indicating the DateTime formatting is incorrect.
            if (dtWorked == false)
            {
                throw new FormatException("Supplied value is not a valid DateTime");
            }
            // Return the DateTime if the DateTime is valid.
            return result;
        }
    }
}
