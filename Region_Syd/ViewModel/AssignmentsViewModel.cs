﻿using Region_Syd.View;
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
            GetFilteredAssignmentsFromRepo();
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

        public void CheckIfAssigned(Assignment a, Assignment? newAssignment)
        {
            if (a == null) // hvis en opgave vælges, fjernes den fra listview
            { AllAssignments.Remove(newAssignment); }
            else { AllAssignments.Add(a); } // hvis den slettes, tilføjes den til listview
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

        public void GetFilteredAssignmentsFromRepo(/*DateTime? pickUpTime = null, ClassOfAssignment? classOfAssignment = null, Region? fromRegion = null, *//*Region? toRegion = null*//* bool isMatched = false*/)
        {
            List<Region_Syd.Model.Assignment> _listOfAssignments = _assignmentRepo.GetAllAssignments();
			AllAssignments = new ObservableCollection<Region_Syd.Model.Assignment>(_listOfAssignments.Where(assignment => !assignment.IsMatched)); // !assignment betyder er false, uden ! finder den true. 
            
        }

        public ObservableCollection<Ambulance> GetAmbulancesFromRepo()
        {
            throw new NotImplementedException();
        }

        public void CombineAssignments()
        {
                _assignmentRepo.ReassignAmbulance(Assignment1, Assignment2);
			    GetFilteredAssignmentsFromRepo();
			    SortAssignmentsByStart();
            Assignment1 = null;
            Assignment2 = null;
        }
        /*
        public void CantCombine()
        {
            MessageBox.Show("Denne kombination er ikke mulig.", "Kombinationsfejl", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        */
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
