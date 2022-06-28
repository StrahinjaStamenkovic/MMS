using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace MMSProjekat
{
    public class FiltersDisplacement
    {
        public static bool RandomJitter(Bitmap b, short nDegree = 5)
        {
            Point[,] ptRandJitter = new Point[b.Width, b.Height];

            int nWidth = b.Width;
            int nHeight = b.Height;

            int newX, newY;

            short nHalf = (short)Math.Floor((double)nDegree / 2);
            Random rnd = new Random();

            for (int x = 0; x < nWidth; ++x)
                for (int y = 0; y < nHeight; ++y)
                {
                    newX = rnd.Next(nDegree) - nHalf;

                    if (x + newX > 0 && x + newX < nWidth)
                        ptRandJitter[x, y].X = newX;
                    else
                        ptRandJitter[x, y].X = 0;

                    newY = rnd.Next(nDegree) - nHalf;

                    if (y + newY > 0 && y + newY < nWidth)
                        ptRandJitter[x, y].Y = newY;
                    else
                        ptRandJitter[x, y].Y = 0;
                }

            OffsetFilter(b, ptRandJitter);

            return true;
        }
        public static bool OffsetFilter(Bitmap b, Point[,] offset)
        {
            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = bmData.Stride - b.Width * 3;
                int nWidth = b.Width;
                int nHeight = b.Height;

                int xOffset, yOffset;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        xOffset = offset[x, y].X;
                        yOffset = offset[x, y].Y;

                        if (y + yOffset >= 0 && y + yOffset < nHeight && x + xOffset >= 0 && x + xOffset < nWidth)
                        {
                            p[0] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3)];
                            p[1] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3) + 1];
                            p[2] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3) + 2];
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
    }
}
