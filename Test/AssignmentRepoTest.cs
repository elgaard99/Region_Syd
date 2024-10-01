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
        string connectionString4 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection4"];

        AssignmentRepo SQLRepo, SQLRepo2, SQLRepo3;
        RegionRepo regionRepo;

        List<Region> regions;

        
        Assignment AssignmentA, AssignmentB, AssignmentC, AssignmentD;
        /*
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

            AssignmentA = new Assignment()
            {
                RegionAssignmentId = "13-WX",
                AssignmentType = "C",
                Start = new DateTime(2024, 09, 12, 08, 30, 00),
                Finish = new DateTime(2024, 09, 12, 10, 30, 00),
                Description = "Kr�ver rolig transport",
                IsMatched = false,
                AmbulanceId = "AmMReg12",
                StartAddress = "S�ndergade 20, 8000 Aarhus",
                EndAddress = "Amagerbrogade 12, 1000 Copenhagen",
                StartRegion = regions.Find(r => r.RegionId == 2),
                EndRegion = regions.Find(r => r.RegionId == 1)
            };

            // til update
            AssignmentB = SQLRepo2.GetAll().ToList()[0];
            AssignmentB.IsMatched = false;
            SQLRepo2.Update(AssignmentB);

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
        
        [TestMethod]
        public void GetById_ShouldFindAssignment()
        {

            Assignment found = SQLRepo.GetById(AssignmentA.RegionAssignmentId);
            StringAssert.Equals(AssignmentA.ToString(), found.ToString());

        }

        [TestMethod]
        public void GetAllAssignments()
        {

            var found = SQLRepo2.GetAll().ToList();
            Assert.IsTrue(found.Count<Assignment>() == 17);
            

                // skal nok v�re i en AssignmentTest.cs
            List<int> shouldParseRegions = new List<int> { 0, 2 };
            List<int> b = new List<int> { 1, 3, 5, 7 };
            List<int> c = new List<int> { 0, 2, 4, 6 };
            string[] toTest = new string[] { "99-ZZ", "98-ZZ", "97-ZZ" };

            for(int i=0; i<3; i++)
            {
                List<int> result = found.Find(a => a.RegionAssignmentId == toTest[i]).FindRegionsPassed();
                result.Sort();

                CollectionAssert.AreEqual(shouldParseRegions, result);

                if (i == 0)
                    shouldParseRegions = b;
                else
                    shouldParseRegions = c;
            }
                // herned til
        }

        [TestMethod]
        public void UpdateAssignment()
        {
            Assignment testAssignment = SQLRepo2.GetAll().ToList()[0];
            Assert.IsFalse(testAssignment.IsMatched);

            Assignment updatedAssignment = testAssignment;
            updatedAssignment.IsMatched = true;
            SQLRepo2.Update(updatedAssignment);

            Assignment resultAssignment = SQLRepo2.GetAll().ToList()[0];
            Assert.IsTrue(resultAssignment.IsMatched);
        }
    }
}