using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenta.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Remote { get; set; }
        public int DepartmentId { get; set; }

        public virtual Departments Department { get; set; }
    }
}
