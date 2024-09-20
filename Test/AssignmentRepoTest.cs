using Region_Syd.Model;
using Region_Syd.ViewModel;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class AssignmentRepoTest
    {
        string cs = @"Server=rene-server1.database.windows.net;
                    Database=test;
                    Trusted_Connection=False;
                    User Id=rene-server1Admin;
                    Password=DatabaseEr1Fase!;";


        Assignment AssignmentA, AssignmentB, AssignmentC, AssignmentD;
        AssignmentRepo _assignmentRepo, SQLRepo;

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
            _assignmentRepo = new AssignmentRepo(cs);
            _assignmentRepo.AddToAllAssignments(AssignmentA);
            _assignmentRepo.AddToAllAssignments(AssignmentB);
            _assignmentRepo.AddToAllAssignments(AssignmentC);

            _assignmentRepo.AddToAllAssignments(AssignmentA);
            _assignmentRepo.AddToAllAssignments(AssignmentB);
            _assignmentRepo.AddToAllAssignments(AssignmentC);

            SQLRepo = new AssignmentRepo(cs);

            AssignmentD = new Assignment()
            {
                RegionAssignmentId = "33-CD",
                AmbulanceId = "AMCReg3",
                StartAddress = "startTES",
                EndAddress = "endTEST",
                Start = new DateTime(2024, 09, 05, 11, 30, 00),
                Finish = new DateTime(2012, 09, 18, 18, 34, 00),
                Description = "MIG",
                AssignmentType = AssignmentTypeEnum.C,
                StartRegion = (RegionEnum)1,
                EndRegion = (RegionEnum)2,
                IsMatched = true
            };

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

        [TestMethod]
        public void shouldFindNothing_WhenAssignmentDoesNotExist()
        {
            Assignment nonExistingAssignment = new Assignment() { RegionAssignmentId = "-1" };
            var found = SQLRepo.GetById(nonExistingAssignment.RegionAssignmentId);
            Assert.IsNull(found);
        }

        [TestMethod]
        public void shouldFindAssignment_WhenAssignmentExist()
        {
            Assignment found = SQLRepo.GetById(AssignmentD.RegionAssignmentId);
            Assert.AreEqual(AssignmentD.ToString(), found.ToString());
        }
    }
}