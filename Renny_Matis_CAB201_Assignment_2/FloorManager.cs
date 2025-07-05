using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A child class of staff. A Floor Manager object is any user who is employed by the hospital, and works as a Floor Manager. Floor Manager's role is to assign patients to rooms, and set up surgeries between the patient and surgeon.
    /// </summary>
    public class FloorManager : Staff
    {
        private int FloorNo;
        private bool Assigning = false;
        private List<Patient> availablePatientList = new List<Patient>();
        private List<Patient> patientsReadyForSurgery = new List<Patient>();
        private List<Patient> patientsCompletedSurgery = new List<Patient>();

        /// <summary>
        /// Default public constructor of floor manager.
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
        /// <param name="floorNo">
        /// The floor number that the floor manager is stationed to work on.
        /// </param>
        public FloorManager(string name, int age, string mobileNo, string email, string password, Hospital hospital, int staffID, int floorNo) : base(name, age, mobileNo, email, password, hospital, staffID)
        {
            // Floor number in the constructor indicates this staff member is a floor manager.
            FloorNo = floorNo;
        }

        public int _FloorNo
        {
            get { return FloorNo; }
        }

        /// <summary>
        /// Overrides the virtual method of user to display staff user details. Displays base staff user details, along with the floor manager relvant field FloorNo.
        /// </summary>
        public override void DisplayUserDetails()
        {
            base.DisplayUserDetails();
            CommandLineUI.DisplayMessage($"Floor: {FloorNo}.");
        }

        /// <summary>
        /// Generates a list of patients that have checked into the hospital.
        /// If patients are checked in and have a room, generates a list of patients that can be assigned surgery times.
        /// If patients have had a surgery and checked out, add them to a list of patients that have had surgeries.
        /// </summary>
        public override void AssignRelevantPatients()
        {
            // Clear all lists that will be filled in this method. Each use of this method should fill the list with only relevant patients.
            availablePatientList.Clear();
            patientsReadyForSurgery.Clear();
            patientsCompletedSurgery.Clear();

            // Check that patients are available to assign from the hospital database.
            if (_Hospital._PatientList.Count == 0)
            {
                CommandLineUI.DisplayMessage("There are no registered patients.");
                return;
            }
            else
            {
                foreach (Patient patient in _Hospital._PatientList)
                {
                    // If floor manager is assigning patients, generate a list of patients that can be assigned to a room or surgery
                    if (Assigning == true)
                    {
                        if (patient._CheckedIn == true && patient._RoomNo == 0)
                        {
                            availablePatientList.Add(patient);
                        }
                        else if (patient._CheckedIn == true && patient._RoomNo != 0 && patient._AssignedSurgeon == null)
                        {
                            patientsReadyForSurgery.Add(patient);
                        }
                    }
                    else if (Assigning == false)
                    {
                        // If floor manager is unassigning, generate a list of patients that can be unassigned to a room (checked out and had a surgery)
                        if (patient._CheckedIn == false && patient._RoomNo != 0 && patient._AssignedSurgeon != null && patient._AssignedSurgery._ConductedSurgery == true)
                        {
                            patientsCompletedSurgery.Add(patient);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

        }

        /// <summary>
        /// Display all registered surgeons at the hospital in a list which can be selected from.
        /// </summary>
        private void ViewSurgeonsAvailable()
        {
            // Loop through surgeons in the surgeon list within the hospital database to display a menu list of all surgeons working at the hospital.
            foreach (Surgeon availableSurgeons in _Hospital._SurgeonList)
            {
                if (_Hospital._SurgeonList != null && _Hospital._SurgeonList.Count > 0)
                {
                    CommandLineUI.DisplayMessage("Please select your surgeon:");

                    for (int i = 0; i < _Hospital._SurgeonList.Count; i++)
                    {
                        CommandLineUI.DisplayMessage($"{i + 1}. {_Hospital._SurgeonList[i]._Name}");
                    }
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Assign a room to a patient. A patient must be ready, and a room must be available.
        /// </summary>
        public void AssignRoom()
        {
            bool allRoomsOnFloorFull = CheckIfAllRoomsOnFloorFull();

            // If there is an available room on this floor begin to assign.
            if (allRoomsOnFloorFull == false)
            {
                Assigning = true;

                // Assign patients to relevant lists about room assigning in this method.
                AssignRelevantPatients();

                // Check there are available patients to assign a room to.
                if (availablePatientList.Count == 0 && _Hospital._PatientList.Count > 0)
                {
                    CommandLineUI.DisplayMessage("There are no checked in patients.");
                    return;
                }
                else if (_Hospital._PatientList.Count == 0)
                {
                    return;
                }
                else if (availablePatientList.Count > 0)
                {
                    // If there are available patients select a valid patient -1 as lists count from 0.
                    int SelectedPatient = DisplayPatientOptions(availablePatientList);
                    int indexedSelectedPatient = SelectedPatient - 1;
                    // Identify the selected patient's name
                    Patient actualPatient = availablePatientList[indexedSelectedPatient];
                    string actualPatientName = availablePatientList[indexedSelectedPatient]._Name;

                    // Select a room to assign to this patient and check if it is full
                    int selectedRoomNo = DisplayRoomNoOptions();
                    Room selectedRoomInstance = CheckSelectedRoomFull(selectedRoomNo);

                    // Assign the selected patient to the selected room number if the selected room is not full
                    CommandLineUI.DisplayMessage($"Patient {actualPatientName} has been assigned to room number {selectedRoomInstance._RoomNo} on floor {FloorNo}.");
                    // Update the selected patient's fields
                    actualPatient._RoomNo = selectedRoomInstance._RoomNo;
                    actualPatient._FloorNo = FloorNo;
                    // Update the selected room's fields
                    selectedRoomInstance._RoomFull = true;
                    selectedRoomInstance._RoomOccupant = actualPatient;
                }

            }
            // Display error if no available rooms on floor.
            else if (allRoomsOnFloorFull == true)
            {
                CommandLineUI.DisplayError("All rooms on this floor are assigned");
                return;
            }

        }

        /// <summary>
        /// Access the hospital database to  ensure the selected room on the floor manager's floor number is not occupied by another patient.
        /// </summary>
        /// <param name="selectedRoomNo"></param>
        /// <returns></returns>
        private Room CheckSelectedRoomFull(int selectedRoomNo)
        {
            Room selectedRoomInstance = null;

            while (selectedRoomInstance == null)
            {
                // Access the hospital database for the list that holds every room
                foreach (Room rooms in _Hospital._RoomList)
                {
                    // Check the specific room number on the floor manager's managing floor is not full
                    if (rooms._RoomNo == selectedRoomNo && rooms._RoomFloorNo == FloorNo)
                    {
                        if (rooms._RoomFull == true)
                        {
                            CommandLineUI.DisplayErrorAgain("Room has been assigned to another patient");
                            selectedRoomNo = DisplayRoomNoOptions();
                        }
                        else
                        {
                            // If room is not full, set the selected room to the room found in the list
                            selectedRoomInstance = rooms;
                            break;
                        }

                    }

                }
            }
            // return the selected room as it is available for the patient to be assigned to, breaking the loop
            return selectedRoomInstance;
        }

        /// <summary>
        /// Unassigns a room from a patient if the patient has had surgery and checked out. Floor manager selects with patient to unassign by selecting an integer.
        /// </summary>
        public void UnassignRoom()
        {
            Assigning = false;

            // Assigning set to false, so assign relevant patients will fill a list of patients that can have their room unassigned.
            AssignRelevantPatients();

            // Check list of patients that have had a surgery and display it as a menu select option.
            if (patientsCompletedSurgery.Count > 0)
            {
                CommandLineUI.DisplayMessage("Please select your patient:");

                for (int i = 0; i < patientsCompletedSurgery.Count; i++)
                {
                    CommandLineUI.DisplayMessage($"{i + 1}. {patientsCompletedSurgery[i]._Name}");
                }

                // Select a patient to unassign the room from -1 as lists count from 0
                CommandLineUI.DisplayMessage($"Please enter a choice between 1 and {patientsCompletedSurgery.Count}.");
                int selectedPatient = CommandLineUI.GetInt();
                int indexedSelectedPatient = selectedPatient - 1;
                Patient actualPatient = patientsCompletedSurgery[indexedSelectedPatient];

                // Search for the patient's assigned room in the hospital database's room list
                Room assignedRoom = null;

                foreach (Room rooms in _Hospital._RoomList)
                {
                    if (rooms._RoomNo == actualPatient._RoomNo && rooms._RoomFloorNo == FloorNo && rooms._RoomFull == true)
                    {
                        assignedRoom = rooms;
                        break;
                    }
                }

                // Unassign the room from the patient
                CommandLineUI.DisplayMessage($"Room number {actualPatient._RoomNo} on floor {FloorNo} has been unassigned.");
                // Update the relevant room's fields
                assignedRoom._RoomFull = false;
                assignedRoom._RoomOccupant = null;
                // Update the selected patient's fields
                actualPatient._RoomNo = 0;
                return;
            }
            else
            {
                CommandLineUI.DisplayMessage("There are no patients ready to have their rooms unassigned.");
            }
        }

        /// <summary>
        /// Assign a surgery for a patient. Requires assigning a surgeon to the patient, and also assigning a DateTime for the surgery.
        /// </summary>
        public void AssignSurgery()
        {
            Assigning = true;

            AssignRelevantPatients();

            if (Assigning == true && patientsReadyForSurgery.Count > 0)
            {
                // Select a patient using an integer -1 as lists count from 0
                int selectedPatient = DisplayPatientOptions(patientsReadyForSurgery);
                int indexedSelectedPatient = selectedPatient - 1;
                Patient actualPatient = patientsReadyForSurgery[indexedSelectedPatient];

                ViewSurgeonsAvailable();

                // Select a surgeon using an integer -1 as lists count from 0
                int selectedSurgeon = SelectSurgeon(_Hospital._SurgeonList);
                int indexedSelectedSurgeon = selectedSurgeon - 1;
                Surgeon surgeonToAssign = _Hospital._SurgeonList[indexedSelectedSurgeon];

                // Assign the patient with the selected surgeon in their field
                actualPatient._AssignedSurgeon = surgeonToAssign;

                // Retrieve a DateTime for the surgery
                DateTime selectedDateTime = GetValidDateTime();
                string selectedDateTimeString = selectedDateTime.ToString(GPHConstants.DATETIMEFORMAT);

                // Assign the patient with the selected DateTime in their field
                actualPatient._SurgeryDateTime = selectedDateTime;

                // Instantiate a surgery object with the relevant fields
                Surgery surgeryToAssign = new Surgery(selectedDateTime, surgeonToAssign, actualPatient, false);

                // Add the surgery to the hospital database list of surgeries
                _Hospital._SurgeryList.Add(surgeryToAssign); 

                // Assign the patient with the instantiated surgery
                actualPatient._AssignedSurgery = surgeryToAssign;

                CommandLineUI.DisplayMessage($"Surgeon {surgeonToAssign._Name} has been assigned to patient {actualPatient._Name}.");
                CommandLineUI.DisplayMessage($"Surgery will take place on {selectedDateTimeString}.");
            }
            else if (patientsReadyForSurgery.Count == 0 && _Hospital._PatientList.Count > 0)
            {
                CommandLineUI.DisplayMessage("There are no patients ready for surgery.");
                return;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Displays 10 rooms to select from for the floor manager, as there are 10 rooms on each floor and the logged in floor manager manages 1 floor. If floor manager selects out of bounds, displays an error.
        /// </summary>
        /// <returns>
        /// Return the selected room number integer option.
        /// </returns>
        private int DisplayRoomNoOptions()
        {

            int SelectedRoomNo;
            bool validRoomNoSelected = false;

            // Loop while a valid room number has not been selected.
            do
            {
                CommandLineUI.DisplayMessage("Please enter your room (1-10):");
                SelectedRoomNo = CommandLineUI.GetInt();

                // Boundaries of the selected integer must be within 1 to 10 as that is the only valid room numbers on any individual floor. Display error if incorrect.
                if (SelectedRoomNo < 0 || SelectedRoomNo > 10)
                {
                    CommandLineUI.DisplayErrorAgain("Supplied value is out of range");
                }
                else
                {
                    validRoomNoSelected = true;
                }
            }
            while (validRoomNoSelected == false);
            // Once a valid room number has been selected, return that value.
            return SelectedRoomNo;
        }

        /// <summary>
        /// Checks if all rooms on the logged in floor managers floor is full.
        /// </summary>
        /// <returns>
        /// Returns a boolean that is true or false regarding whether all rooms on the logged in floor managers floor is full.
        /// </returns>
        private bool CheckIfAllRoomsOnFloorFull()
        {
            // Loop through all rooms in the hospital database for room list and check if they are occupied. Returns a boolean on the outcome.
            foreach (Room room in _Hospital._RoomList)
            {

                if (room._RoomFloorNo == FloorNo && room._RoomFull == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Displays all surgeons from the hospital database, and allows the floor manager to select one. If floor manager selects out of bounds, displays an error.
        /// </summary>
        /// <param name="relevantSurgeons">
        /// List of surgeons that are capable of performing surgery.
        /// </param>
        /// <returns>
        /// Returns the integer selected by the floor manager denoting what surgeon to assign.
        /// </returns>
        private int SelectSurgeon(List<Surgeon> relevantSurgeons)
        {
            int selectedSurgeon;
            bool validSurgeonSelected = false;

            // Loop while a valid surgeon has not been selected.
            do
            {
                // Display an option list of all valid surgeons.
                CommandLineUI.DisplayMessage($"Please enter a choice between 1 and {relevantSurgeons.Count}.");
                selectedSurgeon = CommandLineUI.GetInt();

                // Selected surgeon integer must be within the boundaries of the options displayed.
                if (selectedSurgeon < 0 || selectedSurgeon > relevantSurgeons.Count)
                {
                    CommandLineUI.DisplayErrorAgain("Supplied value is out of range");
                }
                else
                {
                    validSurgeonSelected = true;
                }
            }
            while (validSurgeonSelected == false);
            // Return the selected surgeon if valid.
            return selectedSurgeon;
        }

        /// <summary>
        /// Prompt the user to enter a DateTime that is in the valid format for a surgery's date and time. The DateTime must be input as (HH:MM DD/MM/YYYY" using 24-hour time. If incorrect, will prompt the user to continuously retry while displaying an error.
        /// </summary>
        /// <returns>
        /// Returns the valid DateTime.
        /// </returns>
        private DateTime GetValidDateTime()
        {
            DateTime selectedDateTime = DateTime.MinValue;
            bool validDateTimeSelected = false;

            // Loop while a valid date time is not selected.
            do
            {
                // Error handling if a valid date time is not selected.
                try
                {
                    CommandLineUI.DisplayMessage("Please enter a date and time (e.g. 14:30 31/01/2024).");
                    selectedDateTime = CommandLineUI.GetDateTime();
                    validDateTimeSelected = true;

                }
                // Catch the exception if the format of the input DateTime is incorrect.
                catch (FormatException)
                {
                    CommandLineUI.DisplayError("Supplied value is not a valid DateTime");
                }
            }
            while (validDateTimeSelected == false);
            // Return DateTime value if it is a valid DateTime.
            return selectedDateTime;
        }



    }
}
