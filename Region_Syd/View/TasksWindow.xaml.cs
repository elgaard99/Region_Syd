using System.Windows;
using System.Windows.Data;
using Region_Syd.ViewModel;



namespace Region_Syd.View
{
    /// <summary>
    /// Interaction logic for TasksView.xaml
    /// </summary>
    public partial class TasksWindow : Window
    {
        public TasksWindow()
        {
            InitializeComponent();

            TasksViewModel tvm = new TasksViewModel();
            DataContext = tvm;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(tvm.AllTasks);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("StartRegion");
            view.GroupDescriptions.Add(groupDescription);
        }
    }

    //public class Task
    //{
    //    public string TaskID { get; set; }
    //    public string FromAddress { get; set; }
    //    public string ToAddress { get; set; }
    //    public DateTime PickUpTime { get; set; }
    //    public DateTime TimeOfArrival { get; set; }
    //    public string Description { get; set; }
    //    public ClassOfTask _ClassOfTask { get; set; }
    //    public Region FromRegion { get; set; }
    //    public Region ToRegion { get; set; }

    //}

    //public enum ClassOfTask
    //{
    //    C,
    //    D
    //}

    //public enum Region
    //{
    //    RegionH,
    //    RegionSJ,
    //    RegionS,
    //    RegionM,
    //    RegionN
    //}
}
