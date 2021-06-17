using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Magenta.Models
{
    public class Designs
    {
        public int Id { get; set; }
        public DateTime DateDesigned { get; set; }
        public bool Accepted { get; set; }
        public string AttatchmentsPath { get; set; }
        public int ProjectId { get; set; }
        public string DesignedById { get; set; }

        public virtual Projects Project { get; set; }
        public virtual IdentityUser DesignedBy { get; set; }
    }
}
