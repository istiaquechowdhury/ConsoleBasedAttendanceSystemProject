using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemProject.Entities
{
    public class Attendance
    {
        public int Id { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }

        public DateTime AttandanceDate { get; set; }    

        public bool IsPresent { get; set; }
    }
}
