using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    ///  A child class of staff. A surgeon object is any user who is employed by the hospital, and works as a surgeon. Surgeon's role is to perform surgery on patients.
    /// </summary>
    public class Surgeon : Staff
    {
        private string Speciality;
        private List<Patient> patientsWithSurgeon = new List<Patient>();
        private List<Surgery> SurgeriesToSort = new List<Surgery>();
        private List<Surgery> TemporallySortedSurgeries = new List<Surgery>();

        /// <summary>
        /// Default public constructor of a surgeon.
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
        /// Inherits from the base staff class for staffID.
        /// </param>
        /// <param name="speciality">
        /// What speciality does this surgeon object operate as.
        /// </param>
        public Surgeon(string name, int age, string mobileNo, string email, string password, Hospital hospital, int staffID, string speciality) : base(name, age, mobileNo, email, password, hospital, staffID )
        {
            // Speciality in the constructor indicates this staff member is a surgeon. 
            Speciality = speciality;
        }

        /// <summary>
        /// Generates a personal list of patients from the hospital that have been assigned a surgeon with the same staffID (unique identifier) as the logged in surgeon.
        /// </summary>
        public override void AssignRelevantPatients()
        {
            // Clear list of patients the surgeon is assigned.
            patientsWithSurgeon.Clear();

            // Loop through the list of patients in the hospital database patient list to add to a list of patients the surgeon is assigned.
            foreach (Patient patient in this._Hospital._PatientList)
            {
                if (patient._CheckedIn && patient._RoomNo != 0 && patient._AssignedSurgeon._StaffID == _StaffID && patient._AssignedSurgery._ConductedSurgery == false)
                {
                    patientsWithSurgeon.Add(patient);
                }
            }
        }

        /// <summary>
        /// Overrides the virtual method of user to display staff user details. Displays base staff user details, along with the surgeon relevant field Speciality.
        /// </summary>
        public override void DisplayUserDetails()
        {
            base.DisplayUserDetails();
            CommandLineUI.DisplayMessage($"Speciality: {Speciality}");
        }

        /// <summary>
        /// Surgeon can view all patients that have been assigned to the current logged in surgeon.
        /// </summary>
        public void ViewPatients()
        {
            AssignRelevantPatients();

            CommandLineUI.DisplayMessage("Your Patients.");

            // Loops through a list of patients that have an assigned surgeon, and checks if the assigned surgeon is the current logged in surgeon.
            // Then display the patients.

            if (patientsWithSurgeon != null && patientsWithSurgeon.Count != 0)
            {
                for (int i = 0; i < patientsWithSurgeon.Count; i++)
                {
                    CommandLineUI.DisplayMessage($"{i + 1}. {patientsWithSurgeon[i]._Name}");
                }
            }
            else
            {
                CommandLineUI.DisplayMessage("You do not have any patients assigned.");
            }
            return;
        }

        /// <summary>
        /// Surgeon can view all surgeries assigned to them sorted by chronological order.
        /// </summary>
        public void ViewSchedule()
        {
            CommandLineUI.DisplayMessage("Your schedule.");
            // Call method that sorts the surgeries into chronological order
            SortSurgeriesTemporally();

            // If surgeon has any surgeries, display the surgery's relevant information.
            if (TemporallySortedSurgeries.Count == 0)
            {
                CommandLineUI.DisplayMessage("You do not have any patients assigned.");
                return;
            }
            else
            {
                for (int i = 0; i < TemporallySortedSurgeries.Count; i++)
                {
                    string SurgeryTimeString = TemporallySortedSurgeries[i]._SurgeryTime.ToString(GPHConstants.DATETIMEFORMAT);
                    CommandLineUI.DisplayMessage($"Performing surgery on patient {TemporallySortedSurgeries[i]._PatientWithSurgery._Name} on {SurgeryTimeString}");
                }
            }
        }

        /// <summary>
        /// Surgeon selects a patient assigned to themselves to operate on. Once operated on, patient is marked as already received surgery, therefore cannot be operated on further.
        /// </summary>
        public void PerformSurgery()
        {
            // Update list of patients that have been assigned to the surgeon to be operated on.
            AssignRelevantPatients();
            
            if (patientsWithSurgeon != null && patientsWithSurgeon.Count != 0)
            {
                // Display relevant patient list as a menu.
                int SelectedPatient = DisplayPatientOptions(patientsWithSurgeon);
                // Select patient to operate on and -1 as the list counts from 0.
                int indexedSelectedPatient = SelectedPatient - 1;
                Patient actualPatient = patientsWithSurgeon[indexedSelectedPatient];

                CommandLineUI.DisplayMessage($"Surgery performed on {actualPatient._Name} by {_Name}.");
                actualPatient._AssignedSurgery._ConductedSurgery = true;
            }
            else
            {
                CommandLineUI.DisplayMessage("You do not have any patients assigned.");
            }
            return;
        }

        /// <summary>
        /// Sorts the surgeries assigned to the logged in surgeon from the hospital database into chronological order.
        /// </summary>
        private void SortSurgeriesTemporally()
        {
            // Update relevant lists everytime method is called
            SurgeriesToSort.Clear();
            TemporallySortedSurgeries.Clear();

            // Retrieve the list of all surgeries from the hospital
            foreach (Surgery surgery in this._Hospital._SurgeryList)
            {
                // Any surgery that is assigned to the logged in surgeon is added to a list to sort.
                if (this._Hospital._SurgeryList != null && this._Hospital._SurgeryList.Count > 0 && surgery._SelectedSurgeon._StaffID == _StaffID)
                {
                    SurgeriesToSort.Add(surgery);
                }
            }
            // Sort the DateTime list using OrderBy which sorts chronologically, then add the chronologically sorted DateTimes to a list
            TemporallySortedSurgeries = SurgeriesToSort.OrderBy(surgery => surgery._SurgeryTime).ToList();
        }

    }
}
