using Region_Syd.Model;
using Region_Syd.ViewModel;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class AssignmentRepoTest
    {
        Region_Syd.Model.Assignment AssignmentA, AssignmentB, AssignmentC;
        AssignmentRepo _assignmentRepo;

        [TestInitialize]
        public void Init()
        {
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
                AmbulanceId = "B",
            };
            AssignmentC = new Region_Syd.Model.Assignment()
            {
                RegionAssignmentId = "C",
                Start = DateTime.Now.AddHours(5),
                IsMatched = false,
                AmbulanceId = "C",
            };
            _assignmentRepo = new AssignmentRepo();
            _assignmentRepo.AddToAllAssignments(AssignmentA);
            _assignmentRepo.AddToAllAssignments(AssignmentB);
            _assignmentRepo.AddToAllAssignments(AssignmentC);

            _assignmentRepo.AddToAllAssignments(AssignmentA);
            _assignmentRepo.AddToAllAssignments(AssignmentB);
            _assignmentRepo.AddToAllAssignments(AssignmentC);
        }
        [TestMethod]

        public void AssignmentsAddedToRepo()
        {
            //Assert
            List<Region_Syd.Model.Assignment> testAssignment = _assignmentRepo.GetAllAssignments();
            Assert.AreEqual(AssignmentA, testAssignment[3]);
            Assert.AreEqual(AssignmentB, testAssignment[4]);
            Assert.AreEqual(AssignmentC, testAssignment[5]);
        }
        [TestMethod]

        public void ReAssignAmbulanceTest()
        {
            _assignmentRepo.ReassignAmbulance(AssignmentB, AssignmentC);
            //Assert
            Assert.AreEqual(AssignmentB.AmbulanceId, AssignmentC.AmbulanceId);
        }
        [TestMethod]
        public void SetIsMatchedTrueTest()
        {
            _assignmentRepo.SetIsMatchedTrue(AssignmentC, AssignmentB);
            Assert.IsTrue(AssignmentB.IsMatched == true);
            Assert.IsTrue(AssignmentC.IsMatched == true);
        }
    }
}