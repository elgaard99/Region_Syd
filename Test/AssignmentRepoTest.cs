using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Region_Syd.Model;
using Region_Syd.ViewModel;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace Test
{
    // Idé - Vi laver en metode der "resetter" vores DB til udgangspunktet - altså vores dummy data, så data altid er det samme udgangspunkt i testen.
    [TestClass]
    public class AssignmentRepoTest
    {

        string connectionString = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];
        string connectionString2 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection2"];
        string connectionString3 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection3"];
        string connectionString4 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection4"];

        AssignmentRepo assignmentRepo, assignmentRepo2, assignmentRepo3;
        RegionRepo regionRepo;

        List<Region> regions;
        List<Assignment> assignments;
        
        Assignment AssignmentA, AssignmentB, AssignmentC, AssignmentD;
        
        int totalCountOfAssignments;
        public void CountNumberOfRowsInASSIGNMENTSTable()
        {
            string query = @"SELECT COUNT(*) FROM Assignments";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                totalCountOfAssignments = (int)Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public void ReturnAssignmentWhereSetIsMatchedIsFalse()
        {
            List<Assignment> tempAssignments = assignmentRepo.GetAll().ToList();
            int j = 0;

            for (int i = 0; i<totalCountOfAssignments; i++)
            {
                if (tempAssignments[i].IsMatched == false && tempAssignments[i].AssignmentType == "D" && j<4) {
                    assignments.Add(tempAssignments[i]);
                    j++;
                }
                else if (tempAssignments[i].IsMatched == false && tempAssignments[i].AssignmentType == "C" && j < 4)
                {
                    assignments.Add(tempAssignments[i]);
                    j++;
                }
                else if (j > 3)
                {
                    i = totalCountOfAssignments-1;
                }
            }
            if (assignments.Count <4)
            {
                throw new Exception("Ikke nok data til at teste");
            }
        }

        [TestInitialize]
        public void Init()
        {
            regionRepo = new RegionRepo(connectionString);
            regions = regionRepo.GetAll().ToList();

            assignmentRepo = new AssignmentRepo(connectionString, regions);
            assignmentRepo2 = new AssignmentRepo(connectionString2, regions);
            assignmentRepo3 = new AssignmentRepo(connectionString3, regions);

            // til update
            /*AssignmentB = assignmentRepo2.GetAll().ToList()[0];
            AssignmentB.IsMatched = false;
            assignmentRepo2.Update(AssignmentB);

            
            for (int i = 0; i < 3; i++)
            {
                assignmentRepo.testAllAssignments[i].IsMatched = false;
            }*/
            CountNumberOfRowsInASSIGNMENTSTable();

            assignments = new List<Assignment>();
            ReturnAssignmentWhereSetIsMatchedIsFalse();

            AssignmentA = assignments[0];
            AssignmentB = assignments[1];
            //AssignmentC = assignments[2];
            //AssignmentD = assignments[3];

            
        }

        
        [TestMethod]

        public void ReAssignAmbulanceTest()
        {
            
            assignmentRepo.ReassignAmbulance(AssignmentA, AssignmentB);
            //Assert
            Assert.AreEqual(AssignmentA.AmbulanceId, AssignmentB.AmbulanceId);
        }
        [TestMethod]
        public void SetIsMatchedTrueTest()
        {
            assignmentRepo.SetIsMatchedTrue(AssignmentA, AssignmentB);
            Assert.IsTrue(AssignmentA.IsMatched == true);
            Assert.IsTrue(AssignmentB.IsMatched == true);
        }
        

        [TestMethod]
        public void GetById_ShouldFindNothing()
        {

            Assignment nonExistingAssignment = new Assignment() { RegionAssignmentId = "-1" };
            var found = assignmentRepo.GetById(nonExistingAssignment.RegionAssignmentId);
            Assert.IsNull(found);

        }
        
        [TestMethod]
        public void GetById_ShouldFindAssignment()
        {

            Assignment found = assignmentRepo.GetById(AssignmentA.RegionAssignmentId);
            StringAssert.Equals(AssignmentA.ToString(), found.ToString());

        }
        
        [TestMethod]
        public void GetAllAssignments()
        {

            IEnumerable<Assignment> found = assignmentRepo2.GetAll();
            Assert.IsTrue(found.Count<Assignment>() == totalCountOfAssignments);

        }

        [TestMethod]
        public void UpdateAssignment()
        {
            Assignment testAssignment = AssignmentA;
            /*Assert.IsFalse(testAssignment.IsMatched);

            Assignment updatedAssignment = testAssignment;
            AssignmentA.IsMatched = false;*/
            if (AssignmentA.IsMatched == false) {
                AssignmentA.IsMatched = true;
            }
            else {
                AssignmentA.IsMatched = false;
            }
            assignmentRepo2.Update(AssignmentA);
            Assert.IsFalse(testAssignment.IsMatched == AssignmentA.IsMatched);
           /* Assignment resultAssignment = assignmentRepo2.GetAll().ToList()[0];*/
            
        }

        /*
        [TestMethod] // Hvad er dette????
        public void GetRegion()
        {

            string found = assignmentRepo.GetRegion();
            StringAssert.Equals(found, "Hovedsatden");

        }
        /*
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