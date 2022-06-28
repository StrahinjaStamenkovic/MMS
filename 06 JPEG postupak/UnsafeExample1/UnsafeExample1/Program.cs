using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsafeExample1
{
    // fastcopy.cs
    // compile with: /unsafe
    using System;
    using System.IO;
using System.Runtime.InteropServices;

    

    class Test
    {
        [DllImport("kernel32.dll")]
        static extern unsafe void CopyMemory(IntPtr destination, IntPtr source, uint length);

        // The unsafe keyword allows pointers to be used within
        // the following method:
        static unsafe void Copy(byte[] src, int srcIndex,
            byte[] dst, int dstIndex, long count)
        {
            if (src == null || srcIndex < 0 ||
                dst == null || dstIndex < 0 || count < 0)
            {
                throw new ArgumentException();
            }
            int srcLen = src.Length;
            int dstLen = dst.Length;
            if (srcLen - srcIndex < count ||
                dstLen - dstIndex < count)
            {
                throw new ArgumentException();
            }


            // The following fixed statement pins the location of
            // the src and dst objects in memory so that they will
            // not be moved by garbage collection.          
            fixed (byte* pSrc = src, pDst = dst)
            {
                byte* ps = pSrc;
                byte* pd = pDst;

                // Loop over the count in blocks of 4 bytes, copying an
                // integer (4 bytes) at a time:
                for (int n = 0; n < count / 4; n++)
                {
                    *((int*)pd) = *((int*)ps);
                    pd += 4;
                    ps += 4;
                }

                // Complete the copy by moving any bytes that weren't
                // moved in blocks of 4:
                for (int n = 0; n < count % 4; n++)
                {
                    *pd = *ps;
                    pd++;
                    ps++;
                }
            }
        }


        static void Main(string[] args)
        {
            byte[] a = new byte[100];
            byte[] b = new byte[100];
            for (int i = 0; i < 100; ++i)
                a[i] = (byte)i;
            Copy(a, 0, b, 0, 100);

            Console.WriteLine("The first 10 elements are:");

            for (int i = 0; i < 10; ++i)
                Console.Write(b[i] + " ");
            Console.WriteLine("\n");

            

            FileStream fOpen = File.OpenRead(@"C:\Song.wav");
            Console.WriteLine(string.Format("{0} \n", fOpen.Length));

            byte[] c = new byte[fOpen.Length];
            fOpen.Read(c, 0, (int)fOpen.Length);

            byte[] d = new byte[fOpen.Length];
            byte[] e = new byte[fOpen.Length];
            byte[] f = new byte[fOpen.Length];
            byte[] g = new byte[fOpen.Length];
            long c1 = DateTime.Now.Ticks;

           
            Copy(c, 0, d, 0, fOpen.Length);

            long c2 = DateTime.Now.Ticks;
            Console.WriteLine(string.Format("Safe copy byte-by-byte {0} \n", c2 - c1));
            
            for(int i = 0; i < fOpen.Length; i++)
            {
                e[i] = c[i];
            }

            long c3 = DateTime.Now.Ticks;

            Console.WriteLine(string.Format("Unsafe copy byte-by-byte {0} \n", c3 - c2));
            c.CopyTo(f, 0);

            long c4 = DateTime.Now.Ticks;
            Console.WriteLine(string.Format("Copy method {0} \n", c4 - c3));

            CopyBytes(fOpen, c, g);

            Console.WriteLine(string.Format("CopyMemory {0} \n", DateTime.Now.Ticks - c4));
            
        }

        public static unsafe void CopyBytes(FileStream fOpen, byte[] c, byte[] g)
        {
             fixed (byte* pd = g) { fixed (byte* ps = c) { CopyMemory((IntPtr)pd, (IntPtr)ps, Convert.ToUInt32(fOpen.Length)); } }
            
        }
    }
}
