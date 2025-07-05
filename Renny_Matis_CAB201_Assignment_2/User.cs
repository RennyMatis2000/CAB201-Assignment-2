using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// The user class is for the object of a user that is using the program, and is used throughout the entire program dependent on who the user is.
    /// </summary>
    public class User
    {
        private string Name;
        private int Age;
        private string MobileNo;
        private string Email;
        private string Password;
        private Hospital SelectedHospital;
        private User LoggedInUser;

        /// <summary>
        /// The default public constructor of user.
        /// </summary>
        /// <param name="name">
        /// Name of user.
        /// </param>
        /// <param name="age">
        /// Age of user.
        /// </param>
        /// <param name="mobileNo">
        /// Mobile number of user.
        /// </param>
        /// <param name="email">
        /// Uniquely identifying email of user.
        /// </param>
        /// <param name="password">
        /// Password of user.
        /// </param>
        /// <param name="hospital">
        /// The hospital the user is within.
        /// </param>
        public User(string name, int age, string mobileNo, string email, string password, Hospital hospital)
        {
            Name = name;
            Age = age;
            MobileNo = mobileNo;
            Email = email;
            Password = password;
            SelectedHospital = hospital;
        }

        /// <summary>
        /// Returns the private name value as a public value to retrieve throughout the program.
        /// </summary>
        public string _Name
        {
            get { return Name; }
        }

        /// <summary>
        /// Returns the private age value as a public value to retrieve throughout the program.
        /// </summary>
        public int _Age
        {
            get { return Age; }
        }

        /// <summary>
        /// Returns the private email value as a public value to retrieve throughout the program.
        /// </summary>
        public string _Email
        {
            get { return Email; }
        }

        /// <summary>
        /// Returns the private password value as a public value to retrieve throughout the program.
        /// </summary>
        public string _Password
        {
            get { return Password; }
        }

        /// <summary>
        ///  Returns the private mobile number value as a public value to retrieve throughout the program.
        /// </summary>
        public string _MobileNo
        {
            get { return MobileNo; }
        }

        /// <summary>
        /// Returns the private hospital value as a public value to retrieve throughout the program.
        /// </summary>
        public Hospital _Hospital
        {
            get { return SelectedHospital; }
            set { SelectedHospital = value; }
        }

        /// <summary>
        /// A method that allows for users and inherited user classes to change their password to a new password.
        /// </summary>
        public void ChangePassword()
        {
            CommandLineUI.DisplayMessage("Enter new password:");
            string newPassword = CommandLineUI.GetString();
            Password = newPassword;
            CommandLineUI.DisplayMessage("Password has been changed.");
        }

        /// <summary>
        /// A virtual method that displays the user's current registered details.
        /// </summary>
        public virtual void DisplayUserDetails()
        {
            // Display the user's details using their user fields.
            CommandLineUI.DisplayMessage("Your details.");
            CommandLineUI.DisplayMessage($"Name: {Name}");
            CommandLineUI.DisplayMessage($"Age: {Age}");
            CommandLineUI.DisplayMessage($"Mobile phone: {MobileNo}");
            CommandLineUI.DisplayMessage($"Email: {Email}");
        }

    }
}
