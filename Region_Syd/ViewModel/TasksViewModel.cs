using Region_Syd.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Region_Syd.Model;

namespace Region_Syd.ViewModel
{
    public class TasksViewModel
    {
        public TaskRepo taskRepo;
        AmbulanceRepo abulanceRepo;

        public ObservableCollection<Region_Syd.Model.Task> AllTasks 
        { 
            get; 
            
            set;
        }


        public ObservableCollection <Ambulance> AllAmbulances {  get; }


        public TasksViewModel() 
        {
            TaskRepo taskRepo = new TaskRepo();
            AmbulanceRepo ambulanceRepo = new AmbulanceRepo();
            AllTasks = GetTasksFromRepo();
            
            //AllTasks = //Skal vise en sorteret ObservableCollection 
            //AllAmbulances = new ObservableCollection<Ambulance>();

            
        }


        public ObservableCollection<Region_Syd.Model.Task> GetTasksFromRepo(/*DateTime? pickUpTime = null, ClassOfTask? classOfTask = null, Region? fromRegion = null, *//*Region? toRegion = null*//* bool isMatched = false*/)
        {
            TaskRepo taskRepo = new TaskRepo();
            
            var worklist = new ObservableCollection<Region_Syd.Model.Task>(taskRepo.AllTasks.Where(task => !task.IsMatched).OrderBy(task => task.Start)); // !task betyder er false, uden ! finder den true. 
            

            return worklist;
        }

        public ObservableCollection<Ambulance> GetAmbulancesFromRepo()
        {
            throw new NotImplementedException();
        }

    }

    public class Ambulance
    {

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
