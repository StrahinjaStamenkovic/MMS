using System;
using System.IO;
using System.Collections;
using System.Drawing;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for BitMapBytes.
	/// </summary>
	public class BitMap8Bytes
	{
		public BitMap8Bytes()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		

		public BitMap8Bytes(string fileName) : this()
		{			
			this.fromFile(fileName);
		}

		byte[] bytes;

		int imageStartIndex = 0;
		int height = 0;
		int width = 0;

		public byte[] Bytes
		{
			get
			{
				return this.bytes;
			}
			set
			{
				this.bytes = value;
			}
		}

		public int ImageStartIndex
		{
			get
			{
				return this.imageStartIndex;
			}
			
		}

		public int Height
		{
			get
			{
				return this.height;
			}
			
		}

		public int Width
		{
			get
			{
				return this.width;
			}
			
		}

		ArrayList colors = new ArrayList();

		public int GetColorCount()
		{
			return this.colors.Count;
		}

		public Color GetColor(int index)
		{
			return (Color)this.colors[index];
		}

		public void SetColor(Color newColor, int index)
		{
			bytes[54 + index * 4] = newColor.R;
			bytes[54 + index * 4 + 1] = newColor.G;
			bytes[54 + index * 4 + 2] = newColor.B;
		}

		public void ToFile(string fileName)
		{
			if (bytes.Length > 0)
			{
				FileStream b = File.OpenWrite(fileName);
				b.Write(bytes, 0, bytes.Length);
				b.Close();
			}
		}

		public void fromFile(string fileName)
		{
			FileStream fsr = System.IO.File.OpenRead(fileName);
			byte[] a = new byte[fsr.Length];
			fsr.Read(a, 0, Convert.ToInt32(fsr.Length));
			
			this.imageStartIndex = a[10] + 256 * (a[11] + 256 * (a[12] + 256 * a[13]));
			this.width = a[18] + 256 * (a[19] + 256 * (a[20] + 256 * a[21]));
			this.height = a[22] + 256 * (a[23] + 256 * (a[24] + 256 * a[25]));
			
			this.bytes = (byte[])a.Clone();

			for (int i = 54; i < this.imageStartIndex; i+=4)
			{
				this.colors.Add(Color.FromArgb(a[i], a[i+1], a[i+2]));
			}
			
			fsr.Close();
		}

		public int GetMostSimilarColor(Color c)
		{
			int dif = 768;
			int difIndex = -1;

			for (int i = 0; i < colors.Count; i++)
			{
				Color cc = (Color) colors[i];
				int ndif = Math.Abs(c.R - cc.R) + Math.Abs(c.G - cc.G) + Math.Abs(c.B - cc.B); 				
				if (ndif < dif)
				{
					dif = ndif;
					difIndex = i;
				}
			}

			return difIndex;
		}
	}
}
