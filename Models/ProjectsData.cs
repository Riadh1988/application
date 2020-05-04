using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consume.Models
{
    public class ProjectsData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public virtual ICollection<TasksData> MyTasks { get; set; }
    }
}