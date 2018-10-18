using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class AssignmentDashBoardViewModel
    {
        public Course Course { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Assignment> AssignmentsWithVideo { get; set; }
        public User User { get; set; }

        public List<Assignment> PrintAll(Assignment[] array1, Assignment[] array2)
        {
            // Use nested loop to check for duplicates.
            List<Assignment>
                result = new List<Assignment>
                    ();
            for (int i = 0; i < array1.Length; i++)
            {
                // Check for duplicates in all following elements.
                bool isDuplicate = false;
                for (int y = 0; y < array2.Length; y++)
                {
                    if (array1[i].AssignmentName == array2[y].AssignmentName)
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate)
                {
                    result.Add(array1[i]);
                }
            }
            return result;
        }
    }
}