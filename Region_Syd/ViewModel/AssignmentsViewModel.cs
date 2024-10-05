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
        private Assignment _assignment3;

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

        //FULL AUTO
        public bool CanFullAuto(ObservableCollection<Assignment> aList)
        {
            if (aList != null) return true;

            return false;
        }


        public RelayCommand FullAutoCommand =>
        new RelayCommand(
            execute => FullAuto1(),
            canExecute => CanFullAuto(AllAssignments)
            );
        
        void FullAuto1()
        {
            

            int countFAVM = 0;
            while (countFAVM < AllAssignments.Count)
            {
                var twoAssignments = _potentialRepo.FullAutoMatchesForTours(AllAssignments.ToList());
                if (twoAssignments.bestMatch == null)
                { break; } //Skal breake ud, fordi hvis bestMatch er returneret som null er listen gennemtjekket
                
                FullAutoCombineAssignments(twoAssignments.mostTrue, twoAssignments.bestMatch);
                countFAVM++;
            }


        }

        public void FullAutoCombineAssignments(Assignment a1, Assignment a2)
        {
            Assignment a3 = null;
            if (a1 != null && a2 != null) 
            {
                
                _assignmentRepo.ReassignAmbulance(a1, a2, a3);
                _regionRepo.Update(a1.StartRegion);
                _regionRepo.Update(a2.StartRegion);

                SetAllAssignments();
                SortAssignmentsByStart();
                CurrentAssignments = AllAssignments;

            }
            
        }



        public RelayCommand AddAssignment1Command => 
            new RelayCommand (
                execute => AddAssignment1(), 
                canExecute => CanAddAssignment(Assignment1)
                );
        void AddAssignment1()
        {
            Assignment1 = SelectedAssignment;
            CurrentAssignments = new ObservableCollection<Assignment> (_potentialRepo.CheckForPontialMatchesForTour(Assignment1, AllAssignments.ToList()));
        }
        public RelayCommand AddAssignment2Command =>
            new RelayCommand(
                execute => AddAssignment2(),
                canExecute => CanAddAssignment(Assignment2) && Assignment1 != null
				);
        void AddAssignment2()
        {
            List<Assignment> a1Matches = CurrentAssignments.ToList();
            Assignment2 = SelectedAssignment;
            CurrentAssignments = new ObservableCollection<Assignment>(_potentialRepo.Add2Tour(Assignment1, Assignment2, a1Matches));
        }

        public RelayCommand AddAssignment3Command =>
            new RelayCommand(
                execute => Assignment3 = SelectedAssignment,
                canExecute => CanAddAssignment(Assignment3) && Assignment2 != null && Assignment1 != null
                );



        public RelayCommand RemoveAssignment1Command =>
           new RelayCommand(
               execute => RemoveAssignment1(),
               canExecute => Assignment1 != null && Assignment2 == null
               );
        void RemoveAssignment1()
        {
            Assignment1 = null;

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
			CurrentAssignments = new ObservableCollection<Assignment>(_potentialRepo.CheckForPontialMatchesForTour(Assignment1, AllAssignments.ToList()));
		}

        public RelayCommand RemoveAssignment3Command =>
           new RelayCommand(
               execute => RemoveAssignment3(),
               canExecute => Assignment3 != null
               );
        void RemoveAssignment3()
        {
            Assignment3 = null;
            CurrentAssignments = new ObservableCollection<Assignment>(_potentialRepo.Add2Tour(Assignment1, Assignment2, AllAssignments.ToList()));
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

        public Assignment Assignment3
        {
            get { return _assignment3; }
            set
            {
                CheckIfAssigned(_assignment3, value);

                _assignment3 = value;
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
            Assignment a1, a2, a3;
            a1 = Assignment1;
            a2 = Assignment2;
            a3 = Assignment3;  

            Assignment1 = null;
            Assignment2 = null;
            Assignment3 = null;

            _assignmentRepo.ReassignAmbulance(a1, a2, a3);
            _regionRepo.Update(a1.StartRegion);
            _regionRepo.Update(a2.StartRegion);
            _regionRepo.Update(a3.StartRegion);
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
    }
}
