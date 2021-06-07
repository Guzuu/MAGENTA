using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenta.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Foil { get; set; }
        public string Cover { get; set; }
        public string Folding { get; set; }
        public string CoverRefinement { get; set; }
        public string InsideRefinement { get; set; }
        public string Binding { get; set; }
        public string Paper { get; set; }
        public string InsideColor { get; set; }
    }
}
