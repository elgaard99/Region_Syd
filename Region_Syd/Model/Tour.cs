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

        private bool[] _freeRegionsPassed = new bool[8];
        // En Tour med 1 assignmentment fra Nord til Syd vil være False på 0 og 2
        // da den her er optaget af en opgave. På 3 og 1 vil den være true for at indikere tilgængelighed/tom ambulance
        // (Se Enum RegionBorderCross som går fra 0-7)  

        public bool[] FreeRegionsPassed
        {
            get { return _freeRegionsPassed; }
            set { _freeRegionsPassed = value; }
        }

        public List<Assignment> TourAssignments = new List<Assignment>();

        public void AddToTourAssignments(Assignment assignment)
        {
            TourAssignments.Add(assignment);

            bool[] regionsPassedArray = assignment.RegionsPassed as bool[]; //Caster RegionsPassed af object typen til en bool array type 



            if (FreeRegionsPassed == null) //Tjekker om det er den første Assignment der bliver sat ind på touren
            {
                int count = 0; //Counter til at bestemme arraypladsen
                foreach (bool i in regionsPassedArray) //Går igennem arrayen fra assimentment og tjekker hvilke regioner
                                                       //assignment går igennem, disse muligheder fjernes så fra
                                                       //tilgængeligheden på en given del af turen. 
                {
                    if (i == true) //Hvis assignment arrayet-indexet er true...
                    {
                        FreeRegionsPassed[count] = false; //... sæt samme index i tour til false

                        if (0 == (count % 2)) //Hvis value er et lige tal (retning fra nord mod hovedstaden))
                        { FreeRegionsPassed[count + 1] = false; } //retning fra nord mod hov
                        else { FreeRegionsPassed[count - 1] = false; } //retning fra hov mod nord


                    }



                    count++;
                }
            }
            else
            {
                int count = 0; //Counter til at bestemme arraypladsen
                foreach (bool i in regionsPassedArray) //Går igennem arrayen fra assimentment og tjekker hvilke regioner
                                                       //assignment går igennem, disse muligheder fjernes så fra
                                                       //tilgængeligheden på en given del af turen. 
                {
                    if (i == true) //Hvis assignment arrayet-indexet er true...
                    {
                        FreeRegionsPassed[count] = false;

                    }
                    count++;
                }
            }

        }


        public List<Assignment> GetTourAssignments()
        {
            return TourAssignments;
        }
        public void RemovePlannedAssignment(Assignment assignment)
        {
            TourAssignments.Remove(assignment);

            bool[] regionsPassedArray = assignment.RegionsPassed as bool[]; //Caster RegionsPassed af object typen til en bool array type 

            int count = 0; //Counter til at bestemme arraypladsen
            foreach (bool i in regionsPassedArray) //Går igennem arrayen fra assimentment og tjekker hvilke regioner
                                                         //assignment går igennem, disse muligheder fjernes så fra
                                                         //tilgængeligheden på en given del af turen. 
            {
                if (i == true)
                { FreeRegionsPassed[count] = true; }

                count++;
            }

        }


        


        public List<Assignment> CheckForPontialMatchesForTour()
        {
            // Ikke implementeret endnu
            
            //Get all metode til at se alle opgaver for den givne dag

            List<Assignment> assignments = AssignmentRepo.GetAllAssignments(); 
            
            var dayThenMostTrue = (List<Assignment>)assignments //sorterer efter dag, og derefter hvilken på assignment der har flest trues 
                .OrderBy(a => a.Start.Day)
                .ThenByDescending(a => ((bool[])a.RegionsPassed).Count(b => b));

            AddToTourAssignments(dayThenMostTrue[0]); //Den første på den sorterede liste bliver den første assignment i Tour

            var datePotentials = dayThenMostTrue.Where(a => a.Start.Day == dayThenMostTrue[0].Start.Day);
            //var dateAndRoutePotentials = datePotentials.Where(a => a.RegionsPassed == FreeRegionsPassed);

            List<Assignment> result = new List<Assignment>();


            List<int> indices = new List<int>(); //Liste til at putte index tal ind på
            for (int i = 0; i < FreeRegionsPassed.Length; ++i)
            {
                if (FreeRegionsPassed[i])
                {
                    indices.Add(i);
                }
            }

            foreach (Assignment a in datePotentials) // for hver assignment på dagen
            {
                foreach (var index in indices) // for hver tilængelige plads i touren
                {
                    if (a.RegionsPassed is bool[] regionsPassedArray) // casting fra object til array
                    {
                        if (regionsPassedArray[index]) 
                        {
                            result.Add(a); //adder til liste over potentielle assignments
                        }
                    

                    }

                    
                    
                }
            }


            



            return result; 
        }

    }
}
