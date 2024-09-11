using Region_Syd.Model;
using Region_Syd.ViewModel;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        Region_Syd.Model.Task TaskA, TaskB, TaskC;
        MainViewModel mvm;
        TasksViewModel tvm;

        [TestInitialize]
        public void Init()
        {
            //Arrange
            mvm = new MainViewModel();
            tvm = new TasksViewModel();
            TaskA = new Region_Syd.Model.Task(); //Der er ikke lavet en konstruktor så der kan være der skal sættes parameter ind her
            TaskB = new Region_Syd.Model.Task(); //Der er ikke lavet en konstruktor så der kan være der skal sættes parameter ind her
            TaskC = new Region_Syd.Model.Task(); //Der er ikke lavet en konstruktor så der kan være der skal sættes parameter ind her
            tvm.taskRepo.AddToAllTasks(TaskA);
            tvm.taskRepo.AddToAllTasks(TaskB);
            tvm.taskRepo.AddToAllTasks(TaskC);
        }
        [TestMethod]

        public void TasksAddedToRepo()
        {
            //Assert
            List<Region_Syd.Model.Task> testTask = tvm.taskRepo.GetAllTasks();
            Assert.AreEqual(TaskA, testTask[0]);
            Assert.AreEqual(TaskB, testTask[1]);
            Assert.AreEqual(TaskC, testTask[2]);
        }
        [TestMethod]
        public void RepoTaskEqualsTasksViewModel()
        {
            //Assert
            List<Region_Syd.Model.Task> testTask = tvm.taskRepo.GetAllTasks();
            Assert.AreEqual(tvm.AllTasks[0], testTask[0]);
            Assert.AreEqual(tvm.AllTasks[0], testTask[1]);
            Assert.AreEqual(tvm.AllTasks[0], testTask[2]);
        }
        [TestMethod]
        public void TasksViewModelAllTasksIsSortedByDate()
        {
            //Assert

            //Assert.IsTrue(tvm.AllTasks[0].PickUpTime.Date < tvm.AllTasks[1].PickUpTime.Date); //Ved ikke om man faktisk kan sige det således, men det var mit bedste bud.
            DateTime dateTimeA = TaskA.Start;
            DateTime dateTimeB = TaskB.Start;
            Assert.IsTrue(0 > DateTime.Compare(dateTimeA, dateTimeB));
        }
    }
}