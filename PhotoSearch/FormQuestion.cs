using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoSearch
{
    public class FormQuestion: Form
    {
        public FormQuestion() { }

        public FormQuestion(string caption, List<Oa> o, TyepQuest q, ref Ha c)
        {
            this.Text = caption;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Tahoma", 9);

            switch (q)
            {
                case TyepQuest.IgnoreAbort:
                {
                        this.Size = new System.Drawing.Size(500, 200);
                        for (int i = (o.Count - 1); i > -1; i--)
                        {
                            Label l1 = new Label();
                            l1.Dock = DockStyle.Top;
                            l1.Height = 20;
                            l1.Text = o[i].text;//.Where(w => w.id == 1).FirstOrDefault().text;
                            l1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                            this.Controls.Add(l1);
                        }

                        //Label l2 = new Label();
                        //l2.Dock = DockStyle.Top;
                        //l2.Height = 20;
                        //l2.Text = o.Where(w => w.id == 2).FirstOrDefault().text;
                        //l2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.Controls.Add(l2);

                        //Label l3 = new Label();
                        //l3.Dock = DockStyle.Top;
                        //l3.Height = 20;
                        //l3.Text = o.Where(w => w.id == 3).FirstOrDefault().text;
                        //l3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.Controls.Add(l3);

                        Panel p = new Panel();
                        p.Dock = DockStyle.Bottom;
                        p.Height = 30;

                        Button bCancel = new Button();
                        bCancel.Text = string.Format("{0}", "Відмінити");
                        bCancel.DialogResult = DialogResult.Abort;
                        bCancel.Dock = DockStyle.Right;
                        bCancel.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);
                        bCancel.Width = 120;

                        Button bIgnore = new Button();
                        bIgnore.Text = string.Format("{0}", "Пропустити");
                        bIgnore.DialogResult = DialogResult.Ignore;
                        bIgnore.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);
                        bIgnore.Dock = DockStyle.Right;
                        bIgnore.Width = 120;

                        Button bRename = new Button();
                        bRename.Text = string.Format("{0}", "Перейменувати");
                        bRename.DialogResult = DialogResult.OK;
                        bRename.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);
                        bRename.Dock = DockStyle.Right;
                        bRename.Width = 120;

                        CheckBox ch = new CheckBox();
                        ch.Text = "Для всіх ";
                        ch.DataBindings.Add("Checked", c, "MM");

                        p.Controls.Add(ch);
                        p.Controls.Add(bIgnore);
                        p.Controls.Add(bCancel);
                        p.Controls.Add(bRename);

                        this.Controls.Add(p);
                    break;
                }                    
            }
        }
    }

    public enum TyepQuest
    {
        IgnoreAbort = 1
    }
}
