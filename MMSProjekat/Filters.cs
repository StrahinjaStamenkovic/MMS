using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MMSProjekat
{
	public enum CMY
	{
		Y = 0,
		M = 1,
		C = 2
	}

	public class Filters
	{
		public static int[,] OrderedDitherMap2x2 = new int[2, 2]
		{
			{ 0, 2 },
			{ 3, 1}
		};
		public static int[,] OrderedDitherMap4x4 = new int[4, 4]
		{
			{ 0, 8, 2, 10 },
			{ 12, 4, 14, 6 },
			{ 3, 11, 1, 9 },
			{ 15, 7, 13, 5 },
		};
		public static int[,] OrderedDitherMap8x8 = new int[8, 8]
		{
			{ 0, 32, 8, 40, 2, 34, 10, 42 },
			{ 48, 16, 56, 24, 50, 18, 58, 26},
			{ 12, 44, 4, 36, 14, 46, 6, 38},
			{ 60, 28, 52, 20, 62, 30, 54, 22},
			{ 3, 35, 11, 43, 1, 33, 9, 41},
			{ 51, 19, 59, 27, 49, 17, 57, 25},
			{ 15, 47, 7, 39, 13, 45, 5, 37},
			{ 63, 31, 55, 23, 61, 29, 52, 21},
		};
		public static bool InvertUnsafe(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;
				int nWidth = b.Width;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						p[0] = (byte)(255 - p[0]);
						p[1] = (byte)(255 - p[1]);
						p[2] = (byte)(255 - p[2]);

						p += 3;

					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
		public static bool ConvertBGRtoCMY(Bitmap bmp, CMY channel)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - bmp.Width * 3;
				int nWidth = bmp.Width;

				for (int y = 0; y < bmp.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						//p[0] = (byte)(((int)channel == 1 || (int)channel == 2) ? (255 - p[0]) : 0);
						//p[1] = (byte)(((int)channel == 0 || (int)channel == 2) ? (255 - p[1]) : 0);
						//p[2] = (byte)(((int)channel == 0 || (int)channel == 1) ? (255 - p[2]) : 0);
						byte r = p[2];
						byte g = p[1];
						byte b = p[0];

						switch (channel)
						{
							case CMY.C:
								p[0] = (byte)(255 - r);
								p[1] = (byte)(255 - r);
								p[2] = 0;
								break;
							case CMY.M:
								p[0] = (byte)(255 - g);
								p[1] = 0;
								p[2] = (byte)(255 - g);
								break;
							case CMY.Y:
								p[0] = 0;
								p[1] = (byte)(255 - b);
								p[2] = (byte)(255 - b);
								break;
						}

						p += 3;

					}
					p += nOffset;
				}
			}

			bmp.UnlockBits(bmData);

			return true;
		}
		public static bool InvertSafe(Bitmap b)
		{
			for (int i = 0; i < b.Width; i++)
			{
				for (int j = 0; j < b.Height; j++)
				{
					System.Drawing.Color c = b.GetPixel(i, j);
					b.SetPixel(i, j, System.Drawing.Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
				}
			}

			return true;
		}
		public static bool Gamma(Bitmap b, double red, double green, double blue)
		{
			if (red < .02 || red > 50) return false;
			if (green < .02 || green > 50) return false;
			if (blue < .02 || blue > 50) return false;

			byte[] redGamma = new byte[256];
			byte[] greenGamma = new byte[256];
			byte[] blueGamma = new byte[256];

			for (int i = 0; i < 256; ++i)
			{
				redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / red)) + 0.5));
				greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / green)) + 0.5));
				blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / blue)) + 0.5));
			}

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);


			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						p[2] = redGamma[p[2]];
						p[1] = greenGamma[p[1]];
						p[0] = blueGamma[p[0]];

						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
		public static bool GrayscaleMean(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;
				int nWidth = b.Width;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						byte red = p[2];
						byte green = p[1];
						byte blue = p[0];

						int greyscale = (red + green + blue) / 3;
						p[0] = (byte)greyscale;
						p[1] = (byte)greyscale;
						p[2] = (byte)greyscale;

						p += 3;
					}

				}
				p += nOffset;
			}
			b.UnlockBits(bmData);
			return true;
		}
		public static bool GrayscaleMax(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;
				int nWidth = b.Width;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						byte red = p[2];
						byte green = p[1];
						byte blue = p[0];

						byte greyscale = (byte)Math.Max(red, Math.Max(green, blue));
						p[0] = greyscale;
						p[1] = greyscale;
						p[2] = greyscale;
						p += 3;
					}
				}
				p += nOffset;
			}
			b.UnlockBits(bmData);
			return true;
		}
		public static bool GrayscaleMaxMin(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;
				int nWidth = b.Width;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						byte red = p[2];
						byte green = p[1];
						byte blue = p[0];

						byte greyscale = (byte)((Math.Max(red, Math.Max(green, blue)) + Math.Min(red, Math.Min(green, blue))) / 2);
						p[0] = greyscale;
						p[1] = greyscale;
						p[2] = greyscale;
						p += 3;
					}
				}
				p += nOffset;
			}
			b.UnlockBits(bmData);
			return true;
		}
		public static int[] Histogram(Bitmap bmp, CMY channel)
		{
			int[] values = new int[256];
			for (int i = 0; i < 256; i++)
				values[i] = 0;

			BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - bmp.Width * 3;
				int nWidth = bmp.Width;

				for (int y = 0; y < bmp.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						switch (channel)
						{
							case CMY.C:
								values[p[0]] += 1;
								break;
							case CMY.M:
								values[p[0]] += 1;
								break;
							case CMY.Y:
								values[p[1]] += 1;
								break;
						}
						p += 3;
					}
					p += nOffset;
				}
			}
			bmp.UnlockBits(bmData);
			return values;
		}
		public static bool HistogramFilter(Bitmap b, CMY channel, byte min, byte max)
		{
			byte mostCommonLow = min, mostCommonHigh = max;
			int[] histogramValues = Histogram(b, channel);
			for (byte i = min; i <= max; i++)
			{
				if (i < 128)
				{
					mostCommonLow = (histogramValues[mostCommonLow] > histogramValues[i]) ? mostCommonLow : i;
				}
				else
				{
					mostCommonHigh = (histogramValues[mostCommonHigh] > histogramValues[i]) ? mostCommonHigh : i;

				}
			}


			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						switch (channel)
						{
							case CMY.C:
								if (p[0] < min)
								{
									p[0] = mostCommonLow;
									p[1] = mostCommonLow;
								}
								if (p[0] > max)
								{
									p[0] = mostCommonHigh;
									p[1] = mostCommonHigh;
								}
								break;
							case CMY.M:
								if (p[0] < min)
								{
									p[0] = mostCommonLow;
									p[2] = mostCommonLow;
								}
								if (p[0] > max)
								{
									p[0] = mostCommonHigh;
									p[2] = mostCommonHigh;
								}
								break;
							case CMY.Y:
								if (p[1] < min)
								{
									p[1] = mostCommonLow;
									p[2] = mostCommonLow;
								}
								if (p[1] > max)
								{
									p[1] = mostCommonHigh;
									p[2] = mostCommonHigh;
								}
								break;
						}
						p += 3;
					}
					p += nOffset;
				}
			}
			b.UnlockBits(bmData);
			return true;
		}
		public static bool ConvertCMYtoBGR(Bitmap b, Bitmap bC, Bitmap bM, Bitmap bY)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);
			BitmapData bmDataC = bC.LockBits(new Rectangle(0, 0, bC.Width, bC.Height), ImageLockMode.ReadWrite, bC.PixelFormat); // PixelFormat.Format24bppRgb);
			BitmapData bmDataM = bM.LockBits(new Rectangle(0, 0, bM.Width, bM.Height), ImageLockMode.ReadWrite, bM.PixelFormat); // PixelFormat.Format24bppRgb);
			BitmapData bmDataY = bY.LockBits(new Rectangle(0, 0, bY.Width, bY.Height), ImageLockMode.ReadWrite, bY.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr Scan0C = bmDataC.Scan0;
			System.IntPtr Scan0M = bmDataM.Scan0;
			System.IntPtr Scan0Y = bmDataY.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				byte* cPtr = (byte*)(void*)Scan0C;
				byte* mPtr = (byte*)(void*)Scan0M;
				byte* yPtr = (byte*)(void*)Scan0Y;

				int nOffset = stride - b.Width * 3;
				int nWidth = b.Width;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						byte cyan = p[2];
						byte magenta = p[1];
						byte yellow = p[0];

						p[0] = (byte)(255 - yPtr[1]);
						p[1] = (byte)(255 - mPtr[2]);
						p[2] = (byte)(255 - cPtr[0]);

						p += 3;
						cPtr += 3;
						mPtr += 3;
						yPtr += 3;

					}
					p += nOffset;
					cPtr += nOffset;
					mPtr += nOffset;
					yPtr += nOffset;
				}
			}
			b.UnlockBits(bmData);
			bC.UnlockBits(bmDataC);
			bM.UnlockBits(bmDataM);
			bY.UnlockBits(bmDataY);

			return true;
		}
		public static bool OrderedDithering(Bitmap b, int[,] map, int colorsInPaletteChannel)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;
				int nWidth = b.Width;
				int n = (int)Math.Sqrt(map.Length);
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						p[0] = ClosestColorToPalette((int)Math.Min(255, Math.Max(0, ((p[0] + (255 / colorsInPaletteChannel) * map[y % n, x % n] - .5)))), colorsInPaletteChannel);
						p[1] = ClosestColorToPalette((int)Math.Min(255, Math.Max(0, ((p[1] + (255 / colorsInPaletteChannel) * map[y % n, x % n] - .5)))), colorsInPaletteChannel);
						p[2] = ClosestColorToPalette((int)Math.Min(255, Math.Max(0, ((p[2] + (255 / colorsInPaletteChannel) * map[y % n, x % n] - .5)))), colorsInPaletteChannel);

						p += 3;

					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
		public static byte ClosestColorToPalette(int value, int nCol)
		{

			//decimal stride = (decimal)256 / (decimal)nCol;
			//decimal floor=Math.Floor((decimal)value / stride), ceil = Math.Ceiling((decimal)value / stride);

			//if (ceil * stride > 255)
			//	return (byte)(floor * stride);
			//if (Math.Abs(value - floor * stride) < Math.Abs(value - ceil * stride))
			//	return (byte)(floor * stride);
			//else 
			//	return (byte)(ceil * stride);

			double newValue = (double)value / 255.0;
			return (byte)(Math.Round(newValue) * 255);

		}
		public static bool GenericDither(Bitmap b, int nCol, Point[] offsets, int[] factors)
		{
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						for (int channel = 0; channel < 3; channel++)
						{
							int index1DBase = y * stride + x * 3 + channel;
							byte oldVal = p[index1DBase];
							byte newVal = ClosestColorToPalette(oldVal, nCol);
							int quantError = oldVal - newVal;
							p[index1DBase] = newVal;

							for (int k = 0; k < offsets.Length; k++)
							{
								Point offset = offsets[k];
								int factor = factors[k];
								int index1DOffset = (y + offset.Y) * stride + (x + offset.X) * 3 + channel;

								if (index1DOffset < bmData.Height * bmData.Width * 3)
									p[index1DOffset] = (byte)Math.Max(0, Math.Min(255, p[index1DOffset] + quantError * factor / 16));
							}
						}
					}
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
		public static bool FloydSteinbergDither(Bitmap b, int nCol)
		{
			Point[] offsets = new Point[4]
			{
				new Point(1,0),
				new Point(-1,1),
				new Point(0,1),
				new Point(1,1),

			};
			int[] factors = new int[4] { 7, 3, 5, 1 };
			return GenericDither(b, nCol, offsets, factors);
		}
		public static bool FloydSteinbergDitherSafe(Bitmap b)
		{
			for (int y = 0; y < b.Height - 1; y++)
			{
				for (int x = 1; x < b.Width - 1; x++)
				{
					Color oldPixel = b.GetPixel(x, y);
					Color newPixel = RoundColor(oldPixel, 1);
					b.SetPixel(x, y, newPixel);
					Error error = new Error(oldPixel.R - newPixel.R, oldPixel.G - newPixel.G, oldPixel.B - newPixel.B);

					b.SetPixel(x + 1, y, AddError(b.GetPixel(x + 1, y), error, 7 / 16.0));
					b.SetPixel(x - 1, y + 1, AddError(b.GetPixel(x - 1, y + 1), error, 3 / 16.0));
					b.SetPixel(x, y + 1, AddError(b.GetPixel(x, y + 1), error, 5 / 16.0));
					b.SetPixel(x + 1, y + 1, AddError(b.GetPixel(x + 1, y + 1), error, 1 / 16.0));
				}
			}
			return true;
		}
		public static bool StuckiDither(Bitmap b, int nCol)
		{
			Point[] offsets = new Point[12] {
				new Point(+1,0),
				new Point(+2,0),
				new Point(-2,1),
				new Point(-1,1),
				new Point(0,1),
				new Point(+1,1),
				new Point(+2,1),
				new Point(-2,2),
				new Point(-1,2),
				new Point(0,2),
				new Point(+1,2),
				new Point(+2,2)

			};
			int[] factors = new int[] { 8, 4, 2, 4, 8, 4, 2, 1, 2, 4, 2, 1 };

			return GenericDither(b, nCol, offsets, factors);
		}
		static public Color RoundColor(Color color, int factor)
		{
			double R = (double)factor * color.R / 255.0;
			double newR = Math.Round(R) * (255 / factor);
			double G = (double)factor * color.G / 255.0;
			double newG = Math.Round(G) * (255 / factor);
			double B = (double)factor * color.B / 255.0;
			double newB = Math.Round(B) * (255 / factor);
			return Color.FromArgb((int)newR, (int)newG, (int)newB);
		}
		static public Color AddError(Color pixel, Error error, double amount)
		{
			int R = (int)(pixel.R + (error.R * amount));
			int G = (int)(pixel.G + (error.G * amount));
			int B = (int)(pixel.B + (error.B * amount));
			return Color.FromArgb(Math.Max(0, Math.Min(255, R)), Math.Max(0, Math.Min(255, G)), Math.Max(0, Math.Min(255, B)));
		}
		public class Error
		{
			public double R { get; set; }
			public double G { get; set; }
			public double B { get; set; }
			public Error() { }
			public Error(double r, double g, double b)
			{
				R = r;
				G = g;
				B = b;
			}
		}
		public static byte[,] DefaultGrayscaleToRGBMap = new byte[8, 3]
		{
			{255, 217, 44 },
			{192, 39, 21 },
			{ 192, 50, 3},
			{208, 131, 229 },
			{58, 8, 200 },
			{ 225, 59, 240},
			{55, 232, 92 },
			{234, 199, 70 },
		};
		public static bool ColorizeDefault(Bitmap b, byte[,] map)
		{
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{

						int i = 0;
						while (32 * (++i) < p[0]) ;
						p[0] = map[i - 1, 2];
						p[1] = map[i - 1, 1];
						p[2] = map[i - 1, 0];

						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
		public static bool ColorizeAccordingToSample(Bitmap b, SortedDictionary<byte, byte[]> mappings)
		{
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{

						p[0] = mappings[p[0]][2];
						p[1] = mappings[p[1]][1];
						p[2] = mappings[p[2]][0];

						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
		public static SortedDictionary<byte, byte[]> CalculateGrayscaleToRGBMappings(Bitmap source)
		{
			SortedDictionary<byte, byte[]> mappings = new SortedDictionary<byte, byte[]>();
			BitmapData bmData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite, source.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - source.Width * 3;
				for (int y = 0; y < source.Height; ++y)
				{
					for (int x = 0; x < source.Width; ++x)
					{
						byte grayscale = (byte)(0.299 * p[2] + 0.587 * p[1] + 0.114 * p[0]);

						mappings[grayscale] = new byte[3] { p[0], p[1], p[2] };
						p += 3;
					}
					p += nOffset;
				}
			}
			source.UnlockBits(bmData);

			Random rnd = new Random();


			for (int i = 0; i <= 255 || mappings.Count != 256; i++)
			{
				if (!mappings.ContainsKey((byte)i))
				{
					byte[] rgb = new byte[3];
					rnd.NextBytes(rgb);
					mappings[(byte)i] = rgb;

				}
			}


			return mappings;
		}
		
		//Posto colorize koristim samo na grayscale slikama jer sam tako ukapirao da treba, a svi kanali grayscale slika su identicni,
		//kao rezultat se dobija da je delta uvek 0, kao rezultat slika ostaje nepromenjena
		public static bool CrossDomainColorize(Bitmap b, double Hue, bool UseSaturationValue, double Saturation)
		{
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* ptr = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						byte R = ptr[2];
						byte G = ptr[1];
						byte B = ptr[0];

						double RPrime = ((double)R / 255.0);
						double GPrime = ((double)G / 255.0);
						double BPrime = ((double)B / 255.0);

						double Cmax = Math.Max(RPrime, Math.Max(GPrime, BPrime));
						double Cmin = Math.Min(RPrime, Math.Min(GPrime, BPrime));

						double Delta = Cmax - Cmin;

						double H = Hue;
						double S = (UseSaturationValue) ? Saturation : (Cmax == 0) ? 0 : Delta / Cmax;
						double V = Cmax;

						V *= 255;
						double Hi = Math.Floor(H + 1) % 6;
						double f = (H + 1) - Math.Floor(H + 1);
						double p = V * (1 - S);
						double q = V * (1 - f * S);
						double t = V * (1 - (1 - f) * S);


						switch (Hi)
						{
							case 0:
								R = (byte)V;
								G = (byte)t;
								B = (byte)p;
								break;
							case 1:
								R = (byte)q;
								G = (byte)V;
								B = (byte)p;
								break;
							case 2:
								R = (byte)p;
								G = (byte)V;
								B = (byte)t;
								break;
							case 3:
								R = (byte)p;
								G = (byte)q;
								B = (byte)V;
								break;
							case 4:
								R = (byte)t;
								G = (byte)p;
								B = (byte)V;
								break;
							case 5:
								R = (byte)V;
								G = (byte)p;
								B = (byte)q;
								break;

						}
						ptr[0] = B;
						ptr[1] = G;
						ptr[2] = R;
						ptr += 3;

					}
					ptr += nOffset;
				}
			}

			b.UnlockBits(bmData);


			return true;
		}
		public static Bitmap KuwaharaSafe(Bitmap Image, int Size)
		{
			Bitmap TempBitmap = Image;
			Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);

			if (Size % 4 != 1)
				return null;
			int[] ApetureMinX = { -(Size / 2), 0, -(Size / 2), 0 };
			int[] ApetureMaxX = { 0, (Size / 2), 0, (Size / 2) };
			int[] ApetureMinY = { -(Size / 2), -(Size / 2), 0, 0 };
			int[] ApetureMaxY = { 0, 0, (Size / 2), (Size / 2) };
			for (int x = 0; x < NewBitmap.Width; ++x)
			{
				for (int y = 0; y < NewBitmap.Height; ++y)
				{
					int[] RValues = { 0, 0, 0, 0 };
					int[] GValues = { 0, 0, 0, 0 };
					int[] BValues = { 0, 0, 0, 0 };
					int[] NumPixels = { 0, 0, 0, 0 };
					int[] MaxRValue = { 0, 0, 0, 0 };
					int[] MaxGValue = { 0, 0, 0, 0 };
					int[] MaxBValue = { 0, 0, 0, 0 };
					int[] MinRValue = { 255, 255, 255, 255 };
					int[] MinGValue = { 255, 255, 255, 255 };
					int[] MinBValue = { 255, 255, 255, 255 };
					for (int i = 0; i < 4; ++i)
					{
						for (int x2 = ApetureMinX[i]; x2 < ApetureMaxX[i]; ++x2)
						{
							int TempX = x + x2;
							if (TempX >= 0 && TempX < NewBitmap.Width)
							{
								for (int y2 = ApetureMinY[i]; y2 < ApetureMaxY[i]; ++y2)
								{
									int TempY = y + y2;
									if (TempY >= 0 && TempY < NewBitmap.Height)
									{
										Color TempColor = TempBitmap.GetPixel(TempX, TempY);
										RValues[i] += TempColor.R;
										GValues[i] += TempColor.G;
										BValues[i] += TempColor.B;

										MaxRValue[i] = (TempColor.R > MaxRValue[i]) ? TempColor.R : MaxRValue[i];
										MinRValue[i] = (TempColor.R < MinRValue[i]) ? TempColor.R : MinRValue[i];

										MaxGValue[i] = (TempColor.R > MaxGValue[i]) ? TempColor.R : MaxGValue[i];
										MinGValue[i] = (TempColor.R < MinGValue[i]) ? TempColor.R : MinGValue[i];

										MaxBValue[i] = (TempColor.R > MaxBValue[i]) ? TempColor.R : MaxBValue[i];
										MinBValue[i] = (TempColor.R < MinBValue[i]) ? TempColor.R : MinGValue[i];

										++NumPixels[i];
									}
								}
							}
						}
					}
					int j = 0;
					int MinDifference = 10000;
					for (int i = 0; i < 4; ++i)
					{
						int CurrentDifference = (MaxRValue[i] - MinRValue[i]) + (MaxGValue[i] - MinGValue[i]) + (MaxBValue[i] - MinBValue[i]);
						if (CurrentDifference < MinDifference && NumPixels[i] > 0)
						{
							j = i;
							MinDifference = CurrentDifference;
						}
					}

					Color MeanPixel = Color.FromArgb(RValues[j] / NumPixels[j],
						GValues[j] / NumPixels[j],
						BValues[j] / NumPixels[j]);
					NewBitmap.SetPixel(x, y, MeanPixel);
				}
			}
			return NewBitmap;
		}
		public static bool KuwaharaUnsafe(Bitmap b, int size)
		{
			if (size % 4 != 1)
				return false;
			Bitmap bSrc = (Bitmap)b.Clone();

			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;

			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr SrcScan0 = bmSrc.Scan0;

			int[] ApetureMinX = { -(size / 2), 0, -(size / 2), 0 };
			int[] ApetureMaxX = { 0, (size / 2), 0, (size / 2) };
			int[] ApetureMinY = { -(size / 2), -(size / 2), 0, 0 };
			int[] ApetureMaxY = { 0, 0, (size / 2), (size / 2) };

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				byte* pSrc = (byte*)(void*)SrcScan0;
				int nOffset = stride - b.Width * 3;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						int[,] BGRValues = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
						int[] NumPixels = { 0, 0, 0, 0 };
						int[,] MaxBGRValue = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
						int[,] MinBGRValue = { { 255, 255, 255 }, { 255, 255, 255 }, { 255, 255, 255 }, { 255, 255, 255 } };

						for (int i = 0; i < 4; ++i)
						{
							for (int y2 = ApetureMinY[i]; y2 < ApetureMaxY[i]; ++y2)
							{
								if ((y + y2) >= 0 && (y + y2) < b.Height)
								{
									for (int x2 = ApetureMinX[i]; x2 < ApetureMaxX[i]; ++x2)
									{
										if ((x + x2) >= 0 && (x + x2) < b.Width)
										{
											for (int channel = 0; channel < 3; channel++)
											{
												int index1D = (y + y2) * stride + (x + x2) * 3 + channel;

												BGRValues[i, channel] += pSrc[index1D];

												MaxBGRValue[i, channel] = (pSrc[index1D] > MaxBGRValue[i, channel]) ? pSrc[index1D] : MaxBGRValue[i, channel];
												MinBGRValue[i, channel] = (pSrc[index1D] < MinBGRValue[i, channel]) ? pSrc[index1D] : MinBGRValue[i, channel];
											}
											++NumPixels[i];
										}
									}
								}
							}
						}
						int j = 0;
						int MinDifference = Int32.MaxValue;
						for (int i = 0; i < 4; ++i)
						{
							int CurrentDifference = (MaxBGRValue[i, 0] - MinBGRValue[i, 0]) + (MaxBGRValue[i, 1] - MinBGRValue[i, 1]) + (MaxBGRValue[i, 2] - MinBGRValue[i, 2]);
							if (CurrentDifference < MinDifference && NumPixels[i] > 0)
							{
								j = i;
								MinDifference = CurrentDifference;
							}
						}
						int meanR = BGRValues[j, 2] / NumPixels[j],
							meanG = BGRValues[j, 1] / NumPixels[j],
							meanB = BGRValues[j, 0] / NumPixels[j];

						p[0] = (byte)meanB;
						p[1] = (byte)meanG;
						p[2] = (byte)meanR;

						p += 3;
					}
					p += nOffset;
				}

				b.UnlockBits(bmData);
				bSrc.UnlockBits(bmSrc);
			}
			return true;
		}
		public static int Similarity(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2)
		{
			return (int)Math.Sqrt(Math.Pow(r1 - r2, 2) + Math.Pow(g1 - g2, 2) + Math.Pow(b1 - b2, 2));
		}

		//Ukoliko se izabere slika sa visokom rezolucijom koja ima velike predele sa homogenom bojom dolazi do stack overflowa, my guess je da je zbog toga sto je stack ogranicen na nisku vrednost a nisam nasao nacin kako da rucno povecam alokaciju steka
		//Za sliku 32x32 radi kako treba, nisam probao za vece vrednosti, i za sliku Lenna sa rezolucijom 512x512 sliku sa thresholdom <30
		//Najveci faktor koji utice na oveflow izgleda da je velicina parametra threshold
		public static bool EqualizeColors(Bitmap b, int x, int y, int threshold, byte newR, byte newG, byte newB)
		{
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				int index1D = y * stride + x * 3;

				byte oldB = p[index1D],
					oldG = p[index1D+1],
					oldR = p[index1D+2];
				p[index1D] = newB;
				p[index1D + 1] = newG;
				p[index1D + 2] = newR;
				bool[,] processed = new bool[b.Height,b.Width];
				processed[y,x]=true;

				for (int offsetY = -1; offsetY <= 1; offsetY++)
					if (y + offsetY >= 0 && y + offsetY < b.Height)
						for (int offsetX = -1; offsetX <= 1; offsetX++)
							if (x + offsetX >= 0 && x + offsetX < b.Width)
								if (!(offsetY == 0 && offsetX == 0))
									if (!processed[y + offsetY,x + offsetX])
									{
										int newIndex1D = (y + offsetY) * stride + (x + offsetX) * 3;
										byte currentR = p[newIndex1D + 2],
											currentG = p[newIndex1D + 1],
											currentB = p[newIndex1D];
										if (Similarity(oldR, oldG, oldB, currentR, currentG, currentB) <= threshold)
											ColorizeRecursive(ref bmData, ref processed, x + offsetX, y + offsetY, threshold, newR, newG, newB, oldR, oldG, oldB);
									}
			}

			b.UnlockBits(bmData);

			return true;
		}

		private static bool ColorizeRecursive(ref BitmapData bmData,ref bool[,] processed, int x, int y, int threshold, byte newR, byte newG, byte newB, byte originalR, byte originalG, byte originalB)
		{
			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				int index1D = y * stride + x * 3;

				p[index1D] = newB;
				p[index1D + 1] = newG;
				p[index1D + 2] = newR;


				processed[y, x] = true;

				for (int offsetY = -1; offsetY <= 1; offsetY++)
					if (y + offsetY >= 0 && y + offsetY < bmData.Height)
						for (int offsetX = -1; offsetX <= 1; offsetX++)
							if (x + offsetX >= 0 && x + offsetX < bmData.Width)
								if (!(offsetY == 0 && offsetX == 0))
									if (!processed[y + offsetY, x + offsetX])
									{
										int newIndex1D = (y + offsetY) * stride + (x + offsetX) * 3;
										byte currentR = p[newIndex1D + 2],
											currentG = p[newIndex1D + 1],
											currentB = p[newIndex1D];
										if (Similarity(originalR, originalG, originalB, currentR, currentG, currentB) <= threshold)
											ColorizeRecursive(ref bmData,ref processed, x + offsetX, y + offsetY, threshold, newR, newG, newB, originalR, originalG, originalB);
									}
			}
			return true;
		}

		public static bool Downsample(Bitmap b)
        {
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;
				for (int y = 0; y < b.Height-1; y+=2)
				{
					for (int x = 0; x < b.Width-1; x+=2)
					{
						int index1DUL = y * stride + x * 3 ;
						int index1DUR = y * stride + (x+1) * 3 ;
						int index1DDL = (y+1) * stride + x * 3 ;
						int index1DDR = (y+1) * stride + (x+1) * 3 ;

						for(int channel = 0; channel < 3; channel++)
                        {
							p[index1DUR + channel] = p[index1DUL + channel];
							p[index1DDL + channel] = p[index1DUL + channel];
							p[index1DDR + channel] = p[index1DUL + channel];
						}

					}
					
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
	}
}