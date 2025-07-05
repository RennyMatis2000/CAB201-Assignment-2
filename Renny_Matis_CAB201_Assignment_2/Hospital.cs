using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A Hospital object represents the hospital that this program is currently being used for. Hospital class deals with the storage of user information within the hospital database. 
    /// </summary>
    public class Hospital
    {
        private List<User> userList = new List<User>();
        private List<Patient> patientList = new List<Patient>();
        private List<Staff> staffList = new List<Staff>();
        private List<Surgeon> surgeonList = new List<Surgeon>();
        private List<FloorManager> floorManagerList = new List<FloorManager>();
        private List<Surgery> surgeryList = new List<Surgery>();
        private List <Room> roomList = new List<Room>();

        /// <summary>
        /// Default public Hospital constructor, initialises all the lists that are fields in this hospital class.
        /// </summary>
        public Hospital()
        {
            // Initialise all relevant lists to the hospital database.
            userList = new List<User>();
            patientList = new List<Patient>();
            staffList = new List<Staff>();
            surgeonList = new List<Surgeon>();
            floorManagerList = new List<FloorManager>();
            surgeryList = new List<Surgery>();
            // Call the CreateDefaultRooms method to initialise the hospital object with all rooms for patients at the hospital.
            List<Room> RoomList = CreateDefaultRooms();
        }

        /// <summary>
        /// Returns the private user list value as public so other classes of the program can access the hospital database for business operations.
        /// </summary>
        public List<User> _UserList
        {
            get { return userList; }
        }

        /// <summary>
        /// Returns the private patient list value as public so other classes of the program can access the hospital database for business operations. 
        /// </summary>
        public List<Patient> _PatientList
        {
            get { return patientList; }
        }

        /// <summary>
        /// Returns the private staff list value as public so other classes of the program can access the hospital database for business operations. 
        /// </summary>
        public List<Staff> _StaffList
        {
            get { return staffList; }
        }

        /// <summary>
        /// Returns the private surgeon list value as public so other classes of the program can access the hospital database for business operations.  
        /// </summary>
        public List<Surgeon> _SurgeonList
        {
            get { return surgeonList; }
        }

        /// <summary>
        /// Returns the private floor manager list value as public so other classes of the program can access the hospital database for business operations.  
        /// </summary>
        public List<FloorManager> _FloorManagerList
        {
            get { return floorManagerList; }
        }

        /// <summary>
        /// Returns the private surgery list value as public so other classes of the program can access the hospital database for business operations.   
        /// </summary>
        public List<Surgery> _SurgeryList
        {
            get { return surgeryList; }
        }

        /// <summary>
        /// Returns the private room list value as public so other classes of the program can access the hospital database for business operations.   
        /// </summary>
        public List<Room> _RoomList
        {
            get { return roomList; }
        }

        /// <summary>
        /// Add the registered user to the hospital database.
        /// </summary>
        /// <param name="registeredUser">
        /// The user that has gone through the process of inputting their details and information, and must now be registered to the hospital database based on their user type.
        /// </param>
        public void RegisterUserToDatabase(User registeredUser)
        {
            // Check what user type the registered user is, then add them to the hospital database dependent on their user type.
            if (registeredUser is Patient)
            {
                AddPatientToDatabase((Patient)registeredUser);
            }
            else if (registeredUser is FloorManager)
            {
                AddFloorManagerToDatabase((FloorManager)registeredUser);
            }
            else if (registeredUser is Surgeon)
            {
                AddSurgeonToDatabase((Surgeon)registeredUser);
            }
        }

        /// <summary>
        /// Takes the patient that has been confirmed to be registered, and adds it to the hospital database for relevant roles (user, patient).
        /// </summary>
        /// <param name="registeredPatient">
        /// registeredPatient is the patient that has been confirmed to be registered.
        /// </param>
        private void AddPatientToDatabase(Patient registeredPatient)
        {
            userList.Add(registeredPatient);
            patientList.Add(registeredPatient);
        }

        /// <summary>
        /// Takes the floor manager that has been confirmed to be registered, and adds it to the hospital database for relevant roles (user, staff, floor manager).
        /// </summary>
        /// <param name="registeredFloorManager">
        /// registeredFloorManager is the floor manager that has been confirmed to be registered.
        /// </param>
        private void AddFloorManagerToDatabase(FloorManager registeredFloorManager)
        {
            userList.Add(registeredFloorManager);
            staffList.Add(registeredFloorManager);
            floorManagerList.Add(registeredFloorManager);
        }

        /// <summary>
        /// Takes the surgeon that has been confirmed to be registered, and adds it to the hospital database for relevant roles (user, staff, surgeon).
        /// </summary>
        /// <param name="registeredSurgeon">
        /// registeredSurgeon is the surgeon that has been confirmed to be registered.
        /// </param>
        private void AddSurgeonToDatabase(Surgeon registeredSurgeon)
        {
            userList.Add(registeredSurgeon);
            staffList.Add(registeredSurgeon);
            surgeonList.Add(registeredSurgeon);
        }

        /// <summary>
        /// Creates a list of default room objects that can be occupied by patient objects within the hospital.
        /// </summary>
        /// <returns>
        /// Returns a list of all the patient rooms within the hospital that can be occupied.
        /// </returns>
        private List<Room> CreateDefaultRooms()
        {
            // 6 floors is the maximum amount of floors in the hospital
            for (int floorNo = 1; floorNo <= 6; floorNo++)
            {
                // 10 rooms is the maximum amount of rooms on a floor
                for (int roomNo = 1; roomNo <= 10; roomNo++)
                {
                    Room defaultRoom = new Room(roomNo, floorNo, false, null);
                    roomList.Add(defaultRoom);
                }
            }
            return roomList;
        }

    }
}
