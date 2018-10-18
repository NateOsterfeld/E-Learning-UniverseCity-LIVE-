using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class StudentAssignmentDashBoardViewModel
    {
        public Course Course { get; set; }
        public List<CombinedAssignment> combinedAssignments { get; set; }

    }
}