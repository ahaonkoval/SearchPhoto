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
    public class PHPath
    {
        public PHPath()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Place> GetPathList()
        {
            using (var db = new PathDataDB())
            {
                return db.Paths.ToList().Select(
                        x => new Place
                        {
                            Id = x.Id,
                            Created = x.Created,
                            Name = x.Name,
                            PathValue = x.PathValue,
                            Qty = this.GetCountFiles(x.Id),
                            Worker = null,
                            Lst = null
                        }
                    ).ToList();
            }
        }

        public IEnumerable<PlaceFile> GetFileList(long pathId)
        {
            using (var db = new PathDataDB())
            {
                return db.PathFiles.Where(w => w.PathId == pathId).ToList().Select(
                        x => new PlaceFile
                        {
                            Created = x.Created,
                            Extension = x.Extension,
                            FileName = x.FileName,
                            FilePath = x.FilePath,
                            Id = x.Id,
                            PathId = x.PathId
                        }
                    );
                    
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path_value"></param>
        /// <param name="qty"></param>
        public void SetPath(string name, string path_value, int qty)
        {
            using (var db = new PathDataDB())
            {
                db.Paths.Insert(() => new DataModels.Path
                {
                    Name = name,
                    PathValue = path_value,
                    Qty = qty
                });
            }
        }

        public void UpdatePath(long pathId, string name)
        {
            using (var db = new PathDataDB())
            {
                db.Paths.Where(w => w.Id == pathId).Set(p => p.Name, name).Update();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathId"></param>
        /// <param name="fileName"></param>
        /// <param name="fielPath"></param>
        /// <param name="extension"></param>
        public void SetFilePath(int pathId, string fileName, string fielPath, string extension)
        {
            using (var db = new PathDataDB())
            {
                var pf = db.PathFiles.Where(w => w.FilePath == fielPath).ToList();
                if (pf.Count == 0)
                {
                    db.PathFiles.Insert(() => new PathFile
                    {
                        PathId = pathId,
                        FileName = fileName,
                        FilePath = fielPath,
                        Extension = extension
                    });
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSourceNameById(long id)
        {
            using (var db = new PathDataDB())
            {
                return db.Paths.Where(w => w.Id == id).FirstOrDefault().Name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathId"></param>
        /// <returns></returns>
        public long GetCountFiles(long pathId)
        {
            using (var db = new PathDataDB())
            {
                return db.PathFiles.Where(w => w.PathId == pathId).Count();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathId"></param>
        public void DeletePathFiles(long pathId) {
            using (var db = new PathDataDB())
            {
                db.PathFiles.Delete(dw => dw.PathId == pathId);
            }
        }

        public void DeletePathFilesItem(long id)
        {
            using (var db = new PathDataDB())
            {
                db.PathFiles.Delete(d => d.Id == id);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathId"></param>
        public void DeletePath(long pathId)
        {
            using (var db = new PathDataDB())
            {
                db.PathFiles.Delete(dw => dw.PathId == pathId);
                db.Paths.Delete(o => o.Id == pathId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathValue"></param>
        /// <returns></returns>
        public long CreatePathRecord(string pathValue)
        {
            using (var db = new PathDataDB())
            {
                object id = db.Paths.InsertWithIdentity(() => new DataModels.Path
                {
                    Name = string.Empty,
                    PathValue = pathValue
                });

                return Convert.ToInt64(id);
            }
        }

        public IEnumerable<DataModels.PathFile> GetPathByArticul(string articul)
        {
            using (var db = new PathDataDB())
            {
                string mask = string.Format("%/{0}/%", articul);
                return db.PathFiles.Where(w => w.FileName.IndexOf(articul) > -1).ToList();
            }
        }
    }

    public class Place: DataModels.Path
    {
        public Object Worker { get; set; }

        public IEnumerable<PlaceFile> Lst { get; set; }

    }

    public class PlaceFile: DataModels.PathFile
    {
        private static int Counter;

        public static int qtyPart = 0;

        public string Art { get; set; }

        /// <summary>
        /// Зараз не використовуэться...
        /// </summary>
        /// <param name="list"></param>
        /// <param name="bw"></param>
        /// <returns></returns>
        public bool IsExistsArt(string[] list, ref BackgroundWorker bw)
        {
            bool r = false;
            try
            {
                foreach (string articul in list)
                {
                    if (qtyPart == 10000)
                    {
                        bw.ReportProgress(0, this.FilePath);
                        qtyPart = 0;
                    }

                    qtyPart++;

                    if (this.FilePath.Contains(articul))
                    {
                        r = true;
                        this.Art = articul;
                        this.Id = Counter++;
                        DBWorker.Lg ml = new Lg();
                        ml.Log(articul);
                        //return r;
                    }
                }
            } catch (Exception ex)
            {
                DBWorker.Lg ml = new Lg();
                ml.Log(ex.Message);
            }

            return r;
        }
    }
}
