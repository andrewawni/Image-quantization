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
            
            /*Pixel[,] mat = new Pixel[4,4];

             mat[0,0]=new Pixel(190,252,81);
             mat[0,1]=new Pixel(242,255,0);
             mat[0,2]=new Pixel(242,255,0);
             mat[0,3]=new Pixel(190,252,81);
             mat[1,0]=new Pixel(242,255,0);
             mat[1,1]=new Pixel(242,255,0);
             mat[1,2]=new Pixel(190,252,81);
             mat[1,3]=new Pixel(242,255,0);
             mat[2,0]=new Pixel(0,0,255);
             mat[2,1]=new Pixel(0,0,255);
             mat[2,2]=new Pixel(0,0,255);
             mat[2,3]=new Pixel(190,252,81);
             mat[3,0]=new Pixel(0,0,255);
             mat[3,1]=new Pixel(255,0,0);
             mat[3,2]=new Pixel(255,0,0);
             mat[3,3]=new Pixel(190,252,81);


             ImageGraph gr = new ImageGraph(mat);
             Console.WriteLine("Graph \n");
             gr.Print();
             gr.GetGenericGraph().PrintEdges();
             Console.WriteLine("\n");
             MinumumSpanningTree t=new MinumumSpanningTree();
             t.ConstructTree(gr);
             t.tree.PrintEdges();*/

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
    
        }
    }
}