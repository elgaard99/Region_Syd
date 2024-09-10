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

            Assert.AreEqual(TaskA, tvm.taskRepo[0]);
            Assert.AreEqual(TaskB, tvm.taskRepo[1]);
            Assert.AreEqual(TaskC, tvm.taskRepo[2]);
        }
        [TestMethod]
        public void RepoTaskEqualsTasksViewModel()
        {
            //Assert

            Assert.AreEqual(tvm.AllTasks[0], tvm.taskRepo[0]);
            Assert.AreEqual(tvm.AllTasks[0], tvm.taskRepo[1]);
            Assert.AreEqual(tvm.AllTasks[0], tvm.taskRepo[2]);
        }
        public void TasksViewModelAllTasksIsSortedByDate()
        {
            //Assert

            Assert.IsTrue(tvm.AllTasks[0].PickUpTime.Date < tvm.AllTasks[1].PickUpTime.Date); //Ved ikke om man faktisk kan sige det således, men det var mit bedste bud.
        }
    }
}