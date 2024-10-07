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

        Tour _potentialRepo;

		private ObservableCollection<Assignment> _potentialAssignments;
		public ObservableCollection<Assignment> PotentialAssignments
		{
			get { return _potentialAssignments; }

			set
			{
				_potentialAssignments = value;
				OnPropertyChanged(nameof(PotentialAssignments));
			}
		}

		private ObservableCollection<Assignment> _currentAssignments;
		public ObservableCollection<Assignment> CurrentAssignments
		{
			get => _currentAssignments;
			set
			{
				_currentAssignments = value;
				OnPropertyChanged(nameof(CurrentAssignments)); 
			}
		}


		private Assignment _selectedAssignment;
        private Assignment _assignment1;
        private Assignment _assignment2;

        public AssignmentsViewModel(string connectionString) 
        {
            _regionRepo = new RegionRepo(connectionString);
            _assignmentRepo = new AssignmentRepo(connectionString, _regionRepo.GetAll());
            _potentialRepo = new Tour(_assignmentRepo);

            SetAllAssignments();
            SortAssignmentsByStart();
            CurrentAssignments = AllAssignments;
        }

        public bool CanAddAssignment(Assignment a)
        {
            if (a == null && SelectedAssignment != null) return true;
            
            return false;
        }

        public RelayCommand AddAssignment1Command => 
            new RelayCommand (
                execute => AddAssignment1(), 
                canExecute => CanAddAssignment(Assignment1)
                );
        void AddAssignment1()
        {
            Assignment1 = SelectedAssignment;
            CurrentAssignments = new ObservableCollection<Assignment> (_potentialRepo.CheckForPotentialMatchesForTour(Assignment1, AllAssignments.ToList()));
        }
        public RelayCommand AddAssignment2Command =>
            new RelayCommand(
                execute => Assignment2 = SelectedAssignment,
                canExecute => CanAddAssignment(Assignment2) && Assignment1 != null
				);

        public RelayCommand RemoveAssignment1Command =>
           new RelayCommand(
               execute => RemoveAssignment1(),
               canExecute => Assignment1 != null && Assignment2 == null
               );
        void RemoveAssignment1()
        {
            Assignment1 = null;

			_potentialRepo.PotentialAssignments.Clear(); //Ellers er der mange på lige pludselig
			_potentialRepo.FreeRegionsPassed = new bool[8]; //Ellers kan der være trues tilbage fra tidligere

			SetAllAssignments ();
			SortAssignmentsByStart();
			CurrentAssignments = AllAssignments;

		}

        public RelayCommand RemoveAssignment2Command =>
           new RelayCommand(
               execute => RemoveAssignment2(),
               canExecute => Assignment2 != null
               );
        void RemoveAssignment2()
        {
            Assignment2 = null;
			CurrentAssignments = new ObservableCollection<Assignment>(_potentialRepo.CheckForPotentialMatchesForTour(Assignment1, AllAssignments.ToList()));
		}
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
            { CurrentAssignments.Remove(newAssignment); }
            
            else { CurrentAssignments.Add(assignment); } // hvis den slettes, tilføjes den til listview
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

        public void CombineAssignments()
        {
            Assignment a1, a2;
            a1 = Assignment1;
            a2 = Assignment2;

            Assignment1 = null;
            Assignment2 = null;

            _assignmentRepo.ReassignAmbulance(a1, a2);
            _regionRepo.Update(a1.StartRegion);
            _regionRepo.Update(a2.StartRegion);
            SetAllAssignments();
            SortAssignmentsByStart();
            CurrentAssignments = AllAssignments;
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
        public void ResetDBToDummyData()
        {
            _assignmentRepo.ResetDBToDummyData();
        }
    }
}
