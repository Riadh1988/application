using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consume.Models
{
    public class TasksData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProjectTSId { get; set; }

        public virtual ProjectsData projects { get; set; }
    }
}