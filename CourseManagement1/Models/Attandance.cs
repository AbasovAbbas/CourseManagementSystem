using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class Attandance
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public AttandanceType AttandanceType { get; set; }
        public string StudentId { get; set; }
        public virtual ApplicationUser Student { get; set; }    
    }
    public enum AttandanceType
    {
        Absent,
        Present
    }
}
