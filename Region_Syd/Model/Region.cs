using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    public class Region
    {
		private int _regionId;

		public int RegionId
		{
			get { return _regionId; }
			set { _regionId = value; }
		}
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		private double _hoursSaved;

		public double HoursSaved
		{
			get { return _hoursSaved; }
			set { _hoursSaved = value; }
		}
		private double _distanceSaved;

		public double DistanceSaved
		{
			get { return _distanceSaved; }
			set { _distanceSaved = value; }
		}
		public Region (string name, double hoursSaved, double distanceSaved, int regionId)
		{
			RegionId = regionId;
			Name = name;
			HoursSaved = hoursSaved;
			DistanceSaved = distanceSaved;
		}

	}
}
