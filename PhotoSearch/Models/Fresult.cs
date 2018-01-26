using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Models
{
    public class Fresult
    {
        public long Id { get; set; }

        public string FileName { get; set; }

        public string SourceName { get; set; }

        public long PathId { get; set; }

        public string FilePath { get; set; }

        public string Extension { get; set; }

        public string Articul { get; set; }

        public string Descripion { get; set; }

        public bool Selected { get; set; }

        public string Size { get
            {
                if (this.FilePath.Trim() != string.Empty)
                {
                    FileInfo fi = new FileInfo(this.FilePath); 
                    return string.Format("{0} MB", ((Convert.ToDecimal(fi.Length) / 1024)/1024).ToString("F", System.Globalization.CultureInfo.InvariantCulture));
                }
                else { return string.Empty; }
            } }
    }
}
