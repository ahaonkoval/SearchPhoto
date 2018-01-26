// Decompiled with JetBrains decompiler
// Type: FotoS.Form1
// Assembly: FotoS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 470692A6-11C2-4A54-ACAA-7A0C32039435
// Assembly location: D:\develops\Resharp\FotoS.exe

using FotoS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FotoS
{
    public class Form1 : Form
    {
        private operation_text op_text = new operation_text();
        private int magic_source_length = 4;
        private DataTable NemaArticuls = (DataTable) null;
        private IContainer components = (IContainer) null;
        private DataGridView dgv_source;
        private DataGridView dgv_res1;
        private Button bt_go;
        private Button bt_source_buffer;
        private DataGridView dgv_articuls;
        private Button bt_articuls_buffer;
        private Button bt_go_articuls;
        private TextBox tb_copy_way;
        private Button bt_copy_files;
        private Button bt_clear;
        private DataGridViewTextBoxColumn file;
        private DataGridViewTextBoxColumn size;
        private DataGridViewTextBoxColumn way;
        private DataGridViewCheckBoxColumn check;
        private Button btn_one_articul_add;
        private NumericTextBox tb_one_articul;
        private Button bt_get_one_folder;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lb_result;
        private Button bt_clear_source_dgv;
        private Button bt_clear_articuls_dgv;
        private CheckBox chb_manual;
        private Label lb_status;
        private CheckBox chb_with_subfolders;
        private Button bt_show_nemaarticuls;
        private Button bt_select_folder;
        private CheckBox chb_select_all;
        private Button button1;

        public Form1()
        {
            this.InitializeComponent();
            this.InitSourceTable();
            this.InitArticulsTable();
            this.lb_result.Text = "0";
            this.lb_status.Text = "";
            this.NemaArticuls = new DataTable();
            this.NemaArticuls.Columns.Add(new DataColumn("articul", typeof(string)));
        }

        private void bt_go_Click(object sender, EventArgs e)
        {
            this.dgv_res1.Rows.Clear();
            for (int index = 0; index < this.dgv_source.RowCount; ++index)
            {
                if (this.dgv_source[0, index].Value.ToString().Length > this.magic_source_length)
                    this.ToDo(this.dgv_source[0, index].Value.ToString());
            }
            this.lb_result.Text = this.dgv_res1.Rows.Count.ToString() + " items";
            this.ClearInfo();
        }

        private void ToDo(string source)
        {
            try
            {
                foreach (string file in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        string[] strArray = new string[3]
                        {
                            fileInfo.Name,
                            fileInfo.Length.ToString(),
                            file
                        };
                        if (Regex.IsMatch(fileInfo.Name, "[0-9]{8}", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                        {
                            if (fileInfo.Name.Length < 259 && fileInfo.DirectoryName.Length < 247)
                            {
                                this.dgv_res1.Rows.Add((object[]) strArray);
                                this.dgv_res1.Rows[this.dgv_res1.Rows.Count - 1]
                                    .Cells[this.dgv_res1.Columns["check"].Index].Value = (object) true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        int num = (int) MessageBox.Show("E1. \r\n" + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                int num = (int) MessageBox.Show("E2. \r\n" + ex.ToString());
            }
        }

        private void ToDo(string source, string articul)
        {
            try
            {
                Func<string, bool> predicate = (Func<string, bool>) (i =>
                    Regex.IsMatch(i, articul, RegexOptions.IgnoreCase | RegexOptions.Compiled));

                IEnumerable<string> source1 =
                    ((IEnumerable<string>) Directory.GetFiles(source, "*", SearchOption.AllDirectories)).Where<string>(
                        predicate);
                foreach (string fileName in source1)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(fileName);
                        this.dgv_res1.Rows.Add((object[]) new string[3]
                        {
                            fileInfo.Name,
                            fileInfo.Length.ToString(),
                            fileName
                        });
                        this.dgv_res1.Rows[this.dgv_res1.Rows.Count - 1].Cells[this.dgv_res1.Columns["check"].Index]
                            .Value = (object) true;
                    }
                    catch (Exception ex)
                    {
                        int num = (int) MessageBox.Show("E3. \r\n" + ex.ToString());
                    }
                }
                if (source1.Count<string>() != 0)
                    return;
                this.NemaArticuls.Rows.Add(new object[1]
                {
                    (object) articul
                });
            }
            catch (Exception ex)
            {
                int num = (int) MessageBox.Show("E4. \r\n" + ex.ToString());
            }
        }

        private void dgv_source_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == this.dgv_source.Rows.Count ||
                e.ColumnIndex != this.dgv_source.Columns["delete"].Index)
                return;
            this.dgv_source.Rows.RemoveAt(e.RowIndex);
        }

        private void bt_source_buffer_Click(object sender, EventArgs e)
        {
            this.get_buffer(this.dgv_source);
            this.ClearInfo();
        }

        private void get_buffer(DataGridView dgv)
        {
            string input = Clipboard.GetText().Replace("\r\n", ";");
            if (input.Length < this.magic_source_length)
            {
                int num = (int) MessageBox.Show("в буфері пусто", "увага", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            else
            {
                foreach (string str in Regex.Split(input, ";"))
                {
                    string[] strArray = new string[1] {str};
                    dgv.Rows.Add((object[]) strArray);
                }
            }
            Clipboard.Clear();
        }

        private void InitSourceTable()
        {
            this.dgv_source.ColumnCount = 1;
            this.dgv_source.Columns[0].Name = "source";
            this.dgv_source.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "delete";
            //gridViewImageColumn.Image = (Image) Resources.delete_16;
            this.dgv_source.Columns.Add((DataGridViewColumn) gridViewImageColumn);
            this.dgv_source.Columns[gridViewImageColumn.Name].Width = 60;
        }

        private void InitArticulsTable()
        {
            this.dgv_articuls.ColumnCount = 1;
            this.dgv_articuls.Columns[0].Name = "articul";
            this.dgv_articuls.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "delete";
            //gridViewImageColumn.Image = (Image) Resources.delete_16;
            this.dgv_articuls.Columns.Add((DataGridViewColumn) gridViewImageColumn);
            this.dgv_articuls.Columns[gridViewImageColumn.Name].Width = 60;
        }

        private void bt_go_articuls_Click(object sender, EventArgs e)
        {
            this.dgv_res1.Rows.Clear();
            this.NemaArticuls.Rows.Clear();
            for (int index1 = 0; index1 < this.dgv_source.RowCount; ++index1)
            {
                for (int index2 = 0; index2 < this.dgv_articuls.RowCount; ++index2)
                {
                    if (this.dgv_source[0, index1].Value.ToString().Length > this.magic_source_length)
                        this.ToDo(this.dgv_source[0, index1].Value.ToString(),
                            this.dgv_articuls[0, index2].Value.ToString());
                }
            }
            this.lb_result.Text = this.dgv_res1.Rows.Count.ToString() + " items";
            this.ClearInfo();
        }

        private void bt_articuls_buffer_Click(object sender, EventArgs e)
        {
            string input = this.op_text.Remove_WhiteSpaces_From_String(Clipboard.GetText().Replace("\r\n", ";"));
            if (input.Length < 8)
            {
                int num = (int) MessageBox.Show("в буфері пусто", "увага", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            else
            {
                foreach (string str in Regex.Split(input, ";"))
                {
                    if (str.Length == 8)
                        this.dgv_articuls.Rows.Add((object[]) new string[1]
                        {
                            str
                        });
                }
            }
            Clipboard.Clear();
            this.ClearInfo();
        }

        private void bt_copy_files_Click(object sender, EventArgs e)
        {
            if (this.tb_copy_way.Text.Length < this.magic_source_length)
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    this.tb_copy_way.Text = folderBrowserDialog.SelectedPath;
            }
            this.Docopy();
        }

        private void Docopy()
        {
            try
            {
                if (this.tb_copy_way.Text.Length <= this.magic_source_length)
                    return;
                DirectoryInfo directoryInfo1 = new DirectoryInfo(this.tb_copy_way.Text);
                if (!directoryInfo1.Exists)
                    directoryInfo1.Create();
                if (this.chb_with_subfolders.Checked)
                {
                    for (int index = 1; index < 10; ++index)
                    {
                        DirectoryInfo directoryInfo2 =
                            new DirectoryInfo(this.tb_copy_way.Text + "\\" + index.ToString() + "0");
                        if (!directoryInfo2.Exists)
                            directoryInfo2.Create();
                    }
                    DirectoryInfo directoryInfo3 = new DirectoryInfo(this.tb_copy_way.Text + "\\other");
                    if (!directoryInfo3.Exists)
                        directoryInfo3.Create();
                }
                for (int index = 0; index < this.dgv_res1.Rows.Count; ++index)
                {
                    if ((bool) this.dgv_res1.Rows[index].Cells["check"].Value)
                    {
                        string str1 = Regex.Match(this.dgv_res1["file", index].Value.ToString(), "[0-9]{8,}")
                            .ToString();
                        string str2 = "";
                        if (this.chb_with_subfolders.Checked)
                        {
                            string str3 = str1.Substring(0, 1);
                            str2 = !char.IsDigit(Convert.ToChar(str3)) ? "other\\" : str3 + "0\\";
                        }
                        FileInfo fileInfo1 = new FileInfo(this.tb_copy_way.Text + "\\" + str2 +
                                                          this.dgv_res1["file", index].Value.ToString());
                        string withoutExtension = Path.GetFileNameWithoutExtension(fileInfo1.Name);
                        if (File.Exists(this.tb_copy_way.Text + "\\" + str2 +
                                        this.dgv_res1["file", index].Value.ToString()))
                        {
                            FileInfo fileInfo2 = new FileInfo(this.dgv_res1["way", index].Value.ToString());
                            Func<string, bool> predicate = (Func<string, bool>) (j =>
                                Regex.IsMatch(j, "[0-9]{8,}", RegexOptions.IgnoreCase | RegexOptions.Compiled));
                            IEnumerable<string> strings =
                            ((IEnumerable<string>) Directory.GetFiles(this.tb_copy_way.Text + "\\" + str2, "*",
                                SearchOption.TopDirectoryOnly)).Where<string>(predicate);
                            int num1 = 0;
                            int num2 = 0;
                            foreach (string fileName in strings)
                            {
                                FileInfo fileInfo3 = new FileInfo(fileName);
                                try
                                {
                                    if (fileInfo3.Name == fileInfo2.Name)
                                    {
                                        ++num2;
                                        if (fileInfo3.Name.Substring(0, fileInfo2.Name.Length) == fileInfo2.Name &&
                                            fileInfo3.Length == fileInfo2.Length)
                                            ++num1;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    int num3 = (int) MessageBox.Show(
                                        "E6. \r\n" + fileInfo3.Name + " \r\n" + fileInfo2.Name + " \r\n" +
                                        ex.ToString());
                                }
                            }
                            if (num1 == 0)
                            {
                                string str3 = withoutExtension + "_" + (num2 + 1).ToString() +
                                              Path.GetExtension(fileInfo1.Name);
                                File.Copy(this.dgv_res1["way", index].Value.ToString(),
                                    this.tb_copy_way.Text + "\\" + str2 + str3, true);
                            }
                        }
                        else
                            File.Copy(this.dgv_res1["way", index].Value.ToString(),
                                this.tb_copy_way.Text + "\\" + str2 + this.dgv_res1["file", index].Value.ToString(),
                                true);
                    }
                }
                this.lb_status.Text = "end";
            }
            catch (Exception ex)
            {
                int num = (int) MessageBox.Show("E7. \r\n" + ex.ToString());
            }
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            this.dgv_res1.Rows.Clear();
            this.lb_result.Text = this.dgv_res1.Rows.Count.ToString() + " items";
            this.ClearInfo();
        }

        private void dgv_articuls_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == this.dgv_articuls.Rows.Count ||
                e.ColumnIndex != this.dgv_articuls.Columns["delete"].Index)
                return;
            this.dgv_articuls.Rows.RemoveAt(e.RowIndex);
        }

        private void btn_one_articul_add_Click(object sender, EventArgs e)
        {
            if (this.tb_one_articul.Text.Length != 8)
                return;
            this.dgv_articuls.Rows.Add(new object[1]
            {
                (object) this.tb_one_articul.Text
            });
            this.tb_one_articul.Text = "";
        }

        private void tb_one_articul_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13 || this.tb_one_articul.Text.Length != 8)
                return;
            this.dgv_articuls.Rows.Add(new object[1]
            {
                (object) this.tb_one_articul.Text
            });
            this.tb_one_articul.Text = "";
        }

        private void bt_get_one_folder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;
            this.dgv_source.Rows.Add(new object[1]
            {
                (object) folderBrowserDialog.SelectedPath
            });
        }

        private void bt_clear_source_dgv_Click(object sender, EventArgs e)
        {
            this.dgv_source.Rows.Clear();
            this.ClearInfo();
        }

        private void bt_clear_articuls_dgv_Click(object sender, EventArgs e)
        {
            this.dgv_articuls.Rows.Clear();
            this.ClearInfo();
        }

        private void chb_manual_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearInfo();
            this.tb_copy_way.Text = "";
            if (this.chb_manual.Checked)
            {
                this.tb_copy_way.Enabled = true;
                this.tb_copy_way.ReadOnly = false;
            }
            else
            {
                this.tb_copy_way.Enabled = false;
                this.tb_copy_way.ReadOnly = true;
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    this.tb_copy_way.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void ClearInfo()
        {
            this.lb_status.Text = "";
            this.tb_copy_way.Text = "";
        }

        private void bt_show_nemaarticuls_Click(object sender, EventArgs e)
        {
            this.dgv_articuls.Rows.Clear();
            if (this.NemaArticuls.Rows.Count <= 0)
                return;
            for (int index = 0; index < this.NemaArticuls.Rows.Count; ++index)
                this.dgv_articuls.Rows.Add(new object[1]
                {
                    (object) this.NemaArticuls.Rows[index][0].ToString()
                });
        }

        private void bt_select_folder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;
            this.tb_copy_way.Text = folderBrowserDialog.SelectedPath;
        }

        private void chb_select_all_CheckedChanged(object sender, EventArgs e)
        {
            int rowCount = this.dgv_res1.RowCount;
            if (this.chb_select_all.Checked)
            {
                if (rowCount <= 0)
                    return;
                for (int index = 0; index < rowCount; ++index)
                    this.dgv_res1.Rows[index].Cells[this.dgv_res1.Columns["check"].Index].Value = (object) true;
            }
            else if (rowCount > 0)
            {
                for (int index = 0; index < rowCount; ++index)
                    this.dgv_res1.Rows[index].Cells[this.dgv_res1.Columns["check"].Index].Value = (object) false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Folders().Show();
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
            DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle9 = new DataGridViewCellStyle();
            this.dgv_source = new DataGridView();
            this.dgv_res1 = new DataGridView();
            this.file = new DataGridViewTextBoxColumn();
            this.size = new DataGridViewTextBoxColumn();
            this.way = new DataGridViewTextBoxColumn();
            this.check = new DataGridViewCheckBoxColumn();
            this.bt_go = new Button();
            this.bt_source_buffer = new Button();
            this.dgv_articuls = new DataGridView();
            this.bt_articuls_buffer = new Button();
            this.bt_go_articuls = new Button();
            this.tb_copy_way = new TextBox();
            this.bt_copy_files = new Button();
            this.bt_clear = new Button();
            this.btn_one_articul_add = new Button();
            this.bt_get_one_folder = new Button();
            this.groupBox1 = new GroupBox();
            this.bt_clear_source_dgv = new Button();
            this.groupBox2 = new GroupBox();
            this.bt_show_nemaarticuls = new Button();
            this.bt_clear_articuls_dgv = new Button();
            this.lb_result = new Label();
            this.chb_manual = new CheckBox();
            this.lb_status = new Label();
            this.chb_with_subfolders = new CheckBox();
            this.bt_select_folder = new Button();
            this.chb_select_all = new CheckBox();
            this.button1 = new Button();
            this.tb_one_articul = new NumericTextBox();
            ((ISupportInitialize) this.dgv_source).BeginInit();
            ((ISupportInitialize) this.dgv_res1).BeginInit();
            ((ISupportInitialize) this.dgv_articuls).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            this.dgv_source.AllowUserToAddRows = false;
            this.dgv_source.AllowUserToResizeRows = false;
            this.dgv_source.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle1.BackColor = SystemColors.Control;
            gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle1.ForeColor = SystemColors.WindowText;
            gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dgv_source.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
            this.dgv_source.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle2.BackColor = SystemColors.Window;
            gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle2.ForeColor = SystemColors.ControlText;
            gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            this.dgv_source.DefaultCellStyle = gridViewCellStyle2;
            this.dgv_source.Location = new Point(7, 77);
            this.dgv_source.Name = "dgv_source";
            gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle3.BackColor = SystemColors.Control;
            gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle3.ForeColor = SystemColors.WindowText;
            gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            this.dgv_source.RowHeadersDefaultCellStyle = gridViewCellStyle3;
            this.dgv_source.RowHeadersVisible = false;
            this.dgv_source.Size = new Size(330, 267);
            this.dgv_source.TabIndex = 0;
            this.dgv_source.CellClick += new DataGridViewCellEventHandler(this.dgv_source_CellClick);
            this.dgv_res1.AllowUserToAddRows = false;
            this.dgv_res1.AllowUserToDeleteRows = false;
            this.dgv_res1.AllowUserToResizeRows = false;
            this.dgv_res1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle4.BackColor = SystemColors.Control;
            gridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle4.ForeColor = SystemColors.WindowText;
            gridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            this.dgv_res1.ColumnHeadersDefaultCellStyle = gridViewCellStyle4;
            this.dgv_res1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_res1.Columns.AddRange((DataGridViewColumn) this.file, (DataGridViewColumn) this.size,
                (DataGridViewColumn) this.way, (DataGridViewColumn) this.check);
            gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle5.BackColor = SystemColors.Window;
            gridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle5.ForeColor = SystemColors.ControlText;
            gridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            this.dgv_res1.DefaultCellStyle = gridViewCellStyle5;
            this.dgv_res1.Location = new Point(430, 7);
            this.dgv_res1.Name = "dgv_res1";
            gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle6.BackColor = SystemColors.Control;
            gridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle6.ForeColor = SystemColors.WindowText;
            gridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            this.dgv_res1.RowHeadersDefaultCellStyle = gridViewCellStyle6;
            this.dgv_res1.RowHeadersVisible = false;
            this.dgv_res1.Size = new Size(542, 490);
            this.dgv_res1.TabIndex = 1;
            this.file.HeaderText = "file";
            this.file.Name = "file";
            this.file.ReadOnly = true;
            this.size.HeaderText = "size";
            this.size.Name = "size";
            this.size.ReadOnly = true;
            this.way.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.way.HeaderText = "way";
            this.way.Name = "way";
            this.check.FillWeight = 30f;
            this.check.HeaderText = " ";
            this.check.Name = "check";
            this.check.Resizable = DataGridViewTriState.False;
            this.check.Width = 30;
            this.bt_go.Location = new Point(344, 147);
            this.bt_go.Name = "bt_go";
            this.bt_go.Size = new Size(75, 23);
            this.bt_go.TabIndex = 2;
            this.bt_go.Text = "go >>";
            this.bt_go.UseVisualStyleBackColor = true;
            this.bt_go.Click += new EventHandler(this.bt_go_Click);
            this.bt_source_buffer.Location = new Point(7, 19);
            this.bt_source_buffer.Name = "bt_source_buffer";
            this.bt_source_buffer.Size = new Size(330, 23);
            this.bt_source_buffer.TabIndex = 4;
            this.bt_source_buffer.Text = "get buffer source";
            this.bt_source_buffer.UseVisualStyleBackColor = true;
            this.bt_source_buffer.Click += new EventHandler(this.bt_source_buffer_Click);
            this.dgv_articuls.AllowUserToAddRows = false;
            this.dgv_articuls.AllowUserToResizeRows = false;
            this.dgv_articuls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle7.BackColor = SystemColors.Control;
            gridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle7.ForeColor = SystemColors.WindowText;
            gridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            this.dgv_articuls.ColumnHeadersDefaultCellStyle = gridViewCellStyle7;
            this.dgv_articuls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle8.BackColor = SystemColors.Window;
            gridViewCellStyle8.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle8.ForeColor = SystemColors.ControlText;
            gridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            this.dgv_articuls.DefaultCellStyle = gridViewCellStyle8;
            this.dgv_articuls.Location = new Point(6, 75);
            this.dgv_articuls.Name = "dgv_articuls";
            gridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle9.BackColor = SystemColors.Control;
            gridViewCellStyle9.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point,
                (byte) 204);
            gridViewCellStyle9.ForeColor = SystemColors.WindowText;
            gridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            this.dgv_articuls.RowHeadersDefaultCellStyle = gridViewCellStyle9;
            this.dgv_articuls.RowHeadersVisible = false;
            this.dgv_articuls.Size = new Size(331, 258);
            this.dgv_articuls.TabIndex = 5;
            this.dgv_articuls.CellClick += new DataGridViewCellEventHandler(this.dgv_articuls_CellClick);
            this.bt_articuls_buffer.Location = new Point(6, 17);
            this.bt_articuls_buffer.Name = "bt_articuls_buffer";
            this.bt_articuls_buffer.Size = new Size(331, 23);
            this.bt_articuls_buffer.TabIndex = 6;
            this.bt_articuls_buffer.Text = "get buffer articuls";
            this.bt_articuls_buffer.UseVisualStyleBackColor = true;
            this.bt_articuls_buffer.Click += new EventHandler(this.bt_articuls_buffer_Click);
            this.bt_go_articuls.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.bt_go_articuls.Location = new Point(338, 199);
            this.bt_go_articuls.Name = "bt_go_articuls";
            this.bt_go_articuls.Size = new Size(75, 23);
            this.bt_go_articuls.TabIndex = 7;
            this.bt_go_articuls.Text = "go >>";
            this.bt_go_articuls.UseVisualStyleBackColor = true;
            this.bt_go_articuls.Click += new EventHandler(this.bt_go_articuls_Click);
            this.tb_copy_way.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.tb_copy_way.Enabled = false;
            this.tb_copy_way.Location = new Point(511, 530);
            this.tb_copy_way.Name = "tb_copy_way";
            this.tb_copy_way.ReadOnly = true;
            this.tb_copy_way.Size = new Size(363, 20);
            this.tb_copy_way.TabIndex = 8;
            this.bt_copy_files.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.bt_copy_files.Location = new Point(932, 528);
            this.bt_copy_files.Name = "bt_copy_files";
            this.bt_copy_files.Size = new Size(40, 23);
            this.bt_copy_files.TabIndex = 9;
            this.bt_copy_files.Text = "copy";
            this.bt_copy_files.UseVisualStyleBackColor = true;
            this.bt_copy_files.Click += new EventHandler(this.bt_copy_files_Click);
            this.bt_clear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.bt_clear.Location = new Point(430, 528);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new Size(75, 23);
            this.bt_clear.TabIndex = 10;
            this.bt_clear.Text = "clear all";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new EventHandler(this.bt_clear_Click);
            this.btn_one_articul_add.Location = new Point(128, 46);
            this.btn_one_articul_add.Name = "btn_one_articul_add";
            this.btn_one_articul_add.Size = new Size(35, 23);
            this.btn_one_articul_add.TabIndex = 12;
            this.btn_one_articul_add.Text = "add";
            this.btn_one_articul_add.UseVisualStyleBackColor = true;
            this.btn_one_articul_add.Click += new EventHandler(this.btn_one_articul_add_Click);
            this.bt_get_one_folder.Location = new Point(7, 48);
            this.bt_get_one_folder.Name = "bt_get_one_folder";
            this.bt_get_one_folder.Size = new Size(330, 23);
            this.bt_get_one_folder.TabIndex = 14;
            this.bt_get_one_folder.Text = "add folder";
            this.bt_get_one_folder.UseVisualStyleBackColor = true;
            this.bt_get_one_folder.Click += new EventHandler(this.bt_get_one_folder_Click);
            this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.groupBox1.Controls.Add((Control) this.button1);
            this.groupBox1.Controls.Add((Control) this.bt_clear_source_dgv);
            this.groupBox1.Controls.Add((Control) this.bt_get_one_folder);
            this.groupBox1.Controls.Add((Control) this.dgv_source);
            this.groupBox1.Controls.Add((Control) this.bt_source_buffer);
            this.groupBox1.Location = new Point(6, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(418, 350);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.bt_clear_source_dgv.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.bt_clear_source_dgv.Location = new Point(338, 321);
            this.bt_clear_source_dgv.Name = "bt_clear_source_dgv";
            this.bt_clear_source_dgv.Size = new Size(75, 23);
            this.bt_clear_source_dgv.TabIndex = 18;
            this.bt_clear_source_dgv.Text = "clear";
            this.bt_clear_source_dgv.UseVisualStyleBackColor = true;
            this.bt_clear_source_dgv.Click += new EventHandler(this.bt_clear_source_dgv_Click);
            this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.groupBox2.Controls.Add((Control) this.bt_show_nemaarticuls);
            this.groupBox2.Controls.Add((Control) this.bt_clear_articuls_dgv);
            this.groupBox2.Controls.Add((Control) this.dgv_articuls);
            this.groupBox2.Controls.Add((Control) this.bt_articuls_buffer);
            this.groupBox2.Controls.Add((Control) this.tb_one_articul);
            this.groupBox2.Controls.Add((Control) this.bt_go_articuls);
            this.groupBox2.Controls.Add((Control) this.btn_one_articul_add);
            this.groupBox2.Location = new Point(6, 266);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(418, 339);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.bt_show_nemaarticuls.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.bt_show_nemaarticuls.Location = new Point(337, 259);
            this.bt_show_nemaarticuls.Name = "bt_show_nemaarticuls";
            this.bt_show_nemaarticuls.Size = new Size(75, 23);
            this.bt_show_nemaarticuls.TabIndex = 20;
            this.bt_show_nemaarticuls.Text = "<< nema";
            this.bt_show_nemaarticuls.UseVisualStyleBackColor = true;
            this.bt_show_nemaarticuls.Click += new EventHandler(this.bt_show_nemaarticuls_Click);
            this.bt_clear_articuls_dgv.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.bt_clear_articuls_dgv.Location = new Point(338, 310);
            this.bt_clear_articuls_dgv.Name = "bt_clear_articuls_dgv";
            this.bt_clear_articuls_dgv.Size = new Size(75, 23);
            this.bt_clear_articuls_dgv.TabIndex = 19;
            this.bt_clear_articuls_dgv.Text = "clear";
            this.bt_clear_articuls_dgv.UseVisualStyleBackColor = true;
            this.bt_clear_articuls_dgv.Click += new EventHandler(this.bt_clear_articuls_dgv_Click);
            this.lb_result.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lb_result.AutoSize = true;
            this.lb_result.Location = new Point(430, 505);
            this.lb_result.Name = "lb_result";
            this.lb_result.Size = new Size(13, 13);
            this.lb_result.TabIndex = 17;
            this.lb_result.Text = "0";
            this.chb_manual.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.chb_manual.AutoSize = true;
            this.chb_manual.Location = new Point(831, 505);
            this.chb_manual.Name = "chb_manual";
            this.chb_manual.Size = new Size(60, 17);
            this.chb_manual.TabIndex = 18;
            this.chb_manual.Text = "manual";
            this.chb_manual.UseVisualStyleBackColor = true;
            this.chb_manual.CheckedChanged += new EventHandler(this.chb_manual_CheckedChanged);
            this.lb_status.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.lb_status.AutoSize = true;
            this.lb_status.Location = new Point(929, 507);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new Size(16, 13);
            this.lb_status.TabIndex = 19;
            this.lb_status.Text = "---";
            this.chb_with_subfolders.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.chb_with_subfolders.AutoSize = true;
            this.chb_with_subfolders.Location = new Point(729, 505);
            this.chb_with_subfolders.Name = "chb_with_subfolders";
            this.chb_with_subfolders.Size = new Size(96, 17);
            this.chb_with_subfolders.TabIndex = 20;
            this.chb_with_subfolders.Text = "with subfolders";
            this.chb_with_subfolders.UseVisualStyleBackColor = true;
            this.bt_select_folder.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.bt_select_folder.Location = new Point(880, 528);
            this.bt_select_folder.Name = "bt_select_folder";
            this.bt_select_folder.Size = new Size(46, 23);
            this.bt_select_folder.TabIndex = 21;
            this.bt_select_folder.Text = "select";
            this.bt_select_folder.UseVisualStyleBackColor = true;
            this.bt_select_folder.Click += new EventHandler(this.bt_select_folder_Click);
            this.chb_select_all.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.chb_select_all.AutoSize = true;
            this.chb_select_all.Checked = true;
            this.chb_select_all.CheckState = CheckState.Checked;
            this.chb_select_all.Location = new Point(610, 505);
            this.chb_select_all.Name = "chb_select_all";
            this.chb_select_all.Size = new Size(112, 17);
            this.chb_select_all.TabIndex = 22;
            this.chb_select_all.Text = "select/unselect all";
            this.chb_select_all.UseVisualStyleBackColor = true;
            this.chb_select_all.CheckedChanged += new EventHandler(this.chb_select_all_CheckedChanged);
            this.button1.Location = new Point(2, 159);
            this.button1.Name = "button1";
            this.button1.Size = new Size(330, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "add folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.tb_one_articul.AllowSpace = false;
            this.tb_one_articul.ColorFocus = false;
            this.tb_one_articul.EventFireMode = false;
            this.tb_one_articul.HideInput = false;
            this.tb_one_articul.IsSelectionCursorInBegin = false;
            this.tb_one_articul.Location = new Point(6, 48);
            this.tb_one_articul.Name = "tb_one_articul";
            this.tb_one_articul.Size = new Size(116, 20);
            this.tb_one_articul.TabIndex = 13;
            this.tb_one_articul.KeyDown += new KeyEventHandler(this.tb_one_articul_KeyDown);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(984, 611);
            this.Controls.Add((Control) this.chb_select_all);
            this.Controls.Add((Control) this.bt_select_folder);
            this.Controls.Add((Control) this.chb_with_subfolders);
            this.Controls.Add((Control) this.lb_status);
            this.Controls.Add((Control) this.chb_manual);
            this.Controls.Add((Control) this.lb_result);
            this.Controls.Add((Control) this.bt_go);
            this.Controls.Add((Control) this.groupBox2);
            this.Controls.Add((Control) this.groupBox1);
            this.Controls.Add((Control) this.bt_clear);
            this.Controls.Add((Control) this.bt_copy_files);
            this.Controls.Add((Control) this.tb_copy_way);
            this.Controls.Add((Control) this.dgv_res1);
            this.Name = nameof(Form1);
            ((ISupportInitialize) this.dgv_source).EndInit();
            ((ISupportInitialize) this.dgv_res1).EndInit();
            ((ISupportInitialize) this.dgv_articuls).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
