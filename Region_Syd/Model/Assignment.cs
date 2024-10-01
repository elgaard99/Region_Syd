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
        private string _assignmentType;
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

		public string AssignmentType
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

		public Assignment (string id, string startAddress, string endAddress, DateTime start, DateTime finish, string description, string type, Region startRegion, Region endRegion, bool isMatched, string ambulanceId)
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
			return $"{RegionAssignmentId}, {StartAddress}, {EndAddress}, {Start}, {Finish}, {Description}, {AssignmentType}, {StartRegion.RegionId}, {EndRegion.RegionId}, {IsMatched}, {AmbulanceId}";
        }


        // rene

        // RegionsPassed = new[] { 0, 2, 4, 6 } // Den vil sætte _regionsPassed[0], [2], [4], og [6] til true
        // som altså vil svare til en tur fra region nord til region H
        // (NorMid + MidSyd + SydSjl + SjlHov)


        public List<int> FindRegionsPassed()
        {
            int start = StartRegion.RegionId;
            int end = EndRegion.RegionId;

            List<int> parsedRegions = new List<int>();

            if (start == 1) // hovedstaden
            {

                parsedRegions.Add(7);

                switch (end)
                {
                    case 2: // midt 
                        parsedRegions.Add(5);
                        parsedRegions.Add(3);
                        break;
                    case 3: // nord
                        parsedRegions.Add(5);
                        parsedRegions.Add(3);
                        parsedRegions.Add(1);
                        break;
                    case 5: // syd
                        parsedRegions.Add(5);
                        break;
                }

            }
            else if (start == 4) // sjælland
            {
                switch (end)
                {
                    case 1: // hoved
                        parsedRegions.Add(6);
                        break;
                    case 2: // midt
                        parsedRegions.Add(5);
                        parsedRegions.Add(3);
                        break;
                    case 3: // nord
                        parsedRegions.Add(5);
                        parsedRegions.Add(3);
                        parsedRegions.Add(1);
                        break;
                    case 5: // syd
                        parsedRegions.Add(5);
                        break;
                }
            }
            else if (start == 5) // syd 
            {
                switch (end)
                {
                    case 1: // hoved
                        parsedRegions.Add(4);
                        parsedRegions.Add(6);
                        break;
                    case 2: // midt
                        parsedRegions.Add(3);
                        break;
                    case 3: // nord
                        parsedRegions.Add(3);
                        parsedRegions.Add(1);
                        break;
                    case 4: // sjælland
                        parsedRegions.Add(4);
                        break;
                }
            }
            else if (start == 2) // midt
            {
                switch (end)
                {
                    case 1: // hoved
                        parsedRegions.Add(2);
                        parsedRegions.Add(4);
                        parsedRegions.Add(6);
                        break;
                    case 3: // nord
                        parsedRegions.Add(1);
                        break;
                    case 4: // sjælland
                        parsedRegions.Add(2);
                        parsedRegions.Add(4);
                        break;
                    case 5: // syd
                        parsedRegions.Add(2);
                        break;
                }
            }
            else if (start == 3) // nord
            {
                parsedRegions.Add(0);

                switch (end)
                {
                    case 1: // hoved
                        parsedRegions.Add(2);
                        parsedRegions.Add(4);
                        parsedRegions.Add(6);
                        break;
                    case 4: // sjælland
                        parsedRegions.Add(2);
                        parsedRegions.Add(4);
                        break;
                    case 5:
                        parsedRegions.Add(2);
                        break;
                }
            }

            return parsedRegions;
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
