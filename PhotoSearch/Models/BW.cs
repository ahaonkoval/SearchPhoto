using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using DataModels;
using DBWorker;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace PhotoSearch.Models
{
    public class BW: BackgroundWorker
    {
        private DataGridViewRow current_row;
        private Prms prms { get; set; }

        private DBW dbw;

        private int Counter = 0;

        private Guid GlobalId;

        public BW()
        {
            this.DoWork += BW_DoWork;
            this.ProgressChanged += BW_ProgressChanged;
            this.RunWorkerCompleted += BW_RunWorkerCompleted;
            this.WorkerReportsProgress = true;

            dbw = new DBW();
        }

        public BW(string loadPath, int pathId)
        {
            this.DoWork += BW_DoWork;
            this.ProgressChanged += BW_ProgressChanged;
            this.RunWorkerCompleted += BW_RunWorkerCompleted;
            this.WorkerReportsProgress = true;
            GlobalId = Guid.NewGuid();
            dbw = new DBW();
            this.prms = new Models.Prms(loadPath, pathId);
        }

        public void SetCurrentRow(DataGridViewRow row)
        {
            this.current_row = row;
        }

        public void Start()
        {
            this.RunWorkerAsync(this.prms);
        }

        private void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.current_row.Cells["Qty"].Value = dbw.Pth.GetCountFiles(
                Convert.ToInt64(this.current_row.Cells["Id"].Value));

            Helper.SetRowColor(this.current_row, Color.White);

            BindingSource bnd = (BindingSource)this.current_row.DataGridView.DataSource;

            Place p = (Place)bnd.Current;
            
            p.Lst = dbw.Pth.GetFileList(p.Id);

            this.current_row.ReadOnly = false;
        }

        private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            St st = (St)e.UserState;
            //string status = e.UserState.ToString();
            this.current_row.Cells["columnStatus"].Value = st.Status;
            this.current_row.Cells["Qty"].Value = Convert.ToInt64(st.Qty);
        }

        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            Prms parameters = (Prms)e.Argument;
            this.ReportProgress(0, new St("Start scanning directories", 0));

            IEnumerable<string> src = ((IEnumerable<string>)Directory.GetFiles(parameters.LoadPath, "*", SearchOption.AllDirectories));

            dbw.Pth.DeletePathFiles(parameters.PathId);

            this.ReportProgress(0, new St("Start saving in the database", 0));

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["PathData"].ConnectionString;
            System.Data.SQLite.SQLiteConnection connect = new System.Data.SQLite.SQLiteConnection(connection);
            DBWorker.SQLite.SQLiteBulkInsert bi = new DBWorker.SQLite.SQLiteBulkInsert(connect, "PathFiles");

            bi.AddParameter("path_id", DbType.Int32);
            bi.AddParameter("file_name", DbType.String);
            bi.AddParameter("file_path", DbType.String);
            bi.AddParameter("extension", DbType.String);

            connect.Open();
            int counter = 1;
            foreach (string s in src)
            {
                bi.Insert(new object[] { parameters.PathId, System.IO.Path.GetFileName(s), s, System.IO.Path.GetExtension(s) });
                //this.ReportProgress(counter++, "Start saving in the database");
                this.ReportProgress(0, new St("Process saving in the database", counter++));
            }
            bi.Flush();

            this.ReportProgress(0, new St("", counter++));

            //Рекурсию пока откладем...
            //GetAllFile(parameters.LoadPath, parameters.PathId);
        }
        /// <summary>
        /// Рекурсию пока откладем...
        /// </summary>
        /// <param name="startdirectory"></param>
        /// <param name="pathId"></param>
        void GetAllFile(string startdirectory, int pathId)
        {
            try
            {
                string[] searchdirectory = Directory.GetDirectories(startdirectory);
                if (searchdirectory.Length > 0)
                {
                    for (int i = 0; i < searchdirectory.Length; i++)
                    {
                        GetAllFile(searchdirectory[i] + @"\", pathId);
                    }
                }
                string[] filesss = Directory.GetFiles(startdirectory);
                for (int i = 0; i < filesss.Length; i++)
                {
                    dbw.Pth.SetFilePath(
                        pathId,
                        System.IO.Path.GetFileName(filesss[i]),
                        filesss[i].ToString(),
                        System.IO.Path.GetExtension(filesss[i]));

                    Counter++;
                    this.ReportProgress(Counter);
                }
            }
            catch (Exception error)
            {
                return;
            }
        }
    }

    public class Prms
    {
        public string LoadPath { get; set; }

        public int PathId { get; set; }

        public Prms(string loadPath, int pathId)
        {
            this.LoadPath = loadPath;
            this.PathId = pathId;
        }
    }

    public class St
    {
        public string Status { get; set; }

        public int Qty { get; set; }

        public St(string status, int qty)
        {
            this.Status = status;
            this.Qty = qty;
        }
    }
}
