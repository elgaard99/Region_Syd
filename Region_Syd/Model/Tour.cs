﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Region_Syd.Model
{
    internal class Tour
    {

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
		public List<Assignment> PotentialAssignments = new List<Assignment>();


		AssignmentRepo _assignmentRepo;

        public Tour(AssignmentRepo repo) { _assignmentRepo = repo; }

        public void AddToTourAssignments(Assignment assignment)
        {
            TourAssignments.Add(assignment);

            bool[] regionsPassedArray = assignment.RegionsPassed as bool[]; //Caster RegionsPassed af object typen til en bool array type 

            bool alleFalse = true; 
            foreach (bool i in FreeRegionsPassed)  
            {
                if (i == true) //Hvis assignment arrayet-indexet er true...
                {
                    alleFalse = false;
                    break;
                }
                
            }




            if (alleFalse) //Tjekker om det er den første Assignment der bliver sat ind på touren
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
                        { FreeRegionsPassed[count + 1] = true; } //retning fra nord mod hov
                        else { FreeRegionsPassed[count - 1] = true; } //retning fra hov mod nord


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


		public List<Assignment> SortAssignmentsByPotential(List<Assignment> PA)
		{
			var result = PA
                .OrderByDescending(a => ((bool[])a.RegionsPassed).Count(b => b == true)) //Sorterer listen efter dem med flest trues, altså de bedste matches i toppen
                .ThenBy(a => a.Start);
            return result.ToList();
		}



		public List<Assignment> CheckForPotentialMatchesForTour(Assignment assignment, List<Assignment> assignments)
        {
            //List<Assignment> assignments = _assignmentRepo.GetAll().ToList(); //Dem der er isMatched false
           
            AddToTourAssignments(assignment); //assignment bliver sat ind som første element på Touren

            List<Assignment> datePotentials = assignments.FindAll(a => a.Start.Day == assignment.Start.Day && a.Start > assignment.Finish); //Finder de assignments der er samme dag og Efter assignments sluttid.


            //(TourArray)Liste til at putte index tal ind på, for at have en liste med tilgængeligheden som vi kan sammenligne med de potentielle assignments arrays
            List<int> tourIndices = new List<int>(); 
            for (int i = 0; i < FreeRegionsPassed.Length; ++i)
            {
                if (FreeRegionsPassed[i]) //Hvis index i på arrayet er true...
                {
                    tourIndices.Add(i); //...add index i til indices listen
                }
            }

            




            foreach (Assignment a in datePotentials) // for hver assignment på dagen
            {
                
                    //(AssignmentArray)Liste til at putte index tal ind på (bliver brugt i linje 172)
                    List<int> assignmentIndices = new List<int>();

                    if (a.RegionsPassed is bool[] regionsPassedArray) // casting fra object til array
                    {
                        //Herunder putter vi assignments true-placeringer ind på listen assignmentIndices
                        for (int i = 0; i < regionsPassedArray.Length; ++i)
                        {
                            if (regionsPassedArray[i]) //Hvis index i på arrayet er true...
                            {
                                assignmentIndices.Add(i); //...add index i til indices listen
                            }
                        }

                    }


                    // Her sammenligner vi de to indices lister
                    // Fra Christian: 
                    // "Første tal samme lighed, første tal skal være større eller lig med, sum skal være mindre eller lig med"
                    if ((tourIndices[0] % 2) == (assignmentIndices[0] % 2)) 
                    {
                        if (tourIndices[0] <= assignmentIndices[0])  
                        {
                                if (tourIndices.Sum() >= assignmentIndices.Sum() && !PotentialAssignments.Any(pa => pa.RegionAssignmentId == a.RegionAssignmentId)) //
                                    { PotentialAssignments.Add(a); }
                        }
                    }

                
            }

            return SortAssignmentsByPotential(PotentialAssignments);

            // return PotentialAssignments; 
        }




        public List<Assignment> FullAutoMatchesForTours()
        {
            List<Assignment> assignments = _assignmentRepo.GetAll().ToList(); //Dem der er isMatched false

            var dayThenMostTrue = (List<Assignment>)assignments //sorterer efter dag, og derefter hvilken på assignment der har flest trues 
                .OrderBy(a => a.Start.Day)
                .ThenByDescending(a => ((bool[])a.RegionsPassed).Count(b => b));

            AddToTourAssignments(dayThenMostTrue[0]); //Den første på den sorterede liste bliver den første assignment i Tour

            var datePotentials = assignments.Where(a => a.Start.Day == dayThenMostTrue[0].Start.Day && a.Start > dayThenMostTrue[0].Finish);

            List<Assignment> AutoPotentialAssignments = new List<Assignment>();


            List<int> indices = new List<int>(); //Liste til at putte index tal ind på
            for (int i = 0; i < FreeRegionsPassed.Length; ++i)
            {
                if (FreeRegionsPassed[i]) //Hvis index i på arrayet er true...
                {
                    indices.Add(i); //...add index i til indices listen
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
                            AutoPotentialAssignments.Add(a); //adder til liste over potentielle assignments
                        }
                    }

                }
            }

            return AutoPotentialAssignments;
        }

    }
}
