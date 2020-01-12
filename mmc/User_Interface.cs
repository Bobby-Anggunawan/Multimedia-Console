using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.IO;

namespace mmc
{
    class User_Interface
    {


        public static void mmc_Menu() {
            Console.WriteLine("Multimedia Console\n");

            Console.WriteLine("Pilih Menu");
            Console.WriteLine("1. Menampilkan daftar file");
            Console.WriteLine("2. Menjalankan file media");
            //Console.WriteLine("3. Convert video ke format .tarsier");
            //Console.WriteLine("4. Clear screen");
            Console.WriteLine("3. Clear screen");

            int stat = 0;
Lable1:
            Console.Write("Pilihan anda = ");
            try
            {
                stat = Convert.ToInt32(Console.ReadLine());
            }
            catch {
                Console.WriteLine("Masukkan harus angka");
                goto Lable1;
            }
            switch (stat) {
                case 1:
                    Essential.List_File alist = new Essential.List_File();
                    alist.Print_File_List();
                    break;
                case 2:
                    string path;
                    Console.Write("Masukkan path ke file = ");
                    path = Console.ReadLine();
                    Core.Play(path);
                    break;
                case 3:
                    Color pixel = Color.Black;
                    Console.Write("\x1b[48;2;{0};{1};{2}m\n", pixel.R, pixel.G, pixel.B);
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Pilihan tidak tersedia");
                    break;
            }

        }



        public class cmd {
            public static void Args_Handler(string[] args) {
                switch (args.Length) {
                    case 0:
                        while (true) {
                            mmc_Menu();
                        }
                        break;
                    case 1:
                        switch (args[0]) {
                            case "ls":
                                Essential.List_File alist = new Essential.List_File();
                                alist.Print_File_List();
                                break;
                            case "clear":
                                Color pixel = Color.Black;
                                Console.Write("\x1b[48;2;{0};{1};{2}m\n", pixel.R, pixel.G, pixel.B);
                                Console.Clear();
                                break;
                            default:
                                try
                                {
                                    Core.Play(args[0]);
                                }
                                catch {
                                    Console.WriteLine("Perintah tidak dikenali");
                                }
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Perintah tidak dikenali");
                        break;
                }
            }
        }


        public class GraphicalInterface
        {
            static Essential.List_File File = new Essential.List_File();
            static string PlayPath;

            public static void MainMenu() {
                Console.SetCursorPosition((Console.WindowWidth / 2)-(18/2)+2, 1);
                Console.WriteLine("Multimedia Console");
                Console.SetCursorPosition((Console.WindowWidth / 2) - (4 / 2)+2, 2);
                Console.WriteLine("Menu");
                SwitchButton Buttons = new SwitchButton();

                Buttons.CreatButton("Media Library", MediaLiblary, (Console.WindowWidth/2)-(13/2), 5);
                Buttons.CreatButton("View File", ViewFileIn, (Console.WindowWidth / 2)-(9/2), 9);
                Buttons.CreatButton("Exit", Exit, (Console.WindowWidth / 2)-(4/2), 13);
            }

            public static void MediaLiblary() {
                SwitchButton Buttons = new SwitchButton();

                int pos_y = 0;
                int pos_x = 0;
                int TextLength = 15;        //Panjang maksimum karakter yang ditampilkan

                for (int x = 0; x < File.name_list.Count; x++)
                {
                    string Nama;

                    if (File.name_list[x].Length > TextLength) Nama = File.name_list[x].Substring(0, TextLength - 2) + "..";
                    else Nama = File.name_list[x].PadRight(TextLength);

                    Buttons.CreatButton(Nama, ViewFile, pos_x * (TextLength + 6), pos_y); //5 = padL(2)+padR(2)+JarakAntarTombol

                    pos_x = (pos_x + 1) % 5; //5 adalah jumlah kolom
                    if (pos_x == 0) pos_y += 4;
                }
            }

            static void ViewFile() {
                var FileInfo_ = new FileInfo(File.path_list[SwitchButton.LastActive]);
                string Nama = FileInfo_.Name;
                string Path = FileInfo_.FullName;
                string Format = FileInfo_.Extension;
                Essential.Media_Type JenisMedia = Essential.File_Get_Type(Path);

                Console.WriteLine("Nama       : {0}", Nama);
                Console.WriteLine("FullPath   : {0}", Path);
                Console.WriteLine("Format     : {0}", Format);
                Console.WriteLine("Jenis File : {0}", Convert.ToString(JenisMedia));

                SwitchButton Buttons = new SwitchButton();
                Buttons.CreatButton("Run", Play, 0, 7);
                Buttons.CreatButton("Back", MediaLiblary, 8, 7);
            }

            static void ViewFile(string FileLoc)
            {
                Console.Clear();
                var FileInfo_ = new FileInfo(FileLoc);
                string Nama = FileInfo_.Name;
                string Path = FileInfo_.FullName;
                string Format = FileInfo_.Extension;
                Essential.Media_Type JenisMedia = Essential.File_Get_Type(Path);

                Console.WriteLine("Nama       : {0}", Nama);
                Console.WriteLine("FullPath   : {0}", Path);
                Console.WriteLine("Format     : {0}", Format);
                Console.WriteLine("Jenis File : {0}", Convert.ToString(JenisMedia));

                SwitchButton Buttons = new SwitchButton();
                Buttons.CreatButton("Run", PlayFrom, 0, 7);
                Buttons.CreatButton("Back", MainMenu, 8, 7);
            }

            static void ViewFileIn() {
                Console.Write("Masukkan path ke file: ");
                string Path = Console.ReadLine();
                PlayPath = Path;
                ViewFile(Path);

            }

            static void Play() {
                Console.WriteLine("Loading...");
                Core.Play(File.path_list[SwitchButton.LastActive]);
                Console.ReadKey();

                Console.Clear();
                MainMenu();
            }

            static void PlayFrom() {
                Console.WriteLine("Loading...");
                Core.Play(PlayPath);
                Console.ReadKey();

                Console.Clear();
                MainMenu();
            }

            static void Exit() {
                Environment.Exit(0);
            }
        }


    }
}
