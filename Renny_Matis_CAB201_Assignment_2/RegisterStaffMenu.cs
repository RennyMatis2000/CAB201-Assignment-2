using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// A child class of Register Menu, handles ensuring that while registering a staff member, a valid staffID is input.
    /// </summary>
    public abstract class RegisterStaffMenu : RegisterMenu
    {
        /// <summary>
        /// A protected method to be used by staff employed at the hospital. Prompts the user to enter an int between 100 to 999 that is unregistered. If incorrect, will prompt the user to continuously retry while displaying an error.
        /// </summary>
        /// <param name="StaffList">
        /// Staff list as a parameter to ensure that no user at the hospital has already registered the input staffID integer.
        /// </param>
        /// <returns>
        /// Returns a valid staffID integer.
        /// </returns>
        protected int GetValidStaffID(List<Staff> StaffList)
        {
            int staffID;
            bool validStaffIDFound = false;

            // Continue loop until a valid staffID is found.
            do
            {
                CommandLineUI.DisplayMessage("Please enter in your staff ID:");
                staffID = CommandLineUI.GetInt();

                // staffID must be an integer between 100 and 999.
                if (staffID >= 100 && staffID <= 999)
                {
                    bool staffIDAlreadyRegistered = false;

                    // Loop through relevant staff list to check if the staffID is already registered.
                    foreach (Staff staff in StaffList)
                    {
                        if (staff._StaffID == staffID)
                        {
                            staffIDAlreadyRegistered = true;
                            break;
                        }
                    }

                    // If staffID is not already registered, staffID selected is valid. Display error if incorrect.
                    if (staffIDAlreadyRegistered == false)
                    {
                        validStaffIDFound = true;
                    }
                    else
                    {
                        CommandLineUI.DisplayErrorAgain("Staff ID is already registered");
                    }
                }
                else // Display error if integer input is out of bounds.
                {
                    CommandLineUI.DisplayErrorAgain("Supplied staff identification number is invalid");
                }

            }
            while (validStaffIDFound == false);
            // Return staffID if valid integer selected.
            return staffID;
        }
    }
}
