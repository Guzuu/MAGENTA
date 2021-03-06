using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenta.Models
{
    public class Works
    {
        public int Id { get; set; }
        public int AmountProcessed { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime DateProcessed { get; set; }
        public int ProjectId { get; set; }
        public int WorkTypeId { get; set; }
        public string ProcessedById { get; set; }

        public virtual Projects Project { get; set; }
        public virtual WorkTypes WorkType { get; set; }
        public virtual IdentityUser ProcessedBy { get; set; }
    }
}
