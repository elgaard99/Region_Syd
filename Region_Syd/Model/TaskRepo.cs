using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    public class TaskRepo
    {
        List<Task> AllTasks;
        public TaskRepo()
        {
            AllTasks = new List<Task>();
            AllTasks.Add(new Task()
            {
                RegionTaskId = "12-AB",
                StartAddress = "Sygehus Syd",
                EndAddress = "Riget",
                Start = new DateTime(2024, 09, 06, 10, 40, 00),
                Finish = new DateTime(2024, 09, 06, 13, 40, 00),
                Description = "PAtienten er PsyKOtisK",
                TaskType = TaskType.C,
                StartRegion = RegionEnum.RSj,
                EndRegion = RegionEnum.RH,
                IsMatched = false,


            });
            AllTasks.Add(new Task()
            {
                RegionTaskId = "21-BA",
                StartAddress = "Riget",
                EndAddress = "Sygehus Syd",
                Start = new DateTime(2024, 09, 06, 14, 00, 00),
                Finish = new DateTime(2024, 09, 06, 17, 30, 00),
                Description = "Kræver forsigtig kørsel",
                TaskType = TaskType.D,
                StartRegion = RegionEnum.RH,
                EndRegion = RegionEnum.RSj,
                IsMatched = false,
            });
            AllTasks.Add(new Task()
            {
                RegionTaskId = "33-CD",
                StartAddress = "Roskilde Hos.",
                EndAddress = "Kongensgade 118, 9320 Hjallerup",
                Start = new DateTime(2024, 09, 05, 15, 00, 00),
                Finish = new DateTime(2024, 09, 06, 13, 00, 00),
                Description = "Kræver ilt i ambulancen",
                TaskType = TaskType.D,
                StartRegion = RegionEnum.RSj,
                EndRegion = RegionEnum.RN,
                IsMatched = true,
            });
        }
        public void AddToAllTasks(Task task)
        {
            AllTasks.Add(task);
        }
        public List<Task> GetAllTasks()
        {
            return AllTasks;
        }
        public void RemoveTask(Task task)
        {
            AllTasks.Remove(task);
        }
    }
}
