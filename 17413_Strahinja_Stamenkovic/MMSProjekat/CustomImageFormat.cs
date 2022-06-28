using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSProjekat
{
    public class CustomImageFormat
    {
        //Ideja je cuvati 1d reprezentaciju matrice gde se za svaki kanal cuva 1 byte,
        //Format pixela ce biti cmy i citace se po tom redosledu
        //Klasa ce imati metode za konverziju iz i u Bitmap format,
        //Kao i za cuvanje u fajl i citanje iz njega
        public byte[] ImageData { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public CustomImageFormat()
        {
            ImageData = null;
            Width = -1;
            Height = -1;
        }
        public CustomImageFormat(int w,int h)
        {
            Width = w;
            Height = h;
            ImageData = new byte[w*h*3];
        }
        public CustomImageFormat(Bitmap b)
        {

        }

        //Iz nekog razloga ne konvertuje lepo u format, iako sam prilicno siguran da sam nema gresaka u kodu
        //Svakako i downsample radi kao i kompresovani upis u fajl i citanje iz fajla
        public bool FromBitmap(ref Bitmap b)
        {
            Height = b.Height;
            Width = b.Width;
            ImageData = new byte[Width * Height * 3];

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);
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
                        byte red = p[2];
                        byte green = p[1];
                        byte blue = p[0];

                        byte cyan = (byte)(255 - red);
                        byte magenta = (byte)(255 - green);
                        byte yellow = (byte)(255 - blue);

                        ImageData[y * Height + x * 3] = cyan;
                        ImageData[y * Height + x * 3 + 1] = magenta;
                        ImageData[y * Height + x * 3 + 2] = yellow;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }
        public Bitmap ToBitmap()
        {
            Bitmap b = new Bitmap(Width, Height);

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
                        byte red = (byte)(255 - ImageData[y * Height + x * 3]);
                        byte green = (byte)(255 - ImageData[y * Height + x * 3 + 1]);
                        byte blue = (byte)(255 - ImageData[y * Height + x * 3 + 2]);

                        p[0] = blue;
                        p[1] = green;
                        p[2] = red;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);
            return b;
        }
       
        public bool Downsample(CMY channelToKeep)
        {
            for (int y = 0; y < Height - 1; y += 2)
            {
                for (int x = 0; x < Width - 1; x += 2)
                {
                    int index1DUL = y * Height + x * 3;
                    int index1DUR = y * Height + (x + 1) * 3;
                    int index1DDL = (y + 1) * Height + x * 3;
                    int index1DDR = (y + 1) * Height + (x + 1) * 3;

                    for (int channel = 0; channel < 3; channel++)
                    {
                        if (channel != (Math.Abs((int)channelToKeep - 3) % 3))
                        {
                            ImageData[index1DUR + channel] = ImageData[index1DUL + channel];
                            ImageData[index1DDL + channel] = ImageData[index1DUL + channel];
                            ImageData[index1DDR + channel] = ImageData[index1DUL + channel];
                        }
                    }

                }
            }
            return true;
        }

        public bool SaveAndCompress(string path)
        {
            
            uint byteCount = (uint)(Height * Width * 3);
            byte[] compressedData = new byte[byteCount * (101 / 100) + 384];

            int compressedDataSize = ShannonFano.Compress(ImageData, compressedData, byteCount);

            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                bw.Write(Height);
                bw.Write(Width);
                bw.Write(compressedData);
            }

            return true;
        }
        public bool ReadAndDecompress(string path)
        {
            
            using (BinaryReader bw = new BinaryReader(File.Open(path,FileMode.Open)))
            {
                Height=bw.ReadInt32();
                Width=bw.ReadInt32();
                uint byteCount = (uint)(Height * Width * 3);
                ImageData = new byte[byteCount];
                byte[] compressedData = bw.ReadBytes((int)(byteCount * (101 / 100) + 384));
                ShannonFano.Decompress(compressedData, ImageData, (uint)(byteCount * (101 / 100) + 384), byteCount);
            }

            return true;
        }
    }
}
