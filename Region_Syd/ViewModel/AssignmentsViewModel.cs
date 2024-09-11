using Region_Syd.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Region_Syd.Model;

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
            AllAssignments = GetFilteredAssignmentsFromRepo();
            
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

    }
}
