using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace mmc
{

    //Class yang menyediakan class, fungsi, property, dan enum untuk membantu class lain
    class Essential
    {
        //===========================================================================================================================
        //Enum
        public enum Media_Type { picture = 1, audio = 2, video = 3, undefined = 4 }

        //===========================================================================================================================
        //Property
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

        //===========================================================================================================================
        //Fungsi
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

        public static Media_Type File_Get_Type(string path)
        {
            FileInfo file = new FileInfo(path);
            switch (file.Extension.ToLower())
            {
                case ".jpeg":
                case ".gif":
                case ".bmp":
                case ".png":
                    return Media_Type.picture;
                    break;
                case ".aac":
                case ".m4a":
                case ".mp3":
                case ".ogg":
                case ".wav":
                case ".wma":
                    return Media_Type.audio;
                    break;
                case ".3gp":
                case ".webm":
                case ".mkv":
                case ".flv":
                case ".vob":
                case ".avi":
                case ".mov":
                case ".wmv":
                case ".mp4":
                case ".mpg":
                case ".mpeg":
                case ".m4v":
                    return Media_Type.video;
                    break;
                default:
                    return Media_Type.undefined;
            }
        }

        //===========================================================================================================================
        //Class
        public class List_File  //Menampilkan dan menyimpan list file media
        {

            public List_File()
            {
                this.Get_List();
            }

            public List<string> path_list = new List<string>();
            public List<string> name_list = new List<string>();


            public void Get_List()
            {
                string name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string user = @"C:\Users" + name.Substring(name.IndexOf(@"\"));

                //Picture
                DirectoryInfo picture = new DirectoryInfo(user + @"\Pictures");
                foreach (FileInfo file in picture.GetFiles())
                {
                    if (Is_Media(file))
                    {
                        path_list.Add(file.FullName);
                        name_list.Add(file.Name);
                    }

                }

                //Video
                DirectoryInfo video = new DirectoryInfo(user + @"\Videos");
                foreach (FileInfo file in video.GetFiles())
                {
                    if (Is_Media(file))
                    {
                        path_list.Add(file.FullName);
                        name_list.Add(file.Name);
                    }

                }

                //document
                DirectoryInfo documen = new DirectoryInfo(user + @"\Documents");
                foreach (FileInfo file in documen.GetFiles())
                {
                    if (Is_Media(file))
                    {
                        path_list.Add(file.FullName);
                        name_list.Add(file.Name);
                    }

                }

                //music
                DirectoryInfo music = new DirectoryInfo(user + @"\Music");
                foreach (FileInfo file in music.GetFiles())
                {
                    if (Is_Media(file))
                    {
                        path_list.Add(file.FullName);
                        name_list.Add(file.Name);
                    }

                }
            }


            public void Print_File_List()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var a in name_list)
                {
                    Console.WriteLine("\t"+a);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }

            public static bool Is_Media(FileInfo file)
            {
                switch (file.Extension.ToLower())
                {
                    case ".jpeg":
                    case ".gif":
                    case ".bmp":
                    case ".png":
                    case ".3gp":
                    case ".aac":
                    case ".m4a":
                    case ".mp3":
                    case ".ogg":
                    case ".wav":
                    case ".wma":
                    case ".webm":
                    case ".mkv":
                    case ".flv":
                    case ".vob":
                    case ".avi":
                    case ".mov":
                    case ".wmv":
                    case ".mp4":
                    case ".mpg":
                    case ".mpeg":
                    case ".m4v":
                        return true;
                    default:
                        return false;
                }
            }
        }

    }
}
