using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Region_Syd.Model;
using Region_Syd.ViewModel;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class AssignmentRepoTest
    {
        string cs = @"Server=rene-server1.database.windows.net;
                    Database=Sander;
                    Trusted_Connection=False;
                    User Id=rene-server1Admin;
                    Password=DatabaseEr1Fase!;";


        Assignment AssignmentA, AssignmentB, AssignmentC, AssignmentD;
        AssignmentRepo /*_assignmentRepo,*/ SQLRepo;
        int totalCountOfAssignments;
        //Leger her---------------------------------------------------------------
        public void CountNumberOfRowsInASSIGNMENTSTable() 
        {
            string query = @"SELECT COUNT(*) FROM ASSIGNMENTS";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                totalCountOfAssignments = (int)command.ExecuteScalar();
            }
        }
        [TestInitialize]
        public void Init()
        {
            /*AssignmentA = new Region_Syd.Model.Assignment()
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
            AssignmentD = new Assignment()
            {
                RegionAssignmentId = "33-CD",
                AmbulanceId = "AmCReg2",
                StartAddress = "Sygehusvej 10, 4000 Roskilde",
                EndAddress = "Testrupvej 56, 8320 Mårslet",
                Start = new DateTime(2024, 09, 05, 11, 00, 00),
                Finish = new DateTime(2014, 09, 05, 13, 30, 00),
                Description = "Kræver ilt i ambulancen",
                AssignmentType = AssignmentTypeEnum.C,
                StartRegion = RegionEnum.RSj,
                EndRegion = RegionEnum.RM,
                IsMatched = true
            };*/
            //_assignmentRepo = new AssignmentRepo(cs);
            /*_assignmentRepo.AddToAllAssignments(AssignmentA);
            _assignmentRepo.AddToAllAssignments(AssignmentB);
            _assignmentRepo.AddToAllAssignments(AssignmentC);

            _assignmentRepo.AddToAllAssignments(AssignmentA);
            _assignmentRepo.AddToAllAssignments(AssignmentB);
            _assignmentRepo.AddToAllAssignments(AssignmentC);*/

            SQLRepo = new AssignmentRepo(cs);
            for (int i = 0; i < 4; i++) //For at være sikker på de første 4 er IsMatched = false, så vi kan teste på disse
            {
                SQLRepo.testAllAssignments[i].IsMatched = false;
            }
            AssignmentA = SQLRepo.testAllAssignments[0];
            AssignmentB = SQLRepo.testAllAssignments[1];
            AssignmentC = SQLRepo.testAllAssignments[2];
            AssignmentD = SQLRepo.testAllAssignments[3];
            CountNumberOfRowsInASSIGNMENTSTable();
        }
        /* [TestMethod]

         public void AssignmentsAddedToRepo()
         {
             //Assert
             List<Region_Syd.Model.Assignment> testAssignment = _assignmentRepo.GetAllAssignments();
             Assert.AreEqual(AssignmentA, testAssignment[3]);
             Assert.AreEqual(AssignmentB, testAssignment[4]);
             Assert.AreEqual(AssignmentC, testAssignment[5]);
         }*/
        [TestMethod]

        public void ReAssignAmbulanceTest()
        {
            
            SQLRepo.ReassignAmbulance(AssignmentA, AssignmentB);
            //Assert
            Assert.AreEqual(AssignmentA.AmbulanceId, AssignmentB.AmbulanceId);
        }
        [TestMethod]
        public void SetIsMatchedTrueTest()
        {
            SQLRepo.SetIsMatchedTrue(AssignmentA, AssignmentB);
            Assert.IsTrue(AssignmentA.IsMatched == true);
            Assert.IsTrue(AssignmentB.IsMatched == true);
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
            StringAssert.Equals(AssignmentD.ToString(), found.ToString());
            //Assert.AreEqual<string>(AssignmentD.ToString(), found.ToString());

            // Er de ikke ens ??
            // Expected:<33-CD, Sygehusvej 10, 4000 Roskilde, Testrupvej 56, 8320 Mårslet, 05.09.2024 11.00.00, 05.09.2014 13.30.00, Kræver ilt i ambulancen, C, RSj, RM, True, AmCReg2>.
            //   Actual:<33-CD, Sygehusvej 10, 4000 Roskilde, Testrupvej 56, 8320 Mårslet, 05.09.2024 11.00.00, 05.09.2024 13.30.00, Kræver ilt i ambulancen, C, RSj, RM, True, AmCReg2>. 

        }

        [TestMethod]
        public void GetAllAssignments()
        {

            IEnumerable<Assignment> found = SQLRepo.GetAll();
            Assert.IsTrue(found.Count<Assignment>() == totalCountOfAssignments);
        }
        [TestMethod]
        public void UpdateAssignment()
        {
            AssignmentD.AmbulanceId = "changed";
            AssignmentD.IsMatched = true;
            SQLRepo.Update(AssignmentD);
            SQLRepo.GetById(AssignmentD.RegionAssignmentId);
            Assert.AreEqual(SQLRepo.GetById(AssignmentD.RegionAssignmentId).IsMatched, true);
            Assert.AreEqual(SQLRepo.GetById(AssignmentD.RegionAssignmentId).AmbulanceId, "changed");
        }
    }
}