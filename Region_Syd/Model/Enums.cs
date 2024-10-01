using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.Model
{
    public enum AssignmentTypeEnum
    {
        A,
        B, 
        C,
        D
    }
        
    public enum RegionBorderCross
    {
        NorMid, // 0
        MidNor, // 1
        MidSyd, // 2
        SydMid, // 3
        SydSjl, // 4
        SjlSyd, // 5
        SjlHov, // 6
        HovSjl, // 7
    }

    public static class Extensions
    {
        public static AssignmentTypeEnum ToAssignmentTypeEnum(this string str)
        {
            AssignmentTypeEnum type;
            switch (str)
            {
                case "A":
                    type = AssignmentTypeEnum.A; break;
                case "B":
                    type = AssignmentTypeEnum.B; break;
                case "C":
                    type = AssignmentTypeEnum.C; break;
                case "D":
                    type = AssignmentTypeEnum.D; break;
                default:
                    throw new ArgumentException($"Your string: '{str}', is not a valid AssignmentType.");
            }

            return type;
        }

        public static AssignmentTypeEnum ToAssignmentTypeEnum(this char c)
        {
            return (c.ToString()).ToAssignmentTypeEnum();
        }
    }
}