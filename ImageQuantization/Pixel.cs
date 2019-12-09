using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            this.R = R;
            this.G = G;
            this.B = B;
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

        public void update(byte R, byte G, byte B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public void update(Pixel pixel)
        {
            R = pixel.getRed();
            G = pixel.getGreen();
            B = pixel.getBlue();
        }

        public void update(DPixel pixel)
        {
            R = (byte)pixel.getRed();
            G = (byte)pixel.getGreen();
            B = (byte)pixel.getBlue();
        }

        public void updateRed(byte R)
        {
            this.R = R;
        }

        public void updateGreen(byte G)
        {
            this.G = G;
        }

        public void updateBlue(byte B)
        {
            this.B = B;
        }
        
        public UInt32 getDecimalValue()
        {
            UInt32 value = 0;
            value += R;
            value = value << 8;
            value += G;
            value = value << 8;
            value += B;
            return value;
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
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public void updateRed(double R)
        {
            this.R = R;
        }

        public void updateGreen(double G)
        {
            this.G = G;
        }

        public void updateBlue(double B)
        {
            this.B = B;
        }
    }
}
