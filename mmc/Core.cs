using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace mmc
{
    class Core
    {
        public static void Play(string filePath)
        {

            Essential.List_File alist = new Essential.List_File();
            if (alist.name_list.IndexOf(filePath) != -1)
            {
                switch (Essential.File_Get_Type(alist.path_list[alist.name_list.IndexOf(filePath)]))
                {
                    case Essential.Media_Type.audio:
                        try
                        {
                            Audio.Get_Audio_And_Play(alist.path_list[alist.name_list.IndexOf(filePath)]);
                        }
                        catch {
                            Console.WriteLine("Error");
                        }
                        break;
                    case Essential.Media_Type.picture:
                        try
                        {
                            Image.Draw_Image(alist.path_list[alist.name_list.IndexOf(filePath)]);
                        }
                        catch
                        {
                            Console.WriteLine("Error");
                        }
                        break;
                    case Essential.Media_Type.video:
                        try
                        {
                            Video.Play_Video(alist.path_list[alist.name_list.IndexOf(filePath)]);
                        }
                        catch
                        {
                            Console.WriteLine("Error");
                        }
                        break;
                    default:
                        Console.WriteLine("File tidak bisa ditemukan");
                        break;
                }
            }

            else {
                switch (Essential.File_Get_Type(filePath))
                {
                    case Essential.Media_Type.audio:
                        Audio.Get_Audio_And_Play(filePath);
                        break;
                    case Essential.Media_Type.picture:
                        Image.Draw_Image(filePath);
                        break;
                    case Essential.Media_Type.video:
                        Video.Play_Video(filePath);
                        break;
                    default:
                        Console.WriteLine("File tidak dapat dijalankan");
                        break;
                }
            }

            Color pixel = Color.Black;
            Console.Write("\x1b[48;2;{0};{1};{2}m\n", pixel.R, pixel.G, pixel.B);


        }
    }
}
