using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace mmc
{
    class User_Interface
    {


        public static void mmc_Menu() {
            Console.WriteLine("Multimedia Console\n");

            Console.WriteLine("Pilih Menu");
            Console.WriteLine("1. Menampilkan daftar file");
            Console.WriteLine("2. Menjalankan file media");
            Console.WriteLine("3. Convert video ke format .tarsier");
            Console.WriteLine("4. Clear screen");

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
                    //otw
                    break;
                case 4:
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

    }
}
