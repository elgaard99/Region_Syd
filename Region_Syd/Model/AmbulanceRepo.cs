using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.Model
{
    public class AmbulanceRepo
    {
        public List<Ambulance> AllAmbulances = new List<Ambulance>();

        public List<Ambulance> GetAmbulances()
        {
            return AllAmbulances;
        }
    }
}
