using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Photoshop;
using PhotoSearch.Models;

namespace PhotoSearch
{
    public partial class FrmShowImage : Form
    {
        public FrmShowImage()
        {
            InitializeComponent();
        }

        public FrmShowImage(Fresult f)
        {
            InitializeComponent();

            LoadImg(f);
        }

        void LoadImg(Fresult f)
        {
            switch (f.Extension)
            {
                case ".psd":
                    {
                        using (var psdFile = new PsdFile(f.FilePath))
                        {
                            Bitmap image = psdFile.CompositImage.Clone() as Bitmap;
                            this.Pps.Image = image;
                        }
                        break;
                    }
                case ".jpg":
                    {
                        Image image = Image.FromFile(f.FilePath);
                        this.Pps.Image = image;
                        break;
                    }
                case ".jpeg":
                    {
                        Image image = Image.FromFile(f.FilePath);
                        this.Pps.Image = image;
                        break;
                    }
                case ".png":
                    {
                        Image image = Image.FromFile(f.FilePath);
                        this.Pps.Image = image;
                        break;
                    }
                case ".bmp":
                    {
                        Image image = Image.FromFile(f.FilePath);
                        this.Pps.Image = image;
                        break;
                    }
                default:
                    {
                        try
                        {
                            Image image = Image.FromFile(f.FilePath);
                            this.Pps.Image = image;
                        }
                        catch { }
                        break;
                    }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
