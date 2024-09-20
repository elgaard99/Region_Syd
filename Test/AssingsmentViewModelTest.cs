﻿using Region_Syd.Model;
using Region_Syd.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class AssingsmentViewModelTest
    {
        Region_Syd.Model.Assignment AssignmentA, AssignmentB, AssignmentC;
        MainViewModel mvm;
        AssignmentsViewModel avm;

        [TestInitialize]
        public void Init()
        {
            //Arrange
            mvm = new MainViewModel();
            avm = new AssignmentsViewModel();
            AssignmentA = new Region_Syd.Model.Assignment() 
            {
                RegionAssignmentId = "A",
                Start = DateTime.Now,
                IsMatched = true,
            };
            AssignmentB = new Region_Syd.Model.Assignment()
            {
                RegionAssignmentId = "B",
                Start = DateTime.Now.AddHours(1),
                IsMatched = false,
            };
            AssignmentC = new Region_Syd.Model.Assignment()
            {
                RegionAssignmentId = "C",
                Start = DateTime.Now.AddHours(5),
                IsMatched = false,
            };
            
            //tror vi er nødt til enten at lave det public eller lave en setter. Alternativt lave et test objekt der er public eller har setter (fx. testAssignemntRepo)
            //tvm._assignmentRepo = _assignmentRepo;
            avm.TestAssignmentRepo.AddToAllAssignments(AssignmentA);
            avm.TestAssignmentRepo.AddToAllAssignments(AssignmentB);
            avm.TestAssignmentRepo.AddToAllAssignments(AssignmentC);
        }
        [TestMethod]
        public void GetFilteredAssignmentsFromRepoWhenAssignmentsAreUnmatchedReturnsObservableCollection()
        {
            //Assert
            ObservableCollection<Region_Syd.Model.Assignment> testAssignment = avm.GetFilteredAssignmentsFromRepo();
            Assert.IsTrue(testAssignment.Count == 4);
        }
        [TestMethod]
        public void AssignmentsViewModelAllAssignmentsIsSortedByDate()
        {
            //Assert
            //Assert.IsTrue(tvm.AllAssignments[0].PickUpTime.Date < tvm.AllAssignments[1].PickUpTime.Date); //Ved ikke om man faktisk kan sige det således, men det var mit bedste bud.
            DateTime dateTimeA = AssignmentA.Start;
            DateTime dateTimeB = AssignmentB.Start;
            //Forslag til rettelse
            int countOfAssignments = avm.AllAssignments.Count;
            DateTime lastAssignmentStart = avm.AllAssignments[countOfAssignments - 1].Start; //sætter det til den sidste item i listen
            DateTime secondLastStart = avm.AllAssignments[countOfAssignments - 2].Start; //sætter til ANDEN sidste item i listen
            Assert.IsTrue(0 > DateTime.Compare(secondLastStart, lastAssignmentStart));
        }
        [TestMethod]
        public void CombineAssignmentsTest()
        {
            avm.UpdateAllAssignments();
            int CountBefore = avm.AllAssignments.Count;
            avm.CombineAssignments(AssignmentB, AssignmentC);
            //Assert
            Assert.IsTrue(CountBefore > avm.AllAssignments.Count);
        }
    }
}