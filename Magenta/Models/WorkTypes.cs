using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenta.Models
{
    public class WorkTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MachineId { get; set; }

        public virtual Machines Machine { get; set; }
    }
}
