using System;

namespace ImageQuantization
{
    public class Pixel
    {
        private byte R, G, B;
        public Pixel()
        {
            R = 0;
            G = 0;
            B = 0;
        }
        public Pixel(byte R, byte G, byte B)
        {
            this.R = R;    //O(1)
            this.G = G;    //O(1)
            this.B = B;    //O(1)
        }

        public Pixel(DPixel pixel)
        {
            R = (byte)pixel.getRed();
            G = (byte)pixel.getGreen();
            B = (byte)pixel.getBlue();
        }

        public byte getRed()
        {
            return R;
        }

        public byte getGreen()
        {
            return G;
        }

        public byte getBlue()
        {
            return B;
        }

        public int getDecimalValue()
        {
            int value = 0;
            value += R;
            value = value << 8;    //O(1)
            value += G;
            value = value << 8;    //O(1)
            value += B;
            return value;
        }
        
        public static Pixel GetPixelFromDecimalValue(int value)
        {
            byte blue = (byte)((value) & 0xFF);              //O(1)
            byte green = (byte)((value >> 8) & 0xFF);        //O(1)
            byte red = (byte)((value >> 16) & 0xFF);         //O(1)
            
            return new Pixel(red, green, blue);
        }
    }


    public class DPixel
    {
        private double R, G, B;
        public DPixel()
        {
            R = 0;
            G = 0;
            B = 0;
        }
        public DPixel(double R, double G, double B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public double getRed()
        {
            return R;
        }

        public double getGreen()
        {
            return G;
        }

        public double getBlue()
        {
            return B;
        }

        public void update(double R, double G, double B)
        {
            this.R = R;    //O(1)
            this.G = G;    //O(1)
            this.B = B;    //O(1)
        }
    }
}
