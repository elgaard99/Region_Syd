using Region_Syd;
using Region_Syd.Model;

namespace Test
{
    [TestClass]
    public class RegionRepoTest
    {

        string connectionString = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];
        string connectionString2 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection2"];
        string connectionString3 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection3"];
                
        RegionRepo SQLRepo, SQLRepo2, SQLRepo3;
        


        [TestInitialize]
        public void Init()
        {
            SQLRepo = new RegionRepo(connectionString);
            SQLRepo2 = new RegionRepo(connectionString2);
            SQLRepo3 = new RegionRepo(connectionString3);

            // til update
            Region testRegion = SQLRepo2.GetAll().ToList()[0];
            testRegion.HoursSaved = 0;
            SQLRepo2.Update(testRegion);

            // til calculate total
            IEnumerable<Region> testRegions3 = SQLRepo3.GetAll();
            foreach (Region region in testRegions3)
            {
                region.HoursSaved = 1;
                SQLRepo3.Update(region);
            }
        }

        

        [TestMethod]
        public void GetAllRegions()
        {

            IEnumerable<Region> found = SQLRepo.GetAll();
            Assert.IsTrue(found.Count<Region>() == 5);

        }
        [TestMethod]
        public void UpdateRegions()
        {
            Region testRegion = SQLRepo2.GetAll().ToList()[0];
            Assert.IsFalse(testRegion.HoursSaved == 2.2);
            Region updatedRegion = testRegion;
            updatedRegion.HoursSaved = 2.2;
            SQLRepo2.Update(updatedRegion);
            Region resultRegion = SQLRepo2.GetAll().ToList()[0];
            Assert.IsTrue(resultRegion.HoursSaved == 2.2);
        }
        [TestMethod]
        public void TestCalculate()
        {
            Region testRegion = SQLRepo3.CalculateTotalSavings();
            StringAssert.Equals(testRegion.Name, "Danmark");
            Assert.IsTrue(testRegion.HoursSaved == 5);
        }
    }
}