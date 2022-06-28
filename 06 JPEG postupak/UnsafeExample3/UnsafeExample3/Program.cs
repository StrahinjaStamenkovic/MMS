using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsafeExample3
{
    // fastcopy.cs
    // compile with: /unsafe
    using System;
    using System.IO;
    using System.Runtime.InteropServices;



    class Test
    {
        //static void Main(string[] args)
        //{
        //    FileStream fOpen = File.OpenRead(@"C:\22JS.mp4");
        //    Console.WriteLine(string.Format("{0} \n", fOpen.Length));
        //    byte[] c = new byte[fOpen.Length];
        //    fOpen.Read(c, 0, (int)fOpen.Length);
        //    byte[] d = new byte[fOpen.Length];

        //    c.CopyTo(d, 0);

        //    FileStream s = new FileStream(@"C:\22JSA.mp4", FileMode.OpenOrCreate);
        //    s.Write(d, 0, (int)fOpen.Length);

        //    fOpen.Close();
        //    s.Close();

        //}

        //static void Main(string[] args)
        //{
        //    FileStream fOpen = File.OpenRead(@"C:\22JS.mp4");
        //    FileStream s = new FileStream(@"C:\JS22a.mp4", FileMode.OpenOrCreate);

        //    Console.WriteLine(string.Format("{0} \n", fOpen.Length));
        //    int startPos = 0;
        //    int move = (int)fOpen.Length / 100;
        //    for (int i = 0; i < 100; i++)
        //    {
        //        byte[] c = new byte[move];
        //        fOpen.Read(c, 0, move);
        //        s.Write(c, 0, move);

        //        startPos += move;
        //        Console.WriteLine(string.Format("{0}/{1} ({2}%)", move, fOpen.Length, i));
        //    }

        //    fOpen.Close();
        //    s.Close();

        //}

        //static void Main(string[] args)
        //{
        //    FileStream fOpen = File.OpenRead(@"C:\song.wav");
        //    FileStream s = new FileStream(@"C:\CRF.BMP", FileMode.Open);

        //    byte[] song = new byte[fOpen.Length];
        //    byte[] img = new byte[s.Length];

        //    fOpen.Read(song, 0, song.Length);
        //    s.Read(img, 0, img.Length);
        //    bool cond = false;
        //    int pom = 0;

        //    for (int i = song.Length - 1; i > 0 && !cond; i -= 11)
        //    {
        //        byte pByte = img[pom];
        //        song[i] = pByte;
        //        pom++;
        //        cond = pom >= img.Length;
        //    }

        //    FileStream fCr = File.OpenWrite(@"C:\songCr.wav");
        //    fCr.Write(song, 0, song.Length);
        //    fCr.Close();

        //    fOpen.Close();
        //    s.Close();

        //}

        static void Main(string[] args)
        {
            FileStream fOpen = File.OpenRead(@"C:\songCr.wav");
            FileStream s = new FileStream(@"C:\CRF.BMP", FileMode.Open);

            byte[] song = new byte[fOpen.Length];
            byte[] img = new byte[s.Length];

            fOpen.Read(song, 0, song.Length);

            bool cond = false;
            int pom = 0;
            for (int i = song.Length - 1; i > 0 && !cond; i -= 11)
            {
                byte pByte = song[i];
                img[pom] = pByte;
                pom++;
                cond = pom >= img.Length;
            }

            FileStream fCr = File.OpenWrite(@"C:\cfrdca.bmp");
            fCr.Write(img, 0, img.Length);
            fCr.Close();

            fOpen.Close();
            s.Close();

        }

        
    }
}
