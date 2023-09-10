using AttendanceSystemProject.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemProject.Entities
{
    public class ClassSchedule
    {
        public int Id { get; set; } 

        public Course Course { get; set; }  

        public Day Day { get; set; }  

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TotalNumberOfClasses { get; set; }   
        
    }
}
