using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTracker
{
    public class Task
    {
        public int Id { get; set; }
        public required string  Title { get; set; }
        public string Status { get; set; } // e.g., "todo", "in-progress", "done"

    }
}
