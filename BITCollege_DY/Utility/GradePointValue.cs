using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// GradePointValue:  Struct to align letter grades with gradepoint values
    /// Acts as an ENUM but with double values.
    /// </summary>
    public struct GradePointValue
    {
        public const double A_PLUS = 4.5;
        public const double A = 4;
        public const double B_PLUS = 3.5;
        public const double B = 3;
        public const double C_PLUS = 2.5;
        public const double C = 2;
        public const double D = 1;
        public const double F = 0;
        public const double PASS = 4;
        public const double FAIL = 0;
        public const double INCOMPLETE = -1;
    }
}
