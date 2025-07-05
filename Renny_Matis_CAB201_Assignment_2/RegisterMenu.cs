using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A child class of Menu, handles displaying menu options to register a user and ensuring input register user information meets restrictions.
    /// </summary>
    public abstract class RegisterMenu : Menu
    {
        /// <summary>
        /// A virtual method that collects the base user details used by all users and the inherited classes into a tuple, to be used for registering the user for their user type.
        /// </summary>
        /// <param name="userType">
        /// A string that is used to display what user type the user is registering as.
        /// </param>
        /// Returns the tuple which stores all the base user details.
        /// <returns></returns>
        public virtual (string name, int age, string mobileNo, string email, string password) RegisterUserTypeInformation(string userType, User registeringUser)
        {
            string registerAs = $"Registering as a {userType}.";
            // initialise the age as -1, as age cannot be null - and this is an impossible age to have for any user, whereas 0 is possible for a patient.
            int age = -1;

            CommandLineUI.DisplayMessage(registerAs);
            string name = GetValidName();
            // the age restrictions are dependent on the user type
            if (userType == "patient")
            {
                age = GetValidAge(1);
            }
            else if (userType == "floor manager")
            {
                age = GetValidAge(2);
            }
            else if (userType == "surgeon")
            {
                age = GetValidAge(3);
            }
            string mobileNo = GetValidMobileNo();
            string email = GetValidEmail(registeringUser._Hospital._UserList);
            string password = GetValidPassword();

            // Return the tuple of user fields to be used in registering methods.
            return (name, age, mobileNo, email, password);
        }

        /// <summary>
        /// Prompt the user to enter a string that is valid for a name using regex. The string must contain atleast one letter, and must only consist of letters and spaces. If incorrect, will prompt the user to continuously retry while displaying an error.
        /// </summary>
        /// <returns>
        /// Returns the valid name string.
        /// </returns>
        private string GetValidName()
        {
            string name;
            bool validNameFound = false;

            // Continue the loop until a valid name is input.
            do
            {
                CommandLineUI.DisplayMessage("Please enter in your name:");
                name = CommandLineUI.GetString();

                // Checks whether user has input atleast a name string that has atleast one letter, and consists of only letters and spaces. Displays error if incorrect.
                if (!string.IsNullOrEmpty(name) && Regex.IsMatch(name, @"^(?=.*[a-zA-Z])[a-zA-Z\s]+$"))
                {
                    validNameFound = true;
                }
                else
                {
                    CommandLineUI.DisplayErrorAgain("Supplied name is invalid");
                }

            }
            while (validNameFound == false);
            // Return a valid name input.
            return name;

        }

        /// <summary>
        /// Prompt the user to enter an int that is valid for the user's age. Uses a switch case to capture the different age restrictions enforced for different user types. If incorrect, will prompt the user to continuously retry while displaying an error.
        /// </summary>
        /// <param name="registeringUserType">
        /// The int used for the switch cases that enforce age restrictions, 1 is for patient, 2 is for floor manager, and 3 is for surgeon.
        /// </param>
        /// <returns>
        /// Returns the valid age integer.
        /// </returns>
        private int GetValidAge(int registeringUserType)
        {
            int age = 0;
            bool validAgeFound = false;
            string InvalidAge = "Supplied age is invalid";

            // Continue loop until a valid age is found.
            while (validAgeFound == false)
            {
                // Error handling incase input integer value is not an integer. Displays error if not an integer.
                try
                {
                    age = CommandLineUI.GetInt("Please enter in your age:");

                    // Switch cases to check for age restrictions dependent on user type. Displays error if input integer is not within age restriction boundaries.
                    switch (registeringUserType)
                    {
                        case 1:
                            if (age >= 0 && age <= 100)
                            {
                                validAgeFound = true;
                            }
                            else
                            {
                                CommandLineUI.DisplayErrorAgain(InvalidAge);
                            }
                            break;

                        case 2:
                            if (age >= 21 && age <= 70)
                            {
                                validAgeFound = true;
                            }
                            else
                            {
                                CommandLineUI.DisplayErrorAgain(InvalidAge);
                            }
                            break;

                        case 3:
                            if (age >= 30 && age <= 75)
                            {
                                validAgeFound = true;
                            }
                            else
                            {
                                CommandLineUI.DisplayErrorAgain(InvalidAge);
                            }
                            break;

                        // Default switch case if user inputs out of bounds age for all user types.
                        default:
                            if (age < 0 || age > 100)
                            {
                                CommandLineUI.DisplayErrorAgain(InvalidAge);
                            }
                            else
                            {
                                break;
                            }
                            break;
                    }
                }
                catch (FormatException)
                {
                    CommandLineUI.DisplayErrorAgain("Supplied value is not an integer");
                }
            }
            // Return valid age input.
            return age;
        }

        /// <summary>
        /// Prompt the user to enter a string that is valid for a mobile number using regex. The string must begin with 0, be exactly 10 characters long, and can only consist of numbers. If incorrect, will prompt the user to continuously retry while displaying an error.
        /// </summary>
        /// <returns>
        /// Returns the valid mobile number string.
        /// </returns>
        private string GetValidMobileNo()
        {
            string mobileNo;
            bool validMobileNoFound = false;

            // Continue the loop until a valid mobile found number is found.
            do
            {
                CommandLineUI.DisplayMessage("Please enter in your mobile number:");
                mobileNo = CommandLineUI.GetString();

                // Checks whether the user has input a mobile number string that begins with 0, and is exactly 10 characters long of only numbers. Displays error if incorrect.
                if (!string.IsNullOrEmpty(mobileNo) && Regex.IsMatch(mobileNo, @"^0\d{9}$"))
                {
                    validMobileNoFound = true;
                }
                else
                {
                    CommandLineUI.DisplayErrorAgain("Supplied mobile number is invalid");
                }

            }
            while (validMobileNoFound == false);
            // Return a valid mobile number input.
            return mobileNo;
        }

        /// <summary>
        /// Prompt the user to enter a string that is valid for an email using regex. The email must include an "@" character and have at least one character on either side. If incorrect, will prompt the user to continuously retry while displaying an error.
        /// </summary>
        /// <param name="UserList">
        /// A restriction enforced by the program is that the email must be unique, therefore this method checks the hospital database for their user list, and ensures no other users have registered using the input email string.
        /// </param>
        /// <returns>
        /// Returns the valid email string.
        /// </returns>
        private string GetValidEmail(List<User> UserList)
        {
            string email;
            bool validEmailFound = false;

            // Continue the loop until a valid email is found.
            do
            {
                CommandLineUI.DisplayMessage("Please enter in your email:");
                email = CommandLineUI.GetString();

                // Checks whether the user has input an email string with an @ character and atleast one character on either side. Displays error if incorrect.
                if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^.{1,}@.{1,}$"))
                {
                    bool emailAlreadyRegistered = false;

                    // Check if email has already been registered in the user list parameter.
                    foreach (User registeredUsers in UserList)
                    {
                        if (registeredUsers._Email == email)
                        {
                            emailAlreadyRegistered = true;
                            break;
                        }
                    }

                    // If user email is not already on the list, input email is valid. Display error if email is already in user list.
                    if (emailAlreadyRegistered == false)
                    {
                        validEmailFound = true;
                    }
                    else
                    {
                        CommandLineUI.DisplayErrorAgain("Email is already registered");
                    }

                }
                else // Display email is invalid if doesn't suffice the regex check.
                {
                    CommandLineUI.DisplayErrorAgain("Supplied email is invalid");
                }
            }
            while (validEmailFound == false);
            // Return a valid email input.
            return email;
        }

        /// <summary>
        /// Prompt the user to enter a string that is valid for a password using regex. The password must be atleast 8 characters long, use both numbers and letters, and use mixed cases for letters. If incorrect, will prompt the user to continuously retry while displaying an error.
        /// </summary>
        /// <returns>
        /// Returns the valid password string.
        /// </returns>
        private string GetValidPassword()
        {
            string password;
            bool validPasswordFound = false;

            // Continue the loop until a valid password is found.
            do
            {
                // Checks whether the user has input a password string that is atleast 8 characters long, uses both numbers and letters, and mixed cases for letters. Displays error if incorrect.
                CommandLineUI.DisplayMessage("Please enter in your password:");
                password = CommandLineUI.GetString();

                if (!string.IsNullOrEmpty(password) && Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
                {
                    validPasswordFound = true;
                }
                else
                {
                    CommandLineUI.DisplayErrorAgain("Supplied password is invalid");
                }
            }
            while (validPasswordFound == false);
            // Return a valid password input.
            return password;
        }

    }
}
