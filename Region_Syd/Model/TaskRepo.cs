using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    public class TaskRepo
    {
        List<Task> AllTasks = new List<Task>();

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
