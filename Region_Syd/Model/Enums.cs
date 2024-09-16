using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.Model
{
        public enum RegionEnum
        {
            RSj,
            RSy,
            RH,
            RM,
            RN
        }
        public enum AssignmentTypeEnum
        {
            A,
            B, 
            C,
            D
        }
        
        public enum RegionBorderCross
        {
            NorMid,
            MidNor,
            MidSyd,
            SydMid,
            SydSjl,
            SjlSyd,
            SjlHov,
            HovSjl,
        }
   
}
