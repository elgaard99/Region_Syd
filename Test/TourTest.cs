using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Region_Syd.Model;

namespace Test
{

    [TestClass]
    internal class TourTest
    {
        string connectionString = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];
        string connectionString2 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection2"];
        string connectionString3 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection3"];
        string connectionString4 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection4"];

        AssignmentRepo SQLRepo, SQLRepo2, SQLRepo3;
        RegionRepo regionRepo;

        List<Region> regions;

        Assignment AssignmentA, AssignmentB, AssignmentC, AssignmentD, AssignmentE; 

        [TestInitialize]
        public void Init()
        {
            regionRepo = new RegionRepo(connectionString);
            regions = regionRepo.GetAll().ToList();

            SQLRepo = new AssignmentRepo(connectionString, regions);
            SQLRepo2 = new AssignmentRepo(connectionString2, regions);
            SQLRepo3 = new AssignmentRepo(connectionString3, regions);

            AssignmentA = new Assignment() // Den første som touren skal baseres på
            {

                Start = new DateTime(2024, 09, 12, 08, 30, 00),
                Finish = new DateTime(2024, 09, 12, 10, 30, 00),
                
                StartRegion = regions.Find(r => r.RegionId == 3),
                EndRegion = regions.Find(r => r.RegionId == 1)
            };

            AssignmentB = new Assignment() // 3 matches
            {
                
                AssignmentType = AssignmentTypeEnum.C,
                Start = new DateTime(2024, 09, 12, 08, 30, 00),
                Finish = new DateTime(2024, 09, 12, 10, 30, 00),
                
                StartRegion = regions.Find(r => r.RegionId == 2),
                EndRegion = regions.Find(r => r.RegionId == 1)
            };

            AssignmentC = new Assignment() // 2 matches
            {
               
                AssignmentType = AssignmentTypeEnum.C,
                Start = new DateTime(2024, 09, 12, 08, 30, 00),
                Finish = new DateTime(2024, 09, 12, 10, 30, 00),
               
                StartRegion = regions.Find(r => r.RegionId == 2),
                EndRegion = regions.Find(r => r.RegionId == 1)
            };

            AssignmentD = new Assignment() // 1 match
            {
                
                AssignmentType = AssignmentTypeEnum.C,
                Start = new DateTime(2024, 09, 12, 08, 30, 00),
                Finish = new DateTime(2024, 09, 12, 10, 30, 00),
                
                StartRegion = regions.Find(r => r.RegionId == 2),
                EndRegion = regions.Find(r => r.RegionId == 1)
            };

            AssignmentE = new Assignment() //Ikke samme dato, skal sorteres fra
            {
                
                AssignmentType = AssignmentTypeEnum.C,
                Start = new DateTime(2024, 09, 12, 08, 30, 00),
                Finish = new DateTime(2024, 09, 12, 10, 30, 00),
                
                StartRegion = regions.Find(r => r.RegionId == 2),
                EndRegion = regions.Find(r => r.RegionId == 1)
            };



        }
    }
}
