using Region_Syd.Model;
using Region_Syd.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class AssingsmentViewModelTest
    {
        Region_Syd.Model.Assignment AssignmentA, AssignmentB, AssignmentC;
        MainViewModel mvm;
        AssignmentsViewModel avm;

        string connectionString = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];
        string connectionString2 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection2"];
        string connectionString3 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection3"];
        string connectionString4 = BaseTest.InitConfiguration().GetSection("ConnectionStrings")["TestConnection4"];

        [TestInitialize]
        public void Init()
        {
            //Arrange
            mvm = new MainViewModel();
            avm = new AssignmentsViewModel(connectionString3);
            
            //tror vi er nødt til enten at lave det public eller lave en setter. Alternativt lave et test objekt der er public eller har setter (fx. testAssignemntRepo)
            //tvm._assignmentRepo = _assignmentRepo;
            avm.TestAssignmentRepo.AddToAllAssignments(AssignmentA);
            avm.TestAssignmentRepo.AddToAllAssignments(AssignmentB);
            avm.TestAssignmentRepo.AddToAllAssignments(AssignmentC);

            avm.GetFilteredAssignmentsFromRepo();
            //sørger for at assignment A, B og C kommer videre fra _allAssignments i AssignmentRepo til
            //AllAssignments i AssignmentsViewModel så countBefore bliver korrekt i CombineAssignmentsTest /cla
        }
        [TestMethod]
        public void GetFilteredAssignmentsFromRepoWhenAssignmentsAreUnmatchedReturnsObservableCollection()
        {
            //Assert
            //ObservableCollection<Assignment> testAssignment = avm.GetFilteredAssignmentsFromRepo();
            //Assert.IsTrue(testAssignment.Count == 4);
        }
        [TestMethod]
        public void AssignmentsViewModelAllAssignmentsIsSortedByDate()
        {
            //Assert
            //Assert.IsTrue(tvm.AllAssignments[0].PickUpTime.Date < tvm.AllAssignments[1].PickUpTime.Date); //Ved ikke om man faktisk kan sige det således, men det var mit bedste bud.
            DateTime dateTimeA = AssignmentA.Start;
            DateTime dateTimeB = AssignmentB.Start;
            //Forslag til rettelse
            int countOfAssignments = avm.AllAssignments.Count;
            DateTime lastAssignmentStart = avm.AllAssignments[countOfAssignments - 1].Start; //sætter det til den sidste item i listen
            DateTime secondLastStart = avm.AllAssignments[countOfAssignments - 2].Start; //sætter til ANDEN sidste item i listen
            Assert.IsTrue(0 > DateTime.Compare(secondLastStart, lastAssignmentStart));
        }
        [TestMethod]
        public void CombineAssignmentsTest()
        {
            //arrange
            int CountBefore = avm.AllAssignments.Count;
            
            avm.Assignment1 = AssignmentC;
            avm.Assignment2 = AssignmentB;
                        
            avm.CombineAssignments();            
            //Assert
            Assert.IsTrue(CountBefore > avm.AllAssignments.Count);
        }
    }
}
