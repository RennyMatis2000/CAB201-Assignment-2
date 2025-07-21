using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A set of strings that can be used throughout the program
    /// </summary>
    public static class GPHConstants
    {
        // The format for the date and time strings
        public const string DATETIMEFORMAT = "HH:mm dd/MM/yyyy";
        // If the option selected by the user is invalid
        public const string INVALIDMENU = "Invalid Menu Option";
        public const string MAINMENU_STR = "Please choose from the menu below:";
        public const string DISPLAYDETAILS_STR = "Display my details";
        public const string CHANGEPW_STR = "Change password";
        public const string LOGOUT_STR = "Log out";
        // The int that is used to select display details on the type specific menu
        public const int DISPLAYDETAILS_INT = 0;
        // The int that is used to select display details on the type specific menu
        public const int CHANGEPW_INT = 1;
    }
}
