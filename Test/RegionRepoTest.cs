using Region_Syd;
using Region_Syd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class RegionRepoTest
    {
        string connectionString = @"Data Source=C:\Users\Bruger\Source\Repos\elgaard99\Region_Syd\Region_Syd\testDB.db";
        RegionRepo SQLRepo;

        [TestInitialize]
        public void Init()
        {
            SQLRepo = new RegionRepo(connectionString);

    
        }

        

        [TestMethod]
        public void GetAllRegions()
        {

            IEnumerable<Region> found = SQLRepo.GetAll();
            Assert.IsTrue(found.Count<Region>() == 5);

        }

        /*[TestMethod]
        public void GetRegion()
        {

            string found = SQLRepo.GetRegion();
            StringAssert.Equals(found, "Hovedsatden");

        }
        */
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
    }
}