using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    public class Task
    {
		private string _regionTaskId;

		public string RegionTaskId
		{
			get { return _regionTaskId; }
			set { _regionTaskId = value; }
		}

		private string _startAddress;

		public string StartAddress
		{
			get { return _startAddress; }
			set { _startAddress = value; }
		}

		private string _endAddress;

		public string EndAddress
		{
			get { return _endAddress; }
			set { _endAddress = value; }
		}

		private DateTime _start;

		public DateTime Start
		{
			get { return _start; }
			set { _start = value; }
		}

		private DateTime _finish;

		public DateTime Finish
		{
			get { return _finish; }
			set { _finish = value; }
		}

		private string _description;

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		private TaskType _taskType;

		public  TaskType TaskType
		{
			get { return _taskType; }
			set { _taskType = value; }
		}

		private RegionEnum _startRegion;
		public RegionEnum StartRegion
		{
			get { return _startRegion; }
			set { _startRegion = value; }
		}

		private RegionEnum _endRegion;
		public RegionEnum EndRegion
		{
			get { return _endRegion; }
			set { _endRegion = value; }
		}

		private bool _isMatched;

		public bool IsMatched
		{
			get { return _isMatched; }
			set { _isMatched = value; }
		}

	}
}
