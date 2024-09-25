using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Region_Syd.Model;
using Region_Syd.ViewModel;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class AssignmentRepoTest
    {
        string cs = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), "testDB.db");

        string cs1 = @"Server=rene-server1.database.windows.net;
                    Database=Sander;
                    Trusted_Connection=False;
                    User Id=rene-server1Admin;
                    Password=DatabaseEr1Fase!;";


        //Assignment AssignmentA, AssignmentB, AssignmentC, AssignmentD;
        //AssignmentRepo SQLRepo;
        //int totalCountOfAssignments;
        ////Leger her---------------------------------------------------------------
        //public void CountNumberOfRowsInASSIGNMENTSTable() 
        //{
        //    string query = @"SELECT COUNT(*) FROM ASSIGNMENTS";

        //    using (SqlConnection connection = new SqlConnection(cs))
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);
        //        connection.Open();
        //        totalCountOfAssignments = (int)command.ExecuteScalar() -1000; 
        //    }
        //}
        [TestInitialize]
        public void Init()
        {
           

            //SQLRepo = new AssignmentRepo(cs);
            //for (int i = 0; i < 3; i++) 
            //{
            //    SQLRepo.testAllAssignments[i].IsMatched = false;
            //}
            //AssignmentA = SQLRepo.testAllAssignments[0];
            //AssignmentB = SQLRepo.testAllAssignments[1];
            //AssignmentC = SQLRepo.testAllAssignments[2];
            //AssignmentD = SQLRepo.testAllAssignments[3];
            //CountNumberOfRowsInASSIGNMENTSTable();
        }

        //[TestMethod]

        //public void ReAssignAmbulanceTest()
        //{

        //    SQLRepo.ReassignAmbulance(AssignmentA, AssignmentB);
        //    //Assert
        //    Assert.AreEqual(AssignmentA.AmbulanceId, AssignmentB.AmbulanceId);
        //}
        //[TestMethod]
        //public void SetIsMatchedTrueTest()
        //{
        //    SQLRepo.SetIsMatchedTrue(AssignmentA, AssignmentB);
        //    Assert.IsTrue(AssignmentA.IsMatched == true);
        //    Assert.IsTrue(AssignmentB.IsMatched == true);
        //}

        //[TestMethod]
        //public void shouldFindNothing_WhenAssignmentDoesNotExist()
        //{
        //    Assignment nonExistingAssignment = new Assignment() { RegionAssignmentId = "-1" };
        //    var found = SQLRepo.GetById(nonExistingAssignment.RegionAssignmentId);
        //    Assert.IsNull(found);
        //}

        //[TestMethod]
        //public void shouldFindAssignment_WhenAssignmentExist()
        //{
        //    Assignment found = SQLRepo.GetById(AssignmentC.RegionAssignmentId);
        //    StringAssert.Equals(AssignmentC.ToString(), found.ToString());
        //    //Assert.AreEqual<string>(AssignmentD.ToString(), found.ToString());

        //    // Er de ikke ens ??
        //    // Expected:<33-CD, Sygehusvej 10, 4000 Roskilde, Testrupvej 56, 8320 Mårslet, 05.09.2024 11.00.00, 05.09.2014 13.30.00, Kræver ilt i ambulancen, C, RSj, RM, True, AmCReg2>.
        //    //   Actual:<33-CD, Sygehusvej 10, 4000 Roskilde, Testrupvej 56, 8320 Mårslet, 05.09.2024 11.00.00, 05.09.2024 13.30.00, Kræver ilt i ambulancen, C, RSj, RM, True, AmCReg2>. 

        //}

        //[TestMethod]
        //public void GetAllAssignments()
        //{

        //    IEnumerable<Assignment> found = SQLRepo.GetAll();
        //    Assert.IsTrue(found.Count<Assignment>() == totalCountOfAssignments);
        //}
        //[TestMethod]
        //public void UpdateAssignment()
        //{
        //    AssignmentC.AmbulanceId = "changed";
        //    AssignmentC.IsMatched = true;
        //    SQLRepo.Update(AssignmentC);
        //    SQLRepo.GetById(AssignmentC.RegionAssignmentId);
        //    Assert.AreEqual(SQLRepo.GetById(AssignmentC.RegionAssignmentId).IsMatched, true);
        //    Assert.AreEqual(SQLRepo.GetById(AssignmentC.RegionAssignmentId).AmbulanceId, "changed");
        //}

        //[TestMethod]
        //public void CanConnect()
        //{
        //    AssignmentRepo myRepo = new AssignmentRepo(cs);
        //    bool connected = myRepo.ConnectionSucceded();

        //    Assert.IsTrue(connected);
        //}

        [TestMethod]
        public void CanRead()
        {
            AssignmentRepo myRepo = new AssignmentRepo(cs);
            List<int> connected = myRepo.ReadData();

            Assert.IsTrue(connected.Count == 1);
        }
    }
}