using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenta.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public int OrderedQuantity { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateDeadline { get; set; }
        public string Status { get; set; }
        public string AttatchmentsPath { get; set; }
        public int ProductId { get; set; } 
        public int AddedById { get; set; }

        public virtual Products Product { get; set; }
        public virtual IdentityUser AddedBy { get; set; }
    }
}
