using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A child class of the user. The staff object is any user who is employed by the hospital, these two roles are a floor manager or a surgeon. The staff class is abstract as a staff member is never registered, only floor managers and surgeons are.
    /// </summary>
    public abstract class Staff : User
    {
        private int StaffID;

        /// <summary>
        /// Returns the private value of a staffID as a public value to be accessed in other classes in the program.
        /// </summary>
        public int _StaffID
        {
            get { return StaffID; }
        }

        /// <summary>
        /// Default constructor for a staff member, being a floor manager or a surgeon. Abstract class therefore does not instantiate a staff member.
        /// </summary>
        /// <param name="name">
        /// Inherits from the base user class for name.
        /// </param>
        /// <param name="age">
        /// Inherits from the base user class for age.
        /// </param>
        /// <param name="mobileNo">
        /// Inherits from the base user class for mobile number.
        /// </param>
        /// <param name="email">
        /// Inherits from the base user class for email.
        /// </param>
        /// <param name="password">
        /// Inherits from the base user class for password.
        /// </param>
        /// <param name="hospital">
        /// Inherits from the base user class for hospital.
        /// </param>
        /// <param name="staffID">
        /// Uniquely identifying integer representing the staff member's identification number.
        /// </param>
        public Staff(string name, int age, string mobileNo, string email, string password, Hospital hospital, int staffID) : base(name, age, mobileNo, email, password, hospital)
        {
            // staffID in staff constructor indicates this user is a staff member.
            StaffID = staffID; 
        }

        /// <summary>
        /// Used to display patient options for the staff user to select for their relevant activities.
        /// </summary>
        /// <param name="relevantPatientList">
        /// Supply a relevant patient list to select from.
        /// </param>
        /// <returns>
        /// Returns the selected integer on the list -1 as lists count from 0.
        /// </returns>
        protected int DisplayPatientOptions(List<Patient> relevantPatientList)
        {
            int SelectedPatient;
            bool validPatientSelected = false;

            // Menu header
            CommandLineUI.DisplayMessage("Please select your patient:");

            // Displays all patients within the relevant patient list parameter
            for (int i = 0; i < relevantPatientList.Count; i++)
            {
                CommandLineUI.DisplayMessage($"{i + 1}. {relevantPatientList[i]._Name}");
            }

            // Loop for staff member to select a valid patient option, until staff member selects a valid patient option will continuously ask for a valid patient.
            do
            {
                CommandLineUI.DisplayMessage($"Please enter a choice between 1 and {relevantPatientList.Count}.");
                SelectedPatient = CommandLineUI.GetInt();

                // If option selected is out of the option boundaries, display an error.
                if (SelectedPatient < 0 || SelectedPatient > relevantPatientList.Count)
                {
                    CommandLineUI.DisplayErrorAgain("Supplied value is out of range");
                    validPatientSelected = false;
                }
                else
                {
                    validPatientSelected = true;
                }
            }
            while (validPatientSelected == false);
            // Return the selected patient if valid option selected.
            return SelectedPatient;
        }

        /// <summary>
        /// Overrides the virtual method of user to display user details. Displays base user details, along with the staff relevant field StaffID.
        /// </summary>
        public override void DisplayUserDetails()
        {
            base.DisplayUserDetails();
            CommandLineUI.DisplayMessage($"Staff ID: {StaffID}");
        }

        /// <summary>
        /// Abstract method used by floor manager and surgeons as they must assign relevant patients for methods, but these methods operate differently from eachother.
        /// </summary>
        public abstract void AssignRelevantPatients();

    }
}
