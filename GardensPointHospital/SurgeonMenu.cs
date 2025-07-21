using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// Represents an object of the user type specific menu, for Surgeon functionality.
    /// </summary>
    public class SurgeonMenu : UserTypeMenu
    {
        /// <summary>
        /// Displays the menu relevant to surgeon functionality, surgeons can select activities to conduct, or log out.
        /// </summary>
        /// <param name="userLoggedIn">
        /// userLoggedIn is a user that is registered and logged in. In this case, the user is confirmed to be a surgeon.
        /// </param>
        /// <param name="userType">
        /// The user's role type within the hospital, all users that are registered and logged in have a type. In this case, the user has already been confirmed to be a surgeon.
        /// </param>
        /// <returns>
        /// Returns running is true to keep the menu running, if false the menu closes.
        /// </returns>
        public override bool DisplayUserTypeMenu(User userLoggedIn, string userType)
        {
            // If user is a surgeon, allow them to conduct surgeon related behaviour.
            if (userLoggedIn is Surgeon surgeonLoggedIn)
            {
                bool running = true;

                // Display the menu if the menu is running.
                while (running)
                {
                    // Menu spacing and menu header.
                    CommandLineUI.DisplayMessage();
                    CommandLineUI.DisplayMessage($"{userType} Menu.");

                    // Surgeon menu string options.
                    const string PATIENTLIST_STR = "See your list of patients";
                    const string SEESCHEDULE_STR = "See your schedule";
                    const string PERFORMSURGERY_STR = "Perform surgery";

                    // Integer for each surgeon menu string option.
                    const int DISPLAYDETAILS_INT = GPHConstants.DISPLAYDETAILS_INT, CHANGEPW_INT = GPHConstants.CHANGEPW_INT, PATIENTLIST_INT = 2, SEESCHEDULE_INT = 3, PERFORMSURGERY_INT = 4, LOGOUT_INT = 5;

                    // Display the surgeon menu with CommandLineUI.GetOption for surgeon functionality.
                    int option = CommandLineUI.GetOption(GPHConstants.MAINMENU_STR, GPHConstants.DISPLAYDETAILS_STR, GPHConstants.CHANGEPW_STR, PATIENTLIST_STR, SEESCHEDULE_STR, PERFORMSURGERY_STR, GPHConstants.LOGOUT_STR);

                    // Switch cases for all surgeon functionality.
                    switch (option)
                    {
                        case DISPLAYDETAILS_INT:
                            surgeonLoggedIn.DisplayUserDetails();
                            break;
                        case CHANGEPW_INT:
                            surgeonLoggedIn.ChangePassword();
                            break;
                        case PATIENTLIST_INT:
                            surgeonLoggedIn.ViewPatients();
                            break;
                        case SEESCHEDULE_INT:
                            surgeonLoggedIn.ViewSchedule();
                            break;
                        case PERFORMSURGERY_INT:
                            surgeonLoggedIn.PerformSurgery();
                            break;
                        case LOGOUT_INT:
                            running = LogOut("Surgeon", surgeonLoggedIn);
                            // Set running to false as LogOut method returns a boolean, which closes the surgeon menu.
                            break;
                        default:
                            break;
                    }
                }
            }
            // Returns false which closes the menu if the user is not a surgeon.
            return false;
        }

        /// <summary>
        /// Allows the surgeon to log out.
        /// </summary>
        /// <param name="UserType">
        /// A string that displays the users type for the log out message.
        /// </param>
        /// <param name="surgeonLoggedIn">
        /// The surgeon that is currently logged in.
        /// </param>
        /// <returns>
        /// Sets the running flag in the display menu to false, exiting the menu.
        /// </returns>
        protected override bool LogOut(string UserType, User surgeonLoggedIn)
        {
            base.LogOut(UserType, surgeonLoggedIn);
            return false;
        }
    }
    
}
