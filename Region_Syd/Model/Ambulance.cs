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

		public Ambulance()
		{

		}
		
		public Ambulance(RegionEnum region, string id)
		{
			RegionId = region;
			AmbulanceId = id;
		}

	}
}
