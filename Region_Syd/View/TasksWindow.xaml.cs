using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            List<Task> tasks = new List<Task>();
            tasks.Add(new Task() 
            { 
                TaskID = "12-AB",
                FromAddress = "Sygehus Syd",
                ToAddress = "Riget",
                PickUpTime = new DateTime(2024, 09, 06, 10, 40, 00),
                TimeOfArrival = new DateTime(2024, 09, 06, 13, 40, 00),
                Description = "PAtienten er PsyKOtisK",
                _ClassOfTask = ClassOfTask.C,
                FromRegion = Region.RegionS,
                ToRegion = Region.RegionH,
            });
            tasks.Add(new Task()
            {
                TaskID = "21-BA",
                FromAddress = "Riget",
                ToAddress = "Sygehus Syd",
                PickUpTime = new DateTime(2024, 09, 06, 14, 00, 00),
                TimeOfArrival = new DateTime(2024, 09, 06, 17, 30, 00),
                Description = "Kræver forsigtig kørsel",
                _ClassOfTask = ClassOfTask.D,
                FromRegion = Region.RegionH,
                ToRegion = Region.RegionS,
            });
            tasks.Add(new Task()
            {
                TaskID = "33-CD",
                FromAddress = "Roskilde Hos.",
                ToAddress = "Kongensgade 118, 9320 Hjallerup",
                PickUpTime = new DateTime(2024, 09, 05, 15, 00, 00),
                TimeOfArrival = new DateTime(2024, 09, 06, 13, 00, 00),
                Description = "Kræver ilt i ambulancen",
                _ClassOfTask = ClassOfTask.D,
                FromRegion = Region.RegionSJ,
                ToRegion = Region.RegionN,
            });
            lvTasks.ItemsSource = tasks;
        }
    }

    public class Task
    {
        public string TaskID { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime TimeOfArrival { get; set; }
        public string Description { get; set; }
        public ClassOfTask _ClassOfTask { get; set; }
        public Region FromRegion { get; set; }
        public Region ToRegion { get; set; }

    }

    public enum ClassOfTask
    {
        C,
        D
    }

    public enum Region
    {
        RegionH,
        RegionSJ,
        RegionS,
        RegionM,
        RegionN
    }
}
