using Region_Syd.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.ViewModel
{
    public class TasksViewModel
    {
        TaskRepo taskRepo;
        AmbulanceRepo abulanceRepo;

        public ObservableCollection<Task> AllTasks 
        { 
            get; 
            
            set;
        }


        public ObservableCollection <Ambulance> AllAmbulances {  get; }


        public TasksViewModel() 
        {
            TaskRepo taskRepo = new TaskRepo();
            AmbulanceRepo ambulanceRepo = new AmbulanceRepo();

            taskRepo = new TaskRepo();
            //AllTasks = //Skal vise en sorteret ObservableCollection 
            //AllAmbulances = new ObservableCollection<Ambulance>();

            
        }


        public ObservableCollection<Task> GetTasksFromRepo(DateTime? pickUpTime = null, ClassOfTask? classOfTask = null, Region? fromRegion = null, /*Region? toRegion = null*/ bool isMatched = false)
        {
            List<Task> worklist = taskRepo.Tasks.FindAll(t => t.Equals(isMatched = false));




            return new ObservableCollection<Task>(worklist);
        }

        public ObservableCollection<Ambulance> GetAmbulancesFromRepo()
        {
            throw new NotImplementedException();
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
        public bool IsMatched { get; set; } //skal (måske) vises i domænemodel

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

    public class Ambulance
    {

    }

    public class TaskRepo
    { 
        public List<Task> Tasks;

        public TaskRepo()
        {
            Tasks = new List<Task>();
            Tasks.Add(new Task()
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
                IsMatched = false,
                

            });
            Tasks.Add(new Task()
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
                IsMatched = false,
            });
            Tasks.Add(new Task()
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
                IsMatched = true,
            });
        }
    }

    public class AmbulanceRepo
    {
        //public List<Ambulance> Ambulances;

        //public AmbulanceRepo()
        //{
        //    Ambulances = new List<Ambulace>();
        //    Ambulances.Add(new Ambulance()
        //    {
        //        AmbulanceID = "Amb2",
        //         = "Sygehus Syd",
                

        //    });

    }
}
