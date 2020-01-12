using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


using static System.Console;
using System.Drawing;                   //Untuk memproses file gambar agar bisa ditampilkan di console

namespace mmc
{
    class Program
    {
        //==================================================================================
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);
        //====================================================================================


        static void Main(string[] args)
        {
            //===========================================================================
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
            //==========================================================================

            //Video.Play_Video(@"D:\Climax Form.mp4", 10);
            //Core.Play("Tak berjudul 2 960x540 2,14Mbps 2019-08-01 15-55-22.mp4");

            //Core.Play(@"C:\Users\Asus X441M\Videos\Tak berjudul 2 960x540 2,14Mbps 2019-08-01 15-55-22.mp4");
            //Video.Convert_To_Tarsier(@"D:\Super Climax.mp4", "deno.tarsier", 5, 50, 100);
            //Video.Play_My_Video_Format("deno.tarsier");



            //User_Interface.cmd.Args_Handler(args);

            Console.CursorVisible = false;
            User_Interface.GraphicalInterface.MainMenu();

            //ReadKey();
        }
    }
}
