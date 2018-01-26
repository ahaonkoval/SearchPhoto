using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Classes
{
    public class Src1
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public long Qty { get; set; }

        public Src1() { }

        public Src1(int id, string path, long qty)
        {
            this.Id = id; this.Path = path; this.Qty = qty;
        }
    }



}
