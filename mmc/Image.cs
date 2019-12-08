using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace mmc
{

    //Class yang menampung semua fungsi untuk memproses gambar
    class Image
    {
        public static void Draw_Image(string filePath)  //Menampilkan isi dari file gambar ke layar
        {
            int kebawah = Essential.lebar_layar, kesamping = Essential.panjang_layar;
            Bitmap gambar = new Bitmap(filePath);
            Color pixel;

            gambar.RotateFlip(RotateFlipType.Rotate90FlipX);

            int jlh_titik_kebawah = gambar.Width / kebawah;
            int jlh_titik_kesamping = gambar.Height / kesamping;

            try
            {
                for (int x = 0; x < kebawah; x++)
                {
                    for (int y = 0; y < kesamping; y++)
                    {

                        int j = (x * jlh_titik_kebawah);
                        int k = (y * jlh_titik_kesamping);

                        pixel = gambar.GetPixel(j, k);
                        Console.Write("\x1b[48;2;{0};{1};{2}m  ", pixel.R, pixel.G, pixel.B);
                    }

                    Console.Write("\n");
                }
            }
            catch
            {
                Console.WriteLine("\n\nImage\twidth = {0}\n\theight = {1}", gambar.Width, gambar.Height);
                Console.WriteLine("Print\twidth = {0}\n\theight = {1}", kesamping * jlh_titik_kesamping, kebawah * jlh_titik_kebawah);
            }

        }


        public static bool Get_Frame(string filePath, string time)      //Mengambil frame dari video pada waktu ke {0h:0m:0s.0ms} dan disimpan pada directory ini dengan nama Gambar.jpg
        {
            string param = String.Join("", "\"", filePath, "\"");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = @"..\..\ffmpeg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = "-i " +param+ " -vf scale=100:60 -ss " + time + " -vframes 1 gambar.jpg";
            startInfo.Arguments = "-i " + param + " -ss " + time + " -vframes 1 gambar.jpg";

            startInfo.RedirectStandardInput = true;

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    StreamWriter myStreamWriter = exeProcess.StandardInput;
                    myStreamWriter.Write("y");

                    myStreamWriter.Close();
                    exeProcess.WaitForExit();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
