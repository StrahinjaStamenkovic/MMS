using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace CSharpFilters
{
	public class ConvMatrix
	{
		public int TopLeft = 0, TopMid = 0, TopRight = 0;
		public int MidLeft = 0, Pixel = 1, MidRight = 0;
		public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
		public int Factor = 1;
		public int Offset = 0;
		public void SetAll(int nVal)
		{
			TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
		}
	}

	public struct FloatPoint
	{
		public double X;
		public double Y;
	}

	public class BitmapFilterConvolution
	{
		public const short EDGE_DETECT_KIRSH		= 1;
		public const short EDGE_DETECT_PREWITT		= 2;
		public const short EDGE_DETECT_SOBEL		= 3;

		public static bool Conv3x3(Bitmap b, ConvMatrix m)
		{
			// Avoid divide by zero errors
			if (0 == m.Factor) return false;

			Bitmap bSrc = (Bitmap)b.Clone(); 

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			int stride2 = stride * 2;
			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr SrcScan0 = bmSrc.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;
				byte * pSrc = (byte *)(void *)SrcScan0;

				int nOffset = stride - b.Width*3;
				int nWidth = b.Width - 2;
				int nHeight = b.Height - 2;

				int nPixel;

				for(int y=0;y < nHeight;++y)
				{
					for(int x=0; x < nWidth; ++x )
					{
						nPixel = ( ( ( (pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
							(pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
							(pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

						nPixel = Math.Max(0, Math.Min(255,nPixel));

						p[5 + stride]= (byte)nPixel;

						nPixel = ( ( ( (pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
							(pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
							(pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

						nPixel = Math.Max(0, Math.Min(255, nPixel));

						p[4 + stride] = (byte)nPixel;

						nPixel =  ( ( (pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
							(pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
							(pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset;

						nPixel = Math.Max(0, Math.Min(255, nPixel));

						p[3 + stride] = (byte)nPixel;

						p += 3;
						pSrc += 3;
					}
					p += nOffset;
					pSrc += nOffset;
				}
			}

			b.UnlockBits(bmData);
			bSrc.UnlockBits(bmSrc);

			return true;
		}

		public static bool Smooth(Bitmap b, int nWeight /* default to 1 */)
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(1);
			m.Pixel = nWeight;
			m.Factor = nWeight + 8;

			return  BitmapFilterConvolution.Conv3x3(b, m);
		}

		public static bool GaussianBlur(Bitmap b, int nWeight /* default to 4*/)
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(1);
			m.Pixel = nWeight;
			m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
			m.Factor = nWeight + 12;

			return  BitmapFilterConvolution.Conv3x3(b, m);
		}
		public static bool MeanRemoval(Bitmap b, int nWeight /* default to 9*/ )
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(-1);
			m.Pixel = nWeight;
			m.Factor = nWeight - 8;

			return BitmapFilterConvolution.Conv3x3(b, m);
		}
		public static bool Sharpen(Bitmap b, int nWeight /* default to 11*/ )
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(0);
			m.Pixel = nWeight;
			m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = -2;
			m.Factor = nWeight - 8;

			return  BitmapFilterConvolution.Conv3x3(b, m);
		}
		public static bool EmbossLaplacian(Bitmap b)
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(-1);
			m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 0;
			m.Pixel = 4;
			m.Offset = 127;

			return  BitmapFilterConvolution.Conv3x3(b, m);
		}	
		public static bool EdgeDetectQuick(Bitmap b)
		{
			ConvMatrix m = new ConvMatrix();
			m.TopLeft = m.TopMid = m.TopRight = -1;
			m.MidLeft = m.Pixel = m.MidRight = 0;
			m.BottomLeft = m.BottomMid = m.BottomRight = 1;
		
			m.Offset = 127;

			return  BitmapFilterConvolution.Conv3x3(b, m);
		}

		public static bool EdgeDetectConvolution(Bitmap b, short nType, byte nThreshold)
		{
			ConvMatrix m = new ConvMatrix();

			// I need to make a copy of this bitmap BEFORE I alter it 80)
			Bitmap bTemp = (Bitmap)b.Clone();

			switch (nType)
			{
				case EDGE_DETECT_SOBEL:
					m.SetAll(0);
					m.TopLeft = m.BottomLeft = 1;
					m.TopRight = m.BottomRight = -1;
					m.MidLeft = 2;
					m.MidRight = -2;
					m.Offset = 0;
					break;
				case EDGE_DETECT_PREWITT:
					m.SetAll(0);
					m.TopLeft = m.MidLeft = m.BottomLeft = -1;
					m.TopRight = m.MidRight = m.BottomRight = 1;
					m.Offset = 0;
					break;
				case EDGE_DETECT_KIRSH:
					m.SetAll(-3);
					m.Pixel = 0;
					m.TopLeft = m.MidLeft = m.BottomLeft = 5;
					m.Offset = 0;
					break;
			}

			BitmapFilterConvolution.Conv3x3(b, m);

          
            if (nThreshold > 0)
                IncludeThreshold(b, nThreshold, bTemp);

			return true;
		}

        private static void IncludeThreshold(Bitmap b, byte nThreshold, Bitmap bTemp)
        {
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmData2 = bTemp.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr Scan02 = bmData2.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* p2 = (byte*)(void*)Scan02;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;

                int nPixel = 0;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = (int)Math.Sqrt((p[0] * p[0]) + (p2[0] * p2[0]));
                        if (nPixel < nThreshold) nPixel = nThreshold;
                        if (nPixel > 255) nPixel = 255;
                        p[0] = (byte)nPixel;
                        ++p;
                        ++p2;
                    }
                    p += nOffset;
                    p2 += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bTemp.UnlockBits(bmData2);
        }
	
		public static bool EdgeDetectHorizontal(Bitmap b)
		{
			Bitmap bmTemp = (Bitmap)b.Clone();

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmData2 = bmTemp.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr Scan02 = bmData2.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;
				byte * p2 = (byte *)(void *)Scan02;

				int nOffset = stride - b.Width*3;
				int nWidth = b.Width * 3;

				int nPixel = 0;
	
				p += stride;
				p2 += stride;

				for(int y=1;y<b.Height-1;++y)
				{
					p += 9;
					p2 += 9;

					for(int x=9; x < nWidth-9; ++x )
					{
						nPixel = ((p2 + stride - 9)[0] +
							(p2 + stride - 6)[0] +
							(p2 + stride - 3)[0] +
							(p2 + stride)[0] +
							(p2 + stride + 3)[0] +
							(p2 + stride + 6)[0] +
							(p2 + stride + 9)[0] -
							(p2 - stride - 9)[0] -
							(p2 - stride - 6)[0] -
							(p2 - stride - 3)[0] -
							(p2 - stride)[0] -
							(p2 - stride + 3)[0] -
							(p2 - stride + 6)[0] -
							(p2 - stride + 9)[0]);

						if (nPixel < 0) nPixel = 0;
						if (nPixel > 255) nPixel = 255;

						(p+stride)[0] = (byte) nPixel;
					
						++ p;
						++ p2;
					}

					p += 9 + nOffset;
					p2 += 9 + nOffset;
				}
			}

			b.UnlockBits(bmData);
			bmTemp.UnlockBits(bmData2);

			return true;
		}

		public static bool EdgeDetectVertical(Bitmap b)
		{
			Bitmap bmTemp = (Bitmap)b.Clone();

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmData2 = bmTemp.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr Scan02 = bmData2.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;
				byte * p2 = (byte *)(void *)Scan02;

				int nOffset = stride - b.Width*3;
				int nWidth = b.Width * 3;

				int nPixel = 0;

				int nStride2 = stride *2;
				int nStride3 = stride * 3;
	
				p += nStride3;
				p2 += nStride3;

				for(int y=3;y<b.Height-3;++y)
				{
					p += 3;
					p2 += 3;

					for(int x=3; x < nWidth-3; ++x )
					{
						nPixel = ((p2 + nStride3 + 3)[0] +
							(p2 + nStride2 + 3)[0] +
							(p2 + stride + 3)[0] +
							(p2 + 3)[0] +
							(p2 - stride + 3)[0] +
							(p2 - nStride2 + 3)[0] +
							(p2 - nStride3 + 3)[0] -
							(p2 + nStride3 - 3)[0] -
							(p2 + nStride2 - 3)[0] -
							(p2 + stride - 3)[0] -
							(p2 - 3)[0] -
							(p2 - stride - 3)[0] -
							(p2 - nStride2 - 3)[0] -
							(p2 - nStride3 - 3)[0]);

						if (nPixel < 0) nPixel = 0;
						if (nPixel > 255) nPixel = 255;

						p[0] = (byte) nPixel;
					
						++ p;
						++ p2;
					}

					p += 3 + nOffset;
					p2 += 3 + nOffset;
				}
			}

			b.UnlockBits(bmData);
			bmTemp.UnlockBits(bmData2);

			return true;
		}

		public static bool EdgeDetectHomogenity(Bitmap b, byte nThreshold)
		{
			// This one works by working out the greatest difference between a pixel and it's eight neighbours.
			// The threshold allows softer edges to be forced down to black, use 0 to negate it's effect.
			Bitmap b2 = (Bitmap) b.Clone();

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmData2 = b2.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr Scan02 = bmData2.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;
				byte * p2 = (byte *)(void *)Scan02;

				int nOffset = stride - b.Width*3;
				int nWidth = b.Width * 3;

				int nPixel = 0, nPixelMax = 0;

				p += stride;
				p2 += stride;

				for(int y=1;y<b.Height-1;++y)
				{
					p += 3;
					p2 += 3;

					for(int x=3; x < nWidth-3; ++x )
					{
						nPixelMax = Math.Abs(p2[0] - (p2+stride-3)[0]);
						nPixel = Math.Abs(p2[0] - (p2 + stride)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs(p2[0] - (p2 + stride + 3)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs(p2[0] - (p2 - stride)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs(p2[0] - (p2 + stride)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs(p2[0] - (p2 - stride - 3)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs(p2[0] - (p2 - stride)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs(p2[0] - (p2 - stride + 3)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						if (nPixelMax < nThreshold) nPixelMax = 0;

						p[0] = (byte) nPixelMax;

						++ p;
						++ p2;
					}

					p += 3 + nOffset;
					p2 += 3 + nOffset;
				}
			}

			b.UnlockBits(bmData);
			b2.UnlockBits(bmData2);

			return true;
            
		}
		public static bool EdgeDetectDifference(Bitmap b, byte nThreshold)
		{
			// This one works by working out the greatest difference between a pixel and it's eight neighbours.
			// The threshold allows softer edges to be forced down to black, use 0 to negate it's effect.
			Bitmap b2 = (Bitmap) b.Clone();

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmData2 = b2.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr Scan02 = bmData2.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;
				byte * p2 = (byte *)(void *)Scan02;

				int nOffset = stride - b.Width*3;
				int nWidth = b.Width * 3;

				int nPixel = 0, nPixelMax = 0;

				p += stride;
				p2 += stride;

				for(int y=1;y<b.Height-1;++y)
				{
					p += 3;
					p2 += 3;

					for(int x=3; x < nWidth-3; ++x )
					{
						nPixelMax = Math.Abs((p2 - stride + 3)[0] - (p2+stride-3)[0]);
						nPixel = Math.Abs((p2 + stride + 3)[0] - (p2 - stride - 3)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs((p2 - stride)[0] - (p2 + stride)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs((p2+3)[0] - (p2 - 3)[0]);
						if (nPixel>nPixelMax) nPixelMax = nPixel;

						if (nPixelMax < nThreshold) nPixelMax = 0;

						p[0] = (byte) nPixelMax;

						++ p;
						++ p2;
					}

					p += 3 + nOffset;
					p2 += 3 + nOffset;
				}
			}

			b.UnlockBits(bmData);
			b2.UnlockBits(bmData2);

			return true;
            
		}

		public static bool EdgeEnhance(Bitmap b, byte nThreshold)
		{
			// This one works by working out the greatest difference between a nPixel and it's eight neighbours.
			// The threshold allows softer edges to be forced down to black, use 0 to negate it's effect.
			Bitmap b2 = (Bitmap) b.Clone();

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmData2 = b2.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr Scan02 = bmData2.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;
				byte * p2 = (byte *)(void *)Scan02;

				int nOffset = stride - b.Width*3;
				int nWidth = b.Width * 3;

				int nPixel = 0, nPixelMax = 0;

				p += stride;
				p2 += stride;

				for (int y = 1; y < b.Height-1; ++y)
				{
					p += 3;
					p2 += 3;

					for (int x = 3; x < nWidth-3; ++x)
					{
						nPixelMax = Math.Abs((p2 - stride + 3)[0] - (p2 + stride - 3)[0]);

						nPixel = Math.Abs((p2 + stride + 3)[0] - (p2 - stride - 3)[0]);

						if (nPixel > nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs((p2 - stride)[0] - (p2 + stride)[0]);

						if (nPixel > nPixelMax) nPixelMax = nPixel;

						nPixel = Math.Abs((p2 + 3)[0] - (p2 - 3)[0]);

						if (nPixel > nPixelMax) nPixelMax = nPixel;

						if (nPixelMax > nThreshold && nPixelMax > p[0])
							p[0] = (byte) Math.Max(p[0], nPixelMax);

						++ p;
						++ p2;			
					}

					p += nOffset + 3;
					p2 += nOffset + 3;
				}
			}	

			b.UnlockBits(bmData);
			b2.UnlockBits(bmData2);

			return true;
		}

		public static Bitmap Resize(Bitmap b, int nWidth, int nHeight, bool bBilinear)
		{
			Bitmap bTemp = (Bitmap)b.Clone();
			b = new Bitmap(nWidth, nHeight, bTemp.PixelFormat);

			double nXFactor = (double)bTemp.Width/(double)nWidth;
			double nYFactor = (double)bTemp.Height/(double)nHeight;

			if (bBilinear)
			{
				double fraction_x, fraction_y, one_minus_x, one_minus_y;
				int ceil_x, ceil_y, floor_x, floor_y;
				Color c1 = new Color();
				Color c2 = new Color();
				Color c3 = new Color();
				Color c4 = new Color();
				byte red, green, blue;

				byte b1, b2;

				for (int x = 0; x < b.Width; ++x)
					for (int y = 0; y < b.Height; ++y)
					{
						// Setup

						floor_x = (int)Math.Floor(x * nXFactor);
						floor_y = (int)Math.Floor(y * nYFactor);
						ceil_x = floor_x + 1;
						if (ceil_x >= bTemp.Width) ceil_x = floor_x;
						ceil_y = floor_y + 1;
						if (ceil_y >= bTemp.Height) ceil_y = floor_y;
						fraction_x = x * nXFactor - floor_x;
						fraction_y = y * nYFactor - floor_y;
						one_minus_x = 1.0 - fraction_x;
						one_minus_y = 1.0 - fraction_y;

						c1 = bTemp.GetPixel(floor_x, floor_y);
						c2 = bTemp.GetPixel(ceil_x, floor_y);
						c3 = bTemp.GetPixel(floor_x, ceil_y);
						c4 = bTemp.GetPixel(ceil_x, ceil_y);

						// Blue
						b1 = (byte)(one_minus_x * c1.B + fraction_x * c2.B);

						b2 = (byte)(one_minus_x * c3.B + fraction_x * c4.B);
						
						blue = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

						// Green
						b1 = (byte)(one_minus_x * c1.G + fraction_x * c2.G);

						b2 = (byte)(one_minus_x * c3.G + fraction_x * c4.G);
						
						green = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

						// Red
						b1 = (byte)(one_minus_x * c1.R + fraction_x * c2.R);

						b2 = (byte)(one_minus_x * c3.R + fraction_x * c4.R);
						
						red = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

						b.SetPixel(x,y, System.Drawing.Color.FromArgb(255, red, green, blue));
					}
			}
			else
			{
				for (int x = 0; x < b.Width; ++x)
					for (int y = 0; y < b.Height; ++y)
						b.SetPixel(x, y, bTemp.GetPixel((int)(Math.Floor(x * nXFactor)),(int)(Math.Floor(y * nYFactor))));
			}

			return b;
		}

		
	}
}
