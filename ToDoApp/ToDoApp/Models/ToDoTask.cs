using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class ToDoTask
    {
        public Guid TaskID { get; set; }
        public Guid UserID { get; set; }
        public string TaskDescription { get; set; }
        public string TaskStatus { get; set; }
        public string TaskImageName { get; set; }
    }
}
