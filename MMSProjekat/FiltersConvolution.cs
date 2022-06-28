using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace MMSProjekat
{
    public class FiltersConvolution
    {
        public static int[,] EmbossLaplacian3x3 = new int[3,3]{ 
                                                    { -1, 0, -1 },
                                                    { 0, 4, 0 },
                                                    { -1, 0, -1 } 
                                                  };
        public static int[,] EmbossLaplacian5x5 = new int[5, 5]{
                                                    { -1, -1, 0, 0, -1 },
                                                    { 0, -1, 0, -1, -1 },
                                                    { 0, 0, 4, 0, 0 },
                                                    { -1, -1, 0, -1, 0 },
                                                    { -1, -1, 0, -1, -1 },
                                                  };
        public static int[,] EmbossLaplacian7x7 = new int[7, 7]{
                                                    { -1, -1, 0, 0, 0, 0, -1 },
                                                    { 0, -1, -1, 0, 0, -1, -1 },
                                                    { 0, 0, -1, 0, -1, -1, 0},
                                                    { 0, 0, 0, 4, 0, 0, 0},
                                                    { 0, -1, -1, 0, -1, 0, 0 },
                                                    { -1, -1, -1, 0, -1, -1, 0 },
                                                    { -1, 0, 0, 0, 0, -1, -1 },
                                                  };
        public class ConvMatrix
        {
            public int[,] Matrix;
			public int Height, Width;
			public int Factor = 1;
            public int Offset = 0;

            public int this[int i, int j]
            {
                get { return Matrix[i,j]; }
                set { Matrix[i, j] = value; }
            }

            public ConvMatrix(int[,] mat,int n)
            {
                if (mat != null && n>0) {
					Height = Width = n;
                    Matrix = mat;
                }
            }
        }
        public static bool ConvolutionFilter(Bitmap b, ConvMatrix m, bool usePreviouslyCalculatedValues = false)
		{
			if (0 == m.Factor) return false;


			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            //Efekat da se predhodno racunate konvolucione vrednosti koriste za generisanje narednih dobijamo tako sto za izvor slike stavimo odrediste
			Bitmap bSrc = (Bitmap)b.Clone();
            BitmapData bmSrc = usePreviouslyCalculatedValues ? bmData : bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;

			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr SrcScan0 = bmSrc.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				byte* pSrc = (byte*)(void*)SrcScan0;
                int nOffset = stride - b.Width * 3;
                
                for (int y = 0; y < b.Height; y++)
                {
                    for (int x = 0; x < b.Width; x++)
                    {
                        for (int channel = 0; channel < 3; channel++)
                        {
                            int channelSum = 0;
                            int baseIndex1D = y * stride + x * 3;
                            for (int matrixY = -m.Height / 2; matrixY <= (m.Height / 2); matrixY++)
                            {
                                for (int matrixX = -m.Width / 2; matrixX <= (m.Width / 2); matrixX++)
                                {
                                    int sourceX = x + matrixX;
                                    int sourceY = y + matrixY;

                                    if (!CorrectIndices(ref sourceX, ref sourceY, b.Width, b.Height))
                                        continue;
                                    int offsetedIndex1D = sourceY * stride + sourceX*3;
                                    channelSum += pSrc[offsetedIndex1D + channel] * m[matrixY + m.Height >> 2, matrixX + m.Width >> 2];
                                
                                }
                            }
                            p[baseIndex1D+channel] = (byte)Math.Max(0, Math.Min(255, channelSum / m.Factor + m.Offset));
                        }
                    }
                }
            }
			b.UnlockBits(bmData);
            if(!usePreviouslyCalculatedValues)
    			bSrc.UnlockBits(bmSrc);

			return true;
		}

        private static bool CorrectIndices(ref int indX,ref int indY, int width, int height)
        {
            
            if ((indX < 0 || indX >=  width) && (indY < 0 || indY >= height))// if the indices of the extended matrix are in one of 4 corners then skip the iteration (has the same effect as treating that pixel like its value is 0)
                return false; 

            if (indX < 0)// reflect around the left vertical boundary
                indX = 0 + -indX - 1;  

            if (indX >= width)// reflect around the right vertical boundary
                indX = width - 1 - (indX - (width - 1)) + 1; 

            if (indY < 0)// reflect around the top horizontal boundary
                indY = 0 + -indY - 1;

            if (indY >= height) // reflect around the bottom horizontal boundary
                indY = height - 1 - (indY - (height - 1)) + 1;

            return true;
        }
    }
}
