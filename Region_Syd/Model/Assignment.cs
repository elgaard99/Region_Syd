using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.Model
{
    public class Assignment
    {
		private string _regionAssignmentId;
        private string _ambulanceId;
        private string _startAddress;
        private string _endAddress;
        private DateTime _start;
        private DateTime _finish;
        private string _description;
        private AssignmentTypeEnum _assignmentType;
        private Region _startRegion;
        private Region _endRegion;
        private bool _isMatched;



        public string RegionAssignmentId
		{
			get { return _regionAssignmentId; }
			set { _regionAssignmentId = value; }
		}

		public string AmbulanceId
		{
			get { return _ambulanceId; }
			set { _ambulanceId = value; }
		}

		public string StartAddress
		{
			get { return _startAddress; }
			set { _startAddress = value; }
		}

		public string EndAddress
		{
			get { return _endAddress; }
			set { _endAddress = value; }
		}

		public DateTime Start
		{
			get { return _start; }
			set { _start = value; }
		}

		public DateTime Finish
		{
			get { return _finish; }
			set { _finish = value; }
		}

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public  AssignmentTypeEnum AssignmentType
		{
			get { return _assignmentType; }
			set { _assignmentType = value; }
		}
		
		public Region StartRegion
		{
			get { return _startRegion; }
			set { _startRegion = value; }
		}

		public Region EndRegion
		{
			get { return _endRegion; }
			set { _endRegion = value; }
		}

		public bool IsMatched
		{
			get { return _isMatched; }
			set { _isMatched = value; }
		}

		public Assignment() { }

		public Assignment (string id, string startAddress, string endAddress, DateTime start, DateTime finish, string description, AssignmentTypeEnum type, Region startRegion, Region endRegion, bool isMatched, string ambulanceId)
		{
			RegionAssignmentId = id;
			StartAddress = startAddress;
			EndAddress = endAddress;
			Start = start;
			Finish = finish;
			Description = description;
			AssignmentType = type;
			StartRegion = startRegion;
			EndRegion = endRegion;
			IsMatched = isMatched;
			AmbulanceId = ambulanceId;
		}

        public override string ToString()
        {
			return $"{RegionAssignmentId}, {StartAddress}, {EndAddress}, {Start}, {Finish}, {Description}, {AssignmentType}, {StartRegion}, {EndRegion}, {IsMatched}, {AmbulanceId}";
        }
    }
}
