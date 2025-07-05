using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// Handles displaying the login menu and authenticating the user as they attempt to login.
    /// </summary>
    public class LoginMenu
    {
        /// <summary>
        /// A public method used to link the menu class to the user class by allowing a user to login with a valid email and password.
        /// </summary>
        /// <param name="currentMenu">
        /// Requires the current menu instance so that this method can act as a bridge for the user to restricted menu access. 
        /// The user provides proof of existing as a valid registered user, and gains access to more menu options that are relevant to their user type.
        /// </param>
        /// <param name="userAttemptingLogin">
        /// The user that is attempting to login to the system.
        /// </param>
        public void Login(Menu currentMenu, User userAttemptingLogin)
        {
            // Treat the user as anonymous initially
            CommandLineUI.DisplayMessage("Login Menu.");

            if (userAttemptingLogin._Hospital._UserList.Count == 0)
            {
                CommandLineUI.DisplayError("There are no people registered");
                return;
            }

            // If user inputs a valid email, they are a known registered user and no longer anonymous
            CommandLineUI.DisplayMessage("Please enter in your email:");
            string inputEmail = CommandLineUI.GetString();

            // Set the variable that indicates the user is not anonymous, as null until the user is deemed a registered user.
            User identifiedUser = null;

            // Search through hospital user list for a matching email.
            foreach (User user in userAttemptingLogin._Hospital._UserList)
            {
                if (inputEmail == user._Email)
                {
                    identifiedUser = user;
                    break;
                }
            }

            // If no matching email is found to the users email input.
            if (identifiedUser == null)
            {
                CommandLineUI.DisplayError("Email is not registered");
                return;
            }

            // The user is confirmed registered, therefore the password they input must match the identified user
            CommandLineUI.DisplayMessage("Please enter in your password:");
            string inputPassword = CommandLineUI.GetString();

            if (inputPassword != identifiedUser._Password)
            {
                CommandLineUI.DisplayError("Wrong Password");
                return;
            }

            CommandLineUI.DisplayMessage($"Hello {identifiedUser._Name} welcome back.");

            // If login is successful, will display a menu that is relevant to the registered users type
            if (identifiedUser != null)
            {
                currentMenu.DisplayWhichUserTypeMenu(identifiedUser);
            }
        }
    }
}
