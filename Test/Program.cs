using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            bool pr = "40902439, 40902440,40902443,40902441,40902442,40902479,40902482,40902483,40902480,40902485.psd".Contains("40902485");
            Console.WriteLine(pr.ToString());
            Console.Read();
        }
    }
}
