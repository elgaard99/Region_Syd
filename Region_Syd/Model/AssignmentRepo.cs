﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.Model
{
    public class AssignmentRepo
    {
        private List<Assignment> _allAssignments;
        public AssignmentRepo()
        {
            _allAssignments = new List<Assignment>();
            _allAssignments.Add(new Assignment()
            {
                RegionAssignmentId = "12-AB",
                StartAddress = "Sygehus Syd",
                EndAddress = "Riget",
                Start = new DateTime(2024, 09, 06, 10, 40, 00),
                Finish = new DateTime(2024, 09, 06, 13, 40, 00),
                Description = "PAtienten er PsyKOtisK",
                AssignmentType = AssignmentTypeEnum.C,
                StartRegion = RegionEnum.RSj,
                EndRegion = RegionEnum.RH,
                IsMatched = false,


            });
            _allAssignments.Add(new Assignment()
            {
                RegionAssignmentId = "21-BA",
                StartAddress = "Riget",
                EndAddress = "Sygehus Syd",
                Start = new DateTime(2024, 09, 06, 14, 00, 00),
                Finish = new DateTime(2024, 09, 06, 17, 30, 00),
                Description = "Kræver forsigtig kørsel",
                AssignmentType = AssignmentTypeEnum.D,
                StartRegion = RegionEnum.RH,
                EndRegion = RegionEnum.RSj,
                IsMatched = false,
            });
            _allAssignments.Add(new Assignment()
            {
                RegionAssignmentId = "33-CD",
                StartAddress = "Roskilde Hos.",
                EndAddress = "Kongensgade 118, 9320 Hjallerup",
                Start = new DateTime(2024, 09, 05, 15, 00, 00),
                Finish = new DateTime(2024, 09, 06, 13, 00, 00),
                Description = "Kræver ilt i ambulancen",
                AssignmentType = AssignmentTypeEnum.D,
                StartRegion = RegionEnum.RSj,
                EndRegion = RegionEnum.RN,
                IsMatched = true,
            });
        }
        public void AddToAllAssignments(Assignment assignment)
        {
            _allAssignments.Add(assignment);
        }
        public List<Assignment> GetAllAssignments()
        {
            return _allAssignments;
        }
        public void RemoveAssignment(Assignment assignment)
        {
            _allAssignments.Remove(assignment);
        }

        public void ReassignAmbulance(Assignment a1, Assignment a2)
        {
			if (DateTime.Compare(a1.Start, a2.Start) > 0) //assignment 1 skal have 2's ambulance
			{
                a1.AmbulanceId = a2.AmbulanceId;
			}
			else if (DateTime.Compare(a1.Start, a2.Start) < 0) //assignment 2 skal have 1's ambulance
			{
                a2.AmbulanceId = a1.AmbulanceId;
			}
		}
        
        public void SetIsMatchedTrue (Assignment a1, Assignment a2)
        {
            a1.IsMatched = true;
            a2.IsMatched = true;
        }
    }
}
