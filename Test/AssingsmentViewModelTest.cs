using Region_Syd.Model;
using Region_Syd.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace Test
{
    [TestClass]
    public class AssingsmentViewModelTest
    {
        string connectionString = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];
        string connectionString2 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection2"];
        string connectionString3 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection3"];
        string connectionString4 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection4"];

        Region_Syd.Model.Assignment AssignmentA, AssignmentB, AssignmentC;
        MainViewModel mvm;
        AssignmentsViewModel avm;
     
        [TestInitialize]
        public void Init()
        {
            //Arrange
            mvm = new MainViewModel();
            avm = new AssignmentsViewModel(connectionString2);

            AssignmentA = avm.AllAssignments[1];
            AssignmentB = avm.AllAssignments[2];
            AssignmentC = avm.AllAssignments[3];
        }
        [TestMethod]
        public void SetAllAssignments()
        {
            //Assert
            foreach (Assignment assignment in avm.AllAssignments)
            {
                Assert.IsFalse(assignment.IsMatched);
            }
        }
        [TestMethod]
        public void SortByStart()
        {
            //Assert
            for (int i = 1; i < avm.AllAssignments.Count; i++)
            {
                if (avm.AllAssignments[i-1].StartRegion == avm.AllAssignments[i].StartRegion)
                {
                    Assert.IsTrue(DateTime.Compare(avm.AllAssignments[i - 1].Start, avm.AllAssignments[i].Start) > 0);
                }
                
            }
        }

        [TestMethod]
        public void CombineAssignmentsTest()
        {
            //arrange
            int CountBefore = avm.AllAssignments.Count;
            
            avm.Assignment1 = AssignmentA;
            avm.Assignment2 = AssignmentB;
                        
            avm.CombineAssignments();            
            //Assert
            Assert.IsTrue(CountBefore > avm.AllAssignments.Count);
        }
    }
}
