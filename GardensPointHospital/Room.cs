using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// Room represents an object of a room that can be occupied by a patient within the hospital.
    /// </summary>
    public class Room
    {
        private int RoomNo;
        private int RoomFloorNo;
        private bool RoomFull;
        private Patient RoomOccupant;

        /// <summary>
        /// Returns the private room number value as a public room number to be accessed in other classes of the program.
        /// </summary>
        public int _RoomNo
        {
            get { return RoomNo; }
        }

        /// <summary>
        /// Returns the private boolean value of room full as a public bool to be accessed and modified in other classes of the program.
        /// </summary>
        public bool _RoomFull
        {
            get { return RoomFull; }
            set { RoomFull = value; }
        }

        /// <summary>
        /// Returns the private floor number value as a public floor number to be accessed in other classes of the program.
        /// </summary>
        public int _RoomFloorNo
        {
            get { return RoomFloorNo; }
        }

        /// <summary>
        /// Returns the private patient value as a public patient to be accessed and modified in other classes of the program.
        /// </summary>
        public Patient _RoomOccupant
        {
            get { return RoomOccupant; }
            set { RoomOccupant = value; }
        }

        /// <summary>
        /// To instantiate an object of a room.
        /// </summary>
        /// <param name="roomNo">
        /// The room number of the room
        /// </param>
        /// <param name="roomFloorNo">
        /// The floor number of the room.
        /// </param>
        /// <param name="roomFull">
        /// Whether the room is occupied by a patient or not.
        /// </param>
        /// <param name="roomOccupant">
        /// The patient that is occupying the room.
        /// </param>
        public Room(int roomNo, int roomFloorNo, bool roomFull, Patient roomOccupant)
        {
            RoomNo = roomNo;
            RoomFloorNo = roomFloorNo;
            RoomFull = roomFull;
            RoomOccupant = roomOccupant;
        }

    }
}
