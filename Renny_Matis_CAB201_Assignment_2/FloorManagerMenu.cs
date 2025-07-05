using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// Represents an object of the user type specific menu, for Floor manager functionality.
    /// </summary>
    public class FloorManagerMenu : UserTypeMenu
    {
        /// <summary>
        /// Displays the menu relevant to floor manager functionality, floor managers can select activities to conduct, or log out.
        /// </summary>
        /// <param name="userLoggedIn">
        /// userLoggedIn is a user that is registered and logged in. In this case, the user is confirmed to be a floor manager.
        /// </param>
        /// <param name="userType">
        /// The user's role type within the hospital, all users that are registered and logged in have a type. In this case, the user has already been confirmed to be a floor manager.
        /// </param>
        /// <returns>
        /// Returns running is true to keep the menu running, if false the menu closes.
        /// </returns>
        public override bool DisplayUserTypeMenu(User userLoggedIn, string userType)
        {
            // If user is a floor manager, allow them to conduct floor manager related behaviour.
            if (userLoggedIn is FloorManager floorManagerLoggedIn)
            {
                bool running = true;

                // Display the menu if the menu is running.
                while (running)
                {
                    // Menu spacing and menu header
                    CommandLineUI.DisplayMessage();
                    CommandLineUI.DisplayMessage($"{userType} Menu.");

                    // Floor manager menu string options.
                    const string ASSIGNROOM_STR = "Assign room to patient";
                    const string ASSIGNSURGERY_STR = "Assign surgery";
                    const string UNASSIGNROOM_STR = "Unassign room";

                    // Integer for each floor manager menu string option.
                    const int DISPLAYDETAILS_INT = GPHConstants.DISPLAYDETAILS_INT, CHANGEPW_INT = GPHConstants.CHANGEPW_INT, ASSIGNROOM_INT = 2, ASSIGNSURGERY_INT = 3, UNASSIGNROOM_INT = 4, LOGOUT_INT = 5;

                    // Display the floor manager menu with CommandLineUI.GetOption for floor manager functionality.
                    int option = CommandLineUI.GetOption(GPHConstants.MAINMENU_STR, GPHConstants.DISPLAYDETAILS_STR, GPHConstants.CHANGEPW_STR, ASSIGNROOM_STR, ASSIGNSURGERY_STR, UNASSIGNROOM_STR, GPHConstants.LOGOUT_STR);

                    // Switch cases for all floor manager functionality.
                    switch (option)
                    {
                        case DISPLAYDETAILS_INT:
                            floorManagerLoggedIn.DisplayUserDetails();
                            break;
                        case CHANGEPW_INT:
                            floorManagerLoggedIn.ChangePassword();
                            break;
                        case ASSIGNROOM_INT:
                            floorManagerLoggedIn.AssignRoom();
                            break;
                        case ASSIGNSURGERY_INT:
                            floorManagerLoggedIn.AssignSurgery();
                            break;
                        case UNASSIGNROOM_INT:
                            floorManagerLoggedIn.UnassignRoom();
                            break;
                        case LOGOUT_INT:
                            running = LogOut("Floor manager", floorManagerLoggedIn);
                            // Set running to false as LogOut method returns a boolean, which closes the floor manager menu.
                            break;
                        default:
                            break;
                    }
                }
                
            }
            // Returns false which closes the menu if user is not a floor manager.
            return false;
        }

        /// <summary>
        /// Allows the floor manager to log out.
        /// </summary>
        /// <param name="UserType">
        /// A string that displays the users type for the log out message.
        /// </param>
        /// <param name="floorManagerLoggedIn">
        /// The floor manager that is currently logged in.
        /// </param>
        /// <returns>
        /// Sets the running flag in the display menu to false, exiting the menu.
        /// </returns>
        protected override bool LogOut(string UserType, User floorManagerLoggedIn)
        {
            base.LogOut(UserType, floorManagerLoggedIn);
            return false;
        }
    }
}
