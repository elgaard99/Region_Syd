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

        string connectionString = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];
        string connectionString2 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection2"];
        string connectionString3 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection3"];

        AssignmentRepo SQLRepo, SQLRepo2, SQLRepo3;
        RegionRepo regionRepo;

        List<Region> regions;

        /*
        Assignment AssignmentA, AssignmentB, AssignmentC, AssignmentD;
        int totalCountOfAssignments;
        //Leger her---------------------------------------------------------------
        public void CountNumberOfRowsInASSIGNMENTSTable()
        {
            string query = @"SELECT COUNT(*) FROM ASSIGNMENTS";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                totalCountOfAssignments = (int)command.ExecuteScalar() - 1000;
            }
        }
        */

        [TestInitialize]
        public void Init()
        {
            regionRepo = new RegionRepo(connectionString);
            regions = regionRepo.GetAll().ToList();

            SQLRepo = new AssignmentRepo(connectionString, regions);
            SQLRepo2 = new AssignmentRepo(connectionString2, regions);
            SQLRepo3 = new AssignmentRepo(connectionString3, regions);

            /*
            for (int i = 0; i < 3; i++)
            {
                SQLRepo.testAllAssignments[i].IsMatched = false;
            }
            AssignmentA = SQLRepo.testAllAssignments[0];
            AssignmentB = SQLRepo.testAllAssignments[1];
            AssignmentC = SQLRepo.testAllAssignments[2];
            //AssignmentD = SQLRepo.testAllAssignments[3];
            CountNumberOfRowsInASSIGNMENTSTable();
            */

        }

        /*
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
        */

        [TestMethod]
        public void GetById_ShouldFindNothing()
        {

            Assignment nonExistingAssignment = new Assignment() { RegionAssignmentId = "-1" };
            var found = SQLRepo.GetById(nonExistingAssignment.RegionAssignmentId);
            Assert.IsNull(found);

        }
        /*
        [TestMethod]
        public void GetById_ShouldFindAssignment()
        {

            Assignment found = SQLRepo.GetById(AssignmentC.RegionAssignmentId);
            StringAssert.Equals(AssignmentC.ToString(), found.ToString());

        }

        [TestMethod]
        public void GetAllAssignments()
        {

            IEnumerable<Assignment> found = SQLRepo.GetAll();
            Assert.IsTrue(found.Count<Assignment>() == 2);

        }

        [TestMethod]
        public void GetRegion()
        {

            string found = SQLRepo.GetRegion();
            StringAssert.Equals(found, "Hovedsatden");

        }

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
        */
    }
}