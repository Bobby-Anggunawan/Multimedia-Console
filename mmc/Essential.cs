using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace mmc
{

    //Class yang menyediakan fungsi dan property untuk membantu class lain
    class Essential
    {
        public static int panjang_layar{    //Panjang layar dibagi 2 karena saat gambar ditampilkan di console, kami menggunakan 2X spasi untuk menggambarkan 1 pixel
            get
            {
                return Console.WindowWidth / 2;
            }
        }

        public static int lebar_layar{
            get
            {
                return Console.WindowHeight;
            }
        }

        public static int TimeSpan_To_Seconds(TimeSpan interval)    //Mengkonversi objek timespan(interval waktu) menjadi detik
        {
            return interval.Days * 24 * 60 * 60 +
                   interval.Hours * 60 * 60 +
                   interval.Minutes * 60 +
                   interval.Seconds;
        }

        public static Int64 TimeSpan_To_MiliSeconds(TimeSpan interval)  //Mengkonversi objek timespan(interval waktu) menjadi milidetik
        {
            return interval.Days * 24 * 60 * 60 * 1000 +
                   interval.Hours * 60 * 60 * 1000 +
                   interval.Minutes * 60 * 1000 +
                   interval.Seconds * 1000 +
                   interval.Milliseconds;
        }

        public static void Execute_FFMPEG(string command, bool wait)    //Mengeksekusi ffmpeg.exe
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = @"..\..\ffmpeg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = command;

            Process exeProcess = Process.Start(startInfo);
            if (wait) exeProcess.WaitForExit();
        }


    }
}
