using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// Program class to instantiate an object of the program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method to run the program.
        /// </summary>
        /// <param name="args">
        /// Holds the arguments in the program.
        /// </param>
        public static void Main(string[] args)
        {
            // Instantiate a menu and execute Run from the Menu class to run the program.
            Menu menu1 = new Menu();
            menu1.Run();
        }
    }
}