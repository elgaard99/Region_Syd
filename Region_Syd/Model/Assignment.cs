﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.Model
{
    public class Assignment
    {
		private string _regionAssignmentId;

		public string RegionAssignmentId
		{
			get { return _regionAssignmentId; }
			set { _regionAssignmentId = value; }
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

		private AssignmentType _assignmentType;

		public  AssignmentType AssignmentType
		{
			get { return _assignmentType; }
			set { _assignmentType = value; }
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