using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A child class of RegisterMenu, handles displaying menu options to register a patient.
    /// </summary>
    public class RegisterPatientMenu : RegisterMenu
    {

        /// <summary>
        /// Overrides the virtual user method that collects the users base details. Collects the base details, and registers the patient.
        /// </summary>
        /// <param name="userType">
        /// Method displays the user type that is being registered to the user as a string.
        /// </param>
        /// <param name="registeringPatient">
        /// The registering patient.
        /// </param>
        /// <returns>
        /// Returns the tuple with variables from the user's base detail collection.
        /// </returns>
        public override (string name, int age, string mobileNo, string email, string password) RegisterUserTypeInformation(string userType, User registeringPatient)
        {
            // Store the information returned about the base user from the tuple.
            var (name, age, mobileNo, email, password) = base.RegisterUserTypeInformation(userType, registeringPatient);

            // Register the patient with the returned information from the tuple.
            RegisterPatient(name, age, mobileNo, email, password, registeringPatient._Hospital);
            // Return tuple information.
            return (name, age, mobileNo, email, password);
        }

        /// <summary>
        /// Registers a patient through instantiation, the patient object is then added as a valid registered patient to the hospital database.
        /// </summary>
        /// <param name="name">
        /// Patients name.
        /// </param>
        /// <param name="age">
        /// Patients age.
        /// </param>
        /// <param name="mobileNo">
        /// Patients mobile number.
        /// </param>
        /// <param name="email">
        /// Patients email.
        /// </param>
        /// <param name="password">
        /// Patients password.
        /// </param>
        /// <param name="hospital">
        /// Hospital the patient is staying at.
        /// </param>
        private void RegisterPatient(string name, int age, string mobileNo, string email, string password, Hospital hospital)
        {
            // Register the patient with details they have provided to be added to their hospital database. Include placeholder values for patient fields that are provided after creation.
            Patient registeredPatient = new Patient(name, age, mobileNo, email, password, hospital, false, 0, 0, null, default, null);
            hospital.RegisterUserToDatabase(registeredPatient);
            CommandLineUI.DisplayMessage($"{registeredPatient._Name} is registered as a patient.");
        }
    }
}
