using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            SaveFileDialog sfd = new SaveFileDialog();

            ofd.Multiselect = true;
            var oResult= ofd.ShowDialog();
            if (oResult==DialogResult.OK)
            {
                var files = ofd.FileNames;
                var temp1 = files.ToList();
                temp1.Sort();
                files = temp1.ToArray();

                Bitmap[] bitmaps = new Bitmap[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    bitmaps[i] = new Bitmap(files[i]);
                }


                Bitmap output = new Bitmap(bitmaps[0].Width, bitmaps.Select(temp=>temp.Height).Sum());

                int yy = 0;

                for (int i = 0; i < bitmaps.Length; i++)
                {
                    for (int y = 0; y < bitmaps[i].Height; y++)
                    {
                        for (int x = 0; x < bitmaps[i].Width; x++)
                        {
                            var c = bitmaps[i].GetPixel(x, y);

                            output.SetPixel(x,yy, c);
                        }

                        yy++;
                    }
                }

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    output.Save(sfd.FileName, ImageFormat.Jpeg);
                }
            }



            sfd.Dispose();
            ofd.Dispose();
        }
    }
}
