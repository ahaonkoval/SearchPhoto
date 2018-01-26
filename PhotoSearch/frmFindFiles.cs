using NLog;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DataModels;
using DBWorker;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using PhotoSearch.Models;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace PhotoSearch
{
    public partial class frmFindFiles : Form
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        //private DataGridViewTextBoxColumn file;
        //private DataGridViewTextBoxColumn size;
        //private DataGridViewTextBoxColumn way;
        //private DataGridViewCheckBoxColumn check;

        DBWorker.DBW dbw;

        private int freeBufferLength = 4;

        private BindingSource bndSrc;

        private BindingSource bndArt;

        private BindingSource bndResult;

        private BindingSource bndResultAll;

        private BindingSource bndIsnotFound;

        private BindingSource bndExt;

        public frmFindFiles()
        {
            InitializeComponent();

            dbw = new DBWorker.DBW();

            this.bndResultAll = new BindingSource();

            Bind();

            BindExt();

            BindGridArticul();

            BindResults();

            BindIsNotFound();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //DirectoryInfo dir = new DirectoryInfo(@"Z:\\");
            //foreach (var item in dir.GetDirectories())
            //{
            //    //Console.WriteLine(item.Name);
            //    this.listPath.Items.Add(item.FullName);
            //    foreach (var it in item.GetDirectories())
            //        this.listPath.Items.Add(it.FullName);
            //}
            ////Console.ReadLine();

            //bgwSrc.RunWorkerAsync();
        }

        #region BIND
        /// <summary>
        /// 
        /// </summary>
        void BindExt()
        {
            this.bndExt = new BindingSource();
            this.bndExt.ListChanged += BndExt_ListChanged;
            this.bndExt.DataSource = dbw.Ext.GetExtList();

            this.dgwFilterExt.AutoGenerateColumns = false;
            this.dgwFilterExt.DataSource = this.bndExt;
            this.dgwFilterExt.BorderStyle = BorderStyle.None;

            DataGridViewCheckBoxColumn clc = new DataGridViewCheckBoxColumn();
            clc.Width = 20;
            clc.DataPropertyName = "Selected";
            clc.Name = "columnSelected";
            clc.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clc.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgwFilterExt.Columns.Add(clc);

            DataGridViewColumn clnName = new DataGridViewTextBoxColumn();
            clnName.DataPropertyName = "Name";
            clnName.HeaderText = "Назва";
            clnName.Name = "Name";
            clnName.Width = 90;
            clnName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnName.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnName.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwFilterExt.Columns.Add(clnName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BndExt_ListChanged(object sender, ListChangedEventArgs e)
        {
            BindingSource bind = (BindingSource)sender;
            IEnumerable<SelectImgExt> exts = (IEnumerable<SelectImgExt>)bind.List;
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (e.NewIndex == 0)
                {
                    SelectImgExt c = (SelectImgExt)bind.Current;
                    if (c.Selected)
                    {
                        foreach (SelectImgExt sie in exts)
                        {
                            sie.Selected = false;
                        }
                        c.Selected = true;
                        this.bndResult.DataSource = FillGridResult(false);
                        this.dgwResults.DataSource = this.bndResult;

                    }
                    else
                    {

                    }
                }
                else
                {
                    var o = exts.Where(w => w.ExtId == 0).FirstOrDefault();
                    o.Selected = false;

                    this.bndResult.DataSource = FillGridResult(true);
                    this.dgwResults.DataSource = this.bndResult;
                }

                this.dgwFilterExt.Refresh();
            }
        }

        void BindIsNotFound()
        {
            this.bndIsnotFound = new BindingSource();
            this.dgwIsNotFounds.AutoGenerateColumns = false;
            this.bndIsnotFound.DataSource = Helper.GetArticulList();
            this.dgwIsNotFounds.DataSource = bndIsnotFound;

            DataGridViewColumn clnId = new DataGridViewTextBoxColumn();
            clnId.Name = "Id";
            clnId.HeaderText = "#";
            clnId.DataPropertyName = "Id";
            clnId.Width = 25;
            clnId.ReadOnly = true;
            clnId.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnId.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwIsNotFounds.Columns.Add(clnId);

            DataGridViewColumn clnArticuls = new DataGridViewTextBoxColumn();
            clnArticuls.Name = "columnArticuls";
            clnArticuls.HeaderText = "Артикул(и)";
            clnArticuls.DataPropertyName = "Value";
            clnArticuls.Width = 70;
            clnArticuls.ReadOnly = true;
            clnArticuls.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnArticuls.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnArticuls.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnArticuls.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgwIsNotFounds.Columns.Add(clnArticuls);

            DataGridViewColumn clnDescryption = new DataGridViewTextBoxColumn();
            clnDescryption.Name = "columnDescription";
            clnDescryption.HeaderText = "Коментарый до артикулу...";
            clnDescryption.DataPropertyName = "Description";
            clnDescryption.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            clnDescryption.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnDescryption.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwIsNotFounds.Columns.Add(clnDescryption);
        }

        void BindResults()
        {
            this.bndResult = new BindingSource();
            this.dgwResults.AutoGenerateColumns = false;
            this.bndResult.DataSource = Helper.GetFresultList();

            this.bndResultAll.DataSource = Helper.GetFresultList();

            this.dgwResults.DataSource = this.bndResult;

            DataGridViewColumn clnId = new DataGridViewTextBoxColumn();
            clnId.Name = "Id";
            clnId.HeaderText = "#";
            clnId.DataPropertyName = "Id";
            clnId.Width = 25;
            clnId.ReadOnly = true;
            clnId.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnId.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(clnId);

            DataGridViewColumn clnArticuls = new DataGridViewTextBoxColumn();
            clnArticuls.Name = "columnArticuls";
            clnArticuls.HeaderText = "Артикул(и)";
            clnArticuls.DataPropertyName = "Articul";
            clnArticuls.Width = 70;
            clnArticuls.ReadOnly = true;
            clnArticuls.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnArticuls.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnArticuls.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnArticuls.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgwResults.Columns.Add(clnArticuls);

            DataGridViewColumn clnFilePath = new DataGridViewTextBoxColumn();
            clnFilePath.Name = "columnDescripion";
            clnFilePath.HeaderText = "Назва товару";
            clnFilePath.DataPropertyName = "Descripion";
            clnFilePath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            clnFilePath.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnFilePath.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(clnFilePath);

            DataGridViewColumn clnSource = new DataGridViewTextBoxColumn();
            clnSource.Name = "columnSourceName";
            clnSource.HeaderText = "Джерело";
            clnSource.Width = 50;
            clnSource.DataPropertyName = "SourceName";
            clnSource.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnSource.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(clnSource);
            ///
            DataGridViewColumn clnFileName = new DataGridViewTextBoxColumn();
            clnFileName.Name = "columnFileName";
            clnFileName.HeaderText = "Файл";
            clnFileName.DataPropertyName = "FileName";
            clnFileName.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnFileName.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(clnFileName);
            ///
            DataGridViewColumn clnExtension = new DataGridViewTextBoxColumn();
            clnExtension.Name = "columnExtension";
            clnExtension.HeaderText = "Ext";
            clnExtension.Width = 40;
            clnExtension.DataPropertyName = "Extension";
            clnExtension.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnExtension.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(clnExtension);

            ///
            DataGridViewColumn clnSize = new DataGridViewTextBoxColumn();
            clnSize.Name = "columnSize";
            clnSize.HeaderText = "Size";
            clnSize.Width = 60;
            clnSize.DataPropertyName = "Size";
            clnSize.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnSize.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(clnSize);

            DataGridViewColumn clnShowView = new DataGridViewButtonColumn();
            clnShowView.Name = "columnShowViewResult";
            clnShowView.HeaderText = "Перегляд";
            clnShowView.Width = 60;
            clnShowView.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnShowView.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(clnShowView);


            DataGridViewColumn clnShowDirectory = new DataGridViewButtonColumn();
            clnShowDirectory.Name = "columnShowDirectory";
            clnShowDirectory.HeaderText = string.Empty;
            clnShowDirectory.Width = 30;
            clnShowDirectory.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnShowDirectory.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(clnShowDirectory);

            DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
            c.Width = 50;
            c.Name = "columnSelect";
            c.DataPropertyName = "Selected";
            c.HeaderText = "Вибрати";
            c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwResults.Columns.Add(c);
            //DataGridViewImageColumn clnImg = new DataGridViewImageColumn();
            //clnImg.HeaderText = "Вибрати";
            //clnImg.Name = "columnSelect";
            //clnImg.Image = Rsc.Empty;
            //clnImg.Width = 60;
            //clnImg.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            //clnImg.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            //this.dgwResults.Columns.Add(clnImg);
        }

        void BindGridArticul()
        {
            this.bndArt = new BindingSource();
            this.dgwArts.AutoGenerateColumns = false;
            this.bndArt.DataSource = Helper.GetArticulList();
            this.dgwArts.DataSource = this.bndArt;

            DataGridViewColumn clnId = new DataGridViewTextBoxColumn();
            clnId.Name = "Id";
            clnId.HeaderText = "#";
            clnId.DataPropertyName = "Id";
            clnId.Width = 25;
            clnId.ReadOnly = true;
            clnId.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnId.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwArts.Columns.Add(clnId);

            DataGridViewColumn clnArticuls = new DataGridViewTextBoxColumn();
            clnArticuls.Name = "columnArticuls";
            clnArticuls.HeaderText = "Артикул(и)";
            clnArticuls.DataPropertyName = "Value";
            clnArticuls.Width = 70;
            clnArticuls.ReadOnly = true;
            clnArticuls.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnArticuls.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnArticuls.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnArticuls.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgwArts.Columns.Add(clnArticuls);

            DataGridViewColumn clnDescryption = new DataGridViewTextBoxColumn();
            clnDescryption.Name = "columnDescription";
            clnDescryption.HeaderText = "Коментарый до артикулу...";
            clnDescryption.DataPropertyName = "Description";
            clnDescryption.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            clnDescryption.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnDescryption.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwArts.Columns.Add(clnDescryption);

            DataGridViewButtonColumn clnDelete = new DataGridViewButtonColumn();
            clnDelete.HeaderText = "Видалити";
            clnDelete.Name = "columnDelete";
            clnDelete.Width = 60;
            //Bitmap bmDelete = new Bitmap((Image)Rsc.Delete.ToBitmap(), new Size(16, 16));
            clnDelete.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnDelete.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwArts.Columns.Add(clnDelete);
        }

        void Bind()
        {
            this.bndSrc = new BindingSource();
            //this.bndSrc.Add(new Classes.Src(1, "\\192.168.1.107\\", 0));
            this.bndSrc.DataSource = dbw.Pth.GetPathList();

            this.dgwSrc.AutoGenerateColumns = false;
            //this.dgwSrc.AutoSize = true;
            this.dgwSrc.DataSource = this.bndSrc;
            this.dgwSrc.ColumnHeadersHeight = 35;

            DataGridViewColumn clnId = new DataGridViewTextBoxColumn();
            clnId.DataPropertyName = "Id";
            clnId.HeaderText = "#";
            clnId.Name = "Id";
            clnId.Width = 25;
            clnId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnId.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnId.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwSrc.Columns.Add(clnId);

            DataGridViewColumn clnName = new DataGridViewTextBoxColumn();
            clnName.DataPropertyName = "Name";
            clnName.HeaderText = "Назва";
            clnName.Name = "Name";
            clnName.Width = 140;
            clnName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnName.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnName.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwSrc.Columns.Add(clnName);

            DataGridViewColumn clnPath = new DataGridViewTextBoxColumn();
            clnPath.DataPropertyName = "PathValue";
            clnPath.HeaderText = "Шлях";
            clnPath.Name = "PathValue";
            clnPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            clnPath.ReadOnly = true;
            //clnPath.Width = 360;
            //clnPath.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnPath.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnPath.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwSrc.Columns.Add(clnPath);

            DataGridViewColumn clnQty = new DataGridViewTextBoxColumn();
            clnQty.DataPropertyName = "Qty";
            clnQty.HeaderText = "Поточна кількість";
            clnQty.Name = "Qty";
            clnQty.Width = 60;
            clnQty.ReadOnly = true;
            clnQty.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
            clnQty.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnQty.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnQty.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnQty.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgwSrc.Columns.Add(clnQty);

            DataGridViewColumn clnBtn = new DataGridViewButtonColumn();
            clnBtn.Name = "columnStartScanning";
            clnBtn.HeaderText = "Аналіз";
            clnBtn.Width = 60;
            clnBtn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnBtn.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            this.dgwSrc.Columns.Add(clnBtn);

            //DataGridViewColumn clnShowView = new DataGridViewButtonColumn();
            //clnShowView.Name = "columnShowView";
            //clnShowView.HeaderText = "Перегляд";
            //clnShowView.Width = 60;
            //clnShowView.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //clnShowView.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            //this.dgwSrc.Columns.Add(clnShowView);

            DataGridViewTextBoxColumn clnStatus = new DataGridViewTextBoxColumn();
            clnStatus.Name = "columnStatus";
            clnStatus.HeaderText = "Статус аналізу";
            clnStatus.Width = 190;
            clnStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            clnStatus.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnStatus.ReadOnly = true;

            this.dgwSrc.Columns.Add(clnStatus);

            DataGridViewButtonColumn clnDelete = new DataGridViewButtonColumn();
            clnDelete.HeaderText = "Видалити";
            clnDelete.Name = "columnDelete";
            clnDelete.Width = 60;
            Bitmap bmDelete = new Bitmap((Image)Rsc.Delete.ToBitmap(), new Size(16, 16));
            clnDelete.HeaderCell.Style.Font = new System.Drawing.Font("Tahoma", 8);
            clnDelete.CellTemplate.Style.Font = new System.Drawing.Font("Tahoma", 8);

            this.dgwSrc.Columns.Add(clnDelete);
            this.dgwSrc.Width = 1080;

            this.BwDownloadData.RunWorkerAsync();
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwSrc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex].Name == "columnStartScanning" &&
                e.RowIndex >= 0)
            {
                if (!senderGrid.Rows[e.RowIndex].ReadOnly)
                {
                    var answer = MessageBox.Show(
                        string.Format("Запустити аналіз по '{0}'", senderGrid.Rows[e.RowIndex].Cells["PathValue"].Value),
                        "Увага!"
                        , MessageBoxButtons.YesNo);
                    if (answer == DialogResult.Yes)
                    {
                        senderGrid.ClearSelection();
                        senderGrid.Rows[e.RowIndex].ReadOnly = true;

                        Helper.SetRowColor(senderGrid.Rows[e.RowIndex], Color.Gray);

                        Place pl = (Place)this.bndSrc.Current;

                        if (pl.Worker == null)
                        {
                            Models.BW bw = new Models.BW(
                                senderGrid.Rows[e.RowIndex].Cells["PathValue"].Value.ToString(),
                                Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["Id"].Value)
                                );
                            bw.SetCurrentRow(senderGrid.Rows[e.RowIndex]);
                            bw.Start();
                        }
                        else
                        {
                            ((Models.BW)pl.Worker).SetCurrentRow(senderGrid.Rows[e.RowIndex]);
                        }
                    }
                }
                else
                {
                    senderGrid.ClearSelection();
                }
            }
            else if (senderGrid.Columns[e.ColumnIndex].Name == "columnDelete")
            {
                // Видалення
                Place pl = (Place)this.bndSrc.Current;
                var answer = MessageBox.Show(
                        string.Format("Видалити шлях '{0}'", pl.PathValue),
                        "Увага!"
                        , MessageBoxButtons.YesNo);
                if (answer == DialogResult.Yes)
                {
                    DataModels.Path p = (DataModels.Path)this.bndSrc.Current;
                    this.dbw.Pth.DeletePath(p.Id);
                    this.bndSrc.RemoveCurrent();
                }
            }
        }

        private void dgwSrc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var senderGrid = (DataGridView)sender;
            if (senderGrid.Rows[e.RowIndex].ReadOnly)
            {
                senderGrid.ClearSelection();
            }
        }

        private void btnFindArticul_Click(object sender, EventArgs e)
        {
            this.dgwResults.DataSource = null;

            SetEnabledFinds(false);

            this.BwFindArticul.RunWorkerAsync();
        }

        private void dgwSrc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView senderGrid = (DataGridView)sender;

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex < 0)
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            if (senderGrid.Columns[e.ColumnIndex].Name == "columnStartScanning")
            {
                Bitmap bm = Rsc.Settings;
                var w = bm.Width;
                var h = bm.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(bm, new Rectangle(x, y, 15, 15));
            }
            if (senderGrid.Columns[e.ColumnIndex].Name == "columnDelete")
            {
                Bitmap bm = Rsc.Delete.ToBitmap();
                var w = bm.Width;
                var h = bm.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(bm, new Rectangle(x, y, 15, 15));
            }

            if (senderGrid.Columns[e.ColumnIndex].Name == "columnShowView")
            {
                Bitmap bm = Rsc.View.ToBitmap();
                var w = bm.Width;
                var h = bm.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(bm, new Rectangle(x, y, 15, 15));
            }

            e.Handled = true;
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            DataModels.Path current = (DataModels.Path)this.bndSrc.AddNew();
            current.Id = dbw.Pth.CreatePathRecord(folderBrowserDialog.SelectedPath.ToString());
            current.PathValue = folderBrowserDialog.SelectedPath.ToString();
        }

        private void dgwSrc_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataModels.Path p = (DataModels.Path)this.bndSrc.Current;
            this.dbw.Pth.UpdatePath(p.Id, p.Name);
        }

        private void btnAddFolderFromBuffer_Click(object sender, EventArgs e)
        {
            string input = Clipboard.GetText().Replace("\r\n", ";");
            if (input.Length < this.freeBufferLength)
            {
                int num = (int)MessageBox.Show("В буфері пусто", "Увага", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            else
            {
                DataModels.Path current = (DataModels.Path)this.bndSrc.AddNew();
                current.Id = dbw.Pth.CreatePathRecord(input);
                current.PathValue = input;
            }
            Clipboard.Clear();
        }

        void ArticulAdd(string articul, string description)
        {
            if (articul.Length == 8)
            {
                IEnumerable<Articul> a = (List<Articul>)this.bndArt.List;

                if (a.Where(w => w.Value == articul).Count() == 0)
                {
                    long max = 0;
                    if (a.Count() != 0)
                    {
                        max = a.Max(m => m.Id);
                    }
                    this.bndArt.Add(new Models.Articul
                    {
                        Id = max + 1,
                        Value = articul,
                        Description = description
                    });
                }
            }
        }

        private void bntAddArt_Click(object sender, EventArgs e)
        {
            ArticulAdd(this.mtxtArt.Text.Trim(), string.Empty);
        }

        private void btnAddArtFromBuffer_Click(object sender, EventArgs e)
        {
            string[] separators = new string[] { "\r\n" };
            string[] source = Clipboard.GetText().Split(separators, StringSplitOptions.None);
            if (source.Length > 0)
            {
                foreach (string sc in source)
                {
                    string[] co = sc.Split(new string[] { "\t" }, StringSplitOptions.None);
                    if (co.Length > 0)
                    {
                        string articul = co[0];
                        string description = co.Length >= 2 ? co[1] : string.Empty;
                        if (co[0].Trim().Length > 0)
                        {
                            int r;
                            if (int.TryParse(articul, out r))
                            {
                                ArticulAdd(articul, description);
                            }
                        }
                    }
                }
            }
            else
            {
                int num = (int)MessageBox.Show("В буфері пусто", "Увага!", MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            Clipboard.Clear();
        }

        private void dgwArts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView senderGrid = (DataGridView)sender;

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex < 0)
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);
            if (senderGrid.Columns[e.ColumnIndex].Name == "columnDelete")
            {
                Bitmap bm = Rsc.Delete.ToBitmap();
                var w = bm.Width;
                var h = bm.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(bm, new Rectangle(x, y, 15, 15));
            }
            e.Handled = true;
        }

        private void dgwArts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex].Name == "columnDelete" &&
                e.RowIndex >= 0)
            {
                // Видалення
                Articul art = (Articul)this.bndArt.Current;
                var answer = MessageBox.Show(
                        string.Format("Видалити артикул: '{0}'", art.Value),
                        "Увага!"
                        , MessageBoxButtons.YesNo);
                if (answer == DialogResult.Yes)
                {
                    this.bndArt.Remove(art);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.bndArt.Clear();
        }

        private void BwDownloadData_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker b = (BackgroundWorker)sender;
            IEnumerable<Place> pls = (IEnumerable<Place>)this.bndSrc.DataSource;
            foreach (Place p in pls)
            {
                b.ReportProgress(0, string.Format("Завантажуються дані: {0} ({1})", p.Name, p.PathValue));
                p.Lst = dbw.Pth.GetFileList(p.Id);
            }
        }

        private void BwDownloadData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.tssStatus.Text = e.UserState.ToString();
        }

        private void BwDownloadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.tssStatus.Text = string.Empty;
            this.tssStatusLabel.Text = string.Empty;

            this.dgwSrc.Enabled = true;
        }

        private void btnSetBuffer_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            StringBuilder sb = new StringBuilder();
            if (this.bndIsnotFound.List.Count > 0)
            {
                foreach (var o in this.bndIsnotFound.List)
                {
                    Articul a = (Articul)o;
                    sb.AppendFormat("{0}{1}{2}{3}", a.Value, "\t", a.Description, Environment.NewLine);
                }
                Clipboard.SetText(sb.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwResults_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView senderGrid = (DataGridView)sender;

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex < 0)
                return;
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);
            if (senderGrid.Columns[e.ColumnIndex].Name == "columnShowViewResult")
            {
                Bitmap bm = Rsc.View.ToBitmap();
                var w = bm.Width;
                var h = bm.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(bm, new Rectangle(x, y, 15, 15));
                e.Handled = true;
            }

            if (senderGrid.Columns[e.ColumnIndex].Name == "columnShowDirectory")
            {
                Bitmap bm = Rsc.Open_16x16;
                var w = bm.Width;
                var h = bm.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                e.Graphics.DrawImage(bm, new Rectangle(x, y, 15, 15));
                e.Handled = true;
            }
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BwFindArticul_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bw = (BackgroundWorker)sender;

                this.bndResultAll.Clear();
                IEnumerable<Place> ps = (IEnumerable<Place>)this.bndSrc.DataSource;
                IEnumerable<Articul> alst = (List<Articul>)this.bndArt.List;

                string[] mm = new string[alst.Count()];

                int counter = 0;
                foreach (Articul a in alst)
                {
                    mm[counter] = a.Value;
                    counter++;
                }

                if (alst.Count() == 0) return;
                List<PlaceFile> except = new List<PlaceFile>();

                if (!this.chFindAll.Checked)
                {
                    Place place = (Place)this.bndSrc.Current;
                    var rs = place.Lst.Where(w => w.IsExistsArt(mm, ref bw)).ToList();
                    if (rs.Count > 0)
                    {
                        foreach (PlaceFile p in rs)
                        {
                            bw.ReportProgress(0, p.FileName);

                            if (File.Exists(p.FilePath))
                            {
                                this.bndResultAll.Add(new Fresult
                                {
                                    Id = p.Id,
                                    Extension = p.Extension,
                                    FileName = p.FileName,
                                    FilePath = p.FilePath,
                                    SourceName = this.dbw.Pth.GetSourceNameById(p.PathId),
                                    Articul = p.Art,
                                    Selected = false,
                                    PathId = p.PathId,
                                    Descripion = alst.Where(m => m.Value == p.Art).FirstOrDefault().Description
                                });

                            }
                            else
                            {
                                except.Add(p);
                                this.dbw.Pth.DeletePathFilesItem(p.Id);
                            }
                        }

                        place.Lst = place.Lst.Except(except);
                    }
                }
                else
                {
                    foreach (Place place in ps)
                    {
                        foreach (Articul art in alst)
                        {
                            bw.ReportProgress(0, string.Format("Пошук артикула: {0}", art.Value.ToString()));

                            var ft =
                                from source in place.Lst
                                where source.FileName.Contains(art.Value)
                                select new Fresult
                                {
                                    Id = source.Id,
                                    Extension = source.Extension,
                                    FileName = source.FileName,
                                    FilePath = source.FilePath,
                                    SourceName = this.dbw.Pth.GetSourceNameById(source.PathId),
                                    Articul = art.Value,
                                    Selected = false,
                                    PathId = source.PathId,
                                    Descripion = alst.Where(m => m.Value == art.Value).FirstOrDefault() == null ? string.Empty : alst.Where(m => m.Value == art.Value).FirstOrDefault().Description
                                };

                            foreach (Fresult sc in ft)
                            {
                                if (File.Exists(sc.FilePath))
                                {
                                    this.bndResultAll.Add(new Fresult
                                    {
                                        Id = sc.Id,
                                        Extension = sc.Extension,
                                        FileName = sc.FileName,
                                        FilePath = sc.FilePath,
                                        SourceName = this.dbw.Pth.GetSourceNameById(sc.PathId),
                                        Articul = sc.Articul,
                                        Selected = sc.Selected,
                                        PathId = sc.PathId,
                                        Descripion = sc.Descripion
                                    });
                                }
                                else
                                {
                                    except.Add(new PlaceFile
                                    {
                                        Id = sc.Id,
                                        Extension = sc.Extension,
                                        FileName = sc.FileName,
                                        FilePath = sc.FilePath
                                    });
                                    this.dbw.Pth.DeletePathFilesItem(sc.Id);
                                }
                            }

                            //from la in ps
                            //join re in alst on la.Name equals re.Value into pso  //la. equals re.Articul into pso
                            //                    from re in pso.DefaultIfEmpty()
                            //select new Articul { Id = la.Id, Value = la.Value, Description = la.Description, IsFound = re == null ? 0 : 1 };
                        }

                        /*
                        var rs = place.Lst.Where(w => w.IsExistsArt(mm, ref bw)).ToList();
                        if (rs.Count > 0)
                        {
                            foreach (PlaceFile p in rs)
                            {
                                if (File.Exists(p.FilePath))
                                {
                                    this.bndResultAll.Add(new Fresult
                                    {
                                        Id = p.Id,
                                        Extension = p.Extension,
                                        FileName = p.FileName,
                                        FilePath = p.FilePath,
                                        SourceName = this.dbw.Pth.GetSourceNameById(p.PathId),
                                        Articul = p.Art,
                                        Selected = false,
                                        PathId = p.PathId,
                                        Descripion = alst.Where(m => m.Value == p.Art).FirstOrDefault().Description
                                    });
                                }
                                else
                                {
                                    except.Add(p);
                                    this.dbw.Pth.DeletePathFilesItem(p.Id);
                                }
                            }
                        }
                    }
                    */


                        //IEnumerable<Fresult> ores = (IEnumerable<Fresult>)bndResult.List;

                        //var qn =
                        //   from la in alst
                        //   join re in ores on la.Value equals re.Articul into pso
                        //   from re in pso.DefaultIfEmpty()
                        //   select new Articul { Id = la.Id, Value = la.Value, Description = la.Description, IsFound = re == null ? 0 : 1 };

                        //this.bndIsnotFound.DataSource = qn.Where(w => w.IsFound == 0).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                MessageBox.Show(ex.Message, "В додатку виникла помилка!");
            }
        }

        private void BwFindArticul_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            this.bndResult.DataSource = FillGridResult(false);// lst.OrderBy(o => o.Articul).ToList();

            this.dgwResults.DataSource = this.bndResult;

            this.bndResult.ResetBindings(true);
            this.tssStatusLabel.Text = string.Empty;

            IEnumerable<Fresult> ores = (IEnumerable<Fresult>)bndResult.List;
            IEnumerable<Articul> alst = (IEnumerable<Articul>)bndArt.List;

            IEnumerable<Articul> qn =
               from la in alst
               join re in ores on la.Value equals re.Articul into pso
               from re in pso.DefaultIfEmpty()
               select new Articul { Id = la.Id, Value = la.Value, Description = la.Description, IsFound = re == null ? 0 : 1 };

            var query = qn.Distinct();

            this.bndIsnotFound.DataSource = query.Where(w => w.IsFound == 0).ToList();

            System.Threading.Thread.Sleep(1000);

            SetEnabledFinds(true);
        }

        IEnumerable<Fresult> FillGridResult(bool filtred)
        {
            IEnumerable<Fresult> lst = (IEnumerable<Fresult>)this.bndResultAll.List;
            IEnumerable<SelectImgExt> extensions = ((IEnumerable<SelectImgExt>)this.bndExt.List).Where(w => w.Selected == true);
            if (filtred)
            {
                var listFilters =
                    from l in lst
                    join es in extensions on l.Extension equals es.Name
                    select new Fresult
                    {
                        Articul = l.Articul,
                        Descripion = l.Descripion,
                        Extension = l.Extension,
                        FileName = l.FileName,
                        FilePath = l.FilePath,
                        Id = l.Id,
                        PathId = l.PathId,
                        Selected = l.Selected,
                        SourceName = l.SourceName
                    };
                return listFilters;
            }
            else
            {
                return lst;
            }
        }

        private void cSelectDeselect_CheckedChanged(object sender, EventArgs e)
        {
            IEnumerable<Fresult> results = (IEnumerable<Fresult>)this.bndResult.List;
            if (((CheckBox)sender).Checked)
            {
                foreach (Fresult r in results)
                {
                    r.Selected = true;
                }
            }
            else
            {
                foreach (Fresult r in results)
                {
                    r.Selected = false;
                }
            }
            this.dgwResults.Refresh();
        }

        private void btnSetDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                this.txtCopyPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void btnCopyFiles_Click(object sender, EventArgs e)
        {

            if (this.txtCopyPath.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Путь копіювання не вказаний.");
                return;
            }

            SetEnabled(false);
            this.bwCopy.RunWorkerAsync();
        }

        private void bwCopy_DoWork(object sender, DoWorkEventArgs e)
        {
            Current current = null;
            BackgroundWorker bw = (BackgroundWorker)sender;
            foreach (Fresult fr in this.bndResult.List)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (fr.Selected)
                {
                    bw.ReportProgress(0, fr);

                    if (!this.cCopyDir.Checked)
                    {
                        #region
                        if (!File.Exists(string.Format("{0}\\{1}{2}", this.txtCopyPath.Text, fr.FileName, fr.Extension)))
                        {
                            File.Copy(fr.FilePath, string.Format("{0}\\{1}{2}", this.txtCopyPath.Text, fr.FileName, fr.Extension));
                        }
                        else
                        {
                            DirectoryInfo di = new DirectoryInfo(fr.FilePath);
                            FileInfo fiOld = new FileInfo(string.Format("{0}\\{1}{2}", this.txtCopyPath.Text, fr.FileName, fr.Extension));
                            FileInfo fiNew = new FileInfo(fr.FilePath);

                            List<Oa> list = new List<Oa>();

                            list.Add(new Oa { id = 1, text = string.Format("Файл: {0}{1}", fr.FileName, fr.Extension) });
                            list.Add(new Oa { id = 2, text = string.Format("розташований по: {0}", fiNew.DirectoryName) });
                            list.Add(new Oa { id = 3, text = string.Format("має копію в: {0}", fiOld.DirectoryName) });
                            list.Add(new Oa { id = 4, text = string.Format("розмір старого файлу: {0}", string.Format("{0} Kb", (fiOld.Length / 1024).ToString())) });
                            list.Add(new Oa { id = 5, text = string.Format("розмір нового файлу: {0}", string.Format("{0} Kb", (fiNew.Length / 1024).ToString())) });

                            DialogResult dr;
                            if (current == null)
                            {
                                Ha ha = new Ha();
                                FormQuestion qf = new FormQuestion(
                                    "УВАГА!",
                                    list,
                                    TyepQuest.IgnoreAbort, ref ha);
                                dr = qf.ShowDialog();
                                if (ha.MM)
                                {
                                    current = new Current { C = dr };
                                }
                            }
                            else
                            {
                                dr = current.C;
                            }

                            switch (dr)
                            {
                                case DialogResult.Abort:
                                    {
                                        return;
                                    }
                                case DialogResult.Ignore:
                                    {
                                        break;
                                    }
                                case DialogResult.OK:
                                    {
                                        File.Copy(fr.FilePath, string.Format("{0}\\{1}{2}",
                                            this.txtCopyPath.Text,
                                            string.Format("{0}_{1}", fr.FileName, Guid.NewGuid().ToString()),
                                            fr.Extension));
                                        break;
                                    }
                            }

                        }
                        fr.Selected = false;
                        #endregion
                    }
                    else
                    {
                        IEnumerable<Place> places = (IEnumerable<Place>)this.bndSrc.List;
                        string pts = places.Where(w => w.Id == fr.PathId).FirstOrDefault().PathValue;
                        string filePth = fr.FilePath.Replace(pts, this.txtCopyPath.Text);
                        FileInfo fi = new FileInfo(filePth);
                        DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
                        if (!di.Exists)
                        {
                            di.Create();
                        }
                        if (!File.Exists(string.Format("{0}\\{1}{2}", fi.DirectoryName, fr.FileName, fr.Extension)))
                        {
                            File.Copy(fr.FilePath, string.Format("{0}\\{1}{2}", fi.DirectoryName, fr.FileName, fr.Extension));
                        }
                        else
                        {
                            File.Copy(fr.FilePath, string.Format("{0}\\{1}{2}",
                                fi.DirectoryName,
                                string.Format("{0}_{1}", fr.FileName, Guid.NewGuid().ToString()),
                                fr.Extension));
                        }
                    }
                }
            }
        }

        private void bwCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tssStatusLabel.Text = string.Empty;
            SetEnabled(true);
            this.dgwResults.Refresh();
            MessageBox.Show("Копіювання завершено.");
        }

        void SetEnabled(bool b)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name == "grpResults")
                {
                    foreach (Control o in c.Controls)
                    {
                        if (o.Name == "btnCancelCopy")
                        {
                            o.Enabled = !b;
                            o.Visible = !b;
                        }
                        else
                        {
                            o.Enabled = b;
                        }
                    }

                }
                else
                    c.Enabled = b;
            }
        }

        void SetEnabledFinds(bool b)
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = b;
            }
        }

        private void btnCancelCopy_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Припинити копіювання?", "УВАГА!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.bwCopy.CancelAsync();
        }

        private void bwCopy_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Fresult f = (Fresult)e.UserState;
            tssStatusLabel.Text = string.Format("Копіюється файл: {0}{1}", f.FileName, f.Extension);
            int p = this.bndResult.List.IndexOf(f);
            this.bndResult.Position = p;
        }

        private void btnClearResults_Click(object sender, EventArgs e)
        {
            this.bndResult.Position = -1;
            this.bndResult.Clear();
        }

        private void BwFindArticul_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.tssStatusLabel.Text = e.UserState.ToString();
        }

        private void dgwResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex].Name == "columnShowViewResult" &&
                e.RowIndex >= 0)
            {
                FrmShowImage frmShowImage = new FrmShowImage((Fresult)this.bndResult.Current);
                frmShowImage.ShowDialog();
            }

            if (senderGrid.Columns[e.ColumnIndex].Name == "columnShowDirectory" && e.RowIndex >= 0)
            {
                Fresult current = (Fresult)this.bndResult.Current;
                FileInfo fi = new FileInfo(current.FilePath);
                System.Diagnostics.Process.Start(fi.DirectoryName);
            }
        }

        private void dgwFilterExt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView g = (DataGridView)sender;
            if (g.Columns[e.ColumnIndex].Name == "columnSelected")
            {
                g.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange);
            }
        }

        private void dgwResults_SelectionChanged(object sender, EventArgs e)
        {
            if (this.bndResult.List.Count > 0)
            {
                if (this.bndResult.Position != -1)
                {
                    if (this.bndResult.Current != null && this.bndResult.Current.GetType().Name == "Fresult")
                    {
                        Fresult current = (Fresult)this.bndResult.Current;
                        this.lblPath.Text = string.Format("Шлях: {0}", current.FilePath);
                    }
                }
            }
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            string excelFilePath = string.Empty;

            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "Excel (*.xlsx)|*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
                excelFilePath = ofd.FileName;

            if (excelFilePath != string.Empty)
            {
                var excelApp = new Excel.Application();
                excelApp.Workbooks.Add();
                Excel._Worksheet wsheet = excelApp.ActiveSheet;

                IEnumerable<Fresult> ft = (IEnumerable<Fresult>)this.bndResult.List;
                wsheet.Cells[1, 1] = "id";
                wsheet.Cells[1, 2] = "Descripion";
                wsheet.Cells[1, 3] = "Articul";
                wsheet.Cells[1, 4] = "FileName";
                wsheet.Cells[1, 5] = "FilePath";
                int i = 2;
                foreach (Fresult f in ft)
                {
                    wsheet.Cells[i, 1] = f.Id;
                    wsheet.Cells[i, 2] = f.Descripion;
                    wsheet.Cells[i, 3] = f.Articul;
                    wsheet.Cells[i, 4] = f.FileName;
                    wsheet.Cells[i, 5] = f.FilePath;
                    i++;
                }
                wsheet.SaveAs(excelFilePath);
                excelApp.Quit();

                char[] separator = { '.' };
                string[] lp = excelFilePath.Split(separator);

                if (lp[lp.Length - 1] != "xlsx")
                    excelFilePath = excelFilePath + ".xlsx";

                System.Diagnostics.Process.Start(excelFilePath);
            }
        }

        public void ExportToExcel(DataTable tbl, string excelFilePath = null)
        {
            try
            {
                if (tbl == null || tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (var i = 0; i < tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = tbl.Columns[i].ColumnName;
                }

                // rows
                for (var i = 0; i < tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = tbl.Rows[i][j];
                    }
                }

                // check file path
                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    try
                    {
                        workSheet.SaveAs(excelFilePath);
                        excelApp.Quit();
                        MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else
                { // no file path is given
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
    }

    public class Oa
    {
        public int id { get; set; }

        public string text { get; set; }
    }

    public class Current
    {
        public DialogResult C { get; set; }
    }

    public class Ha : INotifyPropertyChanged
    {
        private bool _value;
        public bool MM
        {
            get { return _value; }
            set
            {
                _value = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("Value"));
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}