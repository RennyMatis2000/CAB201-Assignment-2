using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    ///  A child class of RegisterStaffMenu, handles displaying menu options to register a floor manager.
    /// </summary>
    public class RegisterFloorManagerMenu : RegisterStaffMenu
    {
        /// <summary>
        /// Overrides the virtual user method that collects the users base details. Collects the base details, and relevant floor manager details to register the floor manager.
        /// </summary>
        /// <param name="userType">
        /// Method displays the user type that is being registered to the user as a string.
        /// </param>
        /// <param name="registeringFloorManager">
        /// The registering floor manager.
        /// </param>
        /// <returns>
        /// Returns the tuple with variables from the user's base detail collection.
        /// </returns>
        public override (string name, int age, string mobileNo, string email, string password) RegisterUserTypeInformation(string userType, User registeringFloorManager)
        {
            // Store the information returned about the base user from the tuple.
            var (name, age, mobileNo, email, password) = base.RegisterUserTypeInformation(userType, registeringFloorManager);
            // Prompt the user to input a valid staffID and FloorNo during registration.
            int staffID = GetValidStaffID(registeringFloorManager._Hospital._StaffList);
            int floorNo = GetValidFloorNo(registeringFloorManager._Hospital._FloorManagerList);

            // Register the floor manager with the returned information from the tuple. 
            RegisterFloorManager(name, age, mobileNo, email, password, registeringFloorManager._Hospital, staffID, floorNo);
            // Return tuple information.
            return (name, age, mobileNo, email, password);
        }

        /// <summary>
        /// Registers a floor manager through instantiation, the floor manager object is then added as a valid registered floor manager to the hospital database.
        /// </summary>
        /// <param name="name">
        /// Floor managers name.
        /// </param>
        /// <param name="age">
        /// Floor managers age.
        /// </param>
        /// <param name="mobileNo">
        /// Floor managers mobile number.
        /// </param>
        /// <param name="email">
        /// Floor managers email.
        /// </param>
        /// <param name="password">
        /// Floor managers password.
        /// </param>
        /// <param name="hospital">
        /// Hospital the floor manager works at.
        /// </param>
        /// <param name="staffID">
        /// staffID of the floor manager.
        /// </param>
        /// <param name="floorNo">
        /// Floor number the floor manager is stationed to work on.
        /// </param>
        private void RegisterFloorManager(string name, int age, string mobileNo, string email, string password, Hospital hospital, int staffID, int floorNo)
        {
            // Register the floor manager with details they have provided to be added to their hospital database. 
            FloorManager registeredFloorManager = new FloorManager(name, age, mobileNo, email, password, hospital, staffID, floorNo);
            hospital.RegisterUserToDatabase(registeredFloorManager);
            CommandLineUI.DisplayMessage($"{registeredFloorManager._Name} is registered as a floor manager.");
        }

        /// <summary>
        /// Prompt the user to enter an int that is valid between 1-6 as there is overall 6 floors in the hospital. If the user enters out of bounds, will prompt the user to continuously retry while displaying an error.
        /// </summary>
        /// <param name="FloorManagerList"></param>
        /// <returns>
        /// Returns the valid integer selected.
        /// </returns>
        private int GetValidFloorNo(List<FloorManager> FloorManagerList)
        {
            int floorNo;
            bool validFloorNoFound = false;

            // Loop while valid floor number is not selected.
            do
            {
                CommandLineUI.DisplayMessage("Please enter in your floor number:");
                floorNo = CommandLineUI.GetInt();

                // Input floor number must be between 1 to 6 as those are the only valid floors.
                if (floorNo >= 1 && floorNo <= 6)
                {
                    bool floorNoAlreadyRegistered = false;

                    // Check each floor manager in the floor manager hospital database list to see if the floor selected is already taken.
                    foreach (FloorManager registeredFloorManagersFloorNo in FloorManagerList)
                    {
                        if (registeredFloorManagersFloorNo._FloorNo == floorNo)
                        {
                            floorNoAlreadyRegistered = true;
                            break;
                        }
                    }

                    // If floor is not already taken, the selected floor number by the user is valid.
                    if (floorNoAlreadyRegistered == false)
                    {
                        validFloorNoFound = true;
                    }
                    else
                    {
                        CommandLineUI.DisplayErrorAgain("Floor has been assigned to another floor manager");
                    }
                }
                // If floor number selected is outside the boundaries (1 to 6) display an error.
                else
                {
                    CommandLineUI.DisplayErrorAgain("Supplied floor is invalid");
                }
            }
            while (validFloorNoFound == false);
            // Return floor number if valid floor number is selected.
            return floorNo;
        }

        /// <summary>
        /// Checks if there is no available floor to assign the floor manager to work on. If there is an available floor, the floor manager can register.
        /// </summary>
        public void RegisterFloorManagerIfFloorNoAvailable(string userType, User registeringUser) 
        {
            string AllFloorsAreAssigned = "All floors are assigned";

            // If floor manager list is 6 or above, indicates all floors are taken by a registered floor manager already.
            if (registeringUser._Hospital._FloorManagerList.Count >= 6)
            {
                CommandLineUI.DisplayError(AllFloorsAreAssigned);
                return;
            }

            // Check if there are any available floors in the list.
            bool floorsAvailable = CheckIfAvailableFloors(registeringUser);

            if (floorsAvailable == true)
            {
                RegisterUserTypeInformation(userType, registeringUser);
                return;
            }
            else
            {
                CommandLineUI.DisplayError(AllFloorsAreAssigned);
                return;
            }
        }

        /// <summary>
        /// Check the hospital database when registering a floor manager to see if there are any available floors of the 6 overall that are available to assign a floor manager to.
        /// </summary>
        /// <returns>
        /// Boolean that indicates true or false if there is an available floor to assign a floor manager to work on.
        /// </returns>
        private bool CheckIfAvailableFloors(User registeringUser)
        {
            // Loops 6 times to check if the floor manager list in the hospital database is not taken by a registered floor manager.
            for (int i = 1; i <= 6; i++)
            {
                bool availableFloor = true;

                foreach (FloorManager registeredFloorManagersAvailableFloor in registeringUser._Hospital._FloorManagerList)
                {
                    if (registeredFloorManagersAvailableFloor._FloorNo == i)
                    {
                        availableFloor = false;
                        break;
                    }
                }
                if (availableFloor == true)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
