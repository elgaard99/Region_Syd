using Region_Syd.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Region_Syd.Model;
using System.Windows;

namespace Region_Syd.ViewModel
{
    public class AssignmentsViewModel
    {
        AssignmentRepo _assignmentRepo;
        AmbulanceRepo _ambulanceRepo;

        public ObservableCollection<Region_Syd.Model.Assignment> AllAssignments 
        { 
            get; 
            
            set;
        }


        public ObservableCollection <Ambulance> AllAmbulances {  get; }


        public AssignmentsViewModel() 
        {
            _assignmentRepo = new AssignmentRepo();
            AmbulanceRepo ambulanceRepo = new AmbulanceRepo();
            UpdateAllAssignments();
            SortAssignmentsByStart();


            //AllAssignments = //Skal vise en sorteret ObservableCollection 
            //AllAmbulances = new ObservableCollection<Ambulance>();


        }

        public void SortAssignmentsByStart()
        {

            AllAssignments.OrderBy(assignment => assignment.Start);

        }

        public ObservableCollection<Region_Syd.Model.Assignment> GetFilteredAssignmentsFromRepo(/*DateTime? pickUpTime = null, ClassOfAssignment? classOfAssignment = null, Region? fromRegion = null, *//*Region? toRegion = null*//* bool isMatched = false*/)
        {
            List<Region_Syd.Model.Assignment> _listOfAssignments = _assignmentRepo.GetAllAssignments();
            var worklist = new ObservableCollection<Region_Syd.Model.Assignment>(_listOfAssignments.Where(assignment => !assignment.IsMatched)); // !assignment betyder er false, uden ! finder den true. 
            

            return worklist;
        }

        public ObservableCollection<Ambulance> GetAmbulancesFromRepo()
        {
            throw new NotImplementedException();
        }

        public void CombineAssignments(Assignment assignment1, Assignment assignment2)
        {
                _assignmentRepo.ReassignAmbulance(assignment1, assignment2);
                _assignmentRepo.SetIsMatchedTrue(assignment1, assignment2);
                UpdateAllAssignments();
                SortAssignmentsByStart();
        }
        /*
        public void CantCombine()
        {
            MessageBox.Show("Denne kombination er ikke mulig.", "Kombinationsfejl", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        */

        public void UpdateAllAssignments()
        {
            AllAssignments = GetFilteredAssignmentsFromRepo();
        }
    }
}
