using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    internal class Tour
    {
        private string _tourId;

        public string TourId
        {
            get { return _tourId; }
            set { _tourId = value; }
        }

        public List<Assignment> TourAssignments = new List<Assignment>();

        public void AddToTourAssignments(Assignment assignment)
        {
            TourAssignments.Add(assignment);

            int count = 0; //Counter til at bestemme arraypladsen
            foreach (bool i in assignment.RegionsPassed) //Går igennem arrayen fra assimentment og tjekker hvilke regioner
                                                         //assignment går igennem, disse muligheder fjernes så fra
                                                         //tilgængeligheden på en given del af turen. 
            {
                if (i == true)
                { FreeRegionsPassed[count] = false; }
                

                
                count++;
            }
        }
        public List<Assignment> GetTourAssignments()
        {
            return TourAssignments;
        }
        public void RemovePlannedAssignment(Assignment assignment)
        {
            TourAssignments.Remove(assignment);
            
            int count = 0; //Counter til at bestemme arraypladsen
            foreach (bool i in assignment.RegionsPassed) //Går igennem arrayen fra assimentment og tjekker hvilke regioner
                                                         //assignment går igennem, disse muligheder fjernes så fra
                                                         //tilgængeligheden på en given del af turen. 
            {
                if (i == true)
                { FreeRegionsPassed[count] = true; }

                count++;
            }

        }


        private bool[] _freeRegionsPassed; 
        // En Tour med 1 assignmentment fra Nord til Syd vil være False på 0 og 2
        // da den her er optaget af en opgave. På 3 og 1 vil den være true for at indikere tilgængelighed/tom ambulance
        // (Se Enum RegionBorderCross som går fra 0-7)  

        public bool[] FreeRegionsPassed
        {
            get { return _freeRegionsPassed; }
            set { _freeRegionsPassed = value; }
        }

    }
}
