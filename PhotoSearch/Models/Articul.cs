using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Models
{
    public class Articul
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public int IsFound { get; set; }
        public Articul()
        {

        }
    }
}
