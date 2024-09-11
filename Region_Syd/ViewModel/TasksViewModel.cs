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

            taskRepo = new TaskRepo();
            //AllTasks = //Skal vise en sorteret ObservableCollection 
            //AllAmbulances = new ObservableCollection<Ambulance>();

            
        }


        public ObservableCollection<Region_Syd.Model.Task> GetTasksFromRepo(DateTime? pickUpTime = null, ClassOfTask? classOfTask = null, Region? fromRegion = null, /*Region? toRegion = null*/ bool isMatched = false)
        {
            List<Region_Syd.Model.Task> worklist = taskRepo.GetAllTasks();


            return new ObservableCollection <Region_Syd.Model.Task> (worklist);
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
