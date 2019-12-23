using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Pixel[,] ImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            ImageGraph gr = new ImageGraph(ImageMatrix);
            PrimMinSpanningTree t=new PrimMinSpanningTree(1 << 24, gr);
            t.printMST();
            Console.WriteLine("\n Sum of MST : "+Math.Round(t.SumOfTree(),2));
            Console.WriteLine(gr.GetVertices().Count);
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            
            Console.WriteLine("Enter K:\n");
            int k = Convert.ToInt32(Console.ReadLine());
            Dictionary<int,int> temp =  t.GenerateClusters(k);
            
            Console.WriteLine("Mapped colors:\n");
            foreach (var color in temp.Keys)
            {
                Console.WriteLine(color + " : " + temp[color]);
            }

            int h = ImageOperations.GetHeight(ImageMatrix), w = ImageOperations.GetWidth(ImageMatrix);
            Pixel[,] output = new Pixel[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    output[i,j] = Pixel.GetPixelFromDecimalValue(temp[ImageMatrix[i,j].getDecimalValue()]);
                }
            }
            ImageOperations.DisplayImage(output, pictureBox2);
            
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

       
       
    }
}