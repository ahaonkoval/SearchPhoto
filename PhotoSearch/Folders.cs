// Decompiled with JetBrains decompiler
// Type: FotoS.Folders
// Assembly: FotoS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 470692A6-11C2-4A54-ACAA-7A0C32039435
// Assembly location: D:\develops\Resharp\FotoS.exe

using FotoS.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FotoS
{
    public class Folders : Form
    {
        private IContainer components = (IContainer)null;
        private Button button1;
        private DataGridView dgv_list_articuls;
        private Button bt_select_folder;

        public Folders()
        {
            this.InitializeComponent();
            this.InitArticulsTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "source.txt";
            if (!File.Exists(path))
                return;
            try
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    while (streamReader.Peek() >= 0)
                        ;
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Ошибка открытия файла " + ex.Message);
            }
        }

        private void InitArticulsTable()
        {
            this.dgv_list_articuls.ColumnCount = 1;
            this.dgv_list_articuls.Columns[0].Name = "folder";
            this.dgv_list_articuls.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.HeaderText = "видалити";
            gridViewImageColumn.Name = "img";
            gridViewImageColumn.Image = (Image)Resources.delete_16;
            this.dgv_list_articuls.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            this.dgv_list_articuls.Columns[gridViewImageColumn.Name].Width = 60;
            this.dgv_list_articuls.CellClick += new DataGridViewCellEventHandler(this.dgv_list_articuls_CellClick);
        }

        private void dgv_list_articuls_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != this.dgv_list_articuls.Columns[1].Index)
                return;
            this.dgv_list_articuls.Rows.RemoveAt(e.RowIndex);
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.dgv_list_articuls.Rows.Clear();
            this.dgv_list_articuls.Refresh();
        }

        private void bt_select_folder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;
            this.dgv_list_articuls.Rows.Add((object[])new string[1]
            {
        folderBrowserDialog.SelectedPath
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
            this.button1 = new Button();
            this.dgv_list_articuls = new DataGridView();
            this.bt_select_folder = new Button();
            ((ISupportInitialize)this.dgv_list_articuls).BeginInit();
            this.SuspendLayout();
            this.button1.Location = new Point(34, 12);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.dgv_list_articuls.AllowUserToAddRows = false;
            this.dgv_list_articuls.AllowUserToResizeRows = false;
            this.dgv_list_articuls.BackgroundColor = Color.White;
            gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle1.BackColor = SystemColors.Control;
            gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            gridViewCellStyle1.ForeColor = SystemColors.WindowText;
            gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dgv_list_articuls.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
            this.dgv_list_articuls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle2.BackColor = SystemColors.Window;
            gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            gridViewCellStyle2.ForeColor = SystemColors.ControlText;
            gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            this.dgv_list_articuls.DefaultCellStyle = gridViewCellStyle2;
            this.dgv_list_articuls.Location = new Point(34, 79);
            this.dgv_list_articuls.MultiSelect = false;
            this.dgv_list_articuls.Name = "dgv_list_articuls";
            this.dgv_list_articuls.ReadOnly = true;
            gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle3.BackColor = SystemColors.Control;
            gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            gridViewCellStyle3.ForeColor = SystemColors.WindowText;
            gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            this.dgv_list_articuls.RowHeadersDefaultCellStyle = gridViewCellStyle3;
            this.dgv_list_articuls.RowHeadersVisible = false;
            this.dgv_list_articuls.Size = new Size(419, 185);
            this.dgv_list_articuls.TabIndex = 5;
            this.bt_select_folder.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.bt_select_folder.Location = new Point(34, 41);
            this.bt_select_folder.Name = "bt_select_folder";
            this.bt_select_folder.Size = new Size(46, 23);
            this.bt_select_folder.TabIndex = 22;
            this.bt_select_folder.Text = "select";
            this.bt_select_folder.UseVisualStyleBackColor = true;
            this.bt_select_folder.Click += new EventHandler(this.bt_select_folder_Click);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(465, 415);
            this.Controls.Add((Control)this.bt_select_folder);
            this.Controls.Add((Control)this.dgv_list_articuls);
            this.Controls.Add((Control)this.button1);
            this.Name = nameof(Folders);
            this.Text = nameof(Folders);
            ((ISupportInitialize)this.dgv_list_articuls).EndInit();
            this.ResumeLayout(false);
        }
    }
}
