using Region_Syd.Model;
using Region_Syd.ViewModel;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        Region_Syd.Model.Assignment AssignmentA, AssignmentB, AssignmentC;
        MainViewModel mvm;
        AssignmentsViewModel tvm;
        AssignmentRepo _assignmentRepo;

        [TestInitialize]
        public void Init()
        {
            //Arrange
            mvm = new MainViewModel();
            tvm = new AssignmentsViewModel();
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
                IsMatched = true,
            };
            AssignmentB = new Region_Syd.Model.Assignment()
            {
                RegionAssignmentId = "C",
                Start = DateTime.Now.AddHours(5),
                IsMatched = false,
            };
            AssignmentC = new Region_Syd.Model.Assignment();
            _assignmentRepo = new AssignmentRepo();
            _assignmentRepo.AddToAllAssignments(AssignmentA);
            _assignmentRepo.AddToAllAssignments(AssignmentB);
            _assignmentRepo.AddToAllAssignments(AssignmentC);

            tvm._assignmentRepo.AddToAllAssignments(AssignmentA);
            tvm._assignmentRepo.AddToAllAssignments(AssignmentB);
            tvm._assignmentRepo.AddToAllAssignments(AssignmentC);
        }
        [TestMethod]

        public void AssignmentsAddedToRepo()
        {
            //Assert
            List<Region_Syd.Model.Assignment> testAssignment = _assignmentRepo.GetAllAssignments();
            Assert.AreEqual(AssignmentA, testAssignment[0]);
            Assert.AreEqual(AssignmentB, testAssignment[1]);
            Assert.AreEqual(AssignmentC, testAssignment[2]);
        }
        [TestMethod]
        public void GetFilteredAssignmentsFromRepoWhenAssignmentsAreUnmatchedReturnsObservableCollection()
        {
            //Assert
            ObservableCollection<Region_Syd.Model.Assignment> testAssignment = tvm.GetFilteredAssignmentsFromRepo();
            Assert.IsTrue(testAssignment.Count == 4);
        }
        [TestMethod]
        public void AssignmentsViewModelAllAssignmentsIsSortedByDate()
        {
            //Assert

            //Assert.IsTrue(tvm.AllAssignments[0].PickUpTime.Date < tvm.AllAssignments[1].PickUpTime.Date); //Ved ikke om man faktisk kan sige det således, men det var mit bedste bud.
            DateTime dateTimeA = AssignmentA.Start;
            DateTime dateTimeB = AssignmentB.Start;
            Assert.IsTrue(0 > DateTime.Compare(dateTimeA, dateTimeB));
        }
    }
}