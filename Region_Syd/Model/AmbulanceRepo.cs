using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.Model
{
    public class AmbulanceRepo
    {
        public List<Ambulance> AllAmbulances = new List<Ambulance>();


        public AmbulanceRepo()
        {
            AllAmbulances.Add(
                new Ambulance()
                {
                    AmbulanceId = "a1",
                    RegionId = RegionEnum.RSy
                });
            
            AllAmbulances.Add(
                new Ambulance()
                {
                    AmbulanceId = "a2",
                    RegionId = RegionEnum.RH
                });


        }
        public List<Ambulance> GetAmbulances()
        {
            return AllAmbulances;
        }

        public Ambulance GetAmbulance(string ambulanceId)
        {
            return AllAmbulances.Find(a => a.AmbulanceId == ambulanceId);            
        }
    }
}
