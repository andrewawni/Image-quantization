using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Console.WriteLine("1-Run the program.\n2-Benchmark cases.");
            int choice = Console.Read();
            if (choice == '2')
            {
                /**
                 * uncomment to run full benchmark,
                 * place all pictures in the project directory
                 */

                //    /*
                Console.WriteLine();
                List<KeyValuePair<string, int>> paths = new List<KeyValuePair<string, int>>();

                StreamReader file = new StreamReader(@"../../paths.txt"); //move up from ./bin/debug
                StreamWriter outfile = new StreamWriter(@"../../benchmark.txt");
                string path = "";
                int k;
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string num = line.Substring(0, line.IndexOf('.'));
                    k = Int32.Parse(num);
                    path = line.Substring(line.IndexOf('.'), line.Length - line.IndexOf('.'));
                    //Console.WriteLine(k + " " + path);
                    paths.Add(new KeyValuePair<string, int>("../." + path, k));
                }


                Stopwatch stopwatch = new Stopwatch();

                for (int img = paths.Count - 1; img >= 0; img--)
                {

                    path = paths[img].Key;
                    ImageMatrix = ImageOperations.OpenImage(paths[img].Key);
                    k = paths[img].Value;
                    //ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
                    string imgname = "k=" + k + "_" +
                                     paths[img].Key.Substring(paths[img].Key.LastIndexOf('/') + 1,
                                         path.Length - path.LastIndexOf('/') - 1);

                    outfile.WriteLine(imgname);
                    Console.WriteLine(imgname);

                    stopwatch.Start();
                    ImageGraph gr = new ImageGraph(ImageMatrix);
                    PrimMinSpanningTree t = new PrimMinSpanningTree(1 << 24, gr);

                    Console.WriteLine("Distinct colors: " + gr.GetVertices().Count);
                    outfile.WriteLine("Distinct colors: " + gr.GetVertices().Count);


                    Console.WriteLine("MST sum: " + Math.Round(t.SumOfTree(), 2));
                    outfile.WriteLine("MST sum: " + Math.Round(t.SumOfTree(), 2));

                    //txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
                    ///txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();

                    Dictionary<int, int> temp = t.GenerateClusters(k);
                    int h = ImageOperations.GetHeight(ImageMatrix), w = ImageOperations.GetWidth(ImageMatrix);
                    Pixel[,] output = new Pixel[h, w];
                    for (int i = 0; i < h; i++)
                    {
                        for (int j = 0; j < w; j++)
                        {
                            output[i, j] = Pixel.GetPixelFromDecimalValue(temp[ImageMatrix[i, j].getDecimalValue()]);
                        }
                    }

                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss\\.fff}", stopwatch.Elapsed);
                    outfile.WriteLine("Time elapsed: {0:hh\\:mm\\:ss\\.fff}", stopwatch.Elapsed);

                    stopwatch.Reset();
                    //ImageOperations.DisplayImage(output, pictureBox2);

                    saveImage(@imgname, output);

                    outfile.WriteLine("====================================");
                    Console.WriteLine("====================================");
                }

                outfile.Close();
                file.Close();
                // */
            }
        }

        private void saveImage(string name, Pixel[,] img)
        {
            int Height = img.GetLength(0);
            int Width = img.GetLength(1);

            Bitmap ImageBMP = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = Width * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        p[2] = img[i, j].getRed();
                        p[1] = img[i, j].getGreen();
                        p[0] = img[i, j].getBlue();
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd);
            }
            
            //For testing purposes
            ImageBMP.Save(name);
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
            PrimMinSpanningTree t = new PrimMinSpanningTree(1 << 24, gr);
            //t.printMST();
            Console.WriteLine("\n Sum of MST : "+Math.Round(t.SumOfTree(),2));
            //Console.WriteLine(gr.GetVertices().Count);
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            
            Console.WriteLine("Enter K:\n");
            int k = Convert.ToInt32(Console.ReadLine());
           
            Dictionary<int,int> temp =  t.GenerateClusters(k);
            
            /*
            Console.WriteLine("Mapped colors:\n");
            foreach (var color in temp.Keys)
            {
                Console.WriteLine(color + " : " + temp[color]);
            }
            */
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