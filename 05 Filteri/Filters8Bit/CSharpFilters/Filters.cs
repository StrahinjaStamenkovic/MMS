using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CSharpFilters
{
	
	
	public class BitmapFilter
	{
		public const short EDGE_DETECT_KIRSH		= 1;
		public const short EDGE_DETECT_PREWITT		= 2;
		public const short EDGE_DETECT_SOBEL		= 3;

		public static string filepath = "";

		
		public static bool InvertImg(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;

				int nOffset = stride - b.Width;
				int nWidth = b.Width;
	
				for(int y=0;y<b.Height;++y)
				{
					for(int x=0; x < nWidth; ++x )
					{
						p[0] = (byte)(255-p[0]);
						++p;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}

		public static bool Invert(Bitmap b)
		{
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				int red, green, blue;

				red = 255 - c.R;
				green = 255 - c.G;
				blue = 255 - c.B;				

				bm8.SetColor(System.Drawing.Color.FromArgb(red, green, blue), i);
			}

			using (MemoryStream ms = new MemoryStream(bm8.Bytes))
			{
				Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
				b.Palette = bm2.Palette;
			}
					
			return true;
			
		}


		public static bool GrayScale(Bitmap b, double redR, double greenR, double blueR)
		{
			if (redR < -255 || redR > 255) return false;
			if (greenR < -255 || greenR > 255) return false;
			if (blueR < -255 || blueR > 255) return false;
			
			redR = (redR + 255.0)/512.0;
			greenR = (greenR + 255.0)/512.0;
			blueR = (blueR + 255.0)/512.0;

			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				int rgb;

				rgb = System.Convert.ToInt32(redR * c.R + greenR * c.G + blueR * c.B);

				if (rgb > 255) rgb = 255;
				if (rgb < 0) rgb = 0;

				bm8.SetColor(System.Drawing.Color.FromArgb(rgb, rgb, rgb), i);
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;
					
			return true;


			
		}


		public static bool BrightnessImg(Bitmap b, int nBrightness)
		{
			if (nBrightness < -255 || nBrightness > 255)
				return false;

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			int nVal = 0;

			
			unsafe
			{
				byte * p = (byte *)(void *)Scan0;

				int nOffset = stride - b.Width;
				int nWidth = b.Width;

				for(int y=0;y<b.Height;++y)
				{
					for(int x=0; x < nWidth; ++x )
					{
						nVal = (int) (p[0] + nBrightness);
		
						if (nVal < 0) nVal = 0;
						if (nVal > 255) nVal = 255;

						p[0] = (byte)nVal;

						++p;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}

		public static bool Brightness(Bitmap b, int nBrightness)
		{
			if (nBrightness < -255 || nBrightness > 255)
				return false;

			int nVal = 0;

			
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				int red, green, blue;

				nVal = c.R + nBrightness;
				if (nVal < 0) nVal = 0;
				if (nVal > 255) nVal = 255;
				red = nVal;

				nVal = c.G + nBrightness;
				if (nVal < 0) nVal = 0;
				if (nVal > 255) nVal = 255;
				green = nVal;

				nVal = c.B + nBrightness;
				if (nVal < 0) nVal = 0;
				if (nVal > 255) nVal = 255;
				blue = nVal;

				bm8.SetColor(System.Drawing.Color.FromArgb(red, green, blue), i);
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;

			return true;
		}


		public static bool DiversityTo2(Bitmap b)
		{
			int nVal = 0;

			
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				
				nVal = c.R + c.G + c.B;
				
				if (nVal < 307)
					bm8.SetColor(System.Drawing.Color.FromArgb(0, 0, 0), i);
				else
					bm8.SetColor(System.Drawing.Color.FromArgb(255, 255, 255), i);
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;

			return true;
		}

		public static bool DiversityTo4(Bitmap b)
		{
			

			
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				bm8.SetColor(ConvertToGrayLinear(c, 2), i);
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;

			return true;
		}

		public static bool DiversityTo16(Bitmap b)
		{		

			
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				bm8.SetColor(ConvertToGraySquare(c, 4), i);
			
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;

			return true;
		}


		public static System.Drawing.Color ConvertToGrayLinear(Color c, int targetPaletteBits)
		{
			int colorNum = Convert.ToInt32(Math.Pow(2, targetPaletteBits));
			int nVal = (c.R + c.G + c.B) / (3 * colorNum);
			return System.Drawing.Color.FromArgb(	(colorNum - 1) * nVal, 
													(colorNum - 1) * nVal, 
													(colorNum - 1) * nVal);
		}

		public static System.Drawing.Color ConvertToGraySquare(	Color c, 
																int targetPaletteBits)
		{
			int colorNum = Convert.ToInt32(Math.Pow(2, targetPaletteBits));
			int nVal = Convert.ToInt32(Math.Sqrt((c.R * c.R + c.G * c.G + c.B * c.B) / 3)) / colorNum;
			return System.Drawing.Color.FromArgb(	(colorNum - 1) * nVal, 
				(colorNum - 1) * nVal, 
				(colorNum - 1) * nVal);
		}

		public static bool ContrastImg(Bitmap b, sbyte nContrast)
		{
			if (nContrast < -100) return false;
			if (nContrast >  100) return false;

			double pixel = 0, contrast = (100.0+nContrast)/100.0;

			contrast *= contrast;

					
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat); // PixelFormat.Format24bppRgb);
			
			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;

				int nOffset = stride - b.Width;

				for(int y=0;y<b.Height;++y)
				{
					for(int x=0; x < b.Width; ++x )
					{
						
				
						pixel = p[0]/255.0;
						pixel -= 0.5;
						pixel *= contrast;
						pixel += 0.5;
						pixel *= 255;
						if (pixel < 0) pixel = 0;
						if (pixel > 255) pixel = 255;
						p[0] = (byte) pixel;
						p++;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
	
		public static bool Contrast(Bitmap b, sbyte nContrast)
		{
			if (nContrast < -100) return false;
			if (nContrast >  100) return false;

			double pixel = 0, contrast = (100.0+nContrast)/100.0;

			contrast *= contrast;
			
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				int red, green, blue;

				pixel = c.R/255.0;
				pixel -= 0.5;
				pixel *= contrast;
				pixel += 0.5;
				pixel *= 255;
				if (pixel < 0) pixel = 0;
				if (pixel > 255) pixel = 255;
				red = (byte) pixel;

				pixel = c.G/255.0;
				pixel -= 0.5;
				pixel *= contrast;
				pixel += 0.5;
				pixel *= 255;
				if (pixel < 0) pixel = 0;
				if (pixel > 255) pixel = 255;
				green = (byte) pixel;

				pixel = c.B/255.0;
				pixel -= 0.5;
				pixel *= contrast;
				pixel += 0.5;
				pixel *= 255;
				if (pixel < 0) pixel = 0;
				if (pixel > 255) pixel = 255;
				blue = (byte) pixel;				

				bm8.SetColor(System.Drawing.Color.FromArgb(red, green, blue), i);
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;
					
			return true;
		}
	

		public static bool Gamma(Bitmap b, double red, double green, double blue)
		{
			if (red < .02 || red > 50) return false;
			if (green < .02 || green > 50) return false;
			if (blue < .02 || blue > 50) return false;

			byte [] redGamma = new byte [256];
			byte [] greenGamma = new byte [256];
			byte [] blueGamma = new byte [256];

			for (int i = 0; i< 256; ++i)
			{
				redGamma[i] = (byte)Math.Min(255, (int)(( 255.0 * Math.Pow(i/255.0, 1.0/red)) + 0.5));
				greenGamma[i] = (byte)Math.Min(255, (int)(( 255.0 * Math.Pow(i/255.0, 1.0/green)) + 0.5));
				blueGamma[i] = (byte)Math.Min(255, (int)(( 255.0 * Math.Pow(i/255.0, 1.0/blue)) + 0.5));
			}

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			
			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;
				
				int nOffset = stride - b.Width*3;

				for(int y=0;y<b.Height;++y)
				{
					for(int x=0; x < b.Width; ++x )
					{
						p[2] = redGamma[ p[2] ];
						p[1] = greenGamma[ p[1] ];
						p[0] = blueGamma[ p[0] ];

						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}

		public static bool Color(Bitmap b, int red, int green, int blue)
		{
			if (red < -255 || red > 255) return false;
			if (green < -255 || green > 255) return false;
			if (blue < -255 || blue > 255) return false;

			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;

				

				int nOffset = stride - b.Width*3;
				int nPixel;

				for(int y=0;y<b.Height;++y)
				{
					for(int x=0; x < b.Width; ++x )
					{
						nPixel = p[2] + red;
						nPixel = Math.Max(nPixel, 0);
						p[2] = (byte)Math.Min(255, nPixel);

						nPixel = p[1] + green;
						nPixel = Math.Max(nPixel, 0);
						p[1] = (byte)Math.Min(255, nPixel);

						nPixel = p[0] + blue;
						nPixel = Math.Max(nPixel, 0);
						p[0] = (byte)Math.Min(255, nPixel);

						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
		
		public static bool RedChannel(Bitmap b)
		{
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				
				bm8.SetColor(System.Drawing.Color.FromArgb(c.R, 0, 0), i);
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;

			return true;
		}

		public static bool GreenChannel(Bitmap b)
		{
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				
				bm8.SetColor(System.Drawing.Color.FromArgb(0, c.G, 0), i);
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;

			return true;
		}

		public static bool BlueChannel(Bitmap b)
		{
			BitMap8Bytes bm8 = new BitMap8Bytes(filepath);
			
			for (int i = 0; i < bm8.GetColorCount(); i++)
			{
				Color c = bm8.GetColor(i);
				
				bm8.SetColor(System.Drawing.Color.FromArgb(0, 0, c.B), i);
			}

			MemoryStream ms = new MemoryStream(bm8.Bytes);
	
					
			Bitmap bm2 = (Bitmap)Bitmap.FromStream(ms, true);
			b.Palette = bm2.Palette;

			return true;
		}
		
		
	}
}
