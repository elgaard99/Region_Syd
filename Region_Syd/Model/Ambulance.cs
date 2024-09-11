using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public List<Assignment> PlannedAssignments = new List<Assignment>();

		public void AddToPlannedAssignments(Assignment assignment)
		{
			PlannedAssignments.Add(assignment);
		}
		public List<Assignment> GetPlannedAssignments()
		{
			return PlannedAssignments;
		}
		public void RemovePlannedAssignment(Assignment assignment)
		{
			PlannedAssignments.Remove(assignment);
		}
	}
}
