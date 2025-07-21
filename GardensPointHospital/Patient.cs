using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A child class of the user. The patient object are users who are administered to the hospital to receive surgery.
    /// </summary>
    public class Patient : User
    {
        private bool CheckedIn;
        private int RoomNo;
        private int FloorNo;
        private Surgeon AssignedSurgeon;
        private DateTime SurgeryDateTime;
        private Surgery AssignedSurgery;

        /// <summary>
        /// Returns the private CheckedIn value as a public value, also allows other classes in the program to modify whether the patient is checked in or out.
        /// </summary>
        public bool _CheckedIn
        {
            get { return CheckedIn; }
            set { CheckedIn = value; }
        }

        /// <summary>
        /// Returns the private room number value as a public value, also allows other classes in the program to modify the patient's floor number.
        /// </summary>
        public int _RoomNo
        {
            get { return RoomNo; }
            set { RoomNo = value; }
        }

        /// <summary>
        /// Returns the patients private floor number as a public value, also allows other classes in the program to modify the patient's floor number.
        /// </summary>
        public int _FloorNo
        {
            get { return FloorNo; }
            set { FloorNo = value; }
        }

        /// <summary>
        /// Returns the private value assigned surgeon to the patient as a public value, allowing other classes in the program to modify the patient's assigned surgeon.
        /// </summary>
        public Surgeon _AssignedSurgeon
        {
            get { return AssignedSurgeon; }
            set { AssignedSurgeon = value; }
        }

        /// <summary>
        /// Returns the private surgery date time value as a public value, allowing other classes in the program to modify the surgery date and time of the patient.
        /// </summary>
        public DateTime _SurgeryDateTime
        {
            get { return SurgeryDateTime; }
            set { SurgeryDateTime = value; }
        }

        /// <summary>
        /// Returns the private assigned surgery value of the patient as a public value, allowing other classes in the program to modify the patient's assigned surgery.
        /// </summary>
        public Surgery _AssignedSurgery
        {
            get { return AssignedSurgery; }
            set { AssignedSurgery = value; }
        }

        public Patient(string name, int age, string mobileNo, string email, string password, Hospital hospital, bool checkedIn = false, int roomNo = 0, int floorNo = 0, Surgeon assignedSurgeon = null, DateTime surgeryDateTime = default(DateTime), Surgery assignedSurgery = null) : base(name, age, mobileNo, email, password, hospital)
        {
            CheckedIn = checkedIn; 
            FloorNo = floorNo;
            AssignedSurgeon = assignedSurgeon;
            AssignedSurgery = assignedSurgery;

            // Instantiate the minimum default time for the DateTime of a users surgery as it cannot be null
            if (surgeryDateTime == default(DateTime))
            {
                SurgeryDateTime = DateTime.MinValue;
            }

            SurgeryDateTime = surgeryDateTime;
        }

        /// <summary>
        /// Allows the patient to check in and out dependent on whether their CheckedIn boolean is true or false. If the patient has an assigned surgery and has not finished their surgery, they will be unable to check out.
        /// </summary>
        public void PatientCheckInOut()
        {
            // If patient is not checked in, allow them to check in.
            if (_CheckedIn == false && _AssignedSurgery == null)
            {
                _CheckedIn = true;
                CommandLineUI.DisplayMessage($"Patient {_Name} has been checked in.");
            }
            // If patient has already had a surgery, do not let them check in.
            else if (_CheckedIn == false && _AssignedSurgery != null && _AssignedSurgery._ConductedSurgery == true)
            {
                CommandLineUI.DisplayMessage("You are unable to check in at this time.");
            }
            // If patient is checked in and has had their surgery, allow them to check out. If surgery has not concluded, do not let them check out.
            else if (_CheckedIn == true)
            {
                if (AssignedSurgery != null && AssignedSurgery._ConductedSurgery == true)
                {
                    _CheckedIn = false;
                    CommandLineUI.DisplayMessage($"Patient {_Name} has been checked out.");
                }
                else
                {
                    CommandLineUI.DisplayMessage("You are unable to check out at this time.");
                }

            }
            return;

        }

        /// <summary>
        /// Allows a patient to see their room number.
        /// </summary>
        public void SeePatientRoomNo()
        {
            // If room number and floor number are not placeholder values indicating no room, show room.
            if (RoomNo != 0 && FloorNo != 0)
            {
                CommandLineUI.DisplayMessage($"Your room is number {RoomNo} on floor {FloorNo}.");
            }
            else
            {
                CommandLineUI.DisplayMessage("You do not have an assigned room.");
                return;
            }
        }

        /// <summary>
        /// Allows a patient to see their assigned surgeon.
        /// </summary>
        public void SeeAssignedSurgeon()
        {
            if (AssignedSurgeon != null)
            {
                CommandLineUI.DisplayMessage($"Your surgeon is {AssignedSurgeon._Name}.");
            }
            else
            {
                CommandLineUI.DisplayMessage("You do not have an assigned surgeon.");
                return;
            }
        }

        /// <summary>
        /// Allows a patient to see their surgery time on a specific date.
        /// </summary>
        public void SeeSurgeryDateTime()
        {
            // If SurgeryDateTime is not placeholder value indicating no surgery, show SurgeryDateTime.
            if (SurgeryDateTime != DateTime.MinValue)
            {
                string surgeryDateTimeString = SurgeryDateTime.ToString(GPHConstants.DATETIMEFORMAT);
                CommandLineUI.DisplayMessage($"Your surgery time is {surgeryDateTimeString}.");
            }
            else
            {
                CommandLineUI.DisplayMessage("You do not have assigned surgery.");
                return;
            }
        }

    }
}
