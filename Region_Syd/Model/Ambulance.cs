using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    public class Ambulance
    {
		private RegionEnum _regionId;
		public RegionEnum RegionId
		{
			get { return _regionId; }
			set { _regionId = value; }
		}
		private string _ambulanceId;

		public string AmbulanceId
		{
			get { return _ambulanceId; }
			set { _ambulanceId = value; }
		}

		public List<Task> PlannedTasks = new List<Task>();

		public void AddToPlannedTasks(Task task)
		{
			PlannedTasks.Add(task);
		}
		public List<Task> GetPlannedTasks()
		{
			return PlannedTasks;
		}
		public void RemovePlannedTask(Task task)
		{
			PlannedTasks.Remove(task);
		}
	}
}
