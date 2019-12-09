using System;
using System.Windows.Forms;

namespace ImageQuantization
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
/*
 //Please keep this for debugging purposes.
            byte k=0, l=1, m=2;
            Pixel[,] mat = new Pixel[2,2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    mat[i,j] = new Pixel(k, l, m);
                    k++;
                    l++;
                    m++;
                }
            }
            ImageGraph gr = new ImageGraph(mat);
            gr.Print();
*/