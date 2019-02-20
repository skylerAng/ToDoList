using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustDoIt.Models
{
    public class ToDo
    {
        // Id for each Item
        public int Id { get; set; }

        // Item description
        public string Description { get; set; }

        // Check whether item is ticked
        public Boolean IsDone { get; set; }

        // Reference to the user
        public virtual ApplicationUser User { get; set; }
    }
}