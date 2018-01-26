using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoSearch.Models
{
    public class Helper
    {
        public static void SetRowColor(DataGridViewRow w, Color c)
        {
            foreach (DataGridViewCell cell in w.Cells)
            {
                cell.Style.BackColor = c;
            }
        }

        public static IEnumerable<Articul> GetArticulList()
        {
            //Articul art = new Articul {
            //    Id = 1,
            //    Value = "12345678",
            //    Description = "dsfgdsfghdfgdfsghdfgs"
            //};
            var al = new List<Articul>();
            return al;
        }

        public static IEnumerable<Fresult> GetFresultList()
        {
            List<Fresult> t = new List<Fresult>();
            return t;
        }

        public static string ClearString(string s)
        {
            string str = "";
            try
            {
                foreach (char c in s)
                {
                    if (!char.IsWhiteSpace(c))
                        str += c.ToString();
                }
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="onlyResizeIfWider"></param>
        /// <returns></returns>
        public static Image ResizeImage(string file, int width, int height, bool onlyResizeIfWider)
        {
            using (Image image = Image.FromFile(file))
            {
                // Prevent using images internal thumbnail
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);

                if (onlyResizeIfWider == true)
                {
                    if (image.Width <= width)
                    {
                        width = image.Width;
                    }
                }

                int newHeight = image.Height * width / image.Width;
                if (newHeight > height)
                {
                    // Resize with height instead
                    width = image.Width * height / image.Height;
                    newHeight = height;
                }

                Image NewImage = image.GetThumbnailImage(width, newHeight, null, IntPtr.Zero);

                return NewImage;
            }
        }

        public static Image ResizeImage(Image image, int width, int height, bool onlyResizeIfWider)
        {
            // Prevent using images internal thumbnail
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);

            if (onlyResizeIfWider == true)
            {
                if (image.Width <= width)
                {
                    width = image.Width;
                }
            }

            int newHeight = image.Height * width / image.Width;
            if (newHeight > height)
            {
                // Resize with height instead
                width = image.Width * height / image.Height;
                newHeight = height;
            }

            Image NewImage = image.GetThumbnailImage(width, newHeight, null, IntPtr.Zero);

            return NewImage;

        }
    }
}
