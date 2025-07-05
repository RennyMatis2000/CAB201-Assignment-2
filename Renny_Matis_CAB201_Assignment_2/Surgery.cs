using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardensPointHospitalFinal4
{
    /// <summary>
    /// Surgery class represents a surgery object. This is used for the floor manager to instantiate a surgery with the time, surgeon, patient, and whether the surgery has been conducted.
    /// </summary>
    public class Surgery
    {
        private DateTime SurgeryTime;
        private Surgeon SelectedSurgeon;
        private Patient PatientWithSurgery;
        private bool ConductedSurgery;

        /// <summary>
        /// Returns the private surgery time value as public.
        /// </summary>
        public DateTime _SurgeryTime
        {
            get { return SurgeryTime; }
        }

        /// <summary>
        /// Returns the private patient that has a surgery value as public.
        /// </summary>
        public Patient _PatientWithSurgery
        {
            get { return PatientWithSurgery; }
        }

        /// <summary>
        /// Returns the selected surgeon for the surgery value as public.
        /// </summary>
        public Surgeon _SelectedSurgeon
        {
            get { return SelectedSurgeon; }
        }

        /// <summary>
        /// Returns whether the private boolean value of whether the surgery has been conducted as public.
        /// </summary>
        public bool _ConductedSurgery
        {
            get { return ConductedSurgery; }
            set { ConductedSurgery = value; }
        }

        /// <summary>
        /// Default public constructor for a surgery.
        /// </summary>
        /// <param name="surgeryTime">
        /// When the surgery will occur as a DateTime.
        /// </param>
        /// <param name="selectedSurgeon">
        /// Surgeon conducting the surgery.
        /// </param>
        /// <param name="patientWithSurgery">
        /// Patient that will have the surgery.
        /// </param>
        /// <param name="conductedSurgery">
        /// Boolean whether the surgery has been completed.
        /// </param>
        public Surgery(DateTime surgeryTime, Surgeon selectedSurgeon, Patient patientWithSurgery, bool conductedSurgery = false)
        {
            SurgeryTime = surgeryTime;
            SelectedSurgeon = selectedSurgeon;
            PatientWithSurgery = patientWithSurgery;
            ConductedSurgery = conductedSurgery;
        }
    }
}
