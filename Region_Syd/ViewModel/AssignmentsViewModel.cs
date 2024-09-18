using Region_Syd.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Region_Syd.Model;
using System.Windows;
using System.ComponentModel;

namespace Region_Syd.ViewModel
{
    public class AssignmentsViewModel : ViewModelBase // skal den ikke være internal eller private??
    {
        AssignmentRepo _assignmentRepo;
        public AssignmentRepo TestAssignmentRepo { get { return _assignmentRepo; } set { _assignmentRepo = value; } }

        AmbulanceRepo _ambulanceRepo;

        public ObservableCollection<Assignment> AllAssignments 
        { 
            get; 
            
            set;
        }

        public ObservableCollection <Ambulance> AllAmbulances {  get; }

        private Assignment _selectedAssignment;
        private Assignment _assignment1;
        private Assignment _assignment2;

        public AssignmentsViewModel() 
        {
            _assignmentRepo = new AssignmentRepo();
            AmbulanceRepo ambulanceRepo = new AmbulanceRepo();
            UpdateAllAssignments();
            SortAssignmentsByStart();


            //AllAssignments = //Skal vise en sorteret ObservableCollection 
            //AllAmbulances = new ObservableCollection<Ambulance>();


        }

        public bool CanAddAssignment(Assignment a)
        {
            if (a == null && SelectedAssignment != null) return true;
            
            return false;
        }

        public RelayCommand AddAssignment1Command => 
            new RelayCommand (
                execute => Assignment1 = SelectedAssignment, 
                canExecute => CanAddAssignment(Assignment1)
                );

        public RelayCommand AddAssignment2Command =>
            new RelayCommand(
                execute => Assignment2 = SelectedAssignment,
                canExecute => CanAddAssignment(Assignment2)
                );

        public RelayCommand RemoveAssignment1Command =>
           new RelayCommand(
               execute => Assignment1 = null,
               canExecute => Assignment1 != null
               );

        public RelayCommand RemoveAssignment2Command =>
           new RelayCommand(
               execute => Assignment2 = null,
               canExecute => Assignment2 != null
               );

        public RelayCommand CombineAssignmentsCommand =>
           new RelayCommand(
               execute => CombineAssignments(),
               canExecute => Assignment1 != null && Assignment2 != null
               );

        public Assignment SelectedAssignment
        {
            get { return _selectedAssignment; }
            set
            {
                _selectedAssignment = value;
                OnPropertyChanged();
            }
        }

        public void CheckIfAssigned(Assignment assignment, Assignment? newAssignment)
        {
            if (assignment == null) // hvis en opgave vælges, fjernes den fra listview
            { AllAssignments.Remove(newAssignment); }
            
            else { AllAssignments.Add(assignment); } // hvis den slettes, tilføjes den til listview
        }

        public Assignment Assignment1
        {
            get { return _assignment1; }
            set
            {
                CheckIfAssigned(_assignment1, value);

                _assignment1 = value;
                OnPropertyChanged();
            }
        }

        public Assignment Assignment2
        {
            get { return _assignment2; }
            set
            {
                CheckIfAssigned(_assignment2, value);

                _assignment2 = value;
                OnPropertyChanged();
            }
        }

        public void SortAssignmentsByStart()
        {

            AllAssignments.OrderBy(assignment => assignment.Start);

        }

        public ObservableCollection<Assignment> GetFilteredAssignmentsFromRepo(/*DateTime? pickUpTime = null, ClassOfAssignment? classOfAssignment = null, Region? fromRegion = null, *//*Region? toRegion = null*//* bool isMatched = false*/)
        {
            List<Region_Syd.Model.Assignment> _listOfAssignments = _assignmentRepo.GetAllAssignments();
            var worklist = new ObservableCollection<Assignment>(_listOfAssignments.Where(assignment => !assignment.IsMatched)); // !assignment betyder er false, uden ! finder den true. 
            

            return worklist;
        }

        public ObservableCollection<Ambulance> GetAmbulancesFromRepo()
        {
            throw new NotImplementedException();
        }

        public void CombineAssignments()
        {
            Assignment a1, a2;
            a1 = Assignment1;
            a2 = Assignment2;

            Assignment1 = null;
            Assignment2 = null;

            _assignmentRepo.ReassignAmbulance(a1, a2);
            UpdateAllAssignments();
            SortAssignmentsByStart();
        }
        /*
        public void CantCombine()
        {
            MessageBox.Show("Denne kombination er ikke mulig.", "Kombinationsfejl", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        */

        public void UpdateAllAssignments() //Skulle denne ikke indeholde både GetFiltered og SortBy?
        {
            AllAssignments = GetFilteredAssignmentsFromRepo();
        }
    }
}
