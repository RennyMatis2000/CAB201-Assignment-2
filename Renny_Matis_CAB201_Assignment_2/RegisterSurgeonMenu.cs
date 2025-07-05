using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    ///  A child class of RegisterStaffMenu, handles displaying menu options to register a surgeon.
    /// </summary>
    public class RegisterSurgeonMenu : RegisterStaffMenu
    {
        /// <summary>
        /// Overrides the virtual user method that collects the users base details. Collects the base details, and relevant surgeon details to register the surgeon.
        /// </summary>
        /// <param name="userType">
        /// Method displays the user type that is being registered to the user as a string.
        /// </param>
        /// <param name="registeringSurgeon">
        /// The registering surgeon.
        /// </param>
        /// <returns>
        /// Returns the tuple with variables from the user's base detail collection.
        /// </returns>
        public override (string name, int age, string mobileNo, string email, string password) RegisterUserTypeInformation(string userType, User registeringSurgeon)
        {
            // Store the information returned about the base user from the tuple.
            var (name, age, mobileNo, email, password) = base.RegisterUserTypeInformation(userType, registeringSurgeon);
            int staffID = GetValidStaffID(registeringSurgeon._Hospital._StaffList);
            string speciality = "";

            // Strings for speciality options.
            const string CHOOSESPECIALITY_STR = "Please choose your speciality:";
            const string GENERAL_STR = "General Surgeon";
            const string ORTHOPAEDIC_STR = "Orthopaedic Surgeon";
            const string CARDIOTHORACIC_STR = "Cardiothoracic Surgeon";
            const string NEUROSURGEON_STR = "Neurosurgeon";

            // Integers for speciality option.
            const int GENERAL_INT = 0, ORTHOPAEDIC_INT = 1, CARDIOTHORACIC_INT = 2, NEUROSURGEON_INT = 3;

            // Display the speciality option select menu
            int option = CommandLineUI.GetOption(CHOOSESPECIALITY_STR, GENERAL_STR, ORTHOPAEDIC_STR, CARDIOTHORACIC_STR, NEUROSURGEON_STR);

            // Switch cases for which speciality to choose.
            switch (option)
            {
                case GENERAL_INT:
                    speciality = GENERAL_STR;
                    break;
                case ORTHOPAEDIC_INT:
                    speciality = ORTHOPAEDIC_STR;
                    break;
                case CARDIOTHORACIC_INT:
                    speciality = CARDIOTHORACIC_STR;
                    break;
                case NEUROSURGEON_INT:
                    speciality = NEUROSURGEON_STR;
                    break;
                default:
                    // If not within the boundaries of the speciality option select, make the user try again.
                    CommandLineUI.DisplayErrorAgain("Non-valid speciality type");
                    option = CommandLineUI.GetOption(CHOOSESPECIALITY_STR, GENERAL_STR, ORTHOPAEDIC_STR, CARDIOTHORACIC_STR, NEUROSURGEON_STR);
                    break;
            }

            // Register the surgeon with the returned information from the tuple. 
            RegisterSurgeon(name, age, mobileNo, email, password, registeringSurgeon._Hospital, staffID, speciality);
            // Return tuple information.
            return (name, age, mobileNo, email, password);
        }

        /// <summary>
        /// Registers a surgeon through instantiation, the surgeon object is then added as a valid registered surgeon to the hospital database.
        /// </summary>
        /// <param name="name">
        /// Surgeon name.
        /// </param>
        /// <param name="age">
        /// Surgeon age.
        /// </param>
        /// <param name="mobileNo">
        /// Surgeon mobile number.
        /// </param>
        /// <param name="email">
        /// Surgeon email.
        /// </param>
        /// <param name="password">
        /// Surgeon password.
        /// </param>
        /// <param name="hospital">
        /// Hospital the surgeon works at.
        /// </param>
        /// <param name="staffID">
        /// staffID of the surgeon.
        /// </param>
        /// <param name="speciality">
        /// Speciality the surgeon operates as.
        /// </param>
        private void RegisterSurgeon(string name, int age, string mobileNo, string email, string password, Hospital hospital, int staffID, string speciality)
        {
            // Register the surgeon with details they have provided to be added to their hospital database.
            Surgeon registeredFloorManager = new Surgeon(name, age, mobileNo, email, password, hospital, staffID, speciality);
            hospital.RegisterUserToDatabase(registeredFloorManager);
            CommandLineUI.DisplayMessage($"{registeredFloorManager._Name} is registered as a surgeon.");
        }
    }
}
