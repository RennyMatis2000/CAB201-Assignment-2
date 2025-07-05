using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// Menu class displays all menu systems to the user, handling the flow of using the program as a user. An object of menu class is what causese the program to operate.
    /// </summary>
    public class Menu
    {
        private Hospital GardensPointHospital = new Hospital();

        /// <summary>
        /// Displays the menu header text
        /// </summary>
        private void DisplayMenuHeader()
        {
            const string MenuHeaderText = "=================================\r\nWelcome to Gardens Point Hospital\r\n=================================";
            CommandLineUI.DisplayMessage(MenuHeaderText);
        }

        /// <summary>
        /// Keeps the program operating if running is true, if running is false the program will no longer display any menu.
        /// </summary>
        public void Run()
        {
            DisplayMenuHeader();
            bool running = true;
            // Menu will continuously display unless user sets running to false using the exit menu option.
            while (running)
            {
                running = DisplayMainMenu();
            }
        }

        /// <summary>
        /// Displays the main menu with options that can be selected to open more menus. Can login, register, or exit the program.
        /// </summary>
        /// <returns>
        /// Returns running is true to keep the menu running, if false the menu closes.
        /// </returns>
        private bool DisplayMainMenu()
        {
            // Menu spacing
            CommandLineUI.DisplayMessage();

            // Main menu strings
            const string LOGIN_STR = "Login as a registered user";
            const string REGISTER_STR = "Register as a new user";
            const string EXIT_STR = "Exit";

            // Integer for each main menu string option
            const int LOGIN_INT = 0, REGISTER_INT = 1, EXIT_INT = 2;

            // Display the main menu using CommandLineUI.GetOption method
            int option = CommandLineUI.GetOption(GPHConstants.MAINMENU_STR, LOGIN_STR, REGISTER_STR, EXIT_STR);

            // Switch cases to display main menu functionality, logging in or registeirng.
            switch (option)
            {
                case LOGIN_INT:
                    DisplayLoginMenu();
                    break;
                case REGISTER_INT:
                    DisplayRegisterMenu();
                    break;
                case EXIT_INT:
                    CommandLineUI.DisplayMessage("Goodbye. Please stay safe.");
                    return false; // Program will exit due to boolean running is false.
                default:
                    CommandLineUI.DisplayErrorAgain(GPHConstants.INVALIDMENU);
                    break;
            }
            // Display main menu will continue to keep running due to boolean running is true.
            return true;
        }

        /// <summary>
        /// Instantiates a default user with the relevant hospital object so that the user can register with valid details for that specific hospital.
        /// </summary>
        private void DisplayLoginMenu()
        {
            // Set default user's values to empty values
            string name = "";
            int age = 0;
            string mobileNo = "";
            string email = "";
            string password = "";

            // Instantiate a user trying to login with placeholder values
            User userAttemptingLogin = new User(name, age, mobileNo, email, password, GardensPointHospital);
            // Call method to login to this menu instance
            LoginMenu newLoginMenu = new LoginMenu();
            newLoginMenu.Login(this, userAttemptingLogin);
        }

        /// <summary>
        /// Displays a menu with options to register as a patient, staff, or return to initial menu.
        /// </summary>
        /// <returns>
        /// Returns running is true to keep the menu running, if false the menu closes.
        /// </returns>
        private bool DisplayRegisterMenu()
        {
            // Menu spacing
            CommandLineUI.DisplayMessage();
            
            // Register menu string options
            const string REGISTERAS_STR = "Register as which type of user:";
            const string REGISTERPATIENT_STR = "Patient";
            const string REGISTERSTAFF_STR = "Staff";
            const string RETURNFIRST_STR = "Return to the first menu";

            // Integer for each register menu string option
            const int REGISTERPATIENT_INT = 0, REGISTERSTAFF_INT = 1, RETURNFIRST_INT = 2;

            // Display the register menu using CommandLineUI.GetOption method
            int option = CommandLineUI.GetOption(REGISTERAS_STR, REGISTERPATIENT_STR, REGISTERSTAFF_STR, RETURNFIRST_STR);

            // Switch cases for registering as a user.
            switch (option)
            {
                case REGISTERPATIENT_INT:
                    DisplayUserRegisterMenu("patient");
                    break;
                case REGISTERSTAFF_INT:
                    DisplayRegisterStaffMenu();
                    break;
                case RETURNFIRST_INT:
                    return false; // Returns false, closing the menu.
                default:
                    CommandLineUI.DisplayErrorAgain(GPHConstants.INVALIDMENU);
                    break;
            }
            // Display register menu will continue to keep running as boolean is true.
            return true;
        }

        /// <summary>
        /// Displays a menu with options to register as the two types of staff members, either a floor manager or a surgeon. Otherwise return to initial menu.
        /// </summary>
        /// <returns>
        /// Returns running is true to keep the menu running, if false the menu closes.
        /// </returns>
        private bool DisplayRegisterStaffMenu()
        {
            // Menu spacing
            CommandLineUI.DisplayMessage();

            // Register staff menu string options
            const string REGISTERASSTAFF_STR = "Register as which type of staff:";
            const string REGISTERFLOOR_STR = "Floor manager";
            const string REGISTERSURGEON_STR = "Surgeon";
            const string RETURNFIRST_STR = "Return to the first menu";

            // Integer for each register staff menu option
            const int REGISTERFLOOR_INT = 0, REGISTER_SURGEON = 1, RETURNFIRST_INT = 2;

            // Display the register staff menu using CommandLineUI.GetOption method
            int option = CommandLineUI.GetOption(REGISTERASSTAFF_STR, REGISTERFLOOR_STR, REGISTERSURGEON_STR, RETURNFIRST_STR);

            // Switch cases for registering a staff member.
            switch (option)
            {
                case REGISTERFLOOR_INT:
                    DisplayUserRegisterMenu("floor manager");
                    break;
                case REGISTER_SURGEON:
                    DisplayUserRegisterMenu("surgeon");
                    break;
                case RETURNFIRST_INT:
                    return false; // Returns false, closing the menu.
                default:
                    DisplayMainMenu();
                    break;
            }
            // Display register staff menu will continue to keep running as boolean is true.
            return true;
        }

        /// <summary>
        /// Once the user is confirmed to be registered, the user will be checked for their role, then the relevant role menu will be instantiated and displayed for viewing.
        /// </summary>
        /// <param name="userLoggedIn">
        /// userLoggedIn is a user that is confirmed to be registered legitimately.
        /// </param>
        public void DisplayWhichUserTypeMenu(User userLoggedIn)
        {
            // Initialise user as an empty string
            string userType = "";

            // Dependent on what the user type is, display which relevant user type menu as a new object of that user type menu.
            if (userLoggedIn is Patient patientLoggedIn)
            {
                PatientMenu newPatientMenu = new PatientMenu();
                userType = "Patient";
                newPatientMenu.DisplayUserTypeMenu(userLoggedIn, userType);
            }
            else if (userLoggedIn is FloorManager floorManagerLoggedIn)
            {
                FloorManagerMenu newFloorManagerMenu = new FloorManagerMenu();
                userType = "Floor Manager";
                newFloorManagerMenu.DisplayUserTypeMenu(userLoggedIn, userType);
            }
            else if (userLoggedIn is Surgeon surgeonLoggedIn)
            {
                SurgeonMenu newSurgeonMenu = new SurgeonMenu();
                userType = "Surgeon";
                newSurgeonMenu.DisplayUserTypeMenu(userLoggedIn, userType);
            }
        }

        /// <summary>
        /// Displays the user register menu by instantiating a default user, and presenting the relevant user type register menu.
        /// </summary>
        /// <param name="userType">
        /// The type of user that is being registered as. This parameter will indicate what sort of register menu is displayed.
        /// </param>
        private void DisplayUserRegisterMenu(string userType)
        {
            // Set default user value's to empty values
            string name = "";
            int age = 0;
            string mobileNo = "";
            string email = "";
            string password = "";

            // Instantiate a user trying to register with placeholder values
            User newRegisteringUser = new User(name, age, mobileNo, email, password, GardensPointHospital);
            // Call method that registers the user as a specific type
            IdentifyRegisteringAsUserType(userType, newRegisteringUser);
        }

        /// <summary>
        /// Checks which user registration option has been selected by the user, and then conducts the registration process for that user type.
        /// </summary>
        /// <param name="registeringUserType">
        /// The type that the user is registering as, registration process will be dependent based on user type.
        /// </param>
        /// <param name="registeringUser">
        /// The registering user.
        /// </param>
        private void IdentifyRegisteringAsUserType(string registeringUserType, User registeringUser)
        {
            int staffID = 0;

            // Create a default user of the user type patient, floor manager, or surgeon. The selected user type will have their fields filled with placeholder values and instantiated to begin the registering process.
            if (registeringUserType == "patient")
            {
                Patient newPatient = new Patient(registeringUser._Name, registeringUser._Age, registeringUser._MobileNo, registeringUser._Email, registeringUser._Password, registeringUser._Hospital);
                RegisterPatientMenu newRegisterPatientMenu = new RegisterPatientMenu();
                newRegisterPatientMenu.RegisterUserTypeInformation(registeringUserType, newPatient);
            }
            else if (registeringUserType == "floor manager")
            {
                int floorNo = 0;
                FloorManager newFloorManager = new FloorManager(registeringUser._Name, registeringUser._Age, registeringUser._MobileNo, registeringUser._Email, registeringUser._Password, registeringUser._Hospital, staffID, floorNo);
                RegisterFloorManagerMenu newRegisterFloorManagerMenu = new RegisterFloorManagerMenu();
                newRegisterFloorManagerMenu.RegisterFloorManagerIfFloorNoAvailable(registeringUserType, newFloorManager);
            }
            else if (registeringUserType == "surgeon")
            {
                string speciality = "";
                Surgeon newSurgeon = new Surgeon(registeringUser._Name, registeringUser._Age, registeringUser._MobileNo, registeringUser._Email, registeringUser._Password, registeringUser._Hospital, staffID, speciality);
                RegisterSurgeonMenu newRegisterSurgeonMenu = new RegisterSurgeonMenu();
                newRegisterSurgeonMenu.RegisterUserTypeInformation(registeringUserType, newSurgeon);
            }
        }
    }
}
