using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// Represents an object of the user type specific menu, for Patient functionality.
    /// </summary>
    public class PatientMenu : UserTypeMenu
    {
        /// <summary>
        /// Displays the menu relevant to patient functionality, patients can select activities to conduct, or log out.
        /// </summary>
        /// <param name="userLoggedIn">
        /// userLoggedIn is a user that is registered and logged in. In this case, the user is confirmed to be a patient.
        /// </param>
        /// <param name="userType">
        /// The user's role type within the hospital, all users that are registered and logged in have a type. In this case, the user has already been confirmed to be a patient.
        /// </param>
        /// <returns>
        /// Returns running is true to keep the menu running, if false the menu closes.
        /// </returns>
        public override bool DisplayUserTypeMenu(User userLoggedIn, string userType)
        {
            // If user is a patient, allow them to conduct patient related behaviour.
            if (userLoggedIn is Patient patientLoggedIn)
            {
                bool running = true;

                // Display the menu if the menu is running.
                while (running)
                {
                    // Determine the menu string dependent on if the patient is checked in or out.
                    string CHECKEDINOUT_STR = DisplayAccurateCheckInOut(patientLoggedIn);

                    // Menu spacing and menu header.
                    CommandLineUI.DisplayMessage();
                    CommandLineUI.DisplayMessage($"{userType} Menu.");

                    // Patient menu string options
                    const string SEEROOM_STR = "See room";
                    const string SEESURGEON_STR = "See surgeon";
                    const string SEESURGERY_STR = "See surgery date and time";

                    // Integer for each patient menu string option.
                    const int DISPLAYDETAILS_INT = GPHConstants.DISPLAYDETAILS_INT, CHANGEPW_INT = GPHConstants.CHANGEPW_INT, CHECKEDINOUT_INT = 2, SEEROOM_INT = 3, SEESURGEON_INT = 4, SEESURGERY_INT = 5, LOGOUT_INT = 6;

                    // Display the patient menu with CommandLineUI.GetOption for patient functionality.
                    int option = CommandLineUI.GetOption(GPHConstants.MAINMENU_STR, GPHConstants.DISPLAYDETAILS_STR, GPHConstants.CHANGEPW_STR, CHECKEDINOUT_STR, SEEROOM_STR, SEESURGEON_STR, SEESURGERY_STR, GPHConstants.LOGOUT_STR);

                    // Switch cases for all patient functionality.
                    switch (option)
                    {
                        case DISPLAYDETAILS_INT:
                            patientLoggedIn.DisplayUserDetails();
                            break;
                        case CHANGEPW_INT:
                            patientLoggedIn.ChangePassword();
                            break;
                        case CHECKEDINOUT_INT:
                            patientLoggedIn.PatientCheckInOut();
                            break;
                        case SEEROOM_INT:
                            patientLoggedIn.SeePatientRoomNo();
                            break;
                        case SEESURGEON_INT:
                            patientLoggedIn.SeeAssignedSurgeon();
                            break;
                        case SEESURGERY_INT:
                            patientLoggedIn.SeeSurgeryDateTime();
                            break;
                        case LOGOUT_INT:
                            // Set running to false as Logout method returns a boolean, which closes the patient menu.
                            running = LogOut("Patient", patientLoggedIn);
                            break;
                        default:
                            break;
                    }
                }
            }
            // Returns false which closes the menu if user is not a patient.
            return false;
        }

        /// <summary>
        /// Allows the patient to log out.
        /// </summary>
        /// <param name="UserType">
        /// A string that displays the users type for the log out message.
        /// </param>
        /// <param name="patientLoggedIn">
        /// The patient that is logged in currently.
        /// </param>
        /// <returns>
        /// Sets the running flag in the display menu to false, exiting the menu.
        /// </returns>
        protected override bool LogOut(string UserType, User patientLoggedIn)
        {
            base.LogOut(UserType, patientLoggedIn);
            return false;
        }

        /// <summary>
        /// Displays to the patient whether they can check in or out based on their CheckedIn status.
        /// </summary>
        /// <param name="patientLoggedIn"></param>
        /// <returns>
        /// Returns the relevant string to display to the patient menu about whether they can check in or out.
        /// </returns>
        private string DisplayAccurateCheckInOut(Patient patientLoggedIn)
        {
            string CHECKEDINOUT_STR;

            if (patientLoggedIn._CheckedIn == false)
            {
                CHECKEDINOUT_STR = "Check in";
            }
            else
            {
                CHECKEDINOUT_STR = "Check out";
            }
            return CHECKEDINOUT_STR;
        }

    }
}
