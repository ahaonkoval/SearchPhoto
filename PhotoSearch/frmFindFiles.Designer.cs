namespace PhotoSearch
{
    partial class frmFindFiles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFind = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chFindAll = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgwArts = new System.Windows.Forms.DataGridView();
            this.tabPageNotFound = new System.Windows.Forms.TabPage();
            this.btnSetBuffer = new System.Windows.Forms.Button();
            this.dgwIsNotFounds = new System.Windows.Forms.DataGridView();
            this.dgwSrc = new System.Windows.Forms.DataGridView();
            this.mtxtArt = new FotoS.NumericTextBox();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.bntAddArt = new System.Windows.Forms.Button();
            this.btnAddFolderFromBuffer = new System.Windows.Forms.Button();
            this.btnFindArticul = new System.Windows.Forms.Button();
            this.btnAddArtFromBuffer = new System.Windows.Forms.Button();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.btnToExcel = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.dgwFilterExt = new System.Windows.Forms.DataGridView();
            this.btnCancelCopy = new System.Windows.Forms.Button();
            this.txtCopyPath = new System.Windows.Forms.TextBox();
            this.btnSetDir = new System.Windows.Forms.Button();
            this.btnClearResults = new System.Windows.Forms.Button();
            this.btnCopyFiles = new System.Windows.Forms.Button();
            this.cCopyDir = new System.Windows.Forms.CheckBox();
            this.cSelectDeselect = new System.Windows.Forms.CheckBox();
            this.dgwResults = new System.Windows.Forms.DataGridView();
            this.StripStatus = new System.Windows.Forms.StatusStrip();
            this.tssStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.BwDownloadData = new System.ComponentModel.BackgroundWorker();
            this.BwFindArticul = new System.ComponentModel.BackgroundWorker();
            this.bwCopy = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwArts)).BeginInit();
            this.tabPageNotFound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwIsNotFounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwSrc)).BeginInit();
            this.grpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFilterExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwResults)).BeginInit();
            this.StripStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(1085, 832);
            this.btnFind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(87, 28);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "btnFind";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chFindAll);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.dgwSrc);
            this.groupBox1.Controls.Add(this.mtxtArt);
            this.groupBox1.Controls.Add(this.btnAddFolder);
            this.groupBox1.Controls.Add(this.bntAddArt);
            this.groupBox1.Controls.Add(this.btnAddFolderFromBuffer);
            this.groupBox1.Controls.Add(this.btnFindArticul);
            this.groupBox1.Controls.Add(this.btnAddArtFromBuffer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1236, 388);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Перелік джерел:";
            // 
            // chFindAll
            // 
            this.chFindAll.AutoSize = true;
            this.chFindAll.Checked = true;
            this.chFindAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chFindAll.Location = new System.Drawing.Point(1102, 323);
            this.chFindAll.Name = "chFindAll";
            this.chFindAll.Size = new System.Drawing.Size(134, 20);
            this.chFindAll.TabIndex = 11;
            this.chFindAll.Text = "По всім джерелам";
            this.chFindAll.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(1102, 283);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(128, 26);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Очистити";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageNotFound);
            this.tabControl1.Location = new System.Drawing.Point(2, 147);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1094, 235);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgwArts);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1086, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Перелік артикулів";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgwArts
            // 
            this.dgwArts.AllowUserToAddRows = false;
            this.dgwArts.AllowUserToDeleteRows = false;
            this.dgwArts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwArts.Location = new System.Drawing.Point(3, 3);
            this.dgwArts.Name = "dgwArts";
            this.dgwArts.RowHeadersWidth = 10;
            this.dgwArts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwArts.Size = new System.Drawing.Size(1080, 200);
            this.dgwArts.TabIndex = 7;
            this.dgwArts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwArts_CellClick);
            this.dgwArts.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgwArts_CellPainting);
            // 
            // tabPageNotFound
            // 
            this.tabPageNotFound.Controls.Add(this.btnSetBuffer);
            this.tabPageNotFound.Controls.Add(this.dgwIsNotFounds);
            this.tabPageNotFound.Location = new System.Drawing.Point(4, 25);
            this.tabPageNotFound.Name = "tabPageNotFound";
            this.tabPageNotFound.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNotFound.Size = new System.Drawing.Size(1086, 206);
            this.tabPageNotFound.TabIndex = 1;
            this.tabPageNotFound.Text = "Не знайдено";
            this.tabPageNotFound.UseVisualStyleBackColor = true;
            // 
            // btnSetBuffer
            // 
            this.btnSetBuffer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetBuffer.Location = new System.Drawing.Point(870, 3);
            this.btnSetBuffer.Name = "btnSetBuffer";
            this.btnSetBuffer.Size = new System.Drawing.Size(128, 200);
            this.btnSetBuffer.TabIndex = 9;
            this.btnSetBuffer.Text = "Копіювати в буфер";
            this.btnSetBuffer.UseVisualStyleBackColor = true;
            this.btnSetBuffer.Click += new System.EventHandler(this.btnSetBuffer_Click);
            // 
            // dgwIsNotFounds
            // 
            this.dgwIsNotFounds.AllowUserToAddRows = false;
            this.dgwIsNotFounds.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgwIsNotFounds.Location = new System.Drawing.Point(3, 3);
            this.dgwIsNotFounds.Name = "dgwIsNotFounds";
            this.dgwIsNotFounds.RowHeadersWidth = 10;
            this.dgwIsNotFounds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwIsNotFounds.Size = new System.Drawing.Size(861, 200);
            this.dgwIsNotFounds.TabIndex = 8;
            // 
            // dgwSrc
            // 
            this.dgwSrc.AllowUserToAddRows = false;
            this.dgwSrc.Enabled = false;
            this.dgwSrc.Location = new System.Drawing.Point(9, 19);
            this.dgwSrc.Name = "dgwSrc";
            this.dgwSrc.RowHeadersWidth = 10;
            this.dgwSrc.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgwSrc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwSrc.Size = new System.Drawing.Size(1080, 124);
            this.dgwSrc.TabIndex = 8;
            this.dgwSrc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwSrc_CellClick);
            this.dgwSrc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwSrc_CellContentClick);
            this.dgwSrc.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwSrc_CellEndEdit);
            this.dgwSrc.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgwSrc_CellPainting);
            // 
            // mtxtArt
            // 
            this.mtxtArt.AllowSpace = false;
            this.mtxtArt.ColorFocus = false;
            this.mtxtArt.EventFireMode = false;
            this.mtxtArt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mtxtArt.HideInput = false;
            this.mtxtArt.IsSelectionCursorInBegin = false;
            this.mtxtArt.Location = new System.Drawing.Point(1102, 213);
            this.mtxtArt.MaxLength = 8;
            this.mtxtArt.Name = "mtxtArt";
            this.mtxtArt.Size = new System.Drawing.Size(128, 23);
            this.mtxtArt.TabIndex = 6;
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddFolder.Location = new System.Drawing.Point(1102, 51);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(128, 26);
            this.btnAddFolder.TabIndex = 3;
            this.btnAddFolder.Text = "Додати папку";
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // bntAddArt
            // 
            this.bntAddArt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bntAddArt.Location = new System.Drawing.Point(1102, 242);
            this.bntAddArt.Name = "bntAddArt";
            this.bntAddArt.Size = new System.Drawing.Size(128, 26);
            this.bntAddArt.TabIndex = 5;
            this.bntAddArt.Text = "Додати артикул";
            this.bntAddArt.UseVisualStyleBackColor = true;
            this.bntAddArt.Click += new System.EventHandler(this.bntAddArt_Click);
            // 
            // btnAddFolderFromBuffer
            // 
            this.btnAddFolderFromBuffer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddFolderFromBuffer.Location = new System.Drawing.Point(1102, 19);
            this.btnAddFolderFromBuffer.Name = "btnAddFolderFromBuffer";
            this.btnAddFolderFromBuffer.Size = new System.Drawing.Size(128, 26);
            this.btnAddFolderFromBuffer.TabIndex = 2;
            this.btnAddFolderFromBuffer.Text = "Додати з буферу";
            this.btnAddFolderFromBuffer.UseVisualStyleBackColor = true;
            this.btnAddFolderFromBuffer.Click += new System.EventHandler(this.btnAddFolderFromBuffer_Click);
            // 
            // btnFindArticul
            // 
            this.btnFindArticul.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFindArticul.Location = new System.Drawing.Point(1102, 349);
            this.btnFindArticul.Name = "btnFindArticul";
            this.btnFindArticul.Size = new System.Drawing.Size(128, 26);
            this.btnFindArticul.TabIndex = 9;
            this.btnFindArticul.Text = "Шукати";
            this.btnFindArticul.UseVisualStyleBackColor = false;
            this.btnFindArticul.Click += new System.EventHandler(this.btnFindArticul_Click);
            // 
            // btnAddArtFromBuffer
            // 
            this.btnAddArtFromBuffer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddArtFromBuffer.Location = new System.Drawing.Point(1102, 182);
            this.btnAddArtFromBuffer.Name = "btnAddArtFromBuffer";
            this.btnAddArtFromBuffer.Size = new System.Drawing.Size(128, 26);
            this.btnAddArtFromBuffer.TabIndex = 4;
            this.btnAddArtFromBuffer.Text = "Додати з буферу";
            this.btnAddArtFromBuffer.UseVisualStyleBackColor = true;
            this.btnAddArtFromBuffer.Click += new System.EventHandler(this.btnAddArtFromBuffer_Click);
            // 
            // grpResults
            // 
            this.grpResults.Controls.Add(this.btnToExcel);
            this.grpResults.Controls.Add(this.lblPath);
            this.grpResults.Controls.Add(this.dgwFilterExt);
            this.grpResults.Controls.Add(this.btnCancelCopy);
            this.grpResults.Controls.Add(this.txtCopyPath);
            this.grpResults.Controls.Add(this.btnSetDir);
            this.grpResults.Controls.Add(this.btnClearResults);
            this.grpResults.Controls.Add(this.btnCopyFiles);
            this.grpResults.Controls.Add(this.cCopyDir);
            this.grpResults.Controls.Add(this.cSelectDeselect);
            this.grpResults.Controls.Add(this.dgwResults);
            this.grpResults.Location = new System.Drawing.Point(2, 394);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(1232, 371);
            this.grpResults.TabIndex = 2;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Результати";
            // 
            // btnToExcel
            // 
            this.btnToExcel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnToExcel.Location = new System.Drawing.Point(146, 340);
            this.btnToExcel.Name = "btnToExcel";
            this.btnToExcel.Size = new System.Drawing.Size(106, 26);
            this.btnToExcel.TabIndex = 20;
            this.btnToExcel.Text = "-> Excel";
            this.btnToExcel.UseVisualStyleBackColor = true;
            this.btnToExcel.Click += new System.EventHandler(this.btnToExcel_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(7, 318);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(45, 16);
            this.lblPath.TabIndex = 19;
            this.lblPath.Text = "Шлях:";
            // 
            // dgwFilterExt
            // 
            this.dgwFilterExt.AllowDrop = true;
            this.dgwFilterExt.AllowUserToAddRows = false;
            this.dgwFilterExt.AllowUserToDeleteRows = false;
            this.dgwFilterExt.AllowUserToOrderColumns = true;
            this.dgwFilterExt.AllowUserToResizeColumns = false;
            this.dgwFilterExt.AllowUserToResizeRows = false;
            this.dgwFilterExt.ColumnHeadersVisible = false;
            this.dgwFilterExt.Location = new System.Drawing.Point(1098, 22);
            this.dgwFilterExt.Name = "dgwFilterExt";
            this.dgwFilterExt.RowHeadersVisible = false;
            this.dgwFilterExt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwFilterExt.Size = new System.Drawing.Size(128, 143);
            this.dgwFilterExt.TabIndex = 18;
            this.dgwFilterExt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFilterExt_CellContentClick);
            // 
            // btnCancelCopy
            // 
            this.btnCancelCopy.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCancelCopy.Location = new System.Drawing.Point(1099, 312);
            this.btnCancelCopy.Name = "btnCancelCopy";
            this.btnCancelCopy.Size = new System.Drawing.Size(128, 26);
            this.btnCancelCopy.TabIndex = 12;
            this.btnCancelCopy.Text = "Припинити";
            this.btnCancelCopy.UseVisualStyleBackColor = false;
            this.btnCancelCopy.Visible = false;
            this.btnCancelCopy.Click += new System.EventHandler(this.btnCancelCopy_Click);
            // 
            // txtCopyPath
            // 
            this.txtCopyPath.Location = new System.Drawing.Point(382, 342);
            this.txtCopyPath.Name = "txtCopyPath";
            this.txtCopyPath.Size = new System.Drawing.Size(647, 23);
            this.txtCopyPath.TabIndex = 17;
            // 
            // btnSetDir
            // 
            this.btnSetDir.Location = new System.Drawing.Point(1035, 340);
            this.btnSetDir.Name = "btnSetDir";
            this.btnSetDir.Size = new System.Drawing.Size(53, 26);
            this.btnSetDir.TabIndex = 16;
            this.btnSetDir.Text = "...";
            this.btnSetDir.UseVisualStyleBackColor = true;
            this.btnSetDir.Click += new System.EventHandler(this.btnSetDir_Click);
            // 
            // btnClearResults
            // 
            this.btnClearResults.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClearResults.Location = new System.Drawing.Point(6, 340);
            this.btnClearResults.Name = "btnClearResults";
            this.btnClearResults.Size = new System.Drawing.Size(133, 26);
            this.btnClearResults.TabIndex = 15;
            this.btnClearResults.Text = "Очистити результати";
            this.btnClearResults.UseVisualStyleBackColor = true;
            this.btnClearResults.Click += new System.EventHandler(this.btnClearResults_Click);
            // 
            // btnCopyFiles
            // 
            this.btnCopyFiles.Location = new System.Drawing.Point(1099, 339);
            this.btnCopyFiles.Name = "btnCopyFiles";
            this.btnCopyFiles.Size = new System.Drawing.Size(128, 26);
            this.btnCopyFiles.TabIndex = 14;
            this.btnCopyFiles.Text = "Копіювати";
            this.btnCopyFiles.UseVisualStyleBackColor = true;
            this.btnCopyFiles.Click += new System.EventHandler(this.btnCopyFiles_Click);
            // 
            // cCopyDir
            // 
            this.cCopyDir.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cCopyDir.Location = new System.Drawing.Point(1100, 268);
            this.cCopyDir.Name = "cCopyDir";
            this.cCopyDir.Size = new System.Drawing.Size(128, 38);
            this.cCopyDir.TabIndex = 11;
            this.cCopyDir.Text = "Копіювати з піддерикоріями";
            this.cCopyDir.UseVisualStyleBackColor = false;
            // 
            // cSelectDeselect
            // 
            this.cSelectDeselect.AutoSize = true;
            this.cSelectDeselect.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cSelectDeselect.Location = new System.Drawing.Point(1100, 243);
            this.cSelectDeselect.Name = "cSelectDeselect";
            this.cSelectDeselect.Size = new System.Drawing.Size(92, 19);
            this.cSelectDeselect.TabIndex = 10;
            this.cSelectDeselect.Text = "Вибрати всі";
            this.cSelectDeselect.UseVisualStyleBackColor = true;
            this.cSelectDeselect.CheckedChanged += new System.EventHandler(this.cSelectDeselect_CheckedChanged);
            // 
            // dgwResults
            // 
            this.dgwResults.AllowUserToAddRows = false;
            this.dgwResults.AllowUserToDeleteRows = false;
            this.dgwResults.AllowUserToResizeColumns = false;
            this.dgwResults.Location = new System.Drawing.Point(7, 22);
            this.dgwResults.Name = "dgwResults";
            this.dgwResults.RowHeadersWidth = 10;
            this.dgwResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwResults.Size = new System.Drawing.Size(1080, 290);
            this.dgwResults.TabIndex = 8;
            this.dgwResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwResults_CellContentClick);
            this.dgwResults.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgwResults_CellPainting);
            this.dgwResults.SelectionChanged += new System.EventHandler(this.dgwResults_SelectionChanged);
            // 
            // StripStatus
            // 
            this.StripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatusLabel,
            this.tssStatus});
            this.StripStatus.Location = new System.Drawing.Point(0, 768);
            this.StripStatus.Name = "StripStatus";
            this.StripStatus.Size = new System.Drawing.Size(1236, 22);
            this.StripStatus.TabIndex = 3;
            this.StripStatus.Text = "statusStrip1";
            // 
            // tssStatusLabel
            // 
            this.tssStatusLabel.Name = "tssStatusLabel";
            this.tssStatusLabel.Size = new System.Drawing.Size(115, 17);
            this.tssStatusLabel.Text = "Стан завантаження:";
            // 
            // tssStatus
            // 
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // BwDownloadData
            // 
            this.BwDownloadData.WorkerReportsProgress = true;
            this.BwDownloadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwDownloadData_DoWork);
            this.BwDownloadData.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BwDownloadData_ProgressChanged);
            this.BwDownloadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwDownloadData_RunWorkerCompleted);
            // 
            // BwFindArticul
            // 
            this.BwFindArticul.WorkerReportsProgress = true;
            this.BwFindArticul.WorkerSupportsCancellation = true;
            this.BwFindArticul.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwFindArticul_DoWork);
            this.BwFindArticul.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BwFindArticul_ProgressChanged);
            this.BwFindArticul.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwFindArticul_RunWorkerCompleted);
            // 
            // bwCopy
            // 
            this.bwCopy.WorkerReportsProgress = true;
            this.bwCopy.WorkerSupportsCancellation = true;
            this.bwCopy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCopy_DoWork);
            this.bwCopy.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwCopy_ProgressChanged);
            this.bwCopy.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCopy_RunWorkerCompleted);
            // 
            // frmFindFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 790);
            this.Controls.Add(this.StripStatus);
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFind);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmFindFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пошук зображень товару";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwArts)).EndInit();
            this.tabPageNotFound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwIsNotFounds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwSrc)).EndInit();
            this.grpResults.ResumeLayout(false);
            this.grpResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFilterExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgwResults)).EndInit();
            this.StripStatus.ResumeLayout(false);
            this.StripStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnAddFolderFromBuffer;
        private System.Windows.Forms.Button bntAddArt;
        private System.Windows.Forms.Button btnAddArtFromBuffer;
        private System.Windows.Forms.DataGridView dgwArts;
        private FotoS.NumericTextBox mtxtArt;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.DataGridView dgwResults;
        private System.Windows.Forms.Button btnFindArticul;
        private System.Windows.Forms.CheckBox cCopyDir;
        private System.Windows.Forms.CheckBox cSelectDeselect;
        private System.Windows.Forms.DataGridView dgwSrc;
        private System.Windows.Forms.Button btnCopyFiles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPageNotFound;
        private System.Windows.Forms.Button btnClearResults;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.StatusStrip StripStatus;
        private System.Windows.Forms.ToolStripStatusLabel tssStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus;
        private System.ComponentModel.BackgroundWorker BwDownloadData;
        private System.Windows.Forms.CheckBox chFindAll;
        private System.Windows.Forms.DataGridView dgwIsNotFounds;
        private System.Windows.Forms.Button btnSetBuffer;
        private System.ComponentModel.BackgroundWorker BwFindArticul;
        private System.Windows.Forms.Button btnSetDir;
        private System.Windows.Forms.TextBox txtCopyPath;
        private System.ComponentModel.BackgroundWorker bwCopy;
        private System.Windows.Forms.Button btnCancelCopy;
        private System.Windows.Forms.DataGridView dgwFilterExt;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnToExcel;
    }
}