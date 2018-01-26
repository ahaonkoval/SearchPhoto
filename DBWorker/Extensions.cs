using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using DataModels;
using LinqToDB;
using System.ComponentModel;

namespace DBWorker
{
    public class Extensions
    {
        public IEnumerable<SelectImgExt> GetExtList()
        {
            using (var db = new PathDataDB())
            {
                return db.ImgExt.ToList().Select(
                    x => new SelectImgExt
                    {
                        ExtId = x.ExtId,
                        Name = x.Name,
                        Selected = x.ExtId == 0 ? true :false
                    });
            }
        }
    }

    public class SelectImgExt: ImgExt
    {
        public bool Selected { get; set; }
    }
}
