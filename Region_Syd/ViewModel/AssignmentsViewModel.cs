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
    public class AssignmentsViewModel : ViewModelBase
    {

        AssignmentRepo _assignmentRepo;
        public AssignmentRepo TestAssignmentRepo { get { return _assignmentRepo; } private set { _assignmentRepo = value; } }

        RegionRepo _regionRepo;

        private ObservableCollection<Assignment> _allAssignments;
        public ObservableCollection<Assignment> AllAssignments 
        {
            get { return _allAssignments; } 
            
            set 
            { 
                _allAssignments = value; 
                OnPropertyChanged(nameof(AllAssignments));
            }
        }
        
        private Assignment _selectedAssignment;
        private Assignment _assignment1;
        private Assignment _assignment2;

        public AssignmentsViewModel(string connectionString) 
        {
            _regionRepo = new RegionRepo(connectionString);
            _assignmentRepo = new AssignmentRepo(connectionString, _regionRepo.GetAll());

            SetAllAssignments();
            SortAssignmentsByStart();
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
               canExecute => Assignment1 != null && Assignment2 != null && DoAssignmentsOverlap() == true
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

			var sortedAssignments = AllAssignments
                    .OrderBy(assignment => assignment.StartRegion.Name)
                    .ThenBy(assignment => assignment.Start)
                    .ToList();
			if (!AllAssignments.SequenceEqual(sortedAssignments))
			{
				AllAssignments = new ObservableCollection<Assignment>(sortedAssignments); 
			}

		}

        public void SetAllAssignments()
        {
            List<Assignment> _listOfAssignments = _assignmentRepo.GetAll().ToList();
            AllAssignments = new ObservableCollection<Region_Syd.Model.Assignment>(_listOfAssignments.Where(assignment => !assignment.IsMatched && (assignment.AssignmentType == "C" || assignment.AssignmentType == "D")));// !assignment betyder er false, uden ! finder den true. 

        }

        public ObservableCollection<Assignment> GetSuggestions()
        {
            return null;
        }


        public void CombineAssignments()
        {
            Assignment a1, a2;
            a1 = Assignment1;
            a2 = Assignment2;

            Assignment1 = null;
            Assignment2 = null;

            _assignmentRepo.ReassignAmbulance(a1, a2);            
            SetAllAssignments();
            SortAssignmentsByStart();
        }
        
        private bool DoAssignmentsOverlap()
        {
            List<Assignment> sortedByDateTime = new List<Assignment>();
            sortedByDateTime.Add(Assignment1);
            sortedByDateTime.Add(Assignment2);
            sortedByDateTime.OrderBy(assignment => assignment.Start);
            /*hvis den tidligste er færdig før den sidste kan den tage turen,*/
            if (DateTime.Compare(sortedByDateTime[0].Finish, sortedByDateTime[1].Start) > 0) {  return false; }
            else { return true; }
        }
    }
}
