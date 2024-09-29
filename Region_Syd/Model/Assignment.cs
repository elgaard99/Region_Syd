using System;
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

		private AssignmentTypeEnum _assignmentType;

		public  AssignmentTypeEnum AssignmentType
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




        

        private bool[] _regionsPassed = new bool[8]; // Initialized with 8 elements

        public object RegionsPassed
        {
            get { return _regionsPassed; }
            set
            {
                // Check if the value is a single integer
                if (value is int singleIndex)
                {
                    SetIndexToTrue(singleIndex);

                }
                // Check if the value is an array or list of integers
                else if (value is IEnumerable<int> indices)
                {
                    foreach (var index in indices)
                    {
                        SetIndexToTrue(index); // Set each index to true
                        
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid type. Only int or IEnumerable<int> are supported.");
                }
            }
        }

        // Helper method to check bounds and set index to true
        private void SetIndexToTrue(int index)
        {
            if (index >= 0 && index < _regionsPassed.Length)
            {
                _regionsPassed[index] = true;
            }
            else
            {
                throw new IndexOutOfRangeException($"Index {index} is out of bounds.");
            }
        }

        private void SetIndexToFalse(int index2)
        {
            if (index2 >= 0 && index2 < _regionsPassed.Length)
            {
                _regionsPassed[index2] = false;
            }
            else
            {
                throw new IndexOutOfRangeException($"Index {index2} is out of bounds.");
            }
        }






    }
}
