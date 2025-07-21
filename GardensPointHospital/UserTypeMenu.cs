using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A base class for all user type menus, abstract because a user type menu does not exist, rather the subclasses type menus are objects.
    /// </summary>
    public abstract class UserTypeMenu : Menu
    {
        /// <summary>
        /// Displays the menu specific to the user type, allowing the user to select options for their relevant user type.
        /// </summary>
        /// <param name="userLoggedIn">
        /// userLoggedIn is a user that is registered and logged in.
        /// </param>
        /// <param name="userType">
        /// The user's role type within the hospital, all users that are registered and logged in have a type.
        /// </param>
        /// <returns>
        /// Returns running is true to keep the menu running, if false the menu closes.
        /// </returns>
        public abstract bool DisplayUserTypeMenu(User userLoggedIn, string userType);

        /// <summary>
        /// A virtual method that allows the user to log out.
        /// </summary>
        /// <param name="UserType">
        /// A string that displays the users type for the log out message.
        /// </param>
        /// <param name="userLoggedIn">
        /// The user that is currently logged in.
        /// </param>
        /// <returns>
        /// Sets the running flag in the display menu to false, exiting the menu.
        /// </returns>
        protected virtual bool LogOut(string UserType, User userLoggedIn)
        {
            CommandLineUI.DisplayMessage($"{UserType} {userLoggedIn._Name} has logged out.");
            return false; // Returns false for the user type menu, closing their user type menu access.
        }
    }
}

